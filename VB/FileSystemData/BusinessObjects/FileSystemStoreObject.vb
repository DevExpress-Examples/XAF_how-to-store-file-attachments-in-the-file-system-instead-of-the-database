Imports System
Imports System.IO
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp.Utils
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation

Namespace FileSystemData.BusinessObjects
	''' <summary>
	''' This class enables you to store uploaded files in a centralized file system location instead of the database. You can configure the file system store location via the static FileSystemDataModule.FileSystemStoreLocation property.
	''' </summary>
	<DefaultProperty("FileName")>
	Public Class FileSystemStoreObject
		Inherits BaseObject
		Implements IFileData, IEmptyCheckable

'INSTANT VB NOTE: The variable tempSourceStream was renamed since Visual Basic does not allow variables and other class members to have the same name:
		Private tempSourceStream_Renamed As Stream
		Private tempFileName As String = String.Empty
		Private Shared syncRoot As New Object()
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		Public ReadOnly Property RealFileName() As String
			Get
				If (Not String.IsNullOrEmpty(FileName)) AndAlso Oid <> Guid.Empty Then
					Return Path.Combine(FileSystemDataModule.FileSystemStoreLocation, String.Format("{0}-{1}", Oid, FileName))
				End If
				Return Nothing
			End Get
		End Property
		Protected Overridable Sub SaveFileToStore()
			If Not String.IsNullOrEmpty(RealFileName) Then
				Try
					Using destination As Stream = File.OpenWrite(RealFileName)
						FileSystemDataModule.CopyStream(TempSourceStream, destination)
						Size = CInt(destination.Length)
					End Using
				Catch exc As DirectoryNotFoundException
					Throw New UserFriendlyException(exc)
				End Try
			End If
		End Sub
		Private Sub RemoveOldFileFromStore()
			'Dennis: We need to remove the old file from the store when saving the current object.
			If (Not String.IsNullOrEmpty(tempFileName)) AndAlso tempFileName <> RealFileName Then 'B222892
				Try
					File.Delete(tempFileName)
					tempFileName = String.Empty
				Catch exc As DirectoryNotFoundException
					Throw New UserFriendlyException(exc)
				End Try
			End If
		End Sub
		Protected Overrides Sub OnSaving()
			MyBase.OnSaving()
			Guard.ArgumentNotNullOrEmpty(FileSystemDataModule.FileSystemStoreLocation, "FileSystemStoreLocation")
			SyncLock syncRoot
				If Not Directory.Exists(FileSystemDataModule.FileSystemStoreLocation) Then
					Directory.CreateDirectory(FileSystemDataModule.FileSystemStoreLocation)
				End If
			End SyncLock
			SaveFileToStore()
			RemoveOldFileFromStore()
		End Sub
		Protected Overrides Sub OnDeleting()
			'Dennis: We need to remove the old file from the store.
			Clear()
			MyBase.OnDeleting()
		End Sub
		Protected Overrides Sub Invalidate(ByVal disposing As Boolean)
			If disposing AndAlso TempSourceStream IsNot Nothing Then
				TempSourceStream.Close()
				TempSourceStream = Nothing
			End If
			MyBase.Invalidate(disposing)
		End Sub
		#Region "IFileData Members"
		Public Sub Clear() Implements IFileData.Clear
			'Dennis: When clearing the file name property we need to save the name of the old file to remove it from the store in the future. You can also setup a separate service for that.
			If String.IsNullOrEmpty(tempFileName) Then
				tempFileName = RealFileName
			End If
			FileName = String.Empty
			Size = 0
		End Sub
		<Size(260)>
		Public Property FileName() As String Implements IFileData.FileName
			Get
				Return GetPropertyValue(Of String)("FileName")
			End Get
			Set(ByVal value As String)
				SetPropertyValue("FileName", value)
			End Set
		End Property
		<Browsable(False)>
		Public Property TempSourceStream() As Stream
			Get
				Return tempSourceStream_Renamed
			End Get
			Set(ByVal value As Stream)
				'Michael: The original Stream might be closed after a while (on the web too - T160753)
				If value Is Nothing Then
					tempSourceStream_Renamed = Nothing
				Else
					If value.Length > CLng(Integer.MaxValue) Then
						Throw New UserFriendlyException("File is too long")
					End If
					tempSourceStream_Renamed = New MemoryStream(CInt(value.Length))
					FileSystemDataModule.CopyStream(value, tempSourceStream_Renamed)
					tempSourceStream_Renamed.Position = 0
				End If
			End Set
		End Property
		'Dennis: Fires when uploading a file.
		Private Sub IFileData_LoadFromStream(ByVal fileName As String, ByVal source As Stream) Implements IFileData.LoadFromStream
			Me.FileName = fileName
			TempSourceStream = source
			'Dennis: When assigning a new file we need to save the name of the old file to remove it from the store in the future.
			If String.IsNullOrEmpty(tempFileName) Then
				tempFileName = RealFileName
			End If
		End Sub
		'Dennis: Fires when saving or opening a file.
		Private Sub IFileData_SaveToStream(ByVal destination As Stream) Implements IFileData.SaveToStream
			Try
				If Not String.IsNullOrEmpty(RealFileName) Then
					If destination Is Nothing Then
						FileSystemDataModule.OpenFileWithDefaultProgram(RealFileName)
					Else
						FileSystemDataModule.CopyFileToStream(RealFileName, destination)
					End If
				ElseIf TempSourceStream IsNot Nothing Then
					FileSystemDataModule.CopyStream(TempSourceStream, destination)
				End If
			Catch exc As DirectoryNotFoundException
				Throw New UserFriendlyException(exc)
			Catch exc As FileNotFoundException
				Throw New UserFriendlyException(exc)
			End Try
		End Sub
		Private ReadOnly Property IFileData_Size() As Integer Implements IFileData.Size
			Get
				Return Size
			End Get
		End Property
		<Persistent>
		Public Property Size() As Integer
			Get
				Return GetPropertyValue(Of Integer)("Size")
			End Get
			Private Set(ByVal value As Integer)
				SetPropertyValue(Of Integer)("Size", value)
			End Set
		End Property
		#End Region
		#Region "IEmptyCheckable Members"
		Public ReadOnly Property IsEmpty() As Boolean Implements IEmptyCheckable.IsEmpty
			'T153149
			Get
				Return FileDataHelper.IsFileDataEmpty(Me) OrElse Not(Me.TempSourceStream IsNot Nothing OrElse File.Exists(RealFileName))
			End Get
		End Property
		#End Region
	End Class
End Namespace
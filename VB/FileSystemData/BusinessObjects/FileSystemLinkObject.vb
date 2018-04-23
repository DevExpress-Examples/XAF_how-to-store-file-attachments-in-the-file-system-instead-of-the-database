Imports System
Imports System.IO
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Validation

Namespace FileSystemData.BusinessObjects
	''' <summary>
	''' This class enables you to add soft links to real files instead of saving their contents to the database. It is intended for use in Windows Forms applications only.
	''' </summary>
	<DefaultProperty("FileName")>
	Public Class FileSystemLinkObject
		Inherits BaseObject
		Implements IFileData, IEmptyCheckable, ISupportFullName

		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		#Region "IFileData Members"
		<Size(260), Custom("AllowEdit", "False")>
		Public Property FileName() As String Implements IFileData.FileName
			Get
				Return GetPropertyValue(Of String)("FileName")
			End Get
			Set(ByVal value As String)
				SetPropertyValue("FileName", value)
			End Set
		End Property
		Private Sub IFileData_Clear() Implements IFileData.Clear
			Size = 0
			FileName = String.Empty
		End Sub
		'Dennis: Fires when uploading a file.
		Private Sub IFileData_LoadFromStream(ByVal fileName As String, ByVal source As Stream) Implements IFileData.LoadFromStream
			Size = CInt(source.Length)
			Me.FileName = fileName
		End Sub
		'Dennis: Fires when saving or opening a file.
		Private Sub IFileData_SaveToStream(ByVal destination As Stream) Implements IFileData.SaveToStream
			Try
				If destination Is Nothing Then
					FileSystemDataModule.OpenFileWithDefaultProgram(FullName)
				Else
					FileSystemDataModule.CopyFileToStream(FullName, destination)
				End If
			Catch exc As Exception
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
			Get
				Return Not File.Exists(FullName)
			End Get
		End Property
		#End Region
		#Region "ISupportFullName Members"
		<Size(260), Custom("AllowEdit", "False")>
		Public Property FullName() As String Implements ISupportFullName.FullName
			Get
				Return GetPropertyValue(Of String)("FullName")
			End Get
			Set(ByVal value As String)
				SetPropertyValue(Of String)("FullName", value)
			End Set
		End Property
		#End Region
	End Class
End Namespace
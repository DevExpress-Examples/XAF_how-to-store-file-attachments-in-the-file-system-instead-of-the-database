Imports System
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Xpo
Imports DevExpress.ExpressApp.Win
Imports System.Collections.Generic

Namespace E965.Win
	Partial Public Class E965WindowsFormsApplication
		Inherits WinApplication

		Public Sub New()
			InitializeComponent()
			DelayedViewItemsInitialization = True
			''' To open the linked file without creating a temporary one (and keep any changes to it after closing the program), handle the FileAttachmentsWindowsFormsModule.CustomOpenFileWithDefaultProgram event and call the e.FileData.SaveToStream method and pass null (nothing in VB.NET) as a parameter.
			AddHandler fileAttachmentsWindowsFormsModule1.CustomOpenFileWithDefaultProgram, AddressOf fileAttachmentsWindowsFormsModule1_CustomOpenFileWithDefaultProgram
		End Sub
		Private Sub fileAttachmentsWindowsFormsModule1_CustomOpenFileWithDefaultProgram(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.FileAttachments.Win.CustomFileOperationEventArgs)
			If (TypeOf e.FileData Is FileSystemData.BusinessObjects.FileSystemLinkObject) OrElse (TypeOf e.FileData Is FileSystemData.BusinessObjects.FileSystemStoreObject) Then
				e.FileData.SaveToStream(Nothing)
				e.Handled = True
			End If
		End Sub
		Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
			args.ObjectSpaceProvider = New XPObjectSpaceProvider(args.ConnectionString, args.Connection)
		End Sub
		Private Sub E965WindowsFormsApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs) Handles MyBase.DatabaseVersionMismatch
#If EASYTEST Then
			e.Updater.Update()
			e.Handled = True
#Else
			If System.Diagnostics.Debugger.IsAttached Then
				e.Updater.Update()
				e.Handled = True
			Else
				Throw New InvalidOperationException("The application cannot connect to the specified database, because the latter doesn't exist or its version is older than that of the application." & ControlChars.CrLf & "This error occurred  because the automatic database update was disabled when the application was started without debugging." & ControlChars.CrLf & "To avoid this error, you should either start the application under Visual Studio in debug mode, or modify the " & "source code of the 'DatabaseVersionMismatch' event handler to enable automatic database update, " & "or manually create a database using the 'DBUpdater' tool." & ControlChars.CrLf & "Anyway, refer to the 'Update Application and Database Versions' help topic at http://www.devexpress.com/Help/?document=ExpressApp/CustomDocument2795.htm " & "for more detailed information. If this doesn't help, please contact our Support Team at http://www.devexpress.com/Support/Center/")
			End If
#End If
		End Sub
	End Class
End Namespace

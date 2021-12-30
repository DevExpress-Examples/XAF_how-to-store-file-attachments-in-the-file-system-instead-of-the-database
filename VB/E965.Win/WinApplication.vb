Imports System
Imports System.ComponentModel
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Win
Imports System.Collections.Generic
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp.Win.Utils
Imports DevExpress.ExpressApp.Xpo

Namespace E965.Win
	Partial Public Class E965WindowsFormsApplication
		Inherits WinApplication

		Public Sub New()
			InitializeComponent()
			SplashScreen = New DXSplashScreen(GetType(XafSplashScreen), New DefaultOverlayFormOptions())
			DelayedViewItemsInitialization = True
			' To open the linked file without creating a temporary one (and keep any changes to it after closing the program), handle the FileAttachmentsWindowsFormsModule.CustomOpenFileWithDefaultProgram event and call the e.FileData.SaveToStream method and pass null (nothing in VB.NET) as a parameter.
			AddHandler fileAttachmentsWindowsFormsModule.CustomOpenFileWithDefaultProgram, AddressOf fileAttachmentsWindowsFormsModule_CustomOpenFileWithDefaultProgram
		End Sub
		Private Sub fileAttachmentsWindowsFormsModule_CustomOpenFileWithDefaultProgram(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.FileAttachments.Win.CustomFileOperationEventArgs)
			If (TypeOf e.FileData Is FileSystemData.BusinessObjects.FileSystemLinkObject) OrElse (TypeOf e.FileData Is FileSystemData.BusinessObjects.FileSystemStoreObject) Then
				e.FileData.SaveToStream(Nothing)
				e.Handled = True
			End If
		End Sub
		Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
			args.ObjectSpaceProviders.Add(New XPObjectSpaceProvider(XPObjectSpaceProvider.GetDataStoreProvider(args.ConnectionString, args.Connection, True), False))
			args.ObjectSpaceProviders.Add(New NonPersistentObjectSpaceProvider(TypesInfo, Nothing))
		End Sub
		Private Sub E965WindowsFormsApplication_CustomizeLanguagesList(ByVal sender As Object, ByVal e As CustomizeLanguagesListEventArgs) Handles Me.CustomizeLanguagesList
			Dim userLanguageName As String = System.Threading.Thread.CurrentThread.CurrentUICulture.Name
			If userLanguageName <> "en-US" AndAlso e.Languages.IndexOf(userLanguageName) = -1 Then
				e.Languages.Add(userLanguageName)
			End If
		End Sub
		Private Sub E965WindowsFormsApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs) Handles Me.DatabaseVersionMismatch
#If EASYTEST Then
			e.Updater.Update()
			e.Handled = True
#Else
			If System.Diagnostics.Debugger.IsAttached Then
				e.Updater.Update()
				e.Handled = True
			Else
				Dim message As String = "The application cannot connect to the specified database, " & "because the database doesn't exist, its version is older " & "than that of the application or its schema does not match " & "the ORM data model structure. To avoid this error, use one " & "of the solutions from the https://www.devexpress.com/kb=T367835 KB Article."

				If e.CompatibilityError IsNot Nothing AndAlso e.CompatibilityError.Exception IsNot Nothing Then
					message &= vbCrLf & vbCrLf & "Inner exception: " & e.CompatibilityError.Exception.Message
				End If
				Throw New InvalidOperationException(message)
			End If
#End If
		End Sub
	End Class
End Namespace

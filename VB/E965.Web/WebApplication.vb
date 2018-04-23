Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Web
Imports DevExpress.ExpressApp.Xpo

Namespace E965.Web
	Partial Public Class E965AspNetApplication
		Inherits WebApplication

		Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
		Private module2 As DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule
		Private module3 As E965.Module.E965Module
		Private businessClassLibraryCustomizationModule1 As DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule
		Private securityModule1 As DevExpress.ExpressApp.Security.SecurityModule
		Private fileSystemDataModule1 As FileSystemData.FileSystemDataModule
		Private fileAttachmentsAspNetModule1 As DevExpress.ExpressApp.FileAttachments.Web.FileAttachmentsAspNetModule
		Private sqlConnection1 As System.Data.SqlClient.SqlConnection

		Public Sub New()
			InitializeComponent()
		End Sub

		Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
			args.ObjectSpaceProvider = New XPObjectSpaceProvider(args.ConnectionString, args.Connection, True)
		End Sub

		Private Sub E965AspNetApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs) Handles MyBase.DatabaseVersionMismatch
#If EASYTEST Then
			e.Updater.Update()
			e.Handled = True
#Else
			If System.Diagnostics.Debugger.IsAttached Then
				e.Updater.Update()
				e.Handled = True
			Else
				Dim message As String = "The application cannot connect to the specified database, because the latter doesn't exist or its version is older than that of the application." & ControlChars.CrLf & "This error occurred  because the automatic database update was disabled when the application was started without debugging." & ControlChars.CrLf & "To avoid this error, you should either start the application under Visual Studio in debug mode, or modify the " & "source code of the 'DatabaseVersionMismatch' event handler to enable automatic database update, " & "or manually create a database using the 'DBUpdater' tool." & ControlChars.CrLf & "Anyway, refer to the following help topics for more detailed information:" & ControlChars.CrLf & "'Update Application and Database Versions' at http://www.devexpress.com/Help/?document=ExpressApp/CustomDocument2795.htm" & ControlChars.CrLf & "'Database Security References' at http://www.devexpress.com/Help/?document=ExpressApp/CustomDocument3237.htm" & ControlChars.CrLf & "If this doesn't help, please contact our Support Team at http://www.devexpress.com/Support/Center/"

				If e.CompatibilityError IsNot Nothing AndAlso e.CompatibilityError.Exception IsNot Nothing Then
					message &= ControlChars.CrLf & ControlChars.CrLf & "Inner exception: " & e.CompatibilityError.Exception.Message
				End If
				Throw New InvalidOperationException(message)
			End If
#End If
		End Sub

		Private Sub InitializeComponent()
			Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
			Me.module2 = New DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule()
			Me.module3 = New E965.Module.E965Module()
			Me.sqlConnection1 = New System.Data.SqlClient.SqlConnection()
			Me.businessClassLibraryCustomizationModule1 = New DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule()
			Me.securityModule1 = New DevExpress.ExpressApp.Security.SecurityModule()
			Me.fileSystemDataModule1 = New FileSystemData.FileSystemDataModule()
			Me.fileAttachmentsAspNetModule1 = New DevExpress.ExpressApp.FileAttachments.Web.FileAttachmentsAspNetModule()
			DirectCast(Me, System.ComponentModel.ISupportInitialize).BeginInit()
			' 
			' sqlConnection1
			' 
			Me.sqlConnection1.ConnectionString = "Integrated Security=SSPI;Pooling=false;Data Source=.;Initial Catalog=E965"
			Me.sqlConnection1.FireInfoMessageEventOnUserErrors = False
			' 
			' E965AspNetApplication
			' 
			Me.ApplicationName = "E965"
			Me.CheckCompatibilityType = DevExpress.ExpressApp.CheckCompatibilityType.DatabaseSchema
			Me.Connection = Me.sqlConnection1
			Me.Modules.Add(Me.module1)
			Me.Modules.Add(Me.module2)
			Me.Modules.Add(Me.businessClassLibraryCustomizationModule1)
			Me.Modules.Add(Me.securityModule1)
			Me.Modules.Add(Me.fileSystemDataModule1)
			Me.Modules.Add(Me.module3)
			Me.Modules.Add(Me.fileAttachmentsAspNetModule1)
'			Me.DatabaseVersionMismatch += New System.EventHandler(Of DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs)(Me.E965AspNetApplication_DatabaseVersionMismatch)
			DirectCast(Me, System.ComponentModel.ISupportInitialize).EndInit()

		End Sub
	End Class
End Namespace

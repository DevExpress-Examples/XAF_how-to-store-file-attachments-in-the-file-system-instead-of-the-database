﻿Namespace E965.Module.Win
	Partial Public Class E965WindowsFormsModule
		''' <summary> 
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary> 
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Component Designer generated code"

		''' <summary> 
		''' Required method for Designer support - do not modify 
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			' 
			' E965WindowsFormsModule
			' 
			Me.RequiredModuleTypes.Add(GetType(E965Module))
			Me.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule))
			Me.RequiredModuleTypes.Add(GetType(DevExpress.ExpressApp.FileAttachments.Win.FileAttachmentsWindowsFormsModule))
		End Sub

		#End Region
	End Class
End Namespace
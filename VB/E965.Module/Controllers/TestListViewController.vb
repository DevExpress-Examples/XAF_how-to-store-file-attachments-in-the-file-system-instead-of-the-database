Imports System
Imports System.IO
Imports System.Linq
Imports FileSystemData
Imports DevExpress.ExpressApp
Imports System.Threading.Tasks
Imports DevExpress.Persistent.Base
Imports System.Collections.Generic
Imports DevExpress.ExpressApp.Actions
Imports FileSystemData.BusinessObjects
Imports DevExpress.Persistent.Validation

Namespace E965.Module.Controllers
	Partial Public Class TestListViewController
		Inherits ViewController

		Private Const MaxTestersCount As Integer = 10
		Private Const STR_Testfile As String = "Test.file"
		Private testAction As SimpleAction
		Public Sub New()
			testAction = New SimpleAction(Me, "Test", "View")
			TargetViewType = ViewType.ListView
			AddHandler testAction.Execute, AddressOf testAction_Execute
		End Sub
		Private Sub testAction_Execute(ByVal sender As Object, ByVal e As SimpleActionExecuteEventArgs)
			testAction.Caption = "Test is running..."
			Dim tasks(MaxTestersCount - 1) As Task
			For i As Integer = 0 To MaxTestersCount - 1
				tasks(i) = Task.Factory.StartNew(Sub()
					For j As Integer = 0 To MaxTestersCount - 1
						Dim testStream As Stream = Me.GetType().Assembly.GetManifestResourceStream("Test.file")
						Using os As IObjectSpace = Application.CreateObjectSpace()
							Try
								Dim theObject As IFileData = os.CreateObject(Of FileSystemStoreObject)()
								If Not DirectCast(theObject, IEmptyCheckable).IsEmpty Then
									Throw New Exception("Must be empty")
								End If
								theObject.LoadFromStream(STR_Testfile, testStream)
								If theObject.FileName <> STR_Testfile Then
									Throw New Exception("Must have file name")
								End If
								os.CommitChanges()
								If theObject.Size <> 44 Then
									Throw New Exception("Must have size 44")
								End If
								If DirectCast(theObject, IEmptyCheckable).IsEmpty Then
									Throw New Exception("Must be non-empty")
								End If
							Catch
								os.Rollback()
								Throw
							End Try
						End Using
					Next j
				End Sub)
			Next i
			Try
				Task.WaitAll(tasks)
				View.ObjectSpace.Refresh()
				testAction.Caption = "Succeeded"
			Catch ex As Exception
				testAction.Caption = "Failed"
				Tracing.Tracer.LogError(ex)
			End Try
			testAction.Enabled("Run only once") = False
		End Sub
	End Class
End Namespace
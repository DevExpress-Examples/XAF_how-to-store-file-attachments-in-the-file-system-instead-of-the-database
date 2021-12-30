Imports System
Imports System.Linq
Imports System.Text
Imports System.ComponentModel
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.DC
Imports System.Collections.Generic
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp.Model.Core
Imports DevExpress.ExpressApp.Model.DomainLogics
Imports DevExpress.ExpressApp.Model.NodeGenerators
Imports E965.Module.BusinessObjects
Imports DevExpress.Persistent.Base

Namespace E965.Module.Web
	' For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
	<ToolboxItemFilter("Xaf.Platform.Web")>
	Public NotInheritable Partial Class E965AspNetModule
		Inherits ModuleBase

		Public Sub New()
			InitializeComponent()
		End Sub
		Public Overrides Function GetModuleUpdaters(ByVal objectSpace As IObjectSpace, ByVal versionFromDB As Version) As IEnumerable(Of ModuleUpdater)
			Return ModuleUpdater.EmptyModuleUpdaters
		End Function
		Public Overrides Sub Setup(ByVal application As XafApplication)
			MyBase.Setup(application)
			' Manage various aspects of the application UI and behavior at the module level.
		End Sub
		Public Overrides Sub CustomizeTypesInfo(ByVal typesInfo As ITypesInfo)
			MyBase.CustomizeTypesInfo(typesInfo)

			'FileSystemLinkObjectDemo is not supported in Web applications. It is hidden in the App Model, but you can also hide it by using the code below
			'Dim typeInfo As ITypeInfo = typesInfo.FindTypeInfo(GetType(FileSystemLinkObjectDemo))
			'DirectCast(typeInfo, BaseInfo).RemoveAttribute(New DefaultClassOptionsAttribute())
			'DirectCast(typeInfo, BaseInfo).RemoveAttribute(New NavigationItemAttribute())
		End Sub
	End Class
End Namespace

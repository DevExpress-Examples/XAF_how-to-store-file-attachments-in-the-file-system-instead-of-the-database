﻿Imports System
Imports System.Text
Imports System.Linq
Imports DevExpress.ExpressApp
Imports System.ComponentModel
Imports DevExpress.ExpressApp.DC
Imports System.Collections.Generic
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp.Model
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.ExpressApp.Model.Core
Imports DevExpress.ExpressApp.Model.DomainLogics
Imports DevExpress.ExpressApp.Model.NodeGenerators
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp.Xpo

Namespace E965.Module
	' For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
	Partial Public NotInheritable Class E965Module
		Inherits ModuleBase

		Public Sub New()
			InitializeComponent()
		End Sub
		Public Overrides Function GetModuleUpdaters(ByVal objectSpace As IObjectSpace, ByVal versionFromDB As Version) As IEnumerable(Of ModuleUpdater)
			Dim updater As ModuleUpdater = New DatabaseUpdate.Updater(objectSpace, versionFromDB)
			Return New ModuleUpdater() {updater}
		End Function
		Public Overrides Sub Setup(ByVal application As XafApplication)
			MyBase.Setup(application)
			' Manage various aspects of the application UI and behavior at the module level.
		End Sub
		Public Overrides Sub CustomizeTypesInfo(ByVal typesInfo As ITypesInfo)
			MyBase.CustomizeTypesInfo(typesInfo)
			CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo)
		End Sub
	End Class
End Namespace

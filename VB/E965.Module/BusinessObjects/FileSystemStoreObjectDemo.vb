Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Xpo
Imports FileSystemData.BusinessObjects

Namespace E965.Module.BusinessObjects
	<DefaultClassOptions, FileAttachment("File")>
	Public Class FileSystemStoreObjectDemo
		Inherits BaseObject

		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		<Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never), ImmediatePostData>
		Public Property File() As FileSystemStoreObject
			Get
				Return GetPropertyValue(Of FileSystemStoreObject)("File")
			End Get
			Set(ByVal value As FileSystemStoreObject)
				SetPropertyValue(Of FileSystemStoreObject)("File", value)
			End Set
		End Property
	End Class
End Namespace

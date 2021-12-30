Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Xpo
Imports FileSystemData.BusinessObjects

Namespace E965.Module.BusinessObjects
	<DefaultClassOptions, FileAttachment("File")>
	Public Class FileSystemLinkObjectDemo
		Inherits BaseObject

		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		<Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never), ImmediatePostData>
		Public Property File() As FileSystemLinkObject
			Get
				Return GetPropertyValue(Of FileSystemLinkObject)("File")
			End Get
			Set(ByVal value As FileSystemLinkObject)
				SetPropertyValue(Of FileSystemLinkObject)("File", value)
			End Set
		End Property
	End Class
End Namespace

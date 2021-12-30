Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Xpo

Namespace E965.Module.BusinessObjects
	<DefaultClassOptions, FileAttachment("File")>
	Public Class StandardFileDataDemo
		Inherits BaseObject

		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		<Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never)>
		Public Property File() As FileData
			Get
				Return GetPropertyValue(Of FileData)("File")
			End Get
			Set(ByVal value As FileData)
				SetPropertyValue(Of FileData)("File", value)
			End Set
		End Property
	End Class
End Namespace

Public Class OrganizationalContacts
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim objOrganizationContacts As New BusinessLogic.Organization("Demo", 1)

        With radContacts

            .DataSource = objOrganizationContacts.RetrieveAll().Tables(0)
            .DataBind()

            ViewState("OrgContacts") = .DataSource

        End With


    End Sub

    Private Sub cmdAddNew_Click(sender As Object, e As EventArgs) Handles cmdAddNew.Click

        Response.Redirect("~/OrganizationDetails.aspx")

    End Sub

    Private Sub radContacts_DetailTableDataBind(sender As Object, e As Telerik.Web.UI.GridDetailTableDataBindEventArgs) Handles radContacts.DetailTableDataBind

        Dim dataItem As Telerik.Web.UI.GridDataItem = CType(e.DetailTableView.ParentItem, Telerik.Web.UI.GridDataItem)
        Dim objSubOffices As New BusinessLogic.SubOffices("Demo", 1)

        If e.DetailTableView.Name = "dsContacts" Then

            e.DetailTableView.DataSource = objSubOffices.GetSubOfficesByOrganization(dataItem.GetDataKeyValue("OrganizationID").ToString())

        End If

    End Sub

    Private Sub radContacts_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles radContacts.NeedDataSource

        radContacts.DataSource = DirectCast(ViewState("OrgContacts"), DataTable)

    End Sub
End Class
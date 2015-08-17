Public Class OrganizationalBeneficiaries
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim objOrganization As New BusinessLogic.Organization("Demo", 1)

        With radOrgListing

            .DataSource = objOrganization.RetrieveAll().Tables(0)
            .DataBind()

        End With

    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click

        Response.Redirect("~/OrganizationDetails.aspx")

    End Sub
End Class
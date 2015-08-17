Public Class GroupsListing
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim objGroups As New BusinessLogic.Groups("Demo", 1)

        With radGroupListing

            .DataSource = objGroups.RetrieveAll().Tables(0)
            .DataBind()

        End With

    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click

        Response.Redirect("~/GroupsDetails.aspx")

    End Sub
End Class
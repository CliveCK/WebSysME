Public Class SchoolsListing
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim objSchools As New BusinessLogic.Schools("Demo", 1)

        With radSchoolListing

            .DataSource = objSchools.RetrieveAll().Tables(0)
            .DataBind()

        End With

    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click

        Response.Redirect("~/SchoolManagement.aspx")

    End Sub
End Class
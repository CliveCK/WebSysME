Public Class LibraryListing
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim objFiles As New BusinessLogic.Files("Demo", 1)

        With radFileListing

            .DataSource = objFiles.GetAllFiles()
            .DataBind()

            Session("Files") = .DataSource

        End With

    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click

        Response.Redirect("~/Files.aspx")

    End Sub
End Class
Public Class IndividualContacts
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim objStaff As New BusinessLogic.StaffMember("Demo", 1)

        With radContacts

            .DataSource = objStaff.GetAllStaffMember()
            .DataBind()

        End With

    End Sub

    Private Sub cmdNew_Click(sender As Object, e As EventArgs) Handles cmdNew.Click

        Response.Redirect("~/StaffMembers.aspx")

    End Sub
End Class
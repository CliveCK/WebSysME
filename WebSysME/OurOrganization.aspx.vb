Public Class OurOrganization
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            LoadGrid()

        End If

    End Sub

    Public Sub LoadGrid()

        Dim objSubOffices As New BusinessLogic.SubOffices("Demo", 1)
        Dim OrganizationID As Long = IIf(IsNumeric(DirectCast(ucOrganizationControl.FindControl("txtOrganizationID"), TextBox).Text), DirectCast(ucOrganizationControl.FindControl("txtOrganizationID"), TextBox).Text, 0)

        With radAddress

            .DataSource = objSubOffices.GetSubOffices(OrganizationID)
            .DataBind()

        End With

        With radStaff

            .DataSource = String.Empty
            .DataBind()

        End With

    End Sub

End Class
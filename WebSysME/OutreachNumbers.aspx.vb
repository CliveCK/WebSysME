Public Class OutreachNumbers
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim objOutreach As New BusinessLogic.Trips("Demo", 1)

        With radOutreach

            .DataSource = objOutreach.GetAllTrips("SELECT * FROM tblOutreach")
            .DataBind()

        End With

    End Sub

End Class
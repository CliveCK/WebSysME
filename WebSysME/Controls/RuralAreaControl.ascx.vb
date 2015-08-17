﻿Public Class RuralAreaControl
    Inherits System.Web.UI.UserControl

#Region "Status Messages"

    Public Event Message(ByVal Message As String, ByVal MessageType As MessageTypeEnum)

    Public Sub ShowMessage(ByVal Message As String, ByVal MessageType As MessageTypeEnum, Optional ByVal LocalOnly As Boolean = False)

        lblError.Text = Message
        pnlError.CssClass = "msg" & [Enum].GetName(GetType(MessageTypeEnum), MessageType)

        If Not LocalOnly Then RaiseEvent Message(Message, MessageType)

    End Sub

    Public Sub ShowMessage(ByVal Message As Exception, ByVal MessageType As MessageTypeEnum, Optional ByVal LocalOnly As Boolean = False)

        lblError.Text = Message.Message
        If Message.InnerException IsNot Nothing Then lblError.Text &= " - " & Message.InnerException.Message
        If Not LocalOnly Then RaiseEvent Message(Message.Message, MessageType)

        pnlError.CssClass = "msg" & [Enum].GetName(GetType(MessageTypeEnum), MessageType)

    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            cboProvince.Enabled = False
            txtProvince.Enabled = False
            cboDistrict.Enabled = False
            txtDistrict.Enabled = False
            cboWard.Enabled = False
            txtWard.Enabled = False

            Dim objLookup As New BusinessLogic.CommonFunctions

            With cboCountry

                .DataSource = objLookup.Lookup("luCountries", "CountryID", "CountryName").Tables(0)
                .DataValueField = "CountryID"
                .DataTextField = "CountryName"
                .DataBind()

                .Items.Insert(0, New ListItem(String.Empty, String.Empty))
                .SelectedIndex = 0

            End With

        End If

    End Sub

    Private Sub rbLstSaveOption_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rbLstSaveOption.SelectedIndexChanged

        Select Case rbLstSaveOption.SelectedValue

            Case "Country"
                cboProvince.Enabled = False
                txtProvince.Enabled = False
                cboDistrict.Enabled = False
                txtDistrict.Enabled = False
                cboWard.Enabled = False
                txtWard.Enabled = False

            Case "Province"
                cboProvince.Enabled = True
                txtProvince.Enabled = True
                cboDistrict.Enabled = False
                txtDistrict.Enabled = False
                cboWard.Enabled = False
                txtWard.Enabled = False

            Case "District"
                cboProvince.Enabled = True
                txtProvince.Enabled = True
                cboDistrict.Enabled = True
                txtDistrict.Enabled = True
                cboWard.Enabled = False
                txtWard.Enabled = False

            Case "Ward"
                cboProvince.Enabled = True
                txtProvince.Enabled = True
                cboWard.Enabled = True
                cboDistrict.Enabled = True
                txtDistrict.Enabled = True
                txtWard.Enabled = True

        End Select

    End Sub

    Private Sub cboCountry_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCountry.SelectedIndexChanged

        Dim objLookup As New BusinessLogic.CommonFunctions

        If cboCountry.SelectedIndex > 0 Then

            With cboProvince

                .DataSource = objLookup.Lookup("tblProvinces", "ProvinceID", "Name", , "CountryID = " & cboCountry.SelectedValue).Tables(0)
                .DataValueField = "ProvinceID"
                .DataTextField = "Name"
                .DataBind()

            End With

        End If

    End Sub

    Private Sub cboProvince_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProvince.SelectedIndexChanged

        Dim objLookup As New BusinessLogic.CommonFunctions

        If cboProvince.SelectedIndex > 0 Then

            With cboDistrict

                .DataSource = objLookup.Lookup("tblDistricts", "DistrictID", "Name", , "ProvinceID = " & cboProvince.SelectedValue).Tables(0)
                .DataValueField = "DistrictID"
                .DataTextField = "Name"
                .DataBind()

            End With

        End If

    End Sub

    Private Sub cboDistrict_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDistrict.SelectedIndexChanged

        Dim objLookup As New BusinessLogic.CommonFunctions

        If cboDistrict.SelectedIndex > 0 Then

            With cboWard

                .DataSource = objLookup.Lookup("tblWards", "WardID", "Name", , "DistrictID = " & cboDistrict.SelectedValue).Tables(0)
                .DataValueField = "WardID"
                .DataTextField = "Name"
                .DataBind()

            End With

        End If

    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click

        Dim objRuralArea As New BusinessLogic.RuralArea("Demo", 1)

        With objRuralArea

            Select Case rbLstSaveOption.SelectedValue

                Case "Country"
                    If txtCountry.Text = "" Then Exit Sub
                    .Country = txtCountry.Text

                Case "Province"
                    If txtProvince.Text = "" Then Exit Sub
                    .Province = txtProvince.Text
                    .CountryID = cboCountry.SelectedValue

                Case "District"
                    If txtDistrict.Text = "" Then Exit Sub
                    .District = txtDistrict.Text
                    .ProvinceID = cboProvince.SelectedValue

                Case "Ward"
                    If txtWard.Text = "" Then Exit Sub
                    .Ward = txtWard.Text
                    .DistrictID = cboDistrict.SelectedValue

            End Select

            If .Save(rbLstSaveOption.SelectedValue) Then

                ShowMessage(rbLstSaveOption.SelectedValue & " saved successfully...", MessageTypeEnum.Information)

            Else

                ShowMessage("Error: Failed to save " & rbLstSaveOption.SelectedValue, MessageTypeEnum.Error)

            End If

        End With

    End Sub
End Class
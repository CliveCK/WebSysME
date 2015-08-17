﻿Imports BusinessLogic
    Partial Class GroupsDetailsControl
        Inherits System.Web.UI.UserControl

        Private objUrlEncoder As New Security.SpecialEncryptionServices.UrlServices.EncryptDecryptQueryString

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


        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            If Not Page.IsPostBack Then

                If Not IsNothing(Request.QueryString("id")) Then

                    LoadGrid(objUrlEncoder.Decrypt(Request.QueryString("id")))
                    LoadGroups(objUrlEncoder.Decrypt(Request.QueryString("id")))

                Else

                    Dim objLookup As New BusinessLogic.CommonFunctions

                    With cboProvince

                        .DataSource = objLookup.Lookup("tblProvinces", "ProvinceID", "Name").Tables(0)
                        .DataValueField = "ProvinceID"
                        .DataTextField = "Name"
                        .DataBind()

                        .Items.Insert(0, New ListItem(String.Empty, String.Empty))
                        .SelectedIndex = 0

                    End With

                End If

            End If

        End Sub

        Private Sub LoadGrid(ByVal GroupID As Long)

            Dim objBeneficiaries As New Groups("Demo", 1)

            With radGroupMembership

                .DataSource = objBeneficiaries.GetGroupMembership(GroupID)
                .DataBind()

            End With

        End Sub

        Protected Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click

            Save()

        End Sub

        Public Function LoadGroups(ByVal GroupID As Long) As Boolean

            Try

                Dim objGroups As New Groups("Demo", 1)

                With objGroups

                    If .Retrieve(GroupID) Then

                        txtGroupID.Text = .GroupID
                        If Not IsNothing(cboWard.Items.FindByValue(.WardID)) Then cboWard.SelectedValue = .WardID
                        If Not IsNothing(cboGroupType.Items.FindByValue(.GroupTypeID)) Then cboGroupType.SelectedValue = .GroupTypeID
                        txtDescription.Text = .Description
                        txtGroupSize.Text = .GroupSize
                        txtGroupName.Text = .GroupName

                        ShowMessage("Groups loaded successfully...", MessageTypeEnum.Information)
                        Return True

                    Else

                        ShowMessage("Failed to loadGroups: & .ErrorMessage", MessageTypeEnum.Error)
                        Return False

                    End If

                End With

            Catch ex As Exception

                ShowMessage(ex, MessageTypeEnum.Error)
                Return False

            End Try

        End Function

        Public Function Save() As Boolean

            Try

                Dim objGroups As New Groups("Demo", 1)

                With objGroups

                    .GroupID = IIf(IsNumeric(txtGroupID.Text), txtGroupID.Text, 0)
                    If cboWard.SelectedIndex > -1 Then .WardID = cboWard.SelectedValue
                    If cboGroupType.SelectedIndex > -1 Then .GroupTypeID = cboGroupType.SelectedValue
                    .Description = txtDescription.Text
                    .GroupSize = txtGroupSize.Text
                    .GroupName = txtGroupName.Text

                    If .Save Then

                        If Not IsNumeric(txtGroupID.Text) OrElse Trim(txtGroupID.Text) = 0 Then txtGroupID.Text = .GroupID
                        LoadGrid(GroupID:=.GroupID)
                        ShowMessage("Group saved successfully...", MessageTypeEnum.Information)

                        Return True

                    Else

                        ShowMessage("Error: Failed to save group", MessageTypeEnum.Error)
                        Return False

                    End If

                End With


            Catch ex As Exception

                ShowMessage(ex, MessageTypeEnum.Error)
                Return False

            End Try

        End Function

        Public Sub Clear()

            txtGroupID.Text = ""
            If Not IsNothing(cboWard.Items.FindByValue("")) Then
                cboWard.SelectedValue = ""
            ElseIf Not IsNothing(cboWard.Items.FindByValue(0)) Then
                cboWard.SelectedValue = 0
            Else
                cboWard.SelectedIndex = -1
            End If
            If Not IsNothing(cboGroupType.Items.FindByValue("")) Then
                cboGroupType.SelectedValue = ""
            ElseIf Not IsNothing(cboGroupType.Items.FindByValue(0)) Then
                cboGroupType.SelectedValue = 0
            Else
                cboGroupType.SelectedIndex = -1
            End If
            txtDescription.Text = ""
            txtGroupSize.Text = ""
            txtGroupName.Text = ""

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
    End Class



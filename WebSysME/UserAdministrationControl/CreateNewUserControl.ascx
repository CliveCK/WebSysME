<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="CreateNewUserControl.ascx.vb"
    Inherits="WebSysME.CreateNewUserControl" %>
<asp:Panel ID="Panel1" runat="server" GroupingText="   User Details" Width="100%" >

    <table width="100%" cellpadding="3" style="margin-left:2%">
        <tr>
            <td style="height: 21px; width: 131px;">Username
            </td>
            <td style="height: 21px">
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
            </td>
            <td style="height: 21px">
            <asp:Label ID="lblPasswordPolicyMsg" Width="100%" runat="server" CssClass="Error"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 21px; width: 131px;">Password
            </td>
            <td colspan="2" style="height: 21px" nowrap="nowrap">
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                <asp:CustomValidator ID="cvMinLength" runat="server" ControlToValidate="txtPassword" ValidateEmptyText="True"
                    ClientValidationFunction="clientValidate" Display="Dynamic"></asp:CustomValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPassword"
                    ErrorMessage="Enter password"></asp:RequiredFieldValidator>
                 <asp:CompareValidator ID="cvPasswordStrength" runat="server" ValueToCompare="3" Type="Integer"
                    ControlToValidate="txtPasswordStrength" Operator = "LessThan" ErrorMessage="Passwords to weak"></asp:CompareValidator>

            </td>
        </tr>
        <tr>
            <td style="height: 21px; width: 131px;">Confirm Password
            </td>
            <td style="height: 21px">
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPassword"
                    ControlToValidate="txtConfirmPassword" ErrorMessage="Passwords do not match"></asp:CompareValidator>
            </td>
            <td style="height: 21px; color: #000000;"></td>
        </tr>
        <tr style="color: #000000">
            <td style="width: 131px">E-mail Address
            </td>
            <td>
                <asp:TextBox ID="txtEmailAddress" runat="server" Width="250px" CssClass="form-control"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmailAddress"
                    ErrorMessage="Invalid Email Address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </td>
            <td></td>
        </tr>
        <tr style="color: #000000">
            <td style="width: 131px">Mobile No
            </td>
            <td>
                <asp:TextBox ID="txtMobileNo" runat="server" Width="250px" CssClass="form-control"></asp:TextBox>
            </td>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 131px">User Firstname
            </td>
            <td>
                <asp:TextBox ID="txtFirstname" runat="server" Width="200px" CssClass="form-control"></asp:TextBox>
            </td>
            <td></td>
        </tr>
        <tr>
            <td style="height: 26px; width: 131px;">User Surname
            </td>
            <td style="height: 26px">
                <asp:TextBox ID="txtSurname" runat="server" Width="200px" CssClass="form-control"></asp:TextBox>
            </td>
            <td style="height: 26px"></td>
        </tr>
        <tr>
            <td valign="top" style="width: 131px">User Group
            </td>
            <td colspan="2" valign="top">
                <asp:CheckBoxList ID="chkUserGroup" runat="server" RepeatDirection="Horizontal" CssClass="form-control">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPasswordExpires" runat="server" Text="Password Expires"></asp:Label>
            </td>
            <td colspan="2">
                <asp:CheckBox ID="chkPasswordExpires" runat="server" onclick="enableTextBox()" />
                <asp:Label ID="lblAfter" runat="server" Text="After"></asp:Label>
                <asp:TextBox ID="txtPasswordValidityPeriod" runat="server" Width="50px" CssClass="form-control"></asp:TextBox>
                <asp:Label ID="lblDays" runat="server" Text="Days" ></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="3" valign="top">
                <asp:CheckBox ID="chkIsLockedOut" runat="server" onclick="enableTextBox()" />
                <asp:Label ID="lblIsLockedOut" runat="server" Text="Is Locked Out" Font-Bold="True" Font-Size="10px" ForeColor="Black"></asp:Label>

            </td>
        </tr>
        <tr>
            <td colspan="3" valign="top">
                <asp:CheckBox ID="chkSend" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="Black"
                    Text="Send password email" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtMinLength" runat="server" Width="50px" CssClass="HiddenControl"></asp:TextBox>
                <asp:TextBox ID="txtPasswordStrength" runat="server" Width="50px" CssClass="HiddenControl"></asp:TextBox>
                 <asp:TextBox ID="txtPasswordTemplateID" runat="server" Width="50px" CssClass="HiddenControl"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblStatus" Width="100%" runat="server" CssClass="Error"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="cmdSave" runat="server" Text="Save" CssClass="btn btn-default" Width="56px" />
                <asp:Button ID="cmdAddUser" runat="server" CausesValidation="False" Text="Add Another User"
                    Visible="False" Width="25%" CssClass="btn btn-default" />
            </td>
            <td>
                <asp:TextBox ID="txtUserID" runat="server" CssClass="HiddenControl"></asp:TextBox>
            </td>
        </tr>
    </table>

    <script language="javascript" type="text/javascript">
        function enableTextBox() {
            if (document.getElementById('<%= chkPasswordExpires.ClientID %>').checked == false) {
                document.getElementById('<%= lblAfter.ClientID %>').disabled = true;
                document.getElementById('<%= txtPasswordValidityPeriod.ClientID %>').disabled = true;
                document.getElementById('<%= lblDays.ClientID %>').disabled = true;
            }
            else {
                document.getElementById('<%= lblAfter.ClientID %>').disabled = false;
                document.getElementById('<%= txtPasswordValidityPeriod.ClientID %>').disabled = false;
                document.getElementById('<%= lblDays.ClientID %>').disabled = false;
            }
        }
        document.getElementById("strength").style.display = "none";
    </script>

</asp:Panel>

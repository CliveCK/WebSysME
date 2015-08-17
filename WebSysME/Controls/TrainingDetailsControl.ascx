﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="TrainingDetailsControl.ascx.vb" Inherits="WebSysME.TrainingDetailsControl"%>
<table cellpadding="4" cellspacing="0" border="0" style="width:100%;margin-left:2%"> 
	<tr> 
		<td colspan="4" class="PageTitle"><h4>Training Details</h4></td> 
	</tr> 
	<tr> 
		<td >Training Type</td> 
        	<td ><asp:dropdownlist id="cboTrainingType" runat="server" CssClass="form-control"></asp:dropdownlist> </td> 
	</tr> 
	<tr> 
		<td >Name</td> 
        	<td ><asp:textbox id="txtName" runat="server" CssClass="form-control"></asp:textbox> </td> 
		<td >Description</td> 
        	<td ><asp:textbox id="txtDescription" runat="server" CssClass="form-control"></asp:textbox> </td> 
	</tr> 
	<tr> 
		<td >Location</td> 
        	<td ><asp:textbox id="txtLocation" runat="server" CssClass="form-control"></asp:textbox> </td> 
        <td >Facilitator</td> 
        	<td ><asp:textbox id="txtFacilitator" runat="server" CssClass="form-control"></asp:textbox> </td> 
	<tr> 
		<td colspan="4"> 
            		<asp:Panel id="pnlError" width="95%" runat="server" EnableViewState="False"><asp:label id="lblError" Width="100%" runat="server" CssClass="Error" EnableViewState="False"></asp:label></asp:Panel> 
     </td> 
	</tr> 
	<tr> 
		<td colspan="4"> 
            		<asp:button id="cmdSave" runat="server" Text="Save" CssClass="btn btn-default"></asp:button> 
                    <asp:button id="cmdClear" runat="server" Text="Clear" CssClass="btn btn-default"></asp:button>
     </td> 
	</tr> 
	<tr> 
		<td colspan="4"> 
			<asp:TextBox id="txtTrainingID" runat="server" CssClass="HiddenControl"></asp:TextBox> <br />
		</td> 
	</tr> 
    <tr>
        <td><asp:LinkButton ID="lnkBeneficiaries" runat="server" Text="Training Attendants"></asp:LinkButton></td>
        <td><asp:LinkButton ID="lnkInputs" runat="server" Text="Inputs" ></asp:LinkButton></td>
        <td><asp:LinkButton ID="lnkFiles" runat="server" Text="File uploads" ></asp:LinkButton></td>
    </tr>
</table> 

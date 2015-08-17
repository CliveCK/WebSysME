<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="CommunityDetailsControl.ascx.vb" Inherits="WebSysME.CommunityDetailsControl" %>

<style type="text/css">
    .auto-style1 {
        width: 235px;
    }
    .auto-style2 {
        width: 236px;
    }
</style>
<div style="margin-left:2%">
<table cellpadding="3" cellspacing="0" border="0" style="width:100%;margin-left:2%"> 
	<tr> 
		<td colspan="4" class="PageTitle"><h4>Community Details</h4><br /></td> 
	</tr> 
    <tr> 
		<td >Province</td> 
        	<td ><asp:dropdownlist id="cboProvince" runat="server" AutoPostBack="true" CssClass="form-control"></asp:dropdownlist> </td> 
	</tr> 
    <tr> 
		<td >District</td> 
        	<td ><asp:dropdownlist id="cboDistrict" runat="server" AutoPostBack="true" CssClass="form-control"></asp:dropdownlist> </td> 
	</tr> 
	<tr> 
		<td >Ward</td> 
        	<td ><asp:dropdownlist id="cboWard" runat="server" CssClass="form-control"></asp:dropdownlist> </td> 
	</tr> 
	<tr> 
		<td >No Of Households</td> 
        	<td ><asp:textbox id="txtNoOfHouseholds" runat="server"  CssClass="form-control"></asp:textbox> </td> 
		<td >No Of Individual Adult Males</td> 
        	<td ><asp:textbox id="txtNoOfIndividualAdultMales" runat="server" CssClass="form-control"></asp:textbox> </td> 
	</tr> 
	<tr> 
		<td >No Of Individual Adult Females</td> 
        	<td ><asp:textbox id="txtNoOfIndividualAdultFemales" runat="server" CssClass="form-control"></asp:textbox> </td> 
		<td >No Of Male Youths</td> 
        	<td ><asp:textbox id="txtNoOfMaleYouths" runat="server" CssClass="form-control"></asp:textbox> </td> 
	</tr> 
	<tr> 
		<td >No Of Female Youth</td> 
        	<td ><asp:textbox id="txtNoOfFemaleYouth" runat="server" CssClass="form-control"></asp:textbox> </td> 
		<td >No Of Children</td> 
        	<td ><asp:textbox id="txtNoOfChildren" runat="server" CssClass="form-control"></asp:textbox> </td> 
	</tr> 
	<tr> 
		<td >Name</td> 
        	<td ><asp:textbox id="txtName" runat="server" CssClass="form-control"></asp:textbox> </td> 
		<td >Description</td> 
        	<td ><asp:textbox id="txtDescription" runat="server" CssClass="form-control"></asp:textbox> </td> 
	</tr> 
	<tr> 
		<td colspan="4"> 
            		<asp:Panel id="pnlError" width="95%" runat="server" EnableViewState="False"><asp:label id="lblError" Width="100%" runat="server" CssClass="Error" EnableViewState="False"></asp:label></asp:Panel> 
     </td> 
	</tr> 
	<tr> 
		<td colspan="4"> 
            		<asp:button id="cmdSave" runat="server" Text="Save" CssClass="btn btn-default"></asp:button> 
     </td> 
	</tr> 
	<tr> 
		<td colspan="4"> 
			<asp:TextBox id="txtCommunityID" runat="server" CssClass="HiddenControl"></asp:TextBox> 
		</td> 
	</tr> 
</table> 
</div>

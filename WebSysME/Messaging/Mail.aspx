<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Mail.aspx.vb" Inherits="WebSysME.Mail" MasterPageFile="~/Site.Master"%>

<%@ Register Src="~/Messaging/ReadMailControl.ascx" TagName="MailControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
    <uc1:MailControl ID="ucMailControl" runat="server"  />
</asp:Content>
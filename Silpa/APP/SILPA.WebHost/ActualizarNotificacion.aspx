<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPASinMenu.master" AutoEventWireup="true" CodeFile="ActualizarNotificacion.aspx.cs" Inherits="ActualizarNotificacion" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        
         <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Actualizar" />
    <asp:Label ID="Label1" runat="server"></asp:Label>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <img src="App_Themes/Img/ajax-loader.gif" />
        </ProgressTemplate>
        </asp:UpdateProgress>
    </ContentTemplate>
    </asp:UpdatePanel>
   
</asp:Content>


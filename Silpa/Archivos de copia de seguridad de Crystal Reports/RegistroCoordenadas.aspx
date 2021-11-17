<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPASinMenuFlash.master" AutoEventWireup="true" CodeFile="RegistroCoordenadas.aspx.cs" Inherits="RegistroMinero_RegistroCoordenadas" MaintainScrollPositionOnPostback="true" %>
<%@ Register src="../controles/exp_localizaciones.ascx" tagname="exp_localizaciones" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="sm" runat="server">
    </asp:ScriptManager>
    <uc1:exp_localizaciones ID="exp_localizaciones1" EnableViewState="true" runat="server" />
    <div>
        <asp:Button ID="btnFinalizar" Text="Finalizar Registro" runat="server" SkinID="boton"
            onclick="btnFinalizar_Click" />
    </div>
</asp:Content>


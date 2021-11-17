<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="RUA.aspx.cs" Inherits="RUAS_RUA" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%		   
        //Response.WriteFile("RUA.htm");
    %>
    <div class="div-titulo">
        <asp:Label ID="lbl_titulo" runat="server" Text="RUA" SkinID="titulo_principal_blanco"></asp:Label>
    </div>
    <div class="div-contenido">
        <table>
            <tr>
                <td>
                    <asp:HyperLink ID="hpl" SkinID="vinculo_texto" runat="server" NavigateUrl="~/RUAS/RUA.htm">Ver Formulario</asp:HyperLink>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

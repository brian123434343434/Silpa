<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="FirmaDocumentoSila.aspx.cs" Inherits="FirmaDigital_FirmaDocumento"
    Title="Untitled Page" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
              <div class="div-titulo">
                <asp:Label ID="Label1" SkinID="titulo_principal_blanco" runat="server" Text="FIRMAR DOCUMENTO"></asp:Label>
        </div>
    <div class="div-contenido">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 600px">
                    <tr>
                        <td>
                            <asp:Button ID="btn_firmar" runat="server" SkinID="boton" Text="Firmar" 
                                onclick="btn_firmar_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btn_rechazar" runat="server" SkinID="boton" Text="Rechazar" 
                                onclick="btn_rechazar_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btn_volver" runat="server" SkinID="boton" Text="Volver" 
                                onclick="btn_volver_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 50px" colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="pnl_certificados" runat="server" Visible="false">
                                <table id="certificados">
                                    <thead>
                                        <tr>
                                            <td colspan="2" align="left">
                                                <asp:Label ID="lbl_certificados" runat="server" Text="Certificados Disponibles" SkinID="titulo_principal"></asp:Label>
                                            </td>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:RadioButtonList runat="server" ID="rbl_certificados" Font-Names="Tahoma">
                                                    <asp:ListItem Value="1"><img alt="a" height="20px" src="../App_Themes/Img/digital-certificate.jpg" />Certificado 1</asp:ListItem>
                                                    <asp:ListItem Value="2"><img alt="b" height="20px" src="../App_Themes/Img/digital-certificate.jpg" />Certificado 2</asp:ListItem>
                                                    <asp:ListItem Value="3"><img alt="c" height="20px" src="../App_Themes/Img/digital-certificate.jpg" />Certificado 3</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btn_aceptar" Text="Aceptar" SkinID="boton" runat="server" OnClick="btn_aceptar_Click" /><asp:Button
                                                    ID="Button1" Text="Rechazar" SkinID="boton" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                        </tr>
                                    </tbody>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Panel ID="pnl_clave" runat="server" Visible="false">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_clave" runat="server" Text="Escriba la Contraseña:" SkinID="etiqueta_negra"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBox1" runat="server" TextMode="Password" SkinID="texto"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button ID="btn_aceptar_clave" Text="Aceptar" SkinID="boton" runat="server" 
                                                onclick="btn_aceptar_clave_Click" /><asp:Button
                                                ID="btn_cancelar_clave" Text="Cancelar" SkinID="boton" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center">
                            <asp:Label ID="lbl_resultado" Text="" runat="server" SkinID="etiqueta_negra" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <iframe id="ifdoc" src="../documentos/firma/Auto_de_Inicio.pdf" height="600" width="600">
                            </iframe>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

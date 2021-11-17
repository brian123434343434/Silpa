<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPASinMenu.master" AutoEventWireup="true" CodeFile="LoginRUIA.aspx.cs" Inherits="LoginRUIA" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="div-titulo">
    <asp:Label ID="lblTituloPrincipal" runat="server" Text="INICIAR SESIÓN EN RUIA" SkinID="titulo_principal_blanco"></asp:Label>
</div>
<div class="div-contenido">
    <div style="text-align: center">
        <table style="width: 70%; border: solid 1px #D8D8D8">
            <tr>
                <td>
                    <table>
                        <tr>   
                            <td align="right">
                                <table width="100%">
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblUsuario" runat="server" SkinID="etiqueta_negra" Text="Usuario:"></asp:Label></td>
                                        <td align="left">
                                            <asp:TextBox ID="txtUsuario" runat="server" Columns="30" MaxLength="75" TextMode="SingleLine" ToolTip="Escriba aquí el usuario"
                                                ValidationGroup="Crear" Width="150px" SkinID="texto"></asp:TextBox>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblClave" runat="server" SkinID="etiqueta_negra" Text="Clave Actual:"></asp:Label></td>
                                        <td align="left">
                                            <asp:TextBox ID="txtClave" runat="server" Columns="30" MaxLength="75"
                                            TextMode="Password" ToolTip="Escriba aquí la clave actual" ValidationGroup="Crear"
                                            Width="150px" SkinID="texto"></asp:TextBox>
                                        </td>
                                    </tr>
                                     <tr>
                                         <td align="right" >
                                             <asp:Label ID="lbl_autoridad_ambiental" runat="server" Text="Autoridad Ambiental:" SkinID="etiqueta_negra"></asp:Label>
                                         </td>
                                         <td align="left" >
                                             <asp:DropDownList ID="cboAutoridadAmbiental" runat="server" SkinID="lista_desplegable">
                                             </asp:DropDownList></td>
                                     </tr>
                                    <tr>
                                    <td colspan="2" align="center">
                                        <asp:Button ID="btnIngresar" runat="server" 
                                            Text="Ingresar" ToolTip="Haga clic aquí para ingresar al sistema" ValidationGroup="Crear" SkinID="boton_copia" OnClick="btnIngresar_Click" /></td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" SkinID="etiqueta_roja_error"
                                                Visible="False"></asp:Label></td>
                                    </tr>
                                </table>       
                            </td>
                        </tr>
                        <tr>
                            <td align="right" ><asp:HyperLink ID="hlk_Olvide_Clave" NavigateUrl="olvide_clave.aspx" runat="server" Text="&raquo; Olvide mi clave" Visible="False"></asp:HyperLink>
                                &nbsp;</td>
                            </tr>
                        <tr>
                            <td align="right"><asp:HyperLink ID="hlk_Cambiar_Clave" NavigateUrl="cambiar_clave.aspx" runat="server" Text="&raquo; Cambiar mi clave" Visible="False"></asp:HyperLink>
                            &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</div>
</asp:Content>


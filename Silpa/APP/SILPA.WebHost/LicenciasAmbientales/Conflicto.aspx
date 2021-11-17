<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPASinMenu.master" AutoEventWireup="true"
    CodeFile="Conflicto.aspx.cs" Inherits="LicenciasAmbientales_Conflicto" Title="Conflicto de Competencias" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="../js/JScript.js"></script>
    
    <div class="div-titulo">
    <asp:Label ID="lblTitulo" runat="server" Text="CONFLICTO DE COMPETENCIAS" SkinID="titulo_principal_blanco"></asp:Label>
    </div>
    <div class="div-contenido">
    
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        
         <table style="width: 600px">
                    <tr>
                        <td>
                            <asp:Label ID="lblNombreFormulario" Visible="false" runat="server" SkinID="etiqueta_negra"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMensaje" Visible="False" runat="server" SkinID="etiqueta_negra"
                                Text="Se ha Se ha detectado que su Proyecto, Obra o Actividad tiene jurisdicción en varias autoridades ambientales, por tal razón debe seleccionar la Autoridad Ambienta a la cual desea enviar su solicitud que su Proyelcto, Obra o Actividad tiene jurisdicción en varias autoridades ambientales, por tal razón debe seleccionar la Autoridad Ambiental a la cual desea enviar su solicitud:" Width="598px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="optAutoridadAmbiental" Width="600px" runat="server"
                                AutoPostBack="True" Font-Bold="False" OnSelectedIndexChanged="optAutoridadAmbiental_SelectedIndexChanged" Height="74px" Visible="False">
                                <asp:ListItem Value="1" Selected="True">Seleccionar al MAVDT para que este defina la Autoridad Ambiental que dar&#225; tr&#225;mite a su solicitud y proyecto.</asp:ListItem>
                                <asp:ListItem Value="2">Seleccionar la AA a la cual desea enviar su solicitud</asp:ListItem>
                            </asp:RadioButtonList>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 20px">
                            <asp:DropDownList ID="cboAutoridadAmbiental" runat="server" SkinID="lista_desplegable" AutoPostBack="True" Visible="False" Width="118px">
                            </asp:DropDownList>
                            <br /><br />
                            <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClientClick="deshabilitarBoton()"
                                OnClick="btnAceptar_Click" SkinID="boton_copia" />&nbsp;<asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click"
                                SkinID="boton_copia" Width="68px" CausesValidation="False" />
                              
                         </td> 
                    </tr>
                    <tr>
                        <td style="height: 20px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                &nbsp;&nbsp;

    </div>
</asp:Content>

<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="NotTiempo.aspx.cs" Inherits="Administracion_Tablasbasicas_NotTiempo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="div-contenido">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        &nbsp; &nbsp;
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="600">
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblDescripcion" SkinID="summary" runat="server" Text="Escriba a continuación el tiempo que tardará el servicio de notificación en consultar el estado de los Actos Administrativos pendientes por Ejecutoriar en Notificación en Línea - PDI"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTiempo" SkinID="etiqueta_negra" runat="server" Text="Tiempo de Espera  (Horas):" Width="159px"></asp:Label>
                        </td>
                        <td style="width:300px; vertical-align:middle; height:50px">
                            <asp:TextBox ID="txtTiempo" SkinID="texto_corto" runat="server" CausesValidation="True"></asp:TextBox>
                            <asp:ImageButton ID="btnArriba" runat="server" ImageUrl="~/App_Themes/Img/iconos/arriba.png" ImageAlign="Middle" />&nbsp;
                            <asp:ImageButton ID="btnAbajo" runat="server" ImageUrl="~/App_Themes/Img/iconos/abajo.png" ImageAlign="Middle" />
                            <br />
                            <asp:CheckBox ID="chkActivo" runat="server" Checked="True" SkinID="check" Text="Activo" /><br />
                            <cc1:NumericUpDownExtender ID="NumericUpDownExtender1" TargetButtonUpID="btnArriba" TargetButtonDownID="btnAbajo" Minimum="0.5" Maximum="30" Step="0.5" TargetControlID="txtTiempo" runat="server">
                            </cc1:NumericUpDownExtender>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Text="*" ControlToValidate="txtTiempo" runat="server" ErrorMessage="*Debe escribir un Nombre"></asp:RequiredFieldValidator>
                        
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td style="width: 135px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center" style="height: 26px">
                            <asp:Button ID="btnGuardar" SkinID="boton_copia" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
                            <asp:Button ID="btnCancelar" SkinID="boton_copia" runat="server" Text="Cancelar" OnClientClick="atras()" CausesValidation="False" />&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3" style="height: 26px">
                            <asp:Label ID="lblResultado" runat="server" SkinID="etiqueta_verde"></asp:Label></td>
                    </tr>
                </table>
                
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>



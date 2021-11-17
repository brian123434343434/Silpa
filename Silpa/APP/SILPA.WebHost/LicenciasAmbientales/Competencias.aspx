<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPASinMenu.master" AutoEventWireup="true"
    CodeFile="Competencias.aspx.cs" Inherits="LicenciasAmbientales_Competencias"
    Title="Conflicto de Competencias" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
    function deshabilitarBoton(e, obj) {        
        obj.disabled = true;
    }
</script>
    <div class="div-titulo-sin">
        <%--<asp:Label ID="lbl_titulo_principal" runat="server" Text="SOLICITUD DE DIAGNOSTICO AMBIENTAL DE ALTERNATIVAS Y/O TERMINOS DE REFERENCIA PARA EL ESTUDIO DE IMPACTO AMBIENTAL
Base legal: Decreto 1220 DE 2005"
            SkinID="normal"></asp:Label>--%>
    </div>
    <div class="div-contenido">
    
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:UpdatePanel ID="pnlPrincipal" runat="server">
        <ContentTemplate>
                <table style="width: 600px">
                    <tr>
                        <td>
                            &nbsp;<asp:Label ID="lblEnlaceDescarga" Visible="false" runat="server" SkinID="etiqueta_negra"
                                Text="Descargue los TDR para DAA:"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;<asp:HyperLink ID="hplEnlaceDescarga" Visible="false" runat="server"></asp:HyperLink></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMensaje" Visible="False" runat="server" SkinID="etiqueta_negra"
                                Text="Se ha detectado que su Proyecto, Obra o Actividad tiene jurisdicción en varias Autoridades Ambientales, por tal razón usted puede:" Width="598px"></asp:Label></td>
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
                            </asp:DropDownList><br />                                                
                         </td>
                    </tr>
                    <tr>                    
                        <td>
                                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClientClick="deshabilitarBoton(event,this)"
                                OnClick="btnAceptar_Click" SkinID="boton_copia" Visible="False" />
                                <asp:Button ID="btnSolicitarTDREIA" Visible="False" SkinID="boton_copia" runat="server"
                                Text="Solicitar TDR para EIA"  OnClick="btnSolicitarTDREIA_Click" />
                                <asp:Button ID="btnSolicitarDAA" Visible="false" runat="server" Text="Solicitar DAA"
                                SkinID="boton_copia" OnClientClick="deshabilitarBoton(event,this)" OnClick="btnSolicitarDAA_Click" />
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click"
                                SkinID="boton_copia" Width="68px" CausesValidation="False" />
                            
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 20px">
                        </td>
                    </tr>
                    <tr>
                      
                          &nbsp;</tr>
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
                </ContentTemplate>       
        </asp:UpdatePanel>
        &nbsp;
        <br />
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="pnlPrincipal">
                                <ProgressTemplate>
                                    <asp:Label ID="lblProcesando" runat="server" SkinID="etiqueta_blanca" Text="Procesando, Por Favor Estpere..."></asp:Label><br />
                                    <asp:Image ID="imgProgeso" BackColor="white" ImageAlign="middle" runat="server" ImageUrl="~/App_Themes/Img/ajax-loader.gif" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <br />
        <br />
        <cc1:UpdatePanelAnimationExtender ID="extender" runat="server" TargetControlID="pnlPrincipal" Enabled="False">
            <Animations>
                                <OnUpdating>
                                   <Sequence>
                                     <EnableAction AnimationTarget="btnSolicitarTDREIA"  Enabled="false" />
                                      <EnableAction AnimationTarget="btnSolicitarDAA"  Enabled="false" />
                                       <EnableAction AnimationTarget="btnCancelar"  Enabled="false" />
                                   </Sequence>
                               </OnUpdating></Animations>
        </cc1:UpdatePanelAnimationExtender>
    </div>
</asp:Content>

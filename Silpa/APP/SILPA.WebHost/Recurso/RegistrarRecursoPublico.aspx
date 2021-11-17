<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPASinMenuFlash.master" AutoEventWireup="true" CodeFile="RegistrarRecursoPublico.aspx.cs" Inherits="Recurso_RegistrarRecursoPublico" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlaceHolder" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script language="javascript" type="text/javascript">

    function closeDialog() {
        opener.refreshWindow();
        self.close();
    }

    function closeDialogUpdate() {
        opener.refreshWindowRollback();
        self.close();
    }
    
</script>
  <script type="text/javascript" src="../js/JScript.js"></script>

    <div class="div-titulo">
        <asp:Label id="lblNumeroSilpaReal" runat="server" visible="false"></asp:Label>
        <asp:Label ID="lblTituloPrincipal" runat="server" SkinID="titulo_principal_blanco"
            Text="RECURSO DE REPOSICIÓN"></asp:Label>
    </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table width="100%">
        <tr>
        <td width="10%"></td>
        <td>
        <%--<cc1:TabContainer id="tbcContenedor" runat="server" ActiveTabIndex="0">
        <cc1:TabPanel runat="server" HeaderText="Datos de Recurso" ID="tabDatosRecurso">
        <ContentTemplate>--%>
        <table>
        <tr>
            <td style="HEIGHT: 21px" colSpan=3>&nbsp; 
            </td>
         </tr>
         <tr>
            <TD colSpan=3>&nbsp;</td>
         </TR>
         <tr>
            <td>
                <asp:Label id="lblNombreProy" runat="server" Text="Nombre Proyecto:"></asp:Label> 
            </td>
            <td>
                <asp:Label id="lblNombreProyDato" runat="server"></asp:Label> 
            </td>
            <td>
            </td>
         </TR>
         <tr>
            <td>
                <asp:Label id="lblNumeroSILPA" runat="server" Text="Número VITAL:"></asp:Label> 
            </td>
            <td>
                <asp:Label id="lblNumeroSILPADato" runat="server"></asp:Label> 
            </td>
            <td>
            </td>
         </TR>
         <tr>
            <td>
                <asp:Label id="lblNumeroExpediente" runat="server" Text="Número de Expediente:"></asp:Label> 
            </td>
            <td>
                <asp:Label id="lblNumeroExpedienteDato" runat="server"></asp:Label> 
            </td>
            <td>
            </td>
         </TR>
         <tr>
            <td>
                <asp:Label id="Label4" runat="server" Text="Número Acto Administrativo:"></asp:Label> 
            </td>
            <td>
                <asp:Label id="lblNumeroActoDato" runat="server"></asp:Label> 
            </td>
            <td>
            </td>
         </TR>
         <tr>
            <td>
                <asp:Label id="lblFechaActo" runat="server">Fecha de Acto Administrativo:</asp:Label> 
            </td>
            <td>
                <asp:Label id="lblFechaActoDato" runat="server"></asp:Label> 
            </td>
            <td>
            </td>
         </TR>
         <tr>
            <td>
                <asp:Label id="lblFechaNoti" runat="server">Fecha de Notificacion:</asp:Label> 
            </td>
            <td>
                <asp:Label id="lblFechaNotiDato" runat="server"></asp:Label> 
            </td>
            <td>
            </td>
         </TR>
         <tr>
            <td>
                <asp:Label id="lblDescripcion" runat="server">Descripcion</asp:Label> 
            </td>
            <td>
                <asp:TextBox id="txtDescripcion" runat="server" SkinID="texto" TextMode="MultiLine"></asp:TextBox>
             </td>
             <td>
             </td>
         </TR>
         <tr>
            <td></td><td></td><td></td></TR>
                 <tr><td></td><td></td><td></td></TR>
                <tr><TD colSpan=3><asp:Label id="lblMensajeAgregar" runat="server" Text='Para agregar documentos a su recurso, por favor dar clic sobre el botón "Agregar"'></asp:Label> </td></TR><tr><TD style="HEIGHT: 74px" colSpan=3>
                <asp:GridView id="grdDocumentosRecurso" runat="server" SkinID="Grilla_simple_peq" OnRowCommand="grdDocumentosRecurso_RowCommand" Width="493px" DataKeyNames="ID">
                <Columns>
        <asp:ButtonField CommandName="Actualizar" Text="Ver"></asp:ButtonField>
        <asp:ButtonField CommandName="Eliminar" Text="Eliminar"></asp:ButtonField>
            </Columns>
            </asp:GridView> </td></TR><tr><TD style="HEIGHT: 26px">&nbsp; <asp:Button id="btnAgregarDocumentoRecurso" onclick="btnAgregarDocumentoRecurso_Click" runat="server" Text="Agregar" SkinID="boton_copia" CausesValidation="False"></asp:Button> <DIV style="VISIBILITY: hidden"><asp:Button id="btnActualizarDocumentoRecurso" runat="server" Text="Actualizar" SkinID="boton_copia" CausesValidation="False"></asp:Button> </DIV></td><TD style="HEIGHT: 26px">&nbsp;</td><TD style="HEIGHT: 26px"></td></TR><tr><TD style="HEIGHT: 26px" colSpan=2>&nbsp;&nbsp; </td></TR></TABLE>
       <%-- </ContentTemplate>
        </cc1:TabPanel>
        </cc1:TabContainer> --%>

        <asp:Button ID="btnRegistrar" runat="server" CausesValidation="False" SkinID="boton_copia"
                                            Text="Enviar" OnClick="btnRegistrar_Click" OnClientClick="fntMensajeRecurso" />
        </td>
        <td width="10%"></td>
        </tr>
        </table>
   <cc1:ModalPopupExtender ID="mpeAgregarDocumento" runat="server" PopupControlID="dvAgregarDocumento" TargetControlID="lblDescripcion" BehaviorID="mpeAgregarDocumento" CancelControlID="btnCancelar" BackgroundCssClass="caja-dialogo-fondo-aplicacion">
    </cc1:ModalPopupExtender>         
        <div id="dvAgregarDocumento" style="display:none;" class="caja-dialogo2">
          <table>
                        <tr>
                            <td colspan="3" style="height: 21px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Documento"></asp:Label>
                            </td>
                            <td>
                                <asp:FileUpload ID="fupDocumento" runat="server" /></td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNumeroRadicado" runat="server" Text="Número de Radicado:"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtNumeroRadicado" runat="server"></asp:TextBox></td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 26px">
                                <asp:Button ID="btnAceptar" runat="server" OnClick="btnAceptar_Click" Text="Aceptar"
                                    SkinID="boton_copia" />
                            </td>
                            <td style="height: 26px">
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" SkinID="boton_copia" />
                            </td>
                            <td style="height: 26px">
                            </td>
                        </tr>
                    </table>      
        </div>
</asp:Content>


<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RegistrarRecursoReposicion.ascx.cs" Inherits="Recurso_Controles_RegistrarRecursoReposicion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script src="../js/Ayuda.js" type="text/javascript"></script>
<link href="../App_Themes/skin/AyudaStyle.css" rel="stylesheet" />

<script type="text/javascript">
    var progressVisible = false;

    function MostrarProgressDropDownCargarArchivo() {
        $get('<%= uppModalPresentarRecurso.ClientID %>').style.display = 'block';
        progressVisible = true;
    }

    function OcultarProgressDropDownCargarArchivo() {
        if (progressVisible) {
            $get('<%= uppModalPresentarRecurso.ClientID %>').style.display = 'none';
            }
    }

    function ErrorArchivoRecurso() {
        OcultarProgressDropDownCargarArchivo();
        alert("El tamaño del archivo sobrepasa el máximo permitido");
    }

    $(function () {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {

            var plugin = $.keypad;

            if ($("#dvModalPresentarRecurso").is(":visible")) {

                $("#formWithKeyPadPresentarRecurso").append(plugin.mainDiv).on('mousedown.' + plugin.name, plugin._checkExternalClick);

                $('#txtSegundaContrasenaPresentarRecurso').keypad({
                    randomiseNumeric: true,
                    prompt: '',
                    closeText: 'Cerrar',
                    clearText: 'Limpiar',
                    backText: '<<',
                    Width: 160.75
                });
            }

        });
    });

</script>

<div id="formWithKeyPadPresentarRecurso"></div>

<asp:UpdatePanel runat="server" ID="upnlModalPresentarRecurso" UpdateMode="Conditional">
    <ContentTemplate>   
        <table id="dvModalPresentarRecurso" class="TablaFormularioNotElec">
            <tr>
                <td class="TituloSeccionNotElec" colspan="2">
                    PRESENTAR RECURSO DE REPOSICIÓN
                </td>
            </tr>
            <tr>
                <td class="ModalLabelFormularioBusquedaNotElec">
                    Número Vital:
                </td>
                <td class="ModalCamposFormularioBusquedaNotElec">
                    <asp:Literal runat="server" ID="ltlNumeroVital"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="ModalLabelFormularioBusquedaNotElec">
                    Autoridad Ambiental:
                </td>
                <td class="ModalCamposFormularioBusquedaNotElec">
                    <asp:Literal runat="server" ID="ltlAutoridadAmbiental"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="ModalLabelFormularioBusquedaNotElec">
                    Número de Expediente:
                </td>
                <td class="ModalCamposFormularioBusquedaNotElec">
                    <asp:Literal runat="server" ID="ltlExpediente"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="ModalLabelFormularioBusquedaNotElec">
                    Número Acto Administrativo:
                </td>
                <td class="ModalCamposFormularioBusquedaNotElec">
                    <asp:Literal runat="server" ID="ltlNumeroActoAdministrativo"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="ModalLabelFormularioBusquedaNotElec">
                    Fecha de Acto Administrativo:
                </td>
                <td class="ModalCamposFormularioBusquedaNotElec">
                    <asp:Literal runat="server" ID="ltlFechaActoAdministrativo"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="ModalLabelFormularioBusquedaNotElec">
                    Fecha de Notificacion:
                </td>
                <td class="ModalCamposFormularioBusquedaNotElec">
                    <asp:Literal runat="server" ID="ltlFechaNotificacion"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="ModalLabelFormularioBusquedaNotElec">
                    Descripción:
                    <span id="spnDescripcionPresentarRecurso" class="botonAyudaUP" title='Ingrese la descripción del recusro de reposición que desea presentar sobre el acto administrativo.".' divModal="dvModalPresentarRecurso"></span>
                </td>
                <td class="ModalCamposFormularioBusquedaNotElec">
                    <asp:TextBox runat="server" ID="txtDescripcionPresentarRecurso" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDescripcionPresentarRecurso" Display="Dynamic" runat="server" ControlToValidate="txtDescripcionPresentarRecurso" ErrorMessage="Debe ingresar la descripción del recurso de reposición que va ha presentar">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="ModalLabelFormularioBusquedaNotElec">
                    Documentos:
                    <span id="spnTipoNotificacion" class="botonAyudaUP" title='Para ingresar documentos seleccione el archivo y haga clic en el botón de "Adicionar". Si desea retirar un documento haga clic en el link de "Eliminar" correspondiente al archivo que desea retirar del listado.' divModal="dvModalPresentarRecurso"></span><br />
                    <asp:CustomValidator runat="server" ID="cvListaDocumentosPresentarRecurso" ErrorMessage="Debe ingresar por lo menos un archivo en el listado" OnServerValidate="cvListaDocumentosPresentarRecurso_ServerValidate">*</asp:CustomValidator>
                </td>
                <td class="ModalCamposFormularioBusquedaNotElec">
                    <div style="display:none">
                        <cc1:AsyncFileUpload runat="server" ID="fuplRegisterNoVisible"  />
                    </div>
                    <asp:GridView runat="server" AutoGenerateColumns="False" ID="grdArchivosPresentarRecurso" ShowFooter="True" SkinID="GrillaListaNotificacionElectronica">
                        <Columns>                                                                                        
                            <asp:TemplateField HeaderText = "DOCUMENTO">
                                <ItemTemplate>
                                    <asp:Literal ID="lblNombreDocumento" runat="server" Text='<%# Eval("NombreDocumento")%>'></asp:Literal>
                                </ItemTemplate>
                                <FooterTemplate>                                                    
                                    <cc1:AsyncFileUpload runat="server" ID="fuplCargarDocumentoPresentarRecurso" ClientIDMode="AutoID" CssClass="FileUploadRegistroNot" OnClientUploadStarted="MostrarProgressDropDownCargarArchivo" OnClientUploadComplete="OcultarProgressDropDownCargarArchivo" OnClientUploadError="ErrorArchivoRecurso" />                                                    
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VER">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkVerArchivoPresentarRecurso" runat="server" Text="Ver" CommandArgument="<%# Container.DataItemIndex %>" OnClick="lnkVerArchivoPresentarRecurso_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ELIMINAR">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEliminarArchivoPresentarRecurso" runat="server" Text="Eliminar" CommandArgument="<%# Container.DataItemIndex %>" OnClick="lnkEliminarArchivoPresentarRecurso_Click"/>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lnkAdicionarArchivoPresentarRecurso" runat="server" Text="Adicionar" CausesValidation="true" ValidationGroup="ListaArchivosPresentarRecurso" OnClick="lnkAdicionarArchivoPresentarRecurso_Click"></asp:LinkButton>
                                    <asp:CustomValidator runat="server" ID="cvCargarDocumentoPresentarRecurso" ErrorMessage="Debe ingresar por lo menos un archivo en el listado" OnServerValidate="cvCargarDocumentoPresentarRecurso_ServerValidate" ValidationGroup="ListaArchivosPresentarRecurso">&nbsp;</asp:CustomValidator>
                                </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr runat="server" id="trSegundaClave">
                <td class="ModalLabelFormularioBusquedaNotElec">
                    Segunda Clave:
                    <span id="spnSegundaContrasena" class="botonAyudaUP" title='Ingrese por medio del teclado virtual la segunda clave VITAL, en caso de no haberla registrado haga clic en la opción de "Seguridad" del menú principal y luego ingrese en la opción de "Segunda Clave".' divModal="dvModalPresentarRecurso"></span>
                </td>
                <td class="ModalCamposFormularioBusquedaNotElec">
                    <asp:TextBox runat="server" ID="txtSegundaContrasenaPresentarRecurso" TextMode="Password" ClientIDMode="Static" MaxLength="6"></asp:TextBox>                                        
                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="rfvSegundaContrasenaPresentarRecurso" ControlToValidate="txtSegundaContrasenaPresentarRecurso" ErrorMessage="Debe ingresar la segunda clave">*</asp:RequiredFieldValidator>
                    <asp:CustomValidator runat="server" ID="cvSegundaContrasena" ErrorMessage="La segunda clave ingresada es incorrecta" OnServerValidate="cvSegundaContrasena_ServerValidate">*</asp:CustomValidator>
                </td>
            </tr>
        </table>        
        <asp:ValidationSummary runat="server" ID="valListaArchivosPresentarRecurso" ValidationGroup="ListaArchivosPresentarRecurso" ShowSummary="false" ShowMessageBox="true" />
    </ContentTemplate>    
</asp:UpdatePanel>

<asp:UpdateProgress ID="uppModalPresentarRecurso" runat="server" AssociatedUpdatePanelID="upnlModalPresentarRecurso">
    <ProgressTemplate>  
        <div id="ModalProgressContainer">
            <div>
                <p>Procesando...</p>
                <p><asp:Image ID="imgUpdateProgresModalPresentarRecurso" runat="server" SkinId="procesando"/></p>
            </div>
        </div>                         
    </ProgressTemplate>
</asp:UpdateProgress>

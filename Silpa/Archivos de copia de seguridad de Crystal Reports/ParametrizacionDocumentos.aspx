<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="ParametrizacionDocumentos.aspx.cs" Inherits="AdquisicionDocumentos_ParametrizacionDocumentos" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content3" ContentPlaceHolderID="headPlaceHolder" runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        //Trim the input text
        function Trim(input) {
            var lre = /^\s*/;
            var rre = /\s*$/;
            input = input.replace(lre, "");
            input = input.replace(rre, "");
            return input;
        }

        // filter the files before Uploading for text file only  
        function CheckForTestFile() {

            var file = $("#fluArchivoImagenEdit");
            var fileName = file.val();
            //Checking for file browsed or not 
            if (Trim(fileName) == '') {
                alert("Seleccione un archivo para subir");
                file.focus();
                return false;
            }

            //Setting the extension array for diff. type of text files 
            var extArray = new Array(".JPG", ".GIF", ".PNG");

            //getting the file name
            while (fileName.indexOf("\\") != -1)
                fileName = fileName.slice(fileName.indexOf("\\") + 1);

            //Getting the file extension                     
            var ext = fileName.slice(fileName.indexOf(".")).toLowerCase();

            //matching extension with our given extensions.
            for (var i = 0; i < extArray.length; i++) {
                if (extArray[i] == ext.toUpperCase()) {
                    return true;
                }
            }
            alert("Solo se admiten archivos con las siguientes extensiones:  "
              + (extArray.join("  ")) + "\nSeleccione un nuevo archivo");
            file.focus();
            return false;
        }
    </script>

    <style type="text/css">
        table {
            /*border: 1px solid #000;*/
        }
        table tr td {
            border: 0px solid #ddd !important;
            padding: 4px;
        }
        .Button{
            background-color: #ddd;
        }

        .CajaDialogo {
            background-color: #fff;
            border-width: 1px;
            border-style: outset;
            border-color: Yellow;
            padding: 0px;
        }

            .CajaDialogo div {
                margin: 5px;
            }

        .FondoAplicacion {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .accordionContent {
            background-color: #D3DEEF;
            border-color: -moz-use-text-color #2F4F4F #2F4F4F;
            border-right: 1px dashed #2F4F4F;
            border-style: none dashed dashed;
            border-width: medium 1px 1px;
            padding: 10px 5px 5px;
            /*width:20%;*/
        }

        .accordionHeaderSelected {
            background-color: #E0F1FF;
            border: 1px solid #2F4F4F;
            color: white;
            cursor: pointer;
            font-family: Arial,Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            margin-top: 5px;
            padding: 5px;
            /*width:20%;*/
        }

        .accordionHeader {
            background-color: #D5EBFF;
            border: 1px solid #2F4F4F;
            color: white;
            cursor: pointer;
            font-family: Arial,Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            margin-top: 5px;
            padding: 5px;
            /*width:20%;*/
        }

        .href {
            color: White;
            font-weight: bold;
            text-decoration: none;
        }

        .ajax__calendar_container {
            position: absolute;
            z-index: 100003 !important;
            background-color: white;
        }
    </style>

    <div class="div-titulo">
        <asp:Label ID="lbl_titulo_principal" runat="server" SkinID="titulo_principal_blanco" Text="Parametrización de Documentos"></asp:Label>
    </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <%--<div class="div-contenido2">--%>
    <div class="table-responsive">
        <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
            <tbody>
                <tr>
                    <td>
                        <asp:Label ID="lblNombreDoc" runat="server" SkinID="etiqueta_negra" Width="150px" Text="Nombre del Documento"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNombreDocumento" runat="server" SkinID="texto" Width="100px" MaxLength="200"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvDocumento" runat="server" ErrorMessage="¡El nombre del documento es requerido!" ControlToValidate="txtNombreDocumento" Display="Dynamic">*</asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblEnlaceAplicativo" runat="server" SkinID="etiqueta_negra" Width="150px" Text="Enlace del Aplicativo"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEnlace" runat="server" SkinID="texto" Width="100px" MaxLength="200"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEnlace" runat="server" ErrorMessage="¡El enlace del aplicativo es requerido!" ControlToValidate="txtEnlace" Display="Dynamic">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEntidad" runat="server" SkinID="etiqueta_negra" Width="150px" Text="Entidad Externa"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="cboEntidad" runat="server" SkinID="lista_desplegable" OnSelectedIndexChanged="cboEntidad_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblTipoAdquisicion" runat="server" SkinID="etiqueta_negra" Width="150px" Text="Tipo de aquisición"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="cboTipoAdquisicion" runat="server" SkinID="lista_desplegable" OnSelectedIndexChanged="cboTipoAdquisicion_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCodigoProceso" runat="server" SkinID="etiqueta_negra" Width="150px" Text="Codigo Proceso" maxlength="100"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCodigoProceso" runat="server" SkinID="texto" Width="100px" MaxLength="200"></asp:TextBox>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="4" style="padding: 20px; text-align: center; vertical-align: middle;">
                        <asp:Button ID="cmdGuardar" OnClick="cmdGuardar_Click" runat="server" Text="Guardar"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="padding-top: 10px; padding-bottom: 10px; text-align: left; vertical-align: middle;">
                        <asp:ValidationSummary ID="valResumen" runat="server"></asp:ValidationSummary>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align: left; vertical-align: middle;">
                        <cc1:FilteredTextBoxExtender ID="fteNombre" runat="server" TargetControlID="txtNombreDocumento" FilterType="Custom, Numbers, UppercaseLetters, LowercaseLetters" ValidChars="/,.:;-\&quot;ñÑáéíóú ÁÉÍÓÚ()#@'"></cc1:FilteredTextBoxExtender>
                    </td>
                </tr>
            </tbody>
        </table>
        <div style="margin: 0 !important; padding: 0 !important; width: 100%; overflow: auto; text-align: left; vertical-align: top;">
            <asp:GridView ID="grdDocumentos" runat="server" Width="100%" 
                DataKeyNames="DOC_ID" GridLines="None" ForeColor="#333333" 
                CellPadding="4" AutoGenerateColumns="False" PageSize="1" 
                OnPageIndexChanging="grdDocumentos_PageIndexChanging" 
                OnRowCommand="grdDocumentos_RowCommand" 
                OnRowDataBound="grdDocumentos_RowDataBound">
                <RowStyle BackColor="#E3EAEB"></RowStyle>
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgEditar" SkinID="icoEditar" runat="server" CommandName="Editar" CommandArgument='<%# Eval("DOC_ID") %>' CausesValidation="false" style="cursor: pointer;"></asp:ImageButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="DOC_ID" HeaderText="C&#243;digo del Documento" SortExpression="DOC_ID" Visible="False"></asp:BoundField>
                    <asp:BoundField DataField="NOMBRE" HeaderText="Nombre del Documento" SortExpression="NOMBRE"></asp:BoundField>
                    <asp:BoundField DataField="ENTIDAD" HeaderText="Entidad " SortExpression="ENTIDAD"></asp:BoundField>
                    <asp:BoundField DataField="ENLACE" HeaderText="Enlace Aplicativo" SortExpression="ENLACE"></asp:BoundField>
                    <asp:BoundField DataField="ADQUISICION" HeaderText="Tipo de adquisici&#243;n" SortExpression="ADQUISICION"></asp:BoundField>
                    <asp:BoundField DataField="CODIGO_PROCESO" HeaderText="Código Proceso" SortExpression="CODIGO_PROCESO"></asp:BoundField>
                    <asp:TemplateField HeaderText="Imagen">
                        <ItemTemplate>
                            <asp:Image ID="imgDocumento" runat="server" Width="100%"></asp:Image>
                            <asp:HiddenField ID="hdfImagen" runat="server" Value='<%# Eval("IMAGEN_URL") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></FooterStyle>
                <PagerStyle HorizontalAlign="Center" BackColor="#666666" ForeColor="White"></PagerStyle>
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></HeaderStyle>
                <EditRowStyle BackColor="#7C6F57"></EditRowStyle>
                <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
            </asp:GridView>
        </div>

        <asp:Label ID="lblEdicion" runat="server"></asp:Label>
        <cc1:ModalPopupExtender ID="mpeEdicionDocumento" runat="server"
            TargetControlID="lblEdicion"
            PopupControlID="pnlEdicionDocumento"
            DropShadow="True" Enabled="True" DynamicServicePath=""
            BackgroundCssClass="FondoAplicacion" />

        <asp:Panel ID="pnlEdicionDocumento" runat="server" Style="display: none;" CssClass="CajaDialogo">
            <asp:UpdatePanel ID="upnlAvanzar" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnAceptar" />
                    <asp:PostBackTrigger ControlID="btnCancelar" />
                </Triggers>
                <ContentTemplate>
                    <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
                        <tr>
                            <td SkinID="etiqueta_negra">Nombre:</td>
                            <td>
                                <asp:TextBox ID="txtNombreEdit" runat="server" Width="95%" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                <asp:HiddenField ID="hdDocID" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td SkinID="etiqueta_negra">Entidad:</td>
                            <td>
                                <asp:DropDownList ID="cboEntidadEdit" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfEntidadEdit" runat="server" ErrorMessage="Debe Seleccionar una Entidad" ControlToValidate="cboEntidadEdit" Display="Dynamic" InitialValue="" ValidationGroup="Edit">*</asp:RequiredFieldValidator>
                                <asp:HiddenField ID="hdfEntidadEdit" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td SkinID="etiqueta_negra">Enlace:</td>
                            <td>
                                <asp:TextBox ID="txtEnlaceEdit" runat="server" Width="95%" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvEnlaceEdit" runat="server" ErrorMessage="Debe Ingresar un Enlace" ControlToValidate="txtEnlaceEdit" Display="Dynamic" ValidationGroup="Edit">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td SkinID="etiqueta_negra">Tipo Adquisición:</td>
                            <td>
                                <asp:DropDownList ID="cboTipoAdquisicionEdit" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Debe Seleccionar un Tipo de Adquisición" ControlToValidate="cboTipoAdquisicionEdit" Display="Dynamic" InitialValue="" ValidationGroup="Edit">*</asp:RequiredFieldValidator>
                                <asp:HiddenField ID="hdfTipoAdquisicionEdit" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td SkinID="etiqueta_negra">Codigo Proceso:</td>
                            <td>
                                <asp:TextBox ID="txtCodigoProceosEdit" runat="server" MaxLength="10" Width="80%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td SkinID="etiqueta_negra">Imagen:</td>
                            <td>
                                <asp:FileUpload ID="fluArchivoImagenEdit" runat="server" ClientIDMode="Static" />
                                <asp:RequiredFieldValidator ID="rfvArchivoImagenEdit" runat="server" ErrorMessage="Debe Seleccionar un Archivo de Imagen" ControlToValidate="fluArchivoImagenEdit" ValidationGroup="Edit">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="padding: 20px; text-align: center; vertical-align: middle;">
                                <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; width: 100%;">
                                    <tr>
                                        <td style="padding-right: 15px; text-align: right; vertical-align: middle;">
                                            <asp:Button ID="btnAceptar" ClientIDMode="Static" runat="server" Text="Aceptar" SkinID="boton_copia" ValidationGroup="Edit" OnClientClick="return CheckForTestFile();" OnClick="btnAceptar_Click" />
                                        </td>
                                        <td style="padding-left: 15px; text-align: left; vertical-align: middle;">
                                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="false" SkinID="boton_copia" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>

        <br />
        &nbsp;
    </div>
</asp:Content>


<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="CorreoPlantilla.aspx.cs" Inherits="Administracion_Tablasbasicas_CorreoPlantilla" ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:content id="Content1" contentplaceholderid="ContentPlaceHolder1" runat="Server">

    <link href="../../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />

    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/jquery/fontsize/js/jquery.jfontsize-1.0.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/5.0.1/js/bootstrap.min.js") %>' type="text/javascript"></script>
    

    <style>
        .opciones {
            margin-top:15px;
            padding:10px 0px 10px 0px ;
        }

        input[type=text] {
            width: 90%;
            border: 1px solid #C3DBF9;
            border-radius: 5px;
            opacity: 1;
            text-align: left;
            letter-spacing: 0px;
            color: #4B4B4B;
            text-indent: 3%;
            outline: none;
        }

        .div-contenido .row {
            display: block;
        }

        .div-contenido table td {
            /*padding-top: calc(.220rem + 5px);
            padding-bottom: calc(.220rem + 5px);*/
        }
        .form-group row [input]{
            display: inline-block !important;
        }

        .col-sm-2 {
            display: flex;
        }

            .col-sm-2 span {
                padding-top: calc(.220rem + 1px);
                padding-bottom: calc(.220rem + 1px);
                margin-bottom: 0;
                line-height: 1.5;
            }
        .container-loader-correoplantilla {
            background-color: #868889 !important;
            opacity: 0.3;
            position: fixed;
            left: 0px;
            top: 0px;
            z-index: 10001;
            width: 100%;
            height: 100%;
        }

        .loader-correoplantilla {
            position: fixed;
            z-index: 10002;
            top: 0;
            left: 0;
            margin-left: calc((100vw - 120px)/2);
            margin-top: calc((100vh - 120px)/2);
            border: 16px solid #F1F1F1; /* Gris */
            border-top: 16px solid #3366CB; /* Azul */
            border-radius: 50%;
            width: 120px;
            height: 120px;
            animation: spin 2s linear infinite;
        }
        .ModalBackgroundCorreoPlantilla {
            background-color:Gray !important;
	        filter:alpha(opacity=40) !important;
	        opacity:0.4 !important;
            border:1px solid black;
            z-index: 100 !important;
        }
        .ContenedorModalCorreoPlantilla {
            max-height:700px;
            border:1px solid black;
            overflow-y: scroll;
            background-color: #FFFFFF;
            margin-left: auto;
            margin-right: auto;
            padding: calc(.220rem + 5px);
            -moz-border-radius: 7px;
            -webkit-border-radius: 7px;
        }
        

    </style>
    <script>
        function repaintEditors() {
            try {
                $telerik.$(".RadEditor").each(function (index, elem) {
                    if (elem.control && elem.control.repaint) elem.control.repaint();

                });
            }
            catch (ex) {
                //jQuery and/or Telerik libraries are not loaded (yet)
            }
        }
        $(document).ready(function () {
            repaintEditors();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                repaintEditors();
            }
        });
        
    </script>
  
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    
    <div class="div-contenido col-md-12">
        <asp:UpdatePanel ID="upnlBotonosConsulta" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="titulo_pagina">
                        Administración plantilla de correos
                    </div>
                </div>
                <div class="row opciones">
                    <div class="col-12">
                        <div class="form-group row">
                            <div class="col-sm-2">
                                <asp:Label ID="lblNombreParametro" CssClass="labelVITAL" runat="server" Text="Descripción"></asp:Label>
                            </div>
                            <div class="col-sm-10">
                                <asp:TextBox ID="txtNombreParametro" SkinID="texto" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group row text-right">
                            <div class="col-sm-12">
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary btn-sm" OnClick="btnBuscar_Click" />
                                <asp:Button ID="btnagregar" runat="server" Text="Agregar" CssClass="btn btn-primary btn-sm" OnClick="btnagregar_Click" Visible="false" />
                                <asp:Button ID="btnVolver" runat="server" CssClass="btn btn-secondary btn-sm" Text="Cancelar" PostBackUrl="~/Administracion/Tablasbasicas/TablasBasicas.aspx"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row resultados">
                   <%-- <asp:Panel ID="pnlConsultar" runat="server" Width="100%" Visible="true">
                            <asp:Panel ID="pnlConsultaPlantilla" runat="server" ScrollBars="Auto">--%>
                                <asp:GridView ID="grdDatos" runat="server" Width="100%" OnPageIndexChanging="grdDatos_PageIndexChanging" CssClass="tabla_datos"
                                    DataKeyNames="CORREO_PLANTILLA_ID" OnRowCommand="grdDatos_RowCommand" EmptyDataText="No existen datos registrados en ésta tabla"
                                    AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False" PageSize="10" OnSelectedIndexChanged="grdDatos_SelectedIndexChanged" PagerStyle-CssClass="paginador">
                                    <Columns>
                                        <asp:BoundField DataField="CORREO_PLANTILLA_ID" HeaderText="Id Plantilla"></asp:BoundField>
                                        <asp:BoundField DataField="DE" HeaderText="De"></asp:BoundField>
                                        <asp:BoundField DataField="CC" HeaderText="CC"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Ver">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgBVerPlantilla" runat="server" CommandName="VerPlantilla" CommandArgument='<%# Container.DataItemIndex %>' ImageUrl="~/App_Themes/Img/ver.png" />
                                                <asp:HiddenField ID="hdfPlantilla" runat="server" Value='<%# Eval("PLANTILLA") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ASUNTO" HeaderText="Asunto" />
                                        <asp:BoundField DataField="CORREO_SERVIDOR_ID" HeaderText="Id Servidor Correo"></asp:BoundField>
                                        <asp:BoundField DataField="NOMBRE_SERVIDOR" HeaderText="Nombre Servidor"></asp:BoundField>
                                        <asp:BoundField DataField="CONFIRMAR_ENVIO" HeaderText="Confirma Envio"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Editar">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkModificar" CommandName="Modificar" runat="server" CommandArgument='<%# Container.DataItemIndex %>'>Modificar Registro</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Eliminar">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEliminar" CommandName="Eliminar" runat="server" CommandArgument='<%# Container.DataItemIndex %>'
                                                    OnClientClick="return confirm('Esta seguro de Eliminar este registro?')">Eliminar Registro</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings FirstPageText="First Page" LastPageText="Last Page" NextPageText="Siguiente" PreviousPageText="Anterior" Mode="NumericFirstLast" Position="Bottom" />
                                </asp:GridView>
                        <%--    </asp:Panel>
                        </asp:Panel>--%>
                </div>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
         <asp:UpdateProgress ID="upplAccionesBoton" runat="server" AssociatedUpdatePanelID="upnlBotonosConsulta" ClientIDMode="Static">
        <ProgressTemplate>  
           <div id="container-loader" class="container-loader-correoplantilla"></div>
           <div id="loader" class="loader-correoplantilla"></div>
        </ProgressTemplate>
    </asp:UpdateProgress>

                <asp:UpdatePanel ID="updConsultar" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="pnlEditar" runat="server" Visible="false">
                        </asp:Panel>
                        <asp:Panel ID="pnlNuevo" runat="server" Visible="false">
                            <table>
                                <tbody>
                                    <tr>
                                        <td style="width: 10%" align="left">
                                            <asp:Label ID="lblDe_Nuevo" runat="server" Text="De"></asp:Label>
                                        </td>
                                        <td style="width: 25%" align="left" colspan="3">
                                            <asp:TextBox ID="txtDe_Nuevo" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%" align="left">
                                            <asp:Label ID="lblPara_Nuevo" runat="server" Text="Para"></asp:Label>
                                        </td>
                                        <td style="width: 25%" align="left" colspan="3">
                                            <asp:TextBox ID="txtPara_Nuevo" runat="server" Width="193px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%" align="left">
                                            <asp:Label ID="lblPlantilla_Nuevo" runat="server" Text="Plantilla"></asp:Label>
                                        </td>
                                        <td style="width: 25%" align="left" colspan="3">
                                            <asp:TextBox ID="txtPlantilla_Nuevo" runat="server" Width="349px" TextMode="MultiLine"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%" align="left">
                                            <asp:Label ID="lblAsunto_Nuevo" runat="server" Text="Asunto"></asp:Label>
                                        </td>
                                        <td style="width: 25%" align="left" colspan="3">
                                            <asp:TextBox ID="txtAsunto_Nuevo" runat="server" Width="194px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%" align="left">
                                            <asp:Label ID="lblIdCorreoServidor_Nuevo" runat="server" Text="Correo Servidor"></asp:Label>
                                        </td>
                                        <td style="width: 25%" align="left" colspan="3">
                                            <asp:DropDownList ID="cmbNombreServidor_Nuevo" runat="server" __designer:wfdid="w1">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%" align="left">
                                            <asp:Label ID="lblConfirmarEnvio_Nuevo" runat="server" Text="Confirmar Envio"></asp:Label>
                                        </td>
                                        <td style="width: 25%" align="left" colspan="3">
                                            &nbsp;<asp:DropDownList ID="cmbConfirmaEnvio_Nuevo" runat="server" __designer:wfdid="w5">
                                                <asp:ListItem Value="0">NO</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="1">SI</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="4">
                                            &nbsp;<asp:Button ID="btnRegistrar" OnClick="btnRegistrar_Click" runat="server" SkinID="boton_copia"
                                                Text="Aceptar"></asp:Button>
                                            <asp:Button ID="btnCancelarReg" OnClick="btnCancelarReg_Click" runat="server" SkinID="boton_copia"
                                                Text="Cancelar"></asp:Button>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </asp:Panel>
                        <asp:Label ID="lblMensajeError" runat="server" text="dasdasdasd" Font-Bold="true" ForeColor="red"></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnCancelar" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                &nbsp;
            <%--</td>
        </tr>
    </table>--%>
    </div>
    <asp:Label ID="lblmpePlantilla" runat="server"></asp:Label>
        <cc1:ModalPopupExtender ID="mpeVerPlantilla" runat="server"
            TargetControlID="lblmpePlantilla"
            PopupControlID="dvVerPlantilla"
            Enabled="True" 
            BackgroundCssClass="ModalBackgroundCorreoPlantilla">
        </cc1:ModalPopupExtender>
        <div id="dvVerPlantilla" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalCorreoPlantilla">
            <asp:UpdatePanel ID="upnlVerPlantilla" runat="server">
                <ContentTemplate>
                   <telerik:RadEditor id="redtPlantilla" Runat=server Language="es-ES" Skin="Bootstrap">
                    </telerik:RadEditor>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div style="padding-top: 20px; text-align: center; vertical-align: middle; width: 100%;">
                <asp:Button ID="Button2" runat="server" Text="Cerrar" CausesValidation="False" CssClass="btn btn-primary btn-sm" />
            </div>
        </div>

    <asp:Label ID="lblmpeEditarPlantilla" runat="server"></asp:Label>
        <cc1:ModalPopupExtender ID="mpeEditarPlantilla" runat="server"
            TargetControlID="lblmpeEditarPlantilla"
            PopupControlID="dvEditarPlantilla"
            Enabled="True" 
            BackgroundCssClass="ModalBackgroundCorreoPlantilla">
        </cc1:ModalPopupExtender>
        <div id="dvEditarPlantilla" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalCorreoPlantilla">
            <asp:UpdatePanel ID="upnlEditarPlantilla" runat="server">
                <ContentTemplate>
                   <table width="100%">
                        <tbody>
                            <tr>
                                <td class="col-sm-2">
                                    <asp:Label ID="lblDe" runat="server" Text="De" SkinID="etiqueta_negra"></asp:Label>
                                    <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
                                </td>
                                <td class="col-sm-10">
                                    <asp:TextBox ID="txtDe" runat="server" Width="190px"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="col-sm-2">
                                    <asp:Label ID="lblPara" runat="server" Text="Para" SkinID="etiqueta_negra"></asp:Label></td>
                                <td class="col-sm-10">
                                    <asp:TextBox ID="txtPara" runat="server" Width="193px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="col-sm-2">
                                    <asp:Label ID="lblPlantilla" runat="server" Text="Plantilla" SkinID="etiqueta_negra"></asp:Label></td>
                                <td class="col-sm-10">
                                    <telerik:RadEditor id="radPlantillaEdicion" Runat="server" ToolsFile="~/ToolsPlantillaCorreo.xml" Language="es-ES" Skin="Bootstrap">
                                        <ImageManager ViewPaths="~/Editor/images/UserDir/Marketing,~/Editor/images/UserDir/PublicRelations"
                                            UploadPaths="~/Editor/images/UserDir/Marketing,~/Editor/images/UserDir/PublicRelations"
                                            DeletePaths="~/Editor/images/UserDir/Marketing,~/Editor/images/UserDir/PublicRelations"
                                            SearchPatterns="*.jpeg,*.jpg,*.png,*.gif,*.bmp"
                                            EnableAsyncUpload="true" />
                                    </telerik:RadEditor></td>
                            </tr>
                            <tr>
                                <td class="col-sm-2">
                                    <asp:Label ID="lblAsunto" runat="server" Text="Asunto" SkinID="etiqueta_negra"></asp:Label></td>
                                <td class="col-sm-10">
                                    <asp:TextBox ID="txtAsunto" runat="server" Width="194px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td class="col-sm-2">
                                    <asp:Label ID="lblIdCorreoServidor" runat="server" Text="Correo Servidor" SkinID="etiqueta_negra"></asp:Label></td>
                                <td class="col-sm-10">
                                    <asp:DropDownList ID="cmbNombreServidor" runat="server">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td class="col-sm-2">
                                    <asp:Label ID="lblConfirmarEnvio" runat="server" Text="Confirmar Envio" SkinID="etiqueta_negra"></asp:Label></td>
                                <td class="col-sm-10">
                                    <asp:DropDownList ID="cmbConfirmarEnvio" runat="server">
                                        <asp:ListItem Value="0">NO</asp:ListItem>
                                        <asp:ListItem Selected="True" Value="1">SI</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td class="col-sm-2"></td>
                                <td class="col-sm-10">
                                    <div class="text-right">
                                        <asp:Button ID="btnActualizar" OnClick="btnActualizar_Click" runat="server" CssClass="btn btn-primary btn-sm" class="float-right"
                                        Text="Aceptar"></asp:Button>
                                        <asp:Button ID="btnCancelar" OnClick="btnCancelar_Click" runat="server" CssClass="btn btn-secondary btn-sm" class="float-right"
                                        Text="Cancelar"></asp:Button>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
</asp:content>

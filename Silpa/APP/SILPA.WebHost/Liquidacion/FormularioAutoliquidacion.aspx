<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="FormularioAutoliquidacion.aspx.cs" Inherits="Liquidacion_FormularioAutoliquidacion" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>
    <div class='burbujaAyuda'></div>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
    </style>

    <script src="../jquery/jquery.js" type="text/javascript"></script>        
    <link href="../jquery/EstiloGris/jquery-ui.css" rel="stylesheet" />
    <script src="../jquery/EstiloGris/jquery-ui.js"  type="text/javascript"></script>
    <script src="../js/Ayuda.js" type="text/javascript"></script>
    <link href="css/FormularioAutoliquidacion.css" rel="stylesheet" />
    <script src="../js/Autoliquidacion.js"  type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        window.history.forward();
    </script>

    <script language="javascript" type="text/javascript">
    <!--
        function VerificarTerminoCondiciones(oSrouce, args) {
            if ($('#chkAceptarTerminoCondiciones').is(':checked')) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }
        }

        $(function () {            
            var prm = Sys.WebForms.PageRequestManager.getInstance();

            prm.add_endRequest(function () {                 
                if ($('#txtValorProyectoLiquidacion').val() != "") {
                    NumeroALetras("txtValorProyectoLiquidacion", "txtValorProyectoLetrasLiquidacion");
                }
                if ($('#txtValorModificacionLiquidacion').val() != "") {
                    NumeroALetras("txtValorModificacionLiquidacion", "txtValorModificacionLetrasLiquidacion");
                }
            });
        });
    //-->
    </script>

    <asp:ScriptManager ID="scmManager" runat="server"></asp:ScriptManager>        

    <table class="TablaTituloSeccionAutoliquidacion">
        <tr>
            <td class="div-titulo">
                <asp:Label ID="lblTitulo" runat="server" Text="FORMULARIO SOLICITUD DE LIQUIDACIÓN" SkinID="titulo_principal_blanco"></asp:Label>
            </td>
        </tr>
    </table>

    <asp:UpdatePanel runat="server" ID="upnlMensaje" UpdateMode="Conditional">
        <ContentTemplate>
            <table runat="server" visible="false" id="tblMensaje" class="TablaMensajeErrorAutoliquidacion">
                <tr>
                    <td class="MensajeErrorAutoliquidacion">
                        <asp:Literal runat="server" ID="lblMensaje"></asp:Literal>
                        <br />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="upnlFormulario" UpdateMode="Conditional">
            <ContentTemplate>

                <!-- INICIO DESCRIPCION SOLICITUD -->
                <table runat="server" id="tblDescripcionSolicitud" class="TablaFormularioAutoliquidacion">
                    <tr>
                        <td colspan="2" class="TituloSeccionAutoliquidacion">
                            Descripción de la Solicitud
                        </td>
                    </tr>

                    <tr runat="server" id="trTipoSolicitudLiquidacion">
                        <td class="LabelFormularioAutoliquidacion">
                            Liquidación:
                            <span id="spnTipoSolicitudLiquidacion" class="botonAyudaUP" title='Tipo de solicitud de liquidación de evaluación que se desea realizar'></span>
                        </td>
                        <td class="CamposFormularioAutoliquidacion">
                            <asp:DropDownList id="cboTipoSolicitudLiquidacion" runat="server" OnSelectedIndexChanged="cboTipoSolicitudLiquidacion_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="Seleccione." Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvTipoSolicitudLiquidacion" ControlToValidate="cboTipoSolicitudLiquidacion" InitialValue="-1" ValidationGroup="FormularioAutoliquidacion" ErrorMessage="Debe seleccionar el tipo de liquidación que desea presentar.">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr runat="server" id="trSolicitudLiquidacion">
                        <td class="LabelFormularioAutoliquidacion">
                            Solicitud de Liquidación:
                            <span id="spnSolicitudLiquidacion" class="botonAyudaUP" title='Solicitud de liquidación de evaluación que se desea realizar'></span>
                        </td>
                        <td class="CamposFormularioAutoliquidacion">
                            <asp:DropDownList id="cboSolicitudLiquidacion" runat="server" OnSelectedIndexChanged="cboSolicitudLiquidacion_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="Seleccione." Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvSolicitudLiquidacion" ControlToValidate="cboSolicitudLiquidacion" InitialValue="-1" ValidationGroup="FormularioAutoliquidacion" ErrorMessage="Debe seleccionar la solicitud de liquidación que desea presentar.">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>


                    <tr runat="server" id="trTramiteLiquidacion">
                        <td class="LabelFormularioAutoliquidacion">
                            <asp:Literal runat="server" ID="ltlTramiteLiquidacion" Text="Trámite"></asp:Literal>                            
                            <span runat="server" id="spnTramiteLiquidacion" class="botonAyudaUP" title='Trámite que se desea realizar'></span>
                        </td>
                        <td class="CamposFormularioAutoliquidacion">
                            <asp:DropDownList id="cboTramiteLiquidacion" runat="server" OnSelectedIndexChanged="cboTramiteLiquidacion_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="Seleccione." Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvTramiteLiquidacion" ControlToValidate="cboTramiteLiquidacion" InitialValue="-1" ValidationGroup="FormularioAutoliquidacion" ErrorMessage="Debe seleccionar el trámite que desea realizar.">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr runat="server" id="trSectorLiquidacion">
                        <td class="LabelFormularioAutoliquidacion">
                            Sector:
                            <span id="spnSectorLiquidacion" class="botonAyudaUP" title='Sector al cual va dirigida la solicitud de evaluación de liquidación'></span>
                        </td>
                        <td class="CamposFormularioAutoliquidacion">
                            <asp:DropDownList id="cboSectorLiquidacion" runat="server" OnSelectedIndexChanged="cboSectorLiquidacion_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="Seleccione." Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvSectorLiquidacion" ControlToValidate="cboSectorLiquidacion" InitialValue="-1" ValidationGroup="FormularioAutoliquidacion" ErrorMessage="Debe seleccionar el sector al cual va dirigida la solicitud.">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>

                <!-- FIN DESCRIPCION SOLICITUD -->


                <!-- INICIO DESCRIPCION PROYECTO -->
                <div runat="server" id="tblDescripcionProyecto">
                    <table class="TablaFormularioAutoliquidacion">
                        <tr>
                            <td colspan="2" class="TituloSeccionAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlTituloDescripcionProyecto" Text="Descripción del Proyecto"></asp:Literal>
                            </td>
                        </tr>

                        <tr runat="server" id="trProyectoLiquidacion">
                            <td class="LabelFormularioAutoliquidacion">
                                Proyecto:
                                <span id="spnProyectoLiquidacion" class="botonAyudaUP" title='Proyecto que va a efectuar y sobre el cual se desea se realice la liquidación'></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:DropDownList id="cboProyectoLiquidacion" runat="server" OnSelectedIndexChanged="cboProyectoLiquidacion_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="Seleccione." Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="rfvProyectoLiquidacion" ControlToValidate="cboProyectoLiquidacion" InitialValue="-1" ValidationGroup="FormularioAutoliquidacion" ErrorMessage="Debe seleccionar el proyecto a realizar.">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr runat="server" id="trActividadLiquidacion">
                            <td class="LabelFormularioAutoliquidacion">
                                Tipo de Proyecto, Obra o Actividad:
                                <span id="spnActividadLiquidacion" class="botonAyudaUP" title='Tipo de Proyecto, Obra o Actividad a realizar (Identifique el numeral y/o literal del Art. 8 y 9, Decreto 2041/2014)'></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:DropDownList id="cboActividadLiquidacion" runat="server" OnSelectedIndexChanged="cboActividadLiquidacion_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="Seleccione." Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="rfvActividadLiquidacion" ControlToValidate="cboActividadLiquidacion" InitialValue="-1" ValidationGroup="FormularioAutoliquidacion" ErrorMessage="Debe seleccionar el tipo de proyecto, obra o actividad.">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr runat="server" id="trNombreProyectoLiquidacion">
                            <td class="LabelFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlNombreProyectoLiquidacion" Text="Nombre del Proyecto, Obra o Actividad:"></asp:Literal>
                                <span runat="server" id="spnNombreProyectoLiquidacion" class="botonAyudaUP" title='Nombre del proyecto, obra o actividad que se va a realizar y por el cual se presenta la solicitud de liquidación'></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:TextBox runat="server" ID="txtNombreProyectoLiquidacion" TextMode="MultiLine" Rows="3" Width="100%"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvNombreProyectoLiquidacion" ControlToValidate="txtNombreProyectoLiquidacion" ValidationGroup="FormularioAutoliquidacion" ErrorMessage="Debe ingresar el nombre del proyecto, obra o actividad a realizar.">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr runat="server" id="trDescripcionProyectoLiquidacion">
                            <td class="LabelFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlDescripcionProyectoLiquidacion" Text="Descripción breve del Instrumento de Manejo y Control:"></asp:Literal>
                                <span runat="server" id="spnDescripcionProyectoLiquidacion" class="botonAyudaUP" title='Descripción breve del instrumento de manejo y control ambiental objeto del servicio de liquidación'></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:TextBox runat="server" ID="txtDescripcionProyectoLiquidacion" TextMode="MultiLine" Rows="3" Width="100%"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvDescripcionProyectoLiquidacion" ControlToValidate="txtDescripcionProyectoLiquidacion" ValidationGroup="FormularioAutoliquidacion" ErrorMessage="Debe ingresar una breve descripción del proyecto, obra o actividad a realizar.">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>


                        <tr runat="server" id="trValorProyectoLiquidacion">
                            <td class="LabelFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlValorProyectoLiquidacion" Text="Valor de Proyecto en Pesos Colombianos:"></asp:Literal>
                                <span runat="server" id="spnValorProyectoLiquidacion" class="botonAyudaUP" title='Valor del proyecto, obra o actividad en pesos colombianos'></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:TextBox runat="server" ID="txtValorProyectoLiquidacion" MaxLength="14" ClientIDMode="Static" OnKeyPress="return ValidarValorProyectoAutoliquidacion(event);" OnKeyUp='NumeroALetras("txtValorProyectoLiquidacion","txtValorProyectoLetrasLiquidacion");'></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvValorProyectoLiquidacion" ControlToValidate="txtValorProyectoLiquidacion" ValidationGroup="FormularioAutoliquidacion" ErrorMessage="Debe ingresar el valor del proyecto.">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rexValorProyectoLiquidacion" runat="server" Display="Dynamic" ValidationGroup="FormularioAutoliquidacion" ErrorMessage="Ingrese un valor del proyecto válido." ControlToValidate="txtValorProyectoLiquidacion" ValidationExpression="^\d+$">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>

                        <tr runat="server" id="trValorProyectoLetrasLiquidacion">
                            <td class="LabelFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlValorProyectoLetrasLiquidacion" Text="Valor en Letras:"></asp:Literal>
                                <span runat="server" id="spnValorProyectoLetrasLiquidacion" class="botonAyudaUP" title='Valor del proyecto, obra o actividad en letras'></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:TextBox runat="server" ID="txtValorProyectoLetrasLiquidacion" MaxLength="1000" Width="100%" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>                                
                            </td>
                        </tr>

                        <tr runat="server" id="trValorModificacionLiquidacion">
                            <td class="LabelFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlValorModificacionLiquidacion" Text="Valor de Modificación en Pesos Colombianos:"></asp:Literal>
                                <span runat="server" id="spnValorModificacionLiquidacion" class="botonAyudaUP" title='Valor de la modificación del proyecto, obra o actividad en pesos colombianos'></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:TextBox runat="server" ID="txtValorModificacionLiquidacion" MaxLength="14" ClientIDMode="Static" OnKeyPress="return ValidarValorProyectoAutoliquidacion(event);" OnKeyUp='NumeroALetras("txtValorModificacionLiquidacion","txtValorModificacionLetrasLiquidacion");'></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvValorModificacionLiquidacion" ControlToValidate="txtValorModificacionLiquidacion" ValidationGroup="FormularioAutoliquidacion" ErrorMessage="Debe ingresar el valor de la modificación del proyecto.">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rexValorModificacionLiquidacion" runat="server" Display="Dynamic" ValidationGroup="FormularioAutoliquidacion" ErrorMessage="Ingrese un valor de modificación válido." ControlToValidate="txtValorModificacionLiquidacion" ValidationExpression="^\d+$">*</asp:RegularExpressionValidator>
                            </td>
                        </tr>

                        <tr runat="server" id="trValorModificacionLetrasLiquidacion">
                            <td class="LabelFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlValorModificacionLetrasLiquidacion" Text="Valor de Modificación en Letras:"></asp:Literal>
                                <span runat="server" id="spnValorModificacionLetrasLiquidacion" class="botonAyudaUP" title='Valor de la modificación del proyecto, obra o actividad en letras'></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:TextBox runat="server" ID="txtValorModificacionLetrasLiquidacion" MaxLength="1000" Width="100%" ClientIDMode="Static" ReadOnly="true"></asp:TextBox>                                
                            </td>
                        </tr>

                        <tr runat="server" id="trProyectoPINELiquidacion">
                            <td class="LabelFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlProyectoPINELiquidacion" Text="Proyecto PINE:"></asp:Literal>
                                <span runat="server" id="spnProyectoPINELiquidacion" class="botonAyudaUP" title='Indica si es un proyecto PINE validado por la comisión intersectorial de infraestructura y proyectos estratégicos - CIIPE'></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:DropDownList id="cboProyectoPINELiquidacion" runat="server">
                                    <asp:ListItem Text="Seleccione." Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="rfvProyectoPINELiquidacion" ControlToValidate="cboProyectoPINELiquidacion" InitialValue="-1" ValidationGroup="FormularioAutoliquidacion" ErrorMessage="Debe indicar si es un proyecto PINE.">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>

                    </table>
                </div>

                <!-- FIN DESCRIPCION PROYECTO -->


                <!-- INICIO PERMISOS REQUERIDOS -->

                <div runat="server" id="tblPermisosRequeridos">
                    <table class="TablaFormularioAutoliquidacion">
                        <tr>
                            <td colspan="2" class="TituloSeccionAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlTituloPermisosRequeridos" Text="Relación de Permisos, Autorizaciones y/o Concesiones Ambientales Requeridos"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="LabelFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlPermisosLiquidacion" Text="Permisos:"></asp:Literal>
                                <span runat="server" id="spnPermisosLiquidacion" class="botonAyudaUP" title='Listado de permisos, autorizaciones y/o concesiones ambientales requeridos para la realización del proyecto, obra o actividad.<br/>Para ingresar la información sobre los permisos requeridos hacer clic sobre el botón "Agregar"'></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <table class="TablaGrillaCampoAutoliquidacion">
                                    <tr>
                                        <td class="BotonesCamposAutoliquidacion">
                                            <asp:Button runat="server" ID="cmdAgregarPermisoLiquidacion" CausesValidation="false" Text="Agregar" ClientIDMode="Static" OnClick="cmdAgregarPermisoLiquidacion_Click"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grdPermisosLiquidacion" runat="server" Width="100%" AutoGenerateColumns="False" ShowFooter="False" SkinID="GrillaSolicitudAutoliquidacion"
                                                          ShowHeaderWhenEmpty="true" EmptyDataText="No se han adicionado permisos a la solicitud de liquidación">
                                                <Columns>
                                                    <asp:TemplateField HeaderText = "Permiso">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlPermiso" runat="server" Text='<%# Eval("Permiso.Permiso") %>'></asp:Literal>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText = "Autoridad Ambiental">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlAutoridad" runat="server" Text='<%# Eval("Autoridad") %>'></asp:Literal>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Eliminar" ItemStyle-CssClass="TextoFilaCentro">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEliminarPermiso" runat="server" Text="Eliminar" CommandArgument='<%# Container.DisplayIndex %>' OnClick="lnkEliminarPermiso_Click" />
                                                        </ItemTemplate>                                                        
                                                    </asp:TemplateField>
                                                </Columns>                                                
                                            </asp:GridView>  
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>

                <!-- FIN PERMISOS REQUERIDOS -->


                <!-- INICIO LOCALIZACION -->

                <div runat="server" id="tblLocalizacionProyecto">
                    <table class="TablaFormularioAutoliquidacion">
                        <tr>
                            <td colspan="2" class="TituloSeccionAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlTituloLocalizacionProyecto" Text="Localización del Proyecto, Obra, Actividad, Permiso o Domicilio Principal"></asp:Literal>      
                            </td>
                        </tr>
                        <tr>
                            <td class="LabelFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlRegionLiquidación" Text="Región:"></asp:Literal>
                                <span runat="server" id="spnRegionLiquidacion" class="botonAyudaUP" title='Región donde se va a desarrollar el proyecto, obra o actividad.'></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:CheckBoxList runat="server" ID="chklRegionAutoliquidacion" RepeatDirection="Horizontal"></asp:CheckBoxList>                                
                            </td>
                        </tr>
                        <tr>
                            <td class="LabelFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlUbicaciónLiquidacion" Text="Ubicación:"></asp:Literal>
                                <span runat="server" id="spnLocalizacionLiquidacion" class="botonAyudaUP" title='Ubicación del proyecto, obra o actividad.<br/>Para ingresar la información sobre la ubicación hacer clic sobre el botón "Agregar"'></span>
                                <asp:CustomValidator runat="server" ID="cvLocalizacionLiquidacion" OnServerValidate="cvLocalizacionLiquidacion_ServerValidate" ErrorMessage="Debe ingresar al listado por lo menos una ubicación." ValidationGroup="FormularioAutoliquidacion">*</asp:CustomValidator>                                
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <table class="TablaGrillaCampoAutoliquidacion">
                                    <tr>
                                        <td class="BotonesCamposAutoliquidacion">
                                            <asp:Button runat="server" ID="cmdAgregarUbicacionLiquidacion" CausesValidation="false" Text="Agregar" ClientIDMode="Static" OnClick="cmdAgregarUbicacionLiquidacion_Click"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grdUbicacionLiquidacion" runat="server" Width="100%" AutoGenerateColumns="False" ShowFooter="False" SkinID="GrillaSolicitudAutoliquidacion"
                                                          ShowHeaderWhenEmpty="true" EmptyDataText="No se han adicionado ubicaciones a la solicitud de liquidación">
                                                <Columns>
                                                    <asp:TemplateField HeaderText = "Departamento">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlDepartamento" runat="server" Text='<%# Eval("Departamento.Nombre") %>'></asp:Literal>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText = "Municipio">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlMunicipio" runat="server" Text='<%# Eval("Municipio.NombreMunicipio") %>'></asp:Literal>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText = "Corregimiento">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlCorregimiento" runat="server" Text='<%# (!string.IsNullOrWhiteSpace(Eval("Corregimiento").ToString()) ? Eval("Corregimiento") : "-") %>'></asp:Literal>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText = "Vereda">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlVereda" runat="server" Text='<%# (!string.IsNullOrWhiteSpace(Eval("Vereda").ToString()) ? Eval("Vereda") : "-") %>'></asp:Literal>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Eliminar" ItemStyle-CssClass="TextoFilaCentro">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEliminarUbicacion" runat="server" Text="Eliminar" CommandArgument='<%# Container.DisplayIndex %>' OnClick="lnkEliminarUbicacion_Click" />
                                                        </ItemTemplate>                                                        
                                                    </asp:TemplateField>
                                                </Columns>                                                
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>                        
                        <tr runat="server" id="trAguasMaritimasLiquidacion">
                            <td class="LabelFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlAguasMaritimasLiquidacion" Text="¿Proyecto en Aguas Marítimas?:"></asp:Literal>
                                <span runat="server" id="spnAguasMaritimasLiquidacion" class="botonAyudaUP" title='Indica si el proyecto, obra o actividad se desarrolla en aguas marítimas'></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:DropDownList id="cboAguasMaritimasLiquidacion" runat="server" OnSelectedIndexChanged="cboAguasMaritimasLiquidacion_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="Seleccione." Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="Si" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="rfvAguasMaritimasLiquidacion" ControlToValidate="cboAguasMaritimasLiquidacion" InitialValue="-1" ValidationGroup="FormularioAutoliquidacion" ErrorMessage="Debe indicar si el proyecto, obra o actividad se desarrolla en aguas marítimas.">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr runat="server" id="trCualAguaMaritimaLiquidacion">
                            <td class="LabelFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlCualAguaMaritimaLiquidacion" Text="¿Cual?:"></asp:Literal>
                                <span runat="server" id="spnCualAguaMaritimaLiquidacion" class="botonAyudaUP" title='Aguas marítimas sobre las cuales se desarrolla el proyecto, obra o actividad.'></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:DropDownList id="cboCualAguaMaritimaLiquidacion" runat="server">
                                    <asp:ListItem Text="Seleccione." Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="rfvCualAguaMaritimaLiquidacion" ControlToValidate="cboCualAguaMaritimaLiquidacion" InitialValue="-1" ValidationGroup="FormularioAutoliquidacion" ErrorMessage="Debe indicar las aguas marítimas sobre las cuales se desarrolla el proyecto, obra o actividad.">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>                        
                    </table>
                </div>
                <!-- FIN LOCALIZACION -->

                <!-- INICIO INFORMACION AUTORIDAD AMBIENTAL -->

                <div  runat="server" id="tblInformacionCompetenciaAutoridad">
                    <table class="TablaFormularioAutoliquidacion">
                        <tr>
                            <td colspan="2" class="TituloSeccionAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlTitInformacionCompetenciaAutoridad" Text="Información Competencia Autoridad Ambiental"></asp:Literal> 
                            </td>
                        </tr>                                 
                        <tr runat="server" id="trAutoridadAmbientalLiquidacion">
                            <td class="LabelFormularioAutoliquidacion">
                                Autoridad Ambiental:
                                <span id="spnAutoridadAmbientalLiquidacion" class="botonAyudaUP" title='Indique la autoridad ambiental que tiene la competencia para atender la solicitud.'></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:DropDownList id="cboAutoridadAmbientalLiquidacion" runat="server" OnSelectedIndexChanged="cboAutoridadAmbientalLiquidacion_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="Seleccione." Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="rfvAutoridadAmbientalLiquidacion" ControlToValidate="cboAutoridadAmbientalLiquidacion" InitialValue="-1" ValidationGroup="FormularioAutoliquidacion" ErrorMessage="Debe seleccionar la autoridad ambiental a la cual va dirigida la solicitud.">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>                        
                    </table>
                </div>

                <!-- FIN INFORMACION AUTORIDAD AMBIENTAL -->


                <!-- INICIO RUTA LOGISTICA -->

                <div runat="server" id="tblRutaLogistica">
                    <table class="TablaFormularioAutoliquidacion">
                        <tr>
                            <td colspan="2" class="TituloSeccionAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlTituloRutaLogistica" Text="Ruta Logística para Acceder a los Puntos Solicitados para el Uso y/o Aprovechamiento de los Recursos Naturales"></asp:Literal> 
                            </td>
                        </tr>
                        <tr>
                            <td class="LabelFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlRutaLogisticaLiquidacion" Text="Ruta Logística:"></asp:Literal>
                                <span runat="server" id="spnRutaLogisticaLiquidacion" class="botonAyudaUP" title='Ruta Logística para acceder a los puntos solicitados para el uso y/o aprovechamiento de los recursos naturales.<br/>Para ingresar la información sobre la ruta logística hacer clic sobre el botón "Agregar"'></span>
                                <asp:CustomValidator runat="server" ID="cvRutaLogisticaLiquidacion" OnServerValidate="cvRutaLogisticaLiquidacion_ServerValidate" ErrorMessage="Debe ingresar al listado por lo menos una ruta." ValidationGroup="FormularioAutoliquidacion">*</asp:CustomValidator>                                
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <table class="TablaGrillaCampoAutoliquidacion">
                                    <tr>
                                        <td class="BotonesCamposAutoliquidacion">
                                            <asp:Button runat="server" ID="cmdAgregarRutaLiquidacion" CausesValidation="false" Text="Agregar" ClientIDMode="Static" OnClick="cmdAgregarRutaLiquidacion_Click"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grdRutaLiquidacion" runat="server" Width="100%" AutoGenerateColumns="False" ShowFooter="False" SkinID="GrillaSolicitudAutoliquidacion"
                                                          ShowHeaderWhenEmpty="true" EmptyDataText="No se ha adicionado una ruta a la solicitud de liquidación">
                                                <Columns>
                                                    <asp:TemplateField HeaderText = "Medio de Transporte">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlMedioTransporte" runat="server" Text='<%# Eval("MedioTransporte.MedioTransporte") %>'></asp:Literal>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText = "Departamento Partida">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlDepartamentoPartida" runat="server" Text='<%# Eval("DepartamentoOrigen.Nombre") %>'></asp:Literal>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText = "Municipio Partida">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlMunicipioPartida" runat="server" Text='<%# Eval("MunicipioOrigen.NombreMunicipio") %>'></asp:Literal>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText = "Departamento de Llegada">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlDepartamentoLLegada" runat="server" Text='<%# Eval("DepartamentoDestino.Nombre") %>'></asp:Literal>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText = "Municipio de Llegada">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlMunicipioLLegada" runat="server" Text='<%# Eval("MunicipioDestino.NombreMunicipio") %>'></asp:Literal>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText = "Tiempo Aproximado Trayecto">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlTiempoAproximado" runat="server" Text='<%# Eval("TiempoAproximadoTrayecto") %>'></asp:Literal>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                                    
                                                    <asp:TemplateField HeaderText="Eliminar" ItemStyle-CssClass="TextoFilaCentro">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEliminarRuta" runat="server" Text="Eliminar" CommandArgument='<%# Container.DisplayIndex %>' OnClick="lnkEliminarRuta_Click" />
                                                        </ItemTemplate>                                                        
                                                    </asp:TemplateField>
                                                </Columns>                                                
                                            </asp:GridView>

                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>

                <!-- FIN RUTA LOGISTICA -->


                <table class="TablaBotonesFormularioAutoliquidacion">
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="cmdEnviar" ValidationGroup="FormularioAutoliquidacion" Text="Enviar" ClientIDMode="Static" OnClick="cmdEnviar_Click"/>
                            <%--<asp:Button runat="server" ID="cmdEnviar"  CausesValidation="false" Text="Enviar" ClientIDMode="Static" OnClick="cmdEnviar_Click"/>--%>
                            <asp:Button runat="server" ID="cmdCancelar" CausesValidation="false" Text="Cancelar" ClientIDMode="Static" OnClick="cmdCancelar_Click"/>
                            <asp:ValidationSummary ID="valFormularioAutoliquidacion" runat="server" ValidationGroup="FormularioAutoliquidacion" ShowMessageBox="true" ShowSummary="false" />
                        </td>
                    </tr>
                </table>

            </ContentTemplate>

            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cboTipoSolicitudLiquidacion" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="cboSolicitudLiquidacion" EventName="SelectedIndexChanged" />  
                <asp:AsyncPostBackTrigger ControlID="cboTramiteLiquidacion" EventName="SelectedIndexChanged" />                  
                <asp:AsyncPostBackTrigger ControlID="cboSectorLiquidacion" EventName="SelectedIndexChanged" />                
                <asp:AsyncPostBackTrigger ControlID="cboAutoridadAmbientalLiquidacion" EventName="SelectedIndexChanged" />                
                <asp:AsyncPostBackTrigger ControlID="cboProyectoLiquidacion" EventName="SelectedIndexChanged" />    
                <asp:AsyncPostBackTrigger ControlID="cboActividadLiquidacion" EventName="SelectedIndexChanged" />                    
                <asp:AsyncPostBackTrigger ControlID="cboAguasMaritimasLiquidacion" EventName="SelectedIndexChanged" />                                                             
                <asp:AsyncPostBackTrigger ControlID="cmdEnviar" EventName="Click" />                                                
                <asp:AsyncPostBackTrigger ControlID="cmdAgregarPermisoLiquidacion" EventName="Click" />                                                                        
            </Triggers>
        </asp:UpdatePanel>

        <asp:UpdateProgress ID="uppFormulario" runat="server" AssociatedUpdatePanelID="upnlFormulario">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imgFormulario" runat="server" SkinId="procesando"/></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>
        


        <input type="button" runat="server" id="cmdAgregarPermisoHide" style="display:none;" />
        <cc1:ModalPopupExtender ID="mpeAgregarPermiso" runat="server" PopupControlID="dvAgregarPermiso" TargetControlID="cmdAgregarPermisoHide" BehaviorID="mpeAgregarPermisos" BackgroundCssClass="ModalBackgroundAutoliquidacion">
        </cc1:ModalPopupExtender>
        <div id="dvAgregarPermiso" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalAutoliquidacion">
            <asp:UpdatePanel runat="server" ID="upnlAgregarPermiso" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="TablaFormularioAutoliquidacion">
                        <tr>
                            <td colspan="2" class="TituloSeccionAutoliquidacion">
                                Agregar Permiso, Autorización y/o Concesión
                            </td>
                        </tr>

                        <tr>
                            <td class="LabelFormularioAutoliquidacion">
                                Permiso, Autorización y/o Concesión:
                                <span id="spnPermisoAgregarPermiso" class="botonAyudaUP" title='Permiso, autorización y/o concesión que desea relacionar a la solicitud' divModal="dvAgregarPermiso"></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:DropDownList id="cboPermisoAgregarPermiso" runat="server">
                                    <asp:ListItem Text="Seleccione." Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="rfvPermisoAgregarPermiso" ControlToValidate="cboPermisoAgregarPermiso" InitialValue="-1" ValidationGroup="FormularioAgregarPermiso" ErrorMessage="Debe seleccionar el permiso, autorización y/o concesión.">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td class="LabelFormularioAutoliquidacion">
                                Autoridad Ambiental:
                                <span id="spnAutoridadAgregarPermiso" class="botonAyudaUP" title='Autoridad ambiental competente que expide el permiso' divModal="dvAgregarPermiso"></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:DropDownList id="cboAutoridadAgregarPermiso" runat="server">
                                    <asp:ListItem Text="Seleccione." Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="rfvAutoridadAgregarPermiso" ControlToValidate="cboAutoridadAgregarPermiso" InitialValue="-1" ValidationGroup="FormularioAgregarPermiso" ErrorMessage="Debe la autoridad ambiental competente que expede el permiso.">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <table class="TablaBotonesFormularioAutoliquidacion">
                        <tr>
                            <td>
                                <asp:Button runat="server" ID="cmdAgregarPermiso" ValidationGroup="FormularioAgregarPermiso" Text="Agregar" ClientIDMode="Static" OnClick="cmdAgregarPermiso_Click"/>
                                <asp:Button runat="server" ID="cmdCancelarPermiso" CausesValidation="false" Text="Cancelar" ClientIDMode="Static" OnClick="cmdCancelarPermiso_Click"/>
                                <asp:ValidationSummary ID="valFormularioAgregarPermiso" runat="server" ValidationGroup="FormularioAgregarPermiso" ShowMessageBox="true" ShowSummary="false" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmdAgregarPermiso" EventName="Click" />                                                        
                    <asp:AsyncPostBackTrigger ControlID="cmdCancelarPermiso" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>

        <asp:UpdateProgress ID="uppAgregarPermiso" runat="server" AssociatedUpdatePanelID="upnlAgregarPermiso">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imgAgregarPermiso" runat="server" SkinId="procesando"/></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>


        <input type="button" runat="server" id="cmdAgregarUbicacionHide" style="display:none;" />
        <cc1:ModalPopupExtender ID="mpeAgregarUbicacion" runat="server" PopupControlID="dvAgregarUbicacion" TargetControlID="cmdAgregarUbicacionHide" BehaviorID="mpeAgregarUbicacions" BackgroundCssClass="ModalBackgroundAutoliquidacion">
        </cc1:ModalPopupExtender>
        <div id="dvAgregarUbicacion" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalAutoliquidacion">
            <asp:UpdatePanel runat="server" ID="upnlAgregarUbicacion" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="TablaFormularioAutoliquidacion">
                        <tr>
                            <td colspan="2" class="TituloSeccionAutoliquidacion">
                                Agregar Ubicación
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2" class="SubTituloSeccionAutoliquidacion">
                                Ubicación
                            </td>
                        </tr>

                        <tr>
                            <td class="LabelFormularioAutoliquidacion">
                                Departamento:
                                <span id="spnDepartamentoAgregarUbicacion" class="botonAyudaUP" title='Departamento de ubicación del proyecto, obra o actividad' divModal="dvAgregarUbicacion"></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:DropDownList runat="server" ID="cboDepartamentoAgregarUbicacion" OnSelectedIndexChanged="cboDepartamentoAgregarUbicacion_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>                                
                                <asp:RequiredFieldValidator runat="server" ID="rfvDepartamentoAgregarUbicacion" ControlToValidate="cboDepartamentoAgregarUbicacion" InitialValue="S" ValidationGroup="FormularioAgregarUbicacion" ErrorMessage="Debe seleccionar el departamento donde se ubica el proyecto.">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td class="LabelFormularioAutoliquidacion">
                                Municipio:
                                <span id="spnMunicipioAgregarUbicacion" class="botonAyudaUP" title='Municipio donde se ubica el proyecto, obra o actividad' divModal="dvAgregarUbicacion"></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:DropDownList runat="server" ID="cboMunicipioAgregarUbicacion"></asp:DropDownList>                                
                                <asp:RequiredFieldValidator runat="server" ID="rfvMunicipioAgregarUbicacion" ControlToValidate="cboMunicipioAgregarUbicacion" InitialValue="S" ValidationGroup="FormularioAgregarUbicacion" ErrorMessage="Debe seleccionar el municipio donde se ubica el proyecto.">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td class="LabelFormularioAutoliquidacion">
                                Corregimiento:
                                <span id="spnCorregimientoAgregarUbicacion" class="botonAyudaUP" title='Corregimiento donde se ubica el proyecto, obra o actividad' divModal="dvAgregarUbicacion"></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:TextBox runat="server" ID="txtCorregimientoAgregarUbicacion" MaxLength="250" Width="100%"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td class="LabelFormularioAutoliquidacion">
                                Vereda:
                                <span id="spnVeredaAgregarUbicacion" class="botonAyudaUP" title='Vereda donde se ubica el proyecto, obra o actividad' divModal="dvAgregarUbicacion"></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:TextBox runat="server" ID="txtVeredaAgregarUbicacion" MaxLength="250" Width="100%"></asp:TextBox>
                            </td>
                        </tr>                        

                        <tr>
                            <td colspan="2" class="SubTituloSeccionAutoliquidacion">
                                Coordenadas de Ubicación
                                <span id="spnCoordenadasAgregarUbicacion" class="botonAyudaUP" title='Coordenadas de ubicación del proyecto, obra o actividad.<br />Para ingresar la información de coordenadas hacer clic sobre el botón "Agregar"' divModal="dvAgregarUbicacion"></span>
                            </td>
                        </tr>
                        <tr>
                            <td class="CamposFormularioAutoliquidacion"  colspan="2">
                                <table class="TablaGrillaCampoAutoliquidacion">
                                    <tr>
                                        <td class="BotonesCamposAutoliquidacion">
                                            <asp:Button runat="server" ID="cmdAgregarCoordenadaAgregarUbicacion" CausesValidation="false" Text="Agregar" ClientIDMode="Static" OnClick="cmdAgregarCoordenadaAgregarUbicacion_Click"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grdCoordenadasAgregarUbicacion" runat="server" Width="100%" AutoGenerateColumns="False" ShowFooter="False" SkinID="GrillaSolicitudAutoliquidacion"
                                                          ShowHeaderWhenEmpty="true" EmptyDataText="No se ha adicionado coordenadas de ubicación">
                                                <Columns>
                                                    <asp:TemplateField HeaderText = "Localización">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlLocalizacion" runat="server" Text='<%# Eval("Localizacion") %>'></asp:Literal>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText = "Tipo Geometría">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlTipoGeometria" runat="server" Text='<%# (Eval("TipoGeometria") != null ? Eval("TipoGeometria.TipoGeometria") : "-") %>'></asp:Literal>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText = "Tipo Coordenada">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlTipoCoordenada" runat="server" Text='<%# (Eval("TipoCoordenada") != null ? Eval("TipoCoordenada.TipoCoordenada") : "-") %>'></asp:Literal>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText = "Origen - Magna Sirgas">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlOrigenMagna" runat="server" Text='<%# ( Eval("OrigenMagna") != null ? Eval("OrigenMagna.OrigenMagna") : "-") %>'></asp:Literal>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText = "Norte">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlNorte" runat="server" Text='<%# Eval("Norte") %>'></asp:Literal>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText = "Este">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="ltlEste" runat="server" Text='<%# Eval("Este") %>'></asp:Literal>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Eliminar" ItemStyle-CssClass="TextoFilaCentro">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEliminarCoordenadaAgregarUbicacion" runat="server" Text="Eliminar" CommandArgument='<%# Container.DisplayIndex %>' OnClick="lnkEliminarCoordenadaAgregarUbicacion_Click" />
                                                        </ItemTemplate>                                                        
                                                    </asp:TemplateField>
                                                </Columns>                                                
                                            </asp:GridView>

                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table class="TablaBotonesFormularioAutoliquidacion">
                        <tr>
                            <td>
                                <asp:Button runat="server" ID="cdmAgregarUbicacion" ValidationGroup="FormularioAgregarUbicacion" Text="Agregar" ClientIDMode="Static" OnClick="cdmAgregarUbicacion_Click"/>
                                <asp:Button runat="server" ID="cmdCancelarAgregarUbicacion" CausesValidation="false" Text="Cancelar" ClientIDMode="Static" OnClick="cmdCancelarAgregarUbicacion_Click"/>
                                <asp:ValidationSummary ID="valFormularioAgregarUbicacion" runat="server" ValidationGroup="FormularioAgregarUbicacion" ShowMessageBox="true" ShowSummary="false" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cboDepartamentoAgregarUbicacion" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cboMunicipioAgregarUbicacion" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cmdAgregarCoordenadaAgregarUbicacion" EventName="Click" />                                                        
                    <asp:AsyncPostBackTrigger ControlID="cdmAgregarUbicacion" EventName="Click" />                                                        
                    <asp:AsyncPostBackTrigger ControlID="cmdCancelarAgregarUbicacion" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>

        <asp:UpdateProgress ID="uppAgregarUbicacion" runat="server" AssociatedUpdatePanelID="upnlAgregarUbicacion">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imgAgregarUbicacion" runat="server" SkinId="procesando"/></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>


        <input type="button" runat="server" id="cmdAgregarRutaHide" style="display:none;" />
        <cc1:ModalPopupExtender ID="mpeAgregarRuta" runat="server" PopupControlID="dvAgregarRuta" TargetControlID="cmdAgregarRutaHide" BehaviorID="mpeAgregarRutas" BackgroundCssClass="ModalBackgroundAutoliquidacion">
        </cc1:ModalPopupExtender>
        <div id="dvAgregarRuta" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalAutoliquidacion">
            <asp:UpdatePanel runat="server" ID="upnlAgregarRuta" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="TablaFormularioAutoliquidacion">
                        <tr>
                            <td colspan="2" class="TituloSeccionAutoliquidacion">
                                Agregar Ruta
                            </td>
                        </tr>

                        <tr>
                            <td class="LabelFormularioAutoliquidacion">
                                Medio de Transporte:
                                <span id="spnMedioTransporteAgregarRuta" class="botonAyudaUP" title='Medio de transporte a utilizar' divModal="dvAgregarRuta"></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:DropDownList runat="server" ID="cboMedioTransporteAgregarRuta" OnSelectedIndexChanged="cboMedioTransporteAgregarRuta_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="rfvMedioTransporteAgregarRuta" ControlToValidate="cboMedioTransporteAgregarRuta" InitialValue="-1" ValidationGroup="FormularioAgregarRuta" ErrorMessage="Debe seleccionar el medio de transporte a utilizar.">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td class="LabelFormularioAutoliquidacion">
                                Departamento de Partida:
                                <span id="spnDepartamentoPartidaAgregarRuta" class="botonAyudaUP" title='Departamento de partida del trayecto a realizar' divModal="dvAgregarRuta"></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:DropDownList runat="server" ID="cboDepartamentoPartidaAgregarRuta" OnSelectedIndexChanged="cboDepartamentoPartidaAgregarRuta_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>                                
                                <asp:RequiredFieldValidator runat="server" ID="rfvDepartamentoPartidaAgregarRuta" ControlToValidate="cboDepartamentoPartidaAgregarRuta" InitialValue="S" ValidationGroup="FormularioAgregarRuta" ErrorMessage="Debe seleccionar el departamento de partida del trayecto a realizar.">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td class="LabelFormularioAutoliquidacion">
                                Municipio de Partida:
                                <span id="spnMunicipioPartidaAgregarRuta" class="botonAyudaUP" title='Departamento de partida del trayecto a realizar' divModal="dvAgregarRuta"></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:DropDownList runat="server" ID="cboMunicipioPartidaAgregarRuta" OnSelectedIndexChanged="cboMunicipioPartidaAgregarRuta_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>                                
                                <asp:RequiredFieldValidator runat="server" ID="rfvMunicipioPartidaAgregarRuta" ControlToValidate="cboMunicipioPartidaAgregarRuta" InitialValue="S" ValidationGroup="FormularioAgregarRuta" ErrorMessage="Debe seleccionar el municipio de partida del trayecto a realizar.">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td class="LabelFormularioAutoliquidacion">
                                Departamento de Llegada:
                                <span id="spnDepartamentoLlegadaAgregarRuta" class="botonAyudaUP" title='Departamento de llegada del trayecto a realizar' divModal="dvAgregarRuta"></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:DropDownList runat="server" ID="cboDepartamentoLlegadaAgregarRuta" OnSelectedIndexChanged="cboDepartamentoLlegadaAgregarRuta_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>                                
                                <asp:RequiredFieldValidator runat="server" ID="rfvDepartamentoLlegadaAgregarRuta" ControlToValidate="cboDepartamentoLlegadaAgregarRuta" InitialValue="S" ValidationGroup="FormularioAgregarRuta" ErrorMessage="Debe seleccionar el departamento de llegada del trayecto a realizar.">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td class="LabelFormularioAutoliquidacion">
                                Municipio de Llegada:
                                <span id="spnMunicipioLlegadaAgregarRuta" class="botonAyudaUP" title='Departamento de llegada del trayecto a realizar' divModal="dvAgregarRuta"></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:DropDownList runat="server" ID="cboMunicipioLlegadaAgregarRuta"></asp:DropDownList>                                
                                <asp:RequiredFieldValidator runat="server" ID="rfvMunicipioLlegadaAgregarRuta" ControlToValidate="cboMunicipioLlegadaAgregarRuta" InitialValue="S" ValidationGroup="FormularioAgregarRuta" ErrorMessage="Debe seleccionar el municipio de llegada del trayecto a realizar.">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>                        

                        <tr>
                            <td class="LabelFormularioAutoliquidacion">
                                Tiempo Aproximado Trayecto:
                                <span id="spnTiempoAgregarRuta" class="botonAyudaUP" title='Tiempo aproximado de viaje desde el punto de salida hasta el punto de llegada en el medio de transporte ingresado' divModal="dvAgregarRuta"></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:TextBox runat="server" ID="txtTiempoAgregarRuta"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvTiempoAgregarRuta" ControlToValidate="txtTiempoAgregarRuta" ValidationGroup="FormularioAgregarRuta" ErrorMessage="Debe ingresar el tiempo aproximado de viaje en el medio de transporte indicado.">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>

                    </table>
                    <table class="TablaBotonesFormularioAutoliquidacion">
                        <tr>
                            <td>
                                <asp:Button runat="server" ID="cdmAgregarRuta" ValidationGroup="FormularioAgregarRuta" Text="Agregar" ClientIDMode="Static" OnClick="cdmAgregarRuta_Click"/>
                                <asp:Button runat="server" ID="cmdCancelarAgregarRuta" CausesValidation="false" Text="Cancelar" ClientIDMode="Static" OnClick="cmdCancelarAgregarRuta_Click"/>
                                <asp:ValidationSummary ID="valFormularioAgregarRuta" runat="server" ValidationGroup="FormularioAgregarRuta" ShowMessageBox="true" ShowSummary="false" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cboMedioTransporteAgregarRuta" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cboDepartamentoPartidaAgregarRuta" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cboMunicipioPartidaAgregarRuta" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cboDepartamentoLlegadaAgregarRuta" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cdmAgregarRuta" EventName="Click" />                                                        
                    <asp:AsyncPostBackTrigger ControlID="cmdCancelarAgregarRuta" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>

        <asp:UpdateProgress ID="uppAgregarRuta" runat="server" AssociatedUpdatePanelID="upnlAgregarRuta">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imgAgregarRuta" runat="server" SkinId="procesando"/></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>


        <input type="button" runat="server" id="cmdAgregarCoordenadaHide" style="display:none;" />
        <cc1:ModalPopupExtender ID="mpeAgregarCoordenada" runat="server" PopupControlID="dvAgregarCoordenada" TargetControlID="cmdAgregarCoordenadaHide" BehaviorID="mpeAgregarCoordenadas" BackgroundCssClass="ModalBackgroundAutoliquidacion">
        </cc1:ModalPopupExtender>
        <div id="dvAgregarCoordenada" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalAutoliquidacion">
            <asp:UpdatePanel runat="server" ID="upnlAgregarCoordenada" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="TablaFormularioAutoliquidacion">
                        <tr>
                            <td colspan="2" class="TituloSeccionAutoliquidacion">
                                Agregar Coordenada
                            </td>
                        </tr>

                        <tr>
                            <td class="LabelFormularioAutoliquidacion">
                                Localización:
                                <span id="spnLocalizacionAgregarCoordenada" class="botonAyudaUP" title='Nombre de la ubicación' divModal="dvAgregarCoordenada"></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:TextBox runat="server" ID="txtLocalizacionAgregarCoordenada"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvLocalizacionAgregarCoordenada" ControlToValidate="txtLocalizacionAgregarCoordenada" ValidationGroup="FormularioAgregarCoordenada" ErrorMessage="Debe ingresar la localización.">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td class="LabelFormularioAutoliquidacion">
                                Tipo de Geometría:
                                <span id="spnTipoGeometriaAgregarCoordenada" class="botonAyudaUP" title='Tipo de geometría de la coordenada' divModal="dvAgregarCoordenada"></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:DropDownList runat="server" ID="cboTipoGeometriaAgregarCoordenada"></asp:DropDownList>                                
                            </td>
                        </tr>

                        <tr>
                            <td class="LabelFormularioAutoliquidacion">
                                Tipo de Coordenada:
                                <span id="spnTipoCoordenadaAgregarCoordenada" class="botonAyudaUP" title='Tipo de coordenada' divModal="dvAgregarCoordenada"></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:DropDownList runat="server" ID="cboTipoCoordenadaAgregarCoordenada"></asp:DropDownList>                                
                            </td>
                        </tr>

                        <tr>
                            <td class="LabelFormularioAutoliquidacion">
                                Origen - Magna Sirgas:
                                <span id="spnOrigenMagnaSirgasAgregarCoordenada" class="botonAyudaUP" title='Origen Marco Geocéntrico Nacional de Referencia' divModal="dvAgregarCoordenada"></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:DropDownList runat="server" ID="cboOrigenMagnaSirgasAgregarCoordenada"></asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td class="LabelFormularioAutoliquidacion">
                                Norte:
                                <span id="spnNorteAgregarCoordenada" class="botonAyudaUP" title='Posición norte-sur.<br/>Al digitar el valor, por favor tenga en cuenta que punto (.) se emplea para decimales y coma (,) se usa para separar el par de coordenadas' divModal="dvAgregarCoordenada"></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:TextBox runat="server" ID="txtNorteAgregarCoordenada"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvNorteAgregarCoordenada" ControlToValidate="txtNorteAgregarCoordenada" ValidationGroup="FormularioAgregarCoordenada" ErrorMessage="Debe ingresar la posición norte-sur.">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>                        

                        <tr>
                            <td class="LabelFormularioAutoliquidacion">
                                Este:
                                <span id="spnEsteAgregarCoordenada" class="botonAyudaUP" title='Posición este-oeste.<br/>Al digitar el valor, por favor tenga en cuenta que punto (.) se emplea para decimales y coma (,) se usa para separar el par de coordenadas' divModal="dvAgregarCoordenada"></span>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:TextBox runat="server" ID="txtEsteAgregarCoordenada"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvEsteAgregarCoordenada" ControlToValidate="txtEsteAgregarCoordenada" ValidationGroup="FormularioAgregarCoordenada" ErrorMessage="Debe ingresar la posición este-oeste.">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>

                    </table>
                    <table class="TablaBotonesFormularioAutoliquidacion">
                        <tr>
                            <td>
                                <asp:Button runat="server" ID="cdmAgregarCoordenada" ValidationGroup="FormularioAgregarCoordenada" Text="Agregar" ClientIDMode="Static" OnClick="cdmAgregarCoordenada_Click"/>
                                <asp:Button runat="server" ID="cmdCancelarAgregarCoordenada" CausesValidation="false" Text="Cancelar" ClientIDMode="Static" OnClick="cmdCancelarAgregarCoordenada_Click"/>
                                <asp:ValidationSummary ID="valFormularioAgregarCoordenada" runat="server" ValidationGroup="FormularioAgregarCoordenada" ShowMessageBox="true" ShowSummary="false" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cdmAgregarCoordenada" EventName="Click" />                                                        
                    <asp:AsyncPostBackTrigger ControlID="cmdCancelarAgregarCoordenada" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>

        <asp:UpdateProgress ID="uppAgregarCoordenada" runat="server" AssociatedUpdatePanelID="upnlAgregarCoordenada">
            <ProgressTemplate>  
                <div id="ModalProgressContainer">
                    <div>
                        <p>Procesando...</p>
                        <p><asp:Image ID="imgAgregarCoordenada" runat="server" SkinId="procesando"/></p>
                    </div>
                </div>                         
            </ProgressTemplate>
        </asp:UpdateProgress>



        <input type="button" runat="server" id="cmdConfirmarEnvioSolicitudHide" style="display:none;" />
        <cc1:ModalPopupExtender ID="mpeConfirmarEnvioSolicitud" runat="server" PopupControlID="dvConfirmarEnvioSolicitud" TargetControlID="cmdConfirmarEnvioSolicitudHide" BehaviorID="mpeConfirmarEnvioSolicitudes" BackgroundCssClass="ModalBackgroundAutoliquidacion">
        </cc1:ModalPopupExtender>
        <div id="dvConfirmarEnvioSolicitud" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalAutoliquidacion">
            <asp:UpdatePanel runat="server" ID="upnlConfirmarEnvioSolicitud" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="TablaFormularioAutoliquidacion">
                        <tr>
                            <td colspan="2" class="TituloSeccionAutoliquidacion">
                                Solicitud de Liquidación
                            </td>
                        </tr>
                        <tr>
                            <td class="ModalImagenes" rowspan="2">
                                <asp:Image runat="server" ID="imgImportanteEnvioSolicitud" ImageUrl="~/images/advertencia.png" />
                            </td>
                            <td class="ModalTextoTerminos">
                                <asp:Literal runat="server" ID="ltlTerminosConfirmarEnvioSolicitud"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table class="ModalTablaAceptarTerminos">
                                    <tr>
                                        <td>
                                            <asp:CheckBox runat="server" ID="chkAceptarTerminoCondiciones" ClientIDMode="Static" EnableTheming="false"/>                                        
                                            <asp:CustomValidator runat="server" ID="cvAceptarAdvertenciaConfirmarEnvioSolicitud" ValidationGroup="FormularioConfirmarEnvioSolicitud" ErrorMessage="Para continuar el proceso de envío de la solicitud debe confirmar que la información proporcionada es verídica y acepta las condiciones especificadas." ClientValidationFunction="VerificarTerminoCondiciones">&nbsp;</asp:CustomValidator>
                                        </td>
                                        <td class="ModalTextoAceptarTerminos">
                                            <b>Confirmo que la información del formulario es verídica y acepto las condiciones especificadas para el envío de la solicitud de liquidación.</b>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table class="TablaBotonesFormularioAutoliquidacion">
                        <tr>
                            <td>
                                <asp:Button runat="server" ID="cmdAceptarConfirmarEnvioSolicitud" ValidationGroup="FormularioConfirmarEnvioSolicitud" Text="Aceptar" ClientIDMode="Static" OnClick="cmdAceptarConfirmarEnvioSolicitud_Click"/>
                                <asp:Button runat="server" ID="cmdCancelarConfirmarEnvioSolicitud" CausesValidation="false" Text="Cancelar" ClientIDMode="Static" OnClick="cmdCancelarConfirmarEnvioSolicitud_Click"/>
                                <asp:ValidationSummary ID="valFormularioConfirmarEnvioSolicitud" runat="server" ValidationGroup="FormularioConfirmarEnvioSolicitud" ShowMessageBox="true" ShowSummary="false" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmdAceptarConfirmarEnvioSolicitud" EventName="Click" />                                                        
                    <asp:AsyncPostBackTrigger ControlID="cmdCancelarConfirmarEnvioSolicitud" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="uppConfirmarEnvioSolicitud" runat="server" AssociatedUpdatePanelID="upnlConfirmarEnvioSolicitud">
                <ProgressTemplate>  
                    <div id="ModalProgressContainer">
                        <div>
                            <p>Procesando...</p>
                            <p><asp:Image ID="imgConfirmarEnvioSolicitud" runat="server" SkinId="procesando"/></p>
                        </div>
                    </div>                         
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>

        <input type="button" runat="server" id="cmdErrorProcesoHide" style="display:none;" />
        <cc1:ModalPopupExtender ID="mpeErrorProceso" runat="server" PopupControlID="dvErrorProceso" TargetControlID="cmdErrorProcesoHide" BehaviorID="mpeErrorProcesos" BackgroundCssClass="ModalBackgroundAutoliquidacion">
        </cc1:ModalPopupExtender>
        <div id="dvErrorProceso" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalAutoliquidacion">
            <asp:UpdatePanel runat="server" ID="upnlErrorProceso" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="TablaFormularioAutoliquidacion">
                        <tr>
                            <td colspan="2" class="TituloSeccionAutoliquidacion">
                                Solicitud de Liquidación
                            </td>
                        </tr>
                        <tr>
                            <td class="ModalImagenes">
                                <asp:Image runat="server" ID="imgIconoErrorProceso" ImageUrl="~/images/error.png" />
                            </td>
                            <td class="ModalTextoTerminos">
                                <asp:Literal runat="server" ID="ltlErrorProceso"></asp:Literal>
                            </td>
                        </tr>                        
                    </table>
                    <table class="TablaBotonesFormularioAutoliquidacion">
                        <tr>
                            <td>
                                <asp:Button runat="server" ID="cmdAceptarErrorProceso" CausesValidation="false" Text="Aceptar" ClientIDMode="Static" OnClick="cmdAceptarErrorProceso_Click"/>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmdAceptarErrorProceso" EventName="Click" />                                                        
                </Triggers>
            </asp:UpdatePanel>

            <asp:UpdateProgress ID="uppErrorProceso" runat="server" AssociatedUpdatePanelID="upnlErrorProceso">
                <ProgressTemplate>  
                    <div id="ModalProgressContainer">
                        <div>
                            <p>Procesando...</p>
                            <p><asp:Image ID="imgErrorProceso" runat="server" SkinId="procesando"/></p>
                        </div>
                    </div>                         
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>

</asp:Content>
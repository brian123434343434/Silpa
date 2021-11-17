<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="VerFormularioAutoliquidacion.aspx.cs" Inherits="Liquidacion_VerFormularioAutoliquidacion" EnableEventValidation="false" %>
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
    <link href="css/FormularioAutoliquidacion.css" rel="stylesheet" />

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
                        </td>
                        <td class="CamposFormularioAutoliquidacion">
                            <asp:Literal runat="server" ID="ltlTipoSolicitudLiquidacionValor"></asp:Literal>
                        </td>
                    </tr>

                    <tr runat="server" id="trSolicitudLiquidacion">
                        <td class="LabelFormularioAutoliquidacion">
                            Solicitud de Liquidación:
                        </td>
                        <td class="CamposFormularioAutoliquidacion">
                            <asp:Literal runat="server" ID="ltlSolicitudLiquidacionValor"></asp:Literal>
                        </td>
                    </tr>

                    <tr runat="server" id="trTramiteLiquidacion">
                        <td class="LabelFormularioAutoliquidacion">
                            <asp:Literal runat="server" ID="ltlTramiteLiquidacion" Text="Trámite"></asp:Literal>                            
                        </td>
                        <td class="CamposFormularioAutoliquidacion">
                            <asp:Literal runat="server" ID="ltlTramiteLiquidacionValor"></asp:Literal>
                        </td>
                    </tr>

                    <tr runat="server" id="trSectorLiquidacion">
                        <td class="LabelFormularioAutoliquidacion">
                            Sector:
                        </td>
                        <td class="CamposFormularioAutoliquidacion">
                            <asp:Literal runat="server" ID="ltlSectorLiquidacionValor"></asp:Literal>
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
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlProyectoLiquidacionValor"></asp:Literal>
                            </td>
                        </tr>

                        <tr runat="server" id="trActividadLiquidacion">
                            <td class="LabelFormularioAutoliquidacion">
                                Tipo de Proyecto, Obra o Actividad:
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlActividadLiquidacionValor"></asp:Literal>
                            </td>
                        </tr>

                        <tr runat="server" id="trNombreProyectoLiquidacion">
                            <td class="LabelFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlNombreProyectoLiquidacion" Text="Nombre del Proyecto, Obra o Actividad:"></asp:Literal>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:TextBox runat="server" ID="txtNombreProyectoLiquidacion" TextMode="MultiLine" Rows="3" Width="100%" ReadOnly="true"></asp:TextBox>                                
                            </td>
                        </tr>

                        <tr runat="server" id="trDescripcionProyectoLiquidacion">
                            <td class="LabelFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlDescripcionProyectoLiquidacion" Text="Descripción breve del Instrumento de Manejo y Control:"></asp:Literal>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:TextBox runat="server" ID="txtDescripcionProyectoLiquidacion" TextMode="MultiLine" Rows="3" Width="100%" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>


                        <tr runat="server" id="trValorProyectoLiquidacion">
                            <td class="LabelFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlValorProyectoLiquidacion" Text="Valor de Proyecto en Pesos Colombianos:"></asp:Literal>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlValorProyectoLiquidacionValor"></asp:Literal>
                            </td>
                        </tr>

                        <tr runat="server" id="trValorProyectoLetrasLiquidacion">
                            <td class="LabelFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlValorProyectoLetrasLiquidacion" Text="Valor en Letras:"></asp:Literal>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlValorProyectoLetrasLiquidacionValor"></asp:Literal>
                            </td>
                        </tr>

                        <tr runat="server" id="trValorModificacionLiquidacion">
                            <td class="LabelFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlValorModificacionLiquidacion" Text="Valor de Modificación en Pesos Colombianos:"></asp:Literal>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlValorModificacionLiquidacionValor"></asp:Literal>
                            </td>
                        </tr>
                        <tr runat="server" id="trValorModificacionLetrasLiquidacion">
                            <td class="LabelFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlValorModificacionLetrasLiquidacion" Text="Valor de Modificación en Letras:"></asp:Literal>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlValorModificacionLetrasLiquidacionValor"></asp:Literal>
                            </td>
                        </tr>

                        <tr runat="server" id="trProyectoPINELiquidacion">
                            <td class="LabelFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlProyectoPINELiquidacion" Text="Proyecto PINE:"></asp:Literal>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlProyectoPINELiquidacionValor"></asp:Literal>
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
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <table class="TablaGrillaCampoAutoliquidacion">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grdPermisosLiquidacion" runat="server" Width="100%" AutoGenerateColumns="False" ShowFooter="False" SkinID="GrillaSolicitudAutoliquidacion"
                                                          ShowHeaderWhenEmpty="true" EmptyDataText="No se adicionaron permisos a la solicitud de liquidación">
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
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlRegionAutoliquidacionValor"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="LabelFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlUbicaciónLiquidacion" Text="Ubicación:"></asp:Literal>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <table class="TablaGrillaCampoAutoliquidacion">
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
                                                    <asp:TemplateField HeaderText="Ver" ItemStyle-CssClass="TextoFilaCentro">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkVerUbicacion" runat="server" Text="Ver" CommandArgument='<%# Eval("UbicacionSolicitudID") %>' OnClick="lnkVerUbicacion_Click" />
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
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlAguasMaritimasLiquidacionValor"></asp:Literal>
                            </td>
                        </tr>
                        <tr runat="server" id="trCualAguaMaritimaLiquidacion">
                            <td class="LabelFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlCualAguaMaritimaLiquidacion" Text="¿Cual?:"></asp:Literal>
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlCualAguaMaritimaLiquidacionValor"></asp:Literal>
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
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlAutoridadAmbientalLiquidacionValor"></asp:Literal>
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
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <table class="TablaGrillaCampoAutoliquidacion">
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
                            <asp:Button runat="server" ID="cmdVolver" CausesValidation="false" Text="Volver al Listado de Solicitudes" ClientIDMode="Static" OnClick="cmdVolver_Click"/>
                        </td>
                    </tr>
                </table>

            </ContentTemplate>

            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdVolver" EventName="Click" />
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

        <input type="button" runat="server" id="cmdAgregarUbicacionHide" style="display:none;" />
        <cc1:ModalPopupExtender ID="mpeAgregarUbicacion" runat="server" PopupControlID="dvAgregarUbicacion" TargetControlID="cmdAgregarUbicacionHide" BehaviorID="mpeAgregarUbicacions" BackgroundCssClass="ModalBackgroundAutoliquidacion">
        </cc1:ModalPopupExtender>
        <div id="dvAgregarUbicacion" style="display:none;" runat="server" clientidmode="Static" class="ContenedorModalAutoliquidacion">
            <asp:UpdatePanel runat="server" ID="upnlAgregarUbicacion" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="TablaFormularioAutoliquidacion">
                        <tr>
                            <td colspan="2" class="TituloSeccionAutoliquidacion">
                                Ubicación
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
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlDepartamentoUbicacionValor"></asp:Literal>
                            </td>
                        </tr>

                        <tr>
                            <td class="LabelFormularioAutoliquidacion">
                                Municipio:
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlMunicipioUbicacionValor"></asp:Literal>
                            </td>
                        </tr>

                        <tr>
                            <td class="LabelFormularioAutoliquidacion">
                                Corregimiento:
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlCorregimientoUbicacionValor"></asp:Literal>
                            </td>
                        </tr>

                        <tr>
                            <td class="LabelFormularioAutoliquidacion">
                                Vereda:
                            </td>
                            <td class="CamposFormularioAutoliquidacion">
                                <asp:Literal runat="server" ID="ltlVeredaUbicacionValor"></asp:Literal>
                            </td>
                        </tr>                        

                        <tr>
                            <td colspan="2" class="SubTituloSeccionAutoliquidacion">
                                Coordenadas de Ubicación
                            </td>
                        </tr>
                        <tr>
                            <td class="CamposFormularioAutoliquidacion"  colspan="2">
                                <table class="TablaGrillaCampoAutoliquidacion">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grdCoordenadasAgregarUbicacion" runat="server" Width="100%" AutoGenerateColumns="False" ShowFooter="False" SkinID="GrillaSolicitudAutoliquidacion"
                                                          ShowHeaderWhenEmpty="true" EmptyDataText="No se ha adicionaron coordenadas de ubicación">
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
                                <asp:Button runat="server" ID="cmdAceptarAgregarUbicacion" CausesValidation="false" Text="Aceptar" ClientIDMode="Static" OnClick="cmdAceptarAgregarUbicacion_Click"/>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cmdAceptarAgregarUbicacion" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
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
        </div>

</asp:Content>
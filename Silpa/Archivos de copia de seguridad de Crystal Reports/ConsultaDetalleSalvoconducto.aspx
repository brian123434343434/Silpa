<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/plantillas/SILPASinMenuFlash.master" CodeFile="ConsultaDetalleSalvoconducto.aspx.cs" Inherits="Salvoconducto_ConsultaDetalleSalvoconducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../jquery/jquery.js"></script>
    <link href="../jquery/EstiloGris/jquery-ui.css" rel="stylesheet" />
    <script src="../jquery/EstiloGris/jquery-ui.js"></script>
    <script src="../jquery/jquery.numeric.js"></script>
    <link href="../App_Themes/skin/StyleREDDS.css" rel="stylesheet" />
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>


    <style type="text/css">
        .CentrarTexto {
            text-align: center;
        }

        .AlinearDescripcion {
            text-align: center;
            vertical-align: central;
            width: 130px;
            font-weight: bold;
            color: #31708f;
            border-color: #bce8f1;
        }


        .alinearTitulos {
            text-align: center;
            vertical-align: central;
            width: 130px;
            font-weight: bold;
            color: #31708f;
            border-color: #bce8f1;
            background-color: #d9edf7;
            vertical-align: middle !important;
        }

        .alinearSubTitulos {
            text-align: center;
            vertical-align: central;
            font-weight: bold;
            background-color: #d9edf7;
            color: #31708f;
            vertical-align: middle !important;
        }

        .alinearTexto {
            text-align: center;
            vertical-align: central;
            font-weight: bold;
        }

        .AnchoAltoCheck {
            Width: 20px;
            Height: 20px;
        }
    </style>

    <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" Text="Detalle Salvoconducto" SkinID="titulo_principal_blanco"></asp:Label>
        &nbsp;
        <a href="#" id="hrCerrarVentana" visible="false" runat="server" onclick="window.close();return false;">Salir</a>
    </div>

    <div class="div-contenido">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableHistory="false">
        </asp:ScriptManager>

        <div class="contact_form">
            <table border="1" width="90%">
                <tr>
                    <td class="alinearTitulos" rowspan="2">EXPEDICION
                    </td>
                    <td class="alinearSubTitulos">Municipio</td>
                    <td class="alinearSubTitulos">Codigo</td>
                    <td class="alinearSubTitulos">Departamento</td>
                    <td class="alinearSubTitulos">Codigo </td>
                    <td class="alinearSubTitulos">Fecha de Expedicion</td>
                </tr>

                <tr>
                    <td class="CentrarTexto">
                        <asp:Label ID="LblMunicipioExp" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label></td>
                    <td class="CentrarTexto">
                        <asp:Label ID="LblCodigoMunExp" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label></td>
                    <td class="CentrarTexto">
                        <asp:Label ID="LblDptoExp" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label></td>
                    <td class="CentrarTexto">
                        <asp:Label ID="LblCodigoDptoExp" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label></td>
                    <td class="CentrarTexto">
                        <asp:Label ID="LblFechaExp" runat="server" Text="" Width="65px" SkinID="etiqueta_negra"></asp:Label></td>
                </tr>
            </table>
            <br />
            <table border="1" width="90%">
                <tr>
                    <td class="alinearTitulos">TIPO DE 
                        <br />
                        SALVOCONDUCTO
                    </td>
                    <td class="CentrarTexto">
                        <asp:Label ID="LblTipoSalvoconducto" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td class="alinearTitulos">ESTADO SALVOCONDUCTO
                    </td>
                    <td class="CentrarTexto">
                        <asp:Label ID="LblEstadoSalvoconducto" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td class="alinearTitulos" id="TituloSerieSalvoconducto" runat="server" visible="true">NUMERO SALVOCONDUCTO
                    </td>
                    <td class="alinearSubTitulos" id="ColSerieSalvoconducto" runat="server" visible="true">
                        <asp:Label ID="LblSerieAsignada" runat="server" Text="" ForeColor="#ff0066" Font-Bold="true" Font-Size="10pt"></asp:Label>
                    </td>
                    <td class="alinearTitulos" id="ColDescCodigoSeguridad" runat="server" visible="false">CODIGO SEGURIDAD
                    </td>
                    <td class="alinearSubTitulos" id="ColCodigoSeguridad" runat="server" visible="false">
                        <asp:Label ID="LblCodigoSeguridad" runat="server" Text="" ForeColor="#ff0066" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <table border="1" width="90%" runat="server" id="TblSalvoconductosAnteriores" visible="false">
                <tr>
                    <td class="alinearTitulos">SALVOCONDUCTOS 
                        <br />
                        ANTERIORES
                    </td>
                    <td style="align-items: center">
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
            </table>
            <br />
            <table border="1" width="90%">
                <tr>
                    <td rowspan="2" class="alinearTitulos">VIGENCIA DEL 
                        <br />
                        SALVOCONDUCTO 
                    </td>
                    <td class="alinearSubTitulos">Desde:</td>

                    <td class="alinearSubTitulos">Hasta:</td>

                </tr>
                <tr>
                    <td class="CentrarTexto">
                        <asp:Label ID="LblVigenciaDesde" runat="server" ClientIDMode="Static" MaxLength="10" Width="65px" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td class="CentrarTexto">
                        <asp:Label ID="LblVigenciaHasta" runat="server" ClientIDMode="Static" MaxLength="10" Width="65px" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <table border="1" width="90%">
                <tr style="text-align: center">
                    <td class="alinearTitulos" rowspan="4">TITULAR DEL 
                        <br />
                        SALVOCONDUCTO 
                    </td>
                    <td class="alinearSubTitulos">Nombre</td>
                    <td class="alinearSubTitulos">Identificacion</td>
                    <td class="alinearSubTitulos">Municipio de Domicilio</td>
                </tr>
                <tr style="text-align: center">
                    <td style="width: 200px;">
                        <asp:Label ID="LblNombreTitular" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td style="width: 200px;">
                        <asp:Label ID="LblIdentificacionTitular" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LbLMunicipioDomicilio" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                    </td>

                </tr>
                <tr style="text-align: center">
                    <td colspan="2" class="alinearSubTitulos">Direccion</td>
                    <td class="alinearSubTitulos">Telefono</td>
                </tr>
                <tr style="text-align: center">
                    <td colspan="2">
                        <asp:Label ID="LblDireccion" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LblTelefono" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                    </td>

                </tr>

            </table>
            <br />
            <table border="1" width="90%">
                <tr>
                    <td class="alinearTitulos">CLASE DE RECURSO
                    </td>
                    <td class="CentrarTexto">
                        <asp:Label ID="LblClaseRecurso" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <table border="1" width="90%" id="TblProcEspecimenesVarios" runat="server" visible="true">
                <tr>
                    <td class="alinearTitulos" rowspan="9">INFORMACION DE LA 
                        <br />
                        OBTENCION LEGAL 
                        <br />
                        DE LOS 
                        <br />
                        ESPECIMENES
                    </td>
                    <td style="align-items: center">
                        <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
            </table>
            <br />
            <table border="1" width="90%" id="TblProcEspecimenesUnico" runat="server" visible="true">
                <tr>
                    <td class="alinearTitulos" rowspan="9">OBTENCION LEGAL 
                        <br />
                        DE LOS 
                        <br />
                        ESPECIMENES
                    </td>
                    <td colspan="5" class="alinearSubTitulos">MODO DE ADQUIRIR DERECHO AL USO, TRANSPORTE Y/O COMERCIALIZACION
                    </td>
                </tr>
                <tr>
                    <td class="alinearSubTitulos">Modo</td>
                    <td class="alinearSubTitulos">Numero Acto Administrativo</td>
                    <td class="alinearSubTitulos">Fecha Expedicion</td>
                    <td class="alinearSubTitulos">Autoridad Ambiental</td>
                    <td class="alinearSubTitulos">Finalidad de Uso</td>
                </tr>
                <tr>
                    <td class="CentrarTexto" style="width: 150px;">
                        <asp:Label ID="LblModo" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td class="CentrarTexto" style="width: 100px">
                        <asp:Label ID="LblActoAdministrativo" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td class="CentrarTexto" style="width: 150px;">
                        <asp:Label ID="LblFechaObtLegalEsp" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td class="CentrarTexto">
                        <asp:Label ID="LblAutoridadAmbiental" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td class="CentrarTexto">
                        <asp:Label ID="LblFinalidadUso" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" class="alinearSubTitulos">INFORMACION DEL TITULAR </td>
                </tr>
                <tr>
                    <td class="alinearSubTitulos" style="width: 80px;">Nombre del Titular
                    </td>
                    <td class="alinearSubTitulos" style="width: 80px;">CC - NIT
                    </td>
                    <td class="alinearSubTitulos" style="width: 80px;">Municipio de Domicilio
                    </td>
                    <td class="alinearSubTitulos" style="width: 80px;">Telefono
                    </td>
                    <td class="alinearSubTitulos">Direccion 
                    </td>
                </tr>
                <tr>
                    <td class="CentrarTexto">
                        <asp:Label ID="LblNomTitularsObtLegalEsp" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td class="CentrarTexto">
                        <asp:Label ID="LblIdentTitularsObtLegalEsp" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td class="CentrarTexto">
                        <asp:Label ID="LblMunTitularsObtLegalEsp" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td class="CentrarTexto">
                        <asp:Label ID="LblTelTitularsObtLegalEsp" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td class="CentrarTexto">
                        <asp:Label ID="LblDirTitularsObtLegalEsp" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" class="alinearSubTitulos">PROCEDENCIA ESPECIMENES
                    </td>
                </tr>
                <tr>
                    <td class="alinearSubTitulos">Vereda
                    </td>
                    <td class="alinearSubTitulos">Municipio
                    </td>
                    <td class="alinearSubTitulos">Codigo
                    </td>
                    <td class="alinearSubTitulos">Departamento
                    </td>
                    <td class="alinearSubTitulos">Codigo
                    </td>
                </tr>
                <tr>
                    <td class="alinearTexto">
                        <asp:Label ID="LblVeredaProcEsp" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td class="alinearTexto">
                        <asp:Label ID="LblMunicipioProcEsp" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td class="alinearTexto">
                        <asp:Label ID="LblCodMunProcEsp" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td class="alinearTexto">
                        <asp:Label ID="LblDepartamentoProcEsp" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td class="alinearTexto">
                        <asp:Label ID="LblCodDptoProcEsp" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <table border="1" width="90%" runat="server" id ="TblRutaDesplazamiento">
                <tr>
                    <td class="alinearTitulos" rowspan="2">RUTA DE DESPLAZAMIENTO
                    </td>
                    <td>Tipo de Movilizacion:  &nbsp
                        <asp:Label ID="LblTipoMovilizacion" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grvRutaDesplazamiento" runat="server" AutoGenerateColumns="false" ShowFooter="true" OnRowDataBound="GridView1_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Orden">
                                            <ItemTemplate>
                                                <asp:Label ID="LblOrden" runat="server" Text='<%# Bind("Orden") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="alinearTitulos" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="IdOrigenDestino" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LblIdOrigenDestino" runat="server" Text='<%# Bind("IdOrigenDestino") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Departamento">
                                            <ItemTemplate>
                                                <asp:Label ID="LblDepartamento" runat="server" Text='<%# Bind("Departamento") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="alinearTitulos" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Municipio">
                                            <ItemTemplate>
                                                <asp:Label ID="LblMunicipio" runat="server" Text='<%# Bind("Municipio") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="alinearTitulos" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Barrio">
                                            <ItemTemplate>
                                                <asp:Label ID="LblBarrio" runat="server" Text='<%# Bind("Barrio") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle CssClass="alinearTitulos" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </td>
                </tr>
            </table>
            <br />
            <table border="1" width="90%" runat="server" id="TblTransporte">
                <tr>
                    <td class="alinearTitulos" rowspan="2">TRANSPORTE
                    </td>
                    <td class="CentrarTexto">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grvTransporte" runat="server" AutoGenerateColumns="false" ShowFooter="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Modo transporte">
                                            <ItemTemplate>
                                                <asp:Label ID="LblModoTransporte" runat="server" Text='<%# Bind("ModoTransporte") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="alinearTitulos" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Típo de Vehículo">
                                            <ItemTemplate>
                                                <asp:Label ID="LblTipoTransporte" runat="server" Text='<%# Eval("TipoTransporteOtro") != null ?Eval("TipoTransporteOtro"): Eval("TipoTransporte") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="alinearTitulos" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Empresa">
                                            <ItemTemplate>
                                                <asp:Label ID="LblEmpresa" runat="server" Text='<%# Bind("Empresa") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="alinearTitulos" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Ident. Transporte">
                                            <ItemTemplate>
                                                <asp:Label ID="LblNumeroIdentificacionMedioTransporte" runat="server" Text='<%# Bind("NumeroIdentificacionMedioTransporte") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="alinearTitulos" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Transportador">
                                            <ItemTemplate>
                                                <asp:Label ID="LblNombreTransportador" runat="server" Text='<%# Bind("NombreTransportador") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="alinearTitulos" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Ident. Transportador">
                                            <ItemTemplate>
                                                <asp:Label ID="LblNumeroIdentificacionTransportador" runat="server" Text='<%# Bind("NumeroIdentificacionTransportador") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="alinearTitulos" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>

                </tr>
            </table>
            <br />

            <table border="1" width="90%">
                <tr>
                    <td class="alinearTitulos" rowspan="2">INFORMACION DE ESPECIMENES
                    </td>
                    <td style="overflow: scroll">
                        <asp:GridView ID="gdvEspecimenes" runat="server" AutoGenerateColumns="false"
                            CellPadding="2" CellSpacing="1" GridLines="None" ShowFooter="true"
                            HorizontalAlign="Center" SkinID="GrillaCoordenadas" OnPageIndexChanging="gdvEspecimenes_PageIndexChanging">
                            <Columns>
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="NumeroSUNLAnterior" HeaderText="Salvoconducto Anterior" ItemStyle-Width="350px" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos"/>
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="NombreEspecie" HeaderText="Nombre" ItemStyle-Width="350px" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos" />
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="NombreComunEspecie" HeaderText="Nombre Comun" ItemStyle-Width="350px" ItemStyle-HorizontalAlign="Justify" FooterStyle-CssClass="alinearSubTitulos"/>
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="ClaseProducto" HeaderText="Clase Producto" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos" />
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="TipoProducto" HeaderText="Tipo Producto" ItemStyle-Width="350px" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos" />
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="UnidadMedida" HeaderText="Unidad Medida" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos" />
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="Cantidad" HeaderText="Cantidad" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos" />
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="Volumen" HeaderText="Volumen" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos" />
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="Dimensiones" HeaderText="Dimensiones" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos" />
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="Descripcion" HeaderText="Descripción" ItemStyle-Width="350px" FooterStyle-CssClass="alinearSubTitulos" ItemStyle-HorizontalAlign="Justify" />
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="CantidadDisponible" HeaderText="Cantidad Disponible" ItemStyle-Width="350px" FooterStyle-CssClass="alinearSubTitulos" ItemStyle-HorizontalAlign="Justify" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
               <tr>
                    <td style="overflow:scroll">
                        <div>
                            <label id="LblLstTotalTipProductUm" visible="false" runat="server" for="GrvTotalesEspecies" title="" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 100%">Detalle por Tipo producto y unidad de medida de los especimenes:</label>
                            <asp:GridView ID="GrvTotalesEspecies" runat="server" AutoGenerateColumns="false" Width="100%"
                                CellPadding="2" CellSpacing="1" GridLines="None" ShowFooter="True" HorizontalAlign="Center" SkinID="GrillaCoordenadas">
                                <Columns>
                                    <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" ItemStyle-Width="350px" FooterStyle-CssClass="alinearSubTitulos" DataField="NombreComunEspecie" HeaderText="Nombre Comun" />
                                    <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" ItemStyle-Width="350px" FooterStyle-CssClass="alinearSubTitulos" DataField="NombreEspecie" HeaderText="Nombre Cientifico" />
                                    <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" ItemStyle-Width="350px" FooterStyle-CssClass="alinearSubTitulos" DataField="TipoProducto" HeaderText="Tipo Producto" />
                                    <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" ItemStyle-Width="350px" FooterStyle-CssClass="alinearSubTitulos" DataField="UnidadMedida" HeaderText="Unidad Medida" />
                                    <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" ItemStyle-Width="350px" FooterStyle-CssClass="alinearSubTitulos" DataField="TotalCantidad" HeaderText="Cantidad" />
                                    <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" ItemStyle-Width="350px" FooterStyle-CssClass="alinearSubTitulos" DataField="TotalVolumen" HeaderText="Volumen Total" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
            </table>
            <br />
            <table border="1" width="90%" id="tblSalvoRelacionados" runat="server">
                <tr>
                    <td class="alinearTitulos">INFORMACION DE SALVOCONDUCTOS RELACIONADOS
                    </td>
                    <td style="overflow: scroll">
                        <asp:GridView ID="gdvSalvoconductoRelacionados" runat="server" AutoGenerateColumns="false"
                            CellPadding="2" CellSpacing="1" GridLines="None" ShowFooter="true"
                            HorizontalAlign="Center" SkinID="GrillaCoordenadas" OnPageIndexChanging="gdvSalvoconductoRelacionados_PageIndexChanging">
                            <Columns>
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="NUMERO" HeaderText="Número" ItemStyle-Width="350px" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos" />
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="TIPO_SALVOCONDUCTO" HeaderText="Tipo" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos" />
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="NOMBRE_CIENTIFICO" HeaderText="Especie" ItemStyle-Width="350px" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos" />
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="TIPO_PRODUCTO" HeaderText="Tipo Producto" ItemStyle-Width="350px" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos" />
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="CANTIDAD" HeaderText="Cantidad" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos" />
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="VOLUMEN" HeaderText="Volumen" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos" />
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="SOLICITANTE" HeaderText="Titular" ItemStyle-Width="350px" FooterStyle-CssClass="alinearSubTitulos" ItemStyle-HorizontalAlign="Justify" />
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <br runat="server" id="BRMotivoRechazo" visible="false" />
            <table border="1" width="90%" runat="server" id="TblMotivoRechazo" visible="false">
                <tr>
                    <td class="alinearTitulos">MOTIVO 
                        <br />
                        RECHAZO
                        <br />
                    </td>
                    <td style="align-items: center; overflow: scroll">
                        <asp:TextBox ID="TxtMotivoRechazo" TextMode="MultiLine" MaxLength="1000" Width="95%" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="UpdBloqueoSalvoconducto" runat="server">
                <ContentTemplate>
                    <br runat="server" id="BRBloqueo" visible="false" />
                    <table border="1" width="90%" runat="server" id="TblBloqueoSalvoconducto" visible="false">
                        <tr>
                            <td class="alinearTitulos">BLOQUEAR                       
                        <br />
                                SALVOCONDUCTO
                        <br />
                            </td>
                            <td style="align-items: flex-start;">&nbsp
                                <asp:Label ID="LblMotivoBloqueo" runat="server" Text="MOTIVO BLOQUEO:" Visible="true" SkinID="etiqueta_negra"></asp:Label>
                                &nbsp
                                <asp:DropDownList ID="CboMotivoBloqueo" runat="server" ValidationGroup="BloquearSalvoconducto"></asp:DropDownList>
                                &nbsp
                        <asp:Button ID="BtnBloquearSalvoconducto" runat="server" Text="Bloquear" OnClientClick="return confirm('Esta Seguro de Bloquear El Salvoconducto?')" OnClick="BtnBloquearSalvoconducto_Click" ValidationGroup="BloquearSalvoconducto" />
                                &nbsp
                        <asp:RequiredFieldValidator ID="RFVMotivoBloqueo" runat="server" ErrorMessage="Debe Seleccionar un Motivo de Bloqueo" ValidationGroup="BloquearSalvoconducto" ControlToValidate="CboMotivoBloqueo" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="BtnBloquearSalvoconducto" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <br />
            <table border="1" width="90%" runat="server" id="tblDesbloqueo" visible="false">
                <tr>
                    <td class="alinearTitulos">DESBLOQUEAR                       
                    <br />
                            SALVOCONDUCTO
                    <br />
                    </td>
                    <td style="align-items: flex-start;">&nbsp
                        <asp:Label ID="Label1" runat="server" Text="MOTIVO DESBLOQUEO:" Visible="true" SkinID="etiqueta_negra"></asp:Label>
                        <asp:Label ID="lblAutoridadBloquea" runat="server" Visible="false"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtMotivoDesbloqueo" runat="server" TextMode="MultiLine" MaxLength="1000" Width="95%"></asp:TextBox>
                        <br />
                        <asp:FileUpload ID="fuplSoporteDesbloqueo" runat="server" />
                        <br />
                        <asp:Button ID="btnDesbloquear" runat="server" Text="Desbloquear" OnClientClick="return confirm('Esta Seguro de desbloquear el salvoconducto?')" OnClick="btnDesbloquear_Click" ValidationGroup="desbloquearSalvoconducto" />
                                &nbsp
                        <asp:RequiredFieldValidator ID="rfvMotivoDesbloqueo" runat="server" ErrorMessage="Debe ingresar un motivo de desbloqueo" ValidationGroup="desbloquearSalvoconducto" ControlToValidate="txtMotivoDesbloqueo" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>


        </div>
    </div>

    <script src="../js/SeriesSalvoConducto.js"></script>



</asp:Content>

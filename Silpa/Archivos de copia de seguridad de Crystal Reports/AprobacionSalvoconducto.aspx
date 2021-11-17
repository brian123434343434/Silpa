<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/plantillas/SILPASinMenuFlash.master" CodeFile="AprobacionSalvoconducto.aspx.cs" Inherits="Salvoconducto_AprobacionSalvoconducto" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" runat="Server">
   <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/jquery.datetimepicker.css" rel="stylesheet" />
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type ="text/css">


        .modal-background {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.5;
            z-index: 10 !important;
        }

        .modal-container {
            border: 1px outset #808080;
            background-color: White;
            padding: 0px;
            font-weight: bold;
            font-style: normal;
            width: 50%;
            padding: 10px 5px 5px 5px;
        }


        .CentrarTexto{
            text-align:center;
            padding-top:10px;
            padding-bottom:10px;
        }

        .AlinearDescripcion
        {
            text-align:center;
            vertical-align:central;
            width:130px;
            font-weight:bold;
            color: #31708f;
            border-color: #bce8f1;
        }


        .alinearTitulos{
            text-align:center;
            vertical-align:central;
            width:130px;
            font-weight:bold;
            color: #31708f;
            border-color: #bce8f1;
            background-color: #d9edf7;
            vertical-align:middle !important;

        }

        .alinearSubTitulos{
            text-align:center;
            vertical-align:central;
            font-weight:bold;
            background-color: #d9edf7;
            color: #31708f;
            vertical-align:middle !important;
        }

        .alinearTexto{
            text-align:center;
            vertical-align:central;
            font-weight:bold;
        }
        
        .AnchoAltoCheck{
            Width:20px;
            Height:20px;
        }


        </style>

     <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" Text="Emitir Salvoconducto" SkinID="titulo_principal_blanco"></asp:Label>
        &nbsp;
        <a href="#" id="hrCerrarVentana" visible="false" runat="server" onclick="window.close();return false;">Salir</a>
    </div>

    <div class="div-contenido">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableHistory ="false">
        </asp:ScriptManager>

        <div class="contact_form" id="dContactForm">
            <table border="1" width="90%">
                <tr>
                    <td class="alinearTitulos" rowspan="2">EXPEDICION
                        <br />
                        <asp:CheckBox ID="ChkDatosExpedicion" runat="server" Width="100px" CssClass="AcceptedAgreement" ClientIDMode="Static" EnableTheming="false" />
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
                        
                        <asp:TextBox ID="TxtFechaExp" runat="server" ClientIDMode="Static" MaxLength="10" Enabled="true" Width="90px" ControlToValidate="TxtFechaExp"></asp:TextBox>
                       
                        <asp:RequiredFieldValidator ID="RFVFechaExp" runat="server" ErrorMessage="Debe Ingresar Fecha Expedicion" ControlToValidate="TxtFechaExp" ValidationGroup="ValidarSalvoconducto" ClientIDMode="Static" Display="Dynamic"></asp:RequiredFieldValidator>
                        
                        <asp:RegularExpressionValidator ID="REFechaExp" runat="server" ErrorMessage="Formato Fecha No Valido" ValidationGroup="ValidarSalvoconducto" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$" ControlToValidate="TxtFechaExp" Display="Dynamic"></asp:RegularExpressionValidator>
                        
                    </td>
                </tr>
            </table>
            <br />
            <table border="1" width="90%">
                <tr>
                    <td class="alinearTitulos" >TIPO DE 
                        <br />
                        SALVOCONDUCTO
                        <br />
                        <asp:CheckBox ID="ChkDatosTipoSalvoconducto" runat="server"  ClientIDMode="Static" EnableTheming="false" />
                    </td>
                    <td class="CentrarTexto">
                        <asp:Label ID="LblTipoSalvoconducto" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>     
                    </td>
                </tr>
            </table>
            <br />
            <table border="1" width="90%" runat="server" ID="TblSalvoconductosAnteriores" visible="false">
                <tr>
                    <td class="alinearTitulos" > SALVOCONDUCTOS 
                        <br />
                        ANTERIORES
                        <br />
                        <asp:CheckBox ID="ChkDatosSalvoconductoAnterior" runat="server" EnableTheming="false" />
                    </td>
                    <td style="align-items:center">
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
                        <br />
                        <asp:CheckBox ID="ChkDatosVigenciaSalvoconducto" runat="server" EnableTheming="false" />
                    </td>
                    <td class="alinearSubTitulos">Desde:</td>
                   
                    <td class="alinearSubTitulos">Hasta:</td>
                    
                </tr>
                <tr> 
                    <td class="CentrarTexto" style="width:350px">
                        
                         <asp:TextBox ID="TxtVigenciaDesde" runat="server" ClientIDMode="Static" MaxLength="10" Width="90px"></asp:TextBox>
                         
                         <asp:RequiredFieldValidator ID="RFVVigenciaDesde" runat="server" ErrorMessage="Debe Ingresar Vigencia Desde" ControlToValidate="TxtVigenciaDesde" ValidationGroup="ValidarSalvoconducto" Display="Dynamic"></asp:RequiredFieldValidator>
                         
                         <asp:RegularExpressionValidator ID="ReFecDesde" runat="server" ErrorMessage="Formato Fecha No Valido" ValidationGroup="ValidarSalvoconducto" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$" ControlToValidate="TxtVigenciaDesde" Display="Dynamic"></asp:RegularExpressionValidator>
                    </td>
                    <td class="CentrarTexto" style="width:350px"> 
                        
                        <asp:TextBox ID="TxtVigenciaHasta" runat="server" ClientIDMode="Static" MaxLength="10" Width="90px"></asp:TextBox>
                        
                        <asp:RequiredFieldValidator ID="RFVVigenciaHasta" runat="server" ErrorMessage="Debe Ingresar Vigencia Hasta" ControlToValidate="TxtVigenciaHasta" ValidationGroup="ValidarSalvoconducto" Display="Dynamic"></asp:RequiredFieldValidator>
                        
                        <%--<asp:CompareValidator ID="CVFecDesdeHasta" runat="server" ErrorMessage="La fecha hasta no puede ser mayor a la desde" ValidationGroup="ValidarSalvoconducto" ControlToCompare="TxtVigenciaDesde" ControlToValidate="TxtVigenciaHasta" Operator="GreaterThan" Type="Date" Display="Dynamic"></asp:CompareValidator>--%>
                        
                        <asp:RegularExpressionValidator ID="ReFecHasta" runat="server" ErrorMessage="Formato Fecha No Valido" ValidationGroup="ValidarSalvoconducto" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$" ControlToValidate="TxtVigenciaHasta" Display="Dynamic"></asp:RegularExpressionValidator>
                    </td>
                </tr>
            </table>
            <br />
            <table border="1" width="90%">
                <tr style="text-align: center">
                    <td class="alinearTitulos" rowspan="4">TITULAR DEL 
                        <br />
                        SALVOCONDUCTO 
                        <br />
                        <asp:CheckBox ID="ChkDatosTitularSalvoconducto" runat="server" EnableTheming="false" />
                    </td>
                    <td class="alinearSubTitulos">Nombre</td>
                    <td class="alinearSubTitulos">Identificacion</td>
                    <td class="alinearSubTitulos">Municipio de Domicilio</td>
                </tr>
                <tr style="text-align: center">
                   <td style="width: 200px;">
                        <asp:Label ID="LblNombreTitular" runat="server" Text="LblNombreTitular" SkinID="etiqueta_negra"></asp:Label>
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
                        <br />
                        <asp:CheckBox ID="ChkDatosClaseRecurso" runat="server" EnableTheming="false" />
                    </td>
                    <td class="CentrarTexto">
                        <asp:Label  ID="LblClaseRecurso" runat="server" Text="LblClaseRecurso" SkinID="etiqueta_negra"></asp:Label>
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
                        <br />
                        <asp:CheckBox ID="ChkDatosInfoProcEspecimenesVarios" runat="server" EnableTheming="false" />
                    </td>
                    <td style="align-items:center">
                        <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                    </td>
               </tr>
            </table>
            <br />
            <table border="1" width="90%" id="TblProcEspecimenesUnico" runat="server" visible="true">
                <tr>
                    <td class="alinearTitulos" rowspan="9">INFORMACION DE LA 
                        <br />
                        OBTENCION LEGAL 
                        <br />
                        DE LOS 
                        <br />
                        ESPECIMENES
                        <br />
                        <asp:CheckBox ID="ChkDatosInfoProcEspecimenesUnico" runat="server" EnableTheming="false" />
                    </td>
                    <td colspan="5" class="alinearSubTitulos">MODO ADQUISICION
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
                        <asp:Label ID="LblModo" runat="server" Text="LblModo" SkinID="etiqueta_negra"></asp:Label>
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
            <table border="1" width="90%">
                <tr>
                    <td class="alinearTitulos" rowspan="3">
                        RUTA DE DESPLAZAMIENTO
                        <br />
                        <asp:CheckBox ID="ChkDatosRutaDesplazamiento" runat="server" EnableTheming="false" />
                    </td>
                    <td>
                        Tipo de Movilizacion:  &nbsp <asp:Label ID="LblTipoMovilizacion" runat="server" Text="" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                        <asp:GridView ID="grvRutaDesplazamiento" runat="server" AutoGenerateColumns="false" ShowFooter ="true" OnRowDataBound="GridView1_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Orden">
                                    <ItemTemplate>
                                        <asp:Label ID="LblOrden" runat="server" Text='<%# Bind("Orden") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="alinearTitulos" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="IdOrigenDestino" Visible ="false" >
                                    <ItemTemplate>
                                        <asp:Label ID="LblIdOrigenDestino" runat="server" Text='<%# Bind("IdOrigenDestino") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Departamento">
                                    <ItemTemplate>
                                        <asp:Label ID="LblDepartamento" runat="server" Text='<%# Bind("Departamento") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table border="0" cellpadding="2" cellspacing="0" style="text-align: center; vertical-align: top;">
                                            <tr>
                                                <td style="text-align: center; vertical-align: top;">
                                                    <asp:DropDownList ID="CboDepartamento" runat="server" CssClass="textInput" OnSelectedIndexChanged="CboDepartamento_SelectedIndexChanged" AutoPostBack="true" Width="130px"></asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <HeaderStyle CssClass="alinearTitulos" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Municipio">
                                    <ItemTemplate>
                                        <asp:Label ID="LblMunicipio" runat="server" Text='<%# Bind("Municipio") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table border="0" cellpadding="2" cellspacing="0" style="text-align: center; vertical-align: top;">
                                            <tr>
                                                <td style="text-align: center; vertical-align: top;">
                                                    <asp:DropDownList ID="CboMunicipio" runat="server" CssClass="textInput" Width="130px"></asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <HeaderStyle CssClass="alinearTitulos" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Top"/>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Barrio">
                                    <ItemTemplate>
                                        <asp:Label ID="LblBarrio" runat="server" Text='<%# Bind("Barrio") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table border="0" cellpadding="2" cellspacing="0" style="text-align: center; vertical-align: top;">
                                            <tr>
                                                <td style="text-align: center; vertical-align: top;">
                                                    <asp:TextBox ID="TxtBarrio" runat="server" Class="textInput" Width="80px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <HeaderStyle CssClass="alinearTitulos" />
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>



                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LnkEliminarRuta" runat="server" CausesValidation="True" CommandName="Update" OnClick="grvRutaDesplazamiento_lnkEliminar_Click" CommandArgument='<%# Eval("Orden")%>'
                                            Text="Eliminar"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <table style="text-align: center; vertical-align: top;">
                                            <tr>
                                                <td style="text-align: center; vertical-align: top;">
                                                    <asp:LinkButton ID="LnkAdicionarRuta" runat="server" CausesValidation="True" ValidationGroup="ValidarRuta"  OnClick="grvRutaDesplazamiento_lnkInsertar_Click" CommandArgument='<%# Eval("Orden")%>'
                                                        Text="Insertar"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <HeaderStyle CssClass="alinearTitulos"/>
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
         <table border="1" width="90%" >
                <tr>
                    <td class="alinearTitulos" rowspan="2">
                        &nbsp;TRANSPORTE
                        <br />
                        <asp:CheckBox ID="ChkDatosModoTransporte" runat="server" EnableTheming="false" />
                    </td>
                    <td class="CentrarTexto" style="overflow:scroll">
                        <asp:GridView ID="grvTransporte" runat="server" AutoGenerateColumns="false" 
                            CellPadding="2" CellSpacing="1" GridLines="None" ShowFooter="true" 
                            HorizontalAlign="Center" SkinID="GrillaCoordenadas">
                            <Columns>
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="ModoTransporte" HeaderText="Modo transporte" ItemStyle-Width="350px" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos"/>
                                 <asp:TemplateField HeaderText="Típo de Vehículo">
                                    <ItemTemplate>
                                        <asp:Label ID="LblTipoTransporte" runat="server" Text='<%# Eval("TipoTransporteOtro") != null ? Eval("TipoTransporteOtro"): Eval("TipoTransporte") %>' EnableTheming="false"></asp:Label>
                                    </ItemTemplate>
                                     <HeaderStyle CssClass="alinearTitulos" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"/>
                                    <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="alinearSubTitulos"/>
                                </asp:TemplateField>
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="Empresa" HeaderText="Empresa" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos"/>
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="NumeroIdentificacionMedioTransporte" HeaderText="Ident. Transporte" ItemStyle-Width="350px" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos"/>
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="NombreTransportador" HeaderText="Transportador" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos" />
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="NumeroIdentificacionTransportador" HeaderText="Ident. Transportador" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos"/>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
         <br />
         <table border="1" width="90%">
                <tr>
                    <td class="alinearTitulos" rowspan="2">
                        INFORMACION DE ESPECIMENES
                        <br />
                        <asp:CheckBox ID="CkhDatosInfoEspecimenes" runat="server" EnableTheming="false" />
                    </td>
                    <td style="overflow:scroll">
                        <asp:GridView ID="gdvEspecimenes" runat="server" AutoGenerateColumns="false" 
                            CellPadding="2" CellSpacing="1" GridLines="None" ShowFooter="true" 
                            HorizontalAlign="Center" SkinID="GrillaCoordenadas" OnPageIndexChanging="gdvEspecimenes_PageIndexChanging">
                            <Columns>
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="NumeroSUNLAnterior" HeaderText="Salvoconducto Anterior" ItemStyle-Width="350px" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos"/>
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="NombreEspecie" HeaderText="Nombre" ItemStyle-Width="350px" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos"/>
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="NombreComunEspecie" HeaderText="Nombre Comun" ItemStyle-Width="350px" ItemStyle-HorizontalAlign="Justify" FooterStyle-CssClass="alinearSubTitulos"/>
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="Descripcion" HeaderText="Descripción" ItemStyle-Width="350px" FooterStyle-CssClass="alinearSubTitulos" ItemStyle-HorizontalAlign="Justify" />
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="ClaseProducto" HeaderText="Clase Producto" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos"/>
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="TipoProducto" HeaderText="Tipo Producto" ItemStyle-Width="350px" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos"/>
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="Cantidad" HeaderText="Cantidad" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos" />
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="UnidadMedida" HeaderText="Unidad Medida" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos"/>
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="Volumen" HeaderText="Volumen" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos" />
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="Dimensiones" HeaderText="Dimensiones" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos" />
                                <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="Identificacion" HeaderText="Identificacion" ItemStyle-HorizontalAlign="Center" FooterStyle-CssClass="alinearSubTitulos" />
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

            <asp:UpdatePanel ID="udpMotivoRechazo" runat="server">
                <ContentTemplate>
                    <table border="1" width="90%" runat="server" id="TblMotivoRechazo" visible="false">
                        <tr>
                            <td class="alinearTitulos" rowspan="2">MOTIVO 
                                <br />
                                RECHAZO
                                <br />
                            </td>
                            <td style="align-items: center; overflow: scroll">
                                <asp:TextBox ID="TxtMotivoRechazo" TextMode="MultiLine" MaxLength="1000" Width="95%" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:UpdatePanel ID="UpdRechazarSalvoconducto" runat ="server">
                                    <ContentTemplate>
                                        <asp:Button ID="BtnGrabarRechazo" runat="server" Text="Grabar Rechazo" ValidationGroup="ValidacionRechazo" OnClientClick="return confirm('Esta Seguro de Rechazar la Solicitud?');" OnClick="BtnGrabarRechazo_Click" ReplaceTitleTo="Grabando..." />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                
                                <br />
                                <asp:RequiredFieldValidator ValidationGroup="ValidacionRechazo" ID="FRVTextoRechazo" runat="server" ErrorMessage="Debe Ingresar un Motivo de Rechazo" ControlToValidate="TxtMotivoRechazo" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="BtnRechazar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
                <br />
            <table>
                <tr>
                    <td style="align-items: center;">
                        <asp:UpdatePanel ID="UpdEmitirSalvoconducto" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="BtnEmitir" runat="server" Text="Emitir" OnClick="BtnEmitir_Click" OnClientClick="return ValidarInformacion();" ValidationGroup="ValidarSalvoconducto" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="align-items: center;">
                        <asp:Button ID="BtnRechazar" runat="server" Text="Rechazar" OnClick="BtnRechazar_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <asp:UpdateProgress ID="UpdPEmitirSalvoconducto" runat="server" AssociatedUpdatePanelID="UpdEmitirSalvoconducto">
        <ProgressTemplate>
            <div id="ModalProgressContainer">
                <div>
                    <p>Procesando...</p>
                    <p>
                        <asp:Image ID="imgUpdateProgress" runat="server" SkinID="procesando" /></p>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdateProgress ID="UpdPRechazarSalvoconducto" runat="server" AssociatedUpdatePanelID="UpdRechazarSalvoconducto">
        <ProgressTemplate>
            <div id="ModalProgressContainer">
                <div>
                    <p>Procesando...</p>
                    <p>
                        <asp:Image ID="imgUpdateProgress1" runat="server" SkinID="procesando" /></p>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

   <input type="button" runat="server" id="BtnValidaSUNLOculto" style="display:none" />
    <ajax:ModalPopupExtender id="mpeValidacionSUNL" runat="server" popupcontrolid="DivValidacionSUNL" targetcontrolid="BtnValidaSUNLOculto" behaviorid="mpeValidacionSUNL" backgroundcssclass="modal-background"></ajax:ModalPopupExtender> 
    <div id="DivValidacionSUNL" clientidmode="Static" runat="server" style="display: none; width: 600px" class="modal-container">
        <div class="center" style="width: 100%">
            <asp:UpdatePanel ID="UpdPnlDivCondPpal" runat="server">
                <ContentTemplate>
                    <table border="1" style="width: 100%; background-color: white">
                        <tr>
                            <td style="text-align: center">
                                <asp:Label ID="LblTitulo" runat="server" Text="Validacion Salvocondcuto" Font-Size="Medium" SkinID="alinearSubTitulos"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:Label ID="LblTexto" runat="server" Text="Se encontraron Inconsistencias al generar el Salvoconducto, por favor Verificar los datos" SkinID="alinearSubTitulos"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="overflow: scroll; width: 100%; height: 100%">
                                    <asp:GridView ID="GrvSaldoEspecies" AllowPaging="false" ShowFooter="false" runat="server" SkinID="GrillaCoordenadas" AutoGenerateColumns="false" Width="100%">
                                        <Columns>
                                            <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="AUTORIDAD_EMISORA" HeaderText="Autoridad Ambiental" FooterStyle-CssClass="alinearSubTitulos" />
                                            <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="TIPO_SUNL_ANTERIOR" HeaderText="Tipo SUNL Anterior" FooterStyle-CssClass="alinearSubTitulos"/>
                                            <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="NUMERO_SUNL_ANTERIOR" HeaderText="Numero SUNL Anterior" FooterStyle-CssClass="alinearSubTitulos"/>
                                            <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="NOMBRE_CIENTIFICO" HeaderText="Nombre Especie" FooterStyle-CssClass="alinearSubTitulos"/>
                                            <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="VOLUMEN_INGRESAR" HeaderText="Cantidad ingresada" FooterStyle-CssClass="alinearSubTitulos"/>
                                            <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="SALDO_DISPONIBLE" HeaderText="Saldo Disponible" FooterStyle-CssClass="alinearSubTitulos"/>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div style="overflow: scroll; width: 100%; height: 100%">
                                    <asp:GridView ID="GrvValidacionGeneral" AllowPaging="false" ShowFooter="false" runat="server" SkinID="GrillaCoordenadas" AutoGenerateColumns="false" Width="100%">
                                        <Columns>
                                            <asp:BoundField HeaderStyle-CssClass="alinearSubTitulos" DataField="DESCRIPCION" HeaderText="Detalle Inconsistencia" FooterStyle-CssClass="alinearSubTitulos" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                <asp:Button ID="Salir" runat="server" Text="Salir" OnClick="Salir_Click" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

    </div>
    <script src="../Scripts/jquery-1.9.1.js"></script>
    <script src="../js/datimepicker-master/build/jquery.datetimepicker.full.js"></script>
    <script src="../jquery/jquery.numeric.js"></script>
    <script src="../js/AprobacionSalvoconducto.js"></script>
</asp:Content>
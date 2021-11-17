<%@ Page Language="C#" MasterPageFile="~/plantillas/Silpa.master" AutoEventWireup="true" CodeFile="QuejasDenuncias.aspx.cs" 
    Title="Presente su Denuncia Ambiental" Inherits="QuejasDenuncias_QuejasDenuncias" 
    MaintainScrollPositionOnPostback="true" ValidateRequest ="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/controles/cltCaptcha.ascx" TagPrefix="cpt" TagName="Captcha" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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

    <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" Text="DENUNCIAS AMBIENTALES" SkinID="titulo_principal"></asp:Label>
    </div>
            
    <%--<div class="div-contenido">--%>
    <div class="table-responsive">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <%--<table class="tabla_estruct">--%>
                <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                    <tr>
                        <td colspan="2" class="titleUpdate" style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="IdUser" runat="server" Visible="false" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="subtitulo-doble-linea" style="text-align: left; border: 0px solid #ddd !important;">Presente su Denuncia Ambiental</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblDescripcionQueja" runat="server" SkinID="etiqueta_negra" Text="Describa su denuncia:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:TextBox ID="txtDescripcionQueja" runat="server" 
                                Rows="5" Width="100%" 
                                TextMode="MultiLine" 
                                style="resize: none;"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDescripcionQueja" runat="server" Display="Dynamic" ControlToValidate="txtDescripcionQueja"
                                ErrorMessage="Debe ingresar la descripción de la queja">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: left; border: 0px solid #ddd !important;">
                            <table style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
                                <tr>
                                    <td style="max-width: 560px; text-align: left; border: 0px solid #ddd !important;">
                                        <asp:FileUpload ID="uplAdjuntarArchivo" runat="server" Width="100%" style="max-width: 560px;" />
                                    </td>
                                    <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                                        <asp:Button ID="btnAdjuntar" runat="server" 
                                            Text="Adjuntar" Width="120px" 
                                            CausesValidation="False" 
                                            OnClick="btnAdjuntar_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="max-width: 560px; text-align: left; border: 0px solid #ddd !important;">
                                        <asp:ListBox ID="lstListaArchivos" runat="server" Height="100px" Width="100%" style="max-width: 560px;"></asp:ListBox>
                                    </td>
                                    <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                                        <asp:Button ID="btnEliminar" runat="server" 
                                            Text="Eliminar" Width="120px" 
                                            CausesValidation="False" 
                                            OnClick="btnEliminar_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="border: 0px solid #ddd !important;">
                                        <asp:Label ID="lblMensajeArchivos" runat="server" Text="Label" SkinID="etiqueta_roja_error" Visible="False"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                    <tr>
                        <td colspan="2" class="subtitulo-doble-linea" style="text-align: left; border: 0px solid #ddd !important;">Lugar de Ocurrencia de los Hechos</td>
                    </tr>
                    <tr>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblDireccionDescripcion" runat="server" SkinID="etiqueta_negra" Text="Dirección o Descripción del Sitio"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:TextBox ID="txtDireccionDescripcion" runat="server" SkinID="texto"></asp:TextBox>&nbsp;
                            <asp:RequiredFieldValidator ID="rfvDireccionDescripcion" runat="server" ControlToValidate="txtDireccionDescripcion"
                                ErrorMessage="Ingrese la dirección o descripción del Sitio">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblDepartamentoQueja" runat="server" SkinID="etiqueta_negra" Text="Departamento:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:DropDownList ID="cboDepartamentoQueja" runat="server" 
                                SkinID="lista_desplegable" AutoPostBack="True" 
                                OnSelectedIndexChanged="cboDepartamentoQueja_SelectedIndexChanged">
                            </asp:DropDownList>&nbsp;
                            <asp:CompareValidator ID="covDepartamentoQueja" runat="server" ControlToValidate="cboDepartamentoQueja"
                                Display="Dynamic" ErrorMessage="Seleccione el Departamento de la Queja" Operator="NotEqual"
                                ValueToCompare="-1">*</asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblMunicipioQuejas" runat="server" SkinID="etiqueta_negra" Text="Municipio:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:DropDownList ID="cboMunicipioQueja" runat="server" 
                                SkinID="lista_desplegable" AutoPostBack="True" 
                                OnSelectedIndexChanged="cboMunicipioQueja_SelectedIndexChanged">
                            </asp:DropDownList>&nbsp;
                            <asp:CompareValidator ID="covMunicipioQueja" runat="server" ControlToValidate="cboMunicipioQueja"
                                Display="Dynamic" ErrorMessage="Seleccione el Municipio de la Queja" Operator="NotEqual"
                                ValueToCompare="-1">*</asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblZonaQueja" runat="server" SkinID="etiqueta_negra" Text="Zona:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:DropDownList ID="cboZonaQueja" runat="server" SkinID="lista_desplegable">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblCorregimientoQueja" runat="server" SkinID="etiqueta_negra" Text="Corregimiento:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:DropDownList ID="cboCorregimientoQueja" runat="server" SkinID="lista_desplegable" 
                                OnSelectedIndexChanged="cboCorregimientoQueja_SelectedIndexChanged" 
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblVeredaQueja" runat="server" SkinID="etiqueta_negra" Text="Vereda:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:DropDownList ID="cboVeredaQueja" runat="server" SkinID="lista_desplegable">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblCuencaQueja" runat="server" SkinID="etiqueta_negra" Text="Cuenca:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; width: 100%;">
                                <tr>
                                    <td style="width: 16%; text-align: left; border: 0px solid #ddd !important;">
                                        <asp:Label ID="lblAreaHidrografica" runat="server" SkinID="etiqueta_negra" Text="Área Hidrográfica:"></asp:Label>
                                    </td>
                                    <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                                        <asp:DropDownList ID="cboAreaHidrografica" runat="server" 
                                            SkinID="lista_desplegable" AutoPostBack="True" 
                                            OnSelectedIndexChanged="cboAreaHidrografica_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 16%; text-align: left; border: 0px solid #ddd !important;">
                                        <asp:Label ID="lblZonaHidrografica" runat="server" SkinID="etiqueta_negra" Text="Zona Hidrográfica:"></asp:Label>
                                    </td>
                                    <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                                        <asp:DropDownList ID="cboZonaHidrografica" runat="server" 
                                            SkinID="lista_desplegable" AutoPostBack="True" 
                                            OnSelectedIndexChanged="cboZonaHidrografica_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 16%; text-align: left; border: 0px solid #ddd !important;">
                                        <asp:Label ID="lblSubZonaHidrografica" runat="server" SkinID="etiqueta_negra" Text="Sub-Zona Hidrográfica:"></asp:Label>
                                    </td>
                                    <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                                        <asp:DropDownList ID="cboSubZonaHidrografica" runat="server" 
                                            SkinID="lista_desplegable">
                                        </asp:DropDownList>
                                    </td>                                
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblAutoridadQueja" runat="server" SkinID="etiqueta_negra" Text="Autoridad Ambiental:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <div style="font-size: 11px; text-align: justify;">Recuerde que, en principio, la entidad autorizada 
                                por la ley para conocer las quejas o denuncias por presuntas infracciones ambientales, 
                                es la autoridad ambiental regional o distrital con competencia en el lugar de ocurrencia de los hechos. 
                                Los casos en que la presunta infracción ambiental consista en la violación de una norma o la causación de 
                                un daño relacionada con licencias, permisos o autorizaciones que le corresponda otorgar o negar al 
                                Ministerio de Ambiente, Vivienda y Desarrollo Territorial, éste será competente para conocer de las 
                                quejas o denuncias respectivas</div>
                            <br />
                            <asp:DropDownList ID="cboAutoridadQueja" runat="server" SkinID="lista_desplegable">
                            </asp:DropDownList>&nbsp;
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="cboAutoridadQueja"
                                Display="Dynamic" ErrorMessage="Seleccione la autoridad Ambiental" Operator="NotEqual"
                                ValueToCompare="-1">*</asp:CompareValidator>
                        </td>
                    </tr>
                    <%--<tr>
                        <td style="width: 14%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblSectorQueja" runat="server" SkinID="etiqueta_negra" Text="Sector:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:DropDownList ID="cboSectorQueja" runat="server" SkinID="lista_desplegable">
                            </asp:DropDownList>
                        </td>
                    </tr>--%>
                    <tr>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblCoordenadasQueja" runat="server" SkinID="etiqueta_negra" Text="Coordenadas:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <table style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
                                <tr>
                                    <td colspan="2" style="border: 0px solid #ddd !important;">
                                        <asp:Label ID="lblCoordenadaX" runat="server" SkinID="etiqueta_negra" Text="X:"></asp:Label>                        
                                        <asp:TextBox ID="txtCoordenadaX" runat="server" SkinID="texto_corto" MaxLength="12" style="text-align: right"></asp:TextBox>
                                        <asp:Label ID="lblCoordenadaY" runat="server" SkinID="etiqueta_negra" Text="Y:"></asp:Label>
                                        <asp:TextBox ID="txtCoordenadaY" runat="server" SkinID="texto_corto" MaxLength="12" style="text-align: right"></asp:TextBox>
                                        <cc1:MaskedEditExtender ID="meeCoordenadaX" runat="server" TargetControlID="txtCoordenadaX" 
                                            Mask="9999999.99" MessageValidatorTip="true" 
                                            OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" 
                                            MaskType="Number" InputDirection="RightToLeft" ErrorTooltipEnabled="True"/>
                                        <cc1:MaskedEditExtender ID="meeCoordenadaY" runat="server" TargetControlID="txtCoordenadaY" 
                                            Mask="9999999.99" MessageValidatorTip="true" 
                                            OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" 
                                            MaskType="Number" InputDirection="RightToLeft"  ErrorTooltipEnabled="True" />
                                        <asp:Button ID="btnAgregarCoordenada" runat="server" 
                                            CausesValidation="False" 
                                            OnClick="btnAgregarCoordenada_Click" 
                                            Text="Agregar Coordenada" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 220px; text-align: left; border: 0px solid #ddd !important;">
                                        <asp:ListBox ID="lstCoordenadas" runat="server" Rows="5" Width="220px"></asp:ListBox><br />
                                    </td>
                                    <td style="text-align: left; padding-left: 5px; border: 0px solid #ddd !important;">
                                        <asp:Button ID="btnEliminarCoordenada" runat="server" 
                                            CausesValidation="False" 
                                            OnClick="btnEliminarCoordenada_Click" 
                                            Text="Eliminar Coordenada" /><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="border: 0px solid #ddd !important;">
                                        <asp:Label ID="lblMensajeCoordenada" runat="server" 
                                            ForeColor="Red" SkinID="etiqueta_roja_error"
                                            Visible="False"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 18%; text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblRecursoQueja" runat="server" SkinID="etiqueta_negra" Text="Recurso Presuntamente Afectado:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:CheckBoxList ID="chkRecursosQueja" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>Flora</asp:ListItem>
                                <asp:ListItem>Fauna</asp:ListItem>
                                <asp:ListItem>Aire</asp:ListItem>
                                <asp:ListItem>Agua</asp:ListItem>
                                <asp:ListItem>Suelo</asp:ListItem>
                                <asp:ListItem>Paisaje</asp:ListItem>
                                <asp:ListItem>Otro</asp:ListItem>
                            </asp:CheckBoxList>
                            <asp:Label ID="lblCualRecurso" runat="server" SkinID="etiqueta_negra" Text="¿Cuál?"></asp:Label>
                            <asp:TextBox ID="txtOtroRecurso" runat="server" SkinID="texto_corto"></asp:TextBox>
                            <asp:CustomValidator ID="cvaOtroRecurso" runat="server" Display="Dynamic" 
                                ErrorMessage='Si seleccionó "Otro" debe ingresar cual es el otro recurso'
                                OnServerValidate="CustomValidator1_ServerValidate">*</asp:CustomValidator>
                        </td>
                    </tr>
                </table>
                <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                    <tr>
                        <td colspan="2" class="subtitulo-doble-linea" style="text-align: left; border: 0px solid #ddd !important;">Datos del Presunto Infractor </td>
                    </tr>
                    <tr>                
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="Label2" runat="server" SkinID="etiqueta_negra" Text="Tipo Persona:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:DropDownList ID="cboTipoPersona" runat="server" SkinID="lista_desplegable" AutoPostBack="True" OnSelectedIndexChanged="cboTipoPersona_SelectedIndexChanged">
                                <asp:ListItem Value="0">Seleccione ...</asp:ListItem>
                                <asp:ListItem Value="1">Persona Natural</asp:ListItem>
                                <asp:ListItem Value="2">Persona Jur&#237;dica</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>                
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblPrimerNombreInfractor" runat="server" SkinID="etiqueta_negra" Text="Primer Nombre:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:TextBox ID="txtPrimerNombreInfractor" MaxLength="59" runat="server" SkinID="texto"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblSegundoNombreInfractor" runat="server" 
                                SkinID="etiqueta_negra" Text="Segundo Nombre:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:TextBox ID="txtSegundoNombreInfractor" runat="server" 
                                MaxLength="59" SkinID="texto"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblPrimerApellidoInfractor" runat="server" SkinID="etiqueta_negra"
                                Text="Primer Apellido:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:TextBox ID="txtPrimerApellidoInfractor" MaxLength="59" runat="server" SkinID="texto"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblSegundoApellidoInfractor" runat="server" SkinID="etiqueta_negra"
                                Text="Segundo Apellido:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:TextBox ID="txtSegundoApellidoInfractor" MaxLength="59" runat="server" SkinID="texto"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblTipoIdentificacionInfractor" runat="server" SkinID="etiqueta_negra"
                                Text="Tipo de Identificación:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:DropDownList ID="cboTipoIdentificacionInfractor" runat="server" 
                                SkinID="lista_desplegable" AutoPostBack="True" 
                                OnSelectedIndexChanged="cboTipoIdentificacionInfractor_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblIdentificacionInfractor" runat="server" SkinID="etiqueta_negra"
                                Text="Nº:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:TextBox ID="txtIdentificacionInfractor" runat="server" SkinID="texto" MaxLength="11"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblOrigenIdentificacionInfractor" runat="server" 
                                SkinID="etiqueta_negra" Text="De:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:DropDownList ID="cboDepartamentoOrigen" runat="server" 
                                AutoPostBack="True" SkinID="lista_desplegable" 
                                OnSelectedIndexChanged="cboDepartamentoOrigen_SelectedIndexChanged">
                            </asp:DropDownList>   -   
                            <asp:DropDownList ID="cboMunicipioOrigen" runat="server" SkinID="lista_desplegable">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblDireccionInfractor" runat="server" 
                                SkinID="etiqueta_negra" Text="Dirección:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:TextBox ID="txtDireccionInfractor" MaxLength="100" runat="server" SkinID="texto"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblPaisInfractor" runat="server" SkinID="etiqueta_negra"
                                Text="País:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:DropDownList ID="cboPaisInfractor" runat="server" AutoPostBack="True" SkinID="lista_desplegable" OnSelectedIndexChanged="cboPaisInfractor_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblDepartamentoInfractor" runat="server" 
                                SkinID="etiqueta_negra" Text="Departamento:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:DropDownList ID="cboDepartamentoInfractor" runat="server" 
                                AutoPostBack="True" SkinID="lista_desplegable" 
                                OnSelectedIndexChanged="cboDepartamentoInfractor_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblMunicipioInfractor" runat="server" SkinID="etiqueta_negra" Text="Municipio:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:DropDownList ID="cboMunicipioInfractor" runat="server" 
                                SkinID="lista_desplegable" AutoPostBack="True" 
                                OnSelectedIndexChanged="cboMunicipioInfractor_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="text-align: left; border: 0px solid #ddd !important;">
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblCorregimientoInfractor" runat="server" 
                                Text="Corregimiento:" SkinID="etiqueta_negra"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:DropDownList ID="cboCorregimientoInfractor" runat="server" 
                                SkinID="lista_desplegable" AutoPostBack="True" 
                                OnSelectedIndexChanged="cboCorregimientoInfractor_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblVeredaInfractor" runat="server" Text="Vereda:" SkinID="etiqueta_negra"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:DropDownList ID="cboVeredaInfractor" runat="server" SkinID="lista_desplegable">
                            </asp:DropDownList>
                        </td>
                    </tr>                
                    <tr>
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblTelefonoInfractor" runat="server" 
                                SkinID="etiqueta_negra" Text="Teléfono(s):"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:TextBox ID="txtTelefonoInfractor" MaxLength="50" runat="server" SkinID="texto"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table id="tb_Quejoso" runat="server" visible="true" border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                    <tr>
                        <td colspan="2" class="subtitulo-doble-linea" style="text-align: left; border: 0px solid #ddd !important;">Identificación del Quejoso o Denunciante</td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblPrimerNombreQuejoso" runat="server" SkinID="etiqueta_negra"
                                Text="Primer Nombre:" ></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:TextBox ID="txtPrimerNombreQuejoso" MaxLength="59" runat="server" SkinID="texto"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblSegundoNombreQuejoso" runat="server" SkinID="etiqueta_negra"
                                Text="Segundo Nombre:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:TextBox ID="txtSegundoNombreQuejoso" MaxLength="59" runat="server" SkinID="texto"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblPrimerApellidoQuejoso" runat="server" SkinID="etiqueta_negra"
                                Text="Primer Apellido:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:TextBox ID="txtPrimerApellidoQuejoso" MaxLength="59" runat="server" SkinID="texto"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblSegundoApellidoQuejoso" runat="server" SkinID="etiqueta_negra"
                                Text="Segundo Apellido:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:TextBox ID="txtSegundoApellidoQuejoso" MaxLength="59" runat="server" SkinID="texto"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblTipoIdentificacionDenunciante" runat="server" SkinID="etiqueta_negra"
                                Text="Tipo de Identificación:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:DropDownList ID="cboTipoIdentificacionDenunciante" runat="server" 
                                SkinID="lista_desplegable" AutoPostBack="True" 
                                OnSelectedIndexChanged="cboTipoIdentificacionDenunciante_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblIdentificacionDenunciante" runat="server" SkinID="etiqueta_negra"
                                Text="Nº:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:TextBox ID="txtIdentificacionDenunciante"  runat="server" SkinID="texto" MaxLength="11"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblOrigenIdentificacionDenunciante" runat="server" 
                                SkinID="etiqueta_negra" Text="De:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:DropDownList ID="cboDepartamentoOrigenDenunciante" runat="server" 
                                AutoPostBack="True" SkinID="lista_desplegable" 
                                OnSelectedIndexChanged="cboDepartamentoOrigenDenunciante_SelectedIndexChanged">
                            </asp:DropDownList>   -   
                            <asp:DropDownList ID="cboMunicipioOrigenDenunciante" runat="server" SkinID="lista_desplegable">
                            </asp:DropDownList>
                        </td>
                    </tr>                                          
                    <tr>
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblDireccionQuejoso" runat="server" 
                                SkinID="etiqueta_negra" Text="Dirección:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:TextBox ID="txtDireccionQuejoso" MaxLength="100" runat="server" SkinID="texto"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblPaisQuejoso" runat="server" 
                                SkinID="etiqueta_negra" Text="País:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:DropDownList ID="cboPaisQuejoso" runat="server" 
                                AutoPostBack="True" SkinID="lista_desplegable" 
                                OnSelectedIndexChanged="cboPaisQuejoso_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblDepartamentoQuejoso" runat="server" 
                                SkinID="etiqueta_negra" Text="Departamento:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:DropDownList ID="cboDepartamentoQuejoso" runat="server" 
                                AutoPostBack="True" SkinID="lista_desplegable" 
                                OnSelectedIndexChanged="cboDepartamentoQuejoso_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblMunicipioQuejoso" runat="server" SkinID="etiqueta_negra" Text="Municipio:"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:DropDownList ID="cboMunicipioQuejoso" runat="server" 
                                SkinID="lista_desplegable" AutoPostBack="True" 
                                OnSelectedIndexChanged="cboMunicipioQuejoso_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblCorregimientoQuejoso" runat="server" Text="Corregimiento:" SkinID="etiqueta_negra"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:DropDownList ID="cboCorregimientoQuejoso" runat="server" 
                                SkinID="lista_desplegable" AutoPostBack="True" 
                                OnSelectedIndexChanged="cboCorregimientoQuejoso_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblVeredaQuejoso" runat="server" Text="Vereda:" SkinID="etiqueta_negra"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:DropDownList ID="cboVeredaQuejoso" runat="server" SkinID="lista_desplegable">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblCorreoQuejoso" runat="server" Text="Correo Electrónico:" SkinID="etiqueta_negra"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:TextBox ID="txtCorreoQuejoso" MaxLength="200" runat="server" SkinID="texto"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revCorreoQuejoso" runat="server" 
                                ControlToValidate="txtCorreoQuejoso"
                                Display="Dynamic" 
                                ErrorMessage="El formato de correo electrónico no es válido" 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">*</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left; border: 0px solid #ddd !important;">
                            <asp:Label ID="lblTelefonoQuejoso" runat="server" SkinID="etiqueta_negra"
                                Text="Teléfono(s):"></asp:Label>
                        </td>
                        <td style="text-align: left; padding-left: 10px; border: 0px solid #ddd !important;">
                            <asp:TextBox ID="txtTelefonoQuejoso" MaxLength="50" runat="server" SkinID="texto"></asp:TextBox>
                        </td>
                    </tr>
                    <tr runat="server" id="trCaptcha">
                        <td colspan="2">
                            <cpt:Captcha runat="server" ID="ctrCaptcha" />
                        </td>
                    </tr>
                </table>
                <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                    <tr>
                        <td colspan="2" style="text-align: center; border: 0px solid #ddd !important;">
                            <asp:BulletedList ID="blsError" runat="server" ForeColor="Red" Visible="False">
                            </asp:BulletedList>
                            <br />
                            <asp:ValidationSummary ID="valResumenQueja" runat="server" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>

        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
            <tr>
                <td style="text-align: center;">
                    <asp:UpdatePanel ID="upnlAccionesBoton" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btnEnviarQueja" runat="server" Text="Enviar" OnClick="btnEnviarQueja_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCancelarQueja" runat="server" Text="Cancelar" OnClick="btnCancelarQueja_Click" CausesValidation="false" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>

    </div>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upnlAccionesBoton">
        <ProgressTemplate>  
            <div id="ModalProgressContainer">
                <div>
                    <p>Procesando...</p>
                    <p><asp:Image ID="imgUpdateProgress" runat="server" SkinId="procesando"/></p>
                </div>
            </div>                         
        </ProgressTemplate>
    </asp:UpdateProgress>

</asp:Content>
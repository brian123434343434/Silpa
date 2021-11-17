<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPASinMenuFlash.master" AutoEventWireup="true" CodeFile="ReporteSalvoconductosEmitidos.aspx.cs" Inherits="Salvoconducto_ReporteSalvoconductosEmitidos" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" runat="Server">
   <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/jquery.datetimepicker.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    <script runat="server">
    </script>

    <style type="text/css">
        table {
            /*border: 1px solid #000;*/
        }

            table tr td {
                border: 0px solid #ddd !important;
                padding: 4px;
            }

        .Button {
            background-color: #ddd;
        }

        .CentrarTexto {
            text-align: center;
        }

        .FondoAplicacion {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .FormatoTexto {
            font-weight: bold;
            color: #31708f;
            border-color: #bce8f1;
            width: 50px;
            text-align: left;
            vertical-align: middle;
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
            font-weight: bold;
            color: #31708f;
            border-color: #bce8f1;
            background-color: #d9edf7;
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

        .CajaDialogo {
            background-color: #fff;
            border-width: 1px;
            border-style: outset;
            border-color: Yellow;
            padding: 0px;
        }
    </style>

    <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" Text="Consultar Salvoconductos Emitidos" SkinID="titulo_principal_blanco"></asp:Label>
        &nbsp;
        <a href="#" id="hrCerrarVentana" visible="false" runat="server" onclick="window.close();return false;">Salir</a>
    </div>

    <%--<div class="div-contenido">--%>
    <div class="table-responsive">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableHistory="false">
        </asp:ScriptManager>

        <div class="contact_form" id="dContactForm">
            <asp:UpdatePanel runat="server" ID="UpdDepartmanetoDestino">
                <ContentTemplate>
                    <table width="90%" border="0">
                        <tr>
                            <td class="FormatoTexto" style="text-align: left; vertical-align: middle;">
                                <label for="CboAutoridadAmbiental" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 200px;">Autoridad Ambiental:</label>
                            </td>
                            <td style="text-align: left; vertical-align: middle;" colspan="4">
                                <asp:DropDownList ID="CboAutoridadAmbiental" runat="server" Enabled="false"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="FormatoTexto">
                                <label for="CboTipoSalvoconducto" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 200px;">Tipo Salvoconducto:</label>
                            </td>
                            <td style="text-align: left; vertical-align: middle;" colspan="4">
                                <asp:DropDownList ID="CboTipoSalvoconducto" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvTipoSalvoconducto" runat="server" ControlToValidate="CboTipoSalvoconducto" ValidationGroup="consulta" ErrorMessage="Debe seleccionar un tipo de salvoconducto">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="FormatoTexto">
                                <label for="CboClaseSalvoconducto" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 200px;">Clase Salvoconducto:</label>
                            </td>
                            <td style="text-align: left; vertical-align: middle;" colspan="4">
                                <asp:DropDownList ID="CboClaseSalvoconducto" runat="server" OnSelectedIndexChanged="CboClaseSalvoconducto_SelectedIndexChanged" AutoPostBack="true" >
                                    <asp:ListItem Selected="True" Value="1">Estandar</asp:ListItem>
                                    <asp:ListItem Value="2">SUNL pre-impresos</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVCboClaseSalvoconducto" runat="server" ControlToValidate="CboClaseSalvoconducto" ValidationGroup="consulta" ErrorMessage="Debe seleccionar una Clase de salvoconducto">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td class="FormatoTexto" style="text-align: left">
                                <label for="CboClaseRecurso" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 200px;">Clase de Recurso:</label>
                            </td>
                            <td style="text-align: left; vertical-align: middle;" colspan="4">
                                <asp:DropDownList ID="CboClaseRecurso" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="FormatoTexto" style="text-align: left; vertical-align: middle;">
                                <label for="TxtNumeroSalvoconducto" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 200px;">Numero de Salvoconducto:</label>
                            </td>
                            <td style="text-align: left; vertical-align: middle;" colspan="4">
                                <asp:TextBox ID="TxtNumeroSalvoconducto" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="FormatoTexto" style="text-align: left; vertical-align: middle;">
                                <label for="CboClaseRecurso" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 200px;">Titular del Salvoconducto:</label>
                            </td>
                            <td style="text-align: left; vertical-align: middle;" colspan="4">
                                <asp:UpdatePanel ID="upnlSolicitante1" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtSolicitante" runat="server" placeholder="Nombre Titular" Width="235px" Enabled="false" /><asp:HiddenField ID="hfIdSolicitante" runat="server" />
                                        <asp:LinkButton ID="lnkSolicitante" runat="server" Text="Agregar Titular" OnClick="lnkSolicitante_Click" CssClass="a_green"></asp:LinkButton>
                                        &nbsp
                                        <asp:LinkButton ID="LnkLimpiarSolicitante" runat="server" Text="Quitar Titular" OnClick="LnkLimpiarSolicitante_Click" CssClass="a_red"></asp:LinkButton>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="lnkSolicitante" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="FormatoTexto" style="text-align: left; vertical-align: middle;">
                                <label for="TxtNumeroSalvoconducto" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 200px;">Estado Vigencia Salvocondcuto:</label>
                            </td>
                            <td style="text-align: left; vertical-align: middle;">
                                <asp:RadioButton ID="ChkVigentes" Text="" runat="server" GroupName="ValidarTipoVigencias" Checked="True" />
                                Vigentes
                            </td>
                            <td style="text-align: left; vertical-align: middle;">
                                <asp:RadioButton ID="ChkNoVigentes" runat="server" GroupName="ValidarTipoVigencias" />
                                No Vigentes
                            </td>
                            <td style="text-align: left; vertical-align: middle;">
                                <asp:RadioButton ID="ChkTodos" runat="server" GroupName="ValidarTipoVigencias" />
                                Todos
                            </td>
                        </tr>
                        <tr>
                            <td class="FormatoTexto" style="text-align: left; vertical-align: middle;">
                                <label for="cboEstadoSalvoconducto" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 200px;">Estado Salvocondcuto:</label>
                            </td>
                            <td style="text-align: left; vertical-align: middle;" colspan="2">
                               <asp:DropDownList ID="cboEstadoSalvoconducto" runat="server">
                                   <asp:ListItem Text="Todos" Value=""></asp:ListItem>
                                   <asp:ListItem Text="Solicitud" Value="1"></asp:ListItem>
                                   <asp:ListItem Text="Emitido" Value="2"></asp:ListItem>
                                   <asp:ListItem Text="Negado" Value="3"></asp:ListItem>
                                   <asp:ListItem Text="Bloqueado" Value="4"></asp:ListItem>
                               </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; vertical-align: middle;">
                                <label for="CboClaseRecurso" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 200px;">Fecha de Expedicion</label>
                            </td>
                            <td style="text-align: left; vertical-align: middle; width: 100px;">Desde:
                            </td>
                            <td>
                                <asp:TextBox ID="TxtFecExpDesde" runat="server" Width="130px" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvFechaexpDesde" runat="server" ControlToValidate="TxtFecExpDesde" ValidationGroup="consulta" ErrorMessage="Debe seleccionar una fecha desde de expedición">*</asp:RequiredFieldValidator>
                            </td>
                            <td style="text-align: left; vertical-align: middle; width: 100px;">Hasta:
                            </td>
                            <td>
                                <asp:TextBox ID="TxtFecExpHasta" runat="server" Width="130px" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvFechaexpHasta" runat="server" ControlToValidate="TxtFecExpHasta" ValidationGroup="consulta" ErrorMessage="Debe seleccionar una fecha hasta de expedición">*</asp:RequiredFieldValidator>
                                <asp:LinkButton ID="LnkLimpiarFechas" runat="server" Text="Remover Fechas" OnClick="LnkLimpiarFechas_Click" CssClass="a_red"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left; vertical-align: middle;">
                                <label for="CboDepartamentoOrigen" id="LblUbicacionOrigen" runat="server" visible="true" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 200px;">Ubicacion de Origen</label>
                                <label for="CboDepartamentoOrigen" id="LblProcedencia" runat="server" visible="false" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 200px;">Ubicacion Procedencia</label>
                            </td>
                            <td style="text-align: left; vertical-align: middle;">Departamento:
                            </td>
                            <td>
                                <asp:DropDownList ID="CboDepartamentoOrigen" runat="server" Width="145px" AutoPostBack="true" OnSelectedIndexChanged="CboDepartamentoOrigen_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td style="text-align: left; vertical-align: middle;">Municipio:
                            </td>
                            <td>
                                <asp:DropDownList ID="CboMunicipioOrigen" runat="server" Width="145px"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trUbicacionDestino" runat="server" >
                            <td style="text-align: left; vertical-align: middle;">
                                <label for="CboDptoMunicipioOrigen" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 200px;">Ubicacion de Destino</label>
                            </td>
                            <td style="text-align: left; vertical-align: middle;">Departamento:
                       
                            </td>
                            <td>
                                <asp:DropDownList ID="CboDepartamentoDestino" runat="server" Width="145px" AutoPostBack="true" OnSelectedIndexChanged="CboDepartamentoDestino_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td style="text-align: left; vertical-align: middle;">Municipio:
                            </td>
                            <td>
                                <asp:DropDownList ID="CboMunicipioDestino" runat="server" Width="145px"></asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="5">
                                <asp:UpdatePanel ID="upnlAccionesBoton" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="BtnConsultar" SkinID="boton_copia" runat="server" Text="Consultar" ValidationGroup="consulta" OnClick="BtnConsultar_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <asp:ValidationSummary runat="server" ValidationGroup="consulta" DisplayMode="BulletList" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <table border="1" width="100%">
                <tr>
                    <td style="overflow-x: scroll;">
                        <asp:UpdatePanel ID="UpnlGrvSalvoconductosRes438" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="GrvSalvoconductosRes438" runat="server" AutoGenerateColumns="false" ShowFooter="false" AllowPaging="True" AllowSorting="True" 
                                    OnPageIndexChanging="GrvSalvoconductosRes438_PageIndexChanging" OnRowCommand="GrvSalvoconductosRes438_RowCommand" PageSize="20" SkinID="GrillaPDV" EmptyDataText="" >
                                    <PagerStyle BackColor="LightGray" Height="30px" VerticalAlign="Bottom" HorizontalAlign="Center" />
                                    <PagerSettings Mode="Numeric" Position="Bottom" PageButtonCount="10" />
                                    <HeaderStyle Font-Size="9pt" />
                                    <FooterStyle Font-Size="9pt" />
                                    <RowStyle Font-Size="9pt" />
                                    <SelectedRowStyle Font-Size="9pt" />
                                    <EditRowStyle Font-Size="9pt" />
                                    <AlternatingRowStyle Font-Size="9pt" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Tipo Salvoconducto">
                                            <ItemTemplate>
                                                <asp:Label ID="LblTipoSalvoconducto" runat="server" Text='<%# Bind("TIPO_SALVOCONDUCTO") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Vigencia.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVigencia" runat="server" Text='<%# Bind("VIGENCIA") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Fecha Registro">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFechaRegistro" runat="server" Text='<%# Bind("FECHA_REGISTRO") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Fecha Expedicion">
                                            <ItemTemplate>
                                                <asp:Label ID="LblFechaExp" runat="server" Text='<%# Bind("FECHA_EXPEDICION") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Vigencia Desde">
                                            <ItemTemplate>
                                                <asp:Label ID="LblFecIniVigencia" runat="server" Text='<%# Bind("FECHA_INI_VIGENCIA") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Vigencia Hasta">
                                            <ItemTemplate>
                                                <asp:Label ID="LblFecFinVigencia" runat="server" Text='<%# Bind("FECHA_FIN_VIGENCIA") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Numero Salvoconducto" >
                                            <ItemTemplate>
                                                <asp:Label ID="LblNumeroSalvoconducto" runat="server" Text='<%# Bind("NUMERO") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Estado">
                                            <ItemTemplate>
                                                <asp:Label ID="LblEstadoSalvoconducto" runat="server" Text='<%# Bind("ESTADO") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Nombre Solicitante">
                                            <ItemTemplate>
                                                <asp:Label ID="LblNombreSolicitante" runat="server" Text='<%# Bind("NOMBRE_SOLICITANTE") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Clase Recurso">
                                            <ItemTemplate>
                                                <asp:Label ID="LblClaseRecurso" runat="server" Text='<%# Bind("CLASE_RECURSO") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dpto procedencia">
                                            <ItemTemplate>
                                                <asp:Label ID="LblDptoProcedencia" runat="server" Text='<%# Bind("DEPARTAMENTO_PROCEDENCIA") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Municipio Procedencia">
                                            <ItemTemplate>
                                                <asp:Label ID="LblMunProcedencia" runat="server" Text='<%# Bind("MUNICIPIO_PROCEDENCIA") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Forma Otorgamiento">
                                            <ItemTemplate>
                                                <asp:Label ID="LblFormaOtorgamiento" runat="server" Text='<%# Bind("FORMA_OTORGAMIENTO") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Modo Adquisicion">
                                            <ItemTemplate>
                                                <asp:Label ID="LblModoAdquisicion" runat="server" Text='<%# Bind("MODO_ADQUISICION") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Finalidad">
                                            <ItemTemplate>
                                                <asp:Label ID="LblFinalidad" runat="server" Text='<%# Bind("FINALIDAD") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Autoridad Cargue">
                                            <ItemTemplate>
                                                <asp:Label ID="LblAutoridadCargue" runat="server" Text='<%# Bind("AUTORIDAD_AMBIENTAL_CARGUE") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Autoridad Emisora">
                                            <ItemTemplate>
                                                <asp:Label ID="LblAutoridadEmisora" runat="server" Text='<%# Bind("AUTORIDAD_AMBIENTAL_EMISORA") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Autoridad Otorga">
                                            <ItemTemplate>
                                                <asp:Label ID="LblAutoridadOtorga" runat="server" Text='<%# Bind("AUTORIDAD_AMBIENTAL_OTORGA") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ShowHeader="false" ItemStyle-Width="200px">
                                            <ItemTemplate>
                                                <table style="text-align: left; vertical-align: top;" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:HyperLink ID="HypVerSalvoconducto" runat="server" Target="_blank" NavigateUrl='<%# urlNavegacionVerSalvoconducto(Eval("SALVOCONDUCTO_ID")) %>' CssClass="a_green">Ver Detalles</asp:HyperLink>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="LnkArchivoSalvoconducto" OnClick="LnkArchivoAdjuntoCreacion_Click" runat="server" CommandArgument='<%# Eval("RUTA_ARCHIVO") +","+ Eval("SALVOCONDUCTO_ID") %>' CommandName="DescargarArchivo" Text="Descargar" CssClass="a_orange"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="overflow-x: scroll;" runat="server" >
                        <asp:UpdatePanel ID="UpdgrvConsultaSalvocondcuto" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grvEstadoSalvoconducto" runat="server" AutoGenerateColumns="false" ShowFooter="false" AllowPaging="True" AllowSorting="True" OnRowCommand="grvEstadoSalvoconducto_RowCommand"
                                    OnPageIndexChanging="grvEstadoSalvoconducto_PageIndexChanging" PageSize="20" SkinID="GrillaPDV" EmptyDataText="" OnRowDataBound="grvEstadoSalvoconducto_RowDataBound">
                                    <PagerStyle BackColor="LightGray" Height="30px" VerticalAlign="Bottom" HorizontalAlign="Center" />
                                    <PagerSettings Mode="Numeric" Position="Bottom" PageButtonCount="10" />
                                    <HeaderStyle Font-Size="9pt" />
                                    <FooterStyle Font-Size="9pt" />
                                    <RowStyle Font-Size="9pt" />
                                    <SelectedRowStyle Font-Size="9pt" />
                                    <EditRowStyle Font-Size="9pt" />
                                    <AlternatingRowStyle Font-Size="9pt" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Autoridad Emisora">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAutoridadEmisora" runat="server" Text='<%# Bind("AUTORIDAD_EMISORA") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Usuario Respo.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUsuarioEmision" runat="server" Text='<%# Bind("USUARIO_CAMBIO_ESTADO") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Fecha Generación">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFechaGeneracion" runat="server" Text='<%# Bind("FECHA_CAMBIO_ESTADO") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Estado">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEstado" runat="server" Text='<%# Bind("ESTADO_SALVOCONDUCTO") %>' SkinID="etiqueta_negra"></asp:Label>
                                                <asp:Label ID="lblEstadoID" runat="server" Text='<%# Bind("ESTADO_ID") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Numero Solicitud" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LblSalvocnductoID" runat="server" Text='<%# Bind("SALVOCONDUCTO_ID") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Numero">
                                            <ItemTemplate>
                                                <asp:Label ID="LblNumeroSalvoconducto" runat="server" Text='<%# Bind("NUMERO") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tipo Salvoconducto">
                                            <ItemTemplate>
                                                <asp:Label ID="LblTipoSalvoconducto" runat="server" Text='<%# Bind("TIPO_SALVOCONDUCTO") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Fecha Expedicion">
                                            <ItemTemplate>
                                                <asp:Label ID="LblFecExpedicion" runat="server" Text='<%# Bind("FECHA_EXPEDICION") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Vigencia Desde">
                                            <ItemTemplate>
                                                <asp:Label ID="LblVigenciaDesde" runat="server" Text='<%# Bind("FECHA_INI_VIGENCIA") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Vigencia Hasta">
                                            <ItemTemplate>
                                                <asp:Label ID="LblVigenciaHasta" runat="server" Text='<%# Bind("FECHA_FIN_VIGENCIA") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Titular">
                                            <ItemTemplate>
                                                <asp:Label ID="LblTitular" runat="server" Text='<%# Bind("NOMBRE_SOLICITANTE") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Clase de Recurso">
                                            <ItemTemplate>
                                                <asp:Label ID="LblClaseRecurso" runat="server" Text='<%# Bind("CLASE_RECURSO") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Departamento Origen">
                                            <ItemTemplate>
                                                <asp:Label ID="LblDptoOrigen" runat="server" Text='<%# Bind("DEPARTAMENTO_ORIGEN") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Municipio Origen">
                                            <ItemTemplate>
                                                <asp:Label ID="LblMunOrigen" runat="server" Text='<%# Bind("MUNICIPIO_ORIGEN") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Departamento Destino">
                                            <ItemTemplate>
                                                <asp:Label ID="LblDptoDestino" runat="server" Text='<%# Bind("DEPARTAMENTO_DESTINO") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Municipio Destino">
                                            <ItemTemplate>
                                                <asp:Label ID="LblMunDestino" runat="server" Text='<%# Bind("MUNICIPIO_DESTINO") %>' SkinID="etiqueta_negra"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ShowHeader="false" ItemStyle-Width="200px">
                                            <ItemTemplate>
                                                <table style="text-align: left; vertical-align: top;" border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td>
                                                            <asp:HyperLink ID="HypVerSalvoconducto" runat="server" Target="_blank" NavigateUrl='<%# urlNavegacionVerSalvoconducto(Eval("SALVOCONDUCTO_ID")) %>' CssClass="a_green">Ver Detalles</asp:HyperLink>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="LnkArchivoSalvoconducto" OnClick="LnkArchivoAdjuntoCreacion_Click" runat="server" CommandArgument='<%# Eval("RUTA_ARCHIVO") +","+ Eval("SALVOCONDUCTO_ID") %>' CommandName="DescargarArchivo" Text="Descargar" CssClass="a_orange"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="upnlTotalRegistros" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblTotalRegistros" runat="server" SkinID="etiqueta_negra" CssClass="texto_usuario" ToolTip="Total de registros encontrados" Text="Número total de registros : "></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        
                    </td>
                </tr>
            </table>
            <br />
            <div class="contact_form" runat="server" id="dvExportar">
                <div class="TableButton">
                    <div class="RowButton">
                        <div class="CellButton">
                            <asp:ImageButton ID="imbExportarExcel" runat="server" Width="68px" Height="16px" ToolTip="Exportar listado a MS Excel" BorderWidth="0px" ImageUrl="../App_Themes/Img/exportar_excel.gif" OnClick="imbExportarExcel_Click"  ValidationGroup="consulta"></asp:ImageButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Label ID="lblSolicitante" runat="server" SkinID="etiqueta_negra"></asp:Label>
    <cc1:ModalPopupExtender ID="mpeSolicitantes" runat="server"
        TargetControlID="lblSolicitante"
        PopupControlID="pnlSolicitantes"
        DropShadow="True" Enabled="True" DynamicServicePath=""
        BackgroundCssClass="FondoAplicacion">
    </cc1:ModalPopupExtender>

    <asp:Panel ID="pnlSolicitantes" runat="server" Style="display: none;" CssClass="CajaDialogo">
        <div>
            <asp:UpdatePanel ID="upnlSolicitante" runat="server">
                <ContentTemplate>
                    <table width="400px">
                        <tr>
                            <td>
                                <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Tipo Identificacion:</label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboTipoIdentificacion" runat="server"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvTipoIdentificacion" Display="Dynamic" runat="server" ControlToValidate="cboTipoIdentificacion" ValidationGroup="solicitante">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Identificacion:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNumeroIdentificacion" runat="server" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNumeroIdentificacion" Display="Dynamic" runat="server" ControlToValidate="txtNumeroIdentificacion" ValidationGroup="solicitante">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnBuscarSolicitante" runat="server" Text="Buscar" SkinID="boton_copia" OnClick="btnBuscarSolicitante_Click" ValidationGroup="solicitante" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNombreSolicitante" runat="server" ClientIDMode="Static" SkinID="etiqueta_negra"></asp:Label>
                                <asp:LinkButton ID="lnkSeleccionarSolicitante" runat="server" OnClick="lnkSeleccionarSolicitante_Click" Text="Seleccionar" Visible="false" CssClass="a_green"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnCerrarVinculosActividad" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:Button ID="btnCerrarVinculosActividad" runat="server" Text="Cerrar" CausesValidation="False" SkinID="boton_copia" OnClick="btnCerrarVinculosActividad_Click" />
        </div>
    </asp:Panel>

    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdgrvConsultaSalvocondcuto">
        <ProgressTemplate>
            <div id="ModalProgressContainer">
                <div>
                    <p>Procesando...</p>
                    <p>
                        <asp:Image ID="imgUpdateProgress2" runat="server" SkinID="procesando" /></p>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upnlAccionesBoton">
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

    <script src="../Scripts/jquery-1.9.1.js"></script>
    <script src="../Scripts/jquery.datetimepicker.js"></script>
    <script src="../jquery/jquery.numeric.js"></script>
    <script src="../js/SeriesSalvoConducto.js"></script>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="SeguimientoRutasSalvoconducto.aspx.cs" Inherits="Salvoconducto_SeguiminetoRutasSalvoconducto" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script src="../jquery/jquery.js"></script>
    <link href="../jquery/EstiloGris/jquery-ui.css" rel="stylesheet" />
    <script src="../jquery/EstiloGris/jquery-ui.js"></script>
    <script src="../jquery/jquery.numeric.js"></script>
    <link href="../App_Themes/skin/StyleREDDS.css" rel="stylesheet" />
     <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

   <style type ="text/css">
        .CentrarTexto{
            text-align:center;
        }

        .FormatoTexto{
            font-weight:bold;
            color: #31708f;
            border-color: #bce8f1;
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
        <asp:Label ID="lblTituloPrincipal" runat="server" Text="Seguimiento Salvoconducto" SkinID="titulo_principal_blanco"></asp:Label>
        &nbsp;
    <a href="#" id="hrCerrarVentana" visible="false" runat="server" onclick="window.close();return false;">Salir</a>
    </div>
        <div class="div-contenido">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableHistory ="false">
        </asp:ScriptManager>
            <div class="contact_form" id="dContactForm">
                <asp:UpdatePanel ID="UpdDatosBasicos" runat="server">
                    <ContentTemplate>
                        <table width="90%" border="0">
                            <tr>
                                <td class="FormatoTexto" style="text-align: left; vertical-align: middle; width: 130px">
                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 150px;" for="TxtNumeroIdentificacionRevisor">Identificacion Revisor:</label>
                                </td>
                                <td style="text-align: left; vertical-align: middle; " >
                                    <asp:TextBox ID="TxtNumeroIdentificacionRevisor" runat="server" Width="220px"></asp:TextBox><asp:RequiredFieldValidator ID="RFVIdentificacionRevisor" Display="Dynamic" runat="server" ControlToValidate="TxtNumeroIdentificacionRevisor" ValidationGroup="seguimiento" ErrorMessage="Debe Ingresar Identificacion Revisor">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="FormatoTexto" style="text-align: left; vertical-align: middle; width: 130px">
                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 150px;" for="TxtNombreRevisor">Nombre Revisor:</label>
                                </td>
                                <td style="text-align: left; vertical-align: middle; " >
                                    <asp:TextBox ID="TxtNombreRevisor" runat="server" Width="220px"></asp:TextBox><asp:RequiredFieldValidator ID="RFVNombreRevisor" Display="Dynamic" runat="server" ControlToValidate="TxtNombreRevisor" ValidationGroup="seguimiento" ErrorMessage="Debe Ingresar Nombre Revisor">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="FormatoTexto" style="text-align: left; vertical-align: middle">
                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 150px;" for="CboDptoUbicacion">Departamento Ubicacion:</label>
                                </td>
                                <td style="text-align: left; vertical-align: middle; ">
                                    <asp:DropDownList ID="CboDptoUbicacion" runat="server" Width="230px" OnSelectedIndexChanged="CboDptoUbicacion_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList><asp:RequiredFieldValidator ID="RFVDptoUbicacion" Display="Dynamic" runat="server" ControlToValidate="CboDptoUbicacion" ValidationGroup="seguimiento" ErrorMessage="Debe Ingresar Departamento Ubicacion">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="FormatoTexto" style="text-align: left; vertical-align: middle">
                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 150px;" for="CboMunicipioUbicacion">Municipio Ubicacion:</label>
                                </td>
                                <td style="text-align: left; vertical-align: middle; ">
                                    <asp:DropDownList ID="CboMunicipioUbicacion" runat="server" Width="230px"></asp:DropDownList><asp:RequiredFieldValidator ID="RFVMunicipioUbicacion" Display="Dynamic" runat="server" ControlToValidate="CboMunicipioUbicacion" ValidationGroup="seguimiento" ErrorMessage="Debe Ingresar Municipio Ubicacion">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="FormatoTexto" style="text-align: left; vertical-align: middle">
                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 150px;" for="TxtNumeroSalvoconducto">Numero Salvoconducto:</label>
                                </td>
                                <td style="text-align: left; vertical-align: middle;">
                                    <asp:TextBox ID="TxtNumeroSalvoconducto" runat="server" MaxLength="150" Width="220px"></asp:TextBox><asp:RequiredFieldValidator ID="RFVNumeroSalvoconducto" Display="Dynamic" runat="server" ControlToValidate="TxtNumeroSalvoconducto" ValidationGroup="seguimiento" ErrorMessage="Debe Ingresar Numero Salvoconducto">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="FormatoTexto" style="text-align: left; vertical-align: middle;">
                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000; width: 150px;" for="TxtNumeroSalvoconducto">Codigo Seguridad:</label>
                                </td>
                                <td style="text-align: left; vertical-align: middle;">
                                    <asp:TextBox ID="TxtCodigoSeguridad" runat="server" MaxLength="50" Width="220px"></asp:TextBox><asp:RequiredFieldValidator ID="RFVCodigoSeguridad" Display="Dynamic" runat="server" ControlToValidate="TxtCodigoSeguridad" ValidationGroup="seguimiento" ErrorMessage="Debe Codigo Seguridad Salvoconducto">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:Button ID="BtnValidarSalvoconducto" runat="server" Text="Validar" ValidationGroup="seguimiento" OnClick="BtnValidarSalvoconducto_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="2">
                                    <asp:ValidationSummary ID="valResumenUsuario" runat="server" ValidationGroup="seguimiento" DisplayMode="List" ShowSummary="true" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table border="1" width="90%">
                            <tr>
                                <td>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grvEstadoSalvoconducto"  Caption="VALIDACION SALVOCONDUCTO" Font-Bold="true" runat="server" AutoGenerateColumns="false" ShowFooter="false" OnRowCommand="grvEstadoSalvoconducto_RowCommand" OnRowDataBound="grvEstadoSalvoconducto_RowDataBound" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SalvoconductoID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblSalvocnductoID" runat="server" Text='<%# Bind("SalvoconductoID") %>' SkinID="etiqueta_negra"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Tipo Salvoconducto">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblTipoSalvoconducto" runat="server" Text='<%# Bind("Tipo_Salvoconducto") %>' SkinID="etiqueta_negra"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="alinearTitulos" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Estado">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblEstadoSalvocondcuto" runat="server" Text='<%# Bind("Estado_Descripcion") %>' SkinID="etiqueta_negra"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="alinearTitulos" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Vigencia Desde">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblVigenciaDesde" runat="server" Text='<%# Bind("Fecha_Ini_Vigencia") %>' SkinID="etiqueta_negra"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="alinearTitulos" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vigencia Hasta">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblVigenciaHasta" runat="server" Text='<%# Bind("Fecha_Fin_Vigencia") %>' SkinID="etiqueta_negra"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="alinearTitulos" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Validacion">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblMensajeError" runat="server" Text='<%# Bind("Mensaje_Error") %>' SkinID="etiqueta_negra"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="alinearTitulos" />
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField ShowHeader="true" HeaderText="Acciones" Visible="true">
                                                        <ItemTemplate>
                                                            <table id="tblComandos" ClientIDMode="Static" runat="server" border="0" cellspacing="0" cellpadding="10" style="width: 80px; border-top: solid 0px black; border-bottom: solid 0px black; text-align: center; vertical-align: top; padding: 0px;">
                                                                <tr>
                                                                    <td style="text-align: center; vertical-align: middle; width: 50px;">
                                                                        <a href='<%# urlNavegacionVerSalvoconducto(Eval("SalvoconductoID")) %>' target="_blank">
                                                                            <img src="../App_Themes/Img/documento.gif" style="width: 20px" title="Ver Salvoconducto" />
                                                                        </a>
                                                                    </td>
                                                                    <td style="text-align: center; vertical-align: middle; width: 50px;">
                                                                        <a href='<%# urlNavegacionAgregarPtoControl(Eval("SalvoconductoID")) %>' target="_blank">
                                                                            <img runat="server" id="imgPuntos" src="../App_Themes/Img/LocationPoint.png" style="width: 20px" title="Adicionar Punto de Control" visible='<%# Eval("EstadoID").ToString() != "4" %>' />
                                                                        </a>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <HeaderStyle CssClass="alinearTitulos" />
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
    </div>

</asp:Content>
<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPASinMenuFlash.master" AutoEventWireup="true" CodeFile="Expedicion.aspx.cs" Inherits="Salvoconducto_Expedicion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/jquery.datetimepicker.css" rel="stylesheet" />
    <style type="text/css">
        .CajaDialogo {
            background-color: #fff;
            border-width: 1px;
            border-style: outset;
            border-color: Yellow;
            padding: 0px;
        }

            .CajaDialogo div {
                <a href="Expedicion.aspx">Expedicion.aspx</a> margin: 5px;
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

        div.centre {
            margin-left: auto;
            margin-right: auto;
            width: 600px;
        }
    </style>
    <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" Text="Expedir Salvoconducto" SkinID="titulo_principal_blanco"></asp:Label>
        &nbsp;
        <a href="#" id="hrCerrarVentana" visible="false" runat="server" onclick="window.close();return false;">Salir</a>
    </div>
    <div class="div-contenido">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="contact_form">
            <table width="90%">
                <tr>
                    <td style="width: 200px">
                        <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="cboAutoridadAmbiental">Autoridad Ambiental:</label>
                    </td>
                    <td>
                        <asp:DropDownList ID="cboAutoridadAmbiental" runat="server" ClientIDMode="Static"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvAutoridadAmbiental" Display="Dynamic" runat="server" ControlToValidate="cboAutoridadAmbiental" ValidationGroup="consulta" ErrorMessage="Seleccione una Autoridad Ambiental">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="cboClaseRecurso">Clase recurso:</label>
                    </td>
                    <td>
                        <asp:DropDownList ID="cboClaseRecurso" runat="server" ClientIDMode="Static"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="cboDepartamento">Departamento:</label>
                    </td>

                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cboDepartamento" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboDepartamento_SelectedIndexChanged"></asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="cboMunicipio">Municipio:</label></td>

                    <td>
                        <asp:UpdatePanel ID="upnlMunicipio" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cboMunicipio" runat="server"></asp:DropDownList>


                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cboDepartamento" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="fechaSolicitud">Fecha Solicitud:</label>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>Desde:</td>
                                <td>
                                    <asp:UpdatePanel ID="upnlFechaDesdeSol" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtFechaSolicitudDesde" ClientIDMode="Static" runat="server" Width="70px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>Hasta:</td>
                                <td>
                                    <asp:UpdatePanel ID="upnlFechaHastaSol" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtFechaSolicitudHasta" ClientIDMode="Static" runat="server" Width="70px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" ValidationGroup="consulta" OnClick="btnBuscar_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:UpdatePanel ID="upnlSalvoconductos" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grvSalvoconductos" runat="server" 
                                    OnRowCommand="grvSalvoconductos_RowCommand" 
                                    AutoGenerateColumns="false" 
                                    ShowFooter="True" 
                                    SkinID="GrillaCoordenadas" 
                                    EmptyDataText="No se encontraron registros" 
                                    HorizontalAlign="Center"
                                    OnPageIndexChanging="grvSalvoconductos_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField AccessibleHeaderText="Nro" HeaderText="Nro. Solicitud" DataField="SalvoconductoID" />
                                        <asp:BoundField AccessibleHeaderText="Tipo" HeaderText="Tipo" DataField="TipoSalvoconducto" />
                                        <asp:BoundField AccessibleHeaderText="Número VITAL" HeaderText="Número VITAL" DataField="NumeroVitalTramite" />
                                        <asp:BoundField AccessibleHeaderText="Estado" HeaderText="Estado" DataField="Estado" />
                                        <asp:BoundField AccessibleHeaderText="Autoridad Ambiental" HeaderText="Autoridad Ambiental" DataField="AutoridadEmisora" />
                                        <asp:BoundField AccessibleHeaderText="Fecha Solicitud" HeaderText="Fecha Solicitud" DataField="FechaSolicitud" />
                                        <asp:BoundField AccessibleHeaderText="Solicitante" HeaderText="Solicitante" DataField="Solicitante" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lnkVerSalvoconducto" runat="server" ClientIDMode="Static" Text="Detalles" Target="_blank" NavigateUrl='<%# urlNavegacion(Eval("SalvoconductoID")) %>' CssClass="a_green"></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnBuscar" />
                            </Triggers>
                        </asp:UpdatePanel>

                    </td>
                </tr>
            </table>
        </div>
    </div>
    <script src="../Scripts/jquery-1.9.1.js"></script>
    <script src="../js/datimepicker-master/build/jquery.datetimepicker.full.js"></script>
    <script src="../jquery/jquery.numeric.js"></script>
    <script src="../js/BuscarSalvoconducto.js"></script>
</asp:Content>


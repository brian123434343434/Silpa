<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPASinMenuFlash.master" AutoEventWireup="true" CodeFile="BuscarSalvoconducto.aspx.cs" Inherits="Salvoconducto_BuscarSalvoconducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="../jquery/jquery.js"></script>
    <link href="../jquery/EstiloGris/jquery-ui.css" rel="stylesheet" />
    <script src="../jquery/EstiloGris/jquery-ui.js"></script>
    <script src="../jquery/jquery.numeric.js"></script>
    <link href="../App_Themes/skin/StyleREDDS.css" rel="stylesheet" />
     <style type="text/css">
        .CajaDialogo
        {
            background-color:#fff; 
            border-width: 1px;
            border-style: outset;
            border-color: Yellow;
            padding: 0px;
        }
        .CajaDialogo div
        {
            margin: 5px;
        }
        .FondoAplicacion
        {
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
        .href
        {
        color:White;  
        font-weight:bold;
        text-decoration:none;
        }
        .ajax__calendar_container { position :absolute;
            z-index : 100003 !important;
            background-color: white;
        }
        div.centre
        {
            margin-left:auto; 
            margin-right:auto;
            width:600px;
        }
</style>
    <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" Text="Buscar Salvoconducto" SkinID="titulo_principal_blanco"></asp:Label>
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
                                    <label for="cboAutoridadAmbiental" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Autoridad Ambiental:</label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cboAutoridadAmbiental" runat="server" ClientIDMode="Static"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvAutoridadAmbiental" Display="Dynamic" runat="server" ControlToValidate="cboAutoridadAmbiental" ValidationGroup="consulta" ErrorMessage="Seleccione una Autoridad Ambiental">*</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="cboTipoSalvoconducto" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Tipo Salvoconducto:</label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cboTipoSalvoconducto" runat="server" ClientIDMode="Static"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Tipo Identificacion Solicitante:</label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cboTipoIdentificacion" runat="server"></asp:DropDownList>
                                </td>

                            </tr>
                            <tr>
                                <td>
                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;"> Identificacion Solicitante:</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNumeroIdentificacion" runat="server" ClientIDMode="Static"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="cboEstado">Estado:</label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cboEstado" runat="server"></asp:DropDownList>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;" for="fechaSolicitud">Fecha Solicitud:</label>
                                </td>
                                <td>Desde:
                                    <asp:UpdatePanel ID="upnlFechaDesdeSol" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtFechaSolicitudDesde" ClientIDMode="Static" runat="server" Width="70px" /><asp:RequiredFieldValidator ID="rfvFechaSolicitudDesde" runat="server" ControlToValidate="txtFechaSolicitudDesde" ValidationGroup="consulta" ErrorMessage="Seleccione una fecha Desde Solicitud">*</asp:RequiredFieldValidator></ContentTemplate>
                                    </asp:UpdatePanel>
                                    Hasta:
                                    <asp:UpdatePanel ID="upnlFechaHastaSol" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtFechaSolicitudHasta" ClientIDMode="Static" runat="server" Width="70px" /><asp:RequiredFieldValidator ID="rfvFechaSolicitudHasta" runat="server" ControlToValidate="txtFechaSolicitudHasta" ValidationGroup="consulta" ErrorMessage="Seleccione una fecha Hasta Solicitud">*</asp:RequiredFieldValidator></ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnBuscar"  runat="server" Text="Buscar" ValidationGroup="consulta" OnClick="btnBuscar_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:GridView ID="grvSalvoconductos" runat="server" Width="100%" 
                                        AutoGenerateColumns="false" ShowFooter="True" SkinID="GrillaCoordenadas" 
                                        EmptyDataText="No se encontraron registros" 
                                        OnPageIndexChanging="grvSalvoconductos_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField AccessibleHeaderText="Numero" HeaderText="Numero" DataField="Numero" />
                                            <asp:BoundField AccessibleHeaderText="Tipo" HeaderText="Tipo" DataField="TipoSalvoconducto" />
                                            <asp:BoundField AccessibleHeaderText="Número VITAL" HeaderText="Número VITAL" DataField="NumeroVitalTramite" />
                                            <asp:BoundField AccessibleHeaderText="Estado" HeaderText="Estado" DataField="Estado" />
                                            <asp:BoundField AccessibleHeaderText="Autoridad Ambiental" HeaderText="Autoridad Ambiental" DataField="Autoridad" />
                                            <asp:BoundField AccessibleHeaderText="Fecha Solicitud" HeaderText="Fecha Solicitud" DataField="FechaSolicitud" />
                                            <asp:BoundField AccessibleHeaderText="Solicitante" HeaderText="Solicitante" DataField="Solicitante" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:HyperLink Target="_blank" ID="lnkVerSalvoconducto" runat="server" Text="Detalles" CssClass="a_green"></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                               </td>
                            </tr>
                        </table>
                    </div>
    </div>
     
    <script src="../js/BuscarSalvoconducto.js"></script>
</asp:Content>


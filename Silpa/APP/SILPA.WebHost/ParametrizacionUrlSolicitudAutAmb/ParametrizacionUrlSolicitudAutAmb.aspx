<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPASinMenuFlash.master" AutoEventWireup="true" CodeFile="ParametrizacionUrlSolicitudAutAmb.aspx.cs" Inherits="ParametrizacionUrlSolicitudAutAmb_ParametrizacionUrlSolicitudAutAmb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="../jquery/jquery.js"></script>
    <link href="../jquery/EstiloGris/jquery-ui.css" rel="stylesheet" />
    <script src="../jquery/EstiloGris/jquery-ui.js"></script>
    <script src="../jquery/jquery.numeric.js"></script>
    <link href="../App_Themes/skin/StyleREDDS.css" rel="stylesheet" />
     <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

    <style type="text/css">
        .modal-background 
        {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.5;
            display: table;
            z-index: 10 !important;
        }
        .AnchoLabel 
        {
            width: 50px;
        }
        .celdaTh
        {
            font-family: Arial, sans-serif, Verdana; 
            font-size: 11pt; 
            font-weight: bold; 
            text-align: center; 
            vertical-align: middle;
            background-color: #0a346f;
            padding: 6px;
            border-bottom: 2px solid black;
            color: white;
        }
        .celdaTdTitle
        {
            font-family: Arial, sans-serif, Verdana; 
            font-size: 9pt; 
            font-weight: bold; 
            text-align: left; 
            vertical-align: middle;
        }
        .celdaTd
        {
            font-family: Arial, sans-serif, Verdana; 
            font-size: 9pt; 
            font-weight: normal; 
            text-align: left; 
            vertical-align: middle;
        }
        .celdaTdCenter
        {
            font-family: Arial, sans-serif, Verdana; 
            font-size: 9pt; 
            font-weight: normal; 
            text-align: center; 
            vertical-align: middle;
        }
        .auto-style1 {
            height: 21px;
        }
        .auto-style2 {
            font-family: Arial, sans-serif, Verdana;
            font-size: 9pt;
            font-weight: bold;
            text-align: left;
            vertical-align: middle;
            height: 38px;
        }
        .auto-style3 {
            font-family: Arial, sans-serif, Verdana;
            font-size: 9pt;
            font-weight: normal;
            text-align: left;
            vertical-align: middle;
            height: 38px;
        }

        .AlinearDiv
        {    
            display: table;
            width: 90%;
            border: 1px solid;
        }
        .FormatoTexto
        {
            width:50px;
            text-align:left; 
            vertical-align: middle;
        }

    </style>


    <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" Text="PARAMETRIZACION URL INFORMATIVA POR TIPO DE TRAMITE Y AUTORIDAD AMBIENTAL" SkinID="titulo_principal_blanco"></asp:Label>
        &nbsp;
        <a href="#" id="hrCerrarVentana" visible="false" runat="server" onclick="window.close();return false;">Salir</a>
    </div>
    <div class="div-contenido">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="contact_form">
            <div class="AlinearDiv">
                <div class="RowBuscarTitulo">
                    <div class="CellBuscarTitulo">
                        <asp:Literal ID="ltlTituloBuscador" runat="server" Text="FILTROS DE BUSQUEDA"></asp:Literal>
                    </div>
                </div>
                        <table width="100%" border="0">
                            <tr>
                                <td class="FormatoTexto">
                                    <label for="cboAutoridadAmbiental">Autoridad Ambiental:</label>
                                </td>
                                <td colspan="2" class="auto-style6">
                                    <asp:DropDownList ID="cboAutoridadAmbiental" runat="server" ClientIDMode="Static"></asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="rfvAutoridadAmbiental" Display="Dynamic" runat="server" ControlToValidate="cboAutoridadAmbiental" ValidationGroup="ConsultarSerie" ErrorMessage="Seleccione una Autoridad Ambiental">*</asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td class="FormatoTexto">
                                    <label for="CboTramite:" style="width: 200px;">Tramite:</label>
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList ID="CboTramite" runat="server" ClientIDMode="Static"></asp:DropDownList>
                                </td>
                            </tr>

                        </table>

                <div class="RowButton">
                    <div class="CellButton">
                        <asp:UpdatePanel ID="updDatosSerie" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="BtnConsultar" runat="server" Text="Consultar" OnClick="BtnConsultar_Click" ValidationGroup="ConsultarSerie" />
                                &nbsp
                                    <a href="AsignarNumeracionSalvoconducto.aspx" target="_blank">
                                        <asp:Button ID="BtnNuevoregistro" runat="server" Text="Nuevo Registro" class="button" OnClick="BtnNuevoregistro_Click"/>
                                        <%--<input id="BtnNuevaSerie" type="button" value="Nueva Serie" class="button" />--%>
                                    </a>
                            </ContentTemplate>
                            <Triggers>
                                 <asp:AsyncPostBackTrigger ControlID="BtnConsultar" EventName="Click" />
                            </Triggers>

                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>

      <div class="div-contenido">
        <div class="contact_form">
            <div class="AlinearDiv" style="overflow:scroll">
                <asp:UpdatePanel ID="upnlseries" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GVASIGANCIONSERIES" runat="server" Width="100%" AllowSorting="True" ShowFooter="false"
                AutoGenerateColumns="False" AllowPaging="True" OnRowDataBound="GridView1_RowDataBound" SkinID="GrillaSeguridad"
                OnRowCommand="GridView1_RowCommand" OnRowDeleting="GVASIGANCIONSERIES_RowDeleting" EmptyDataText="No Hay Datos para Esta Consulta" OnPageIndexChanging="GVASIGANCIONSERIES_PageIndexChanging" PageSize="10">
                <PagerSettings Mode="Numeric" Position="Bottom" PageButtonCount="10" />
                <Columns>
                    <asp:TemplateField HeaderText="Acciones" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <table id="tblComandos" runat="server" border="0" cellspacing="0" cellpadding="10" style="border-top: solid 0px black; text-align: center; vertical-align: top; padding: 0px;">
                                <tr>
                                    <td style="text-align: center; vertical-align: middle; width: 50px;">
                                        <asp:ImageButton ID="btnEditarSerie" runat="server" AlternateText="Editar Url"
                                            ImageUrl="~/App_Themes/Img/Edit.png" ValidationGroup="vgDTE_grid" Width="22px"
                                            CommandName="Editar_Url" CommandArgument='<%# Eval("ID") %>'
                                            ToolTip="Editar Registro" />
                                    </td>
                                    <td style="text-align: center; vertical-align: middle; width: 50px;">
                                        <asp:ImageButton ID="btnBloquearSerie" runat="server" AlternateText="Eliminar Url"
                                            ImageUrl="~/App_Themes/Img/Del.png" ValidationGroup="vgDTE_grid" Width="22px"
                                            CommandName="Eliminar_Url" CommandArgument='<%# Eval("ID") %>'
                                            ToolTip="Eliminar Url" OnClientClick="return confirm('Esta Seguro de Eliminar este registro?')" />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Autoridad Ambiental">
                        <ItemTemplate>
                            <asp:Label ID="Autoridad_Ambiental" runat="server" Text='<%# Bind("AUT_NOMBRE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

       
                    <asp:TemplateField HeaderText="Tramite">
                        <ItemTemplate>
                            <asp:Label ID="LblNombreTipoTramite" runat="server" Text='<%# Bind("NOMBRE_TIPO_TRAMITE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Url" ItemStyle-Width="100px">
                        <ItemTemplate>
                            <asp:Label ID="LblUrl" runat="server" Text='<%# Bind("URL") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Codigo Usuario Corporacion">
                        <ItemTemplate>
                            <asp:Label ID="IdParticipante" runat="server" Text='<%# Bind("ID_PARTICIPANTE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BtnConsultar" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="RowButton">
        <div class="CellButton">
        <%--<asp:ValidationSummary ID="VSSeriesSalvocondcuto" runat="server" ValidationGroup="ConsultarSerie" DisplayMode="List" />--%>
        </div>
        </div>

    </div>     



   <%--edicion de serie--%>
    <input type="button" runat="server" id="BtnValidarNumeracionOculto" style="display:none" />
    <cc1:ModalPopupExtender ID="mpeValidarNumeracion" runat="server" PopupControlID="DivEstadosNumeracion" TargetControlID="BtnValidarNumeracionOculto" BehaviorID="mpeValidarNumeracion" BackgroundCssClass="modal-background"></cc1:ModalPopupExtender>
    <div id="DivEstadosNumeracion" runat="server" visible="true" style="background-color: white;display:none">
        <div class="contact_form">
            <asp:UpdatePanel ID="UpdPnlEditSerie" runat="server" ChildrenAsTriggers ="true">
                <ContentTemplate>
                    <table style="text-align: center; vertical-align: top; border: 2px solid black;" border="0" cellpadding="4" cellspacing="0">
                        <tr>
                            <td colspan="2" class="celdaTh">Actualizacion URL informativa:
                            </td>
                        </tr>
                        <tr>
                            <td class="celdaTdTitle">Autoridad Ambiental:</td>
                            <td class="celdaTd">
                               <asp:DropDownList ID="CboAutAmbientalEdit" runat="server" ClientIDMode="Static"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">Tipo de Tramite:</td>
                            <td colspan="2" class="auto-style3">
                                <asp:DropDownList ID="CboTipoTramiteEdit" runat="server" ClientIDMode="Static"></asp:DropDownList>
                                </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">Url:</td>
                            <td colspan="2" class="auto-style3">
                                <asp:TextBox ID="TxtUrl" ValidationGroup="ActualizarRegistro" runat="server" Width="95%" TextMode="MultiLine" Height="80px"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator  ID="RFVUrl" ValidationGroup="ActualizarRegistro" runat="server" ErrorMessage="Debe Ingresar una URL" ControlToValidate ="TxtUrl">*</asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">Id Solicitante:</td>
                            <td colspan="2" class="auto-style3">
                                <asp:TextBox ID="TxtIdParticipante" runat="server" ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="celdaTdCenter" colspan="3" style="border-top: 2px solid black; padding: 10px; background-color: #CCCCCC;">
                                <asp:Button ValidationGroup="ActualizarRegistro" ID="BtnGrabar" runat="server" Text="Grabar"  OnClick="BtnActualizar_Click" OnClientClick="javascript:document.forms[0].encoding ='multipart/form-data';" style="height: 28px"/>
                                &nbsp&nbsp&nbsp
                                <asp:Button ID="BtnCancelarEdicion" runat="server" Text="Cancelar" OnClick="BtnCancelarEdicion_Click" style="height: 28px"/>
                            </td>
                        </tr>
<%--                        <tr>
                            <td colspan="3">
                                <asp:ValidationSummary ID="VsModificarSerie" runat="server" ValidationGroup="ActualizarRegistro" DisplayMode="List"/>
                            </td>
                        </tr>--%>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="BtnGrabar"/>
                </Triggers>

            </asp:UpdatePanel>
        </div>
    </div>
 
    <asp:UpdateProgress ID="UpdPConsultarSerie" runat="server" AssociatedUpdatePanelID="updDatosSerie">
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

    <%--creacion pantalla para editar los salvoconductos--%>

    <script src="../js/SeriesSalvoConducto.js"></script>
</asp:Content>

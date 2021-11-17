<%@Page Language="C#" AutoEventWireup="true" MasterPageFile="~/plantillas/SILPASinMenuFlash.master" CodeFile="ModuloAdminEspeciesProductos.aspx.cs" Inherits="Salvoconducto_ModuloAdminEspeciesProductos"  EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<script runat="server">


</script>


<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
    <link href="s../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
     <link href="../js/datimepicker-master/build/jquery.datetimepicker.min.css" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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

        .TextoValidadores{
            font-size:medium;
        }

        .CajaDialogo {
            background-color: #fff;
            border-width: 1px;
            border-style: outset;
            border-color: Yellow;
            padding: 0px;
        }

            .CajaDialogo div {
                margin: 5px;
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
        <asp:Label ID="lblTituloPrincipal" runat="server" Text="Parametrizacion Especies - Clases y tipos de producto" SkinID="titulo_principal_blanco"></asp:Label>
        &nbsp;
        <a href="#" id="hrCerrarVentana" visible="false" runat="server" onclick="window.close();return false;">Salir</a>
    </div>

    <div class="table-responsive">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <cc1:TabContainer ID="tbcContenedor" runat="server" Width="100%" ActiveTabIndex="0">
            <cc1:TabPanel runat="server" HeaderText="Adicionar Especimenes" ID="tabEspecimenes">
                <HeaderTemplate>
                    Adicionar Especimenes
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="contact_form">
                        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important;">
                            <tr>
                                <td>
                                    <label for="CboClaseRecurso" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Clase Recurso:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel runat="server" ID="UpdCboClaseRecurso">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="CboClaseRecurso" runat="server"></asp:DropDownList>
                                            &nbsp;
		                                    <asp:RequiredFieldValidator ID="RfvClaseRecurso" runat="server" ControlToValidate="CboClaseRecurso" ValidationGroup="especimen">*</asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <label for="txtNombreCientificoEspecie" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Nombre Científico:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel runat="server" ID="upnlControlEspeice">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtNombreCientificoEspecie" runat="server" />
                                            <asp:LinkButton ID="lnkEspecie" runat="server" OnClick="lnkEspecie_Click" Text="Buscar Especie"></asp:LinkButton>
                                            <asp:HiddenField ID="hfEspcimenID" runat="server" />
                                            &nbsp;
		                                    <asp:RequiredFieldValidator ID="rfvNombreCientificoEspecie" runat="server" ControlToValidate="txtNombreCientificoEspecie" ValidationGroup="especimen">*</asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <label for="txtNombreComunEspecie" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Nombre Comun:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtNombreComunEspecie" runat="server" />
                                            &nbsp;
		                                    <asp:RequiredFieldValidator ID="rfvNombreComunEspecie" runat="server" ControlToValidate="txtNombreComunEspecie" ValidationGroup="especimen">*</asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="txtCodigoIdeam" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Codigo IDEAM:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel runat="server" ID="UpdCodigoIdeam">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtCodigoIdeam" runat="server" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="padding: 20px; text-align: center; vertical-align: middle;">
                        <asp:UpdatePanel ID="UpdAccionesBotonEspecies" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnActualizarEspecie" runat="server" Text="Grabar Informacion" ValidationGroup="especimen" OnClick="btnActualizarEspecie_Click" OnClientClick=" return confirm('Desea Actualizar la informacion?')" />
                                <asp:Button ID="btnCancelarEspecie" runat="server" Text="Cancelar" OnClick="btnCancelarEspecie_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                </ContentTemplate>
            </cc1:TabPanel>

            <cc1:TabPanel runat="server" HeaderText="Adicionar Clase Producto" ID="TabClaseProducto">
                <HeaderTemplate>
                    Adicionar Clase Producto
                </HeaderTemplate>
                <ContentTemplate>
                    <div class="contact_form">
                        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important">
                            <tr>
                                <td>
                                    <label for="CboClaseRecursoClaseProducto" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Clase Recurso:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel runat="server" ID="UpdCboClaseRecursoClaseProducto">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="CboClaseRecursoClaseProducto" runat="server"></asp:DropDownList>
                                            &nbsp;
		                                    <asp:RequiredFieldValidator ID="RfvClaseRecursoClaseProducto" runat="server" ControlToValidate="CboClaseRecursoClaseProducto" ValidationGroup="ClaseProducto">*</asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RfvClaseRecursoClaseProducto2" runat="server" ControlToValidate="CboClaseRecursoClaseProducto" ValidationGroup="ClaseProductoTipoProducto">*</asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="txtClaseProducto" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Clase Producto:</label>
                                </td>

                                <td>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel4">
                                        <ContentTemplate>
                                            <asp:TextBox ID="TxtNombreClaseProducto" runat="server" ToolTip="Descripcion Clase Producto" ClientIDMode="Static"></asp:TextBox>
                                            <asp:LinkButton ID="LnkBuscarClaseProducto" runat="server" Text="Buscar" OnClick="LnkBuscarClaseProducto_Click"></asp:LinkButton>
                                            <asp:HiddenField ID="hfClaseProductoID" runat="server" />
                                            &nbsp;
        		                            <asp:RequiredFieldValidator ID="RfvTxtClaseProducto" runat="server" ControlToValidate="TxtNombreClaseProducto" ValidationGroup="ClaseProducto">*</asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RfvTxtClaseProducto2" runat="server" ControlToValidate="TxtNombreClaseProducto" ValidationGroup="ClaseProductoTipoProducto">*</asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="BtnAgregarTipoProducto" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="txtCodigoIdeamClaseProducto" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Codigo IDEAM:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel runat="server" ID="UpdCodigoIdeamClaseProducto">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtCodigoIdeamClaseProducto" runat="server" ClientIDMode="Static" />
                                            &nbsp;
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>

                            <tr>
                                <div style="display: inline-block; margin-left: 4px;">
                                    <td>
                                        <label for="ChkVerSunlClaseProducto" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Visible para salvoconductos</label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                            <ContentTemplate>
                                                <asp:CheckBox ID="ChkVerSunlClaseProducto" runat="server" SkinID="etiqueta_negra" TextAlign="Left" Checked="true" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </div>
                            </tr>
                            <tr>
                                <div style="display: inline-block; margin-left: 4px;">
                                <td>
                                    <label for="ChkVerAprovClaseProducto" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Visible para Aprovechamientos</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="ChkVerAprovClaseProducto" runat="server" SkinID="etiqueta_negra" TextAlign="Left" Checked="true" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>


                        <div style="border: groove; border-radius: 8px">
                            <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important">
                                <tr>
                                    <td>
                                        <label for="" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">ASOCIAR TIPO PRODUCTO:</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="CboClaseTipoProducto" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Tipo Producto:</label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel runat="server" ID="UpdCboClaseTipoProducto">
                                            <ContentTemplate>

                                                <asp:DropDownList ID="CboClaseTipoProducto" runat="server" Width="100px"></asp:DropDownList>
                                                &nbsp;
		                                    <asp:RequiredFieldValidator ID="RfvCboClaseTipoProducto" runat="server" ControlToValidate="CboClaseTipoProducto" ValidationGroup="ClaseProductoTipoProducto">*</asp:RequiredFieldValidator>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="BtnAgregarTipoProducto" runat="server" Text="Agregar Tipo Producto" ValidationGroup="ClaseProductoTipoProducto" OnClick="BtnAgregarTipoProducto_Click" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr style="width:350px">
                                    <td>
                                        <asp:UpdatePanel ID="upnAgregarTpoProducto" runat="server">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="BtnAgregarTipoProducto" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <div style="overflow: auto; max-width: 800px; max-height: 700px;">
                                                    <asp:GridView ID="grvClaseProductoTipoProducto" runat="server" Width="100%"
                                                        SkinID="grilla" AllowPaging="True" AllowSorting="True" PageSize="12"
                                                        EmptyDataText="No se encontraron datos"
                                                        AutoGenerateColumns="False" CellPadding="2" CellSpacing="1"
                                                        GridLines="None" ShowFooter="True"
                                                        DataKeyNames="CLASE_PRODUCTO_ID,TIPO_PRODUCTO_ID"
                                                        OnRowEditing="grvClaseProductoTipoProducto_RowEditing"
                                                        OnPageIndexChanging="grvClaseProductoTipoProducto_PageIndexChanging" OnRowDeleting="grvClaseProductoTipoProducto_RowDeleting">
                                                        <HeaderStyle Font-Size="9pt" />
                                                        <FooterStyle Font-Size="9pt" ForeColor="Black" CssClass="texto_tablas_paginador" />
                                                        <RowStyle Font-Size="9pt" ForeColor="Black" />
                                                        <SelectedRowStyle Font-Size="9pt" ForeColor="Black" />
                                                        <EditRowStyle Font-Size="9pt" ForeColor="Black" />
                                                        <AlternatingRowStyle Font-Size="9pt" ForeColor="Black" />
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Clase Producto">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="LblClaseProducto" Text='<%# Eval("CLASE_PRODUCTO") %>' SkinID="etiqueta_negra"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Tipo Producto">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" ID="lblNomLblTipoProducto" Text='<%# Eval("TIPO_PRODUCTO") %>' SkinID="etiqueta_negra"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="lnkEliminar" CommandName="Delete" CssClass="a_orange">Eliminar</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                        <PagerSettings FirstPageImageUrl="../App_Themes/Img/pagina_primera.gif" FirstPageText="Primera"
                                                            LastPageImageUrl="../App_Themes/Img/pagina_ultima.gif" LastPageText="Ultima" Mode="NumericFirstLast"
                                                            NextPageImageUrl="../App_Themes/Img/pagina_siguiente.gif" NextPageText="Siguiente" PreviousPageImageUrl="../App_Themes/Img/pagina_anterior.gif"
                                                            PreviousPageText="Anterior" />
                                                    </asp:GridView>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div style="padding: 20px; text-align: center; vertical-align: middle;">
                        <asp:UpdatePanel ID="UpdAccionesBotonClaseProducto" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="BtnGrabarClaseProducto" runat="server" Text="Grabar Informacion" ValidationGroup="ClaseProducto" OnClick="BtnGrabarClaseProducto_Click" OnClientClick=" return confirm('Desea Actualizar la informacion?')" />
                                <asp:Button ID="BtnCancelarClaseProducto" runat="server" Text="Cancelar" OnClick="BtnCancelarClaseProducto_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>

            <cc1:TabPanel runat="server" HeaderText="Adicionar Tipo Producto" ID="TabTipoProducto">
                <ContentTemplate>
                    <div class="contact_form">
                        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important">
                            <tr>
                                <td>
                                    <label for="txtTipoProducto" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Tipo Producto:</label>
                                </td>

                                <td>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel7">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtTipoProducto" runat="server" ToolTip="Descripcion Clase Producto" ClientIDMode="Static"></asp:TextBox>
                                            <asp:LinkButton ID="LnkBuscarTipoProducto" runat="server" Text="Buscar" OnClick="LnkBuscarTipoProducto_Click"></asp:LinkButton>
                                            <asp:HiddenField ID="hfTipoProductoID" runat="server" />
                                            &nbsp;
        		                            <asp:RequiredFieldValidator ID="RFVTipoProducto" runat="server" ControlToValidate="txtTipoProducto" ValidationGroup="TipoProducto">*</asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RFVTipoProducto2" runat="server" ControlToValidate="txtTipoProducto" ValidationGroup="TipoProductoUnidadMedida">*</asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="txtCodigoIdeamClaseProducto" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Codigo IDEAM:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel8">
                                        <ContentTemplate>
                                            <asp:TextBox ID="TxtCodigoIdeamTipoProducto" runat="server" ClientIDMode="Static" />
                                            &nbsp;
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <div style="display: inline-block; margin-left: 4px;">
                                    <td>
                                        <label for="ChkVerCalaseRecursoSUNL" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Visible para salvoconductos</label>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel runat="server" ID="UpdatePanel6">
                                            <ContentTemplate>
                                                <asp:CheckBox ID="ChkVerTipoProductoSUNL" runat="server" SkinID="etiqueta_negra" TextAlign="Left" Checked="true" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </div>
                            </tr>
                            <tr>
                                <div style="display: inline-block; margin-left: 4px;">
                                <td>
                                    <label for="ChkVerTipoProductoAprovechamiento" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Visible para Aprovechamientos</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel9">
                                        <ContentTemplate>
                                            <asp:CheckBox ID="ChkVerTipoProductoAprovechamiento" runat="server" SkinID="etiqueta_negra" TextAlign="Left" Checked="true" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div style="border: groove; border-radius: 8px">
                        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important">
                            <tr>
                                <td>
                                    <label for="" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">ASOCIAR UNIDAD MEDIDA:</label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="CboClaseTipoProducto" style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Unidad Medida:</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel10">
                                        <ContentTemplate>

                                            <asp:DropDownList ID="CboUnidadMedida" runat="server" Width="100px"></asp:DropDownList>
                                            &nbsp;
		                                    <asp:RequiredFieldValidator ID="RfvCboUnidadMedida" runat="server" ControlToValidate="CboUnidadMedida" ValidationGroup="TipoProductoUnidadMedida">*</asp:RequiredFieldValidator>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="BtnAdicionarUnidadMedida" runat="server" Text="Agregar Unidad Medida" ValidationGroup="TipoProductoUnidadMedida" OnClick="BtnAdicionarUnidadMedida_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr style="width:350px">
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="BtnAdicionarUnidadMedida" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <div style="overflow: auto; max-width: 800px; max-height: 700px;">
                                                <asp:GridView ID="GrvTipoProductoUnidadMedida" runat="server" Width="100%"
                                                    SkinID="grilla" AllowPaging="True" AllowSorting="True" PageSize="12"
                                                    EmptyDataText="No se encontraron datos"
                                                    AutoGenerateColumns="False" CellPadding="2" CellSpacing="1"
                                                    GridLines="None" ShowFooter="True"
                                                    DataKeyNames="TIPO_PRODUCTO_ID, UNIDAD_MEDIDA_ID"
                                                    OnRowEditing="GrvTipoProductoUnidadMedida_RowEditing"
                                                    OnPageIndexChanging="GrvTipoProductoUnidadMedida_PageIndexChanging"
                                                    OnRowDeleting="GrvTipoProductoUnidadMedida_RowDeleting">
                                                    <HeaderStyle Font-Size="9pt" />
                                                    <FooterStyle Font-Size="9pt" ForeColor="Black" CssClass="texto_tablas_paginador" />
                                                    <RowStyle Font-Size="9pt" ForeColor="Black" />
                                                    <SelectedRowStyle Font-Size="9pt" ForeColor="Black" />
                                                    <EditRowStyle Font-Size="9pt" ForeColor="Black" />
                                                    <AlternatingRowStyle Font-Size="9pt" ForeColor="Black" />
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Tipo Producto">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblNomLblTipoProducto" Text='<%# Eval("TIPO_PRODUCTO") %>' SkinID="etiqueta_negra"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unidad Medida">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="LblUnidadMedida" Text='<%# Eval("UNIDAD_MEDIDA") %>' SkinID="etiqueta_negra"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkSeleccionar" CommandName="Edit" CssClass="a_orange">Editar</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lnkEliminar" CommandName="Delete" CssClass="a_orange">Eliminar</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    <PagerSettings FirstPageImageUrl="../App_Themes/Img/pagina_primera.gif" FirstPageText="Primera"
                                                        LastPageImageUrl="../App_Themes/Img/pagina_ultima.gif" LastPageText="Ultima" Mode="NumericFirstLast"
                                                        NextPageImageUrl="../App_Themes/Img/pagina_siguiente.gif" NextPageText="Siguiente" PreviousPageImageUrl="../App_Themes/Img/pagina_anterior.gif"
                                                        PreviousPageText="Anterior" />
                                                </asp:GridView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="padding: 20px; text-align: center; vertical-align: middle;">
                        <asp:UpdatePanel ID="upnlAccionesBotonTipoProducto" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="BtnGrabarTipoProducto" runat="server" Text="Grabar Informacion" ValidationGroup="TipoProducto" OnClick="BtnGrabarTipoProducto_Click" OnClientClick=" return confirm('Desea Actualizar la informacion?')" />
                                <asp:Button ID="BtnCancelarTipoproducto" runat="server" Text="Cancelar" OnClick="BtnCancelarTipoproducto_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </ContentTemplate>
            </cc1:TabPanel>

        </cc1:TabContainer>


    </div>    

<asp:Label ID="lblMpEspecimen" runat="server" SkinID="etiqueta_negra"></asp:Label>
        <cc1:ModalPopupExtender ID="mpeEspecimen" runat="server"
            TargetControlID="lblMpEspecimen"
            PopupControlID="pnlEspecimen"
            DropShadow="True" Enabled="True" DynamicServicePath=""
            BackgroundCssClass="FondoAplicacion">
        </cc1:ModalPopupExtender>
        <asp:Panel ID="pnlEspecimen" runat="server" Style="display: none; max-width: 800px; max-height: 700px;" CssClass="CajaDialogo" ScrollBars="Vertical">
            <asp:UpdatePanel ID="upnlEspecimen" runat="server">
                <ContentTemplate>
                    <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important; max-width: 800px;">
                        <tr>
                            <th colspan="3" style="font-size: 12pt; font-weight: bold; border-bottom: 1px solid Gray;">Buscar Especie</th>
                        </tr>
                        <tr>
                            <td>
                                <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Nombre Cientifico:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNombreComun" runat="server" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNombreComun" Display="Dynamic" runat="server" ControlToValidate="txtNombreComun" ValidationGroup="Buscarespecie">*</asp:RequiredFieldValidator>
                            </td>
                            <td style="padding-left: 30px; text-align: right; vertical-align: middle;">
                                <asp:Button ID="btnBuscarEspecie" runat="server" Text="Buscar" SkinID="boton_copia" OnClick="btnBuscarEspecie_Click" ValidationGroup="Buscarespecie" />
                            </td>
                        </tr>
                    </table>
                    <div style="overflow: auto; max-width: 800px; max-height: 700px;">
                        <asp:GridView ID="dgv_Especies" runat="server" Width="100%" 
                            SkinID="grilla" AllowPaging="True" AllowSorting="True" PageSize="12" 
                            EmptyDataText="No se encontraron datos"
                            AutoGenerateColumns="False" CellPadding="2" CellSpacing="1"
                            GridLines="None" ShowFooter="True" 
                            DataKeyNames="ESEPCIE_ID" 
                            OnRowEditing="dgv_Especies_RowEditing" 
                            OnPageIndexChanging="dgv_Especies_PageIndexChanging">
                            <HeaderStyle Font-Size="9pt" />
                            <FooterStyle Font-Size="9pt" ForeColor="#000000" cssclass="texto_tablas_paginador" />
                            <RowStyle Font-Size="9pt" ForeColor="#000000" />
                            <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                            <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                            <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                            <columns>
                                <asp:TemplateField HeaderText = "Nombre Común">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblNombreComun" Text = '<%# Eval("NOMBRE_COMUN") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText = "Nombre Científico">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblNombreCientifico" Text = '<%# Eval("NOMBRE_CIENTIFICO") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText = "Clase Recurso">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblClaseRecurso" Text = '<%# Eval("CLASE_RECURSO") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText = "Clase Recurso ID" visible="false">    
                                <ItemTemplate>
                                        <asp:Label runat="server" ID="lblClaseRecursoID" Text = '<%# Eval("CLASE_RECURSO_ID") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText = "Codigo Ideam">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCodigoIdeam" Text = '<%# Eval("CODIGO_IDEAM") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText = "Seleccionar">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" id="lnkSeleccionar" CommandName="Edit" CssClass="a_orange">Seleccionar</asp:LinkButton>                 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                </columns>
                            <%--<rowstyle cssclass="texto_tablas" />
                            <pagerstyle cssclass="texto_tablas_paginador" horizontalalign="Left" />
                            <headerstyle cssclass="titulo_tablas" />
                            <alternatingrowstyle cssclass="texto_tablas_dos" />--%>
                            <PagerSettings FirstPageImageUrl="../App_Themes/Img/pagina_primera.gif" FirstPageText="Primera"
                                LastPageImageUrl="../App_Themes/Img/pagina_ultima.gif" LastPageText="Ultima" Mode="NumericFirstLast"
                                NextPageImageUrl="../App_Themes/Img/pagina_siguiente.gif" NextPageText="Siguiente" PreviousPageImageUrl="../App_Themes/Img/pagina_anterior.gif"
                                PreviousPageText="Anterior" />
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div style="padding-top: 20px; text-align: center; vertical-align: middle; width: 100%;">
                <asp:Button ID="Button2" runat="server" Text="Cerrar" CausesValidation="False" SkinID="boton_copia" />
            </div>
        </asp:Panel>


<asp:Label ID="LblMpeClaseProducto" runat="server" SkinID="etiqueta_negra"></asp:Label>
        <cc1:ModalPopupExtender ID="MpeClaseProducto" runat="server"
            TargetControlID="LblMpeClaseProducto"
            PopupControlID="pnlClaseProducto"
            DropShadow="True" Enabled="True" DynamicServicePath=""
            BackgroundCssClass="FondoAplicacion">
        </cc1:ModalPopupExtender>

        <asp:Panel ID="pnlClaseProducto" runat="server" Style="display: none; max-width: 800px; max-height: 700px;" CssClass="CajaDialogo" ScrollBars="Vertical">
            <asp:UpdatePanel ID="UplClaseProducto" runat="server">
                <ContentTemplate>
                    <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important; max-width: 800px;">
                        <tr>
                            <th colspan="3" style="font-size: 12pt; font-weight: bold; border-bottom: 1px solid Gray;">Buscar Clase producto</th>
                        </tr>
                        <tr>
                            <td>
                                <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Clase Producto:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtClaseProducto" runat="server" ClientIDMode="Static"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RfvClaseproducto" Display="Dynamic" runat="server" ControlToValidate="txtClaseProducto" ValidationGroup="BuscarClaseProducto">*</asp:RequiredFieldValidator>
                            </td>
                            <td style="padding-left: 30px; text-align: right; vertical-align: middle;">
                                <asp:Button ID="BtnBuscarClaseProducto" runat="server" Text="Buscar" SkinID="boton_copia" OnClick ="BtnBuscarClaseProducto_Click" ValidationGroup="BuscarClaseProducto" />
                            </td>
                        </tr>
                    </table>
                    <div style="overflow: auto; max-width: 800px; max-height: 700px;">
                        <asp:GridView ID="GrvTipoProducto" runat="server" Width="100%" 
                            SkinID="grilla" AllowPaging="True" AllowSorting="True" PageSize="12" 
                            EmptyDataText="No se encontraron datos"
                            AutoGenerateColumns="False" CellPadding="2" CellSpacing="1"
                            GridLines="None" ShowFooter="True" 
                            DataKeyNames="CLASE_RECURSO_ID, CLASE_PRODUCTO_ID" 
                            OnRowEditing= "GrvClaseProducto_RowEditing"
                            OnPageIndexChanging= "GrvClaseProducto_PageIndexChanging">
                            <HeaderStyle Font-Size="9pt" />
                            <FooterStyle Font-Size="9pt" ForeColor="#000000" cssclass="texto_tablas_paginador" />
                            <RowStyle Font-Size="9pt" ForeColor="#000000" />
                            <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                            <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                            <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                            <columns>
                                <asp:TemplateField HeaderText = "Clase Recurso">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblClaseRecursoClaseProducto" Text = '<%# Eval("CLASE_RECURSO") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText = "Clase producto">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbClaseProducto" Text = '<%# Eval("CLASE_PRODUCTO") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText = "Codigo IDEAM">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCodigoIdeamClaseProducto" Text = '<%# Eval("CODIGO_IDEAM") %>' SkinID="etiqueta_negra"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText = "Visible Salvoconductos" ControlStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                           <asp:CheckBox ID="GrvChkSalvoconductoTipoProducto" runat="server" Checked='<%# Eval("SALVOCONDUCTO") %>' Enabled="false" SkinID="etiqueta_negra"/>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText = "Visible Aprovechamientos" ControlStyle-Width="200px" ItemStyle-HorizontalAlign="Center">    
                                <ItemTemplate>
                                        <asp:CheckBox ID="GrvChkAprovechamientosTipoProducto" runat="server" Checked='<%# Eval("APROVECHAMIENTO") %>' Enabled="false" SkinID="etiqueta_negra"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText = "Seleccionar">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" id="lnkSeleccionar" CommandName="Edit" CssClass="a_orange">Seleccionar</asp:LinkButton>                 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                </columns>
                            <%--<rowstyle cssclass="texto_tablas" />
                            <pagerstyle cssclass="texto_tablas_paginador" horizontalalign="Left" />
                            <headerstyle cssclass="titulo_tablas" />
                            <alternatingrowstyle cssclass="texto_tablas_dos" />--%>
                            <PagerSettings FirstPageImageUrl="../App_Themes/Img/pagina_primera.gif" FirstPageText="Primera"
                                LastPageImageUrl="../App_Themes/Img/pagina_ultima.gif" LastPageText="Ultima" Mode="NumericFirstLast"
                                NextPageImageUrl="../App_Themes/Img/pagina_siguiente.gif" NextPageText="Siguiente" PreviousPageImageUrl="../App_Themes/Img/pagina_anterior.gif"
                                PreviousPageText="Anterior" />
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div style="padding-top: 20px; text-align: center; vertical-align: middle; width: 100%;">
                <asp:Button ID="Button3" runat="server" Text="Cerrar" CausesValidation="False" SkinID="boton_copia" />
            </div>
        </asp:Panel>




    <asp:Label ID="LblMpeTipoProducto" runat="server" SkinID="etiqueta_negra"></asp:Label>
        <cc1:ModalPopupExtender ID="MpeTipoProducto" runat="server"
            TargetControlID="LblMpeTipoProducto"
            PopupControlID="pnlTipoProducto"
            DropShadow="True" Enabled="True" DynamicServicePath=""
            BackgroundCssClass="FondoAplicacion">
        </cc1:ModalPopupExtender>

    <asp:Panel ID="pnlTipoProducto" runat="server" Style="display: none; max-width: 800px; max-height: 700px;" CssClass="CajaDialogo" ScrollBars="Vertical">
        <asp:UpdatePanel ID="UplTipoProducto" runat="server">
            <ContentTemplate>
                <table style="left: 0 !important; margin: 0 !important; padding: 0 !important; border: 0px solid #ddd !important; max-width: 800px;">
                    <tr>
                        <th colspan="3" style="font-size: 12pt; font-weight: bold; border-bottom: 1px solid Gray;">Buscar tipo producto</th>
                    </tr>
                    <tr>
                        <td>
                            <label style="font-family: Arial, Helvetica, sans-serif; font-size: 10pt; color: #000000;">Tipo Producto:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBuscarTipoproducto" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFVtxtTipoproducto" Display="Dynamic" runat="server" ControlToValidate="txtBuscarTipoproducto" ValidationGroup="BuscarTipoProducto">*</asp:RequiredFieldValidator>
                        </td>
                        <td style="padding-left: 30px; text-align: right; vertical-align: middle;">
                            <asp:Button ID="BtnBuscarTipoProducto" runat="server" Text="Buscar" SkinID="boton_copia" OnClick="BtnBuscarTipoProducto_Click" ValidationGroup="BuscarTipoProducto" />
                        </td>
                    </tr>
                </table>
                <div style="overflow: auto; max-width: 800px; max-height: 700px;">
                    <asp:GridView ID="GrvTipoClaseProducto" runat="server" Width="100%"
                        SkinID="grilla" AllowPaging="True" AllowSorting="True" PageSize="12"
                        EmptyDataText="No se encontraron datos"
                        AutoGenerateColumns="False" CellPadding="2" CellSpacing="1"
                        GridLines="None" ShowFooter="True"
                        DataKeyNames="TIPO_PRODUCTO_ID"
                        OnRowEditing="GrvTipoClaseProducto_RowEditing"
                        OnPageIndexChanging="GrvTipoClaseProducto_PageIndexChanging">
                        <HeaderStyle Font-Size="9pt" />
                        <FooterStyle Font-Size="9pt" ForeColor="#000000" CssClass="texto_tablas_paginador" />
                        <RowStyle Font-Size="9pt" ForeColor="#000000" />
                        <SelectedRowStyle Font-Size="9pt" ForeColor="#000000" />
                        <EditRowStyle Font-Size="9pt" ForeColor="#000000" />
                        <AlternatingRowStyle Font-Size="9pt" ForeColor="#000000" />
                        <Columns>
                            <asp:TemplateField HeaderText="Tipo Producto">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblNomLblTipoProducto" Text='<%# Eval("TIPO_PRODUCTO") %>' SkinID="etiqueta_negra"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Codigo IDEAM" ControlStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblCodigoIdeamTipoProducto" Text='<%# Eval("CODIGO_IDEAM") %>' SkinID="etiqueta_negra"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Visible Salvoconductos" ControlStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="GrvChkSalvoconductoTipoProducto" runat="server" Checked='<%# Eval("SALVOCONDUCTO") %>' Enabled="false" SkinID="etiqueta_negra" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Visible Aprovechamientos" ControlStyle-Width="200px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="GrvChkAprovechamientosTipoProducto" runat="server" Checked='<%# Eval("APROVECHAMIENTO") %>' Enabled="false" SkinID="etiqueta_negra" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Formula" ControlStyle-Width="100px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblFormula" Text='<%# Eval("FORMULA") %>' SkinID="etiqueta_negra"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Seleccionar">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkSeleccionar" CommandName="Edit" CssClass="a_orange">Seleccionar</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <PagerSettings FirstPageImageUrl="../App_Themes/Img/pagina_primera.gif" FirstPageText="Primera"
                            LastPageImageUrl="../App_Themes/Img/pagina_ultima.gif" LastPageText="Ultima" Mode="NumericFirstLast"
                            NextPageImageUrl="../App_Themes/Img/pagina_siguiente.gif" NextPageText="Siguiente" PreviousPageImageUrl="../App_Themes/Img/pagina_anterior.gif"
                            PreviousPageText="Anterior" />
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="padding-top: 20px; text-align: center; vertical-align: middle; width: 100%;">
            <asp:Button ID="Button4" runat="server" Text="Cerrar" CausesValidation="False" SkinID="boton_copia" />
        </div>
    </asp:Panel>


        <script src="../Scripts/jquery-1.9.1.js"></script>

    </asp:Content>
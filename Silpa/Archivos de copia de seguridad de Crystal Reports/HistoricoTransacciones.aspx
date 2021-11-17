<%@ Page Language="C#"  MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="HistoricoTransacciones.aspx.cs" Inherits="PagoElectronico_HistoricoTransacciones" Title="Historico de Transacciones PSE" %>

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

    <asp:ScriptManager ID="scmManager" runat="server"></asp:ScriptManager>

    <div class="div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="HISTÓRICO DE TRANSACCIONES PSE" SkinID="titulo_principal_blanco"></asp:Label>
    </div>

    <div class="contact_form" id="divConsultaHistoricos" runat="server">
        <asp:UpdatePanel runat="server" ID="upnlBuscar" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="TableBuscarNot">
                    <div class="RowBuscarTitulo">
                        <div class="CellBuscarTitulo">
                            <asp:Literal ID="ltlTituloBuscador" runat="server" Text="FILTRO DE BÚSQUEDA"></asp:Literal>                    
                        </div>
                    </div>
                    <div class="RowBuscarNot">
                        <div class="CellBuscarNot">
                            <div class="TableBuscarInternoNot">
                                <div class="RowBuscarNot">
                                    <div class="CellBuscarNot">
                                        <label for="cboMarcaVehiculo">Número VITAL:</label>
                                        <asp:TextBox runat="server" ID="txtNumeroVital" ClientIDMode="Static" MaxLength="20"></asp:TextBox>                                        
                                    </div>
                                    <div class="CellBuscarNot">
                                        <label for="cboMarcaVehiculo">CUS:</label>
                                        <asp:TextBox runat="server" ID="txtCUS" ClientIDMode="Static" MaxLength="18"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="rexCUS" runat="server" Display="Dynamic" ValidationGroup="NotBuscar" ErrorMessage="Ingrese un número CUS válido" ControlToValidate="txtCUS" ValidationExpression="\d*">&nbsp;</asp:RegularExpressionValidator>
                                    </div>
                                </div>                                
                                <div class="RowBuscarNot">
                                    <div class="CellBuscarNot">
                                        <label for="txtFechaDesde">Fecha Transacción Desde:</label>
                                        <asp:TextBox ID="txtFechaDesde" runat="server" ClientIDMode="Static"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvFechaDesde" runat="server" ControlToValidate="txtFechaDesde" ErrorMessage="Ingrese la Fecha Desde la cual desea buscar." Text="*" ValidationGroup="NotBuscar" InitialValue=""></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rexFechaDesde" runat="server" ControlToValidate="txtFechaDesde" ErrorMessage="Formato de fecha no valido para la Fecha Desde." ValidationExpression="^\d{2}\/\d{2}\/\d{4}" Width="3px" Text="*" ValidationGroup="NotBuscar"></asp:RegularExpressionValidator>
                                        <cc1:CalendarExtender ID="calFechaDesde" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaDesde"/>
                                    </div>
                                    <div class="CellBuscarNot">
                                        <label for="txtFechaHasta">Fecha Transacción Hasta:</label>
                                        <asp:TextBox ID="txtFechaHasta" runat="server" ClientIDMode="Static"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvFechaHasta" runat="server" ControlToValidate="txtFechaHasta" ErrorMessage="Ingrese la Fecha Hasta la cual desea buscar." Text="*" ValidationGroup="NotBuscar" InitialValue=""></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rexFechaHasta" runat="server" ControlToValidate="txtFechaHasta" ErrorMessage="Formato de fecha no valido para la Fecha Hasta." ValidationExpression="^\d{2}\/\d{2}\/\d{4}" Width="3px" Text="*" ValidationGroup="NotBuscar"></asp:RegularExpressionValidator>
                                        <cc1:CalendarExtender ID="calFechaHasta" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaHasta" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="RowButton">
                        <div class="CellButton">
                            <asp:HiddenField runat="server" ID="hdfNumeroVital" />
                            <asp:HiddenField runat="server" ID="hdfCUS" />
                            <asp:HiddenField runat="server" ID="hdfFechaDesde" />
                            <asp:HiddenField runat="server" ID="hdfFechaHasta" />
                            <asp:Button runat="server" ID="cmdBuscar" ValidationGroup="NotBuscar" Text="Buscar" ClientIDMode="Static" OnClick="cmdBuscar_Click"/>
                            <asp:ValidationSummary ID="valNotBuscar" runat="server" ValidationGroup="NotBuscar" ShowMessageBox="true" ShowSummary="false" />
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="cmdBuscar" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <asp:UpdateProgress ID="uppBuscar" runat="server" AssociatedUpdatePanelID="upnlBuscar">
        <ProgressTemplate>  
            <div id="ModalProgressContainer">
                <div>
                    <p>Procesando...</p>
                    <p><asp:Image ID="imgBuscarProgress" runat="server" SkinId="procesando"/></p>
                </div>
            </div>                         
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="upnlMensaje" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="contact_form" id="divMensaje" runat="server" visible="false">  
                <div class="Table">
                    <div class="Row">
                        <div class="CellMensaje">
                            <asp:Label runat="server" ID="lblMensaje"></asp:Label>
                        </div>
                    </div>
                </div>            
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br /><br />
    <asp:UpdatePanel runat="server" ID="upnlTransacciones" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="contact_form" id="divTransacciones" runat="server">        
                <div class="TableLiquidacion">
                    <div class="Row">
                        <div class="CellGridView">

                            <asp:GridView runat="server" AutoGenerateColumns="False" ID="grdTransacciones" SkinID="GrillaAutoliquidacion" AllowPaging="true" Width="100%" PageSize="15" OnPageIndexChanging="grdTransacciones_PageIndexChanging" ShowHeaderWhenEmpty="true" EmptyDataText="No se encontraron transacciones de pagos">
                                <Columns>
                                    <asp:TemplateField HeaderText = "CUS" ItemStyle-CssClass="CellContenidoLiquidacionGridViewDatosCentrado">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCus" runat="server" Text='<%# Eval("CUS") %>'></asp:Label>
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText = "REFERENCIA PAGO" ItemStyle-CssClass="CellContenidoLiquidacionGridViewDatosCentrado">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReferencia" runat="server" Text='<%# Eval("REFERENCIA_PAGO") %>'></asp:Label>
                                        </ItemTemplate>                                
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText = "NÚMERO VITAL" ItemStyle-CssClass="CellContenidoLiquidacionGridViewDatosCentrado">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNumeroVital" runat="server" Text='<%# Eval("NUMERO_VITAL") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText = "EXPEDIENTE" ItemStyle-CssClass="CellContenidoLiquidacionGridViewDatosCentrado">
                                        <ItemTemplate>
                                            <asp:Label ID="lblExpediente" runat="server" Text='<%# Eval("EXPEDIENTE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText = "FECHA TRANSACCIÓN" ItemStyle-CssClass="CellContenidoLiquidacionGridViewDatosCentrado">
                                        <ItemTemplate>
                                            <asp:Label ID="lblValor" runat="server" Text='<%# (Eval("FECHA_REGISTRO") != System.DBNull.Value ? Convert.ToDateTime(Eval("FECHA_REGISTRO")).ToString("dd/MM/yyyy HH:mm") : "-") %>'></asp:Label>
                                        </ItemTemplate>                                
                                    </asp:TemplateField>                            
                                    <asp:TemplateField HeaderText = "VALOR PAGO" ItemStyle-CssClass="CellContenidoLiquidacionGridViewDatosCentrado">
                                        <ItemTemplate>
                                            <asp:Label ID="lblValor" runat="server" Text='<%# string.Format("{0:C}", Eval("VALOR_PAGO")) %>'></asp:Label>
                                        </ItemTemplate>                                
                                    </asp:TemplateField>   
                                    <asp:TemplateField HeaderText = "ESTADO TRANSACCIÓN" ItemStyle-CssClass="CellContenidoLiquidacionGridViewDatosCentrado">
                                        <ItemTemplate>
                                            <asp:Label ID="lblExpediente" runat="server" Text='<%# Eval("ESTADO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                         
                                </Columns>
                            </asp:GridView> 
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="uppTransacciones" runat="server" AssociatedUpdatePanelID="upnlTransacciones">
        <ProgressTemplate>  
            <div id="ModalProgressContainer">
                <div>
                    <p>Procesando...</p>
                    <p><asp:Image ID="imgPaginaProgress" runat="server" SkinId="procesando"/></p>
                </div>
            </div>                         
        </ProgressTemplate>
    </asp:UpdateProgress>

    <div class="TableLiquidacion">
        <div class="Row">
            <div class="CellFormularioBotonPagoDatos">
                <asp:Image id="imgPSE"  runat="server" ImageUrl="~/App_Themes/Img/PSE/headerImage1.png" BorderWidth="0" Width="100px"></asp:Image>
            </div>
        </div>
    </div>

</asp:Content>
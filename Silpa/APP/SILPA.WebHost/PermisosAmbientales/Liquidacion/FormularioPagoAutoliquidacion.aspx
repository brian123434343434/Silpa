<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="FormularioPagoAutoliquidacion.aspx.cs" Inherits="PermisosAmbientales_Liquidacion_FormularioPagoAutoliquidacion" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="../../jquery/jquery.js" type="text/javascript"></script>
    <link href="../../jquery/EstiloGris/jquery-ui.css" rel="stylesheet" />
    <script src="../../jquery/EstiloGris/jquery-ui.js"  type="text/javascript"></script>
    <script language="javascript" type="text/javascript">

        function confirmarAvanzar() {
            return confirm("Si avanza a Registrar Pago no podrá consultar la factura ni efectuar el pago por PSE. Solo podrá anexar el comprobante del pago realizado por medio de la taquilla del banco. ¿En realidad desea avanzar a Registrar el Pago?.");
        }

        function cerrarVentana() {
            window.opener.location.reload();
            window.close();
        }

        $(function () {
            $('#cmdVerFactura').click(function (e) {
                window.open("FacturaPago.aspx?IDProcessInstance=" + $("#hdfInstancia").val(), "Factura", "fullscreen=false,menubar=false,toolbar=false,scrollbars=yes");
            });

            $('#cmdImprimir').click(function (e) {
                $("#divPago").hide();
                window.print();
                $("#divPago").show();
            });

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                $('#cmdVerFactura').click(function (e) {
                    window.open("FacturaPago.aspx?IDProcessInstance=" + $("#hdfInstancia").val(), "Factura", "fullscreen=false,menubar=false,toolbar=false,scrollbars=yes");
                });

                $('#cmdImprimir').click(function (e) {
                    $("#divPago").hide();
                    window.print();
                    $("#divPago").show();
                });
            });
        });
    </script>

     <div class="div-titulo">
        <a href="#" id="hrfCerrarVentana" runat="server" onclick="window.close();return false;" class="Salir">Salir [X]</a>
        <br />
        <asp:Label ID="lblTitulo" runat="server" Text="INFORMACIÓN DE PAGO LIQUIDACIÓN" SkinID="titulo_principal_blanco"></asp:Label>
    </div>
    <div class="div-contenido">
        <asp:ScriptManager ID="scmManejador" runat="server" EnableScriptGlobalization="True">
        </asp:ScriptManager>
    </div>
    <div class="contact_form" id="divMensajeError" runat="server" visible="false">
        <div class="TableBuscar">
            <div class="RowBuscar">
                <div class="CellMensaje">
                    <asp:Label runat="server" ID="lblMensaje"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="upnlPago">
        <ContentTemplate>
            <div class="contact_form" id="divConsultaCertificado" runat="server">
                <div class="TableLiquidacion">
                    <div class="Row">
                        <div class="CellMensajeLiquidacion">
                            <asp:Literal runat="server" ID="ltlMensajeLiquidacion" Text="A continuación se presenta la información de pago para cada uno de los conceptos que hace parte de la liquidación."></asp:Literal>
                        </div>
                    </div>
                </div>
                <div class="TableLiquidacion">
                    <div class="Row">
                        <div class="TituloSeccionesLiquidacion">
                            Servicio
                        </div>
                    </div>
                    <div class="Row">
                        <div class="CellDatosLiquidacion">
                            <div runat="server" id="tblAutoliquidacion" class="TableLiquidacionDatos">
                                <div class="Row">
                                    <div class="CellTituloLiquidacionDatos">
                                        SERVICIO
                                    </div>
                                    <div class="CellTituloLiquidacionDatos">
                                        AUTORIDAD
                                    </div>
                                    <div class="CellTituloLiquidacionDatos">
                                        VALOR
                                    </div>
                                </div>
                                <div class="Row">
                                    <div class="CellContenidoLiquidacionDatos">
                                        <asp:Literal runat="server" ID="ltlDescripcionPago"></asp:Literal>
                                    </div>
                                    <div class="CellContenidoLiquidacionDatosCentrado">
                                        ANLA
                                    </div>
                                    <div class="CellContenidoLiquidacionDatosCentrado">
                                        <asp:Literal runat="server" ID="ltlValorPago"></asp:Literal>
                                    </div>
                                </div>
                            </div>

                            <div runat="server" id="dvCobroSeguimiento" class="TableLiquidacionDatos">
                                <div class="Row">
                                    <div class="CellTituloLiquidacionDatos">
                                        SERVICIO
                                    </div>
                                    <div class="CellTituloLiquidacionDatos">
                                        EXPEDIENTE
                                    </div>
                                    <div class="CellTituloLiquidacionDatos">
                                        AUTORIDAD
                                    </div>
                                    <div class="CellTituloLiquidacionDatos">
                                        VALOR
                                    </div>
                                    <div class="CellTituloLiquidacionDatos">
                                        FECHA LÍMITE DE PAGO
                                    </div>
                                </div>
                                <div class="Row">
                                    <div class="CellContenidoLiquidacionDatos">
                                        <asp:Literal runat="server" ID="ltlDescripcionPagoCS"></asp:Literal>
                                    </div>
                                    <div class="CellContenidoLiquidacionDatosCentrado">
                                        <asp:Literal runat="server" ID="ltlExpedineteCS"></asp:Literal>
                                    </div>
                                    <div class="CellContenidoLiquidacionDatosCentrado">
                                        ANLA
                                    </div>
                                    <div class="CellContenidoLiquidacionDatosCentrado">
                                        <asp:Literal runat="server" ID="ltlValorPagoCS"></asp:Literal>
                                    </div>
                                    <div class="CellContenidoLiquidacionDatosCentrado">
                                        <asp:Literal runat="server" ID="ltlFechaLimitePagoCS"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>            
                    <div class="Row" runat="server" id="divPago" clientidmode="Static">
                        <div class="CellPagoServicio">
                            <asp:Literal runat="server" ID="ltlTituloPago" Text="Pagar Servicio:" />
                            <asp:DropDownList runat="server" ID="cboPagar" OnSelectedIndexChanged="cboPagar_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="-1" Text="Seleccione...."></asp:ListItem>
                                <asp:ListItem Value="1" Text="Seleccione....">Débito Bancario PSE</asp:ListItem>
                                <asp:ListItem Value="2" Text="Seleccione....">Taquilla Banco</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button runat="server" ID="cmdVerFactura" Text="Ver Soporte de Pago" Visible="false" ClientIDMode="Static" />
                            <asp:Button runat="server" ID="cmdFinalizarTarea" Text="Registrar Pago" Visible="false" ClientIDMode="Static" OnClick="cmdFinalizarTarea_Click" OnClientClick="javascript:return confirmarAvanzar();" />
                            <asp:HiddenField runat="server" ID="hdfInstancia" ClientIDMode="Static" />
                        </div>
                    </div>
                    <div class="Row" runat="server" id="divPagoPSE" visible="false">
                        <div class="CellBotonPagoServicio">
                            <asp:ImageButton ID="ImgPse" runat="server" ImageUrl="~/App_Themes/Img/PSE/headerImage1.png" OnClick="ImgPse_Click" />
                        </div>
                    </div>
                    <div class="Row" runat="server" id="divMensajePago" visible="false">
                        <div class="Cell">
                            <div class="TableLiquidacion">
                                <div class="Row">
                                    <div class="CellAnotacionRojoLiquidacion">
                                        <asp:Literal runat="server" ID="ltlMensajePago"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="TableLiquidacion" runat="server" id="divPermisos">
                    <div class="Row">
                        <div class="TituloSeccionesLiquidacion">
                            Permisos
                        </div>
                    </div>
                    <div class="Row">
                        <div class="Cell">
                            <div class="TableLiquidacionDatos">
                                <div class="Row">
                                    <div class="CellGridView">
                                        <asp:GridView runat="server" AutoGenerateColumns="False" ID="grdPermisos" SkinID="GrillaAutoliquidacion" AllowPaging="False" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText = "PERMISO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMarca" runat="server" Text='<%# Eval("Permiso") %>'></asp:Label>
                                                    </ItemTemplate>                                
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText = "AUTORIDAD" ItemStyle-CssClass="CellContenidoLiquidacionGridViewDatosCentrado">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTipoPrueba" runat="server" Text='<%# Eval("Autoridad") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText = "VALOR" ItemStyle-CssClass="CellContenidoLiquidacionGridViewDatosCentrado">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblResolucion" runat="server" Text='<%# string.Format("{0:C}", Eval("ValorTotal")) %>'></asp:Label>
                                                    </ItemTemplate>                                
                                                </asp:TemplateField>                            
                                            </Columns>
                                        </asp:GridView>  
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="Row">
                        <div class="Cell">
                            <div class="TableLiquidacion">
                                <div class="Row">
                                    <div class="CellAnotacionLiquidacion">
                                        * NOTA: El pago de cada uno de los permisos debe realizarse directamente ante la autoridad correspondiente.
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="CellBotonesLiquidacionInferior">
                    <asp:Button runat="server" ID="cmdImprimir" Text="Imprimir" ClientIDMode="Static" />                        
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="cboPagar" EventName="SelectedIndexChanged" />
            <asp:PostBackTrigger ControlID="ImgPse" />
            <asp:PostBackTrigger ControlID="cmdFinalizarTarea" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
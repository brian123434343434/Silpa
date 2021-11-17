<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPASinMenuFlash.master" AutoEventWireup="true" CodeFile="InformacionTransaccion.aspx.cs" Inherits="PagoElectronico_InformacionTransaccion" Title="Respuesta Débito Bancario PSE" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script language="javascript" type="text/javascript">        
        function Imprimir() {
            window.print();
            return false;
        }

        function cerrarVentana() {
            window.opener.location.reload();
            window.close();
        }
    </script>

    <div class="div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="DÉBITO BANCARIO PSE - INFORMACIÓN TRANSACCIÓN" SkinID="titulo_principal_blanco"></asp:Label>
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
    <div class="contact_form" id="divInformacionTransaccion" runat="server">
        <div class="TableLiquidacionDatos">
            <div class="Row">
                <div class="CellFormularioTituloPagoDatos">
                    Estado:
                </div>
                <div class="CellFormularioPagoDatos" runat="server" id="dvValorEstado">
                    <asp:Literal id="lblValorEstado" runat="server"></asp:Literal>
                    <asp:HiddenField runat="server" ID="hdfNombreEstado" />
                </div>
            </div>
            <div class="Row" id="dvObservacion" runat="server">
                <div class="CellFormularioTituloPagoDatos">
                    Observación:
                </div>
                <div class="CellFormularioPagoDatos" runat="server" id="dvValorObservacion">
                    <asp:Literal id="lblObservacion" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="Row">
                <div class="CellFormularioTituloPagoDatos">
                    Transacción/(CUS):
                </div>
                <div class="CellFormularioPagoDatos">
                    <asp:Literal id="lblValorTransaccion" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="Row">
                <div class="CellFormularioTituloPagoDatos">
                    Valor:
                </div>
                <div class="CellFormularioPagoDatos">
                    <asp:Literal id="lblValorTotal" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="Row">
                <div class="CellFormularioTituloPagoDatos">
                    Factura:
                </div>
                <div class="CellFormularioPagoDatos">
                    <asp:Literal id="lblValorFactura" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="Row">
                <div class="CellFormularioTituloPagoDatos">
                    Fecha Solicitud:
                </div>
                <div class="CellFormularioPagoDatos">
                    <asp:Literal id="lblValorFecha" runat="server"></asp:Literal>
                    <asp:HiddenField runat="server" ID="hdfFechaTransaccion" />
                </div>
            </div>
            <div class="Row">
                <div class="CellFormularioTituloPagoDatos">
                    Banco:
                </div>
                <div class="CellFormularioPagoDatos">
                    <asp:Literal id="lblValorBanco" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="Row">
                <div class="CellFormularioTituloPagoDatos">
                    Nit:
                </div>
                <div class="CellFormularioPagoDatos">
                    <asp:Literal id="lblValorNit" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="Row">
                <div class="CellFormularioTituloPagoDatos">
                    Razón Social:
                </div>
                <div class="CellFormularioPagoDatos">
                    <asp:Literal id="lblValorRazonSocial" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="Row">
                <div class="CellFormularioTituloPagoDatos">
                    IP Pública:
                </div>
                <div class="CellFormularioPagoDatos">
                    <asp:Literal id="lblIpPublica" runat="server"></asp:Literal>
                </div>
            </div>
            <div class="Row">
                <div class="CellFormularioTituloPagoDatos">
                    Descripción:
                </div>
                <div class="CellFormularioPagoDatos">
                    <asp:Literal id="lblDescripcion" runat="server"></asp:Literal>
                </div>
            </div>
        </div>

        <div class="TableLiquidacion">
            <div class="Row">
                <div class="CellFormularioBotonPagoDatos">
                    <asp:button id="btnRegresar" runat="server" Text="Finalizar Transacción" OnClick="btnRegresar_Click" Visible="false"></asp:button>
                    <asp:button id="btnVerHistorico" runat="server" Text="Ver Historial Transacciones" OnClick="btnVerHistorico_Click" Visible="false"></asp:button>
                    <asp:button id="btnReintentar" runat="server" Text="Reintentar Pago" OnClick="btnReintentar_Click" Visible="false"></asp:button>
                    <input type="submit" value="Imprimir" onclick="javascript: Imprimir(); return false;" class="button" />
                </div>
            </div>
            <div class="Row">
                <div class="CellFormularioBotonPagoDatos">
                    <asp:Image id="Image1" style="Z-INDEX: 101; LEFT: 41px; TOP: 39px" runat="server" ImageUrl="~/App_Themes/Img/PSE/headerImage1.png"></asp:Image>
                </div>
            </div>
        </div>
    </div>

    
</asp:Content>


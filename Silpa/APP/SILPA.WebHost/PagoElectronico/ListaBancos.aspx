<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="ListaBancos.aspx.cs" Inherits="PagoElectronico_ListaBancos" Title="Débito Bancario PSE" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="../App_Themes/skin/StylePDV.css" rel="stylesheet" />  

    <script type="text/javascript">

        function CerrarVentana() {
            window.close();
        }

    </script>
        

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="div-titulo">
        <a href="#" id="hrfCerrarVentana" runat="server" onclick="window.close();return false;" class="Salir">Salir [X]</a>
        <br />
        <asp:Label ID="lblTitulo" runat="server" Text="DÉBITO BANCARIO PSE" SkinID="titulo_principal_blanco"></asp:Label>
    </div>
    <asp:UpdatePanel runat="server" ID="upnlMensajeError" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="contact_form" id="divMensajeError" runat="server">
                <div class="TableBuscar">
                    <div class="RowBuscar">
                        <div class="CellMensaje">
                            <asp:Literal runat="server" ID="lblMensaje"></asp:Literal>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="upnlBancos" UpdateMode="Conditional">
        <ContentTemplate>            
            <div class="contact_form" id="divBancos" runat="server">
                <div class="TableLiquidacionDatos">            
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
                            Nit:
                        </div>
                        <div class="CellFormularioPagoDatos">
                            <asp:Literal id="lblValorNit" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <div class="Row">
                        <div class="CellFormularioTituloPagoDatos">
                            Tipo de Persona:
                        </div>
                        <div class="CellFormularioPagoDatos">
                            <asp:DropDownList ID="cboTipoUsuario" runat="server">
                                <asp:ListItem>NATURAL</asp:ListItem>
                                <asp:ListItem>CORPORATIVO</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="Row">
                        <div class="CellFormularioTituloPagoDatos">
                            Total a Pagar:
                        </div>
                        <div class="CellFormularioPagoDatos">
                            <asp:Literal id="lblValorTotal" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <div class="Row">
                        <div class="CellFormularioTituloPagoDatos">
                            IVA:
                        </div>
                        <div class="CellFormularioPagoDatos">
                            <asp:Literal id="lblValorIva" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <div class="Row">
                        <div class="CellFormularioTituloPagoDatos">
                            Descripción:
                        </div>
                        <div class="CellFormularioPagoDatos">
                            <asp:Literal id="lblValorDescripcion" runat="server"></asp:Literal>
                        </div>
                    </div>
                    <div class="Row">
                        <div class="CellFormularioTituloPagoDatos">
                            Entidad Financiera:                    
                        </div>
                        <div class="CellFormularioPagoDatos">
                            <asp:DropDownList runat="server" ID="cboEntidadFinanciera"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" ID="rfvEntidadFinanciera" ControlToValidate="cboEntidadFinanciera" InitialValue="-1" ErrorMessage="Seleccione la entidad financiera">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>  
                <div class="TableLiquidacion">
                    <div class="Row">
                        <div class="CellFormularioBotonPagoDatos">
                            <asp:Button id="btnConfirmar" runat="server" Text="Pagar" OnClick="btnConfirmar_Click"></asp:Button>
                            <asp:Button ID="btnIntTarde" runat="server" OnClientClick="CerrarVentana();"  Text="Intentar Más Tarde" Visible="False" CausesValidation="false"/>
                            <asp:ValidationSummary runat="server" ID="valBancos" ShowSummary="false" ShowMessageBox="true" />
                        </div>
                    </div>                    
                </div>      
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnConfirmar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnIntTarde" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="uppBancos" runat="server" AssociatedUpdatePanelID="upnlBancos">
        <ProgressTemplate>  
            <div id="ModalProgressContainer">
                <div>
                    <p>En proceso de crear la transacción...</p>
                    <p><asp:Image ID="imgUpdateProgress" runat="server" SkinId="procesando"/></p>
                </div>
            </div>                         
        </ProgressTemplate>
    </asp:UpdateProgress>    
    <div class="contact_form">
        <div class="TableLiquidacion">
            <div class="Row">
                <div class="CellFormularioBotonPagoDatos">
                    <asp:Image id="imgEncabezado" style="Z-INDEX: 101; LEFT: 41px; TOP: 39px" runat="server" ImageUrl="~/App_Themes/Img/PSE/headerImage1.png" BorderWidth="0"></asp:Image>
                </div>
            </div>
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="upnlMensajeError2" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="contact_form" id="divMensajeError2" runat="server">
                <div class="TableBuscar">
                    <div class="RowBuscar">
                        <div class="CellMensaje">
                            <asp:Literal runat="server" ID="lblMenasje2"></asp:Literal>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


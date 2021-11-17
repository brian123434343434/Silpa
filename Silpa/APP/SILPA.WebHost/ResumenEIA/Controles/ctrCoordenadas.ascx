<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrCoordenadas.ascx.cs" Inherits="ResumenEIA_Controles_ctrCoordenadas" %>

<asp:Panel ID="pnlOriginal" runat="server">
<table width="100%" align="right" style="border: thin solid #7F9DB9;border-style: solid;">
    <asp:PlaceHolder runat="server" ID="plhBotonNuevo">
        <tr>                                                          
            <td width="100%" colspan="4" align="right">
                <asp:Button runat="server" ID="btnNuevaCoordenada" Text="Agregar Coordenada" 
                    onclick="btnNuevaCoordenada_Click" SkinID="boton_copia" />
            </td>
        </tr>
    </asp:PlaceHolder>
    
    
    <asp:PlaceHolder runat="server" ID="plhIngCoordenadas" Visible="false">
        
        <tr>
            <td width="100%" colspan="4" align="right">
                <table width="100%">
                    <tr>
                        <th width="50%" align="center"><asp:Label ID="Label6" runat="server" SkinID="etiqueta_negra" 
                                Text="Coordenada Norte"></asp:Label></th>
                        <th width="50%" align="center"><asp:Label ID="Label17" runat="server" SkinID="etiqueta_negra" 
                                Text="Coordenada Este"></asp:Label></th>
                    </tr>
                    <tr>
                        <td width="50%" align="center">
                            <asp:TextBox ID="txtCoorNorte" runat="server" 
                            SkinID="texto_sintamano" MaxLength="200" width="70%"></asp:TextBox>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender9" runat="server" 
                                TargetControlID="txtCoorNorte" Mask="9999999.9999" MaskType="Number" AcceptNegative="Left" 
                                InputDirection="RightToLeft" ErrorTooltipEnabled="True" 
                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"/>
                            <asp:RangeValidator ID="RangeValidator3" runat="server" ErrorMessage="Coordenada Norte no válida, debe estar entre -4,342327 y 14,993886"
                            ControlToValidate="txtCoorNorte" Display="Dynamic" MinimumValue="-4,342327"
                            MaximumValue="14,993886" Type="Double" ValidationGroup="Coord">*</asp:RangeValidator>
                            <asp:RequiredFieldValidator ID="rfvCoorNorte" runat="server" 
                                ErrorMessage="Ingrese la coordenada Norte"
                                Display="Dynamic" ControlToValidate="txtCoorNorte"
                                ValidationGroup="Coord">*</asp:RequiredFieldValidator>
                        </td>
                        <td width="30%" align="center"><asp:TextBox ID="txtCoorEste" runat="server" 
                            SkinID="texto_sintamano" MaxLength="200" width="70%"
                            ></asp:TextBox>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender10" runat="server" 
                                TargetControlID="txtCoorEste" Mask="9999999.9999" MaskType="Number" 
                                InputDirection="RightToLeft"  ErrorTooltipEnabled="True" 
                                CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" />
                            <asp:RangeValidator ID="RangeValidator4" runat="server" ErrorMessage="Coordenada Este no válida, debe estar entre 66,763877 y 82,036925"
                                ControlToValidate="txtCoorEste" Display="Dynamic" MinimumValue="66,763877"
                                MaximumValue="82,036925" Type="Double" ValidationGroup="Coord">*</asp:RangeValidator>
                            <asp:RequiredFieldValidator ID="rfvCoorEste" runat="server" 
                                ErrorMessage="Ingrese la coordenada Este"
                                Display="Dynamic" ControlToValidate="txtCoorEste"
                                ValidationGroup="Coord">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <table width="100%">
                    <tr>
                        <td width="25%"></td>
                        <td align="center" width="25%">
                            <asp:Button ID="btnAgregarCoordenada" runat="server" SkinID="boton_copia"
                                Text="Agregar" ValidationGroup="Coord" onclick="btnAgregarCoordenada_Click" />
                        </td>
                        <td align="center" width="25%">
                            <asp:Button ID="btnCancelarCoordenada" runat="server" SkinID="boton_copia"
                                Text="Cancelar" onclick="btnCancelarCoordenada_Click" />
                        </td>
                        <td align="center" width="25%"></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4" width="100%" align="center">
                <asp:ValidationSummary runat="server" ID="ValidationSumaryCoord" 
                ValidationGroup="Coord" />
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td width="100%" align="center">
            <asp:GridView runat="server" ID="grvCoordenadas" AutoGenerateColumns="False" >
                <Columns>
                    <asp:BoundField DataField="CoorNorte" HeaderText="Coordenada Norte" />
                    <asp:BoundField DataField="CoorEste" HeaderText="Coordenada este" />
                </Columns>
                
            </asp:GridView>
        </td>
    </tr>
    
    <tr>
        <td width="100%" align="center">
            <asp:Label runat="server" ID="lblMensaje" SkinID="etiqueta_negra"></asp:Label>
        </td>
    </tr>
</table>
</asp:Panel>
<asp:Panel ID="pnlGridView" runat="server" Visible="false">
  <asp:GridView runat="server" ID="grvUbicacion" AutoGenerateColumns="False" SkinID="Grilla_simple"
                width="99%"
                    EmptyDataText="No Encontraron Datos De Ubicación">                     
    <Columns>
        <asp:BoundField DataField="COOR_NORTE" HeaderText="Coordenada Norte" />
        <asp:BoundField DataField="COOR_ESTE" HeaderText="Coordenada Este" />
    </Columns>
</asp:GridView>
</asp:Panel>

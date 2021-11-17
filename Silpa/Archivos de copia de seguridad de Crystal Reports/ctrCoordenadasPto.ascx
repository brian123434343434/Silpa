<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrCoordenadasPto.ascx.cs" Inherits="ResumenEIA_Controles_ctrCoordenadasPto" %>

<table width="100%" align="right" style="border: thin solid #7F9DB9;border-style: solid;">
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
                        <asp:RangeValidator ID="RangeValidator3" runat="server" 
                            ErrorMessage="Coordenada Norte no válida, debe estar entre -4,342327 y 14,993886"
                            ControlToValidate="txtCoorNorte" Display="Dynamic" MinimumValue="-4,342327"
                            MaximumValue="14,993886" Type="Double" ValidationGroup="Coord">*</asp:RangeValidator>
                        <asp:RequiredFieldValidator ID="rfvCoorNortePto" runat="server" 
                            ErrorMessage="Ingrese la coordenada Norte"
                            Display="Dynamic" ControlToValidate="txtCoorNorte"
                            ValidationGroup="CoordPto">*</asp:RequiredFieldValidator>
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
                        <asp:RangeValidator ID="RangeValidator4" runat="server" 
                            ErrorMessage="Coordenada Este no válida, debe estar entre 66,763877 y 82,036925"
                            ControlToValidate="txtCoorEste" Display="Dynamic" MinimumValue="66,763877"
                            MaximumValue="82,036925" Type="Double" ValidationGroup="Coord">*</asp:RangeValidator>
                        <asp:RequiredFieldValidator ID="rfvCoorEstePto" runat="server" 
                            ErrorMessage="Ingrese la coordenada Este"
                            Display="Dynamic" ControlToValidate="txtCoorEste"
                            ValidationGroup="CoordPto">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="4" width="100%" align="center">
            <asp:ValidationSummary runat="server" ID="vasCoordPto" 
            ValidationGroup="Coord" />
        </td>
    </tr>
</table>
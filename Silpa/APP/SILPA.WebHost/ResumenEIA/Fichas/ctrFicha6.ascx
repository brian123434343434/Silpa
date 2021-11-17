<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrFicha6.ascx.cs" Inherits="ResumenEIA_Fichas_ctrFicha6" %>
<style type="text/css">
.auto-style1 {
	text-align: left;
}
</style>
<table width ="100%"  >
    <tr>
        <td>
            6. ZONIFICACIONN DE MANEJO AMBIENTAL DE LA ACTIVIDAD
        </td>
    </tr>
    <tr>
        <td class="titleUpdate">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            seleccione item
            <asp:DropDownList ID="cmbGrilla" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbGrilla_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:Button ID="btnAgregarItem" runat="server" Text="Nueva zonificacion" 
                OnClick="btnAgregarItem_Click" SkinID="boton_copia" />
        </td>
    </tr>
    <tr>
        <td class="titleUpdate">
        </td>
    </tr>
    <tr>
        <td>
            <asp:PlaceHolder ID="plhFormulario" runat="server" Visible="False">
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label ID="lblCodMapa" runat="server" Text="Codigo Mapa" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCodigoMapa" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblUnidadZonificacion" runat="server" Text="Unidad de Zonificaci&#243;n Ambiental Asociada"
                            SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUnidadZonificacion" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblVarAmbiental" runat="server" Text="Variable Ambiental que determina la Exclusi&#243;n"
                            SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtVarAmbiental" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblDescripcionGeneral" runat="server" Text="Descripci&#243;n General de la Unidad"
                            SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescripcionGeneral" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblProyectoInterviene" runat="server" Text="Componente del Proyecto que la Interviene"
                            SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtProyectoInterviene" runat="server" Width="100%"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblAreaPtge" runat="server" Text="% del area Total a Intervenir"
                            SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAreaPtge" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblArea" runat="server" Text="Area (m2)" SkinID="etiqueta_negra"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtArea" runat="server"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblLineamientoManejo" runat="server" Text="Lineamiento de Manejo"
                            SkinID="etiqueta_negra"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;" colspan="4">
                        <asp:TextBox ID="txtLineamientoManejo" runat="server" Height="100px" TextMode="MultiLine"
                            Width="70%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td style="text-align: center;">
                        <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" OnClick="btnRegistrar_Click"
                            SkinID="boton_copia" />&nbsp;
                    </td>
                    <td style="text-align: center;">
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click"
                            SkinID="boton_copia" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
            </asp:PlaceHolder>
        </td>
    </tr>
    <tr>
        <td class="titleUpdate">
        </td>
    </tr>
    <tr>
        <td class="titleUpdate">
        </td>
    </tr>
    <tr>
        <td>
            6.1 Areas de exclusion
        </td>
    </tr>
    <tr>
        <td class="titleUpdate">
        </td>
    </tr>
    <tr>
        <td class="auto-style1">
            <asp:GridView ID="grid1" runat="server" AutoGenerateColumns="False" 
                AllowSorting="True" Width="100%" 
                onselectedindexchanged="grid1_SelectedIndexChanged" 
                onrowdeleting="grid1_RowDeleting" 
                EmptyDataText="no se han registrado valores para areas de exclusion">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" />
                    <asp:BoundField AccessibleHeaderText="Codigo del Mapa" DataField="EAR_COD_MAPA" HeaderText="Codigo del Mapa" />
                    <asp:BoundField AccessibleHeaderText="Unidad de Zonificaci&#243;n Ambiental Asociada"
                        DataField="EAR_UNID_ZON_AMB_ASOC" HeaderText="Unidad de Zonificaci&#243;n Ambiental Asociada" />
                    <asp:BoundField AccessibleHeaderText="Variable Ambiental que determina la Exclusi&#243;n"
                        DataField="EAR_VAR_AMB_DETERMINANTE" HeaderText="Variable Ambiental que determina la Exclusi&#243;n" />
                    <asp:BoundField AccessibleHeaderText="Descripci&#243;n General de la Unidad" DataField="EAR_DESC_UNIDAD"
                        HeaderText="Descripci&#243;n General de la Unidad" />
                    <asp:BoundField AccessibleHeaderText="&#193;rea (m2)" DataField="EAR_AREA" HeaderText="&#193;rea (m2)" />
                    <asp:BoundField AccessibleHeaderText="Lineamiento de Manejo" DataField="EAR_LINEAMIENTO_MANEJO"
                        HeaderText="Lineamiento de Manejo" />
                </Columns>
                <EmptyDataTemplate>
                    no se han registrado valores para areas de exclusion
                </EmptyDataTemplate>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td class="titleUpdate">
            </td>
    </tr>
    <tr>
        <td>
            6.2 Areas de intervencion con Restricciones
        </td>
    </tr>
    <tr>
        <td class="titleUpdate">
           
        </td>
    </tr>
    <tr>
        <td style="text-align: left;">
            <asp:GridView ID="grid2" runat="server" AutoGenerateColumns="False" Width="100%" 
                onrowdeleting="grid2_RowDeleting" 
                EmptyDataText="no se han registrado valores para areas de intervencion con restricciones">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" />
                    <asp:BoundField AccessibleHeaderText="Codigo del Mapa" DataField="EAR_COD_MAPA" HeaderText="Codigo del Mapa" />
                    <asp:BoundField AccessibleHeaderText="Unidad de Zonificaci&#243;n Ambiental Asociada"
                        DataField="EAR_UNID_ZON_AMB_ASOC" HeaderText="Unidad de Zonificaci&#243;n Ambiental Asociada" />
                    <asp:BoundField AccessibleHeaderText="Variable Ambiental que determina la Exclusi&#243;n"
                        DataField="EAR_VAR_AMB_DETERMINANTE" HeaderText="Variable Ambiental que determina la Exclusi&#243;n" />
                    <asp:BoundField AccessibleHeaderText="Descripci&#243;n General de la Unidad" DataField="EAR_DESC_UNIDAD"
                        HeaderText="Descripci&#243;n General de la Unidad" />
                    <asp:BoundField AccessibleHeaderText="Componente del Proyecto que la Interviene"
                        DataField="EAR_COMP_PROYECT" HeaderText="Componente del Proyecto que la Interviene" />
                    <asp:BoundField AccessibleHeaderText="% del &#193;rea Total a Intervenir" DataField="EAR_PORC_AREA_INTER"
                        HeaderText="% del &#193;rea Total a Intervenir" />
                    <asp:BoundField AccessibleHeaderText="&#193;rea (m2)" DataField="EAR_AREA" HeaderText="&#193;rea (m2)" />
                    <asp:BoundField AccessibleHeaderText="Lineamiento de Manejo" DataField="EAR_LINEAMIENTO_MANEJO"
                        HeaderText="Lineamiento de Manejo" />
                </Columns>
                <EmptyDataTemplate>
                    no se han registrado valores paraareas de intervencion con restricciones
                </EmptyDataTemplate>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td class="titleUpdate">
        </td>
    </tr>
    <tr>
        <td>
            6.3 Areas de intervencion
        </td>
    </tr>
    <tr>
        <td class="titleUpdate">
         
        </td>
    </tr>
    <tr>
        <td style="text-align: left;">
            <asp:GridView ID="grid3" runat="server" AutoGenerateColumns="False" Width="100%" 
                onrowdeleting="grid3_RowDeleting" 
                EmptyDataText="no se han registrado valores para areas de intervencion">
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" />
                    <asp:BoundField AccessibleHeaderText="Codigo del Mapa" DataField="EAR_COD_MAPA" HeaderText="Codigo del Mapa" />
                    <asp:BoundField AccessibleHeaderText="Unidad de Zonificaci&#243;n Ambiental Asociada"
                        DataField="EAR_UNID_ZON_AMB_ASOC" HeaderText="Unidad de Zonificaci&#243;n Ambiental Asociada" />
                    <asp:BoundField AccessibleHeaderText="Variable Ambiental que determina la Exclusi&#243;n"
                        DataField="EAR_VAR_AMB_DETERMINANTE" HeaderText="Variable Ambiental que determina la Exclusi&#243;n" />
                    <asp:BoundField AccessibleHeaderText="Descripci&#243;n General de la Unidad" DataField="EAR_DESC_UNIDAD"
                        HeaderText="Descripci&#243;n General de la Unidad" />
                    <asp:BoundField AccessibleHeaderText="Componente del Proyecto que la Interviene"
                        DataField="EAR_COMP_PROYECT" HeaderText="Componente del Proyecto que la Interviene" />
                    <asp:BoundField AccessibleHeaderText="% del &#193;rea Total a Intervenir" DataField="EAR_PORC_AREA_INTER"
                        HeaderText="% del &#193;rea Total a Intervenir" />
                    <asp:BoundField AccessibleHeaderText="&#193;rea (m2)" DataField="EAR_AREA" HeaderText="&#193;rea (m2)" />
                    <asp:BoundField AccessibleHeaderText="Lineamiento de Manejo" DataField="EAR_LINEAMIENTO_MANEJO"
                        HeaderText="Lineamiento de Manejo" />
                </Columns>
                <EmptyDataTemplate>
                    no se han registrado valores para areas de intervencion
                </EmptyDataTemplate>
            </asp:GridView>
        </td>
    </tr>
    </table>

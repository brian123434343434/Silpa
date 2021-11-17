<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrFicha3.ascx.cs" Inherits="ResumenEIA_Fichas_ctrFicha3" %>
<%@ Register src="../Controles/ctrCoordenadasPto.ascx" tagname="ctrCoordenadasPto" tagprefix="uc2" %>
<%@ Register src="../Controles/ctrCoordenadas.ascx" tagname="ctrCoordenadas" tagprefix="uc1" %>
<table style="width: 100%">
    <tbody>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <tr>
            <td colspan="4">
                3. CARACTERIZACIÓN DEL ÁREA DE INFLUENCIA DEL PROYECTO</td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan = "4" width="100%">3.1 Medio abiótico</td>
        </tr>
        <!-- Geología -->
        <tr>
            <td width="25%">3.1.1 Geología</td>
            <td width="75%" colspan="3" align="right">
                <asp:Button ID="btnNuevaGeologia" runat="server" SkinID="boton_copia"
                    Text="Agregar información de Geología" onclick="btnNuevaGeologia_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhGeologia" Visible="False">
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label1" runat="server" SkinID="etiqueta_negra" 
                Text="Código en el mapa:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtCodigoMapaGeologia" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Colocar el código con que se identifica la Unidad en el Mapa respectivo"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCodigoMapaGeologia" runat="server" 
                ErrorMessage="Ingrese el código en el mapa"
                    Display="Dynamic" ControlToValidate="txtCodigoMapaGeologia"
                    ValidationGroup="Geologia">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label2" runat="server" SkinID="etiqueta_negra" 
                Text="Unidades Litológicas en el Área:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtUnidLitoAreaGeografia" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="Ingrese la información de Unidades Litológicas en el Área"
                    Display="Dynamic" ControlToValidate="txtUnidLitoAreaGeografia"
                    ValidationGroup="Geologia">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label3" runat="server" SkinID="etiqueta_negra" 
                Text="Rasgos Estructurales Presentes:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtRasgosEstrucGeologia" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Fallas geológicas, tipo fracturas, espaciamiento diaclasas, continuidad, etc."></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="Ingrese la información de los Rasgos Estructurales Presentes"
                    Display="Dynamic" ControlToValidate="txtRasgosEstrucGeologia"
                    ValidationGroup="Geologia">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label4" runat="server" SkinID="etiqueta_negra" 
                Text="Caracterización de la Unidad Litológica:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtCaracUnidLitoGeologia" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="Grado de meteorización (I - más sano a VI - muy meteorizado), tipo de material (roca, depósitos o suelo)"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="Ingrese la información de Caracterización de la Unidad Litológica"
                    Display="Dynamic" ControlToValidate="txtCaracUnidLitoGeologia"
                    ValidationGroup="Geologia">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label5" runat="server" SkinID="etiqueta_negra" 
                Text="Características Hidrogeológicas:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtCaracHidroGeografia" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="Escorrentia, descarga, acuífero, recarga, etc."></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ErrorMessage="Ingrese la información de Características Hidrogeológica"
                    Display="Dynamic" ControlToValidate="txtCaracHidroGeografia"
                    ValidationGroup="Geologia">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarGeologia" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="Geologia" OnClick="btnAgregarGeologia_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarGeologia" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarGeologia_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary9" runat="server" 
                ValidationGroup="Geologia" />
            </td>
        </tr>
        
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvGeologia" AutoGenerateColumns="False"
                onrowdeleting="grvGeologia_RowDeleting"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de geología del proyecto">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField HeaderText="Codigo del Mapa" DataField="EGM_CODIGO_MAPA"/>
                        <asp:BoundField HeaderText="Unidades Litológicas en el Área" DataField="EGM_UNIDADES_LITOLOGICAS"/>
                        <asp:BoundField HeaderText="Rasgos Estructurales Presentes" DataField="EGM_RASGOS_ESTRUCTURALES"/>
                        <asp:BoundField HeaderText="Caracterización de la Unidad Litológica" DataField="EGM_CARACT_UNIDAD_LITOLOGICA"/>
                        <asp:BoundField HeaderText="Características Hidrogeológicas" DataField="EGM_CARACT_HIDRO_GEOLOGICAS"/>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <!-- Geología -->
        <!-- Geomorfología -->
        <tr>
            <td width="25%">3.1.2 Geomorfología</td>
            <td width="75%" colspan="3" align="right">
                <asp:Button ID="btnNuevaGeomorfologia" runat="server" SkinID="boton_copia"
                    Text="Agregar información de Geomorfología" onclick="btnNuevaGeomorfologia_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhGeomorfologia" Visible="False">
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label6" runat="server" SkinID="etiqueta_negra" 
                Text="Código en el mapa:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtCodigoMapaGeomorfologia" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Colocar el código con que se identifica la Unidad en el Mapa respectivo"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCodigoMapaGeomorfologia" runat="server" 
                ErrorMessage="Ingrese el código en el mapa"
                    Display="Dynamic" ControlToValidate="txtCodigoMapaGeomorfologia"
                    ValidationGroup="Geomorfologia">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label7" runat="server" SkinID="etiqueta_negra" 
                Text="Unidades Geomorfológicas:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtUnidadesGeomorfologia" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                ErrorMessage="Ingrese la información de Unidades Geomorfológicas"
                    Display="Dynamic" ControlToValidate="txtUnidadesGeomorfologia"
                    ValidationGroup="Geomorfologia">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label8" runat="server" SkinID="etiqueta_negra" 
                Text="Pendientes Naturales:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtPendNaturGeomorfologia" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Clasificación del IGAC"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                ErrorMessage="Ingrese la información de Pendientes Naturales"
                    Display="Dynamic" ControlToValidate="txtPendNaturGeomorfologia"
                    ValidationGroup="Geomorfologia">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label9" runat="server" SkinID="etiqueta_negra" 
                Text="Susceptibilidad a la Erosión:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtSuscepErosionGeomorfologia" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="Alta, Medio o Baja. Aplica tanto para unidades de suelo como de roca"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                ErrorMessage="Ingrese la información de Susceptibilidad a la Erosión"
                    Display="Dynamic" ControlToValidate="txtSuscepErosionGeomorfologia"
                    ValidationGroup="Geomorfologia">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label10" runat="server" SkinID="etiqueta_negra" 
                Text="Proceso Morfodinámicos:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtProcMorfoGeomorfologia" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="Flujos de tierra, reptaciñon, deslizamientos, caída de bloques, socavación fluvial, etc."></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                ErrorMessage="Ingrese la información de Proceso Morfodinámicos"
                    Display="Dynamic" ControlToValidate="txtProcMorfoGeomorfologia"
                    ValidationGroup="Geomorfologia">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarGeomorfologia" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="Geomorfologia"
                    OnClick="btnAgregarGeomorfologia_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarGeomorfologia" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarGeomorfologia_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        
         <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                ValidationGroup="Geomorfologia" />
            </td>
        </tr>        
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvGeomorfologia" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de Geomorfología del proyecto" OnRowDeleting="grvGeomorfologia_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField HeaderText="Codigo del Mapa" DataField="EMM_CODIGO_MAPA" />
                        <asp:BoundField HeaderText="Unidades Geomorfológicas" DataField="EMM_UNI_GEOMORFOLOGICAS"/>
                        <asp:BoundField HeaderText="Pendientes Naturales" DataField="EMM_PEND_NATURALES"/>
                        <asp:BoundField HeaderText="Susceptibilidad a la Erosión" DataField="EMM_SUCEPT_EROSION"/>
                        <asp:BoundField HeaderText="Proceso Morfodinámicos" DataField="EMM_PROC_MORFODINAMICO"/>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <!-- Geomorfología -->
        <!-- Estabilidad Geotécnica -->
        <tr>
            <td width="25%">3.1.3 Estabilidad Geotécnica</td>
            <td colspan = "3" width="75%" align="right">
                <asp:Button ID="btnNuevaEstGeotecnica" runat="server" SkinID="boton_copia"
                    Text="Agregar información de Estabilidad Geotécnica" onclick="btnNuevaEstGeotecnica_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>           
            
        </tr>
        
        <asp:PlaceHolder runat="server" ID="plhEstGeotecnica" Visible="False">
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label11" runat="server" SkinID="etiqueta_negra" 
                Text="Código en el mapa:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtCodigoMapaEstGeotecnica" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Colocar el código con que se identifica la Unidad en el Mapa respectivo"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCodigoMapaEstGeotecnica" runat="server" 
                ErrorMessage="Ingrese el código en el mapa"
                    Display="Dynamic" ControlToValidate="txtCodigoMapaEstGeotecnica"
                    ValidationGroup="EstGeotecnica">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label12" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad Geotécnica:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtUnidGeotecnica" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                ErrorMessage="Ingrese la información de Unidades Geotécnica"
                    Display="Dynamic" ControlToValidate="txtUnidGeotecnica"
                    ValidationGroup="EstGeotecnica">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label13" runat="server" SkinID="etiqueta_negra" 
                Text="Descripción de la Unidad Geotécnica:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtDescUnidGeotecnica" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                ErrorMessage="Descripción de la Unidad Geotécnica"
                    Display="Dynamic" ControlToValidate="txtDescUnidGeotecnica"
                    ValidationGroup="EstGeotecnica">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label14" runat="server" SkinID="etiqueta_negra" 
                Text="Grado de Estabilidad:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:DropDownList ID="cboGradoEstabilidad" runat="server" SkinID="lista_desplegable">
                    <asp:ListItem Selected="True" Text="Seleccione..." Value="-1"></asp:ListItem>
                    <asp:ListItem Text="Alta" Value="A"></asp:ListItem>
                    <asp:ListItem Text="Media" Value="M"></asp:ListItem>
                    <asp:ListItem Text="Baja" Value="B"></asp:ListItem>
                </asp:DropDownList><asp:CompareValidator ID="crvSectorProductivo" runat="server"
                    ErrorMessage="Seleccione el grado de estabilidad" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboGradoEstabilidad" 
                    ValidationGroup="EstGeotecnica">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="Button2" runat="server" onclick="Button2_Click" ValidationGroup="EstGeotecnica" Text="Agregar" SkinID="boton_copia" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarEstGeotecnica" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarEstGeotecnica_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
                ValidationGroup="EstGeotecnica" />
            </td>
        </tr>    
        </asp:PlaceHolder>
        
        <tr>
            <td colspan="4">
                <asp:GridView runat="server" ID="grvEstGeotecnica" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"       
                    
            EmptyDataText="No ha agregado información de Estabilidad Geotécnica del proyecto" 
            onrowdeleting="grvEstGeotecnica_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField HeaderText="Codigo del Mapa" DataField="EEG_CODIGO_MAPA" />
                        <asp:BoundField HeaderText="Unidad Geotécnica" 
                            DataField="EEG_UNIDAD_GEOTECNICA" />
                        <asp:BoundField HeaderText="Descripción de la Unidad Geotécnica" 
                            DataField="EEG_DESC_UNIDAD_GEOTECNICA" />
                        <asp:BoundField HeaderText="Grado de Estabilidad" 
                            DataField="EEG_GRADO_ESTABILIDAD" />
                    </Columns>
                </asp:GridView>
            </td>
       </tr>
        <!-- Estabilidad Geotécnica -->
        <tr>
            <td colspan = "4" width="100%">3.1.4 Hidrogeología</td>
        </tr>
        <!--Unidades Hidrogeológicas Presentes -->
        <tr>
            <td width="25%">3.1.4.1 Caracteristicas de Unidades Hidrogeológicas Presentes</td>
            <td width="75%" colspan="3" align="right">
                <asp:Button ID="btnNuevaUnidHidrogeolo" runat="server" SkinID="boton_copia"
                    Text="Agregar información de Unidades Hidrogeológicas" onclick="btnNuevaUnidHidrogeolo_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhUnidHidrogeolo" Visible="False">
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label15" runat="server" SkinID="etiqueta_negra" 
                Text="Código en el mapa:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtCodigoMapaUnidHidrogeolo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Colocar el código con que se identifica la Unidad en el Mapa respectivo"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCodigoMapaUnidHidrogeolo" runat="server" 
                ErrorMessage="Ingrese el código en el mapa"
                    Display="Dynamic" ControlToValidate="txtCodigoMapaUnidHidrogeolo"
                    ValidationGroup="UnidHidrogeolo">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label16" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre de la Unidad Hidrogeológica:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtNombreHidrogeolo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ontextchanged="txtNombreHidrogeolo_TextChanged"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                ErrorMessage="Ingrese la información de Unidades Litológicas en el Área"
                    Display="Dynamic" ControlToValidate="txtNombreHidrogeolo"
                    ValidationGroup="UnidHidrogeolo">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label17" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Acuífero:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboTipoAcuifeUnidHidrogeolo" runat="server" 
                SkinID="lista_desplegable">
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator1" runat="server"
                    ErrorMessage="Seleccione el Tipo de Acuífero" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboTipoAcuifeUnidHidrogeolo" 
                    ValidationGroup="UnidHidrogeolo">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label18" runat="server" SkinID="etiqueta_negra" 
                Text="Gradiente Hidraúlico:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboGradHidraUnidHidrogeolo" runat="server" 
                SkinID="lista_desplegable"></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator2" runat="server"
                    ErrorMessage="Seleccione el Gradiente Hidraúlico" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboGradHidraUnidHidrogeolo" 
                    ValidationGroup="UnidHidrogeolo">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label19" runat="server" SkinID="etiqueta_negra" 
                Text="Dirección del Flujo de Agua Subterránea:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboDirFluUnidHidrogeolo" runat="server" 
                SkinID="lista_desplegable">
                    <asp:ListItem Text="Seleccione..." Value="-1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="N" Value="N"></asp:ListItem>
                    <asp:ListItem Text="NE" Value="NE"></asp:ListItem>
                    <asp:ListItem Text="NW" Value="NW"></asp:ListItem>
                    <asp:ListItem Text="S" Value="S"></asp:ListItem>
                    <asp:ListItem Text="SE" Value="SE"></asp:ListItem>
                    <asp:ListItem Text="SW" Value="SW"></asp:ListItem>
                    <asp:ListItem Text="E" Value="E"></asp:ListItem>
                    <asp:ListItem Text="W" Value="W"></asp:ListItem>
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator3" runat="server"
                    ErrorMessage="Seleccione la Dirección del Flujo de Agua Subterránea" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboDirFluUnidHidrogeolo" 
                    ValidationGroup="UnidHidrogeolo">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarUnidHidrogeolo" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="UnidHidrogeolo" 
                    onclick="btnAgregarUnidHidrogeolo_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarUnidHidrogeolo" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarUnidHidrogeolo_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary3" runat="server" 
                ValidationGroup="UnidHidrogeolo" />
            </td>
        </tr>  
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvUnidHidrogeolo" AutoGenerateColumns="False"
                width="99%"
                    SkinID="grilla_simple"
                    EmptyDataText="No ha agregado información de Unidades Hidrogeológicas Presentes del proyecto" 
                    onrowdeleting="grvUnidHidrogeolo_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField DataField="ECU_ID" HeaderText="Id" ReadOnly="True" />
                        <asp:BoundField HeaderText="Codigo del Mapa" DataField="ECU_CODIGO_MAPA" />
                        <asp:BoundField HeaderText="Nombre de la Unidad Hidrogeológica" 
                            DataField="ECU_NOMBRE_UNIDAD_HG" />
                        <asp:BoundField HeaderText="Tipo de Acuífero" DataField="ACUIFERO" />
                        <asp:BoundField HeaderText="Gradiente Hidraúlico" DataField="GRADIENTE" />
                        <asp:BoundField HeaderText="Dirección del Flujo de Agua Subterráneas" 
                            DataField="DIRECCION" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <!--Unidades Hidrogeológicas Presentes -->
        <!--Puntos de Agua en Área de Influencia-->
        <tr>
            <td  width="25%">3.1.4.2 Puntos de Agua en Área de Influencia</td>
            <td colspan = "3" width="75%" align="right">
                <asp:Button ID="btnNuevaPtoAguaAreaInf" runat="server" SkinID="boton_copia"
                    Text="Agregar información de Puntos de Agua" 
					onclick="btnNuevaPtoAguaAreaInf_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhPtoAguaAreaInf" Visible="False">
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label20" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboTipoPtoAguaAreaInf" runat="server" 
                SkinID="lista_desplegable"></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator4" runat="server"
                    ErrorMessage="Seleccione el Tipo" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboTipoPtoAguaAreaInf" 
                    ValidationGroup="PtoAguaAreaInf">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td  width="25%">
                <asp:Label ID="Label84" runat="server" SkinID="etiqueta_negra" 
                Text="Coordenadas de Ubicación (m)"></asp:Label>
            </td>
            <td colspan = "3" width="70%" align="right" style="text-align:right">
                <uc2:ctrCoordenadasPto runat="server" ID="ctrCoorPtoAguaAreaInf" />
            </td>
        </tr>
      
        <tr>
            <td width="25%">
                <asp:Label ID="Label21" runat="server" SkinID="etiqueta_negra" 
                Text="Nivel Piezométrico:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtNivPiezoPtoAguaAreaInf" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                ErrorMessage="Ingrese la información de Nivel Piezométrico"
                    Display="Dynamic" ControlToValidate="txtNivPiezoPtoAguaAreaInf"
                    ValidationGroup="PtoAguaAreaInf">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label22" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad Acuífera de donde se Capta:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtUnidAcuiPtoAguaAreaInf" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Fallas geológicas, tipo fracturas, espaciamiento diaclasas, continuidad, etc."></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                ErrorMessage="Ingrese la información de Unidad Acuífera de donde se Capta"
                    Display="Dynamic" ControlToValidate="txtUnidAcuiPtoAguaAreaInf"
                    ValidationGroup="PtoAguaAreaInf">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label23" runat="server" SkinID="etiqueta_negra" 
                Text="Caudal:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtCaudalPtoAguaAreaInf" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="Grado de meteorización (I - más sano a VI - muy meteorizado), tipo de material (roca, depósitos o suelo)"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                ErrorMessage="Ingrese la información de Caudal"
                    Display="Dynamic" ControlToValidate="txtCaudalPtoAguaAreaInf"
                    ValidationGroup="PtoAguaAreaInf">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label24" runat="server" SkinID="etiqueta_negra" 
                Text="Uso:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtUsoPtoAguaAreaInf" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="Escorrentia, descarga, acuífero, recarga, etc."></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                ErrorMessage="Ingrese la información de Uso"
                    Display="Dynamic" ControlToValidate="txtUsoPtoAguaAreaInf"
                    ValidationGroup="PtoAguaAreaInf">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarPtoAguaAreaInf" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="PtoAguaAreaInf" 
                    onclick="btnAgregarPtoAguaAreaInf_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarPtoAguaAreaInf" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarPtoAguaAreaInf_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary4" runat="server" 
                ValidationGroup="PtoAguaAreaInf" />
            </td>
        </tr> 
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvPtoAguaAreaInf" AutoGenerateColumns="False"
                width="99%" SkinID="grilla_simple"
                    EmptyDataText="No ha agregado información de los puntos de agua" 
                    onrowdeleting="grvPtoAguaAreaInf_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField HeaderText="Id" DataField="EAA_ID"></asp:BoundField>
                        <asp:BoundField HeaderText="Tipo" DataField="ETP_TIPO_PTO_AGUA" />
                        <asp:BoundField HeaderText="Coordenada Norte de Ubicación" 
                            DataField="EAA_COOR_NORTE_UBICACION" />
                        <asp:BoundField HeaderText="Coordenada Este de Ubicación" 
                            DataField="EAA_COOR_ESTE_UBICACION" />
                        <asp:BoundField HeaderText="Nivel Piezometrico" 
                            DataField="EAA_NIVEL_PIEZOMETRICO" />
                        <asp:BoundField HeaderText="Unidad Acuífera de donde se Capta" 
                            DataField="EAA_UNID_ACUIF_CAPTA" />
                        <asp:BoundField HeaderText="Caudal" DataField="EAA_CAUDAL" />
                        <asp:BoundField HeaderText="Uso" DataField="EAA_USO" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
       <tr>
            <td width="25%">Áreas de Recarga</td>
            <td width="75%" colspan="3" align="right">
                <asp:Button ID="btnNuevaAreaRecarga" runat="server" SkinID="boton_copia"
                    Text="Agregar información de Areas de Recarga Localización" 
					onclick="btnNuevaAreaRecarga_Click"  /></td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhAreasRecarga" Visible="False">
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label25" runat="server" SkinID="etiqueta_negra" 
                Text="Areas de Recarga Localización - Coordenadas planas (Datum Magna-Sirgas)"></asp:Label></td>
            <td width="75%" colspan = "3">
                <uc1:ctrCoordenadas runat="server" ID="ctrCoorAreasRecarga" />
            </td>
        </tr>
       
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarAreasRecarga" runat="server" SkinID="boton_copia"
                    Text="Agregar" OnClick="btnAgregarAreasRecarga_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarAreasRecarga" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarAreasRecarga_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr> 
        </asp:PlaceHolder>       
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvAreasRecarga" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información del area de recarga" OnRowDeleting="grvAreasRecarga_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField DataField="EAR_ID" HeaderText="Polígono" />
                        <asp:TemplateField HeaderText="Areas de Recarga Localización - Coordenadas planas (Datum Magna-Sirgas)">
                            <ItemTemplate>                                
                                <uc1:ctrCoordenadas ID="ctrCoorForestGrid" runat="server" DataGridObject="true" NombreTabla="EIH_COOR_AREA_RECARG" NombreCampo="EAR_ID" ValorCampo='<%# Eval("EAR_ID") %>' ValorCampo2='<%# Eval("EAR_ID") %>' />                                           
                                <asp:Label id="lblEarId" runat="server" text='<%# Eval("EAR_ID") %>' visible="false"></asp:Label>                        
                            </ItemTemplate>
                        </asp:TemplateField>                        
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <!--Areas de recarga-->
        <!--Areas de descarga-->
        <tr>
            <td width="25%">Áreas de descarga</td>
            <td width="75%" colspan = "3" align="right">
                <asp:Button ID="btnNuevaAreadescarga" runat="server" SkinID="boton_copia"
                    Text="Agregar información de Areas de descarga Localización" 
					onclick="btnNuevaAreaDescarga_Click"  /></td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhAreasDescarga" Visible="False">        
        <tr>
            <td width="25%">
                <asp:Label ID="Label26" runat="server" SkinID="etiqueta_negra" 
                Text="Areas de descarga Localización - Coordenadas planas (Datum Magna-Sirgas)"></asp:Label></td>
            <td width="75%" colspan = "3">
                <uc1:ctrCoordenadas runat="server" ID="ctrCoorAreasDescarga" />
            </td>
        </tr>
        
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarAreasdescarga" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="Areasdescarga" OnClick="btnAgregarAreasdescarga_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarAreasdescarga" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarAreasDescarga_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvAreasDescarga" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple" OnRowDeleting="grvAreasDescarga_RowDeleting"
                    EmptyDataText="No ha agregado información del area de descarga">
                    <Columns>
                      <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField DataField="EAD_ID" HeaderText="Polígono" />
                        <asp:TemplateField HeaderText="Areas de Recarga Localización - Coordenadas planas (Datum Magna-Sirgas)">
                            <ItemTemplate>                                
                                <uc1:ctrCoordenadas ID="ctrCoorForestGrid2" runat="server" DataGridObject="true" NombreTabla="EIH_COOR_AREA_DESC" NombreCampo="EAD_ID" ValorCampo='<%# Eval("EAD_ID") %>' ValorCampo2='<%# Eval("EAD_ID") %>' />                                           
                                <asp:Label id="lblEadId" runat="server" text='<%# Eval("EAD_ID") %>' visible="false"></asp:Label>                        
                            </ItemTemplate>
                        </asp:TemplateField>      
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
            <%--Suelos --%>
        <tr>
            <td width="25%">3.1.5 Suelos</td>
            <td width="75%" colspan="3" align="right">
                <asp:Button ID="btnNuevaSuelosMedAbio" runat="server" SkinID="boton_copia"
                    Text="Agregar información de Suelos" onclick="btnNuevaSuelosMedAbio_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhSuelosMedAbio" Visible="False">
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label55" runat="server" SkinID="etiqueta_negra" 
                Text="Código en el mapa:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtCodigoMapaSuelosMedAbio" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Colocar el código con que se identifica la Unidad en el Mapa respectivo"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCodigoMapaSuelosMedAbio" runat="server" 
                ErrorMessage="Ingrese el código en el mapa"
                    Display="Dynamic" ControlToValidate="txtCodigoMapaSuelosMedAbio"
                    ValidationGroup="SuelosMedAbio">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label56" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad de Suelo a Intervenir:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtUnidadesSuelosMedAbio" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" 
                ErrorMessage="Ingrese la información de Unidad de Suelo a Intervenir"
                    Display="Dynamic" ControlToValidate="txtUnidadesSuelosMedAbio"
                    ValidationGroup="SuelosMedAbio">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label57" runat="server" SkinID="etiqueta_negra" 
                Text="Clasificación Agrológica:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtClaAgroSuelosMedAbio" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" 
                ErrorMessage="Ingrese la información de Clasificación Agrológica"
                    Display="Dynamic" ControlToValidate="txtClaAgroSuelosMedAbio"
                    ValidationGroup="SuelosMedAbio">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label58" runat="server" SkinID="etiqueta_negra" 
                Text="Descripción de la Unidad:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtDescUnidadSuelosMedAbio" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" 
                ErrorMessage="Ingrese la información de Descripción de la Unidad"
                    Display="Dynamic" ControlToValidate="txtDescUnidadSuelosMedAbio"
                    ValidationGroup="SuelosMedAbio">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label59" runat="server" SkinID="etiqueta_negra" 
                Text="Principal Uso Actual del Suelo:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtPricUsoSuelosMedAbio" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" 
                ErrorMessage="Ingrese la información de Principal Uso Actual del Suelo"
                    Display="Dynamic" ControlToValidate="txtPricUsoSuelosMedAbio"
                    ValidationGroup="SuelosMedAbio">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarSuelosMedAbio" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="SuelosMedAbio"
                    OnClick="btnAgregarSuelosMedAbio_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarSuelosMedAbio" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarSuelosMedAbio_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvSuelosMedAbio" AutoGenerateColumns="False"
                width="99%" onrowdeleting="grvSuelosMedAbio_RowDeleting" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de Suelos del proyecto">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True"  />
                        <asp:BoundField HeaderText="Codigo del Mapa" DataField="ESM_CODIGO_MAPA" />
                        <asp:BoundField HeaderText="Unidad de Suelo a Intervenir" DataField="ESM_UNIDAD_SUELO" />
                        <asp:BoundField HeaderText="Clasificación Agrológica" DataField="ESM_CLASI_AGROLOGICA" />
                        <asp:BoundField HeaderText="Descripción de la Unidad" DataField="ESM_DESCR_UNIDAD"/>
                        <asp:BoundField HeaderText="Principal Uso Actual del Suelo" DataField="ESM_PRINC_USO_SUELO" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <%--Clima --%>
        
        <tr>
            <td colspan = "4" width="100%">3.1.6 Clima</td>
        </tr>
        <tr>
            <td  width="25%">3.1.6.1 Estaciones Meteorológicas</td>
            <td width="75%" colspan = "3" align="right">
                <asp:Button ID="btnNuevaEstMetereoClima" runat="server" SkinID="boton_copia"
                    Text="Agregar información de Estaciones Meteorológicas" onclick="btnNuevaEstMetereoClima_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhEstMetereoClima" Visible="False">        
        <tr>
            <td width="25%">
                <asp:Label ID="Label50" runat="server" SkinID="etiqueta_negra" 
                Text="Código de la estación:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtCodigoEstMetereoClima" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCodigoMapaEstMetereoClima" runat="server" 
                ErrorMessage="Ingrese el código de la estación"
                    Display="Dynamic" ControlToValidate="txtCodigoEstMetereoClima"
                    ValidationGroup="EstMetereoClima">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label51" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtNombreEstMetereoClima" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" 
                ErrorMessage="Ingrese la información de Nombre"
                    Display="Dynamic" ControlToValidate="txtNombreEstMetereoClima"
                    ValidationGroup="EstMetereoClima">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label52" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtTipoEstMetereoClima" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" 
                ErrorMessage="Ingrese la información de Tipo"
                    Display="Dynamic" ControlToValidate="txtTipoEstMetereoClima"
                    ValidationGroup="EstMetereoClima">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%" style="height: 24px">
                <asp:Label ID="Label63" runat="server" SkinID="etiqueta_negra" 
                Text="Departamento:"></asp:Label></td>
            <td width="25%" style="height: 24px">
                <asp:DropDownList ID="cboDeptoEstMetereoClima" runat="server" SkinID="lista_desplegable"
                AutoPostBack="True" OnSelectedIndexChanged="cboDeptoEstMetereoClima_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:CompareValidator ID="crvUnidadArea" runat="server"
                    ErrorMessage="Seleccione el departamento" 
                    Display="Dynamic"
                    ControlToValidate="cboDeptoEstMetereoClima" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="EstMetereoClima">*</asp:CompareValidator></td>
            <td width="25%" style="height: 24px">
                <asp:Label ID="Label62" runat="server" SkinID="etiqueta_negra" 
                Text="Municipio:"></asp:Label></td>
            <td width="25%" style="height: 24px">
                <asp:DropDownList ID="cboMunicEstMetereoClima" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator5" runat="server"
                    ErrorMessage="Seleccione el Municipio" 
                    Display="Dynamic"
                    ControlToValidate="cboMunicEstMetereoClima" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="EstMetereoClima">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label53" runat="server" SkinID="etiqueta_negra" 
                Text="Corriente:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtCorrienteEstMetereoClima" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" 
                ErrorMessage="Ingrese la información de corrientes"
                    Display="Dynamic" ControlToValidate="txtCorrienteEstMetereoClima"
                    ValidationGroup="EstMetereoClima">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan = "2" width="50%">
                <asp:Label ID="Label27" runat="server" SkinID="etiqueta_negra" 
                Text="Coordenadas de Ubicación (m)"></asp:Label>
            </td>
            <td colspan = "2" width="50%" align="right" style="text-align:right">
                <uc2:ctrCoordenadasPto runat="server" ID="ctrCoorEstMetereoClima" />
            </td>
        </tr>   
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarEstMetereoClima" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="EstMetereoClima" OnClick="btnAgregarEstMetereoClima_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarEstMetereoClima" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarEstMetereoClima_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
         <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary5" runat="server" 
                ValidationGroup="EstMetereoClima" />
            </td>
        </tr> 
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvEstMetereoClima" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de las estaciones meteorológicas del proyecto" OnRowDeleting="grvEstMetereoClima_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True"  />
                        <asp:BoundField DataField="EEH_COD_ESTACION" HeaderText="Código de la Estación" />
                        <asp:BoundField DataField="EEH_NOMBRE_ESTACION" HeaderText="Nombre" />
                        <asp:BoundField DataField="EEH_TIPO_ESTACION" HeaderText="Tipo" />
                        <asp:BoundField DataField="DEP_NOMBRE" HeaderText="Departamento" />
                        <asp:BoundField DataField="MUN_NOMBRE" HeaderText="Municipio" />
                        <asp:BoundField DataField="EEH_CORRIENTE" HeaderText="Corriente" />
                        <asp:BoundField DataField="EEH_COOR_NORTE_UBICACION" HeaderText="Coordenada Norte de Ubicación" />
                        <asp:BoundField DataField="EEH_COOR_ESTE_UBICACION" HeaderText="Coordenada Este de Ubicación" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
          <%--Variables Climaticas  --%>
        <tr>
            <td  width="25%">3.1.6.2 Variables Climáticas</td>
            <td width="75%" align="right" colspan = "3">
                <asp:Button ID="btnNuevaVarClima" runat="server" SkinID="boton_copia"
                    Text="Agregar información de Variables Climáticas" onclick="btnNuevaVarClima_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhVarClima" Visible="False">
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label69" runat="server" SkinID="etiqueta_negra" 
                Text="Variable:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:DropDownList ID="cboVariableClima" runat="server" SkinID="lista_desplegable"></asp:DropDownList><asp:CompareValidator ID="CompareValidator6" runat="server"
                    ErrorMessage="Seleccione la Variable" 
                    Display="Dynamic"
                    ControlToValidate="cboVariableClima" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="VarClima">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label64" runat="server" SkinID="etiqueta_negra" 
                Text="Valor Máximo:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtValMaxVarClima" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCodigoMapaVarClima" runat="server" 
                ErrorMessage="Ingrese el valor máximo"
                    Display="Dynamic" ControlToValidate="txtValMaxVarClima"
                    ValidationGroup="VarClima">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator7" runat="server"
                    ErrorMessage="El valor máximo debe ser un dato numérico" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtValMaxVarClima" 
                    ValidationGroup="VarClima"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label65" runat="server" SkinID="etiqueta_negra" 
                Text="Mes de Valores Máximos:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtMesValMaxVarClima" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
                ErrorMessage="Ingrese la información de Mes de Valores Máximos"
                    Display="Dynamic" ControlToValidate="txtMesValMaxVarClima"
                    ValidationGroup="VarClima">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label66" runat="server" SkinID="etiqueta_negra" 
                Text="Valor Mínimo:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtValMinVarClima" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" 
                ErrorMessage="Ingrese la información de Valor Mínimo"
                    Display="Dynamic" ControlToValidate="txtValMinVarClima"
                    ValidationGroup="VarClima">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator8" runat="server"
                    ErrorMessage="El valor mínimo debe ser un dato numérico" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtValMinVarClima" 
                    ValidationGroup="VarClima"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label67" runat="server" SkinID="etiqueta_negra" 
                Text="Mes de Valores Mínimos:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtMesValMinVarClima" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" 
                ErrorMessage="Ingrese la información de Mes de Valores Mínimos"
                    Display="Dynamic" ControlToValidate="txtMesValMinVarClima"
                    ValidationGroup="VarClima">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label68" runat="server" SkinID="etiqueta_negra" 
                Text="Promedio Multianual:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtPromMultiaVarClima" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" 
                ErrorMessage="Ingrese la información de Promedio Multianual"
                    Display="Dynamic" ControlToValidate="txtPromMultiaVarClima"
                    ValidationGroup="VarClima">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator9" runat="server"
                    ErrorMessage="El promedio multianual debe ser un dato numérico" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtPromMultiaVarClima" 
                    ValidationGroup="VarClima"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarVarClima" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="VarClima" OnClick="btnAgregarVarClima_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarVarClima" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarVarClima_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        
         <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary6" runat="server" 
                ValidationGroup="VarClima" />
            </td>
        </tr> 
        
        </asp:PlaceHolder>
        
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvVarClima" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de Variables Climaticas" OnRowDeleting="grvVarClima_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True"  />
                        <asp:BoundField DataField="EVC_ID" HeaderText="No" />
                        <asp:BoundField DataField="ETV_TIPO_VAR_CLIMATICA" HeaderText="Variable" />
                        <asp:BoundField DataField="EVC_VALOR_MAXIMO" HeaderText="Valor Maximo" />
                        <asp:BoundField DataField="EVC_MES_VALORES_MAXIMOS" HeaderText="Mes de Valores Maximos" />
                        <asp:BoundField DataField="EVC_VALOR_MINIMO" HeaderText="Valor Minimo" />
                        <asp:BoundField DataField="EVC_MES_VALORES_MINIMOS" HeaderText="Mes de Valorer Minimos" />
                        <asp:BoundField DataField="EVC_PROM_MULTIANUAL" HeaderText="Promedio Multianual" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <%-- Cuencas --%>
        <tr>
            <td colspan = "4" width="100%">3.1.7 Hidrología</td>
        </tr>
		<tr>
            <td width="25%">3.1.7.1 Cuencas</td>
            <td width="75%" colspan="3" align="right">
                <asp:Button ID="btnNuevaCuencaHidrologia" runat="server" SkinID="boton_copia"
                    Text="Agregar cuenca" onclick="btnNuevaCuencaHidrologia_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhCuencaHidrologia" Visible="False">
        <tr>
            <td width="25%">
                <asp:Label ID="Label70" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre de la cuenca:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtNombreCuencaHidrologia" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" 
                ErrorMessage="Ingrese el nombre de la cuenca"
                    Display="Dynamic" ControlToValidate="txtNombreCuencaHidrologia"
                    ValidationGroup="CuencaHidrologia">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label71" runat="server" SkinID="etiqueta_negra" 
                Text="Área (km2):"></asp:Label></td>
            <td width="25%">
                <asp:TextBox ID="txtAreaCuencaHidrologia" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAreaCuencaHidrologia" runat="server" 
                ErrorMessage="Ingrese el área de la cuenca hidrográfica"
                    Display="Dynamic" ControlToValidate="txtAreaCuencaHidrologia"
                    ValidationGroup="CuencaHidrologia">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator11" runat="server"
                    ErrorMessage="El el área de la cuenca hidrográfica debe ser un dato numérico" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtAreaCuencaHidrologia" 
                    ValidationGroup="CuencaHidrologia"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label72" runat="server" SkinID="etiqueta_negra" 
                Text="Uso Principal:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtUsoPricCuencaHidrologia" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" 
                ErrorMessage="Ingrese la información del Uso Principal de la cuenca"
                    Display="Dynamic" ControlToValidate="txtUsoPricCuencaHidrologia"
                    ValidationGroup="CuencaHidrologia">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <%--Red de Drenaje --%>
		<tr>
            <td colspan = "4" width="100%">Red de Drenaje</td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label341" runat="server" SkinID="etiqueta_negra" 
                Text="Número de Orden de la Corriente Principal:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtNoOrdenRedDrenaje" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Número de orden del IDEAM"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator223" runat="server" 
                ErrorMessage="Ingrese el Número de Orden de la Corriente Principal"
                    Display="Dynamic" ControlToValidate="txtNoOrdenRedDrenaje"
                    ValidationGroup="CuencaHidrologia">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label342" runat="server" SkinID="etiqueta_negra" 
                Text="Densidad de Drenaje de la Cuenca Intervenida (km/km2):"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtDensidadRedDrenaje" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator224" runat="server" 
                    ErrorMessage="Ingrese la Densidad de Drenaje de la Cuenca Interv"
                    Display="Dynamic" ControlToValidate="txtDensidadRedDrenaje"
                    ValidationGroup="CuencaHidrologia">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator135" runat="server"
                    ErrorMessage="La densidad de Drenaje de la Cuenca Intervenida debe ser un dato numérico" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtDensidadRedDrenaje" 
                    ValidationGroup="CuencaHidrologia"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label343" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Red de Drenaje:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtTipoRedDrenaje" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAreaRedDrenaje" runat="server" 
                    ErrorMessage="Ingrese el Tipo de Red de Drenaje"
                    Display="Dynamic" ControlToValidate="txtTipoRedDrenaje"
                    ValidationGroup="CuencaHidrologia">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarCuencaHidrologia" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="CuencaHidrologia"
                    OnClick="btnAgregarCuencaHidrologia_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarCuencaHidrologia" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarCuencaHidrologia_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
         <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary7" runat="server" 
                ValidationGroup="CuencaHidrologia" />
            </td>
        </tr> 
        
        
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvCuencaHidrologia" AutoGenerateColumns="False"
                width="99%" onrowdeleting="grvCuencaHidrologia_RowDeleting" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de cuencas hidrográficas">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:TemplateField HeaderText="Información adicional">
                            <ItemTemplate>
                                 <a href="javascript:;" 
                                 onclick="javascript:window.open('InfoAdicional/Cuenca.aspx?Cuenca=<%#Eval("ECH_ID") %>','','left=250px, top=245px, width=1024px, height=450px, scrollbars=yes, status=no, resizable=yes');return false;">
                                   Información Adicional</a>                                   
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Nombre de la cuenca" DataField="ECH_NOMBRE_CUENCA" />
                        <asp:BoundField HeaderText="Área (km2)" DataField="ECH_AREA_CUENCA"/>
                        <asp:BoundField HeaderText="Uso Principal" DataField="ECH_USO_PRINCIPAL"/>
                        <asp:BoundField HeaderText="Número de Orden de la Corriente Principal" DataField="ECH_NOMBRE_CUENCA" />
                        <asp:BoundField HeaderText="Densidad de Drenaje de la Cuenca Interv. (km/km2)" DataField="ECH_AREA_CUENCA"/>
                        <asp:BoundField HeaderText="Tipo de Red de Drenaje" DataField="ECH_USO_PRINCIPAL"/>
                        
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        
          <%--Calidad de agua - fuentes superficiales --%>
        <tr>
            <td colspan="4" width="100%">
                3.1.8 Calidad de Fuentes de Agua
            </td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                3.1.8.1 Fuentes de Agua Superficiales
            </td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <tr>
            <td width="25%"><asp:Label ID="Label29" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre del laboratorio que realizó el análisis"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboNombLabFuentAguaSupef" runat="server" SkinID="lista_desplegable2">
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator10" runat="server"
                    ErrorMessage="Seleccione el Nombre del laboratorio" Display="Dynamic"
                    ControlToValidate="cboNombLabFuentAguaSupef" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="FuentAguaSupef">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td colspan="4" align="right" >
                <asp:Button ID="btnNuevaFuentAguaSupef" runat="server" SkinID="boton_copia"
                    Text="Agregar información de fuentes de agua superficiales" 
					onclick="btnNuevaFuentAguaSupef_Click"  /></td>
        </tr>
        <tr id="OtroLab" runat="server" visible="false">
            <td width="25%">
                <asp:Label ID="lblotroLabFuentAguaSupef" runat="server" SkinID="etiqueta_negra" 
                Text="Otro laboratorio que realizó el análisis"></asp:Label>
            </td>
            <td colspan = "3" width="75%">
                <asp:TextBox ID="txtOtroLabFuentAguaSupef" runat="server"  SkinID="texto_sintamano" 
                Text=""></asp:TextBox>
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhFuentAguaSupef" Visible="False">         
        <tr>
            <td width="25%">
                <asp:Label ID="Label80" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre de la fuente"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtNombreFuentAguaSupef" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingresar Información Nombre de la Fuente"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" 
                ErrorMessage="Ingrese el código en el mapa"
                    Display="Dynamic" ControlToValidate="txtNombreFuentAguaSupef"
                    ValidationGroup="FuentAguaSupef">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td  width="25%">
                <asp:Label ID="Label28" runat="server" SkinID="etiqueta_negra" 
                Text="Coordenadas de ubicación (Datum Magna-Sirgas)"></asp:Label>
            </td>
            <td colspan = "3" width="75%" align="right" style="text-align:right">
                <uc2:ctrCoordenadasPto runat="server" ID="ctrCoorFuentAguaSupef" />
            </td>
        </tr>      
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarFuentAguaSupef" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="FuentAguaSupef" OnClick="btnAgregarFuentAguaSupef_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarFuentAguaSupef" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarFuentAguaSupef_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary8" runat="server" 
                ValidationGroup="FuentAguaSupef" />
            </td>
        </tr>
       </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvInvFAgua" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de Inventario de Fuentes de Agua Superficiales en Área de Influencia Directa" OnRowDeleting="grvInvFAgua_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField DataField="ECS_ID" HeaderText="Codigo" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblEcsId" runat="server" Visible="false" Text='<%# Eval("ECS_ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="EPS_ID_NOMBRE_FUENTE" HeaderText="Nombre de la fuente" />
                        <asp:BoundField DataField="EPS_COOR_NORTE_UBICACION" HeaderText="Coordenanda Norte de Ubicación" />
                        <asp:BoundField DataField="EPS_COOR_ESTE_UBICACION" HeaderText="Coordenanda Este de Ubicación" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>        
    
        <tr>
            <td colspan="4" align="right" >
                <asp:Button ID="btnNuevaFuentAguaSupefDet" runat="server" SkinID="boton_copia"
                    Text="Agregar Detalles información de fuentes de agua superficiales" 
					onclick="btnNuevaFuentAguaSupefDet_Click"  /></td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhDetallesFuentesSup" Visible="false">        
        <tr>
            <td width="25%">
                <asp:Label ID="Label30" runat="server" SkinID="etiqueta_negra" 
                Text="Fuentes:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboFuentesSuper" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator13" runat="server"
                    ErrorMessage="Seleccione Una Fuente" 
                    Display="Dynamic"
                    ControlToValidate="cboFuentesSuper" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="FuentAguaSupefDet">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label78" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Muestra:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboTipoMuestraFuentAguaSupef" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator12" runat="server"
                    ErrorMessage="Seleccione el Tipo de Muestra" 
                    Display="Dynamic"
                    ControlToValidate="cboTipoMuestraFuentAguaSupef" 
                      ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="FuentAguaSupefDet">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label79" runat="server" SkinID="etiqueta_negra" 
                Text="Fecha de Muestreo:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtFechaMuestFuentAguaSupef" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="dd/mm/yyyy"></asp:TextBox>        
                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" 
                ErrorMessage="Ingrese Informacion Fecha de Muestreo"
                    Display="Dynamic" ControlToValidate="txtFechaMuestFuentAguaSupef"
                    ValidationGroup="FuentAguaSupefDet">*</asp:RequiredFieldValidator>      
            </td>
        </tr>        
        <tr>
            <td width="25%">
                <asp:Label ID="Label81" runat="server" SkinID="etiqueta_negra" 
                Text="Hora de Muestreo:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtHoraMuestFuentAguaSupef" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="dd/mm/yyyy"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" 
                ErrorMessage="Ingrese la información de la hora de muestreo"
                    Display="Dynamic" ControlToValidate="txtHoraMuestFuentAguaSupef"
                    ValidationGroup="FuentAguaSupefDet">*</asp:RequiredFieldValidator>  </td>   
        </tr>        
        <tr>
                <td width="25%">
                <asp:Label ID="Label82" runat="server" SkinID="etiqueta_negra" 
                Text="Duración del Muestreo:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtDurMuestFuentAguaSupef" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="dd/mm/yyyy"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" 
                ErrorMessage="Ingrese la información de la fecha de muestreo"
                    Display="Dynamic" ControlToValidate="txtDurMuestFuentAguaSupef"
                    ValidationGroup="FuentAguaSupefDet">*</asp:RequiredFieldValidator>    </td>         
            </tr>        
        <tr>
            <td width="25%">
                <asp:Label ID="Label83" runat="server" SkinID="etiqueta_negra" 
                Text="Periodo de Muestreo:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboPerMuestFuentAguaSupef" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator16" runat="server"
                    ErrorMessage="Seleccione el Periodo de Muestreo" 
                    Display="Dynamic"
                    ControlToValidate="cboPerMuestFuentAguaSupef" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="FuentAguaSupefDet">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarFuentAguaDetSupef" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="FuentAguaSupefDet" OnClick="btnAgregarFuentAguaDetSupef_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarFuentAguaDetSupef" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarFuentAguaDetSupef_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary10" runat="server" 
                ValidationGroup="FuentAguaSupefDet" />             
            </td>            
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" align="right" >
                <asp:Button ID="btnCarFisFuenSupp" runat="server" SkinID="boton_copia"
                    Text="Agregar Caracteristicas Fisicas"
					onclick="btnCarFisFuenSupp_Click"  /></td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhCarFisFuenSupp" Visible="false">  
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label35" runat="server" SkinID="etiqueta_negra" 
                Text="Fuentes:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboFuentesCar" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator17" runat="server"
                    ErrorMessage="Seleccione Una Fuente" 
                    Display="Dynamic"
                    ControlToValidate="cboFuentesCar" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="FuentAguaSupefDetCar">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label31" runat="server" SkinID="etiqueta_negra" 
                Text="Caracteriszación Física:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboCaracterFisic" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator14" runat="server"
                    ErrorMessage="Seleccione CaracteristicaFisica" 
                    Display="Dynamic"
                    ControlToValidate="cboCaracterFisic" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="FuentAguaSupefDetCar">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="25%" style="height: 24px">
                <asp:Label ID="Label32" runat="server" SkinID="etiqueta_negra" 
                Text="Metodo de Determinación:"></asp:Label></td>
            <td width="75%" colspan="3" style="height: 24px">
                <asp:DropDownList ID="cboMetodoDetermin" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator15" runat="server"
                    ErrorMessage="Seleccione Metodo de Determinación" 
                    Display="Dynamic"
                    ControlToValidate="cboMetodoDetermin" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="FuentAguaSupefDetCar">*</asp:CompareValidator></td>
        </tr>
       
        <tr>
            <td width="25%">
                <asp:Label ID="Label33" runat="server" SkinID="etiqueta_negra" 
                Text="Limite de Detección:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtLimitedeDeteccion" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="dd/mm/yyyy"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" 
                ErrorMessage="Ingrese la información Limite de Detección"
                    Display="Dynamic" ControlToValidate="txtLimitedeDeteccion"
                    ValidationGroup="FuentAguaSupefDetCar">*</asp:RequiredFieldValidator>     </td>
        </tr> 
        <tr>
            <td width="25%">
                <asp:Label ID="Label34" runat="server" SkinID="etiqueta_negra" 
                Text="Valor Asignado Fuente:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtValorFuente" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="dd/mm/yyyy"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" 
                ErrorMessage="Ingrese la información Que Asignara a Fuente"
                    Display="Dynamic" ControlToValidate="txtValorFuente"
                    ValidationGroup="FuentAguaSupefDetCar">*</asp:RequiredFieldValidator>    </td> 
        </tr>         
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarCaracSub" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="FuentAguaSupefDetCar" OnClick="btnAgregarCaracSub_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarCarcSub" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarCarcSub_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary12" runat="server" 
                ValidationGroup="FuentAguaSupefDetCar" />              
            </td>            
        </tr>
        
        </asp:PlaceHolder>       
        <tr>
            <td colspan="4" align="right" >
                <asp:Button ID="btnCarQuimFuenSupp" runat="server" SkinID="boton_copia"
                    Text="Agregar Caracteristicas Quimicas"
					onclick="btnCarQuimFuenSupp_Click"  /></td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhCarQuimFuenSupp" Visible="false">          
        <tr>
            <td width="25%">
                <asp:Label ID="Label36" runat="server" SkinID="etiqueta_negra" 
                Text="Fuentes:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboFuentesQuimicas" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator18" runat="server"
                    ErrorMessage="Seleccione Una Fuente" 
                    Display="Dynamic"
                    ControlToValidate="cboFuentesQuimicas" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="FuentesCarQui">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label37" runat="server" SkinID="etiqueta_negra" 
                Text="Caracteriszación Quimicas:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboCaracteristicasQuimicas" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator19" runat="server"
                    ErrorMessage="Seleccione Caracteristica Quimica" 
                    Display="Dynamic"
                    ControlToValidate="cboCaracteristicasQuimicas" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="FuentesCarQui">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label38" runat="server" SkinID="etiqueta_negra" 
                Text="Metodo de Determinación:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboMetDetQuim" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator20" runat="server"
                    ErrorMessage="Seleccione Metodo de Determinación" 
                    Display="Dynamic"
                    ControlToValidate="cboMetDetQuim" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="FuentesCarQui">*</asp:CompareValidator></td>
        </tr>
       
        <tr>
            <td width="25%">
                <asp:Label ID="Label39" runat="server" SkinID="etiqueta_negra" 
                Text="Limite de Detección:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtLimiteDetec" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="dd/mm/yyyy"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" 
                ErrorMessage="Ingrese la información Limite de Detección"
                    Display="Dynamic" ControlToValidate="txtLimiteDetec"
                    ValidationGroup="FuentesCarQui">*</asp:RequiredFieldValidator>     </td>
        </tr> 
        <tr>
            <td width="25%">
                <asp:Label ID="Label40" runat="server" SkinID="etiqueta_negra" 
                Text="Valor Asignado Fuente:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtValorCarQui" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="dd/mm/yyyy"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" 
                ErrorMessage="Ingrese la información Que Asignara a Fuente"
                    Display="Dynamic" ControlToValidate="txtValorCarQui"
                    ValidationGroup="FuentesCarQui">*</asp:RequiredFieldValidator>   </td>  
        </tr>         
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarCarQui" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="FuentesCarQui" OnClick="btnAgregarCarQui_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarCarQui" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarCarQui_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary11" runat="server" 
                ValidationGroup="FuentesCarQui" />              
            </td>            
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvCalidadFuentesSuper" AutoGenerateColumns="True"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de detalles de fuentes" OnRowCreated="grvCalidadFuentesSuper_RowCreated">
                    <Columns>                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblEcsId" runat="server" Visible="false" Text='<%# Eval("ECS_ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>        
    <%--Calidad de agua - fuentes subterraneas --%>
        <tr>
            <td colspan="4" width="100%">
                3.1.8.1 Fuentes de Agua Subterraneas
            </td>
        </tr>
		<tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <tr>
            <td width="25%"><asp:Label ID="Label41" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre del laboratorio que realizó el análisis" /></td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboNombLabFuentAguaSubt" runat="server" SkinID="lista_desplegable" AutoPostBack="true" OnSelectedIndexChanged="cboNombLabFuentAguaSubt_SelectedIndexChanged">
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator21" runat="server"
                    ErrorMessage="Seleccione el Nombre del laboratorio" Display="Dynamic"
                    ControlToValidate="cboNombLabFuentAguaSubt" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="FuentAguaSubt">*</asp:CompareValidator></td>
        </tr>
        <tr id="trNombreLab2" runat="server" visible="false">
            <td width="25%">
                <asp:Label ID="lblotroLabFuentAguaSubt" runat="server" SkinID="etiqueta_negra" 
                Text="Otro laboratorio que realizó el análisis"></asp:Label>
            </td>
            <td colspan = "3" width="75%">
                <asp:TextBox ID="txtOtroLabFuentAguaSubt" runat="server"  SkinID="texto_sintamano" MaxLength="200" Width="99%"
                Text=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="right" >
                <asp:Button ID="btnNuevaFuentAguaSubt" runat="server" SkinID="boton_copia"
                    Text="Agregar información de fuentes de agua subterraneas" 
					onclick="btnNuevaFuentAguaSubt_Click"  /></td>
        </tr>
		
        <asp:PlaceHolder runat="server" ID="plhFuentAguaSubt" Visible="False">
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label42" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo Fuente de Agua Subterránea" /></td>
            <td colspan = "3" width="75%">
                <asp:DropDownList ID="cboTipoFuenteFuentAguaSubt" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator23" runat="server"
                    ErrorMessage="Seleccione el Tipo Fuente de Agua" Display="Dynamic"
                    ControlToValidate="cboTipoFuenteFuentAguaSubt" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="FuentAguaSubt">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label43" runat="server" SkinID="etiqueta_negra" 
                Text="Descripción del Piezómetro o distancia del Nivel Freático"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtDescPiezoFuentAguaSubt" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" 
                ErrorMessage="Ingrese la Descripción del Piezómetro o distancia del Nivel Freático"
                    Display="Dynamic" ControlToValidate="txtDescPiezoFuentAguaSubt"
                    ValidationGroup="FuentAguaSubt">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label44" runat="server" SkinID="etiqueta_negra" 
                Text="Coordenadas de ubicación (Datum Magna-Sirgas)"></asp:Label>
            </td>
            <td colspan = "3" width="75%" align="right" style="text-align:right">
                <uc2:ctrCoordenadasPto runat="server" ID="ctrCoorFuentAguaSubt" />
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label45" runat="server" SkinID="etiqueta_negra" 
                Text="Usos"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtUsosFuentAguaSubt" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" 
                ErrorMessage="Ingrese el Usos"
                    Display="Dynamic" ControlToValidate="txtUsosFuentAguaSubt"
                    ValidationGroup="FuentAguaSubt">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarFuentAguaSubt" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="FuentAguaSubt" OnClick="btnAgregarFuentAguaSubt_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarFuentAguaSubt" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarFuentAguaSubt_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary14" runat="server" 
                ValidationGroup="FuentAguaSubt" />              
            </td>            
        </tr>  
        </asp:PlaceHolder>     
    
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvFuentAguaSubt" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de Inventario de Fuentes de Agua Subterraneas en Área de Influencia Directa" OnRowDeleting="grvFuentAguaSubt_RowDeleting">
                    <Columns>             
                        <asp:CommandField ShowDeleteButton="True" />      
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblEptId" runat="server" Visible="false" Text='<%# Eval("EPT_ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:BoundField DataField="EPT_ID" HeaderText="No." />
                        <asp:BoundField DataField="ETS_TIPO_AGUA_SUBT" HeaderText="Tipo Fuente de Agua Subterránea" />
                        <asp:BoundField DataField="EPT_DESC_PIEZO_DIST_NFREAT" HeaderText="Descripción del Piezómetro o distancia del Nivel Freático" />
                        <asp:BoundField DataField="EPT_DESC_PIEZO_DIST_NFREAT" HeaderText="Coordenanda Norte de Ubicación" />
                        <asp:BoundField DataField="EPT_COOR_ESTE_UBICACION" HeaderText="Coordenanda Este de Ubicación" />
                        <asp:BoundField DataField="EPT_USOS" HeaderText="Usos" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="right" >
                <asp:Button ID="btnDetalleFuentesSub" runat="server" SkinID="boton_copia"
                    Text="Agregar Detalles Fuentes" 
					onclick="btnDetalleFuentesSub_Click"  /></td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhDetallesFuentesSub" Visible="False">
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label60" runat="server" SkinID="etiqueta_negra" 
                Text="Fuentes:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboFuentesDet" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator28" runat="server"
                    ErrorMessage="Seleccione Fuente" 
                    Display="Dynamic"
                    ControlToValidate="cboFuentesDet" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="FuentAguaSubtDet">*</asp:CompareValidator></td>
           
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label46" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Muestra:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboTipoMuestraFuentAguaSubt" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator22" runat="server"
                    ErrorMessage="Seleccione el Tipo de Muestra" 
                    Display="Dynamic"
                    ControlToValidate="cboTipoMuestraFuentAguaSubt" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="FuentAguaSubtDet">*</asp:CompareValidator></td>
           
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label47" runat="server" SkinID="etiqueta_negra" 
                Text="Fecha de Muestreo:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtFechaMuestFuentAguaSubt" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="dd/mm/yyyy"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" 
                ErrorMessage="Ingrese la información de la fecha de muestreo"
                    Display="Dynamic" ControlToValidate="txtFechaMuestFuentAguaSubt"
                    ValidationGroup="FuentAguaSubtDet">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator24" runat="server"
                    ErrorMessage="Fecha de muestreo erronea" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Date"
                    ControlToValidate="txtFechaMuestFuentAguaSubt" 
                    ValidationGroup="FuentAguaSubtDet"></asp:CompareValidator>
            </td>
        </tr>        
        <tr>
            <td width="25%">
            <asp:Label ID="Label48" runat="server" SkinID="etiqueta_negra" 
            Text="Hora de Muestreo:"></asp:Label></td>
        <td width="75%" colspan="3">
            <asp:TextBox ID="txtHoraMuestFuentAguaSubt" runat="server" SkinID="texto_sintamano" 
            MaxLength="200" Width="99%"
            ToolTip="HH:MM"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" 
            ErrorMessage="Ingrese la información de la hora de muestreo"
                Display="Dynamic" ControlToValidate="txtHoraMuestFuentAguaSubt"
                ValidationGroup="FuentAguaSubtDet">*</asp:RequiredFieldValidator>
                </td>       
        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
                    TargetControlID="txtHoraMuestFuentAguaSubt" Mask="99:99"  MaskType="Time" 
                    InputDirection="RightToLeft" ErrorTooltipEnabled="True" AcceptAMPM="true"
                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="">
                    </cc1:MaskedEditExtender>  
        </tr>        
        <tr>
            <td width="25%">
            <asp:Label ID="Label49" runat="server" SkinID="etiqueta_negra" 
            Text="Duración del Muestreo:"></asp:Label></td>
        <td width="75%" colspan="3">
            <asp:TextBox ID="txtDurMuestFuentAguaSubt" runat="server" SkinID="texto_sintamano" 
            MaxLength="200" Width="99%"
            ToolTip="dd/mm/yyyy"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" 
            ErrorMessage="Ingrese la información de la fecha de muestreo"
                Display="Dynamic" ControlToValidate="txtDurMuestFuentAguaSubt"
                ValidationGroup="FuentAguaSubtDet">*</asp:RequiredFieldValidator>
            </td>
        </tr>        
        <tr>
            <td width="25%">
                <asp:Label ID="Label54" runat="server" SkinID="etiqueta_negra" 
                Text="Periodo de Muestreo:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboPerMuestFuentAguaSubt" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator27" runat="server"
                    ErrorMessage="Seleccione el Periodo de Muestreo" 
                    Display="Dynamic"
                    ControlToValidate="cboPerMuestFuentAguaSubt" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="FuentAguaSubtDet">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarFuentAguaSubtDet" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="FuentAguaSubtDet" OnClick="btnAgregarFuentAguaSubtDet_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarFuentAguaSubtDet" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarFuentAguaSubtDet_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary13" runat="server" 
                ValidationGroup="FuentAguaSubtDet" />
            </td>
        </tr> 
        </asp:PlaceHolder>  
          <tr>
            <td colspan="4" align="right" >
                <asp:Button ID="btnCarFuentesSubt" runat="server" SkinID="boton_copia"
                    Text="Agregar Caracteristicas Quimicas" 
					onclick="btnCarFuentesSubt_Click"  /></td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhCarFuentesSubt" Visible="False">
        <tr>
            <td width="25%"><asp:Label ID="Label73" runat="server" SkinID="etiqueta_negra" 
                Text="Fuente" /></td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboFuenteQuimSub" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator26" runat="server"
                    ErrorMessage="Seleccione Fuente" Display="Dynamic"
                    ControlToValidate="cboFuenteQuimSub" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="FuenteQuimSub">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="25%"><asp:Label ID="Label61" runat="server" SkinID="etiqueta_negra" 
                Text="Caracterización Quimica" /></td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboCaracQuimSub" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator25" runat="server"
                    ErrorMessage="Seleccione Caracterización Quimica" Display="Dynamic"
                    ControlToValidate="cboCaracQuimSub" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="FuenteQuimSub">*</asp:CompareValidator></td>
        </tr>
          <tr>
            <td width="25%">
                <asp:Label ID="Label74" runat="server" SkinID="etiqueta_negra" 
                Text="Metodo de Determinación:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboMetDeterQuimSub" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator29" runat="server"
                    ErrorMessage="Seleccione Metodo de Determinación" 
                    Display="Dynamic"
                    ControlToValidate="cboMetDeterQuimSub" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="FuenteQuimSub">*</asp:CompareValidator></td>
        </tr>
       
        <tr>
            <td width="25%">
                <asp:Label ID="Label75" runat="server" SkinID="etiqueta_negra" 
                Text="Limite de Detección:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtLimitDeteccSub" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="dd/mm/yyyy"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" 
                ErrorMessage="Ingrese la información Limite de Detección"
                    Display="Dynamic" ControlToValidate="txtLimitDeteccSub"
                    ValidationGroup="FuenteQuimSub">*</asp:RequiredFieldValidator>     </td>
        </tr> 
        <tr>
            <td width="25%">
                <asp:Label ID="Label76" runat="server" SkinID="etiqueta_negra" 
                Text="Valor Asignado Fuente:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtValorFuenteQuimSub" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="dd/mm/yyyy"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" 
                ErrorMessage="Ingrese la información Que Asignara a Fuente"
                    Display="Dynamic" ControlToValidate="txtValorFuenteQuimSub"
                    ValidationGroup="FuenteQuimSub">*</asp:RequiredFieldValidator>   </td>  
        </tr>         
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarCaracQuiSub" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="FuenteQuimSub" OnClick="btnAgregarCaracQuiSub_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarCaracQuiSub" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarCaracQuiSub_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary15" runat="server" 
                ValidationGroup="FuenteQuimSub" />              
            </td>            
        </tr>    
        </asp:PlaceHolder>  
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="drvFuentesSubDet" AutoGenerateColumns="True"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de detalles de fuentes" OnRowCreated="grvCalidadFuentesSub_RowCreated">
                    <Columns>                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblEctId" runat="server" Visible="false" Text='<%# Eval("ECT_ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>  
        
        <%--Oceanografia - Sistemas de Corrientes --%>
        <tr>
            <td colspan="4" width="100%">
                3.1.9 Oceanografía
            </td>
        </tr>
        <tr>
            <td width="25%">
                3.1.9.1 Sistemas de Corrientes
            </td>
			<td width="70%" align="right" colspan="3">
                <asp:Button ID="btnNuevoSistCorriente" runat="server" SkinID="boton_copia"
                    Text="Agregar información de Sistemas de Corrientes" 
					onclick="btnNuevoSistCorriente_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhSistCorriente" Visible="False">
		
        <tr>
            <td width="25%">
                <asp:Label ID="Label77" runat="server" SkinID="etiqueta_negra" 
                Text="Pluma de Dispersión"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtPlumaDispSistCorriente" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" 
                ErrorMessage="Ingrese la Pluma de Dispersión"
                    Display="Dynamic" ControlToValidate="txtPlumaDispSistCorriente"
                    ValidationGroup="SistCorriente">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label85" runat="server" SkinID="etiqueta_negra" 
                Text="Efecto Viento / Marea"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtEfectVientMarSistCorriente" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator45" runat="server" 
                ErrorMessage="Ingrese la información del Efecto Viento / Marea"
                    Display="Dynamic" ControlToValidate="txtEfectVientMarSistCorriente"
                    ValidationGroup="SistCorriente">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label86" runat="server" SkinID="etiqueta_negra" 
                Text="Dirección"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtDireccionSistCorriente" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator46" runat="server" 
                ErrorMessage="Ingrese la información de la dirección"
                    Display="Dynamic" ControlToValidate="txtDireccionSistCorriente"
                    ValidationGroup="SistCorriente">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label87" runat="server" SkinID="etiqueta_negra" 
                Text="Probabilidad de Ocurrencia"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtProbOcurSistCorriente" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator47" runat="server" 
                ErrorMessage="Ingrese la información de la Probabilidad de Ocurrencia"
                    Display="Dynamic" ControlToValidate="txtDireccionSistCorriente"
                    ValidationGroup="SistCorriente">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label88" runat="server" SkinID="etiqueta_negra" 
                Text="Intensidad"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtIntenSistCorriente" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator48" runat="server" 
                ErrorMessage="Ingrese la información de la Intensidad"
                    Display="Dynamic" ControlToValidate="txtDireccionSistCorriente"
                    ValidationGroup="SistCorriente">*</asp:RequiredFieldValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarSistCorriente" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="SistCorriente" OnClick="btnAgregarSistCorriente_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarSistCorriente" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarSistCorriente_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary16" runat="server" 
                ValidationGroup="SistCorriente" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvSistCorriente" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de los Sistemas de Corrientes" OnRowDeleting="grvSistCorriente_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />   
                        <asp:BoundField DataField="ECO_PLUMA_DISPERSION" HeaderText="Pluma de Dispersión" />
                        <asp:BoundField DataField="ECO_EFEC_VIENTO_MAREA" HeaderText="Efecto Viento / Marea" />
                        <asp:BoundField DataField="ECO_DIRECCION" HeaderText="Dirección" />
                        <asp:BoundField DataField="ECO_PROB_OCURRENCIA" HeaderText="Probabilidad de Ocurrencia" />
                        <asp:BoundField DataField="ECO_INTENSIDAD" HeaderText="Intensidad" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
                <%--Oceanografia - Tipo de Oleaje --%>
        <tr>
            <td colspan="4" width="100%">
                3.1.9.2 Oleaje en Playas
            </td>
        </tr>
		<tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label89" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Oleaje:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboTipoOleaje" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                  <asp:CompareValidator ID="CompareValidator30" runat="server"
                    ErrorMessage="Seleccione Tipo de Oleaje" 
                    Display="Dynamic"
                    ControlToValidate="cboTipoOleaje" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="TipoOleaje">*</asp:CompareValidator>
            
            </td>
        </tr>
        <tr>            
            <td colspan="4" align="right">
                <asp:Button id="btnAsignarOleaje" runat="server" SkinID="boton_copia" Text="Asignar Oleaje" ValidationGroup="TipoOleaje" OnClick="btnAsignarOleaje_Click" />
            </td>
        </tr>
          <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary17" runat="server" 
                ValidationGroup="TipoOleaje" />
            </td>
        </tr>
              <%--Oceanografia - Olas --%>
        <tr>
            <td width="25%">
                3.1.9.3 Olas
            </td>
			<td width="75%" align="right" colspan="3">
                <asp:Button ID="btnNuevoOlas" runat="server" SkinID="boton_copia"
                    Text="Agregar información de Olas" 
					onclick="btnNuevoOlas_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhOlas" Visible="False">
		
        <tr>
            <td width="25%">
                <asp:Label ID="Label90" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Olas"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboTipoOlas" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator31" runat="server"
                    ErrorMessage="Seleccione el Tipo de Olas" 
                    Display="Dynamic"
                    ControlToValidate="cboTipoOlas" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="Olas">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label91" runat="server" SkinID="etiqueta_negra" 
                Text="Frecuencia"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtFrecuenciaOlas" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator49" runat="server" 
                ErrorMessage="Ingrese la información de la Frecuencia"
                    Display="Dynamic" ControlToValidate="txtFrecuenciaOlas"
                    ValidationGroup="Olas">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label92" runat="server" SkinID="etiqueta_negra" 
                Text="Altura"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtAlturaOlas" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator50" runat="server" 
                ErrorMessage="Ingrese la información de la Altura"
                    Display="Dynamic" ControlToValidate="txtAlturaOlas"
                    ValidationGroup="Olas">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>        
            <td width="25%">
                <asp:Label ID="Label93" runat="server" SkinID="etiqueta_negra" 
                Text="Dirección"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtDireccionOlas" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator51" runat="server" 
                ErrorMessage="Ingrese la información de la Dirección"
                    Display="Dynamic" ControlToValidate="txtDireccionOlas"
                    ValidationGroup="Olas">*</asp:RequiredFieldValidator>
            </td>
        </tr>
    	<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarOlas" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="Olas" OnClick="btnAgregarOlas_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarOlas" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarOlas_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary18" runat="server" 
                ValidationGroup="Olas" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvOlas" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de las olas" OnRowDeleting="grvOlas_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />  
                        <asp:BoundField DataField="EOO_TIPO_OLAS" HeaderText="Tipo de ola" />
                        <asp:BoundField DataField="EOC_FRECUENCIA" HeaderText="Frecuencia" />
                        <asp:BoundField DataField="EOC_ALTURA" HeaderText="Altura" />
                        <asp:BoundField DataField="EOC_DIRECCION" HeaderText="Dirección" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
          <%--Oceanografia - Mareas --%>
        <tr>
            <td  width="25%">
                3.1.9.4 Mareas
            </td>
			<td width="75%" align="right" colspan="3">
                <asp:Button ID="btnNuevoMareas" runat="server" SkinID="boton_copia"
                    Text="Agregar información de Mareas" 
					onclick="btnNuevoMareas_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhMareas" Visible="False">
        <tr>
            <td width="25%">
                <asp:Label ID="Label94" runat="server" SkinID="etiqueta_negra" 
                Text="Descripción"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtDescMareas" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator52" runat="server" 
                ErrorMessage="Ingrese la Descripción"
                    Display="Dynamic" ControlToValidate="txtDescMareas"
                    ValidationGroup="Mareas">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label95" runat="server" SkinID="etiqueta_negra" 
                Text="Altura Máxima Sicigias (m)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtAltMaxSicigiasMareas" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator53" runat="server" 
                    ErrorMessage="Ingrese la información de la Altura Máxima Sicigias"
                    Display="Dynamic" ControlToValidate="txtAltMaxSicigiasMareas"
                    ValidationGroup="Mareas">*</asp:RequiredFieldValidator>
                <asp:CompareValidator runat="server" ID="covAltMacSicigias"
                    ControlToValidate="txtAltMaxSicigiasMareas" Display="Dynamic" 
                    Operator="DataTypeCheck" Type="Double"
                    ValidationGroup="Mareas" 
                    ErrorMessage="La información la Altura Máxima Sicigias debe ser numérica">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label96" runat="server" SkinID="etiqueta_negra" 
                Text="Altura Mínima Sicigias (m)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtAltMinSicigiasMareas" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator54" runat="server" 
                    ErrorMessage="Ingrese la información de la Altura Mínima Sicigias"
                    Display="Dynamic" ControlToValidate="txtAltMinSicigiasMareas"
                    ValidationGroup="Mareas">*</asp:RequiredFieldValidator>
                <asp:CompareValidator runat="server" ID="CompareValidator32"
                    ControlToValidate="txtAltMinSicigiasMareas" Display="Dynamic" 
                    Operator="DataTypeCheck" Type="Double"
                    ValidationGroup="Mareas" 
                    ErrorMessage="La información la Altura Mínima Sicigias debe ser numérica">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label97" runat="server" SkinID="etiqueta_negra" 
                Text="Altura Máxima Cuadratura (m)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtAltMaxCuadraturaMareas" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator55" runat="server" 
                    ErrorMessage="Ingrese la información de la Altura Máxima Cuadratura"
                    Display="Dynamic" ControlToValidate="txtAltMaxCuadraturaMareas"
                    ValidationGroup="Mareas">*</asp:RequiredFieldValidator>
                <asp:CompareValidator runat="server" ID="CompareValidator33"
                    ControlToValidate="txtAltMaxCuadraturaMareas" Display="Dynamic" 
                    Operator="DataTypeCheck" Type="Double"
                    ValidationGroup="Mareas" 
                    ErrorMessage="La información la Altura Máxima Cuadratura debe ser numérica">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label98" runat="server" SkinID="etiqueta_negra" 
                Text="Altura Mínima Cuadratura (m)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtAltMinCuadraturaMareas" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator56" runat="server" 
                    ErrorMessage="Ingrese la información de la Altura Mínima Cuadratura"
                    Display="Dynamic" ControlToValidate="txtAltMinCuadraturaMareas"
                    ValidationGroup="Mareas">*</asp:RequiredFieldValidator>
                <asp:CompareValidator runat="server" ID="CompareValidator34"
                    ControlToValidate="txtAltMinCuadraturaMareas" Display="Dynamic" 
                    Operator="DataTypeCheck" Type="Double"
                    ValidationGroup="Mareas" 
                    ErrorMessage="La información la Altura Mínima Cuadratura debe ser numérica">*</asp:CompareValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarMareas" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="Mareas"
                    OnClick="btnAgregarMareas_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarMareas" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarMareas_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary19" runat="server" 
                ValidationGroup="Mareas" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvMareas" AutoGenerateColumns="False"
                width="99%" OnRowDeleting="grvMareas_RowDeleting" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de las Mareas">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField HeaderText="Descripción" DataField="EMO_DESCRIPCION" />
                        <asp:BoundField HeaderText="Altura Máxima Sicigias (m)" DataField="EMO_ALT_MAX_SICIGIAS" />
                        <asp:BoundField HeaderText="Altura Mínima Sicigias (m)" DataField="EMO_ALT_MIN_SICIGIAS" />
                        <asp:BoundField HeaderText="Altura Máxima Cuadratura (m)" DataField="EMO_ALT_MAX_CUADRATU" />
                        <asp:BoundField HeaderText="Altura Mínima Cuadratura (m)" DataField="EMO_ALT_MIN_CUADRATU" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
                <%--Oceanografia - Tormentas --%>
        <tr>
            <td width="25%">
                3.1.9.4 Tormentas
            </td>
			<td width="75%" align="right" colspan="3">
                <asp:Button ID="btnNuevoTormentas" runat="server" SkinID="boton_copia"
                    Text="Agregar información de Tormentas" 
					onclick="btnNuevoTormentas_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhTormentas" Visible="False">
        <tr>
            <td width="25%">
                <asp:Label ID="Label99" runat="server" SkinID="etiqueta_negra" 
                Text="Dirección"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtDireccionTormentas" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator57" runat="server" 
                ErrorMessage="Ingrese la Dirección"
                    Display="Dynamic" ControlToValidate="txtDireccionTormentas"
                    ValidationGroup="Tormentas">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label100" runat="server" SkinID="etiqueta_negra" 
                Text="Frecuencia"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtFrecuenciaTormentas" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator58" runat="server" 
                ErrorMessage="Ingrese la información de la Frecuencia"
                    Display="Dynamic" ControlToValidate="txtFrecuenciaTormentas"
                    ValidationGroup="Tormentas">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label101" runat="server" SkinID="etiqueta_negra" 
                Text="Periodos"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtPeriodosTormentas" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator59" runat="server" 
                ErrorMessage="Ingrese la información de los Periodos"
                    Display="Dynamic" ControlToValidate="txtPeriodosTormentas"
                    ValidationGroup="Tormentas">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label102" runat="server" SkinID="etiqueta_negra" 
                Text="Altura de las Olas"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtAltOlasTormentas" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator60" runat="server" 
                ErrorMessage="Ingrese la información de la Altura de las Olas"
                    Display="Dynamic" ControlToValidate="txtAltOlasTormentas"
                    ValidationGroup="Tormentas">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label103" runat="server" SkinID="etiqueta_negra" 
                Text="Velocidad de Propagación"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtVelPropagTormentas" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator61" runat="server" 
                ErrorMessage="Ingrese la información de la Velocidad de Propagación"
                    Display="Dynamic" ControlToValidate="txtVelPropagTormentas"
                    ValidationGroup="Tormentas">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label104" runat="server" SkinID="etiqueta_negra" 
                Text="Épocas de Mayor Actividad"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtEpMayorActTormentas" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator62" runat="server" 
                ErrorMessage="Ingrese la información de las Épocas de Mayor Actividad"
                    Display="Dynamic" ControlToValidate="txtEpMayorActTormentas"
                    ValidationGroup="Tormentas">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label105" runat="server" SkinID="etiqueta_negra" 
                Text="Efectos sobre las Instalaciones Portuarias"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtEfectInstPortTormentas" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator63" runat="server" 
                ErrorMessage="Ingrese la información de los Efectos sobre las Instalaciones Portuarias"
                    Display="Dynamic" ControlToValidate="txtEfectInstPortTormentas"
                    ValidationGroup="Tormentas">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label106" runat="server" SkinID="etiqueta_negra" 
                Text="Predicciones del Fenómeno"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtPredicFenTormentas" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator64" runat="server" 
                ErrorMessage="Ingrese la información de las Predicciones del Fenómeno"
                    Display="Dynamic" ControlToValidate="txtPredicFenTormentas"
                    ValidationGroup="Tormentas">*</asp:RequiredFieldValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarTormentas" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="Tormentas"
                    OnClick="btnAgregarTormentas_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarTormentas" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarTormentas_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary20" runat="server" 
                ValidationGroup="Tormentas" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvTormentas" AutoGenerateColumns="False"
                width="99%" OnRowDeleting="grvTormentas_RowDeleting" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de los Sistemas de Corrientes">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField HeaderText="Dirección" DataField="ETO_DIRECCION"/>
                        <asp:BoundField HeaderText="Frecuencia" DataField="ETO_FRECUENCIA"/>
                        <asp:BoundField HeaderText="Periodos" DataField="ETO_PERIODOS"/>
                        <asp:BoundField HeaderText="Altura de las Olas" DataField="ETO_ALTURA_OLAS"/>
                        <asp:BoundField HeaderText="Velocidad de Propagación" DataField="ETO_VELOCIDAD_PROP"/>
                        <asp:BoundField HeaderText="Épocas de Mayor Actividad" DataField="ETO_EPOC_MAX_ACTIVIDAD"/>
                        <asp:BoundField HeaderText="Efectos sobre las Instalaciones Portuarias" DataField="ETO_EF_INST_PORTUARIAS"/>
                        <asp:BoundField HeaderText="Predicciones del Fenómeno" DataField="ETO_PRED_FENOMENO"/>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
              <%--Calidad del Aire - Fuentes de Emisión Existentes --%>
        <tr>
            <td colspan="4" width="100%">
                3.1.10 Calidad del Aire
            </td>
        </tr>
        <tr>
            <td width="25%">
                3.1.10.1 Fuentes de Emisión Existentes
            </td>
			<td width="75%" align="right" colspan="3">
                <asp:Button ID="btnNuevoFuentEmiExist" runat="server" SkinID="boton_copia"
                    Text="Agregar información de Fuentes de Emisión Fijas" 
					onclick="btnNuevoFuentEmiExist_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhFuentEmiExist" Visible="False">
		
		
        <tr>
            <td width="25%">
                <asp:Label ID="Label107" runat="server" SkinID="etiqueta_negra" 
                Text="Descripción"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtDescripcionFuentEmiExist" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator65" runat="server" 
                ErrorMessage="Ingrese la Descripción"
                    Display="Dynamic" ControlToValidate="txtDescripcionFuentEmiExist"
                    ValidationGroup="FuentEmiExist">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label108" runat="server" SkinID="etiqueta_negra" 
                Text="Coordenadas de Ubicación Aproximadas (m)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <uc2:ctrCoordenadasPto runat="server" ID="ctrCoorFuentEmiExist" />
            </td>
        </tr>
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarFuentEmiExist" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="FuentEmiExist" OnClick="btnAgregarFuentEmiExist_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarFuentEmiExist" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarFuentEmiExist_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary21" runat="server" 
                ValidationGroup="FuentEmiExist" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4">
                <asp:Label ID="Label109" runat="server" SkinID="etiqueta_negra" 
                Text="Fijas"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvFuentEmiExist" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información las fuentes de emisión existentes">
                    <Columns>
                        <asp:BoundField DataField="EFF_ID" HeaderText="No." />                        
                        <asp:BoundField DataField="EFF_DESCRIPCION" HeaderText="Descripción" />
                        <asp:BoundField DataField="EFF_COOR_NORTE_UBICA" HeaderText="Norte" />
                        <asp:BoundField DataField="EFF_COOR_ESTE_UBICA" HeaderText="Este" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
          <tr>
            <td width="25%">
                <asp:Label ID="Label110" runat="server" SkinID="etiqueta_negra" 
                Text="Descripción Fuentes de Emisión Moviles"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtDescFuentEmiMov" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>              
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label111" runat="server" SkinID="etiqueta_negra" 
                Text="Descripción Fuentes de Emisión Lineales"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtDescFuentEmiLin" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>             
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label112" runat="server" SkinID="etiqueta_negra" 
                Text="Descripción Fuentes de Emisión de Área"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtDescFuentEmiArea" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>                
            </td>
        </tr>
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarFuenteDet2" runat="server" SkinID="boton_copia"
                    Text="Asignar" ValidationGroup="FuentEmiExist" OnClick="btnAgregarFuenteDet2_Click" Visible="False"  />
            </td>
            <td align="center" width="25%">                
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary22" runat="server" 
                ValidationGroup="FuentEmiExist" />
            </td>
        </tr>
 <%--Oceanografia - Sitios de Monitoreo de Calidad del Aire --%>
        <tr>
            <td width="25%">
                3.1.10.2 Sitios de Monitoreo de Calidad del Aire
            </td>
			<td width="75%" align="right" colspan="3">
                <asp:Button ID="btnNuevoSitMonitCalAire" runat="server" SkinID="boton_copia"
                    Text="Agregar información de Monitoreo de Calidad del Aire" 
					onclick="btnNuevoSitMonitCalAire_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhSitMonitCalAire" Visible="False">
		
        <tr>
            <td width="25%">
                <asp:Label ID="Label113" runat="server" SkinID="etiqueta_negra" 
                Text="Altitud (m.s.n.m.)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtAltSitMonitCalAire" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator66" runat="server" 
                ErrorMessage="Ingrese la información de la Altitud (m.s.n.m.)"
                    Display="Dynamic" ControlToValidate="txtAltSitMonitCalAire"
                    ValidationGroup="SitMonitCalAire">*</asp:RequiredFieldValidator>
                <asp:CompareValidator runat="server" ID="covAltitudSitMonitCalAire" 
                    ControlToValidate="txtAltSitMonitCalAire" Display="Dynamic" 
                    ErrorMessage="La información de la Altitud (m.s.n.m.) debe ser un dato numérico" 
                    Operator="DataTypeCheck" Type="Double"
                    ValidationGroup="SitMonitCalAire">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label114" runat="server" SkinID="etiqueta_negra" 
                Text="Descripción"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtDescSitMonitCalAire" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator67" runat="server" 
                ErrorMessage="Ingrese la Descripción"
                    Display="Dynamic" ControlToValidate="txtDescSitMonitCalAire"
                    ValidationGroup="SitMonitCalAire">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%" >
                <asp:Label ID="Label115" runat="server" SkinID="etiqueta_negra" 
                Text="Coordenadas de Ubicación (m)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <uc2:ctrCoordenadasPto runat="server" ID="ctrCoorSitMonitCalAire" />
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarSitMonitCalAire" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="SitMonitCalAire"
                    OnClick="btnAgregarSitMonitCalAire_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarSitMonitCalAire" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarSitMonitCalAire_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary23" runat="server" 
                ValidationGroup="SitMonitCalAire" />
            </td>
        </tr> 
        </asp:PlaceHolder>       
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvSitMonitCalAire" AutoGenerateColumns="False"
                width="99%" OnRowDeleting="grvSitMonitCalAire_RowDeleting" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de los Sitios de Monitoreo de Calidad del Aire">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField HeaderText="No. Sitio de Monitoreo" DataField="ESM_NO_SITIO" />
                        <asp:BoundField HeaderText="Descripción" DataField="ESM_DESCRIPCION"/>
                        <asp:BoundField HeaderText="Altitud (m.s.n.m.)" DataField="ESM_ALTITUD"/>
                        <asp:BoundField HeaderText="Coordenadas de Ubicación Norte (m)" DataField="ESM_COOR_NORTE_UBICACION"/>
                        <asp:BoundField HeaderText="Coordenadas de Ubicación Este (m)" DataField="ESM_COOR_ESTE_UBICACION"/>                        
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
          <tr>
            <td colspan="4" width="100%">
                3.1.10.3 Resultados de Monitoreo de Calidad del Aire
            </td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label120" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre del laboratorio que realizó el análisis:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:DropDownList ID="cboLabMonitCalAire" runat="server" SkinID="lista_desplegable" ></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator36" runat="server"
                    ErrorMessage="Seleccione el laboratorio que realizó el análisis" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboLabMonitCalAire" 
                    ValidationGroup="FuentRuidExist">*</asp:CompareValidator>
            </td>
        </tr>
        	<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAsifnarLabFuentRuid" runat="server" SkinID="boton_copia"
                    Text="Asignar" ValidationGroup="FuentRuidExist"
                    OnClick="btnAsifnarLabFuentRuid_Click" />
            </td>
            <td align="center" width="25%">             
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary25" runat="server" 
                ValidationGroup="FuentRuidExist" />
            </td>
        </tr>
        <tr>            
            <td colspan = "4" align="right">       
              <asp:Button ID="btnAsignarDetallesSitios" runat="server" SkinID="boton_copia"
                    Text="Asignar Detalles Sitios" 
                    OnClick="btnAsignarDetallesSitios_Click" />         
            </td>
        </tr>
        
       <asp:PlaceHolder runat="server" ID="plhDetallesSitios" Visible="False">       
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label123" runat="server" SkinID="etiqueta_negra" 
                Text="Seleccione Sitio:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:DropDownList ID="cboSitioDetalles" runat="server" SkinID="lista_desplegable" ></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator38" runat="server"
                    ErrorMessage="Seleccione el laboratorio que realizó el análisis" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboSitioDetalles" 
                    ValidationGroup="SitioDetalles">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label116" runat="server" SkinID="etiqueta_negra" 
                Text="Fecha de Muestreo"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtFeMuestSitMonitCalAire" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator68" runat="server" 
                ErrorMessage="Ingrese la Fecha de Muestreo"
                    Display="Dynamic" ControlToValidate="txtFeMuestSitMonitCalAire"
                    ValidationGroup="SitioDetalles">*</asp:RequiredFieldValidator>
                <asp:CompareValidator runat="server" ID="CompareValidator35"
                    ControlToValidate="txtFeMuestSitMonitCalAire" Display="Dynamic" 
                    Operator="DataTypeCheck" Type="Date"
                    ValidationGroup="SitioDetalles" 
                    ErrorMessage="La información de Fecha de Muestreo debe ser una fecha">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>            
            <td width="25%">
                <asp:Label ID="Label117" runat="server" SkinID="etiqueta_negra" 
                Text="Hora de Muestreo"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtHoMuestSitMonitCalAire" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <cc1:MaskedEditExtender ID="MaskedEditExtender9" runat="server" 
                    TargetControlID="txtHoMuestSitMonitCalAire" Mask="99:99"  MaskType="Time" 
                    InputDirection="RightToLeft" ErrorTooltipEnabled="True" AcceptAMPM="true"
                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="">
                    </cc1:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator69" runat="server" 
                ErrorMessage="Ingrese la Hora de Muestreo"
                    Display="Dynamic" ControlToValidate="txtHoMuestSitMonitCalAire"
                    ValidationGroup="SitioDetalles">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label118" runat="server" SkinID="etiqueta_negra" 
                Text="Duración de Muestreo (h)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtDurMuestSitMonitCalAire" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator70" runat="server" 
                ErrorMessage="Ingrese la Duración de Muestreo"
                    Display="Dynamic" ControlToValidate="txtDurMuestSitMonitCalAire"
                    ValidationGroup="SitioDetalles">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label119" runat="server" SkinID="etiqueta_negra" 
                Text="Frecuencia de Muestreo"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtFrecMuestSitMonitCalAire" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator71" runat="server" 
                ErrorMessage="Ingrese la Frecuencia de Muestreo"
                    Display="Dynamic" ControlToValidate="txtFrecMuestSitMonitCalAire"
                    ValidationGroup="SitioDetalles">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarDetSitios" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="FuentRuidExist"
                    OnClick="btnAgregarDetSitios_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarDetSitios" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarDetSitios_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary26" runat="server" 
                ValidationGroup="SitioDetalles" />
            </td>
        </tr>
        </asp:PlaceHolder> 
        <tr>            
            <td colspan = "4" align="right">       
              <asp:Button ID="btnCarSitiosAire" runat="server" SkinID="boton_copia"
                    Text="Asignar Caracteristicas Sitios" 
                    OnClick="btnCarSitiosAire_Click" />         
            </td>
        </tr>
        
       <asp:PlaceHolder runat="server" ID="plhCarSitiosAire" Visible="False">     
       
        <tr>
            <td width="25%">
                <asp:Label ID="Label124" runat="server" SkinID="etiqueta_negra" 
                Text="Seleccione Sitio:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:DropDownList ID="cboSitiosCar" runat="server" SkinID="lista_desplegable" ></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator39" runat="server"
                    ErrorMessage="Seleccione el Sitio" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboSitiosCar" 
                    ValidationGroup="SitioCar">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label129" runat="server" SkinID="etiqueta_negra" 
                Text="Caracteristicas Fisicoquimicas:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:DropDownList ID="cboCarFisiQ" runat="server" SkinID="lista_desplegable" ></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator41" runat="server"
                    ErrorMessage="Seleccione Caracteristica Fisicoquimica" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboCarFisiQ" 
                    ValidationGroup="SitioCar">*</asp:CompareValidator>
            </td>
        </tr>
           <tr>
            <td width="25%">
                <asp:Label ID="Label130" runat="server" SkinID="etiqueta_negra" 
                Text="Método de determinación:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:DropDownList ID="cboMetDeterm" runat="server" SkinID="lista_desplegable" ></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator42" runat="server"
                    ErrorMessage="Seleccione Metodo de determinación" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboMetDeterm" 
                    ValidationGroup="SitioCar">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label125" runat="server" SkinID="etiqueta_negra" 
                Text="Concentracion Permitida (ug/m3) ó (mg/m3)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtConcePermitida" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator73" runat="server" 
                ErrorMessage="Ingrese Informacion Concentracion Permitida"
                    Display="Dynamic" ControlToValidate="txtConcePermitida"
                    ValidationGroup="SitioCar">*</asp:RequiredFieldValidator>                
            </td>
        </tr>
        <tr>            
            <td width="25%">
                <asp:Label ID="Label126" runat="server" SkinID="etiqueta_negra" 
                Text="Fecuencia Muestreo (dia)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtFrecMuest" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator74" runat="server" 
                ErrorMessage="Ingrese la Hora de Muestreo"
                    Display="Dynamic" ControlToValidate="txtFrecMuest"
                    ValidationGroup="SitioCar">*</asp:RequiredFieldValidator>
            </td>
        </tr>        
        <tr>            
            <td width="25%">
                <asp:Label ID="Label127" runat="server" SkinID="etiqueta_negra" 
                Text="Valor Sitio"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtValorCarSitio" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator75" runat="server" 
                ErrorMessage="Ingrese el Valor Correspondiente al Sitio"
                    Display="Dynamic" ControlToValidate="txtValorCarSitio"
                    ValidationGroup="SitioCar">*</asp:RequiredFieldValidator>
            </td>
        </tr>        
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarCarSitios" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="SitioCar"
                    OnClick="btnAgregarCarSitios_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarCarSitios" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarCarSitios_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary27" runat="server" 
                ValidationGroup="SitioCar" />
            </td>
        </tr>
       </asp:PlaceHolder> 
         <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvResultMonitoreoSitios" AutoGenerateColumns="True"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de detalles de fuentes" OnRowCreated="grvResultMonitoreoSitios_RowCreated" >
                    <Columns>                                                
                    </Columns>
                </asp:GridView>
            </td>
        </tr>        
        
        <%--Calidad del Aire - Fuentes de Ruido Existentes --%>
        <tr>
            <td  width="25%">
                3.1.10.4 Fuentes de Ruido Existentes
            </td>
			<td width="75%" align="right" colspan="3">
                <asp:Button ID="btnNuevoFuentRuidExist" runat="server" SkinID="boton_copia"
                    Text="Agregar Fuentes de Ruido Existentes" 
					onclick="btnNuevoFuentRuidExist_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhFuentRuidExist" Visible="False">
        <tr>
            <td width="25%">
                <asp:Label ID="Label121" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Fuente"></asp:Label>
            </td>
            <td width="75%" colspan = "3">
                <asp:DropDownList ID="cboTipoFuentRuidExist" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator37" runat="server"
                    ErrorMessage="Seleccione el Tipo de Fuente" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboTipoFuentRuidExist" 
                    ValidationGroup="FuentRuidExist">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label122" runat="server" SkinID="etiqueta_negra" 
                Text="Descripción"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtDescFuentRuidExist" runat="server" SkinID="texto_sintamano" 
                Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator72" runat="server" 
                ErrorMessage="Ingrese la información de la Descripción"
                    Display="Dynamic" ControlToValidate="txtDescFuentRuidExist"
                    ValidationGroup="FuentRuidExist">*</asp:RequiredFieldValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarFuentRuidExist" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="FuentRuidExist"
                    OnClick="btnAgregarFuentRuidExist_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarFuentRuidExist" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarFuentRuidExist_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary24" runat="server" 
                ValidationGroup="FuentRuidExist" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvFuentRuidExist" AutoGenerateColumns="False"
                width="99%" OnRowDeleting="grvFuentRuidExist_RowDeleting" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de Fuentes de Ruido Existentes">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField HeaderText="No de Fuente" DataField="EFR_NO_FILA" />
                        <asp:BoundField HeaderText="Tipo de Fuente" DataField="ETR_TIPO_FUENT_RUIDO" />
                        <asp:BoundField HeaderText="Descripción" DataField="EFR_DESCRIP_FUENT_RUIDO" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        
        <%--Calidad del Aire - Sitios de Monitoreo de Ruido Ambiental --%>
        <tr>
            <td width="25%">
                3.1.10.5 Sitios de Monitoreo de Ruido Ambiental
            </td>
			<td width="75%" align="right" colspan="3">
                <asp:Button ID="btnNuevoSitMonitRuido" runat="server" SkinID="boton_copia"
                    Text="Agregar Sitios de Monitoreo de Ruido Ambiental" 
					onclick="btnNuevoSitMonitRuido_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhSitMonitRuido" Visible="False">		
        <tr>
            <td width="25%">
                <asp:Label ID="Label128" runat="server" SkinID="etiqueta_negra" 
                Text="Coordenadas de Ubicación (m)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <uc2:ctrCoordenadasPto runat="server" ID="ctrCoorSitMonitRuido" />
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label131" runat="server" SkinID="etiqueta_negra" 
                Text="Subsector donde se ubica"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboSubSectSitMonitRuido" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator40" runat="server"
                    ErrorMessage="Seleccione el Subsector donde se ubica" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboSubSectSitMonitRuido" 
                    ValidationGroup="SitMonitRuido">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label132" runat="server" SkinID="etiqueta_negra" 
                Text="Fecha de Medición"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtFeMedSitMonitRuido" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator76" runat="server" 
                    ErrorMessage="Ingrese la Fecha de Medición"
                    Display="Dynamic" ControlToValidate="txtFeMedSitMonitRuido"
                    ValidationGroup="SitMonitRuido">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator43" runat="server"
                    ErrorMessage="La Fecha de Medición debe ser un tipo Fecha" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Date"
                    ControlToValidate="txtFeMedSitMonitRuido" 
                    ValidationGroup="SitMonitRuido">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label133" runat="server" SkinID="etiqueta_negra" 
                Text="Hora de Medición"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtHoMedSitMonitRuido" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                    TargetControlID="txtHoMedSitMonitRuido" Mask="99:99"  MaskType="Time" 
                    InputDirection="RightToLeft" ErrorTooltipEnabled="True" AcceptAMPM="true"
                    CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                    CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="">
                    </cc1:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator77" runat="server" 
                    ErrorMessage="Ingrese la Hora de Medición"
                    Display="Dynamic" ControlToValidate="txtHoMedSitMonitRuido"
                    ValidationGroup="SitMonitRuido">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label134" runat="server" SkinID="etiqueta_negra" 
                Text="Equipo de Medida"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtEqMedSitMonitRuido" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator78" runat="server" 
                    ErrorMessage="Ingrese el Equipo de Medida"
                    Display="Dynamic" ControlToValidate="txtEqMedSitMonitRuido"
                    ValidationGroup="SitMonitRuido">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label135" runat="server" SkinID="etiqueta_negra" 
                Text="Duración de la Medición en Minutos"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtDurMedSitMonitRuido" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator79" runat="server" 
                    ErrorMessage="Ingrese la Duración de la Medición"
                    Display="Dynamic" ControlToValidate="txtDurMedSitMonitRuido"
                    ValidationGroup="SitMonitRuido">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator44" runat="server"
                    ErrorMessage="La Duración de la Medición debe ser un entero" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Integer"
                    ControlToValidate="txtDurMedSitMonitRuido" 
                    ValidationGroup="SitMonitRuido">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%" >
                <asp:Label ID="Label136" runat="server" SkinID="etiqueta_negra" 
                Text="Nivel de Presión Sonora Equivalente A (Leqt A)"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <table width="100%">
                <tr>
                    <td width="25%">
                        <asp:Label ID="Label137" runat="server" SkinID="etiqueta_negra" 
                        Text="Diurno"></asp:Label>
                    </td>
                    <td width="75%">
                        <asp:TextBox ID="txtLeqtaDSitMonitRuido" runat="server" SkinID="texto_sintamano" 
                        MaxLength="200" Width="99%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator80" runat="server" 
                            ErrorMessage="Ingrese el Nivel de Presión Sonora Equivalente A Diurno"
                            Display="Dynamic" ControlToValidate="txtLeqtaDSitMonitRuido"
                            ValidationGroup="SitMonitRuido">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator45" runat="server"
                            ErrorMessage="El Nivel de Presión Sonora Equivalente A Diurno debe ser un entero" 
                            Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                            ControlToValidate="txtLeqtaDSitMonitRuido" 
                            ValidationGroup="SitMonitRuido">*</asp:CompareValidator>
                    </td>
                </tr>
                <tr>                    
                    <td width="25%">
                        <asp:Label ID="Label138" runat="server" SkinID="etiqueta_negra" 
                        Text="Nocturno"></asp:Label>
                    </td>
                    <td width="75%">
                        <asp:TextBox ID="txtLeqtaNSitMonitRuido" runat="server" SkinID="texto_sintamano" 
                        MaxLength="200" Width="99%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator81" runat="server" 
                            ErrorMessage="Ingrese el Nivel de Presión Sonora Equivalente A Nocturno"
                            Display="Dynamic" ControlToValidate="txtLeqtaNSitMonitRuido"
                            ValidationGroup="SitMonitRuido">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator46" runat="server"
                            ErrorMessage="El Nivel de Presión Sonora Equivalente A Nocturno debe ser un entero" 
                            Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                            ControlToValidate="txtLeqtaNSitMonitRuido" 
                            ValidationGroup="SitMonitRuido">*</asp:CompareValidator>
                    </td>
                </tr>
                </table>
            </td>
        </tr>
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label139" runat="server" SkinID="etiqueta_negra" 
                Text="Nivel de Presión Sonora Residual (Leqt Residual)"></asp:Label>
            </td>
            <td colspan="3" width="75%">
            <table width="100%" >
                <tr>            
                <td width="25%">
                    <asp:Label ID="Label140" runat="server" SkinID="etiqueta_negra" 
                    Text="Diurno"></asp:Label>
                </td>
                <td width="75%">
                    <asp:TextBox ID="txtLeqtDSitMonitRuido" runat="server" SkinID="texto_sintamano" 
                    MaxLength="200" Width="99%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator82" runat="server" 
                        ErrorMessage="Ingrese el Nivel de Presión Sonora Residual Diurno"
                        Display="Dynamic" ControlToValidate="txtLeqtDSitMonitRuido"
                        ValidationGroup="SitMonitRuido">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator47" runat="server"
                        ErrorMessage="El Nivel de Presión Sonora Residual Diurno debe ser un entero" 
                        Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                        ControlToValidate="txtLeqtDSitMonitRuido" 
                        ValidationGroup="SitMonitRuido">*</asp:CompareValidator>
                </td>
                </tr>
                <tr>
                    <td width="25%">
                        <asp:Label ID="Label141" runat="server" SkinID="etiqueta_negra" 
                        Text="Nocturno"></asp:Label>
                    </td>
                    <td width="75%">
                        <asp:TextBox ID="txtLeqtNSitMonitRuido" runat="server" SkinID="texto_sintamano" 
                        MaxLength="200" Width="99%"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator83" runat="server" 
                            ErrorMessage="Ingrese el Nivel de Presión Sonora Residual Nocturno"
                            Display="Dynamic" ControlToValidate="txtLeqtNSitMonitRuido"
                            ValidationGroup="SitMonitRuido">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator48" runat="server"
                            ErrorMessage="El Nivel de Presión Sonora Residual Nocturno debe ser un entero" 
                            Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                            ControlToValidate="txtLeqtNSitMonitRuido" 
                            ValidationGroup="SitMonitRuido">*</asp:CompareValidator>
                    </td>
                </tr>
            </table>
            </td>
        </tr>        
        <tr>
            <td width="25%">
                <asp:Label ID="Label142" runat="server" SkinID="etiqueta_negra" 
                Text="Nivel Percentil 90 (L90)"></asp:Label>
            </td>
            <td colspan="3" width="75%">
            <table width="100%">
            <tr>
                <td width="25%">
                    <asp:Label ID="Label143" runat="server" SkinID="etiqueta_negra" 
                    Text="Diurno"></asp:Label>
                </td>
                <td width="75%">
                    <asp:TextBox ID="txtL90DSitMonitRuido" runat="server" SkinID="texto_sintamano" 
                    MaxLength="200" Width="99%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator84" runat="server" 
                        ErrorMessage="Ingrese el Nivel Percentil 90 Diurno"
                        Display="Dynamic" ControlToValidate="txtL90DSitMonitRuido"
                        ValidationGroup="SitMonitRuido">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator49" runat="server"
                        ErrorMessage="El Nivel Percentil 90 Diurno debe ser un entero" 
                        Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                        ControlToValidate="txtL90DSitMonitRuido" 
                        ValidationGroup="SitMonitRuido">*</asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td width="25%">
                    <asp:Label ID="Label144" runat="server" SkinID="etiqueta_negra" 
                    Text="Nocturno"></asp:Label>
                </td>
                <td width="75%">
                    <asp:TextBox ID="txtL90NSitMonitRuido" runat="server" SkinID="texto_sintamano" 
                    MaxLength="200" Width="99%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator85" runat="server" 
                        ErrorMessage="Ingrese el Nivel Percentil 90 Nocturno"
                        Display="Dynamic" ControlToValidate="txtL90NSitMonitRuido"
                        ValidationGroup="SitMonitRuido">*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator50" runat="server"
                        ErrorMessage="El Nivel Percentil 90 Nocturno debe ser un entero" 
                        Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                        ControlToValidate="txtL90NSitMonitRuido" 
                        ValidationGroup="SitMonitRuido">*</asp:CompareValidator>
                </td>
            </tr>
            </table>
            </td>
        </tr>    
        <tr>
            <td width="25%">
                <asp:Label ID="Label145" runat="server" SkinID="etiqueta_negra" 
                Text="Niveles de Ruido Permitidos DB (A)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtRuidPerSitMonitRuido" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="Coloque el nivel de ruido permitido por ley según 
                el subsector en que se encuentre cada punto de monitoreo"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator86" runat="server" 
                    ErrorMessage="Ingrese el Nivel de Ruido Permitido"
                    Display="Dynamic" ControlToValidate="txtRuidPerSitMonitRuido"
                    ValidationGroup="SitMonitRuido">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator51" runat="server"
                    ErrorMessage="El Niveles de Ruido Permitidos debe ser un entero" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtRuidPerSitMonitRuido" 
                    ValidationGroup="SitMonitRuido">*</asp:CompareValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarSitMonitRuido" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="SitMonitRuido"
                    OnClick="btnAgregarSitMonitRuido_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarSitMonitRuido" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarSitMonitRuido_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary28" runat="server" 
                ValidationGroup="SitMonitRuido" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvMonitRuido" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información del monitoreo de ruido ambiental" OnRowDeleting="grvMonitRuido_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField HeaderText="No. Sitio de Monitoreo" DataField="ESR_ID" />
                        <asp:BoundField HeaderText="Coordenadas de Ubicacion (m) Norte" DataField="ESR_COOR_NORTE_UBI" />
                        <asp:BoundField HeaderText="Coordenadas de Ubicacion (m) Este" DataField="ESR_COOR_ESTE_UBI" />                        
                        <asp:BoundField HeaderText="Subsector Donde Se Ubica" DataField="ESS_SUBSECTOR_SITIO_MONIT" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label146" runat="server" SkinID="etiqueta_negra" 
                Text="Responsable de la medición (nombre)"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtRespMedSitMonitRuido" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator87" runat="server" 
                    ErrorMessage="Ingrese el Nombre del Responsable de la Medición"
                    Display="Dynamic" ControlToValidate="txtRespMedSitMonitRuido"
                    ValidationGroup="SitMonitRuido">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvSitMonitRuido" AutoGenerateColumns="True"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información del monitoreo de ruido ambiental - Caso especial!!">                    
                </asp:GridView>
            </td>
        </tr>
        
        <tr>
            <td colspan = "4" width="100%">3.2 Medio biótico</td>
        </tr>
        <!-- Medio biótico - Ecosistema Terrestre -->
        <tr>
            <td colspan = "4" width="100%">3.2.1 Ecosistema Terrestre</td>
        </tr>
        <%--Calidad del Aire - Sitios de Monitoreo de Ruido Ambiental --%>
        <tr>
            <td colspan="2" width="50%">
                3.2.1.1 Tipo de Ecosistema
            </td>
			<td width="50%" align="right" colspan="2">
                <asp:Button ID="btnNuevoTipoEcosistema" runat="server" SkinID="boton_copia"
                    Text="Agregar Tipo de Ecosistema" 
					onclick="btnNuevoTipoEcosistema_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhTipoEcosistema" Visible="False">
		
		<tr>
            <td width="25%">
                <asp:Label ID="Label147" runat="server" SkinID="etiqueta_negra" 
                Text="Código en el mapa:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtCodMapaTipoEcosistema" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Colocar el código con que se identifica la Unidad en el Mapa respectivo"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator88" runat="server" 
                ErrorMessage="Ingrese el código en el mapa"
                    Display="Dynamic" ControlToValidate="txtCodMapaTipoEcosistema"
                    ValidationGroup="TipoEcosistema">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label148" runat="server" SkinID="etiqueta_negra" 
                Text="Clasificación Utilizada"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboClasTipoEcosistema" runat="server" SkinID="lista_desplegable" 
                AutoPostBack="True" OnSelectedIndexChanged="cboClasTipoEcosistema_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator52" runat="server"
                    ErrorMessage="Seleccione el Clasificación Utilizada" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboClasTipoEcosistema" 
                    ValidationGroup="TipoEcosistema">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label149" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Ecosistema"></asp:Label>
            </td>
            <td width="75%" colspan="3"> 
                <asp:DropDownList ID="cboTipoEcosistema" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator53" runat="server"
                    ErrorMessage="Seleccione el Tipo de Ecosistema" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboTipoEcosistema" 
                    ValidationGroup="TipoEcosistema">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label150" runat="server" SkinID="etiqueta_negra" 
                Text="Descripción"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtDescTipoEcosistema" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator89" runat="server" 
                ErrorMessage="Ingrese la información de Descripción"
                    Display="Dynamic" ControlToValidate="txtDescTipoEcosistema"
                    ValidationGroup="TipoEcosistema">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label151" runat="server" SkinID="etiqueta_negra" 
                Text="Observaciones"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtObserTipoEcosistema" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator90" runat="server" 
                ErrorMessage="Ingrese la información de las Observaciones"
                    Display="Dynamic" ControlToValidate="txtObserTipoEcosistema"
                    ValidationGroup="TipoEcosistema">*</asp:RequiredFieldValidator>
            </td>
        </tr>        
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarTipoEcosistema" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="TipoEcosistema" OnClick="btnAgregarTipoEcosistema_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarTipoEcosistema" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarTipoEcosistema_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary29" runat="server" 
                ValidationGroup="TipoEcosistema" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvTipoEcosistema" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de los Tipos de Ecosistemas" OnRowDeleting="grvTipoEcosistema_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField DataField="ETT_COD_MAPA" HeaderText="Código en el mapa" />
                        <asp:BoundField DataField="ECE_CLASIFICACION_TERRESTRE" HeaderText="Clasificación Utilizada" />
                        <asp:BoundField DataField="ETE_TIPO_ECOSISTEMA_TERRESTRE" HeaderText="Tipo de Ecosistema" />
                        <asp:BoundField DataField="ETT_DESCRIPCION" HeaderText="Descripción" />
                        <asp:BoundField DataField="ETT_OBSERVACIONES" HeaderText="Observaciones" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
          <%--Calidad del Aire - Fuente de la Información --%>
        <tr>
            <td colspan="2" width="50%">
                3.2.1.2 Fuente de la Información
            </td>
			<td width="50%" align="right" colspan="2">
                <asp:Button ID="btnNuevoFuentInfo" runat="server" SkinID="boton_copia"
                    Text="Agregar Fuente de Información" 
					onclick="btnNuevoFuentInfo_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label152" runat="server" SkinID="etiqueta_negra" 
                Text="Escala de Trabajo"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboEscTraFuentInfo" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <%--<asp:CompareValidator ID="CompareValidator44" runat="server"
                    ErrorMessage="Seleccione la Fuente de Información" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboFuentInfo" 
                    ValidationGroup="FuentInfo">*</asp:CompareValidator>--%>
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhFuentInfo" Visible="False">
		
        <tr>
            <td width="25%">
                <asp:Label ID="Label153" runat="server" SkinID="etiqueta_negra" 
                Text="Fuente de Información"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboFuentInfo" runat="server" SkinID="lista_desplegable" 
                AutoPostBack="True" OnSelectedIndexChanged="cboFuentInfo_SelectedIndexChanged"></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator54" runat="server"
                    ErrorMessage="Seleccione la Fuente de Información" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboFuentInfo" 
                    ValidationGroup="FuentInfo">*</asp:CompareValidator>
            </td>
        </tr>
        <tr id="trOtroFuenteInfo" runat="server" visible="False">
               <td width="25%">
                    <asp:Label ID="Label154" runat="server" SkinID="etiqueta_negra" 
                    Text="Otro"></asp:Label>
                </td>
                <td width="75%" colspan="3">
                    <asp:TextBox ID="txtOtroFuentInfo" runat="server" SkinID="texto_sintamano" 
                    MaxLength="200" Width="99%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator91" runat="server" 
                    ErrorMessage="Ingrese el Año"
                        Display="Dynamic" ControlToValidate="txtOtroFuentInfo"
                        ValidationGroup="FuentInfo">*</asp:RequiredFieldValidator>
                </td>          
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label155" runat="server" SkinID="etiqueta_negra" 
                Text="Año"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtAnioFuentInfo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator92" runat="server" 
                ErrorMessage="Ingrese el Año"
                    Display="Dynamic" ControlToValidate="txtAnioFuentInfo"
                    ValidationGroup="FuentInfo">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        
        <tr id="trEscala" runat="server" visible="false">            
            <td width="25%">
                <asp:Label ID="Label157" runat="server" SkinID="etiqueta_negra" 
                Text="Escala"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtEscalaFuentInfo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator94" runat="server" 
                ErrorMessage="Ingrese la Escala"
                    Display="Dynamic" ControlToValidate="txtEscalaFuentInfo"
                    ValidationGroup="FuentInfo">*</asp:RequiredFieldValidator>
            </td>            
        </tr>
        
        <tr id="trTipoFuente" runat="server" visible="false">
            <td width="25%">
                <asp:Label ID="Label158" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtTipoFuentInfo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator95" runat="server" 
                ErrorMessage="Ingrese el Tipo"
                    Display="Dynamic" ControlToValidate="txtTipoFuentInfo"
                    ValidationGroup="FuentInfo">*</asp:RequiredFieldValidator>
            </td>
           
        </tr>
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label156" runat="server" SkinID="etiqueta_negra" 
                Text="Nivel de Resolución"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtNivResolFuentInfo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator93" runat="server" 
                ErrorMessage="Ingrese el Nivel de Resolución"
                    Display="Dynamic" ControlToValidate="txtNivResolFuentInfo"
                    ValidationGroup="FuentInfo">*</asp:RequiredFieldValidator>
            </td>
        </tr>
       
 
        
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarFuentInfo" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="FuentInfo" OnClick="btnAgregarFuentInfo_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarFuentInfo" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarFuentInfo_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary30" runat="server" 
                ValidationGroup="FuentInfo" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvFuentInfo" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de los Sistemas de Corrientes" OnRowDeleting="grvFuentInfo_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField DataField="EIE_ID" HeaderText="No" />
                        <asp:BoundField DataField="EFI_FUENT_INFO_ECOTERR" HeaderText="Fuente de Información" />
                        <asp:BoundField DataField="EIE_ANIO" HeaderText="Año" />
                        <asp:BoundField DataField="EIE_ESCALA" HeaderText="Escala" />
                        <asp:BoundField DataField="EIE_TIPO" HeaderText="Tipo" />
                        <asp:BoundField DataField="EIE_NIVEL_RESOLUCION" HeaderText="Nivel de Resolución" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>        
          
        <%--Calidad del Aire - Tipos de Unidad de Cobertura Vegetal Presentes --%>
        <tr>
            <td >
                3.2.1.3 Tipos de Unidad de Cobertura Vegetal Presentes
            </td>
            <td align="right">
                Aplica
            </td>			
            <td colspan="2">
                <asp:DropDownList ID="cboAplica1" runat="server"
                AutoPostBack="True" OnSelectedIndexChanged="cboAplica1_SelectedIndexChanged">
                    <asp:ListItem Value="-1">Seleccione ..</asp:ListItem>
                    <asp:ListItem Value="0">Si</asp:ListItem>
                    <asp:ListItem Value="1">No</asp:ListItem>
                </asp:DropDownList>
                <asp:CompareValidator ID="cmvAplica1" runat="server"
                    ErrorMessage="Seleccione la Fuente de Información" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboAplica1" 
                    ValidationGroup="Aplica1">*</asp:CompareValidator>
            </td>			
        </tr>
        <tr>
            <td colspan="4" align="right">
                <asp:Button ID="btnNuevoTipoUnidCoberVeg" runat="server" SkinID="boton_copia"
                    Text="Agregar Fuente de Información" ValidationGroup="Aplica1"
					onclick="btnNuevoTipoUnidCoberVeg_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label159" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad de Área"></asp:Label>
            </td>
            <td width="25%">
                <asp:DropDownList ID="cboUnidAreaTipoUnidCoberVeg" runat="server" SkinID="lista_desplegable" 
                AutoPostBack="True" OnSelectedIndexChanged="cboUnidAreaTipoUnidCoberVeg_SelectedIndexChanged"></asp:DropDownList>              
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhTipoUnidCoberVeg" Visible="False">
		
		<tr>
            <td width="25%">
                <asp:Label ID="Label160" runat="server" SkinID="etiqueta_negra" 
                Text="Código en el mapa:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtCodMapaTipoUnidCoberVeg" runat="server" SkinID="texto_sintamano" 
                    MaxLength="200" Width="99%" 
                    ToolTip="Colocar el código con que se identifica la Unidad en el Mapa respectivo"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator96" runat="server" 
                    ErrorMessage="Ingrese el código en el mapa"
                    Display="Dynamic" ControlToValidate="txtCodMapaTipoUnidCoberVeg"
                    ValidationGroup="TipoUnidCoberVeg">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label161" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Unidad"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtTipoUnidCoberVeg" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator97" runat="server" 
                ErrorMessage="Ingrese el Tipo de Unidad"
                    Display="Dynamic" ControlToValidate="txtTipoUnidCoberVeg"
                    ValidationGroup="TipoUnidCoberVeg">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label162" runat="server" SkinID="etiqueta_negra" 
                Text="Descripción"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtDescTipoUnidCoberVeg" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator98" runat="server" 
                ErrorMessage="Ingrese la Descripción"
                    Display="Dynamic" ControlToValidate="txtDescTipoUnidCoberVeg"
                    ValidationGroup="TipoUnidCoberVeg">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="lblAreaEstidio" runat="server" SkinID="etiqueta_negra" 
                Text="Área del Área de Estudio"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtAreaTipoUnidCoberVeg" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator99" runat="server" 
                ErrorMessage="Ingrese el Área del Área de Estudio"
                    Display="Dynamic" ControlToValidate="txtAreaTipoUnidCoberVeg"
                    ValidationGroup="TipoUnidCoberVeg">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator55" runat="server"
                    ErrorMessage="EL Área del Área Total a Intervenir debe ser numérico"
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtAreaTipoUnidCoberVeg" 
                    ValidationGroup="TipoUnidCoberVeg">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label164" runat="server" SkinID="etiqueta_negra" 
                Text="% del Área Total a Intervenir"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtPorcUnidCoberVeg" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator100" runat="server" 
                    ErrorMessage="Ingrese el % del Área Total a Intervenir"
                    Display="Dynamic" ControlToValidate="txtPorcUnidCoberVeg"
                    ValidationGroup="TipoUnidCoberVeg">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator56" runat="server"
                    ErrorMessage="EL valor del % del Área Total a Intervenir debe ser numérico"
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtPorcUnidCoberVeg" 
                    ValidationGroup="TipoUnidCoberVeg">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label165" runat="server" SkinID="etiqueta_negra" 
                Text="Función Ecológica para Fauna"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtFuncEcoUnidCoberVeg" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator101" runat="server" 
                ErrorMessage="Ingrese la Función Ecológica para Fauna"
                    Display="Dynamic" ControlToValidate="txtFuncEcoUnidCoberVeg"
                    ValidationGroup="TipoUnidCoberVeg">*</asp:RequiredFieldValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarTipoUnidCoberVeg" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="TipoUnidCoberVeg" OnClick="btnAgregarTipoUnidCoberVeg_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarTipoUnidCoberVeg" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarTipoUnidCoberVeg_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary31" runat="server" 
                ValidationGroup="TipoUnidCoberVeg" />
            </td>
        </tr>
        </asp:PlaceHolder>

        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvTipoUnidCoberVeg" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de los sitios de muestreo de la descripción fisionómica" OnRowDeleting="grvTipoUnidCoberVeg_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField DataField="ECV_COD_MAPA" HeaderText="Código en el mapa" />
                        <asp:BoundField DataField="ECV_TIPO_UNIDAD" HeaderText="Tipo de Unidad" />
                        <asp:BoundField DataField="ECV_DESCRIPCION" HeaderText="Descripción" />
                        <asp:BoundField DataField="ECV_AREA_AREA_EST" HeaderText="Área del Área de Estudio" />
                        <asp:BoundField DataField="ECV_PORC_AREA_INTERV" HeaderText="% del Área Total a Intervenir" />
                        <asp:BoundField DataField="ECV_FUNC_ECO_FAUNA" HeaderText="Función Ecológica para Fauna" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        
           <%--Calidad del Aire - 3.2.1.4		Descripción Fisionómica --%>
        <tr>
            <td colspan="4" width="100%">
                3.2.1.4 Descripción Fisionómica
            </td>
        </tr>
        <tr>
            <td colspan="2" width="50%">
                Sitio de Muestreo
            </td>
			<td width="50%" align="right" colspan="2">
                <asp:Button ID="btnNuevoSitioDescFisio" runat="server" SkinID="boton_copia"
                    Text="Agregar Sitio de Muestreo" 
					onclick="btnNuevoSitioDescFisio_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhSitioDescFisio" Visible="False">		
		
		<tr>
            <td width="25%">
                <asp:Label ID="Label163" runat="server" SkinID="etiqueta_negra" 
                Text="Código en el mapa:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtCodMapaSitioDescFisio" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Colocar el código con que se identifica la Unidad en el Mapa respectivo"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator102" runat="server" 
                ErrorMessage="Ingrese el código en el mapa"
                    Display="Dynamic" ControlToValidate="txtCodMapaSitioDescFisio"
                    ValidationGroup="SitioDescFisio">*</asp:RequiredFieldValidator>
            </td>                
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label166" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Unidad"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtTipoUnidadSitioDescFisio" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator103" runat="server" 
                ErrorMessage="Ingrese la Tipo de Unidad"
                    Display="Dynamic" ControlToValidate="txtTipoUnidadSitioDescFisio"
                    ValidationGroup="SitioDescFisio">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label167" runat="server" SkinID="etiqueta_negra" 
                Text="Descripción"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtDescSitioDescFisio" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator104" runat="server" 
                ErrorMessage="Ingrese la Descripción"
                    Display="Dynamic" ControlToValidate="txtDescSitioDescFisio"
                    ValidationGroup="SitioDescFisio">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label168" runat="server" SkinID="etiqueta_negra" 
                Text="Área Muestrada"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtAreaSitioDescFisio" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator105" runat="server" 
                ErrorMessage="Ingrese el Área Muestrada"
                    Display="Dynamic" ControlToValidate="txtAreaSitioDescFisio"
                    ValidationGroup="SitioDescFisio">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator57" runat="server"
                    ErrorMessage="EL valor del Área Muestrada debe ser numérico"
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtAreaSitioDescFisio" 
                    ValidationGroup="SitioDescFisio">*</asp:CompareValidator>
            </td>
        </tr><tr>
            <td width="25%">
                <asp:Label ID="Label169" runat="server" SkinID="etiqueta_negra" 
                Text="Localización - Coordenadas planas (Datum Magna-Sirgas)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <uc1:ctrCoordenadas runat="server" id="ctrCoorSitioDescFisio" />
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarSitioDescFisio" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="SitioDescFisio" OnClick="btnAgregarSitioDescFisio_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarSitioDescFisio" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarSitioDescFisio_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary32" runat="server" 
                ValidationGroup="SitioDescFisio" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvSitioDescFisio" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de los Sistemas de Corrientes" OnRowDeleting="grvSitioDescFisio_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField DataField="ESM_COD_MAPA" HeaderText="Codigo del Mapa" />
                        <asp:BoundField DataField="ESM_TIPO_UNIDAD" HeaderText="Tipo de Unidad" />
                        <asp:BoundField DataField="ESM_DESCRIPCION" HeaderText="Descripción" />
                        <asp:BoundField DataField="ESM_AREA_MUESTREADA" HeaderText="Área muestreada" />                        
                        <asp:TemplateField HeaderText="Localización - Coordenadas planas (Datum Magna-Sirgas)">                        
                            <ItemTemplate>
                                <uc1:ctrCoordenadas DataGridObject="true" NombreTabla="EIH_COOR_STIO_MUES_DESC_FISION" NombreCampo="ESM_ID" ValorCampo='<%# Eval("ESM_ID") %>' ValorCampo2='<%# Eval("ESM_ID") %>' ID="cregrvSuelos" runat="server"/>
                                <asp:Label ID="lblEsmId" runat="server" Visible="false" Text='<%# Eval("ESM_ID").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>    
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label170" runat="server" SkinID="etiqueta_negra" 
                Text="Estructura vertical dominante"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboEstVertDom" runat="server" SkinID="lista_desplegable" 
                AutoPostBack="True" OnSelectedIndexChanged="cboEstVertDom_SelectedIndexChanged"></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator58" runat="server"
                    ErrorMessage="Seleccione la Estructura Vertical Dominante" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboEstVertDom" 
                    ValidationGroup="Tab3">*</asp:CompareValidator>
            </td>
        </tr>
        
        
        <%--Calidad del Aire - Especies Dominantes por Estrato --%>
<tr>
            <td colspan="2" width="50%">
                Especies Dominantes por Estrato
            </td>
			<td width="50%" align="right" colspan="2">
                <asp:Button ID="btnNuevoEspDomEstrato" runat="server" SkinID="boton_copia"
                    Text="Agregar Especie Dominante por Estrato" 
					onclick="btnNuevoEspDomEstrato_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhEspDomEstrato" Visible="False">
	
        <tr>
            <td width="25%">
                <asp:Label ID="Label171" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Estrato"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboTipoEstrato" runat="server" SkinID="lista_desplegable" 
                ></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator59" runat="server"
                    ErrorMessage="Seleccione el Tipo de Estrato" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboTipoEstrato" 
                    ValidationGroup="EspDomEstrato">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label172" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre Común o Vulgar"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtNomComunEspDomEstrato" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="Coloque abajo el nombre científico correspondiente"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator106" runat="server" 
                ErrorMessage="Ingrese el Nombre Común o Vulgar"
                    Display="Dynamic" ControlToValidate="txtNomComunEspDomEstrato"
                    ValidationGroup="EspDomEstrato">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label173" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre Científico"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtNomCientEspDomEstrato" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator107" runat="server" 
                ErrorMessage="Ingrese el Nombre Científico"
                    Display="Dynamic" ControlToValidate="txtNomCientEspDomEstrato"
                    ValidationGroup="EspDomEstrato">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        	</asp:PlaceHolder>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarEspDomEstrato" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="EspDomEstrato" OnClick="btnAgregarEspDomEstrato_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarEspDomEstrato" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarEspDomEstrato_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary33" runat="server" 
                ValidationGroup="EspDomEstrato" />
            </td>
        </tr>
        
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvEspDomEstrato" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de Especies Dominantes por Estrato" OnRowDeleting="grvEspDomEstrato_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField DataField="ETE_TIPO_ESTRATO_ECOTERR" HeaderText="Tipo de Estrato" />
                        <asp:BoundField DataField="EED_NOMBRE_COMUN" HeaderText="Nombre Común o Vulgar" />
                        <asp:BoundField DataField="EED_NOMBRE_CIENTI" HeaderText="Nombre Científico" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label174" runat="server" SkinID="etiqueta_negra" 
                Text="Posición Fitosociológica Dominante"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboPosFitoDom" runat="server" SkinID="lista_desplegable" AutoPostBack="true" OnSelectedIndexChanged="cboPosFitoDom_SelectedIndexChanged" ></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator60" runat="server"
                    ErrorMessage="Seleccione la Posición Fitosociológica Dominante" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboPosFitoDom" 
                    ValidationGroup="Tab3">*</asp:CompareValidator>
            </td>
        </tr>
           <%--Calidad del Aire - Información Complementaria Flora --%>
	<tr>
            <td  width="25%">
                3.2.1.5 Información Complementaria Flora
            </td>
			<td width="75%" align="right" colspan="3">
                <asp:Button ID="btnNuevoInfoFlora" runat="server" SkinID="boton_copia"
                    Text="Agregar Especie Dominante por Estrato" 
					onclick="btnNuevoInfoFlora_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhInfoFlora" Visible="False">
		
        <tr>
            <td width="25%">
                <asp:Label ID="Label175" runat="server" SkinID="etiqueta_negra" 
                Text="Especie de Interés"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboEspIntInfoFlora" runat="server" SkinID="lista_desplegable"></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator61" runat="server"
                    ErrorMessage="Seleccione la Especie de Interés" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboEspIntInfoFlora" 
                    ValidationGroup="InfoFlora">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label176" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre Común o Vulgar"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtNomComInfoFlora" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator108" runat="server" 
                ErrorMessage="Ingrese el Nombre Común o Vulgar"
                    Display="Dynamic" ControlToValidate="txtNomComInfoFlora"
                    ValidationGroup="InfoFlora">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label177" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre Científico"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtNomCientInfoFlora" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator109" runat="server" 
                ErrorMessage="Ingrese el Nombre Científico"
                    Display="Dynamic" ControlToValidate="txtNomCientInfoFlora"
                    ValidationGroup="InfoFlora">*</asp:RequiredFieldValidator>
            </td>
        </tr><tr>
            <td width="25%">
                <asp:Label ID="Label178" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Fuente"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboTipFuentInfoFlora" runat="server" SkinID="lista_desplegable">
                    <asp:ListItem Text="Seleccione..." Value="-1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Información Primaria" Value="P"></asp:ListItem>
                    <asp:ListItem Text="Información Secundaria" Value="S"></asp:ListItem>
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator62" runat="server"
                    ErrorMessage="Seleccione el Tipo de Fuente" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboTipFuentInfoFlora" 
                    ValidationGroup="InfoFlora">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label179" runat="server" SkinID="etiqueta_negra" 
                Text="Fuente"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtFuenteInfoFlora" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator110" runat="server" 
                ErrorMessage="Ingrese la Fuente"
                    Display="Dynamic" ControlToValidate="txtFuenteInfoFlora"
                    ValidationGroup="InfoFlora">*</asp:RequiredFieldValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarInfoFlora" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="InfoFlora" OnClick="btnAgregarInfoFlora_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarInfoFlora" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarInfoFlora_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary34" runat="server" 
                ValidationGroup="InfoFlora" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvInfoFlora" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado Información Complementaria Flora" OnRowDeleting="grvInfoFlora_RowDeleting">
                    <Columns>  
                        <asp:CommandField ShowDeleteButton="True" />                  
                        <asp:BoundField DataField="EEF_TIPO_ESPECIE" HeaderText="Especie de Interés" />
                        <asp:BoundField DataField="EIF_NOMBRE_COMUN" HeaderText="Nombre Común o Vulgar" />
                        <asp:BoundField DataField="EIF_NOMBRE_CIENTF" HeaderText="Nombre Científico" />
                        <asp:BoundField DataField="EIF_TIPO_FUENTE" HeaderText="Tipo de Fuente" />
                        <asp:BoundField DataField="EIF_FUENTE" HeaderText="Fuente" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        
         <%--Información Complementaria Fauna --%>
		<tr>
            <td colspan="4" width="100%">
                3.2.1.6 Información Complementaria Fauna
            </td>
		</tr>
		<tr>
            <td colspan="2" width="50%">
                Anfibios
            </td>
			<td width="50%" align="right" colspan="2">
                <asp:Button ID="btnNuevoInfoFaunaAnf" runat="server" SkinID="boton_copia"
                    Text="Agregar Información Anfibios" 
					onclick="btnNuevoInfoFaunaAnf_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhInfoFaunaAnf" Visible="False">
		
        <tr>
            <td width="25%">
                <asp:Label ID="Label180" runat="server" SkinID="etiqueta_negra" 
                Text="Especie de Interés"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboEspIntInfoFaunaAnf" runat="server" SkinID="lista_desplegable"></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator63" runat="server"
                    ErrorMessage="Seleccione la Especie de Interés" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboEspIntInfoFaunaAnf" 
                    ValidationGroup="InfoFaunaAnf">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label181" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre Común o Vulgar"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtNomComInfoFaunaAnf" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator111" runat="server" 
                ErrorMessage="Ingrese el Nombre Común o Vulgar"
                    Display="Dynamic" ControlToValidate="txtNomComInfoFaunaAnf"
                    ValidationGroup="InfoFaunaAnf">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label182" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre Científico"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtNomCientInfoFaunaAnf" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator112" runat="server" 
                ErrorMessage="Ingrese el Nombre Científico"
                    Display="Dynamic" ControlToValidate="txtNomCientInfoFaunaAnf"
                    ValidationGroup="InfoFaunaAnf">*</asp:RequiredFieldValidator>
            </td>
        </tr><tr>
            <td width="25%">
                <asp:Label ID="Label183" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Fuente"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboTipFuentInfoFaunaAnf" runat="server" SkinID="lista_desplegable">
                    <asp:ListItem Text="Seleccione..." Value="-1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Información Primaria" Value="P"></asp:ListItem>
                    <asp:ListItem Text="Información Secundaria" Value="S"></asp:ListItem>
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator64" runat="server"
                    ErrorMessage="Seleccione el Tipo de Fuente" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboTipFuentInfoFaunaAnf" 
                    ValidationGroup="InfoFaunaAnf">*</asp:CompareValidator>
            </td>          
        </tr>
        <tr>
            <td width="25%" >
                <asp:Label ID="Label184" runat="server" SkinID="etiqueta_negra" 
                Text="Fuente"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtFuenteInfoFaunaAnf" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator113" runat="server" 
                ErrorMessage="Ingrese la Fuente"
                    Display="Dynamic" ControlToValidate="txtFuenteInfoFaunaAnf"
                    ValidationGroup="InfoFaunaAnf">*</asp:RequiredFieldValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarInfoFaunaAnf" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="InfoFaunaAnf" OnClick="btnAgregarInfoFaunaAnf_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarInfoFaunaAnf" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarInfoFaunaAnf_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary35" runat="server" 
                ValidationGroup="InfoFaunaAnf" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvInfoFaunaAnf" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado Información Complementaria Fauna - Anfibios" OnRowDeleting="grvInfoFaunaAnf_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="ETF_TIPO_ESPECIE" HeaderText="Especie de Interés" />
                        <asp:BoundField DataField="EFA_NOMBRE_COMUN" HeaderText="Nombre Común o Vulgar" />
                        <asp:BoundField DataField="EFA_NOMBRE_CIENTF" HeaderText="Nombre Científico" />
                        <asp:BoundField DataField="EFA_TIPO_FUENTE" HeaderText="Tipo de Fuente" />
                        <asp:BoundField DataField="EFA_FUENTE" HeaderText="Fuente" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2" width="50%">
                Reptiles
            </td>
			<td width="50%" align="right" colspan="2">
                <asp:Button ID="btnNuevoInfoFaunaRep" runat="server" SkinID="boton_copia"
                    Text="Agregar Información Reptiles" 
					onclick="btnNuevoInfoFaunaRep_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhInfoFaunaRep" Visible="False">
		
        <tr>
            <td width="25%">
                <asp:Label ID="Label185" runat="server" SkinID="etiqueta_negra" 
                Text="Especie de Interés"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboEspIntInfoFaunaRep" runat="server" SkinID="lista_desplegable"></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator65" runat="server"
                    ErrorMessage="Seleccione la Especie de Interés" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboEspIntInfoFaunaRep" 
                    ValidationGroup="InfoFaunaRep">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label186" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre Común o Vulgar"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtNomComInfoFaunaRep" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator114" runat="server" 
                ErrorMessage="Ingrese el Nombre Común o Vulgar"
                    Display="Dynamic" ControlToValidate="txtNomComInfoFaunaRep"
                    ValidationGroup="InfoFaunaRep">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label187" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre Científico"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtNomCientInfoFaunaRep" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator115" runat="server" 
                ErrorMessage="Ingrese el Nombre Científico"
                    Display="Dynamic" ControlToValidate="txtNomCientInfoFaunaRep"
                    ValidationGroup="InfoFaunaRep">*</asp:RequiredFieldValidator>
            </td>
        </tr><tr>
            <td width="25%">
                <asp:Label ID="Label188" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Fuente"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboTipFuentInfoFaunaRep" runat="server" SkinID="lista_desplegable">
                    <asp:ListItem Text="Seleccione..." Value="-1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Información Primaria" Value="P"></asp:ListItem>
                    <asp:ListItem Text="Información Secundaria" Value="S"></asp:ListItem>
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator66" runat="server"
                    ErrorMessage="Seleccione el Tipo de Fuente" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboTipFuentInfoFaunaRep" 
                    ValidationGroup="InfoFaunaRep">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label189" runat="server" SkinID="etiqueta_negra" 
                Text="Fuente"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtFuenteInfoFaunaRep" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator116" runat="server" 
                ErrorMessage="Ingrese la Fuente"
                    Display="Dynamic" ControlToValidate="txtFuenteInfoFaunaRep"
                    ValidationGroup="InfoFaunaRep">*</asp:RequiredFieldValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarInfoFaunaRep" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="InfoFaunaRep" OnClick="btnAgregarInfoFaunaRep_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarInfoFaunaRep" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarInfoFaunaRep_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary36" runat="server" 
                ValidationGroup="InfoFaunaRep" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvInfoFaunaRep" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado Información Complementaria Fauna - Reptiles" OnRowDeleting="grvInfoFaunaRep_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="ETF_TIPO_ESPECIE" HeaderText="Especie de Interés" />
                        <asp:BoundField DataField="EFR_NOMBRE_COMUN" HeaderText="Nombre Común o Vulgar" />
                        <asp:BoundField DataField="EFR_NOMBRE_CIENTF" HeaderText="Nombre Científico" />
                        <asp:BoundField DataField="EFR_TIPO_FUENTE" HeaderText="Tipo de Fuente" />
                        <asp:BoundField DataField="EFR_FUENTE" HeaderText="Fuente" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
         <tr>
            <td colspan="2" width="50%">
                Aves
            </td>
			<td width="50%" align="right" colspan="2">
                <asp:Button ID="btnNuevoInfoFaunaAve" runat="server" SkinID="boton_copia"
                    Text="Agregar Información Aves" 
					onclick="btnNuevoInfoFaunaAve_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhInfoFaunaAve" Visible="False">
		
        <tr>
            <td width="25%">
                <asp:Label ID="Label190" runat="server" SkinID="etiqueta_negra" 
                Text="Especie de Interés"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboEspIntInfoFaunaAve" runat="server" SkinID="lista_desplegable"></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator67" runat="server"
                    ErrorMessage="Seleccione la Especie de Interés" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboEspIntInfoFaunaAve" 
                    ValidationGroup="InfoFaunaAve">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label191" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre Común o Vulgar"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtNomComInfoFaunaAve" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator117" runat="server" 
                ErrorMessage="Ingrese el Nombre Común o Vulgar"
                    Display="Dynamic" ControlToValidate="txtNomComInfoFaunaAve"
                    ValidationGroup="InfoFaunaAve">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label192" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre Científico"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtNomCientInfoFaunaAve" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator118" runat="server" 
                ErrorMessage="Ingrese el Nombre Científico"
                    Display="Dynamic" ControlToValidate="txtNomCientInfoFaunaAve"
                    ValidationGroup="InfoFaunaAve">*</asp:RequiredFieldValidator>
            </td>
        </tr><tr>
            <td width="25%">
                <asp:Label ID="Label193" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Fuente"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboTipFuentInfoFaunaAve" runat="server" SkinID="lista_desplegable">
                    <asp:ListItem Text="Seleccione..." Value="-1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Información Primaria" Value="P"></asp:ListItem>
                    <asp:ListItem Text="Información Secundaria" Value="S"></asp:ListItem>
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator68" runat="server"
                    ErrorMessage="Seleccione el Tipo de Fuente" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboTipFuentInfoFaunaAve" 
                    ValidationGroup="InfoFaunaAve">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label194" runat="server" SkinID="etiqueta_negra" 
                Text="Fuente"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtFuenteInfoFaunaAve" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator119" runat="server" 
                ErrorMessage="Ingrese la Fuente"
                    Display="Dynamic" ControlToValidate="txtFuenteInfoFaunaAve"
                    ValidationGroup="InfoFaunaAve">*</asp:RequiredFieldValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarInfoFaunaAve" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="InfoFaunaAve" OnClick="btnAgregarInfoFaunaAve_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarInfoFaunaAve" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarInfoFaunaAve_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary37" runat="server" 
                ValidationGroup="InfoFaunaAve" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvInfoFaunaAve" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado Información Complementaria Fauna - Aves" OnRowDeleting="grvInfoFaunaAve_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="ETF_TIPO_ESPECIE" HeaderText="Especie de Interés" />
                        <asp:BoundField DataField="EFA_NOMBRE_COMUN" HeaderText="Nombre Común o Vulgar" />
                        <asp:BoundField DataField="EFA_NOMBRE_CIENTF" HeaderText="Nombre Científico" />
                        <asp:BoundField DataField="EFA_TIPO_FUENTE" HeaderText="Tipo de Fuente" />
                        <asp:BoundField DataField="EFA_FUENTE" HeaderText="Fuente" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        
        <tr>
            <td colspan="2" width="50%">
                Mamíferos
            </td>
			<td width="50%" align="right" colspan="2">
                <asp:Button ID="btnNuevoInfoFaunaMam" runat="server" SkinID="boton_copia"
                    Text="Agregar Información Mamíferos" 
					onclick="btnNuevoInfoFaunaMam_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhInfoFaunaMam" Visible="False">
		
        <tr>
            <td width="25%">
                <asp:Label ID="Label195" runat="server" SkinID="etiqueta_negra" 
                Text="Especie de Interés"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboEspIntInfoFaunaMam" runat="server" SkinID="lista_desplegable"></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator69" runat="server"
                    ErrorMessage="Seleccione la Especie de Interés" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboEspIntInfoFaunaMam" 
                    ValidationGroup="InfoFaunaMam">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label196" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre Común o Vulgar"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtNomComInfoFaunaMam" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator120" runat="server" 
                ErrorMessage="Ingrese el Nombre Común o Vulgar"
                    Display="Dynamic" ControlToValidate="txtNomComInfoFaunaMam"
                    ValidationGroup="InfoFaunaMam">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label197" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre Científico"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtNomCientInfoFaunaMam" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator121" runat="server" 
                ErrorMessage="Ingrese el Nombre Científico"
                    Display="Dynamic" ControlToValidate="txtNomCientInfoFaunaMam"
                    ValidationGroup="InfoFaunaMam">*</asp:RequiredFieldValidator>
            </td>
        </tr><tr>
            <td width="25%">
                <asp:Label ID="Label198" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Fuente"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboTipFuentInfoFaunaMam" runat="server" SkinID="lista_desplegable">
                    <asp:ListItem Text="Seleccione..." Value="-1" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Información Primaria" Value="P"></asp:ListItem>
                    <asp:ListItem Text="Información Secundaria" Value="S"></asp:ListItem>
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator70" runat="server"
                    ErrorMessage="Seleccione el Tipo de Fuente" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboTipFuentInfoFaunaMam" 
                    ValidationGroup="InfoFaunaMam">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>        
            <td width="25%">
                <asp:Label ID="Label199" runat="server" SkinID="etiqueta_negra" 
                Text="Fuente"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtFuenteInfoFaunaMam" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator122" runat="server" 
                ErrorMessage="Ingrese la Fuente"
                    Display="Dynamic" ControlToValidate="txtFuenteInfoFaunaMam"
                    ValidationGroup="InfoFaunaMam">*</asp:RequiredFieldValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarInfoFaunaMam" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="InfoFaunaMam" OnClick="btnAgregarInfoFaunaMam_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarInfoFaunaMam" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarInfoFaunaMam_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary38" runat="server" 
                ValidationGroup="InfoFaunaMam" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvInfoFaunaMam" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado Información Complementaria Fauna - Mamíferos" OnRowDeleting="grvInfoFaunaMam_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="ETF_TIPO_ESPECIE" HeaderText="Especie de Interés" />
                        <asp:BoundField DataField="EFM_NOMBRE_COMUN" HeaderText="Nombre Común o Vulgar" />
                        <asp:BoundField DataField="EFM_NOMBRE_CIENTF" HeaderText="Nombre Científico" />
                        <asp:BoundField DataField="EFM_TIPO_FUENTE" HeaderText="Tipo de Fuente" />
                        <asp:BoundField DataField="EFM_FUENTE" HeaderText="Fuente" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
         <%--Calidad del Aire - Ecosistema Acuático Continental --%>
        <tr>
            <td  width="25%">
                3.2.2 Ecosistema Acuático Continental
            </td>
			<td width="75%" align="right" colspan="3">
                <asp:Button ID="btnNuevoEcoAcuaCont" runat="server" SkinID="boton_copia"
                    Text="Agregar Ecosistema Acuático Continental" 
					onclick="btnNuevoEcoAcuaCont_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhEcoAcuaCont" Visible="False">
		
        <tr>
            <td width="25%">
                <asp:Label ID="Label200" runat="server" SkinID="etiqueta_negra" 
                Text="Sistema Muestreado"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtSistMuestEcoAcuaCont" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator123" runat="server" 
                ErrorMessage="Ingrese la Sistema Muestreado"
                    Display="Dynamic" ControlToValidate="txtSistMuestEcoAcuaCont"
                    ValidationGroup="EcoAcuaCont">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label201" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Sustrato Predominante"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtTtpSustPredEcoAcuaCont" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator124" runat="server" 
                ErrorMessage="Ingrese el Tipo de Sustrato Predominante"
                    Display="Dynamic" ControlToValidate="txtTtpSustPredEcoAcuaCont"
                    ValidationGroup="EcoAcuaCont">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label202" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre del Drenaje o Sistema Muestreado"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtNomDrenEcoAcuaCont" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator125" runat="server" 
                ErrorMessage="Ingrese el Nombre del Drenaje o Sistema Muestreado"
                    Display="Dynamic" ControlToValidate="txtNomDrenEcoAcuaCont"
                    ValidationGroup="EcoAcuaCont">*</asp:RequiredFieldValidator>
            </td>
        </tr>    
        <tr>
            <td width="25%">
                <asp:Label ID="Label204" runat="server" SkinID="etiqueta_negra" 
                Text="Categorías de Protección"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtCatProtecEcoAcuaCont" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="Categoria de Proyección según el plan de Ordenamiento"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator127" runat="server" 
                ErrorMessage="Ingrese la información de la Categorías de Protección"
                    Display="Dynamic" ControlToValidate="txtCatProtecEcoAcuaCont"
                    ValidationGroup="EcoAcuaCont">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label205" runat="server" SkinID="etiqueta_negra" 
                Text="Coordenadas de Ubicación (m)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <uc2:ctrCoordenadasPto runat="server" ID="ctrCoorEcoAcuaCont" />
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarEcoAcuaCont" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="EcoAcuaCont" OnClick="btnAgregarEcoAcuaCont_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarEcoAcuaCont" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarEcoAcuaCont_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary39" runat="server" 
                ValidationGroup="EcoAcuaCont" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvEcoAcuaCont" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de los Sistemas de Corrientes" OnRowDeleting="grvEcoAcuaCont_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EEA_ID" HeaderText="No. Sitio Muestreado" />
                        <asp:BoundField DataField="EEA_SISTEMA_MUESTREADO" HeaderText="Sistema Mustreado" />
                        <asp:BoundField DataField="EEA_TIPO_SUSTRAT_PREDOM" HeaderText="Tipo de Sustrato Prediminante" />
                        <asp:BoundField DataField="EEA_NOMB_DREN_SIST_MUEST" HeaderText="Nombre del Drenaje o Sistema Muestreado" />
                        <asp:BoundField DataField="EEA_CATEG_PROTECCION" HeaderText="Categoria de Protección" />
                        <asp:BoundField DataField="EEA_COOR_NORTE_UBIC" HeaderText="Coordenadas Norte" />
                        <asp:BoundField DataField="EEA_COOR_ESTE_UBIC" HeaderText="Coordenadas Este" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <%-- 3.2.2.1 Sistemas Lóticos --%>
        <tr>
            <td colspan="2" width="50%">
                3.2.2.1 Sistemas Lóticos
            </td>
			<td width="50%" align="right" colspan="2">
                <asp:Button ID="btnNuevoSistLoticos" runat="server" SkinID="boton_copia"
                    Text="Agregar Información Sistemas Lóticos" 
					onclick="btnNuevoSistLoticos_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhSistLoticos" Visible="False">		
        <tr>
            <td width="25%">
                <asp:Label ID="Label203" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Biota"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboTipoBiotaSistLoticos" runat="server" SkinID="lista_desplegable"></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator71" runat="server"
                    ErrorMessage="Seleccione el Tipo de Biota" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboTipoBiotaSistLoticos" 
                    ValidationGroup="SistLoticos">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label206" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre Común o Vulgar"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtNomComSistLoticos" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator126" runat="server" 
                ErrorMessage="Ingrese el Nombre Común o Vulgar"
                    Display="Dynamic" ControlToValidate="txtNomComSistLoticos"
                    ValidationGroup="SistLoticos">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label207" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre Científico"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtNomCientSistLoticos" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator128" runat="server" 
                ErrorMessage="Ingrese el Nombre Científico"
                    Display="Dynamic" ControlToValidate="txtNomCientSistLoticos"
                    ValidationGroup="SistLoticos">*</asp:RequiredFieldValidator>
            </td>
        </tr><tr>
            <td width="25%">
                <asp:Label ID="Label208" runat="server" SkinID="etiqueta_negra" 
                Text="Grupo"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtGrupoSistLoticos" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator129" runat="server" 
                ErrorMessage="Ingrese el Grupo"
                    Display="Dynamic" ControlToValidate="txtGrupoSistLoticos"
                    ValidationGroup="SistLoticos">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label209" runat="server" SkinID="etiqueta_negra" 
                Text="Porcentaje"></asp:Label>
            </td>
            
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtPorcSistLoticos" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator130" runat="server" 
                ErrorMessage="Ingrese el Porcentaje"
                    Display="Dynamic" ControlToValidate="txtPorcSistLoticos"
                    ValidationGroup="SistLoticos">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label210" runat="server" SkinID="etiqueta_negra" 
                Text="Bioindicación"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtBioSistLoticos" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator131" runat="server" 
                ErrorMessage="Ingrese la Bioindicación"
                    Display="Dynamic" ControlToValidate="txtBioSistLoticos"
                    ValidationGroup="SistLoticos">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label211" runat="server" SkinID="etiqueta_negra" 
                Text="Fuente"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtFuenteSistLoticos" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator132" runat="server" 
                ErrorMessage="Ingrese la Fuente"
                    Display="Dynamic" ControlToValidate="txtFuenteSistLoticos"
                    ValidationGroup="txtFuenteSistLoticos">*</asp:RequiredFieldValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarSistLoticos" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="SistLoticos" OnClick="btnAgregarSistLoticos_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarSistLoticos" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarSistLoticos_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary40" runat="server" 
                ValidationGroup="SistLoticos" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvSistLoticos" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado Información de Sistemas Lóticos" OnRowDeleting="grvSistLoticos_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="ETB_TIPO_BIOTA" HeaderText="Tipo de Biota" />
                        <asp:BoundField DataField="ELA_NOMBRE_COMUN" HeaderText="Nombre Común o Vulgar" />
                        <asp:BoundField DataField="ELA_NOMBRE_CIENTF" HeaderText="Nombre Científico" />
                        <asp:BoundField DataField="ELA_GRUPO" HeaderText="Grupo" />
                        <asp:BoundField DataField="ELA_PORCENTAJE" HeaderText="Porcentaje" />
                        <asp:BoundField DataField="ELA_BIOINDICACION" HeaderText="Bioindicación" />
                        <asp:BoundField DataField="ELA_FUENTE" HeaderText="Fuente" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
          <%-- 3.2.2.1 Sistemas Lénticos --%>
        <tr>
            <td colspan="2" width="50%">
                3.2.2.2 Sistemas Lénticos
            </td>
			<td width="50%" align="right" colspan="2">
                <asp:Button ID="btnNuevoSistLenticos" runat="server" SkinID="boton_copia"
                    Text="Agregar Información Sistemas Lénticos" 
					onclick="btnNuevoSistLenticos_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhSistLenticos" Visible="False">
		
        <tr>
            <td width="25%">
                <asp:Label ID="Label212" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Biota"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboTipoBiotaSistLenticos" runat="server" SkinID="lista_desplegable"></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator72" runat="server"
                    ErrorMessage="Seleccione el Tipo de Biota" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboTipoBiotaSistLenticos" 
                    ValidationGroup="SistLenticos">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label213" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre Común o Vulgar"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtNomComSistLenticos" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator133" runat="server" 
                ErrorMessage="Ingrese el Nombre Común o Vulgar"
                    Display="Dynamic" ControlToValidate="txtNomComSistLenticos"
                    ValidationGroup="SistLenticos">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label214" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre Científico"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtNomCientSistLenticos" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator134" runat="server" 
                ErrorMessage="Ingrese el Nombre Científico"
                    Display="Dynamic" ControlToValidate="txtNomCientSistLenticos"
                    ValidationGroup="SistLenticos">*</asp:RequiredFieldValidator>
            </td>
        </tr><tr>
            <td width="25%">
                <asp:Label ID="Label215" runat="server" SkinID="etiqueta_negra" 
                Text="Grupo"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtGrupoSistLenticos" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator135" runat="server" 
                ErrorMessage="Ingrese el Grupo"
                    Display="Dynamic" ControlToValidate="txtGrupoSistLenticos"
                    ValidationGroup="SistLenticos">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label216" runat="server" SkinID="etiqueta_negra" 
                Text="Porcentaje"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtPorcSistLenticos" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator136" runat="server" 
                ErrorMessage="Ingrese el Porcentaje"
                    Display="Dynamic" ControlToValidate="txtPorcSistLenticos"
                    ValidationGroup="SistLenticos">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label217" runat="server" SkinID="etiqueta_negra" 
                Text="Bioindicación"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtBioSistLenticos" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator137" runat="server" 
                ErrorMessage="Ingrese la Bioindicación"
                    Display="Dynamic" ControlToValidate="txtBioSistLenticos"
                    ValidationGroup="SistLenticos">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label218" runat="server" SkinID="etiqueta_negra" 
                Text="Fuente"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtFuenteSistLenticos" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator138" runat="server" 
                ErrorMessage="Ingrese la Fuente"
                    Display="Dynamic" ControlToValidate="txtFuenteSistLenticos"
                    ValidationGroup="txtFuenteSistLenticos">*</asp:RequiredFieldValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarSistLenticos" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="SistLenticos" OnClick="btnAgregarSistLenticos_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarSistLenticos" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarSistLenticos_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary41" runat="server" 
                ValidationGroup="SistLenticos" />
            </td>
        </tr>     
        </asp:PlaceHolder>   
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvSistLenticos" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado Información de Sistemas Lénticos" OnRowDeleting="grvSistLenticos_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="ETB_TIPO_BIOTA" HeaderText="Tipo de Biota" />
                        <asp:BoundField DataField="ESL_NOMBRE_COMUN" HeaderText="Nombre Común o Vulgar" />
                        <asp:BoundField DataField="ESL_NOMBRE_CIENTF" HeaderText="Nombre Científico" />
                        <asp:BoundField DataField="ESL_GRUPO" HeaderText="Grupo" />
                        <asp:BoundField DataField="ESL_PORCENTAJE" HeaderText="Porcentaje" />
                        <asp:BoundField DataField="ESL_BIOINDICACION" HeaderText="Bioindicación" />
                        <asp:BoundField DataField="ESL_FUENTE" HeaderText="Fuente" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
          <%-- 3.2.3 Ecosistema Marino --%>
        <tr>
            <td colspan="4" width="100%">
                3.2.3 Ecosistema Marino
            </td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                3.2.3.1 Fauna
            </td>
        </tr>
        <%--  Corales y Fauna Asociada --%>
        <tr>
            <td width="25%">
                Corales y Fauna Asociada
            </td>
			<td width="75%" align="right" colspan="3">
                <asp:Button ID="btnNuevoInfCoral" runat="server" SkinID="boton_copia"
                    Text="Agregar Información de Corales y Fauna Asociada" 
					onclick="btnNuevoInfCoral_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhInfCoral" Visible="False">
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label219" runat="server" SkinID="etiqueta_negra" 
                Text="Especies de Interés"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboEspIntInfCoral" runat="server" SkinID="lista_desplegable" ></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator73" runat="server"
                    ErrorMessage="Seleccione la Especies de Interés" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboEspIntInfCoral" 
                    ValidationGroup="InfCoral">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label220" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre Común o Vulgar"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtNomComInfCoral" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator139" runat="server" 
                ErrorMessage="Ingrese el Nombre Común o Vulgar"
                    Display="Dynamic" ControlToValidate="txtNomComInfCoral"
                    ValidationGroup="InfCoral">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label221" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre Científico"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtNomCientInfCoral" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator140" runat="server" 
                ErrorMessage="Ingrese el Nombre Científico"
                    Display="Dynamic" ControlToValidate="txtNomCientInfCoral"
                    ValidationGroup="InfCoral">*</asp:RequiredFieldValidator>
            </td>
        </tr><tr>
            <td width="25%">
                <asp:Label ID="Label222" runat="server" SkinID="etiqueta_negra" 
                Text="Ecosistema"></asp:Label>
            </td>
           <td colspan="3" width="75%">
                <asp:DropDownList ID="cboEcoMarino" runat="server" SkinID="lista_desplegable" ></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator74" runat="server"
                    ErrorMessage="Seleccione la Ecosistema" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboEcoMarino" 
                    ValidationGroup="InfCoral">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label223" runat="server" SkinID="etiqueta_negra" 
                Text="Importancia Ecológica"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtImpEcoInfCoral" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator142" runat="server" 
                ErrorMessage="Ingrese la Importancia Ecológica"
                    Display="Dynamic" ControlToValidate="txtImpEcoInfCoral"
                    ValidationGroup="InfCoral">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label224" runat="server" SkinID="etiqueta_negra" 
                Text="Importacia Económica"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtImpEconInfCoral" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator143" runat="server" 
                ErrorMessage="Ingrese la Importacia Económica"
                    Display="Dynamic" ControlToValidate="txtImpEconInfCoral"
                    ValidationGroup="InfCoral">*</asp:RequiredFieldValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarInfCoral" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="InfCoral" OnClick="btnAgregarInfCoral_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarInfCoral" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarInfCoral_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary42" runat="server" 
                ValidationGroup="InfCoral" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvInfCoral" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado Información de Corales y Fauna Asociada" OnRowDeleting="grvInfCoral_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="ETM_TIPO_ESPECIE_MARINA" HeaderText="Especies de Interés" />
                        <asp:BoundField DataField="ECE_NOMBRE_COMUN" HeaderText="Nombre Común o Vulgar" />
                        <asp:BoundField DataField="ECE_NOMBRE_CIENTIF" HeaderText="Nombre Científico" />
                        <asp:BoundField DataField="EEM_TIPO_ECO_MARINO" HeaderText="Ecosistema" />
                        <asp:BoundField DataField="ECE_IMP_ECOLOGICA" HeaderText="Importancia Ecológica" />
                        <asp:BoundField DataField="ECE_IMP_ECONOMICA" HeaderText="Importancia Económica" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
           <tr>
            <td colspan="2" width="50%">
                Bentos
            </td>
			<td width="50%" align="right" colspan="2">
                <asp:Button ID="btnNuevoInfBentos" runat="server" SkinID="boton_copia"
                    Text="Agregar Información de Bentos" 
					onclick="btnNuevoInfBentos_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhInfBentos" Visible="False">
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label225" runat="server" SkinID="etiqueta_negra" 
                Text="Especies de Interés"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboEspIntInfBentos" runat="server" SkinID="lista_desplegable"></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator75" runat="server"
                    ErrorMessage="Seleccione la Especies de Interés" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboEspIntInfBentos" 
                    ValidationGroup="InfBentos">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label226" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre Común o Vulgar"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtNomComInfBentos" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator141" runat="server" 
                ErrorMessage="Ingrese el Nombre Común o Vulgar"
                    Display="Dynamic" ControlToValidate="txtNomComInfBentos"
                    ValidationGroup="InfBentos">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label227" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre Científico"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtNomCientInfBentos" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator144" runat="server" 
                ErrorMessage="Ingrese el Nombre Científico"
                    Display="Dynamic" ControlToValidate="txtNomCientInfBentos"
                    ValidationGroup="InfBentos">*</asp:RequiredFieldValidator>
            </td>
        </tr><tr>
            <td width="25%">
                <asp:Label ID="Label228" runat="server" SkinID="etiqueta_negra" 
                Text="Ecosistema"></asp:Label>
            </td>
             <td colspan="3" width="75%">
                <asp:DropDownList ID="cboEcosistemaBentos" runat="server" SkinID="lista_desplegable"></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator76" runat="server"
                    ErrorMessage="Seleccione Ecosistema" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboEcosistemaBentos" 
                    ValidationGroup="InfBentos">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label229" runat="server" SkinID="etiqueta_negra" 
                Text="Importancia Ecológica"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtImpEcoInfBentos" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator146" runat="server" 
                ErrorMessage="Ingrese la Importancia Ecológica"
                    Display="Dynamic" ControlToValidate="txtImpEcoInfBentos"
                    ValidationGroup="InfBentos">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label230" runat="server" SkinID="etiqueta_negra" 
                Text="Importacia Económica"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtImpEconInfBentos" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator147" runat="server" 
                ErrorMessage="Ingrese la Importacia Económica"
                    Display="Dynamic" ControlToValidate="txtImpEconInfBentos"
                    ValidationGroup="InfBentos">*</asp:RequiredFieldValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarInfBentos" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="InfBentos" OnClick="btnAgregarInfBentos_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarInfBentos" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarInfBentos_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary43" runat="server" 
                ValidationGroup="InfBentos" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvInfBentos" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado Información de Bentos" OnRowDeleting="grvInfBentos_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="ETM_TIPO_ESPECIE_MARINA" HeaderText="Especies de Interés" />
                        <asp:BoundField DataField="EBE_NOMBRE_COMUN" HeaderText="Nombre Común o Vulgar" />
                        <asp:BoundField DataField="EBE_NOMBRE_CIENTIF" HeaderText="Nombre Científico" />
                        <asp:BoundField DataField="EEM_TIPO_ECO_MARINO" HeaderText="Ecosistema" />
                        <asp:BoundField DataField="EBE_IMP_ECOLOGICA" HeaderText="Importancia Ecológica" />
                        <asp:BoundField DataField="EBE_IMP_ECONOMICA" HeaderText="Importancia Económica" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
         <%--  Zooplancton --%>
        <tr>
            <td colspan="2" width="50%">
                Zooplancton
            </td>
			<td width="50%" align="right" colspan="2">
                <asp:Button ID="btnNuevoInfZooplancton" runat="server" SkinID="boton_copia"
                    Text="Agregar Información de Zooplancton" 
					onclick="btnNuevoInfZooplancton_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhInfZooplancton" Visible="False">        
        <tr>
            <td width="25%">
                <asp:Label ID="Label231" runat="server" SkinID="etiqueta_negra" 
                Text="Especies de Interés"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboEspIntInfZooplancton" runat="server" SkinID="lista_desplegable"></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator77" runat="server"
                    ErrorMessage="Seleccione la Especies de Interés" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboEspIntInfZooplancton" 
                    ValidationGroup="InfZooplancton">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label232" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre Común o Vulgar"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtNomComInfZooplancton" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator145" runat="server" 
                ErrorMessage="Ingrese el Nombre Común o Vulgar"
                    Display="Dynamic" ControlToValidate="txtNomComInfZooplancton"
                    ValidationGroup="InfZooplancton">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label233" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre Científico"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtNomCientInfZooplancton" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator148" runat="server" 
                ErrorMessage="Ingrese el Nombre Científico"
                    Display="Dynamic" ControlToValidate="txtNomCientInfZooplancton"
                    ValidationGroup="InfZooplancton">*</asp:RequiredFieldValidator>
            </td>
        </tr><tr>
            <td width="25%">
                <asp:Label ID="Label234" runat="server" SkinID="etiqueta_negra" 
                Text="Ecosistema"></asp:Label>
            </td>
              <td colspan="3" width="75%">
                <asp:DropDownList ID="cboEcoZOO" runat="server" SkinID="lista_desplegable"></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator78" runat="server"
                    ErrorMessage="Seleccione Ecosistema" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboEcoZOO" 
                    ValidationGroup="InfZooplancton">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label235" runat="server" SkinID="etiqueta_negra" 
                Text="Importancia Ecológica"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtImpEcoInfZooplancton" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator150" runat="server" 
                ErrorMessage="Ingrese la Importancia Ecológica"
                    Display="Dynamic" ControlToValidate="txtImpEcoInfZooplancton"
                    ValidationGroup="InfZooplancton">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label236" runat="server" SkinID="etiqueta_negra" 
                Text="Importacia Económica"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtImpEconInfZooplancton" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator151" runat="server" 
                ErrorMessage="Ingrese la Importacia Económica"
                    Display="Dynamic" ControlToValidate="txtImpEconInfZooplancton"
                    ValidationGroup="InfZooplancton">*</asp:RequiredFieldValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarInfZooplancton" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="InfZooplancton" OnClick="btnAgregarInfZooplancton_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarInfZooplancton" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarInfZooplancton_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary44" runat="server" 
                ValidationGroup="InfZooplancton" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvInfZooplancton" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado Información de Zooplancton" OnRowDeleting="grvInfZooplancton_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="ETM_TIPO_ESPECIE_MARINA" HeaderText="Especies de Interés" />
                        <asp:BoundField DataField="EZE_NOMBRE_COMUN" HeaderText="Nombre Común o Vulgar" />
                        <asp:BoundField DataField="EZE_NOMBRE_CIENTIF" HeaderText="Nombre Científico" />
                        <asp:BoundField DataField="EEM_TIPO_ECO_MARINO" HeaderText="Ecosistema" />
                        <asp:BoundField DataField="EZE_IMP_ECOLOGICA" HeaderText="Importancia Ecológica" />
                        <asp:BoundField DataField="EZE_IMP_ECONOMICA" HeaderText="Importancia Económica" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <%--  Ictiofauna --%>
        <tr>
            <td colspan="2" width="50%">
                Ictiofauna
            </td>
			<td width="50%" align="right" colspan="2">
                <asp:Button ID="btnNuevoInfIctiofauna" runat="server" SkinID="boton_copia"
                    Text="Agregar Información de Ictiofauna" 
					onclick="btnNuevoInfIctiofauna_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhInfIctiofauna" Visible="False">
        
        <tr>
            <td width="25%" style="height: 24px">
                <asp:Label ID="Label237" runat="server" SkinID="etiqueta_negra" 
                Text="Especies de Interés"></asp:Label>
            </td>
            <td colspan="3" width="75%" style="height: 24px">
                <asp:DropDownList ID="cboEspIntInfIctiofauna" runat="server" SkinID="lista_desplegable"></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator79" runat="server"
                    ErrorMessage="Seleccione la Especies de Interés" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboEspIntInfIctiofauna" 
                    ValidationGroup="InfIctiofauna">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label238" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre Común o Vulgar"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtNomComInfIctiofauna" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator149" runat="server" 
                ErrorMessage="Ingrese el Nombre Común o Vulgar"
                    Display="Dynamic" ControlToValidate="txtNomComInfIctiofauna"
                    ValidationGroup="InfIctiofauna">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label239" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre Científico"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtNomCientInfIctiofauna" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator152" runat="server" 
                ErrorMessage="Ingrese el Nombre Científico"
                    Display="Dynamic" ControlToValidate="txtNomCientInfIctiofauna"
                    ValidationGroup="InfIctiofauna">*</asp:RequiredFieldValidator>
            </td>
        </tr><tr>
            <td width="25%">
                <asp:Label ID="Label240" runat="server" SkinID="etiqueta_negra" 
                Text="Ecosistema"></asp:Label>
            </td>
           <td colspan="3" width="75%">
                <asp:DropDownList ID="cboEcoIcti" runat="server" SkinID="lista_desplegable"></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator80" runat="server"
                    ErrorMessage="Seleccione Ecosistema" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboEcoIcti" 
                    ValidationGroup="InfIctiofauna">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label241" runat="server" SkinID="etiqueta_negra" 
                Text="Importancia Ecológica"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtImpEcoInfIctiofauna" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator154" runat="server" 
                ErrorMessage="Ingrese la Importancia Ecológica"
                    Display="Dynamic" ControlToValidate="txtImpEcoInfIctiofauna"
                    ValidationGroup="InfIctiofauna">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label242" runat="server" SkinID="etiqueta_negra" 
                Text="Importacia Económica"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtImpEconInfIctiofauna" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator155" runat="server" 
                ErrorMessage="Ingrese la Importacia Económica"
                    Display="Dynamic" ControlToValidate="txtImpEconInfIctiofauna"
                    ValidationGroup="InfIctiofauna">*</asp:RequiredFieldValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarInfIctiofauna" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="InfIctiofauna" OnClick="btnAgregarInfIctiofauna_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarInfIctiofauna" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarInfIctiofauna_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary45" runat="server" 
                ValidationGroup="InfIctiofauna" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvInfIctiofauna" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado Información de Ictiofauna" OnRowDeleting="grvInfIctiofauna_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="ETM_TIPO_ESPECIE_MARINA" HeaderText="Especies de Interés" />
                        <asp:BoundField DataField="EIE_NOMBRE_COMUN" HeaderText="Nombre Común o Vulgar" />
                        <asp:BoundField DataField="EIE_NOMBRE_CIENTIF" HeaderText="Nombre Científico" />
                        <asp:BoundField DataField="EEM_TIPO_ECO_MARINO" HeaderText="Ecosistema" />
                        <asp:BoundField DataField="EIE_IMP_ECOLOGICA" HeaderText="Importancia Ecológica" />
                        <asp:BoundField DataField="EIE_IMP_ECONOMICA" HeaderText="Importancia Económica" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
          <%--  Flora --%>
        <tr>
            <td colspan="2" width="50%">
                3.2.3.2 Flora
            </td>
			<td width="50%" align="right" colspan="2">
                <asp:Button ID="btnNuevoInfFloraMar" runat="server" SkinID="boton_copia"
                    Text="Agregar Información de Flora" 
					onclick="btnNuevoInfFloraMar_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhInfFloraMar" Visible="False">
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label243" runat="server" SkinID="etiqueta_negra" 
                Text="Especies de Interés"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboEspIntInfFloraMar" runat="server" SkinID="lista_desplegable"></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator81" runat="server"
                    ErrorMessage="Seleccione la Especies de Interés" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboEspIntInfFloraMar" 
                    ValidationGroup="InfFloraMar">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label244" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre Común o Vulgar"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtNomComInfFloraMar" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator153" runat="server" 
                ErrorMessage="Ingrese el Nombre Común o Vulgar"
                    Display="Dynamic" ControlToValidate="txtNomComInfFloraMar"
                    ValidationGroup="InfFloraMar">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label245" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre Científico"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtNomCientInfFloraMar" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator156" runat="server" 
                ErrorMessage="Ingrese el Nombre Científico"
                    Display="Dynamic" ControlToValidate="txtNomCientInfFloraMar"
                    ValidationGroup="InfFloraMar">*</asp:RequiredFieldValidator>
            </td>
        </tr><tr>
            <td width="25%">
                <asp:Label ID="Label246" runat="server" SkinID="etiqueta_negra" 
                Text="Ecosistema"></asp:Label>
            </td>
             <td colspan="3" width="75%">
                <asp:DropDownList ID="cboEcoFlora" runat="server" SkinID="lista_desplegable"></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator82" runat="server"
                    ErrorMessage="Seleccione Ecosistema" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboEcoFlora" 
                    ValidationGroup="InfFloraMar">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label247" runat="server" SkinID="etiqueta_negra" 
                Text="Importancia Ecológica"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtImpEcoInfFloraMar" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator158" runat="server" 
                ErrorMessage="Ingrese la Importancia Ecológica"
                    Display="Dynamic" ControlToValidate="txtImpEcoInfFloraMar"
                    ValidationGroup="InfFloraMar">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label248" runat="server" SkinID="etiqueta_negra" 
                Text="Importacia Económica"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtImpEconInfFloraMar" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator159" runat="server" 
                ErrorMessage="Ingrese la Importacia Económica"
                    Display="Dynamic" ControlToValidate="txtImpEconInfFloraMar"
                    ValidationGroup="InfFloraMar">*</asp:RequiredFieldValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarInfFloraMar" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="InfFloraMar" OnClick="btnAgregarInfFloraMar_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarInfFloraMar" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarInfFloraMar_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary46" runat="server" 
                ValidationGroup="InfFloraMar" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvInfFloraMar" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado Información de Flora" OnRowDeleting="grvInfFloraMar_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="ETM_TIPO_ESPECIE_MARINA" HeaderText="Especies de Interés" />
                        <asp:BoundField DataField="EFE_NOMBRE_COMUN" HeaderText="Nombre Común o Vulgar" />
                        <asp:BoundField DataField="EFE_NOMBRE_CIENTIF" HeaderText="Nombre Científico" />
                        <asp:BoundField DataField="EEM_TIPO_ECO_MARINO" HeaderText="Ecosistema" />
                        <asp:BoundField DataField="EFE_IMP_ECOLOGICA" HeaderText="Importancia Ecológica" />
                        <asp:BoundField DataField="EFE_IMP_ECONOMICA" HeaderText="Importancia Económica" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
         <tr>
            <td colspan = "4" width="100%">3.3 Medio Socioeconómico</td>
        </tr>
        <!-- Medio Socioeconómico -  Área de Influencia Directa -->
        <tr>
            <td colspan = "2" width="50%">3.3.1 Área de Influencia Directa</td>
			<td width="50%" align="right" colspan="2">
                <asp:Button ID="btnNuevoAreaInfDir" runat="server" SkinID="boton_copia"
                    Text="Agregar Área de Influencia Directa" 
					onclick="btnNuevoAreaInfDir_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhAreaInfdir" Visible="False">
		
        <tr>
            <td width="25%">
                <asp:Label ID="Label249" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad Politico Administrativa"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboUnidPolAreaInfdir" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator83" runat="server"
                    ErrorMessage="Seleccione la Unidad Politico Administrativa" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboUnidPolAreaInfdir" 
                    ValidationGroup="AreaInfdir">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label250" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre de la Unidad Politico Administrativa"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtNomUnidAreaInfdir" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator157" runat="server" 
                ErrorMessage="Ingrese el Nombre de la Unidad Politico Administrativa"
                    Display="Dynamic" ControlToValidate="txtNomUnidAreaInfdir"
                    ValidationGroup="AreaInfdir">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label251" runat="server" SkinID="etiqueta_negra" 
                Text="Municipio"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtMunicipioAreaInfdir" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator160" runat="server" 
                ErrorMessage="Ingrese la información de la Municipio"
                    Display="Dynamic" ControlToValidate="txtMunicipioAreaInfdir"
                    ValidationGroup="AreaInfdir">*</asp:RequiredFieldValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarAreaInfdir" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="AreaInfdir" OnClick="btnAgregarAreaInfdir_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarAreaInfdir" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarAreaInfdir_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary47" runat="server" 
                ValidationGroup="AreaInfdir" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvAreaInfdir" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información del Área de Influencia Directa" OnRowDeleting="grvAreaInfdir_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EAI_ID" HeaderText="No." />
                        <asp:BoundField DataField="EPA_UNIDAD_POL_ADMIN" HeaderText="Unidad Politico Administrativa" />
                        <asp:BoundField DataField="EAI_NOMBRE" HeaderText="Nombre de la Unidad Politico Administrativa" />
                        <asp:BoundField DataField="EAI_MUNICIPIO" HeaderText="Municipio" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
           <!-- Medio Socioeconómico -  Territorios Étnicos -->
        <tr>
            <td colspan="2" width="50%">
                Territorios Étnicos
            </td>
			<td width="50%" align="right" colspan="2">
                <asp:Button ID="btnNuevoTerrEtnico" runat="server" SkinID="boton_copia"
                    Text="Agregar Territorio Étnico" 
					onclick="btnNuevoTerrEtnico_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhTerrEtnico" Visible="False">		
        <tr>
            <td width="25%">
                <asp:Label ID="Label252" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre del Territorio Étnico"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtNombTerrEtnico" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator161" runat="server" 
                ErrorMessage="Ingrese el Nombre del Territorio Étnico"
                    Display="Dynamic" ControlToValidate="txtNombTerrEtnico"
                    ValidationGroup="TerrEtnico">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label253" runat="server" SkinID="etiqueta_negra" 
                Text="Municipio"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtMunicipioTerrEtnico" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator162" runat="server" 
                ErrorMessage="Ingrese el Municipio"
                    Display="Dynamic" ControlToValidate="txtMunicipioTerrEtnico"
                    ValidationGroup="TerrEtnico">*</asp:RequiredFieldValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarTerrEtnico" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="TerrEtnico" OnClick="btnAgregarTerrEtnico_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarTerrEtnico" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarTerrEtnico_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary48" runat="server" 
                ValidationGroup="TerrEtnico" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvTerrEtnico" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de Territorios Étnicos" OnRowDeleting="grvTerrEtnico_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EEI_ID" HeaderText="No." />
                        <asp:BoundField DataField="EEI_NOMBRE_TERR_ETNICO" HeaderText="Nombre del Territorio Étnico" />
                        <asp:BoundField DataField="EEI_MUNICIPIO" HeaderText="Municipio" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        
          <!-- Medio Socioeconómico -  Dimensión Demográfica Area de Influencia Directa -->
        <tr>
            <td width="25%">
                3.3.2 Dimensión Demográfica Area de Influencia Directa
            </td>
			<td width="75%" align="right" colspan="3">
                <asp:Button ID="btnNuevoDimDemo" runat="server" SkinID="boton_copia"
                    Text="Agregar Dimensión Demográfica" 
					onclick="btnNuevoDimDemo_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhDimDemo" Visible="False">		
		
		<tr>
            <td width="25%">
                <asp:Label ID="Label277" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad Politico Administrativa"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboAreas1" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator105" runat="server"
                    ErrorMessage="Seleccione El Area a Asignar Valores" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboAreas1" 
                    ValidationGroup="DimDemo">*</asp:CompareValidator>
            </td>
        </tr>
		
        <tr>
            <td width="25%">
                <asp:Label ID="Label254" runat="server" SkinID="etiqueta_negra" 
                Text="No. de Habitantes"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtNoHabitDimDemo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator163" runat="server" 
                    ErrorMessage="Ingrese el No. de Habitantes"
                    Display="Dynamic" ControlToValidate="txtNoHabitDimDemo"
                    ValidationGroup="DimDemo">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator84" runat="server"
                    ErrorMessage="El No de Habitantes debe ser un número entero" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Integer"
                    ControlToValidate="txtNoHabitDimDemo" 
                    ValidationGroup="DimDemo">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                Tipo de Población Asentada (%)
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label255" runat="server" SkinID="etiqueta_negra" 
                Text="Indigenas"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtIndigPobDimDemo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator85" runat="server"
                    ErrorMessage="El Porcentaje de Indigenas debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtIndigPobDimDemo" 
                    ValidationGroup="DimDemo">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label256" runat="server" SkinID="etiqueta_negra" 
                Text="Afrodescendientes"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtAfroPobDimDemo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator86" runat="server"
                    ErrorMessage="El Porcentaje de Afrodescendientes debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtAfroPobDimDemo" 
                    ValidationGroup="DimDemo">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label257" runat="server" SkinID="etiqueta_negra" 
                Text="Colonos"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtColonPobDimDemo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator87" runat="server"
                    ErrorMessage="El Porcentaje de Colonos debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtColonPobDimDemo" 
                    ValidationGroup="DimDemo">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label258" runat="server" SkinID="etiqueta_negra" 
                Text="Campesinos"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtCampPobDimDemo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator88" runat="server"
                    ErrorMessage="El Porcentaje de Campesinos debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtCampPobDimDemo" 
                    ValidationGroup="DimDemo">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label259" runat="server" SkinID="etiqueta_negra" 
                Text="Desplazados"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtDesPobDimDemo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator89" runat="server"
                    ErrorMessage="El Porcentaje de Desplazados debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtDesPobDimDemo" 
                    ValidationGroup="DimDemo">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label260" runat="server" SkinID="etiqueta_negra" 
                Text="Otros"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboOtrosPobDimDemo" runat="server" SkinID="lista_desplegable" AutoPostBack="true" OnSelectedIndexChanged="cboOtrosPobDimDemo_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:TextBox ID="txtOtroPobDimDemo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" Visible="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                Distribución por Sexo (%)
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label261" runat="server" SkinID="etiqueta_negra" 
                Text="Masculino"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtMascDimDemo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator90" runat="server"
                    ErrorMessage="El Porcentaje Masculino debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtMascDimDemo" 
                    ValidationGroup="DimDemo">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label262" runat="server" SkinID="etiqueta_negra" 
                Text="Femenino"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtFemDimDemo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator91" runat="server"
                    ErrorMessage="El Porcentaje Femenino debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtFemDimDemo" 
                    ValidationGroup="DimDemo">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label263" runat="server" SkinID="etiqueta_negra" 
                Text="Tendencia de Crecimiento (%)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtTenCreDimDemo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator92" runat="server"
                    ErrorMessage="La Tendencia de Crecimiento debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtTenCreDimDemo" 
                    ValidationGroup="DimDemo">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label264" runat="server" SkinID="etiqueta_negra" 
                Text="Tasa de Natalidad (%)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtTasNatDimDemo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator93" runat="server"
                    ErrorMessage="La Tasa de Natalidad debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtTasNatDimDemo" 
                    ValidationGroup="DimDemo">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label265" runat="server" SkinID="etiqueta_negra" 
                Text="No. de Familias"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtNoFamDimDemo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator94" runat="server"
                    ErrorMessage="El No. de Familias debe ser un número entero" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Integer"
                    ControlToValidate="txtNoFamDimDemo" 
                    ValidationGroup="DimDemo">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                Distribución por Grupos de Edad
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label266" runat="server" SkinID="etiqueta_negra" 
                Text="0-5 Años"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txt0_5AniosDimDemo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator95" runat="server"
                    ErrorMessage="El Número de habitantes entre 0 y 5 años debe ser un número entero" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Integer"
                    ControlToValidate="txt0_5AniosDimDemo" 
                    ValidationGroup="DimDemo">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label267" runat="server" SkinID="etiqueta_negra" 
                Text="6-12 Años"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txt6_12AniosDimDemo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator96" runat="server"
                    ErrorMessage="El Número de habitantes entre 6 y 12 años debe ser un número entero" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Integer"
                    ControlToValidate="txt6_12AniosDimDemo" 
                    ValidationGroup="DimDemo">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label268" runat="server" SkinID="etiqueta_negra" 
                Text="13-50 Años"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txt13_50AniosDimDemo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator97" runat="server"
                    ErrorMessage="El Número de habitantes entre 13 y 50 años debe ser un número entero" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Integer"
                    ControlToValidate="txt13_50AniosDimDemo" 
                    ValidationGroup="DimDemo">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label269" runat="server" SkinID="etiqueta_negra" 
                Text="51-60 Años"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txt51_60AniosDimDemo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator98" runat="server"
                    ErrorMessage="El Número de habitantes entre 51 y 60 años debe ser un número entero" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Integer"
                    ControlToValidate="txt51_60AniosDimDemo" 
                    ValidationGroup="DimDemo">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label270" runat="server" SkinID="etiqueta_negra" 
                Text="Mayores de 60 Años"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txt60AniosDimDemo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator99" runat="server"
                    ErrorMessage="El Número de habitantes Mayores de 60 Años debe ser un número entero" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Integer"
                    ControlToValidate="txt60AniosDimDemo" 
                    ValidationGroup="DimDemo">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label271" runat="server" SkinID="etiqueta_negra" 
                Text="Tasa de Mortalidad (%)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtTasaMortDimDemo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator100" runat="server"
                    ErrorMessage="La Tasa de Mortalidad debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtTasaMortDimDemo" 
                    ValidationGroup="DimDemo">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label272" runat="server" SkinID="etiqueta_negra" 
                Text="Tasa NBI (%)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtTasaNbiDimDemo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator101" runat="server"
                    ErrorMessage="La Tasa NBI debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtTasaNbiDimDemo" 
                    ValidationGroup="DimDemo">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label273" runat="server" SkinID="etiqueta_negra" 
                Text="Población Económicamente Activa (%)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtPobActDimDemo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator102" runat="server"
                    ErrorMessage="La Población Económicamente Activa debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtPobActDimDemo" 
                    ValidationGroup="DimDemo">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label274" runat="server" SkinID="etiqueta_negra" 
                Text="Índice de Calidad de Vida (%)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtIndCalDimDemo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator103" runat="server"
                    ErrorMessage="El índice de Calidad de Vida debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtIndCalDimDemo" 
                    ValidationGroup="DimDemo">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label275" runat="server" SkinID="etiqueta_negra" 
                Text="% Morbilidad"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtPorMorbDimDemo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator104" runat="server"
                    ErrorMessage="El Porcentaje de Morbilidad debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtPorMorbDimDemo" 
                    ValidationGroup="DimDemo">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label276" runat="server" SkinID="etiqueta_negra" 
                Text="Descripcion enfermedades mas importantes"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtDescEnfDimDemo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="Especifique las Tres Principales Enfermedades"></asp:TextBox>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarDimDemo" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="DimDemo" OnClick="btnAgregarDimDemo_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarDimDemo" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarDimDemo_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary49" runat="server" 
                ValidationGroup="DimDemo" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvDimDemo" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de Dimensión Demográfica en el Área de Influencia Directa" OnRowDeleting="grvDimDemo_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EAI_ID" HeaderText="No. Area" />
                        <asp:BoundField DataField="EDD_NO_HABITANTES" HeaderText="No de Habitantes" />
                        <asp:BoundField DataField="EDD_PORC_INDIGENAS" HeaderText="% de Indigenas" />
                        <asp:BoundField DataField="EDD_PORC_AFROD" HeaderText="% de Afrodecendientes" />
                        <asp:BoundField DataField="EDD_PORC_COLONOS" HeaderText="% de Colonos" />
                        <asp:BoundField DataField="EDD_PORC_CAMPESINOS" HeaderText="% de Desplazados" />
                        <asp:BoundField DataField="ETO_TIPO_OTRA_POBLACION" HeaderText="Otro Tipo de Población" />
                        <asp:BoundField DataField="EDD_PORC_MASCULINO" HeaderText="% de Hombres" />
                        <asp:BoundField DataField="EDD_PORC_FEMENINO" HeaderText="% de Mujeres" />
                        <asp:BoundField DataField="EDD_PORC_TEND_CRECIMI" HeaderText="Tendencia de Crecimiento (%)" />
                        <asp:BoundField DataField="EDD_PORC_TASA_NATAL" HeaderText="Tasa de Natalidad (%)" />                        
                    </Columns>
                </asp:GridView>
                <asp:GridView runat="server" ID="grvDimDemo2" AutoGenerateColumns="False" SkinID="Grilla_simple"
                width="99%" OnRowDeleting="grvDimDemo2_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EAI_ID" HeaderText="No. Area" />
                        <asp:BoundField DataField="EDD_NO_HABITANTES" HeaderText="No de Habitantes" />                        
                        <asp:BoundField DataField="EDD_NO_FAMILIAS" HeaderText="No. de Familias" />
                        <asp:BoundField DataField="EDD_NO_0_5" HeaderText="Habitantes de 0 a 5 Años" />
                        <asp:BoundField DataField="EDD_NO_6_12" HeaderText="Habitantes de 6 a 12 Años" />
                        <asp:BoundField DataField="EDD_NO_13_50" HeaderText="Habitantes de 13 a 50 Años" />
                        <asp:BoundField DataField="EDD_NO_51_60" HeaderText="Habitantes de 51 a 60 Años" />                        
                        <asp:BoundField DataField="EDD_NO_60" HeaderText="Habitantes Mayores de 60 Años" />
                        <asp:BoundField DataField="EDD_PORC_TASA_MORTAL" HeaderText="Tasa de Mortalidad (%)" />
                        <asp:BoundField DataField="EDD_PORC_TASA_NBI" HeaderText="Tasa NBI (%)" />
                        <asp:BoundField DataField="EDD_PORC_POBL_EC_ACTI" HeaderText="Población Económicamente Activa (%)" />
                        <asp:BoundField DataField="EDD_PORC_INDIC_CALIDAD_V" HeaderText="Índice de Calidad de Vida (%)" />
                        <asp:BoundField DataField="EDD_PORC_MORBILIDAD" HeaderText="% Morbilidad" />
                        <asp:BoundField DataField="EDD_DESC_ENFERMEDADES" HeaderText="Descripcion enfermedades mas importantes" />                        
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
                <!-- Medio Socioeconómico -  Dimensión Espacial Area de Influencia Directa -->
        <tr>
            <td width="25%">
                3.3.4 Dimensión Espacial Area de Influencia Directa
            </td>
			<td width="75%" align="right" colspan="3">
                <asp:Button ID="btnNuevoDimEspac" runat="server" SkinID="boton_copia"
                    Text="Agregar Dimensión Espacial" 
					onclick="btnNuevoDimEspac_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhDimEspac" Visible="False">
		
		<tr>
            <td width="25%">
                <asp:Label ID="Label291" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad Politico Administrativa"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboAreas2" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator119" runat="server"
                    ErrorMessage="Seleccione El Area a Asignar Valores" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboAreas2" 
                    ValidationGroup="DimEspac">*</asp:CompareValidator>
            </td>
        </tr>
		<tr>
            <td width="25%">
                <asp:Label ID="Label278" runat="server" SkinID="etiqueta_negra" 
                Text="No. de Viviendas"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtNoVivDimeEcono" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator106" runat="server"
                    ErrorMessage="El No de Viviendas debe ser un número entero" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Integer"
                    ControlToValidate="txtNoVivDimeEcono" 
                    ValidationGroup="DimEspac">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label279" runat="server" SkinID="etiqueta_negra" 
                Text="Energia Eléctrica (%)"></asp:Label>
            </td>        
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtPEnerElecDimEspac" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator107" runat="server"
                    ErrorMessage="El Porcentaje de Viviendas con Energia Eléctrica debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtPEnerElecDimEspac" 
                    ValidationGroup="DimEspac">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label280" runat="server" SkinID="etiqueta_negra" 
                Text="Acueducto u Otro Sistema (%)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtPAcuedDimEspac" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator108" runat="server"
                    ErrorMessage="El Porcentaje de Viviendas con Acueducto u Otro Sistema debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtPAcuedDimEspac" 
                    ValidationGroup="DimEspac">*</asp:CompareValidator>
            </td>        
        </tr>       
        <tr>
            <td width="25%">
                <asp:Label ID="Label282" runat="server" SkinID="etiqueta_negra" 
                Text="Alcantarillado (%)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtPAlcanDimEspac" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator110" runat="server"
                    ErrorMessage="El Porcentaje de Viviendas con Alcantarillado debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtPAlcanDimEspac" 
                    ValidationGroup="DimEspac">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label283" runat="server" SkinID="etiqueta_negra" 
                Text="Telefonía (%)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtPTelefDimEspac" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator111" runat="server"
                    ErrorMessage="El Porcentaje de Viviendas con Telefonía debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtPTelefDimEspac" 
                    ValidationGroup="DimEspac">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label284" runat="server" SkinID="etiqueta_negra" 
                Text="Deficit vivienda (%)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtPDefVivienDimEspac" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator112" runat="server"
                    ErrorMessage="El Deficit vivienda debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtPDefVivienDimEspac" 
                    ValidationGroup="DimEspac">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label285" runat="server" SkinID="etiqueta_negra" 
                Text="Cobertura Salud (%)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtPCobSaludDimEspac" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator113" runat="server"
                    ErrorMessage="La Cobertura en Salud debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtPCobSaludDimEspac" 
                    ValidationGroup="DimEspac">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label286" runat="server" SkinID="etiqueta_negra" 
                Text="Cobertura Educación (%)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtPCobEduDimEspac" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator114" runat="server"
                    ErrorMessage="La Cobertura en Educación debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtPCobEduDimEspac" 
                    ValidationGroup="DimEspac">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label287" runat="server" SkinID="etiqueta_negra" 
                Text="Otros Sistemas (%)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtPCobOtrosDimEspac" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator115" runat="server"
                    ErrorMessage="La Cobertura en Otros Sistemas debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtPCobOtrosDimEspac" 
                    ValidationGroup="DimEspac">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                Vías
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label288" runat="server" SkinID="etiqueta_negra" 
                Text="Principales (Km)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtViasPDimEspac" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator116" runat="server"
                    ErrorMessage="La longitud de vías principales debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtViasPDimEspac" 
                    ValidationGroup="DimEspac">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label289" runat="server" SkinID="etiqueta_negra" 
                Text="Secundarias (Km)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtViasSDimEspac" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator117" runat="server"
                    ErrorMessage="La longitud de vías secundarias debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtViasSDimEspac" 
                    ValidationGroup="DimEspac">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label290" runat="server" SkinID="etiqueta_negra" 
                Text="Terciarias (Km)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtViasTDimEspac" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator118" runat="server"
                    ErrorMessage="La longitud de vías terciarias debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtViasTDimEspac" 
                    ValidationGroup="DimEspac">*</asp:CompareValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarDimEspac" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="DimEspac" OnClick="btnAgregarDimEspac_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarDimEspac" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarDimEspac_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary50" runat="server" 
                ValidationGroup="DimEspac" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvDimEspac" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de Dimensión Espacial del Area de Influencia Directa" OnRowDeleting="grvDimEspac_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EAI_ID" HeaderText="No." />
                        <asp:BoundField DataField="EDE_NO_VIVIENDAS" HeaderText="No. de Viviendas" />
                        <asp:BoundField DataField="EDE_PORC_ENERGIA_ELECTRICA" HeaderText="Energia Eléctrica (%)" />
                        <asp:BoundField DataField="EDE_PORC_ACUE_OTRO_SIST" HeaderText="Acueducto u Otro Sistema (%)" />
                        <asp:BoundField DataField="EDE_PORC_ALCANTARILLADO" HeaderText="Alcantarillado (%)" />
                        <asp:BoundField DataField="EDE_PORC_TELEFONIA" HeaderText="Telefonía (%)" />
                        <asp:BoundField DataField="EDE_PORC_DEF_VIVIENDA" HeaderText="Deficit vivienda (%)" />
                        <asp:BoundField DataField="EDE_PORC_COBERT_SALUD" HeaderText="Cobertura Salud (%)" />
                        <asp:BoundField DataField="EDE_PORC_COBERT_EDU" HeaderText="Cobertura Educación (%)" />
                        <asp:BoundField DataField="EDE_PORC_OTROS_SIST" HeaderText="Otros Sistemas (%)" />
                        <asp:BoundField DataField="EDE_VIAS_PRINCIPALES" HeaderText="Vías Principales (Km)" />
                        <asp:BoundField DataField="EDE_VIAS_SECUNDARIAS" HeaderText="Vías Secundarias (Km)" />
                        <asp:BoundField DataField="EDE_VIAS_TERCIARIAS" HeaderText="Vías Terciarias (Km)" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
          <!-- Medio Socioeconómico -  Dimensión Económica Area de Influencia Directa -->
        <tr>
            <td colspan="2" width="50%">
                3.3.4 Dimensión Económica Area de Influencia Directa
            </td>
			<td width="50%" align="right" colspan="2">
                <asp:Button ID="btnNuevoDimEcono" runat="server" SkinID="boton_copia"
                    Text="Agregar Dimensión Económica" 
					onclick="btnNuevoDimEcono_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhDimEcono" Visible="False">
		
		<tr>
		    <td colspan="4" width="100%">
		        Estructura de la Propiedad 
		    </td>
		</tr>
		<tr>
            <td width="25%">
                <asp:Label ID="Label298" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad Politico Administrativa"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboAreas3" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator126" runat="server"
                    ErrorMessage="Seleccione El Area a Asignar Valores" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboAreas3" 
                    ValidationGroup="DimEcono">*</asp:CompareValidator>
            </td>
        </tr>
		<tr>
            <td width="25%">
                <asp:Label ID="Label281" runat="server" SkinID="etiqueta_negra" 
                Text="Minifundio (0-10 Ha) (%)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtMinifunDimEcono" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator109" runat="server"
                    ErrorMessage="El Porcentaje de Minifundios debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtMinifunDimEcono" 
                    ValidationGroup="DimEcono">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label292" runat="server" SkinID="etiqueta_negra" 
                Text="Mediana Propiedad (11-500 Ha) (%)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtMedPropDimEcono" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator120" runat="server"
                    ErrorMessage="El Porcentaje de Media Propiedad debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtMedPropDimEcono" 
                    ValidationGroup="DimEcono">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label293" runat="server" SkinID="etiqueta_negra" 
                Text="Gran Propiedad (>501 Ha) (%)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtGranPDimEcono" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator121" runat="server"
                    ErrorMessage="El Porcentaje de Gran Propiedad debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtGranPDimEcono" 
                    ValidationGroup="DimEcono">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
		    <td colspan="4" width="100%">
		        Actividades Económicas
		    </td>
		</tr>
		<tr>
            <td width="25%">
                <asp:Label ID="Label294" runat="server" SkinID="etiqueta_negra" 
                Text="Primer Orden (%)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtPriOrdDimEcono" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator122" runat="server"
                    ErrorMessage="La información de actividades económicas de primer orden debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtPriOrdDimEcono" 
                    ValidationGroup="DimEcono">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label295" runat="server" SkinID="etiqueta_negra" 
                Text="Segundo Orden (%)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtSegOrdDimEcono" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator123" runat="server"
                    ErrorMessage="La información de actividades económicas de segundo orden debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtSegOrdDimEcono" 
                    ValidationGroup="DimEcono">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label296" runat="server" SkinID="etiqueta_negra" 
                Text="Tercer Orden (%)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtTerOrdDimEcono" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator124" runat="server"
                    ErrorMessage="La información de actividades económicas de tercer orden debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtTerOrdDimEcono" 
                    ValidationGroup="DimEcono">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label297" runat="server" SkinID="etiqueta_negra" 
                Text="Tasa de Desempleo (%)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtTasaDesempDimEcono" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator125" runat="server"
                    ErrorMessage="La Tasa de Desempleo debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtTasaDesempDimEcono" 
                    ValidationGroup="DimEcono">*</asp:CompareValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarDimEcono" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="DimEcono" OnClick="btnAgregarDimEcono_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarDimEcono" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarDimEcono_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary51" runat="server" 
                ValidationGroup="DimEcono" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvDimEcono" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de Dimensión Económica Area de Influencia Directa" OnRowDeleting="grvDimEcono_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EAI_ID" HeaderText="No." />
                        <asp:BoundField DataField="EED_PORC_MINIFUNDIO" HeaderText="Minifundio (0-10 Ha)(%)" />
                        <asp:BoundField DataField="EED_PORC_MED_PROPIEDAD" HeaderText="Mediana Propiedad (11-500 Ha)(%)" />
                        <asp:BoundField DataField="EED_PORC_GRAN_PROPIEDAD" HeaderText="Gran Propiedad (>501 Ha)(%)" />
                        <asp:BoundField DataField="EED_PORC_ACTECO_PRIMER" HeaderText="Primer Orden(%)" />
                        <asp:BoundField DataField="EED_PORC_ACTECO_SEGUNDO" HeaderText="Segundo Orden(%)" />
                        <asp:BoundField DataField="EED_PORC_ACTECO_TERCERO" HeaderText="Tercer Orden(%)" />
                        <asp:BoundField DataField="EED_PORC_TASA_DESEMPLEO" HeaderText="Tasa de Desempleo(%)" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
         <!-- Medio Socioeconómico -  Dimensión Cultural Area de Influencia Directa -->
		<tr>
            <td colspan="4" width="100%">
                3.3.5 Dimensión Cultural Area de Influencia Directa
            </td>
		</tr>
		<tr>
            <td colspan="2" width="50%">
                3.3.5.1 Información de Comunidades NO Étnicas
            </td>
			<td width="50%" align="right" colspan="2">
                <asp:Button ID="btnNuevoComNoEtn" runat="server" SkinID="boton_copia"
                    Text="Agregar Información de Comunidades NO Étnicas"
					onclick="btnNuevoComNoEtn_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhComNoEtn" Visible="False">
		
		<tr>
		    <td colspan="4"  width="100%">
		        Hechos Históricos que Implicaron Cambio y Adapatación
		    </td>
		</tr>
		<tr>
            <td width="25%">
                <asp:Label ID="Label302" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad Politico Administrativa"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboAreas4" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator127" runat="server"
                    ErrorMessage="Seleccione El Area a Asignar Valores" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboAreas4" 
                    ValidationGroup="ComNoEtn">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label299" runat="server" SkinID="etiqueta_negra" 
                Text="Acontecimientos Historicos Humanos"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtHistHumComNoEtn" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator164" runat="server" 
                ErrorMessage="Ingrese los Acontecimientos Historicos Humanos"
                    Display="Dynamic" ControlToValidate="txtHistHumComNoEtn"
                    ValidationGroup="ComNoEtn">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label300" runat="server" SkinID="etiqueta_negra" 
                Text="Acontecimientos Historicos Naturales"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtHistNatComNoEtn" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator165" runat="server" 
                ErrorMessage="Ingrese los Acontecimientos Historicos Naturales"
                    Display="Dynamic" ControlToValidate="txtHistNatComNoEtn"
                    ValidationGroup="ComNoEtn">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label301" runat="server" SkinID="etiqueta_negra" 
                Text="Cambios en la Percepción y Valoración Cultural del Espacio"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtPercValComNoEtn" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator166" runat="server" 
                ErrorMessage="Ingrese los Cambios en la Percepción y Valoración Cultural del Espacio"
                    Display="Dynamic" ControlToValidate="txtPercValComNoEtn"
                    ValidationGroup="ComNoEtn">*</asp:RequiredFieldValidator>
            </td>    
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarComNoEtn" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="ComNoEtn" OnClick="btnAgregarComNoEtn_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarComNoEtn" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarComNoEtn_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary52" runat="server" 
                ValidationGroup="ComNoEtn" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvComNoEtn" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de Comunidades NO Étnicas" OnRowDeleting="grvComNoEtn_RowDeleting">
                    <Columns>                    
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EAI_ID" HeaderText="No." />
                        <asp:BoundField DataField="EHH_ACONT_HIST_HUMANOS" HeaderText="Acontecimientos Historicos Humanos" />
                        <asp:BoundField DataField="EHH_ACONT_HIST_NATURALES" HeaderText="Acontecimientos Historicos Naturales" />                        
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
         <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvPerpCult" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de Comunidades NO Étnicas" OnRowDeleting="grvPerpCult_RowDeleting">
                    <Columns>                    
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EAI_ID" HeaderText="No." />
                        <asp:BoundField DataField="EPC_PERCEP_VAL_CULTURAL" HeaderText="Cambios en la Percepción y Valoración Cultural del Espacio" />                        
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        
         <!-- Medio Socioeconómico -  Relaciones Culturales con el Entorno-->
		<tr>
            <td colspan="1" width="25%">
                3.3.5.2 Relaciones Culturales con el Entorno
            </td>
			<td width="75%" align="right" colspan="3">
                <asp:Button ID="btnNuevoRelCulEnt" runat="server" SkinID="boton_copia"
                    Text="Agregar Relacion Cultural con el Entorno"
					onclick="btnNuevoRelCulEnt_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhRelCulEnt" Visible="False">
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label305" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad Politico Administrativa"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboAreas5" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator128" runat="server"
                    ErrorMessage="Seleccione El Area a Asignar Valores" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboAreas5" 
                    ValidationGroup="RelCulEnt">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label303" runat="server" SkinID="etiqueta_negra" 
                Text="Uso y Manejo de Recursos Naturales"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtUsoManRecNatRelCulEnt" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator167" runat="server" 
                ErrorMessage="Ingrese la información sobre el Uso y Manejo de Recursos Naturales"
                    Display="Dynamic" ControlToValidate="txtUsoManRecNatRelCulEnt"
                    ValidationGroup="RelCulEnt">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label304" runat="server" SkinID="etiqueta_negra" 
                Text="Prácticas y Concepciones Culturales"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtPractConMarRelCulEnt" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator168" runat="server" 
                ErrorMessage="Ingrese la información de Prácticas y Concepciones Culturales"
                    Display="Dynamic" ControlToValidate="txtPractConMarRelCulEnt"
                    ValidationGroup="RelCulEnt">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarRelCulEnt" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="RelCulEnt" OnClick="btnAgregarRelCulEnt_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarRelCulEnt" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarRelCulEnt_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary53" runat="server" 
                ValidationGroup="RelCulEnt" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvRelCulEnt" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de Relaciones Culturales con el Entorno" OnRowDeleting="grvRelCulEnt_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EAI_ID" HeaderText="No." />
                        <asp:BoundField DataField="ERC_USO_REC_NATURALES" HeaderText="Uso y Manejo de Recursos Naturales" />
                        <asp:BoundField DataField="ERC_PRACTICAS_CULTURALES" HeaderText="Prácticas y Concepciones Culturales" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        
        <tr>
            <td colspan="2" width="50%">
                3.3.5.3 Información de Comunidades Étnicas
            </td>
			<td width="50%" align="right" colspan="2">
                <asp:Button ID="btnNuevoInfoComEtn" runat="server" SkinID="boton_copia"
                    Text="Agregar Información de Comunidades Étnicas"
					onclick="btnNuevoInfoComEtn_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhInfoComEtn" Visible="False">
		
		<tr>
            <td width="25%">
                <asp:Label ID="Label312" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad Politico Administrativa"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboAreas6" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator130" runat="server"
                    ErrorMessage="Seleccione El Area a Asignar Valores" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboAreas6" 
                    ValidationGroup="InfoComEtn">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label306" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtNombreInfoComEtn" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator169" runat="server" 
                ErrorMessage="Ingrese el Nombre"
                    Display="Dynamic" ControlToValidate="txtNombreInfoComEtn"
                    ValidationGroup="InfoComEtn">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label307" runat="server" SkinID="etiqueta_negra" 
                Text="Territorio (Resolución)"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtTerrInfoComEtn" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator170" runat="server" 
                ErrorMessage="Ingrese la información del Territorio (Resolución)"
                    Display="Dynamic" ControlToValidate="txtTerrInfoComEtn"
                    ValidationGroup="InfoComEtn">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label308" runat="server" SkinID="etiqueta_negra" 
                Text="No. de Habitantes"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtNoHabInfoComEtn" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator171" runat="server" 
                ErrorMessage="Ingrese el No. de Habitantes"
                    Display="Dynamic" ControlToValidate="txtNoHabInfoComEtn"
                    ValidationGroup="InfoComEtn">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator129" runat="server"
                    ErrorMessage="El No. de Habitantes debe ser un número entero" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Integer"
                    ControlToValidate="cboEspIntInfFloraMar" 
                    ValidationGroup="InfFloraMar">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label309" runat="server" SkinID="etiqueta_negra" 
                Text="Sistema Económico"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtSistEconoInfoComEtn" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator172" runat="server" 
                ErrorMessage="Ingrese el Sistema Económico"
                    Display="Dynamic" ControlToValidate="txtSistEconoInfoComEtn"
                    ValidationGroup="InfoComEtn">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label310" runat="server" SkinID="etiqueta_negra" 
                Text="Presencia Institucional"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtPreInstInfoComEtn" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator173" runat="server" 
                ErrorMessage="Ingrese la información de Presencia Institucional"
                    Display="Dynamic" ControlToValidate="txtPreInstInfoComEtn"
                    ValidationGroup="InfoComEtn">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label311" runat="server" SkinID="etiqueta_negra" 
                Text="Organización Comunitaria"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtOrgComunInfoComEtn" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator174" runat="server" 
                ErrorMessage="Ingrese la información de Organización Comunitaria"
                    Display="Dynamic" ControlToValidate="txtOrgComunInfoComEtn"
                    ValidationGroup="InfoComEtn">*</asp:RequiredFieldValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarInfoComEtn" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="InfoComEtn" OnClick="btnAgregarInfoComEtn_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarInfoComEtn" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarInfoComEtn_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary54" runat="server" 
                ValidationGroup="InfoComEtn" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvInfoComEtn" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de Comunidades Étnicas" OnRowDeleting="grvInfoComEtn_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EAI_ID" HeaderText="No." />
                        <asp:BoundField DataField="EIC_NOMBRE" HeaderText="Nombre" />
                        <asp:BoundField DataField="EIC_TERRITORIO" HeaderText="Territorio (Resolución)" />
                        <asp:BoundField DataField="EIC_NO_HABITANTES" HeaderText="No. de Habitantes" />
                        <asp:BoundField DataField="EIC_SIST_ECONOMICO" HeaderText="Sistema Económico" />
                        <asp:BoundField DataField="EIC_PRES_INSTITUCIONAL" HeaderText="Presencia Institucional" />
                        <asp:BoundField DataField="EIC_ORG_COMUNITARIA" HeaderText="Organización Comunitaria" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td width="25%">
                3.3.5.4 Información de Grupos Indigenas Etnohistóricos
            </td>
			<td width="75%" align="right" colspan="3">
                <asp:Button ID="btnNuevoInfIndHist" runat="server" SkinID="boton_copia"
                    Text="Agregar Información de Grupos Indigenas Etnohistóricos"
					onclick="btnNuevoInfIndHist_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhInfIndHist" Visible="False">		
		<tr>
            <td width="25%">
                <asp:Label ID="Label314" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad Politico Administrativa"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboAreas7" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator131" runat="server"
                    ErrorMessage="Seleccione El Area a Asignar Valores" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboAreas7" 
                    ValidationGroup="InfIndHist">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label313" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtNombreInfIndHist" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator175" runat="server" 
                ErrorMessage="Ingrese el Nombre"
                    Display="Dynamic" ControlToValidate="txtNombreInfIndHist"
                    ValidationGroup="InfIndHist">*</asp:RequiredFieldValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarInfIndHist" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="InfIndHist" OnClick="btnAgregarInfIndHist_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarInfIndHist" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarInfIndHist_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary55" runat="server" 
                ValidationGroup="InfIndHist" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvInfIndHist" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de los Sistemas de Corrientes" OnRowDeleting="grvInfIndHist_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EAI_ID" HeaderText="No." />
                        <asp:BoundField DataField="EGI_NOMBRE" HeaderText="Nombre" />                        
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2" width="50%">
                3.3.5.5 Información de Sitios Arqueológicos con Anterioridad
            </td>
			<td width="50%" align="right" colspan="2">
                <asp:Button ID="btnNuevoSitArqAnt" runat="server" SkinID="boton_copia"
                    Text="Agregar Sitio Arqueológico con Anterioridad"
					onclick="btnNuevoSitArqAnt_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhSitArqAnt" Visible="False">
		
		<tr>
            <td width="25%">
                <asp:Label ID="Label317" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad Politico Administrativa"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboAreas8" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator132" runat="server"
                    ErrorMessage="Seleccione El Area a Asignar Valores" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboAreas8" 
                    ValidationGroup="SitArqAnt">*</asp:CompareValidator>
            </td>
        </tr>
		<tr>
            <td width="25%" >
                <asp:Label ID="Label315" runat="server" SkinID="etiqueta_negra" 
                Text="Localización Coordenadas Planas (Datum Magna-Sirgas):"></asp:Label></td>
            <td width="75%" colspan="3">
                <uc1:ctrCoordenadas runat="server" id="ctrCoorSitArqAnt" />
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label316" runat="server" SkinID="etiqueta_negra" 
                Text="Descripción"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtDescSitArqAnt" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator176" runat="server" 
                ErrorMessage="Ingrese la Descripción"
                    Display="Dynamic" ControlToValidate="txtDescSitArqAnt"
                    ValidationGroup="SitArqAnt">*</asp:RequiredFieldValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarSitArqAnt" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="SitArqAnt" OnClick="btnAgregarSitArqAnt_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarSitArqAnt" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarSitArqAnt_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary56" runat="server" 
                ValidationGroup="SitArqAnt" />
            </td>
        </tr>
       </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvSitArqAnt" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de Sitios Arqueológicos con Anterioridad" OnRowDeleting="grvSitArqAnt_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EAI_ID" HeaderText="No." />
                        <asp:TemplateField HeaderText="Areas de Recarga Localización - Coordenadas planas (Datum Magna-Sirgas)">
                            <ItemTemplate>                                
                                <uc1:ctrCoordenadas ID="ctrCoorForestGrid" runat="server" DataGridObject="true" NombreTabla="EIH_COOR_SITIO_ARQ_ANT" NombreCampo="ESA_ID" ValorCampo='<%# Eval("ESA_ID") %>' ValorCampo2='<%# Eval("ESA_ID") %>' />                                           
                                <asp:Label id="lblEsaId" runat="server" text='<%# Eval("ESA_ID") %>' visible="false"></asp:Label>                        
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:BoundField DataField="ESA_DESCRIPCION" HeaderText="Descripción" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        		<tr>
            <td colspan="2" width="50%">
                3.3.5.6 Información de Sitios Arqueológicos Rescatados en este Proyecto
            </td>
			<td width="50%" align="right" colspan="2">
                <asp:Button ID="btnNuevoSitArqProy" runat="server" SkinID="boton_copia"
                    Text="Agregar Sitio Arqueológico"
					onclick="btnNuevoSitArqProy_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhSitArqProy" Visible="False">
		
		<tr>
            <td width="25%">
                <asp:Label ID="Label320" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad Politico Administrativa"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboAreas9" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator133" runat="server"
                    ErrorMessage="Seleccione El Area a Asignar Valores" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboAreas9" 
                    ValidationGroup="SitArqProy">*</asp:CompareValidator>
            </td>
        </tr>
		<tr>
            <td width="25%">
                <asp:Label ID="Label318" runat="server" SkinID="etiqueta_negra" 
                Text="Localización Coordenadas Planas (Datum Magna-Sirgas):"></asp:Label></td>
            <td width="75%" colspan="3">
                <uc1:ctrCoordenadas runat="server" id="ctrCoorSitArqProy" />
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label319" runat="server" SkinID="etiqueta_negra" 
                Text="Descripción"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtDescSitArqProy" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator177" runat="server" 
                ErrorMessage="Ingrese la Descripción"
                    Display="Dynamic" ControlToValidate="txtDescSitArqProy"
                    ValidationGroup="SitArqProy">*</asp:RequiredFieldValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarSitArqProy" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="SitArqProy" OnClick="btnAgregarSitArqProy_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarSitArqProy" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarSitArqProy_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary57" runat="server" 
                ValidationGroup="SitArqProy" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvSitArqProy" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de Sitios Arqueológicos Rescatados en este Proyecto" OnRowDeleting="grvSitArqProy_RowDeleting">
                    <Columns>
                         <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EAI_ID" HeaderText="No." />
                        <asp:TemplateField HeaderText="Areas de Recarga Localización - Coordenadas planas (Datum Magna-Sirgas)">
                            <ItemTemplate>                                
                                <uc1:ctrCoordenadas ID="ctrCoorForestGrid" runat="server" DataGridObject="true" NombreTabla="EIH_COOR_SIT_ARQ" NombreCampo="ESA_ID" ValorCampo='<%# Eval("ESA_ID") %>' ValorCampo2='<%# Eval("ESA_ID") %>' />                                           
                                <asp:Label id="lblEsaId" runat="server" text='<%# Eval("ESA_ID") %>' visible="false"></asp:Label>                        
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:BoundField DataField="ESA_DESCRIPCION" HeaderText="Descripción" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
                <!-- Medio Socioeconómico - 3.3.5.5		Información de Sitios Arqueológicos con Anterioridad-->
		<tr>
            <td colspan="2" width="50%">
                3.3.6 Dimensión Política Area de Influencia Directa
            </td>
		</tr>
		<tr>
            <td colspan="2" width="50%">
                3.3.6.1  Información de Actores
            </td>
			<td width="50%" align="right" colspan="2">
                <asp:Button ID="btnNuevoActDimPol" runat="server" SkinID="boton_copia"
                    Text="Agregar Actor"
					onclick="btnNuevoActDimPol_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhActDimPol" Visible="False">
		
		<tr>
            <td width="25%">
                <asp:Label ID="Label322" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad Politico Administrativa"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboAreas10" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator134" runat="server"
                    ErrorMessage="Seleccione El Area a Asignar Valores" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboAreas10" 
                    ValidationGroup="ActDimPol">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label321" runat="server" SkinID="etiqueta_negra" 
                Text="Lideres Comunales (Nombre)"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtLidComActDimPol" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator178" runat="server" 
                ErrorMessage="Ingrese el Nombre del Lider Comunal)"
                    Display="Dynamic" ControlToValidate="txtLidComActDimPol"
                    ValidationGroup="ActDimPol">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label323" runat="server" SkinID="etiqueta_negra" 
                Text="Información para contactarlo"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtInfContacActDimPol" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator180" runat="server" 
                ErrorMessage="Ingrese la Información para contactarlo"
                    Display="Dynamic" ControlToValidate="txtInfContacActDimPol"
                    ValidationGroup="ActDimPol">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label324" runat="server" SkinID="etiqueta_negra" 
                Text="Otros (Nombre)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtOtrosActDimPol" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label325" runat="server" SkinID="etiqueta_negra" 
                Text="Información para contactarlo"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtInfContOtroActDimPol" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarActDimPol" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="ActDimPol" OnClick="btnAgregarActDimPol_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarActDimPol" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarActDimPol_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary58" runat="server" 
                ValidationGroup="ActDimPol" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvActDimPol" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado Información de Actores" OnRowDeleting="grvActDimPol_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EAI_ID" HeaderText="No." />
                        <asp:BoundField DataField="EAA_LIDER_COMUNAL" HeaderText="Lideres Comunales (Nombre)" />
                        <asp:BoundField DataField="EAA_INFO_CONTACTO" HeaderText="Información para contactarlo" />
                        <asp:BoundField DataField="EAA_OTROS_ACTORES" HeaderText="Otros (Nombre)" />
                        <asp:BoundField DataField="EAA_INFO_CONTACTO_OTROS" HeaderText="Información para contactarlo" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        
        <!-- Medio Socioeconómico - 3.3.6.2		Información de Presencia Institucional-->
		
		<tr>
            <td width="25%">
                3.3.6.2 Información de Presencia Institucional
            </td>
			<td width="75%" align="right" colspan="3">
                <asp:Button ID="btnNuevoInfPresInst" runat="server" SkinID="boton_copia"
                    Text="Agregar Información de Presencia Institucional"
					onclick="btnNuevoInfPresInst_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhInfPresInst" Visible="False">
		
		
		<tr>
            <td width="25%">
                <asp:Label ID="Label329" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad Politico Administrativa"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboAreas11" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator137" runat="server"
                    ErrorMessage="Seleccione El Area a Asignar Valores" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboAreas11" 
                    ValidationGroup="InfPresInst">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label326" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboTipoInfPresInst" runat="server" 
                    SkinID="lista_desplegable" AutoPostBack="True" OnSelectedIndexChanged="cboTipoInfPresInst_SelectedIndexChanged"></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator136" runat="server"
                    ErrorMessage="Seleccione el Tipo de Institución" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboTipoInfPresInst" 
                    ValidationGroup="InfPresInst">*</asp:CompareValidator>
            </td>
        </tr>
        <tr id="trTipoInfPre" runat="server" visible="false">
            <td width="25%">
                <asp:Label ID="Label327" runat="server" SkinID="etiqueta_negra" 
                Text="Otro"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtOtroTipoInfPresInst" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator181" runat="server" 
                ErrorMessage="Ingrese otro tipo de institución"
                    Display="Dynamic" ControlToValidate="txtOtroTipoInfPresInst"
                    ValidationGroup="InfPresInst">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label328" runat="server" SkinID="etiqueta_negra" 
                Text="Actividad"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtActInfPresInst" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator182" runat="server" 
                ErrorMessage="Ingrese la Actividad"
                    Display="Dynamic" ControlToValidate="txtActInfPresInst"
                    ValidationGroup="InfPresInst">*</asp:RequiredFieldValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarInfPresInst" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="InfPresInst" OnClick="btnAgregarInfPresInst_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarInfPresInst" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarInfPresInst_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary59" runat="server" 
                ValidationGroup="InfPresInst" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvInfPresInst" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de la Presencia Institucional" OnRowDeleting="grvInfPresInst_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />                    
                        <asp:BoundField DataField="EAI_ID" HeaderText="No." />
                        <asp:BoundField DataField="ETI_TIPO_INSTITUCION" HeaderText="Tipo" />
                        <asp:BoundField DataField="EPI_ACTIVIDAD" HeaderText="Actividad" />                        
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        
        <!-- Medio Socioeconómico - 3.3.7.1 Información de Servicios Públicos-->
		<tr>
			<td colspan="4" width="100%">
				3.3.7 Tendencias de Desarrollo Area de Influencia Directa
			</td>
		</tr>
		<tr>
            <td width="25%">
                3.3.7.1 Información de Servicios Públicos
            </td>
			<td width="75%" align="right" colspan="3">
                <asp:Button ID="btnNuevoInfServPub" runat="server" SkinID="boton_copia"
                    Text="Agregar Información de Servicios Públicos"
					onclick="btnNuevoInfServPub_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhInfServPub" Visible="False">
		
		<tr>
            <td width="25%">
                <asp:Label ID="Label334" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad Politico Administrativa"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboAreas12" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator142" runat="server"
                    ErrorMessage="Seleccione El Area a Asignar Valores" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboAreas12" 
                    ValidationGroup="InfServPub">*</asp:CompareValidator>
            </td>
        </tr>
		<tr>
            <td width="25%">
                <asp:Label ID="Label330" runat="server" SkinID="etiqueta_negra" 
                Text="Acueducto (%) (del total de ingresos)"></asp:Label>
            </td>
            <td width="75%" colspan ="3"> 
                <asp:TextBox ID="txtPorcAcuedInfServPub" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator179" runat="server" 
                ErrorMessage="Ingrese porcentaje de Acueducto"
                    Display="Dynamic" ControlToValidate="txtPorcAcuedInfServPub"
                    ValidationGroup="InfServPub">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator138" runat="server"
                    ErrorMessage="El porcentaje de Acueducto debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtPorcAcuedInfServPub" 
                    ValidationGroup="InfServPub">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label331" runat="server" SkinID="etiqueta_negra" 
                Text="Alcantarillado (%) (del total de ingresos)"></asp:Label>
            </td>
            <td width="75%" colspan ="3"> 
                <asp:TextBox ID="txtPorcAlcantInfServPub" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator183" runat="server" 
                ErrorMessage="Ingrese porcentaje de Alcantarillado"
                    Display="Dynamic" ControlToValidate="txtPorcAlcantInfServPub"
                    ValidationGroup="InfServPub">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator139" runat="server"
                    ErrorMessage="El porcentaje de Alcantarillado debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtPorcAlcantInfServPub" 
                    ValidationGroup="InfServPub">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label332" runat="server" SkinID="etiqueta_negra" 
                Text="Electrificación (%) (del total de ingresos)"></asp:Label>
            </td>
            <td width="75%" colspan ="3"> 
                <asp:TextBox ID="txtPorcElectInfServPub" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator187" runat="server" 
                ErrorMessage="Ingrese porcentaje de Electrificación"
                    Display="Dynamic" ControlToValidate="txtPorcElectInfServPub"
                    ValidationGroup="InfServPub">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator140" runat="server"
                    ErrorMessage="El porcentaje de Electrificación debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtPorcElectInfServPub" 
                    ValidationGroup="InfServPub">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label333" runat="server" SkinID="etiqueta_negra" 
                Text="Manejo Residuos Sólidos (%) (del total de ingresos)"></asp:Label>
            </td>
            <td width="75%" colspan ="3"> 
                <asp:TextBox ID="txtPorcReSolInfServPub" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator188" runat="server" 
                ErrorMessage="Ingrese porcentaje de Manejo Residuos Sólidos"
                    Display="Dynamic" ControlToValidate="txtPorcReSolInfServPub"
                    ValidationGroup="InfServPub">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator141" runat="server"
                    ErrorMessage="El porcentaje de Manejo Residuos Sólidos debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtPorcReSolInfServPub" 
                    ValidationGroup="InfServPub">*</asp:CompareValidator>
            </td>
        </tr>
        
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarInfServPub" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="InfServPub" OnClick="btnAgregarInfServPub_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarInfServPub" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarInfServPub_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary60" runat="server" 
                ValidationGroup="InfServPub" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvInfServPub" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de Servicios Públicos" OnRowDeleting="grvInfServPub_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EAI_ID" HeaderText="No." />
                        <asp:BoundField DataField="ESP_PORC_ACUEDUCTO" HeaderText="Acueducto (%) (del total de ingresos)" />
                        <asp:BoundField DataField="ESP_PORC_ALCANTARILLADO" HeaderText="Alcantarillado (%) (del total de ingresos)" />
                        <asp:BoundField DataField="ESP_PORC_ELECTRIFICACION" HeaderText="Electrificación (%) (del total de ingresos)" />
                        <asp:BoundField DataField="ESP_PORC_MAN_RES_SOLIDOS" HeaderText="Manejo Residuos Sólidos (%) (del total de ingresos)" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        
               <!-- Medio Socioeconómico - 3.3.7.2		Información de Servicios Sociales-->
		<tr>
            <td colspan="2" width="50%">
                3.3.7.2 Información de Servicios Sociales
            </td>
			<td width="50%" align="right" colspan="2">
                <asp:Button ID="btnNuevoInfServSoc" runat="server" SkinID="boton_copia"
                    Text="Agregar Información de Servicios Sociales"
					onclick="btnNuevoInfServSoc_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhInfServSoc" Visible="False">
		
		<tr>
            <td width="25%">
                <asp:Label ID="Label339" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad Politico Administrativa"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboAreas13" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator147" runat="server"
                    ErrorMessage="Seleccione El Area a Asignar Valores" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboAreas13" 
                    ValidationGroup="InfServSoc">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label335" runat="server" SkinID="etiqueta_negra" 
                Text="Vivienda (%) (del total de ingresos)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtPorcVivInfServSoc" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator184" runat="server" 
                ErrorMessage="Ingrese el Porcentaje de Vivienda"
                    Display="Dynamic" ControlToValidate="txtPorcVivInfServSoc"
                    ValidationGroup="InfServSoc">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator143" runat="server"
                    ErrorMessage="El Porcentaje de Vivienda debe ser un número"
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtPorcVivInfServSoc" 
                    ValidationGroup="InfServSoc">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label336" runat="server" SkinID="etiqueta_negra" 
                Text="Educación (%) (del total de ingresos)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtPorcEduInfServSoc" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator185" runat="server" 
                ErrorMessage="Ingrese el Porcentaje de Educación"
                    Display="Dynamic" ControlToValidate="txtPorcEduInfServSoc"
                    ValidationGroup="InfServSoc">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator144" runat="server"
                    ErrorMessage="El Porcentaje de Educación debe ser un número"
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtPorcEduInfServSoc" 
                    ValidationGroup="InfServSoc">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label337" runat="server" SkinID="etiqueta_negra" 
                Text="Salud (%) (del total de ingresos)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtPorcSalInfServSoc" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator186" runat="server" 
                ErrorMessage="Ingrese el Porcentaje de Salud"
                    Display="Dynamic" ControlToValidate="txtPorcSalInfServSoc"
                    ValidationGroup="InfServSoc">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator145" runat="server"
                    ErrorMessage="El Porcentaje de Salud debe ser un número"
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtPorcSalInfServSoc" 
                    ValidationGroup="InfServSoc">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label338" runat="server" SkinID="etiqueta_negra" 
                Text="Otros (%) (del total de ingresos)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtPorcOtrosInfServSoc" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator189" runat="server" 
                ErrorMessage="Ingrese el Porcentaje de Otros"
                    Display="Dynamic" ControlToValidate="txtPorcOtrosInfServSoc"
                    ValidationGroup="InfServSoc">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator146" runat="server"
                    ErrorMessage="El Porcentaje de Otros debe ser un número"
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtPorcOtrosInfServSoc" 
                    ValidationGroup="InfServSoc">*</asp:CompareValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarInfServSoc" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="InfServSoc" OnClick="btnAgregarInfServSoc_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarInfServSoc" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarInfServSoc_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary61" runat="server" 
                ValidationGroup="InfServSoc" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvInfServSoc" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de Servicios Sociales" OnRowDeleting="grvInfServSoc_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EAI_ID" HeaderText="No." />
                        <asp:BoundField DataField="ESS_PORC_VIVIENDA" HeaderText="Vivienda (%) (del total de ingresos)" />
                        <asp:BoundField DataField="ESS_PORC_EDUCACION" HeaderText="Educación (%) (del total de ingresos)" />
                        <asp:BoundField DataField="ESS_PORC_SALUD" HeaderText="Salud (%) (del total de ingresos)" />
                        <asp:BoundField DataField="ESS_PORC_OTROS" HeaderText="Otros (%) (del total de ingresos)" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        
               <!-- Medio Socioeconómico - 3.3.7.2		Información de Servicios Sociales-->
		<tr>
            <td width="25%">
                3.3.7.3	Información Economíca y Cultural
            </td>
			<td width="75%" align="right" colspan="3">
                <asp:Button ID="btnNuevoInfEcoCul" runat="server" SkinID="boton_copia"
                    Text="Agregar Información Económica y Cultural"
					onclick="btnNuevoInfEcoCul_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhInfEcoCul" Visible="False">
		
		<tr>
            <td width="25%">
                <asp:Label ID="Label348" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad Politico Administrativa"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboAreas14" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator153" runat="server"
                    ErrorMessage="Seleccione El Area a Asignar Valores" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboAreas14" 
                    ValidationGroup="InfEcoCul">*</asp:CompareValidator>
            </td>
        </tr>
		<tr>
            <td colspan="4" width="100%">
                Información Económica
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label340" runat="server" SkinID="etiqueta_negra" 
                Text="Primer Orden (%) (del total de ingresos)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtEcoPriInfEcoCul" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator190" runat="server" 
                ErrorMessage="Ingrese el porcentaje de Primer Orden"
                    Display="Dynamic" ControlToValidate="txtEcoPriInfEcoCul"
                    ValidationGroup="InfEcoCul">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator148" runat="server"
                    ErrorMessage="El porcentaje de Primer Orden debe ser un número"
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtEcoPriInfEcoCul" 
                    ValidationGroup="InfEcoCul">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label344" runat="server" SkinID="etiqueta_negra" 
                Text="Segundo Orden (%) (del total de ingresos)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtEcoSegInfEcoCul" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator191" runat="server" 
                ErrorMessage="Ingrese el porcentaje de Segundo Orden"
                    Display="Dynamic" ControlToValidate="txtEcoSegInfEcoCul"
                    ValidationGroup="InfEcoCul">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator149" runat="server"
                    ErrorMessage="El porcentaje de Segundo Orden debe ser un número"
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtEcoSegInfEcoCul" 
                    ValidationGroup="InfEcoCul">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label345" runat="server" SkinID="etiqueta_negra" 
                Text="Tercer Orden (%) (del total de ingresos)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtEcoTerInfEcoCul" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator195" runat="server" 
                ErrorMessage="Ingrese el porcentaje de Tercer Orden"
                    Display="Dynamic" ControlToValidate="txtEcoTerInfEcoCul"
                    ValidationGroup="InfEcoCul">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator150" runat="server"
                    ErrorMessage="El porcentaje de Tercer Orden debe ser un número"
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtEcoTerInfEcoCul" 
                    ValidationGroup="InfEcoCul">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                Información Cultural
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label346" runat="server" SkinID="etiqueta_negra" 
                Text="Sociales (%) (del total de ingresos)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtCulSocInfEcoCul" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator196" runat="server" 
                ErrorMessage="Ingrese el porcentaje de Sociales"
                    Display="Dynamic" ControlToValidate="txtCulSocInfEcoCul"
                    ValidationGroup="InfEcoCul">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator151" runat="server"
                    ErrorMessage="El porcentaje de Sociales debe ser un número"
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtCulSocInfEcoCul" 
                    ValidationGroup="InfEcoCul">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label347" runat="server" SkinID="etiqueta_negra" 
                Text="Patrimonio (%) (del total de ingresos)"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtCulPatInfEcoCul" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator197" runat="server" 
                ErrorMessage="Ingrese el porcentaje de Patrimonio"
                    Display="Dynamic" ControlToValidate="txtCulPatInfEcoCul"
                    ValidationGroup="InfEcoCul">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator152" runat="server"
                    ErrorMessage="El porcentaje de Patrimonio debe ser un número"
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtCulPatInfEcoCul" 
                    ValidationGroup="InfEcoCul">*</asp:CompareValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarInfEcoCul" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="InfEcoCul" OnClick="btnAgregarInfEcoCul_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarInfEcoCul" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarInfEcoCul_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary62" runat="server" 
                ValidationGroup="InfEcoCul" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvInfEcoCul" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado Información Economíca y Culturales" OnRowDeleting="grvInfEcoCul_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EAI_ID" HeaderText="No." />
                        <asp:BoundField DataField="EEC_PORC_PRIMER_ORDEN" HeaderText="Económica Primer Orden (%) (del total de ingresos)" />
                        <asp:BoundField DataField="EEC_PORC_SEGUNDO_ORDEN" HeaderText="Económica Segundo Orden (%) (del total de ingresos)" />
                        <asp:BoundField DataField="EEC_PORC_TERCERO_ORDEN" HeaderText="Económica Tercer Orden (%) (del total de ingresos)" />
                        <asp:BoundField DataField="EEC_PORC_SOCIALES" HeaderText="Cultural Sociales Orden (%) (del total de ingresos)" />
                        <asp:BoundField DataField="EEC_PORC_PATRIMONIO" HeaderText="Patrimonio Sociales Orden (%) (del total de ingresos)" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
              <!-- Medio Socioeconómico - 3.3.7.2		Información de Servicios Sociales-->
		<tr>
            <td width="25%">
                3.3.7.4 Información de Organización Comunitaria, de Funcionamiento y de Indicador de Desempeño Municipal
            </td>
			<td width="75%" align="right" colspan="3">
                <asp:Button ID="btnNuevoInfOrgCom" runat="server" SkinID="boton_copia"
                    Text="Agregar Información de Organización Comunitaria"
					onclick="btnNuevoInfOrgCom_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhInfOrgCom" Visible="False">
		
		<tr>
            <td width="25%">
                <asp:Label ID="Label352" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad Politico Administrativa"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboAreas15" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator157" runat="server"
                    ErrorMessage="Seleccione El Area a Asignar Valores" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboAreas15" 
                    ValidationGroup="InfOrgCom">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label349" runat="server" SkinID="etiqueta_negra" 
                Text="Inf. Org. Comunitaria: Capacitaciones (%) (del total de ingresos)"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtCapaInfOrgCom" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator192" runat="server" 
                ErrorMessage="Ingrese el porcentaje de Capacitaciones"
                    Display="Dynamic" ControlToValidate="txtCapaInfOrgCom"
                    ValidationGroup="InfOrgCom">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator154" runat="server"
                    ErrorMessage="El porcentaje de Capacitaciones debe ser un número"
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtCapaInfOrgCom" 
                    ValidationGroup="InfOrgCom">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label350" runat="server" SkinID="etiqueta_negra" 
                Text="Inf. Funcionamiento: Gastos Func. (%) (del total de ingresos)"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtFuncInfOrgCom" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator193" runat="server" 
                ErrorMessage="Ingrese el porcentaje de Gastos de Funcionamiento"
                    Display="Dynamic" ControlToValidate="txtFuncInfOrgCom"
                    ValidationGroup="InfOrgCom">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator155" runat="server"
                    ErrorMessage="El porcentaje de Gastos de Funcionamiento debe ser un número"
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtFuncInfOrgCom" 
                    ValidationGroup="InfOrgCom">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label351" runat="server" SkinID="etiqueta_negra" 
                Text="No. Indicador de Desempeño Municipal"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtIndDesmMun" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator200" runat="server" 
                ErrorMessage="Ingrese el No. Indicador de Desempeño Municipal"
                    Display="Dynamic" ControlToValidate="txtFuncInfOrgCom"
                    ValidationGroup="InfOrgCom">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator156" runat="server"
                    ErrorMessage="El No. Indicador de Desempeño Municipal debe ser un número"
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtFuncInfOrgCom" 
                    ValidationGroup="InfOrgCom">*</asp:CompareValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarInfOrgCom" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="InfOrgCom" OnClick="btnAgregarInfOrgCom_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarInfOrgCom" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarInfOrgCom_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary63" runat="server" 
                ValidationGroup="InfOrgCom" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvInfOrgCom" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado Información de Organización Comunitaria, de Funcionamiento y de Indicador de Desempeño Municipal" OnRowDeleting="grvInfOrgCom_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EAI_ID" HeaderText="No." />
                        <asp:BoundField DataField="EOC_PORC_ORG_COMUNITARIA" HeaderText="Inf. Org. Comunitaria:Capacitaciones (%) (del total de ingresos)" />
                        <asp:BoundField DataField="EOC_PORC_GAST_FUNCIONAMI" HeaderText="Inf. Funcionamiento:Gastos Func. (%) (del total de ingresos)" />
                        <asp:BoundField DataField="EOC_INDICADOR_DESP_MUNIC" HeaderText="No. Indicador de Desempeño Municipal" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        
        <tr>
            <td colspan="4" width="100%">
                3.4 Zonificación Ambiental
            </td>
        </tr>
        <!-- Zonificación Ambiental - 3.4.1 Medio Abiótico (Físico)-->
		<tr>
            <td width="25%">
                3.4.1 Medio Abiótico (Físico)
            </td>
			<td width="75%" align="right" colspan="3">
                <asp:Button ID="btnNuevoUnidZonFis" runat="server" SkinID="boton_copia"
                    Text="Agregar Unidades de Zonificación Físicas"
					onclick="btnNuevoUnidZonFis_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhUnidZonFis" Visible="False">
		
		<tr>
            <td width="25%">
                <asp:Label ID="Label353" runat="server" SkinID="etiqueta_negra" 
                Text="Código en el mapa:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtCodMapaUnidZonFis" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Colocar el código con que se identifica la Unidad en el Mapa respectivo"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator194" runat="server" 
                ErrorMessage="Ingrese el código en el mapa"
                    Display="Dynamic" ControlToValidate="txtCodMapaUnidZonFis"
                    ValidationGroup="UnidZonFis">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label354" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Unidad"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtTipoUnidZonFis" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator198" runat="server" 
                ErrorMessage="Ingrese el Tipo de Unidad"
                    Display="Dynamic" ControlToValidate="txtTipoUnidZonFis"
                    ValidationGroup="UnidZonFis">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label355" runat="server" SkinID="etiqueta_negra" 
                Text="Descripción Básica de la Unidad"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtDescUnidZonFis" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="Con base en las variables describir cada unidad"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator199" runat="server" 
                ErrorMessage="Ingrese la descripción Básica de la Unidad"
                    Display="Dynamic" ControlToValidate="txtDescUnidZonFis"
                    ValidationGroup="UnidZonFis">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label356" runat="server" SkinID="etiqueta_negra" 
                Text="Criterios Considerados para Establecer la Unidad "></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtCritUnidZonFis" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator201" runat="server" 
                ErrorMessage="Ingrese los Criterios Considerados para Establecer la Unidad"
                    Display="Dynamic" ControlToValidate="txtCritUnidZonFis"
                    ValidationGroup="UnidZonFis">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label357" runat="server" SkinID="etiqueta_negra" 
                Text="Área (Ha) del Area de Estudio"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtAreaUnidZonFis" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator202" runat="server" 
                ErrorMessage="Ingrese el área del Area de Estudio"
                    Display="Dynamic" ControlToValidate="txtAreaUnidZonFis"
                    ValidationGroup="UnidZonFis">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator158" runat="server"
                    ErrorMessage="El área del Area de Estudio debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtAreaUnidZonFis" 
                    ValidationGroup="UnidZonFis">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label358" runat="server" SkinID="etiqueta_negra" 
                Text="% del Área Total de Estudio"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtPorcAreaUnidZonFis" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator203" runat="server" 
                ErrorMessage="Ingrese el porcentaje del Area de Estudio"
                    Display="Dynamic" ControlToValidate="txtPorcAreaUnidZonFis"
                    ValidationGroup="UnidZonFis">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator159" runat="server"
                    ErrorMessage="El porcentaje del Area de Estudio debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtPorcAreaUnidZonFis" 
                    ValidationGroup="UnidZonFis">*</asp:CompareValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarUnidZonFis" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="UnidZonFis" OnClick="btnAgregarUnidZonFis_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarUnidZonFis" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarUnidZonFis_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary64" runat="server" 
                ValidationGroup="UnidZonFis" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvUnidZonFis" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de Unidades de Zonificación Físicas" OnRowDeleting="grvUnidZonFis_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EZF_COD_MAPA" HeaderText="Codigo del Mapa" />
                        <asp:BoundField DataField="EZF_TIPO_UNIDAD" HeaderText="Tipo de Unidad" />
                        <asp:BoundField DataField="EZF_DESCRIPCION" HeaderText="Descripción Básica de la Unidad" />
                        <asp:BoundField DataField="EZF_CRITERIOS" HeaderText="Criterios Considerados para Establecer la Unidad " />
                        <asp:BoundField DataField="EZF_AREA_AREA_ESTUDIO" HeaderText="Área (Ha) del Area de Estudio" />
                        <asp:BoundField DataField="EZF_PORC_AREA_ESTUDIO" HeaderText="Área (Ha) del Area de Estudio" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        
        <!-- Zonificación Ambiental - 3.4.2 Medio biótico-->
		<tr>
            <td width="25%">
                3.4.2 Medio biótico
            </td>
			<td width="75%" align="right" colspan="3">
                <asp:Button ID="btnNuevoUnidZonBio" runat="server" SkinID="boton_copia"
                    Text="Agregar Unidades de Zonificación Bióticas"
					onclick="btnNuevoUnidZonBio_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhUnidZonBio" Visible="False">
		
		<tr>
            <td width="25%">
                <asp:Label ID="Label359" runat="server" SkinID="etiqueta_negra" 
                Text="Código en el mapa:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtCodMapaUnidZonBio" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Colocar el código con que se identifica la Unidad en el Mapa respectivo"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator204" runat="server" 
                ErrorMessage="Ingrese el código en el mapa"
                    Display="Dynamic" ControlToValidate="txtCodMapaUnidZonBio"
                    ValidationGroup="UnidZonBio">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label360" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Unidad"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtTipoUnidZonBio" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator205" runat="server" 
                ErrorMessage="Ingrese el Tipo de Unidad"
                    Display="Dynamic" ControlToValidate="txtTipoUnidZonBio"
                    ValidationGroup="UnidZonBio">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label361" runat="server" SkinID="etiqueta_negra" 
                Text="Descripción Básica de la Unidad"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtDescUnidZonBio" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="Con base en las variables describir cada unidad"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator206" runat="server" 
                ErrorMessage="Ingrese la descripción Básica de la Unidad"
                    Display="Dynamic" ControlToValidate="txtDescUnidZonBio"
                    ValidationGroup="UnidZonBio">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label362" runat="server" SkinID="etiqueta_negra" 
                Text="Criterios Considerados para Establecer la Unidad "></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtCritUnidZonBio" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator207" runat="server" 
                ErrorMessage="Ingrese los Criterios Considerados para Establecer la Unidad"
                    Display="Dynamic" ControlToValidate="txtCritUnidZonBio"
                    ValidationGroup="UnidZonBio">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label363" runat="server" SkinID="etiqueta_negra" 
                Text="Área (Ha) del Area de Estudio"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtAreaUnidZonBio" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator208" runat="server" 
                ErrorMessage="Ingrese el área del Area de Estudio"
                    Display="Dynamic" ControlToValidate="txtAreaUnidZonBio"
                    ValidationGroup="UnidZonBio">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator160" runat="server"
                    ErrorMessage="El área del Area de Estudio debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtAreaUnidZonBio" 
                    ValidationGroup="UnidZonBio">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label364" runat="server" SkinID="etiqueta_negra" 
                Text="% del Área Total de Estudio"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtPorcAreaUnidZonBio" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator209" runat="server" 
                ErrorMessage="Ingrese el porcentaje del Area de Estudio"
                    Display="Dynamic" ControlToValidate="txtPorcAreaUnidZonBio"
                    ValidationGroup="UnidZonBio">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator161" runat="server"
                    ErrorMessage="El porcentaje del Area de Estudio debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtPorcAreaUnidZonBio" 
                    ValidationGroup="UnidZonBio">*</asp:CompareValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarUnidZonBio" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="UnidZonBio" OnClick="btnAgregarUnidZonBio_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarUnidZonBio" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarUnidZonBio_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary65" runat="server" 
                ValidationGroup="UnidZonBio" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvUnidZonBio" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de Unidades de Zonificación Bióticas" OnRowDeleting="grvUnidZonBio_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EZB_COD_MAPA" HeaderText="Codigo del Mapa" />
                        <asp:BoundField DataField="EZB_TIPO_UNIDAD" HeaderText="Tipo de Unidad" />
                        <asp:BoundField DataField="EZB_DESCRIPCION" HeaderText="Descripción Básica de la Unidad" />
                        <asp:BoundField DataField="EZB_CRITERIOS" HeaderText="Criterios Considerados para Establecer la Unidad " />
                        <asp:BoundField DataField="EZB_AREA_AREA_ESTUDIO" HeaderText="Área (Ha) del Area de Estudio" />
                        <asp:BoundField DataField="EZB_PORC_AREA_ESTUDIO" HeaderText="Área (Ha) del Area de Estudio" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
          <!-- Zonificación Ambiental - 3.4.3 Medio Socioeconómico-->
		<tr>
            <td width="25%">
                3.4.3 Medio Socioeconómico
            </td>
			<td width="75%" align="right" colspan="3">
                <asp:Button ID="btnNuevoUnidZonSocEco" runat="server" SkinID="boton_copia"
                    Text="Agregar Unidades de Zonificación Socioeconómicas"
					onclick="btnNuevoUnidZonSocEco_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhUnidZonSocEco" Visible="False">
		
		<tr>
            <td width="25%">
                <asp:Label ID="Label365" runat="server" SkinID="etiqueta_negra" 
                Text="Código en el mapa:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtCodMapaUnidZonSocEco" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Colocar el código con que se identifica la Unidad en el Mapa respectivo"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator210" runat="server" 
                ErrorMessage="Ingrese el código en el mapa"
                    Display="Dynamic" ControlToValidate="txtCodMapaUnidZonSocEco"
                    ValidationGroup="UnidZonSocEco">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label366" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Unidad"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtTipoUnidZonSocEco" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator211" runat="server" 
                ErrorMessage="Ingrese el Tipo de Unidad"
                    Display="Dynamic" ControlToValidate="txtTipoUnidZonSocEco"
                    ValidationGroup="UnidZonSocEco">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label367" runat="server" SkinID="etiqueta_negra" 
                Text="Descripción Básica de la Unidad"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtDescUnidZonSocEco" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="Con base en las variables describir cada unidad"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator212" runat="server" 
                ErrorMessage="Ingrese la descripción Básica de la Unidad"
                    Display="Dynamic" ControlToValidate="txtDescUnidZonSocEco"
                    ValidationGroup="UnidZonSocEco">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label368" runat="server" SkinID="etiqueta_negra" 
                Text="Criterios Considerados para Establecer la Unidad "></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtCritUnidZonSocEco" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator213" runat="server" 
                ErrorMessage="Ingrese los Criterios Considerados para Establecer la Unidad"
                    Display="Dynamic" ControlToValidate="txtCritUnidZonSocEco"
                    ValidationGroup="UnidZonSocEco">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label369" runat="server" SkinID="etiqueta_negra" 
                Text="Área (Ha) del Area de Estudio"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtAreaUnidZonSocEco" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator214" runat="server" 
                ErrorMessage="Ingrese el área del Area de Estudio"
                    Display="Dynamic" ControlToValidate="txtAreaUnidZonSocEco"
                    ValidationGroup="UnidZonSocEco">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator162" runat="server"
                    ErrorMessage="El área del Area de Estudio debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtAreaUnidZonSocEco" 
                    ValidationGroup="UnidZonSocEco">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label370" runat="server" SkinID="etiqueta_negra" 
                Text="% del Área Total de Estudio"></asp:Label>
            </td>
            <td width="75%"  colspan="3">
                <asp:TextBox ID="txtPorcAreaUnidZonSocEco" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator215" runat="server" 
                ErrorMessage="Ingrese el porcentaje del Area de Estudio"
                    Display="Dynamic" ControlToValidate="txtPorcAreaUnidZonSocEco"
                    ValidationGroup="UnidZonSocEco">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator163" runat="server"
                    ErrorMessage="El porcentaje del Area de Estudio debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtPorcAreaUnidZonSocEco" 
                    ValidationGroup="UnidZonSocEco">*</asp:CompareValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarUnidZonSocEco" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="UnidZonSocEco" OnClick="btnAgregarUnidZonSocEco_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarUnidZonSocEco" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarUnidZonSocEco_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary66" runat="server" 
                ValidationGroup="UnidZonSocEco" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvUnidZonSocEco" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de Unidades de Zonificación Socioeconómicas" OnRowDeleting="grvUnidZonSocEco_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EZS_COD_MAPA" HeaderText="Codigo del Mapa" />
                        <asp:BoundField DataField="EZS_TIPO_UNIDAD" HeaderText="Tipo de Unidad" />
                        <asp:BoundField DataField="EZS_DESCRIPCION" HeaderText="Descripción Básica de la Unidad" />
                        <asp:BoundField DataField="EZS_CRITERIOS" HeaderText="Criterios Considerados para Establecer la Unidad " />
                        <asp:BoundField DataField="EZS_AREA_AREA_ESTUDIO" HeaderText="Área (Ha) del Area de Estudio" />
                        <asp:BoundField DataField="EZS_PORC_AREA_ESTUDIO" HeaderText="Área (Ha) del Area de Estudio" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
         <!-- Zonificación Ambiental - 3.4.4 Zonificación Ambiental-->
		<tr>
            <td width="25%">
                3.4.1 Zonificación Ambiental
            </td>
			<td width="75%" align="right" colspan="3">
                <asp:Button ID="btnNuevoUnidZonAmb" runat="server" SkinID="boton_copia"
                    Text="Agregar Unidades de Zonificación Ambientales"
					onclick="btnNuevoUnidZonAmb_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
		<asp:PlaceHolder runat="server" ID="plhUnidZonAmb" Visible="False">
		
		<tr>
            <td width="25%">
                <asp:Label ID="Label371" runat="server" SkinID="etiqueta_negra" 
                Text="Código en el mapa:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtCodMapaUnidZonAmb" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Colocar el código con que se identifica la Unidad en el Mapa respectivo"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator216" runat="server" 
                ErrorMessage="Ingrese el código en el mapa"
                    Display="Dynamic" ControlToValidate="txtCodMapaUnidZonAmb"
                    ValidationGroup="UnidZonAmb">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label372" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Unidad"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtTipoUnidZonAmb" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator217" runat="server" 
                ErrorMessage="Ingrese el Tipo de Unidad"
                    Display="Dynamic" ControlToValidate="txtTipoUnidZonAmb"
                    ValidationGroup="UnidZonAmb">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                Descripción Básica de la Unidad
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label373" runat="server" SkinID="etiqueta_negra" 
                Text="Medio Físico"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtMFisUnidZonAmb" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="Con base en las variables describir cada unidad"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator218" runat="server" 
                ErrorMessage="Ingrese le Medio Físico de la descripción Básica de la Unidad"
                    Display="Dynamic" ControlToValidate="txtMFisUnidZonAmb"
                    ValidationGroup="UnidZonAmb">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
             <td width="25%">
                <asp:Label ID="Label374" runat="server" SkinID="etiqueta_negra" 
                Text="Medio Biótico"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtMBioUnidZonAmb" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="Con base en las variables describir cada unidad"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator221" runat="server" 
                ErrorMessage="Ingrese le Medio Biótico de la descripción Básica de la Unidad"
                    Display="Dynamic" ControlToValidate="txtMBioUnidZonAmb"
                    ValidationGroup="UnidZonAmb">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label375" runat="server" SkinID="etiqueta_negra" 
                Text="Medio Social"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtMSocUnidZonAmb" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="Con base en las variables describir cada unidad"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator222" runat="server" 
                ErrorMessage="Ingrese le Medio Social de la descripción Básica de la Unidad"
                    Display="Dynamic" ControlToValidate="txtMSocUnidZonAmb"
                    ValidationGroup="UnidZonAmb">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label376" runat="server" SkinID="etiqueta_negra" 
                Text="Criterios Considerados para Establecer la Unidad "></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtCritUnidZonAmb" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator219" runat="server" 
                ErrorMessage="Ingrese los Criterios Considerados para Establecer la Unidad"
                    Display="Dynamic" ControlToValidate="txtCritUnidZonAmb"
                    ValidationGroup="UnidZonAmb">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label377" runat="server" SkinID="etiqueta_negra" 
                Text="Área (Ha) del Area de Estudio"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtAreaUnidZonAmb" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator220" runat="server" 
                ErrorMessage="Ingrese el área del Area de Estudio"
                    Display="Dynamic" ControlToValidate="txtAreaUnidZonAmb"
                    ValidationGroup="UnidZonAmb">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator164" runat="server"
                    ErrorMessage="El área del Area de Estudio debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtAreaUnidZonAmb" 
                    ValidationGroup="UnidZonAmb">*</asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label378" runat="server" SkinID="etiqueta_negra" 
                Text="% del Área Total de Estudio"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtPorcAreaUnidZonAmb" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator225" runat="server" 
                ErrorMessage="Ingrese el porcentaje del Area de Estudio"
                    Display="Dynamic" ControlToValidate="txtPorcAreaUnidZonAmb"
                    ValidationGroup="UnidZonAmb">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator165" runat="server"
                    ErrorMessage="El porcentaje del Area de Estudio debe ser un número" 
                    Display="Dynamic" Operator="DataTypeCheck" Type="Double"
                    ControlToValidate="txtPorcAreaUnidZonAmb" 
                    ValidationGroup="UnidZonAmb">*</asp:CompareValidator>
            </td>
        </tr>
		<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarUnidZonAmb" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="UnidZonAmb" OnClick="btnAgregarUnidZonAmb_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarUnidZonAmb" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarUnidZonAmb_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary67" runat="server" 
                ValidationGroup="UnidZonAmb" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvUnidZonAmb" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de Unidades de Zonificación Ambientales" OnRowDeleting="grvUnidZonAmb_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EZS_COD_MAPA" HeaderText="Codigo del Mapa" />
                        <asp:BoundField DataField="EZS_TIPO_UNIDAD" HeaderText="Tipo de Unidad" />
                        <asp:BoundField DataField="EZS_MEDIO_FISICO" HeaderText="Descripción Básica de la Unidad: Medio Físico" />
                        <asp:BoundField DataField="EZS_MEDIO_BIOTICO" HeaderText="Descripción Básica de la Unidad: Medio Biótico" />
                        <asp:BoundField DataField="EZS_MEDIO_SOCIAL" HeaderText="Descripción Básica de la Unidad: Medio Social" />
                        <asp:BoundField DataField="EZS_CRITERIOS" HeaderText="Criterios Considerados para Establecer la Unidad " />
                        <asp:BoundField DataField="EZS_AREA_AREA_ESTUDIO" HeaderText="Área (Ha) del Area de Estudio" />
                        <asp:BoundField DataField="EZS_PORC_AREA_ESTUDIO" HeaderText="Área (Ha) del Area de Estudio" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </tbody>
    </table>
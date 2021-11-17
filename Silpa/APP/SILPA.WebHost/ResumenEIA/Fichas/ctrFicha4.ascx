<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrFicha4.ascx.cs" Inherits="ResumenEIA_Fichas_ctrFicha4" %>
<%@ Register Src="~/ResumenEIA/Controles/ctrCoordenadasPto.ascx" tagname="ctrUbicacion" tagprefix="uc1" %>
<%@ Register Src="~/ResumenEIA/Controles/ctrCoordenadas.ascx" tagname="ctrUbicacionPoli" tagprefix="uc2" %>
<%@ Register src="../Controles/ctrPoligonos.ascx" tagname="ctrPoligonos" tagprefix="uc3" %>
<table style="width: 100%">
    <tbody>        
        <tr>
            <td colspan="4" width="100%">
                4. DEMANDA DE RECURSOS NATURALES</td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan = "4" width="100%">
                4.1 CAPTACIÓN DE AGUA</td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%"></td>
        </tr>
        <!-- Geología -->
        <tr>
            <td  style="width: 25%">
                4.1.1 Aguas Superficiales</td>
            <td width="75%" colspan = "3" align="right">
                <asp:Button ID="btnNuevosDrenajes" runat="server" SkinID="boton_copia"
                    Text="Agregar información Aguas Superficiales" OnClick="btnNuevosDrenajes_Click"/></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" style="width: 100%;"></td>
        </tr>
        
        <asp:PlaceHolder runat="server" ID="plhDrenaje" Visible="False">
        
                
        
        <tr>
            <td style="width: 25%;" colspan="">
                <asp:Label ID="Label1" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre del Drenaje:"></asp:Label></td>
            <td colspan="3">
                <asp:TextBox ID="txtNombreDrenajeAguasSup" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Nombre del Drenaje"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCodigoMapaGeologia" runat="server" 
                ErrorMessage="Ingrese el Nombre de Drenaje"
                    Display="Dynamic" ControlToValidate="txtNombreDrenajeAguasSup"
                    ValidationGroup="Drenaje">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="center" style="width: 25%; height: 26px;"></td>
            <td align="center" style="width: 57%; height: 26px;">
                <asp:Button ID="btnAgregarDrenaje" runat="server" SkinID="boton_copia"
                    Text="Agregar Drenaje" ValidationGroup="Drenaje" OnClick="btnAgregarDrenaje_Click"/>
            </td>
            <td align="center"  style="width: 25%;height: 26px">             
            </td>
            <td align="center" style="width: 25%;height: 26px"></td>
        </tr>       
        <tr>
            <td style="width: 25%">
                <asp:Label ID="Label2" runat="server" SkinID="etiqueta_negra" 
                Text="Lista de Drenajes:" ToolTip="Seleccione un Drenaje al cual se le asignara un Sitio"></asp:Label></td>
            <td colspan = "2" style="width: 50%">
                <asp:DropDownList ID="cboListaDrenajes" runat="server" SkinID="lista_desplegable" 
                Width="99%"></asp:DropDownList>
             <asp:CompareValidator ID="CompareValidator1" runat="server"
                    ErrorMessage="Seleccione el Drenaje" 
                    Display="Dynamic"
                    ControlToValidate="cboListaDrenajes" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="DrenajeFuentes">*</asp:CompareValidator>
            </td>
            <td align="right" style="width: 25%">
                <asp:Button ID="btnEliminarAguasSuper" runat="server" SkinID="boton_copia"
                    Text="Eliminar Fuente" ValidationGroup="DrenajeFuentes" OnClick="btnEliminarAguasSuper_Click"/>
            </td>
        </tr>        
        <tr>
            <td style="width: 25%">
                <asp:Label ID="Label3" runat="server" SkinID="etiqueta_negra" 
                Text="Sitio de Capacitación"></asp:Label></td>
            <td colspan = "3">
                <asp:TextBox ID="txtSitioCapacitacion" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese nombre Sitio de Capacitación"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ErrorMessage="Ingrese nombre Sitio de Capacitación"
                    Display="Dynamic" ControlToValidate="txtSitioCapacitacion"
                    ValidationGroup="DrenajeDetalles">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender2" Mask="999" runat="server" MaskType="Number" TargetControlID="txtSitioCapacitacion"  AutoComplete="false" />                                
            </td>
        </tr>
        <tr>
            <td style="width: 25%">
                <asp:Label ID="Label4" runat="server" SkinID="etiqueta_negra" 
                Text="Coordenadas de Ubicación"></asp:Label></td>
            <td style="width: 75%" colspan="3">                
                <uc1:ctrUbicacion runat="server" id="ctrUbic"></uc1:ctrUbicacion>
            </td>                
        </tr>
        <tr>
            <td style="width: 25%">
                <asp:Label ID="Label5" runat="server" SkinID="etiqueta_negra" 
                Text="Caudal Solicitado (l/s):"></asp:Label></td>
            <td style="width:75%" colspan = "3">
                <asp:TextBox ID="txtCaudalSolicitadoAguasSup" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="Ingrese Caudal Solicitado"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ErrorMessage="Ingrese Caudal Solicitado"
                    Display="Dynamic" ControlToValidate="txtCaudalSolicitadoAguasSup"
                    ValidationGroup="DrenajeDetalles" >*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="mskECaudal"  Mask="999999999999999999" runat="server" MaskType="Number" TargetControlID="txtCaudalSolicitadoAguasSup" AutoComplete="false" />                                
                    
            </td>
        </tr>
        <tr>
            <td style="width: 25%">
                <asp:Label ID="Label86" runat="server" SkinID="etiqueta_negra" 
                Text="Caudal Aforado Fuente (l/s):"></asp:Label></td>
            <td style="width: 75%" colspan = "3">
                <asp:TextBox ID="txtCaudalAforado" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="Ingrese la información Caudal Aforado"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" 
                ErrorMessage="Ingrese la información Caudal Aforado"
                    Display="Dynamic" ControlToValidate="txtCaudalAforado"
                    ValidationGroup="DrenajeDetalles">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender1" Mask="999999999999999999" runat="server" MaskType="Number" TargetControlID="txtCaudalAforado" AutoComplete="false" />                                
            </td>
        </tr>
        <tr>
            <td style="width: 25%">
                <asp:Label ID="Label87" runat="server" SkinID="etiqueta_negra" 
                Text="Metodo de Captación Previsto:"></asp:Label></td>
            <td style="width: 75%" colspan = "3">
                <asp:TextBox ID="txtMetodoCaptacionPrevisto" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="Ingrese el metodo de Captación Previsto."></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" 
                ErrorMessage="Ingrese el metodo de Captación Previsto."
                    Display="Dynamic" ControlToValidate="txtMetodoCaptacionPrevisto"
                    ValidationGroup="DrenajeDetalles">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td style="width: 25%">
                <asp:Label ID="Label88" runat="server" SkinID="etiqueta_negra" 
                Text="Usos del Agua, Aguas Abajo:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtUsosDelAgua" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="Ingrese usos del Agua."></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" 
                ErrorMessage="Ingrese Usos del Agua."
                    Display="Dynamic" ControlToValidate="txtUsosDelAgua"
                    ValidationGroup="DrenajeDetalles">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="center" style="width: 25%"></td>
            <td align="center" style="width: 25%">
                <asp:Button ID="btnAgregarDrenajeDetalles" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="DrenajeDetalles" OnClick="btnAgregarDrenajeDetalles_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarDrenajeDetalles" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarDrenajeDetalles_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>  
        
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary9" runat="server" 
                ValidationGroup="Drenaje" />
                <asp:ValidationSummary ID="ValidationSummary10" runat="server" 
                ValidationGroup="DrenajeDetalles" />
                <asp:ValidationSummary ID="ValidationSummary29" runat="server" 
                ValidationGroup="DrenajeFuentes" />
                
            </td>
        </tr>
        </asp:PlaceHolder> 
        
        <%--     Holder 1   --%> 
          
        <tr>
            <td colspan="4">
                Descripción de Fuentes de Agua
            </td>
        </tr>
        <tr>
            <td colspan="4" style="width: 100%">
                <asp:GridView runat="server" ID="grvDrenajes" AutoGenerateColumns="False" SkinID="Grilla_simple"
                width="99%"
                    EmptyDataText="No Existen Asociadas Aguas Superficiales" OnRowCommand="grvDrenajes_RowCommand">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lkbEliminarAguasSuperficiales"  ValidationGroup="ning" CommandArgument='<%# Container.DataItemIndex %>' runat="server" CommandName="Eliminar" Text="Eliminar"></asp:LinkButton>
                                <asp:Label runat="server" ID="AguasSuperficialesEasID" Visible="false" Text='<%# Eval("EAS_ID").ToString() %>'></asp:Label>
                                <asp:Label runat="server" ID="AguasSuperficialesEdsID" Visible="false" Text= '<%# Eval("EDS_ID").ToString() %>'></asp:Label>                                
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="EAS_ID" HeaderText="C&#243;digo" />
                        <asp:BoundField DataField="EAS_NOMBRE_DRENAJE" HeaderText="Nombre del Drenaje" />
                        <asp:BoundField DataField="EDS_NO_SITIO" HeaderText="Sitio de Captaci&#243;n" />
                        <asp:BoundField DataField="EDS_COOR_NORTE_UBICACION" HeaderText="Coordenadas Norte" />
                        <asp:BoundField DataField="EDS_COOR_ESTE_UBICACION" HeaderText="Coordenadas Este" />
                        <asp:BoundField DataField="EDS_CAUDAL_SOLICITADO" HeaderText="Causal Solicitado (Vs)" />
                        <asp:BoundField DataField="EDS_CAUDAL_AFOR_FUENTE" HeaderText="Causal Aforado Fuente (Vs)" />
                        <asp:BoundField DataField="EDS_METOD_CAPT_PREVISTO" HeaderText="M&#233;todo de Captaci&#243;n Previsto" />
                        <asp:BoundField DataField="EDS_USO_AGUA_AGUAS_ABAJO" HeaderText="Uso de Aguas, Aguas Abajo" />                                                
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        
        
        <tr>
            <td class="titleUpdate" colspan="4" style="width: 100%;"></td>
        </tr>  
        <tr>
            <td colspan="1"  width="25%">4.1.2 Aguas Subterráneas</td>
            <td width="75%" colspan="3" align="right">
                <asp:Button ID="btnCaptacionSubterraneos" runat="server" SkinID="boton_copia"
                    Text="Agregar Descripción de Captación Subterraneos" OnClick="btnCaptacionSubterraneos_Click" /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhAguasSubterraneas1" Visible="False">
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label6" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre del Punto de Captación:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtPuntoCaptacion" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese Descripción del Punto de Captación"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCodigoMapaGeomorfologia" runat="server" 
                ErrorMessage="Ingrese Descripción del Punto de Captación"
                    Display="Dynamic" ControlToValidate="txtPuntoCaptacion"
                    ValidationGroup="AguasSubterraneas">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label7" runat="server" SkinID="etiqueta_negra" 
                Text="Coordenadas de Ubicación (m):"></asp:Label></td>
            <td width="75%" colspan="3">
                <uc1:ctrUbicacion runat="server" id="ctrUbicAguasSub"/>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label8" runat="server" SkinID="etiqueta_negra" 
                Text="Caudal Solicitado (l/s):"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtCaudalSolicitadoAguasSub" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Caudal Solicitado"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                ErrorMessage="Ingrese Caudal Solicitado"
                    Display="Dynamic" ControlToValidate="txtCaudalSolicitadoAguasSub"
                    ValidationGroup="AguasSubterraneas">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender3" Mask="999999999999999999" runat="server" MaskType="Number" TargetControlID="txtCaudalSolicitadoAguasSub"  AutoComplete="false" />                                
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label9" runat="server" SkinID="etiqueta_negra" 
                Text="Caudal Disponible:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtCaudalDisponible" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="Caudal Disponible"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                ErrorMessage="Caudal Disponible"
                    Display="Dynamic" ControlToValidate="txtCaudalDisponible"
                    ValidationGroup="AguasSubterraneas">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender4" Mask="999999999999999999" runat="server" MaskType="Number" TargetControlID="txtCaudalDisponible" AutoComplete="false" />                                
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label10" runat="server" SkinID="etiqueta_negra" 
                Text="Metodo de Captación Previsto:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtMetCapPrevs" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="Metodos de Captacion."></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                ErrorMessage="Ingrese la información Metodo de Captación Previsto"
                    Display="Dynamic" ControlToValidate="txtMetCapPrevs"
                    ValidationGroup="AguasSubterraneas">*</asp:RequiredFieldValidator>
            </td>
        </tr>        
         <tr>
            <td width="25%">
                <asp:Label ID="Label89" runat="server" SkinID="etiqueta_negra" 
                Text="Uso del Agua Subterránea en el Área Circundante:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtUsoAguaSubter" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="Uso del Agua Subterránea en el Área Circundante "></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" 
                ErrorMessage="Ingrese la información de Uso del Agua Subterránea en el Área Circundante"
                    Display="Dynamic" ControlToValidate="txtUsoAguaSubter"
                    ValidationGroup="AguasSubterraneas">*</asp:RequiredFieldValidator>
            </td>
        </tr>        
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarAguasSubterraneas" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="AguasSubterraneas" OnClick="btnAgregarAguasSubterraneas_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarAguasSubterraneas" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarAguasSubterraneas_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary11" runat="server" 
                ValidationGroup="AguasSubterraneas" />             
            </td>
        </tr>
        </asp:PlaceHolder>  
              
        <tr>
            <td colspan="4">
                Descripción de Captación Subterraneos
            </td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvAguasSubterraneas" AutoGenerateColumns="False" SkinID="Grilla_simple"
                width="99%"
                    EmptyDataText="No Existen Asociadas Aguas Subterraneas" OnRowDeleting="grvAguasSubterraneas_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EAT_ID" HeaderText="Código" />
                        <asp:BoundField DataField="EAT_NOMBRE" HeaderText="Nombre del Punto de Captación" />
                        <asp:BoundField DataField="EAT_COOR_NORTE_UBICACION" HeaderText="Coordenadas Norte" />
                        <asp:BoundField DataField="EAT_COOR_ESTE_UBICACION" HeaderText="Coordenadas Este" />
                        <asp:BoundField DataField="EAT_CAUDAL_SOLICITADO" HeaderText="Caudal Solicitado (Vs)" />
                        <asp:BoundField DataField="EAT_CAUDAL_DISPONIBLE" HeaderText="Caudal Disponible" />
                        <asp:BoundField DataField="EAT_MET_CAPTA_PREVISTO" HeaderText="Método de capacitación Previsto" />
                        <asp:BoundField DataField="EAT_USO_AGUA_AREA_CIRC" HeaderText="Usos del Agua Subterranea en el Area Circundante" />                        
                    </Columns>
                </asp:GridView>
            </td>
        </tr>   
        
        <asp:PlaceHolder runat="server" ID="plhAguasSubterraneas2" Visible="False">
       
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label90" runat="server" SkinID="etiqueta_negra" 
                Text="SEV:"></asp:Label></td>
            <td width="25%" colspan="3">
                <asp:TextBox ID="txtSEV" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Colocar el SEV"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" 
                ErrorMessage="Ingrese el nombre SEV"
                    Display="Dynamic" ControlToValidate="txtSEV"
                    ValidationGroup="AguasSubterraneas2">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label91" runat="server" SkinID="etiqueta_negra" 
                Text="Coordenadas de Ubicacion (m):"></asp:Label></td>
            <td width="75%" colspan = "3">             
                <uc1:ctrUbicacion runat="server" id="CtrUbicacion2"/>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label92" runat="server" SkinID="etiqueta_negra" 
                Text="Azimut:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtAzimut" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Valor Azimut"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" 
                ErrorMessage="Ingrese la información Valor Azimut"
                    Display="Dynamic" ControlToValidate="txtAzimut"
                    ValidationGroup="AguasSubterraneas2">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label93" runat="server" SkinID="etiqueta_negra" 
                Text="Capa Acuifera:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtCapaAcuifera" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="Información de La Capa Acuifera"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" 
                ErrorMessage="Ingrese la información de La Capa Acuifera"
                    Display="Dynamic" ControlToValidate="txtCapaAcuifera"
                    ValidationGroup="AguasSubterraneas2">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label94" runat="server" SkinID="etiqueta_negra" 
                Text="Resistividad Capa Acuifera:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtResistividadCapaAcuifera" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="Resistividad Capa Acuifera"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" 
                ErrorMessage="Ingrese la información de Resistividad Capa Acuifera"
                    Display="Dynamic" ControlToValidate="txtResistividadCapaAcuifera"
                    ValidationGroup="AguasSubterraneas2">*</asp:RequiredFieldValidator>
            </td>
        </tr>        
         <tr>
            <td width="25%">
                <asp:Label ID="Label95" runat="server" SkinID="etiqueta_negra" 
                Text="Litología:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtLitologia" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="Litologia."></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" 
                ErrorMessage="Ingrese la información de Litologia"
                    Display="Dynamic" ControlToValidate="txtLitologia"
                    ValidationGroup="AguasSubterraneas2">*</asp:RequiredFieldValidator>
            </td>
        </tr>        
        <tr>
            <td width="25%">
                <asp:Label ID="Label96" runat="server" SkinID="etiqueta_negra" 
                Text="Espesor:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtEspesor" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"
                ToolTip="Espesor."></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" 
                ErrorMessage="Ingrese la información del Espesor"
                    Display="Dynamic" ControlToValidate="txtEspesor"
                    ValidationGroup="AguasSubterraneas2">*</asp:RequiredFieldValidator>
            </td>
        </tr>        
        <tr>
            <td align="center" width="25%" style="height: 26px"></td>
            <td align="center" width="25%" style="height: 26px">
                <asp:Button ID="btnAgregarAguasSubterraneas2" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="AguasSubterraneas2" OnClick="btnAgregarAguasSubterraneas2_Click" />
            </td>
            <td align="center" width="25%" style="height: 26px">
                <asp:Button ID="btnCancelarAguasSubterraneas2" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarAguasSubterraneas2_Click"  />
            </td>
            <td align="center" width="25%" style="height: 26px"></td>
        </tr>
        
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary12" runat="server" 
                ValidationGroup="AguasSubterraneas2" />             
            </td>
        </tr>
        </asp:PlaceHolder>
         <%--     Holder 2   --%>     
              
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvAguasSubterraneas2" AutoGenerateColumns="False" SkinID="Grilla_simple"
                width="99%"
                    EmptyDataText="No Existen Asociadas Aguas Superficiales" OnRowDeleting="grvAguasSubterraneas2_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EFS_ID" HeaderText="Código" />
                        <asp:BoundField DataField="EFS_SEV" HeaderText="SEV" />
                        <asp:BoundField DataField="EFS_COOR_NORTE_UBICACION" HeaderText="Coordenadas Norte" />
                        <asp:BoundField DataField="EFS_COOR_ESTE_UBICACION" HeaderText="Coordenadas Este" />
                        <asp:BoundField DataField="EFS_AZIMUT" HeaderText="Azimut" />
                        <asp:BoundField DataField="EFS_CAPA_ACUIFERA" HeaderText="Capa Acuífera" />
                        <asp:BoundField DataField="EFS_RESISTIVIDAD" HeaderText="Resistividad Capa Acuífera" />
                        <asp:BoundField DataField="EFS_LITOLOGIA" HeaderText="Litología" />
                        <asp:BoundField DataField="EFS_ESPESOR" HeaderText="Espesor" />                        
                    </Columns>
                </asp:GridView>
            </td>
        </tr>   
        
        
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <tr>
            <td colspan = "4" width="100%">
                4.2 Vertimientos</td>
        </tr>  
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <tr>
            <td colspan = "4" width="100%">
                4.2.1 Receptores del Vertimiento</td>
        </tr>  
         <tr>
            <td width="25%">
                <asp:Label ID="Label11" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Receptor:"></asp:Label></td>
            <td style="width: 75%">
                <asp:DropDownList ID="cboTipoReceptor" runat="server" SkinID="lista_desplegable" AutoPostBack="true"
                Width="99%" 
                ToolTip="Colocar el código con que se identifica la Unidad en el Mapa respectivo" OnSelectedIndexChanged="cboTipoReceptor_SelectedIndexChanged"></asp:DropDownList>                           
            </td>
        </tr>  
        
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        
        <tr>
            <td colspan = "4" width="100%">
                4.2.1.1 Fuentes de Agua Superficial Receptores de Vertimiento</td>
        </tr>  
         <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>  
        <tr>
            <td colspan = "2" width="50%">4.2.1.1.1 Descripción de las Fuentes de Agua Receptores de Vertimiento</td>
            <td width="50%" colspan = "2" align="right">
                <asp:Button ID="btnNuevasFuentes" runat="server" SkinID="boton_copia"
                    Text="Agregar Información" OnClick="btnNuevasFuentes_Click" /></td>
        </tr>         
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        
        <asp:PlaceHolder runat="server" ID="plhFuentesdeAguaReceptores" Visible="False">        
       
        <tr>
            <td width="25%">
                <asp:Label ID="Label16" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre del Sitio de Vertimiento:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtNombreVertimiento" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ToolTip="Nombre del Sitio Verimiento"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                ErrorMessage="Ingrese la información Nombre del Sitio de Vertimiento"
                    Display="Dynamic" ControlToValidate="txtNombreVertimiento"
                    ValidationGroup="ReceptoresVertimiento">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>            
           <td width="25%">
                <asp:Label ID="Label17" runat="server" SkinID="etiqueta_negra" 
                Text="Coordenadas de Ubicación (m):"></asp:Label></td>
            <td width="75%" colspan="3">
                <uc1:ctrUbicacion runat="server" ID="ctrUbicacionFuentes"/>
           </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label18" runat="server" SkinID="etiqueta_negra" 
                Text="Capacidad de Asimilacion:"></asp:Label></td>
             <td width="75%" colspan="3">
                <asp:TextBox ID="txtCapaAsimilacion" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ToolTip="Capacidad de Asimilación"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ErrorMessage="Ingrese la información Capacidad de Asimilación"
                    Display="Dynamic" ControlToValidate="txtCapaAsimilacion"
                    ValidationGroup="ReceptoresVertimiento">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label19" runat="server" SkinID="etiqueta_negra" 
                Text="Distancia de Mezcla:"></asp:Label></td>
             <td width="75%" colspan="3">
                <asp:TextBox ID="txtDistMezcla" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ToolTip="Distancia de Mezcla"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                ErrorMessage="Ingrese la información Distancia de Mezcla"
                    Display="Dynamic" ControlToValidate="txtDistMezcla"
                    ValidationGroup="ReceptoresVertimiento">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender5" Mask="999999999999999999" runat="server" MaskType="Number" TargetControlID="txtDistMezcla" AutoComplete="false" />                                
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label13" runat="server" SkinID="etiqueta_negra" 
                Text="Caudal de Estiaje (l/s):"></asp:Label></td>
             <td width="75%" colspan="3">
                <asp:TextBox ID="txtCaudalEstiaje" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ToolTip="Caudal de Estiaje"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                ErrorMessage="Ingrese la información Caudal de Estiaje"
                    Display="Dynamic" ControlToValidate="txtCaudalEstiaje"
                    ValidationGroup="ReceptoresVertimiento">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender6" Mask="999999999999999999" runat="server" MaskType="Number" TargetControlID="txtCaudalEstiaje" AutoComplete="false" />                                                    
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label14" runat="server" SkinID="etiqueta_negra" 
                Text="Usos del Agua Abajo del Vertimiento:"></asp:Label></td>
             <td width="75%" colspan="3">
                <asp:TextBox ID="txtUsoAguaVert" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ToolTip="Usos del Agua Abajo del Vertimiento"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" 
                ErrorMessage="Ingrese la información Usos del Agua Abajo del Vertimiento"
                    Display="Dynamic" ControlToValidate="txtUsoAguaVert"
                    ValidationGroup="ReceptoresVertimiento">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarFuentes" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="ReceptoresVertimiento" OnClick="btnAgregarFuentes_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarFuentes" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarFuentes_Click1" />
            </td>
            <td align="center" width="25%"></td>
        </tr>        
        
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary13" runat="server" 
                ValidationGroup="ReceptoresVertimiento" />             
            </td>
        </tr>
        
        </asp:PlaceHolder>   
        
        <!-- Holder 4 -->
        
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvReceptoresVertimiento" AutoGenerateColumns="False" SkinID="Grilla_simple"
                width="99%"
                    EmptyDataText="No ha agregado información de Unidades Hidrogeológicas Presentes del proyecto" OnRowDeleting="grvReceptoresVertimiento_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EFA_ID" HeaderText="Código" />
                        <asp:BoundField DataField="EFA_NOMBRE_SITIO" HeaderText="Nombre del Sitio de Vertimiento" />
                        <asp:BoundField DataField="EFA_COOR_NORTE_UBICACION" HeaderText="Coodenada Norte" />
                        <asp:BoundField DataField="EFA_COOR_ESTE_UBICACION" HeaderText="Coordenada Este" />
                        <asp:BoundField DataField="EFA_CAPACIDAD_ASIMILACION" HeaderText="Capacidad de Asimilación" />
                        <asp:BoundField DataField="EFA_DISTANCIA_MEZCLA" HeaderText="Distancia de Mezcla" />
                        <asp:BoundField DataField="EFA_CAUDAL_ESTIAJE" HeaderText="Caudal de Estiaje (l/s)" />
                        <asp:BoundField DataField="EFA_USOS_AGUA_ABAJO_VERT" HeaderText="Usos del Agua, Aguas Abajo del Vertimiento" />                        
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <!--Unidades Hidrogeológicas Presentes -->
        <!--Puntos de Agua en Área de Influencia-->
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr> 
        <tr>
            <td  width="25%" >4.2.1.1.2 Calidad Fisicoquimicas de la Fuente de Agua Receptores de Vertimiento</td>
            <td width="75%" colspan="3" align="right">
                <asp:Button ID="btnCalidadFisicoquimicas" runat="server" SkinID="boton_copia" Text="Agregar Calidad Fisicoquimicas" OnClick="btnCalidadFisicoquimicas_Click"/></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        
        <asp:PlaceHolder runat="server" ID="plhNombreLaboratorio" Visible="False">        
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label145" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre del Laboratorio que realizó el análisis:"></asp:Label></td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboNombreLaboratorioFuentes" runat="server" SkinID="lista_desplegable2"  AutoPostBack="true"
                Width="99%" 
                ToolTip="Seleccione el Laboratorio" OnSelectedIndexChanged="cboNombreLaboratorioFuentes_SelectedIndexChanged"></asp:DropDownList>    
                <asp:CompareValidator ID="CompareValidator2" runat="server"
                    ErrorMessage="Seleccione el Drenaje" 
                    Display="Dynamic"
                    ControlToValidate="cboNombreLaboratorioFuentes" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="NombreLaboratorio">*</asp:CompareValidator>            
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="lblOtroLaboratorioFuentes" runat="server" SkinID="etiqueta_negra" 
                Text="Otro:" Visible="false"></asp:Label></td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtOtroLaboratorioFuentes" runat="server" SkinID="texto_sintamano" 
                Width="99%" 
                ToolTip="Ingrese otro Laboratorio" Visible="false"></asp:TextBox>                
            </td>
        </tr>
        <tr>            
            <td colspan="4" width="100%" align="right">
               <asp:Button ID="btnAsignarLaboratorio" runat="server" SkinID="boton_copia" Text="Asignar Laboratorio Seleccionado"
                ValidationGroup="NombreLaboratorio" OnClick="btnAsignarLaboratorio_Click"></asp:Button>               
            </td>
        </tr>
        <tr>
            <td colspan="4" width="100%">             
                <asp:ValidationSummary ID="ValidationSummary32" runat="server" 
                ValidationGroup="NombreLaboratorio" />             
            </td>
        </tr>        
        
        </asp:PlaceHolder>
        
        
        <asp:PlaceHolder runat="server" ID="plhCalidadFisicoquimicas" Visible="False">
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label97" runat="server" SkinID="etiqueta_negra" 
                Text="Cantidad de Sitios:"></asp:Label></td>
            <td style="width: 75%">
                <asp:TextBox ID="txtCantidadSitios" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese la cantidad de Sitios a la cual va asignar caracteristicas."></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                ErrorMessage="Ingrese Cantidad de Sitios"
                    Display="Dynamic" ControlToValidate="txtCantidadSitios"
                    ValidationGroup="CalidadFisicoquimicas">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender7" Mask="999" runat="server" MaskType="Number" TargetControlID="txtCantidadSitios" AutoComplete="false" />                                                    
                
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label20" runat="server" SkinID="etiqueta_negra" 
                Text="Seleccione Fuente:" ></asp:Label></td>
            <td style="width: 75%">
                <asp:DropDownList ID="cboFuentes" runat="server" SkinID="lista_desplegable" 
                Width="99%" 
                ToolTip="Seleccione por lo menos uns fuente a la cual se le asignara detalles."></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator3" runat="server"
                    ErrorMessage="Seleccione la Fuente" 
                    Display="Dynamic"
                    ControlToValidate="cboFuentes" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="CalidadFisicoquimicas">*</asp:CompareValidator>    
            </td>
        </tr>   
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label98" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo De Muestra:"></asp:Label></td>
            <td style="width: 75%">
                <asp:Button ID="btnTipoMuestra" runat="server" SkinID="boton_copia" Text="Agregar"
                Width="99%" ValidationGroup="CalidadFisicoquimicas" OnClick="btnTipoMuestra_Click"></asp:Button>                               
            </td>
        </tr>            
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label99" runat="server" SkinID="etiqueta_negra" 
                Text="Fecha de Muestreo:"></asp:Label></td>
            <td style="width: 75%">
                <asp:Button ID="btnFechaMuestreo" runat="server" SkinID="boton_copia" Text="Agregar"
                Width="99%" ValidationGroup="CalidadFisicoquimicas" OnClick="btnFechaMuestreo_Click"></asp:Button>                
            </td>
        </tr>     
        <tr>
            <td width="25%">
                <asp:Label ID="Label100" runat="server" SkinID="etiqueta_negra" 
                Text="Hora de Muestreo:"></asp:Label></td>
            <td style="width: 75%">
                <asp:Button ID="btnHoraMuestreoFisicoQ" runat="server" SkinID="boton_copia" Text="Agregar"
                Width="99%" ValidationGroup="CalidadFisicoquimicas" OnClick="btnHoraMuestreoFisicoQ_Click"></asp:Button>                
            </td>
        </tr>     
        <tr>
            <td width="25%">
                <asp:Label ID="Label101" runat="server" SkinID="etiqueta_negra" 
                Text="Duración de Muestreo (h):"></asp:Label></td>
            <td style="width: 75%">
                <asp:Button ID="btnDuracionMuestreoFisicoQ" runat="server" SkinID="boton_copia" Text="Agregar"
                Width="99%" ValidationGroup="CalidadFisicoquimicas" OnClick="btnDuracionMuestreoFisicoQ_Click"></asp:Button>                
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label102" runat="server" SkinID="etiqueta_negra" 
                Text="Periodo de Muestreo:"></asp:Label></td>
            <td style="width: 75%">
                <asp:Button ID="btnPeriodoMuestreo" runat="server" SkinID="boton_copia" Text="Agregar"
                Width="99%" ValidationGroup="CalidadFisicoquimicas" OnClick="btnPeriodoMuestreo_Click"></asp:Button>                
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label103" runat="server" SkinID="etiqueta_negra" 
                Text="Caracteristicas Fisicas:"></asp:Label></td>
            <td width="50%" colspan="2">
                <asp:DropDownList ID="cboCaracteristicasFisicas" runat="server" SkinID="lista_desplegable" 
                 Width="99%" ></asp:DropDownList>    
                <asp:CompareValidator ID="CompareValidator4" runat="server"
                    ErrorMessage="Seleccione la Caracteristica Fisica" 
                    Display="Dynamic"
                    ControlToValidate="cboCaracteristicasFisicas" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="CalidadFisicoquimicas2">*</asp:CompareValidator>                        
            </td>
            <td width="25%">
                <asp:Button ID="btnAgregarCaracteristicas2" runat="server" SkinID="boton_copia" Text="Agregar"
                Width="99%" ValidationGroup="CalidadFisicoquimicas2" OnClick="btnAgregarCaracteristicas2_Click" ></asp:Button>                
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label104" runat="server" SkinID="etiqueta_negra" 
                Text="Caracteristicas Quimicas:"></asp:Label></td>
            <td style="width: 50%" colspan="2">
                <asp:DropDownList ID="cboCaracteristicasQuimicas" runat="server" SkinID="lista_desplegable" 
                Width="99%" ></asp:DropDownList>   
                <asp:CompareValidator ID="CompareValidator5" runat="server"
                    ErrorMessage="Seleccione la Caracteristica Quimica" 
                    Display="Dynamic"
                    ControlToValidate="cboCaracteristicasQuimicas" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="CalidadFisicoquimicas3">*</asp:CompareValidator>            
            </td>
            <td width="25%">
                <asp:Button ID="btnAgregarCaracteristicas3" runat="server" SkinID="boton_copia" Text="Agregar"
                Width="99%" ValidationGroup="CalidadFisicoquimicas3" OnClick="btnAgregarCaracteristicas3_Click" ></asp:Button>                
            </td>
        </tr>       
        
        <tr>
            <td width="25%">        
            <td style="width: 50%" colspan="2">                
            </td>
            <td width="25%">
                <asp:Button ID="btnCancelarFisicoQuimicas" runat="server" SkinID="boton_copia" Text="Cancelar"
                Width="99%" ValidationGroup="xxx" OnClick="btnCancelarFisicoQuimicas_Click"  ></asp:Button>                
            </td>
        </tr>       
        
        
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary14" runat="server" 
                ValidationGroup="CalidadFisicoquimicas" />             
                <asp:ValidationSummary ID="ValidationSummary15" runat="server" 
                ValidationGroup="CalidadFisicoquimicas2" />             
                <asp:ValidationSummary ID="ValidationSummary16" runat="server" 
                ValidationGroup="CalidadFisicoquimicas3" />             
            </td>
        </tr>        
        <tr>
        <td>
         <div style="visibility:hidden">
                <asp:Button ID="btnActualizaGrilla" runat="server" SkinID="texto_sintamano" Text="Agregar"
                Width="99%"  OnClick="btnActualizaGrilla_Click"></asp:Button>                
        </div>
        </td>
        </tr>
        </asp:PlaceHolder>
        <!-- Holder 5 -->
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvCalidadFisicoquimicas" SkinID="Grilla_simple"
                width="99%"
                    EmptyDataText="No ha agregado información Detalles Fuentes" OnRowCreated="grvCalidadFisicoquimicas_RowCreated" OnRowCommand="grvCalidadFisicoquimicas_RowCommand">                              
                    <Columns>    
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lkbEliminarAguasSuperficiales"  ValidationGroup="ning" CommandArgument='<%# Container.DataItemIndex %>' runat="server" CommandName="Eliminar" Text="Eliminar"></asp:LinkButton>
                                <asp:Label runat="server" ID="lblEfaId" Visible="false" Text='<%# Eval("EFA_ID").ToString() %>'></asp:Label>
                                <asp:Label runat="server" ID="lblEpcId" Visible="false" Text= '<%# Eval("EPC_ID").ToString() %>'></asp:Label>                                
                            </ItemTemplate>
                        </asp:TemplateField>                                                                    
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        
        <%--Areas de recarga--%>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <tr>
            <td colspan = "4" width="75%">4.2.1.2 Suelos Receptores de Vertimiento</td>
            
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>        
        <tr>
            <td width="25%">4.2.1.2.1 Descripción de Predios Receptores</td>            
            <td width="75%" colspan="3" align="right">
                <asp:Button ID="btnDescripcioPredios" runat="server" SkinID="boton_copia"
                    Text="Agregar información de Predios Receptores" OnClick="btnDescripcioPredios_Click"/></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        
        <asp:PlaceHolder runat="server" ID="plhPrediosReceptores" Visible="False">
             
  
        <tr>
            <td width="25%">
                <asp:Label ID="Label22" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre del Predio Receptor de vertimiento."></asp:Label>
            </td>
            <td colspan = "3" width="75%">
                <asp:TextBox ID="txtNombrePredio" runat="server" SkinID="etiqueta_negra" 
                width="99%" ToolTip="Nombre del Predio Receptor de vertimiento."></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator116" runat="server" 
                ErrorMessage="Ingrese a Información Nombre del Predio Receptor de vertimiento."
                    Display="Dynamic" ControlToValidate="txtNombrePredio"
                    ValidationGroup="Prediosreceptores">*</asp:RequiredFieldValidator>        
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label23" runat="server" SkinID="etiqueta_negra" 
                Text="Usos del Suelo."></asp:Label>
            </td>
            <td colspan = "3" width="75%">
                <asp:TextBox ID="txtUsosDelSuelo" runat="server" SkinID="Texto" 
                width="99%"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator117" runat="server" 
                ErrorMessage="Ingrese a Información Usos del Suelo"
                    Display="Dynamic" ControlToValidate="txtUsosDelSuelo"
                    ValidationGroup="Prediosreceptores">*</asp:RequiredFieldValidator>        
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label24" runat="server" SkinID="etiqueta_negra" 
                Text="Localización - Coordenadas planas (Datum Magna-Sirgas)"></asp:Label>
            </td>
            <td colspan = "3" width="75%">
                <uc2:ctrUbicacionPoli runat="server" id="ctrUbicPolSuelos" />                
            </td>
        </tr>
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" style="width: 75%">
                <asp:Button ID="btnAgregarPredios" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="Prediosreceptores" OnClick="btnAgregarPredios_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarPredios" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarPredios_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        
         <%--Holder 7--%>
             
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary17" runat="server" 
                ValidationGroup="Prediosreceptores" />                             
            </td>
        </tr>  
              
        </asp:PlaceHolder>      
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvDescPrediosReceptores" AutoGenerateColumns="False" SkinID="Grilla_simple"
                width="99%"
                    EmptyDataText="No ha agregado información Suelos receptores Vertimiento" OnRowCommand="grvDescPrediosReceptores_RowCommand">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lkbEliminarPrediosReceptores"  ValidationGroup="ning" CommandArgument='<%# Container.DataItemIndex %>' runat="server" CommandName="Eliminar" Text="Eliminar"></asp:LinkButton>
                                <asp:Label runat="server" ID="lblEpvId" Visible="false" Text='<%# Eval("EPV_ID").ToString() %>'></asp:Label>                                
                                <asp:Label runat="server" ID="lblEvtId" Visible="false" Text='<%# Eval("EVT_ID").ToString() %>'></asp:Label>                                                                
                            </ItemTemplate>
                        </asp:TemplateField>    
                        <asp:BoundField DataField="EPV_ID" HeaderText="Código" />
                        <asp:BoundField DataField="EPV_NOMBRE" HeaderText="Nombre del Predio Receptor de Vertimiento" />
                        <asp:BoundField DataField="EPV_USOS_SUELO" HeaderText="Usos del Suelo" />
                        <asp:TemplateField HeaderText="Localización - Coordenadas planas (Datum Magna-Sirgas)">                        
                            <ItemTemplate>
                                <uc2:ctrUbicacionPoli DataGridObject="true" NombreTabla="EIH_COOR_RECEP_VERT" NombreCampo="EPV_ID" ValorCampo='<%# Eval("EPV_ID") %>' ValorCampo2='<%# Eval("EPV_ID") %>' ID="cregrvSuelos" runat="server"/>
                            </ItemTemplate>
                        </asp:TemplateField>                        
                        <asp:BoundField DataField="TIPO_MUESTRA" HeaderText="Tipo de Muestra" />
                        <asp:BoundField DataField="PERIODO_CLIMATICO" HeaderText="Perido Climático de Muestreo" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <!--Areas de descarga-->
        <%--Holder 7--%>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <tr>
            <td width="25%">4.2.1.2.2 Calidad Fisicoquimica del Suelo</td>                        
            <td width="75%" colspan="3" align="right">
                <asp:Button ID="btnNuevosReceptoresVertimiento" runat="server" SkinID="boton_copia"
                    Text="Agregar información Receptor de Vertimientos" OnClick="btnNuevosReceptoresVertimiento_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhLaboratotioSuelos" Visible="False">
       
        <tr>
            <td width="25%"> <asp:Label ID="Label146" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre del Laboratorio que realizo el Analisis:"></asp:Label></td>                        
            <td width="75%"  colspan="3" >
                <asp:DropDownList ID="cboNombreLaboratorioSuelos" AutoPostBack="true"  runat="server" SkinID="lista_desplegable2" OnSelectedIndexChanged="cboNombreLaboratorioSuelos_SelectedIndexChanged"/></td>                            
              <asp:CompareValidator ID="CompareValidator8" runat="server"
                ErrorMessage="Seleccione el Laboratorio" 
                Display="Dynamic"
                ControlToValidate="cboNombreLaboratorioSuelos" 
                ValueToCompare="-1" Operator="NotEqual"
                ValidationGroup="LaboratorioSuelos">*</asp:CompareValidator></tr>             
        <tr>
            <td width="25%"><asp:Label ID="lblOtroLabSuelos" Visible="false" runat="server" SkinID="etiqueta_negra" 
                Text="Otro:"></asp:Label></td>                        
            <td width="75%" colspan="3" >
                <asp:TextBox ID="txtOtroLabSuelos" Visible="false" runat="server" SkinID="etiqueta_negra" Width="99%"/></td>                                        
        </tr>
        <tr>
            <td width="75%" colspan="3"></td>                        
            <td width="25%"  align="right">
                <asp:Button ID="btnAsignarLaboratorioSuelos" Text="Asignar Laboratorio" runat="server" 
                    SkinID="boton_copia" ValidationGroup="LaboratorioSuelos" OnClick="btnAsignarLaboratorioSuelos_Click" /></td>                                        
        </tr>        
        
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary30" runat="server" 
                ValidationGroup="LaboratorioSuelos" />                             
            </td>
        </tr>  
         </asp:PlaceHolder>
            
        <asp:PlaceHolder runat="server" ID="plhReceptoresVertimientos" Visible="False">        
        
        <tr>
            <td width="25%">
             <asp:Label ID="Label147" runat="server" SkinID="etiqueta_negra" 
                Text=" Tipo de muestra:"></asp:Label></td>
            <td style="width: 75%">
                <asp:DropDownList ID="cboTipoMuestra" runat="server"  SkinID="lista_desplegable"/></td>
                <asp:CompareValidator ID="CompareValidator10" runat="server"
                    ErrorMessage="Seleccione El Tipo de Muestra" 
                    Display="Dynamic"
                    ControlToValidate="cboTipoMuestra" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="CaracterReceptores">*</asp:CompareValidator><td width="25%"><asp:Label ID="Label148" runat="server" SkinID="etiqueta_negra" 
                Text=" Periodo Climatico de Muestra"></asp:Label></td>                        
            <td width="25%">
                <asp:DropDownList ID="cboPeridoClimatico" runat="server"  SkinID="lista_desplegable"/></td>
                <asp:CompareValidator ID="CompareValidator9" runat="server"
                    ErrorMessage="Seleccione El periodo Climatico" 
                    Display="Dynamic"
                    ControlToValidate="cboPeridoClimatico" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="CaracterReceptores">*</asp:CompareValidator></tr>        
        <tr>
            <td width="25%">
                <asp:Label ID="Label25" runat="server" SkinID="etiqueta_negra" 
                Text="Receptor de Vertimiento:"></asp:Label></td>
            <td style="width: 25%">
                <asp:DropDownList ID="cboReceptorVertimientos" runat="server" SkinID="lista_desplegable"
                Width="99%" 
                ToolTip="Receptores de vertimientos asignados Creados anteriormente."></asp:DropDownList>   
                <asp:CompareValidator ID="CompareValidator11" runat="server"
                    ErrorMessage="Seleccione El Receptor de Vertimiento" 
                    Display="Dynamic"
                    ControlToValidate="cboReceptorVertimientos" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="SeleccioneFuenteReceptores">*</asp:CompareValidator>                
                
            </td>
            <td style="width: 50%" colspan="2" align="right">
                <asp:Button ID="btnAsignarcarFuentes" Text="Asignar Caracteristicas" runat="server" 
                    SkinID="boton_copia" ValidationGroup="CaracterReceptores" OnClick="btnAsignarcarFuentes_Click"  />
            </td>
               
        </tr>     
        <tr>
            <td width="25%">
                <asp:Label ID="Label26" runat="server" SkinID="etiqueta_negra" 
                Text="Cantidad de Sitios:"></asp:Label></td>
            <td style="width: 75%">
                <asp:TextBox ID="txtCantidadSitiosSuelos" runat="server" SkinID="texto_sintamano"
                MaxLength="200" Width="99%" 
                ToolTip="Cantidad de Sitios que va ha relacionar con los predios."></asp:TextBox>                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator118" runat="server" 
                ErrorMessage="Ingrese a Información Usos del Suelo"
                    Display="Dynamic" ControlToValidate="txtCantidadSitiosSuelos"
                ValidationGroup="ReceptoresVertimientos">*</asp:RequiredFieldValidator>   
                <cc1:MaskedEditExtender ID="MaskedEditExtender8" Mask="999" runat="server" MaskType="Number" TargetControlID="txtCantidadSitiosSuelos" AutoComplete="false" />
            </td>
        </tr>    
        <tr>
            <td width="25%">
                <asp:Label ID="Label55" runat="server" SkinID="etiqueta_negra" 
                Text="Fecha de Muestreo:"></asp:Label></td>
            <td style="width: 75%">
                <asp:Button ID="txtFechadeMuestreo" runat="server" SkinID="boton_copia"  Text="Agregar"
                Width="99%" 
                ToolTip="Ingresar los datos relacionados a Fecha de Muestreo" ValidationGroup="ReceptoresVertimientos" OnClick="txtFechadeMuestreo_Click"></asp:Button>                                   
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label27" runat="server" SkinID="etiqueta_negra" 
                Text="Hora de Muestreo:"></asp:Label></td>
            <td style="width: 75%">
                <asp:Button ID="btnHoraMuestreo" runat="server" SkinID="boton_copia"  Text="Agregar"
                                Width="99%" 
                ToolTip="Ingresar los datos relacionados a la Hora de Muestreo" ValidationGroup="ReceptoresVertimientos" OnClick="btnHoraMuestreo_Click"></asp:Button>                                   
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label28" runat="server" SkinID="etiqueta_negra" 
                Text="Duración de Muestreo(h):"></asp:Label></td>
            <td style="width: 75%">
                <asp:Button ID="btnDuracionMuestreoSuelo" runat="server" SkinID="boton_copia"  Text="Agregar"
                Width="99%" 
                ToolTip="Agregar datos relacionados a la Duracion de Muestreo." ValidationGroup="ReceptoresVertimientos" OnClick="btnDuracionMuestreoSuelo_Click"></asp:Button>                
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label29" runat="server" SkinID="etiqueta_negra" 
                Text="Caracteristicas Fisicoquimicas:"></asp:Label></td>
            <td width="50%" colspan="2">
                <asp:DropDownList ID="cboCaracterisFisicoQ" runat="server" SkinID="lista_desplegable"></asp:DropDownList></td>
               <asp:CompareValidator ID="CompareValidator12" runat="server"
                    ErrorMessage="Seleccione Caracteristica FisicoQuimica" 
                    Display="Dynamic"
                    ControlToValidate="cboCaracterisFisicoQ" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="ReceptoresVertimientos2">*</asp:CompareValidator><td width="25%" align="right">
                <asp:Button ID="btnAsignarCarSuelosFisicoQ" runat="server" SkinID="boton_copia"  Text="Agregar"
                Width="99%" ValidationGroup="ReceptoresVertimientos2" OnClick="btnAsignarCarSuelosFisicoQ_Click"></asp:Button>                
            </td>
        </tr>      
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label30" runat="server" SkinID="etiqueta_negra" 
                Text="Metales:"></asp:Label></td>            
            <td width="75%" >
                <asp:Button ID="tbnAgregarMetales" runat="server" SkinID="boton_copia"  Text="Agregar"
                Width="99%"  OnClick="tbnAgregarMetales_Click"></asp:Button>                
            </td>
        </tr>      
        
        <tr>            
            <td width="25%" colspan="4" align="right">
                <asp:Button ID="btnCancelarFisicoquimicosSuelos" runat="server" SkinID="salirBtn"  Text="Cancelar" OnClick="btnCancelarFisicoquimicosSuelos_Click"  ></asp:Button>                
            </td>
        </tr>      
        
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary18" runat="server" 
                ValidationGroup="ReceptoresVertimientos" />                             
                <asp:ValidationSummary ID="ValidationSummary19" runat="server" 
                ValidationGroup="ReceptoresVertimientos2" />    
                <asp:ValidationSummary ID="ValidationSummary31" runat="server" 
                ValidationGroup="CaracterReceptores" />    
                <asp:ValidationSummary ID="ValidationSummary33" runat="server" 
                ValidationGroup="SeleccioneFuenteReceptores" />    
                
            </td>
        </tr>   
       </asp:PlaceHolder>
       <tr>
       <td>
        <div style="visibility:hidden">
                <asp:Button ID="btnActualizarGrillaSuelos" runat="server" SkinID="texto_sintamano" Text="Agregar"
                Width="99%" OnClick="btnActualizarGrillaSuelos_Click" ></asp:Button>                
        </div>
       </td>
       </tr>
       
                  
       <%--Holder 7--%>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvReceptoresVertimientos" AutoGenerateColumns="True" SkinID="Grilla_Simple"
                width="99%"
                    EmptyDataText="No ha agregado información de Suelos del proyecto" OnRowCommand="grvReceptoresVertimientos_RowCommand" OnRowCreated="grvReceptoresVertimientos_RowCreated">    
                     <Columns>    
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lkbEliminarReceptoresVertimiento"  ValidationGroup="ning" CommandArgument='<%# Container.DataItemIndex %>' runat="server" CommandName="Eliminar" Text="Eliminar"></asp:LinkButton>
                                <asp:Label runat="server" ID="lblEpvId" Visible="false" Text='<%# Eval("EPV_ID").ToString() %>'></asp:Label>
                                <asp:Label runat="server" ID="lblEpcId" Visible="false" Text= '<%# Eval("EPC_ID").ToString() %>'></asp:Label>                                
                            </ItemTemplate>
                        </asp:TemplateField>                                                                    
                    </Columns>         
                </asp:GridView>
            </td>
        </tr>
        <%--Suelos --%> <%-- Holder 7--%>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <tr>
            <td colspan = "4" width="100%">4.2.1.3 Acuiferos Receptores de Vertimiento</td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <tr>
            <td width="25%">4.2.1.3.1 Descripciones de los Acuíferos</td>
            <td colspan = "3" width="75%" align="right">
                <asp:Button ID="btnDescripcionAcuiferos" runat="server" SkinID="boton_copia"
                    Text="Agregar información Descripción Acuiferos" OnClick="btnDescripcionAcuiferos_Click" /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhInformacionAcuiferos" Visible="False">        
        <tr>
            <td width="25%">
                <asp:Label ID="Label52" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Acuifero:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:DropDownList ID="cboTipoAcuifero" runat="server" SkinID="lista_desplegable" 
                MaxLength="200" Width="99%" ToolTip="Tipo de Acuifero"></asp:DropDownList>
                 <asp:CompareValidator ID="CompareValidator13" runat="server"
                    ErrorMessage="Seleccione Tipo Acuifero" 
                    Display="Dynamic"
                    ControlToValidate="cboTipoAcuifero" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="InformacionAcuiferos">*</asp:CompareValidator>
            
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label63" runat="server" SkinID="etiqueta_negra" 
                Text="Caudal a Verter(L/s)" ToolTip="Caudal a Verter"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtCaudalVerter" runat="server" SkinID="texto_sintamano" Width="99%"> 
                </asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="Ingresela Información Caudal a verter"
                    Display="Dynamic" ControlToValidate="txtCaudalVerter"
                    ValidationGroup="InformacionAcuiferos">*</asp:RequiredFieldValidator></td>   
                <cc1:MaskedEditExtender ID="MaskedEditExtender9" Mask="9999999999999" runat="server" MaskType="Number" TargetControlID="txtCaudalVerter" AutoComplete="false" />     
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label53" runat="server" SkinID="etiqueta_negra" 
                Text="Metodo de Inyección de vertimiento:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtMetodoInyeccion" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" 
                ErrorMessage="Ingrese la información Metodo de Inyección de vertimiento"
                    Display="Dynamic" ControlToValidate="txtMetodoInyeccion"
                    ValidationGroup="InformacionAcuiferos">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label31" runat="server" SkinID="etiqueta_negra" 
                Text="Conductividad Hidráulica:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtConductividadHidraulica" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                ErrorMessage="Ingrese la información Conductividad Hidráulica"
                    Display="Dynamic" ControlToValidate="txtConductividadHidraulica"
                    ValidationGroup="InformacionAcuiferos">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender10" Mask="9999999999999" runat="server" MaskType="Number" TargetControlID="txtConductividadHidraulica" AutoComplete="false" />     
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label32" runat="server" SkinID="etiqueta_negra" 
                Text="Coeficiente de Almacenamiento:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtCoeficienteAlmacenamiento" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                ErrorMessage="Ingrese la información de Coeficiente de Almacenamiento"
                    Display="Dynamic" ControlToValidate="txtCoeficienteAlmacenamiento"
                    ValidationGroup="InformacionAcuiferos">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender11" Mask="9999999999999" runat="server" MaskType="Number" TargetControlID="txtCoeficienteAlmacenamiento" AutoComplete="false" />     
                    
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label33" runat="server" SkinID="etiqueta_negra" 
                Text="Profundidad de Inyeccion:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtProfundidadInyeccion" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                ErrorMessage="Ingrese la información de Profundidad de Inyeccion"
                    Display="Dynamic" ControlToValidate="txtProfundidadInyeccion"
                    ValidationGroup="InformacionAcuiferos">*</asp:RequiredFieldValidator>  
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label34" runat="server" SkinID="etiqueta_negra" 
                Text="Otros Usos del Acuifero:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:DropDownList ID="cboOtrosUsos" runat="server" SkinID="lista_desplegable" 
                Width="99%"></asp:DropDownList>               
                <asp:CompareValidator ID="CompareValidator14" runat="server"
                    ErrorMessage="Seleccione Otros Usos Acuifero" 
                    Display="Dynamic"
                    ControlToValidate="cboOtrosUsos" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="InformacionAcuiferos">*</asp:CompareValidator>
            </td>
        </tr>
        
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" style="width: 75%">
                <asp:Button ID="btnAgregarAcuiferos" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="InformacionAcuiferos" OnClick="btnAgregarAcuiferos_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarAcuiferos" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarAcuiferos_Click"  />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        
              
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary20" runat="server" 
                ValidationGroup="InformacionAcuiferos" />                                              
            </td>
        </tr>   
        </asp:PlaceHolder>
        <%--Suelos --%>
        
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvInformacionAcuiferos" AutoGenerateColumns="False" SkinID="Grilla_simple"
                width="99%"
                    EmptyDataText="No ha agregado información Acuifera" OnRowDeleting="grvInformacionAcuiferos_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EAV_ID" HeaderText="Codigo" />
                        <asp:BoundField DataField="TIPO_ACUIFERO" HeaderText="Tipo Acuifero" />
                        <asp:BoundField DataField="EAV_CAUDAL_VERTER" HeaderText="Caudal a Verter (L/s)" />
                        <asp:BoundField DataField="EAV_MET_VERTIMIENTO" HeaderText="Metodo de Inyeccion o Vertimiento" />
                        <asp:BoundField DataField="EAV_CONDUCT_HIDRAULICA" HeaderText="Conductividad Hidraulica" />
                        <asp:BoundField DataField="EAV_COEF_ALMACENAMIENTO" HeaderText="Coeficiente de Almacenamiento" />
                        <asp:BoundField DataField="EAV_PROF_INYECCION" HeaderText="Profundidad de Inyeccion" />
                        <asp:BoundField DataField="OTRO_USO" HeaderText="Otros Usos del Acuífero" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <%--Clima --%> <%--Holder 9--%>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <tr>
            <td colspan = "4" width="100%">4.2.2 Caracteristicas de los Vertimientos</td>            
        </tr>
           <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <tr>
            <td width="25%">4.2.2.1 Descripción de los vertimientos</td>
            <td colspan = "3" width="75%" align="right">
                <asp:Button ID="btnDescripcionVert" runat="server" SkinID="boton_copia"
                    Text="Agregar información Descripción de Vertimientos" OnClick="btnDescripcionVert_Click" /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhDescrVerti" Visible="False">        
        <tr>
            <td width="25%">
                <asp:Label ID="Label69" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad de causal"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:DropDownList ID="cboUnidadCausal" runat="server" SkinID="lista_desplegable" AutoPostBack="true" MaxLength="200" Width="99%" OnSelectedIndexChanged="cboUnidadCausal_SelectedIndexChanged" >
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator6" runat="server"
                    ErrorMessage="Seleccione la Variable" 
                    Display="Dynamic"
                    ControlToValidate="cboUnidadCausal" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="DescrVerti">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="lblTipoVertimiento" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Vertimiento:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:DropDownList ID="cboTipoVertimiento" runat="server" SkinID="lista_desplegable" AutoPostBack="true"
                MaxLength="200" Width="99%" OnSelectedIndexChanged="cboTipoVertimiento_SelectedIndexChanged" ></asp:DropDownList>               
                <asp:CompareValidator ID="CompareValidator7" runat="server"
                    ErrorMessage="Seleccione un tipo de Vertimiento" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboTipoVertimiento" 
                    ValidationGroup="DescrVerti">*</asp:CompareValidator>
            </td>
        </tr>
        <tr id="trTipoVer" runat="server" visible="false">
             <td width="25%">
                <asp:Label ID="lblOtroVertimiento" runat="server" SkinID="etiqueta_negra" 
                Text="Otro:"></asp:Label></td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtTipoVertOtro" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ></asp:TextBox>               
                <asp:RequiredFieldValidator ID="RequiredFieldValidator121" runat="server" 
                ErrorMessage="Seleccione Otros Tipos De Vertimiento"
                    Display="Dynamic" ControlToValidate="txtTipoVertOtro"
                    ValidationGroup="DescrVerti">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label65" runat="server" SkinID="etiqueta_negra" 
                Text="Caudal se Preve Vertir:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtCaudalVertir" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
                ErrorMessage="Ingrese la información de Caudal se Preve Vertir"
                    Display="Dynamic" ControlToValidate="txtCaudalVertir"
                    ValidationGroup="DescrVerti">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender12" Mask="9999999999999" runat="server" MaskType="Number" TargetControlID="txtCaudalVertir" AutoComplete="false" />     
            </td>            
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label66" runat="server" SkinID="etiqueta_negra" 
                Text="Clase de Descarga Prevista:"></asp:Label></td>
            <td width="75%" colspan = "3">
                 <asp:DropDownList ID="cboDescargaPrevista" runat="server" SkinID="lista_desplegable" 
                MaxLength="200" Width="99%" ></asp:DropDownList>               
                <asp:CompareValidator ID="CompareValidator15" runat="server"
                    ErrorMessage="Seleccione Descarga Prevista" 
                    Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                    ControlToValidate="cboDescargaPrevista" 
                    ValidationGroup="DescrVerti">*</asp:CompareValidator>          
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label67" runat="server" SkinID="etiqueta_negra" 
                Text="Coordenadas Planas (Datum Magna-Sirgas) Punto de Descarga (m)"></asp:Label></td>
            <td width="75%" colspan = "3">                
                <uc1:ctrUbicacion runat="server" ID="CoorDescargaPrevis" />
            </td>
        </tr>      
        <tr>
            <td width="25%">
                <asp:Label ID="Label68" runat="server" SkinID="etiqueta_negra" 
                Text="Descripción Punto de Descarga Previsto:"></asp:Label></td>
            <td width="75%" colspan = "3">
                <asp:TextBox ID="txtDescPuntoPrev" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" 
                ErrorMessage="Ingrese la información de Descripción Punto de Descarga Previsto"
                    Display="Dynamic" ControlToValidate="txtDescPuntoPrev"
                    ValidationGroup="DescrVerti">*</asp:RequiredFieldValidator>               
            </td>
        </tr>
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" style="width: 75%">
                <asp:Button ID="btnAgregarDescVerti" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="DescrVerti" OnClick="btnAgregarDescVerti_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarDescVerti" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarDescVerti_Click"  />
            </td>
            <td align="center" width="25%"></td>
        </tr>
<%--        Holder 10--%>
        
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary21" runat="server" 
                ValidationGroup="DescrVerti" />                                              
            </td>
        </tr>   
        
        </asp:PlaceHolder>
        
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvDescrVerti" AutoGenerateColumns="False" SkinID="Grilla_simple"
                width="99%"
                    EmptyDataText="No ha Agregado Información de Vertimientos" OnRowDeleting="grvDescrVerti_RowDeleting" >
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EDV_ID" HeaderText="Tipo de Vertimiento Codigo"  />
                        <asp:BoundField DataField="TIPO_VERT" HeaderText="Tipo de Vertimiento Nombre " />
                        <asp:BoundField DataField="EDV_CAUDAL_VERTER" HeaderText="Caudal se Preve Vertir " />
                        <asp:BoundField DataField="DESC_PREV" HeaderText="Calse de Descarga Prevista" />
                        <asp:BoundField DataField="EDV_COOR_NORTE_PTO_DESC" HeaderText="Ubicacion Norte" />
                        <asp:BoundField DataField="EDV_COOR_ESTE_PTO_DESC" HeaderText="Ubicacion Este" />
                        <asp:BoundField DataField="EDV_DESC_PTO_DESCARGA" HeaderText="Descripcion de Descarga Previsto" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <%--Clima --%>
        <%--        Holder 10--%>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <tr>
            <td width="50%" colspan="2">4.2.2.2 Tipos de Tratamientos Previstos</td>
            <td colspan = "2" width="50%" align="right">
                <asp:Button ID="btnTipoTraPrevistos" runat="server" SkinID="boton_copia"
                    Text="Agregar Información" OnClick="btnTipoTraPrevistos_Click"  /></td>
        </tr>		
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhTipoTraPrevistos" Visible="False">        
        <tr>
            <td width="25%">
                <asp:Label ID="Label70" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Vertimiento Descarga:"></asp:Label></td>
            <td width="75%" colspan = "3">
                 <asp:DropDownList ID="cboTipoVerDesc" runat="server" SkinID="texto_sintamano" 
                Width="99%" ></asp:DropDownList>  
            <asp:CompareValidator ID="CompareValidator17" runat="server"
                ErrorMessage="Seleccione Vertimiento de Descarga" 
                Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                ControlToValidate="cboTipoVerDesc" 
                ValidationGroup="TipoTraPrevistos">*</asp:CompareValidator>       
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label71" runat="server" SkinID="etiqueta_negra" 
                Text="Tratamiento:"></asp:Label></td>
            <td style="width: 75%" colspan="3">
                <asp:DropDownList ID="cboTratamientoVerDesc" runat="server" SkinID="texto_sintamano" 
                Width="99%" AutoPostBack="True" OnSelectedIndexChanged="cboTratamientoVerDesc_SelectedIndexChanged" ></asp:DropDownList>   
              <asp:CompareValidator ID="CompareValidator16" runat="server"
                ErrorMessage="Seleccione Tratamiento" 
                Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                ControlToValidate="cboTratamientoVerDesc" 
                ValidationGroup="TipoTraPrevistos">*</asp:CompareValidator>                    
            </td>             
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label37" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Tratamiento:"></asp:Label></td>
            <td colspan="3" style="width: 75%">
                <asp:DropDownList ID="cboTipoTratamientoVerDesc" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" AutoPostBack="True" OnSelectedIndexChanged="cboTipoTratamientoVerDesc_SelectedIndexChanged" ></asp:DropDownList>      
                 <asp:CompareValidator ID="CompareValidator18" runat="server"
                ErrorMessage="Seleccione Tipo de Tratamiento" 
                Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                ControlToValidate="cboTipoTratamientoVerDesc" 
                ValidationGroup="TipoTraPrevistos">*</asp:CompareValidator>                     
            </td>            
        </tr>
        <tr id="trOtroTipoTra" runat="server" visible="false">
            <td width="25%">
                <asp:Label ID="Label12" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Tratamiento:"></asp:Label></td>
            <td colspan="3" style="width: 75%">
               <asp:TextBox ID="txtOtroTipoTra" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                ErrorMessage="Ingrese la información de Otro Tipo Tratamiento"
                    Display="Dynamic" ControlToValidate="txtOtroTipoTra"
                    ValidationGroup="TipoTraPrevistos">*</asp:RequiredFieldValidator>                                
            </td>            
        </tr>                
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" style="width: 75%">
                <asp:Button ID="btnAgregarTipoTraPrevistos" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="TipoTraPrevistos" OnClick="btnAgregarTipoTraPrevistos_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarTipoTraPrevistos" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarTipoTraPrevistos_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
      <%--  Holder 11--%>
        
               
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary22" runat="server" 
                ValidationGroup="TipoTraPrevistos" />                                              
            </td>
        </tr>           
        
        </asp:PlaceHolder>        
        
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvTipoTraPrevistos" AutoGenerateColumns="True" SkinID="Grilla_simple"
                width="99%"
                    EmptyDataText="No ha agregado información de cuencas hidrográficas" OnRowCommand="grvTipoTraPrevistos_RowCommand" OnRowCreated="grvTipoTraPrevistos_RowCreated">
                    <Columns>    
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lkbEliminarReceptoresVertimiento"  ValidationGroup="ning" CommandArgument='<%# Container.DataItemIndex %>' runat="server" CommandName="Eliminar" Text="Eliminar"></asp:LinkButton>
                                <asp:Label runat="server" ID="lblEvtId" Visible="false" Text='<%# Eval("EVT_ID").ToString() %>'></asp:Label>
                                <asp:Label runat="server" ID="lblCodigo" Visible="false" Text= '<%# Eval("Codigo").ToString() %>'></asp:Label>                                
                            </ItemTemplate>
                        </asp:TemplateField>                                                                    
                    </Columns>     
                </asp:GridView>
            </td>
        </tr>
        <%--Variables Climaticas  --%>        <%--        Holder 10--%>
        
        
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>
        <tr>
            <td  width="25%">
                4.2.2.3 Calidad Fisicoquimica Esperada del Vertimiento o Descarga
            </td>
             <td colspan="3" width="75%" align="right">
                <asp:Button ID="btnCalidadFisicoQ" runat="server" SkinID="boton_copia"
                    Text="Agregar Información Calidad Fisicoquimica" OnClick="btnCalidadFisicoQ_Click"  /></td>
        </tr>   
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>             
        <asp:PlaceHolder runat="server" ID="plhCalidadFisicoQ" Visible="False">
           
        <tr>
            <td width="25%">
                <asp:Label ID="Label80" runat="server" SkinID="etiqueta_negra" 
                Text="Caracterizacion Fisica"></asp:Label>
            </td>
            <td colspan="2" width="50%">
                <asp:DropDownList ID="cboCaracterizacionFis" runat="server" SkinID="texto_sintamano" 
                Width="99%" 
                ToolTip="Seleccione por lo menus 1 caracterizacion Fisica"></asp:DropDownList>     
                <asp:CompareValidator ID="CompareValidator19" runat="server"
                ErrorMessage="Seleccione Caracterizacion Fisica" 
                Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                ControlToValidate="cboCaracterizacionFis" 
                ValidationGroup="CalidadaFisicas">*</asp:CompareValidator>     
            </td>
             <td width="25%" align="right">
                <asp:Button ID="btnAgregarCaracterizacionFisica" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="CalidadaFisicas" OnClick="btnAgregarCaracterizacionFisica_Click"/></td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label46" runat="server" SkinID="etiqueta_negra" 
                Text="Caracterizacion Quimica"></asp:Label>
            </td>
            <td colspan="2" width="50%">
                <asp:DropDownList ID="cboCaracterizacionQuim" runat="server" SkinID="texto_sintamano" 
                Width="99%" 
                ToolTip="Seleccione por lo menus 1 caracterización Quimica"></asp:DropDownList>  
                <asp:CompareValidator ID="CompareValidator20" runat="server"
                ErrorMessage="Seleccione Caracterizacion Quimicas" 
                Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                ControlToValidate="cboCaracterizacionQuim" 
                ValidationGroup="CalidadaQuimicas">*</asp:CompareValidator>                  
            </td>
             <td width="25%" align="right">
                <asp:Button ID="dbtAgregarCaracterizacionQuimica" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="CalidadaQuimicas" OnClick="dbtAgregarCaracterizacionQuimica_Click" /></td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label47" runat="server" SkinID="etiqueta_negra" 
                Text="Caracterizacion Bacteriologica"></asp:Label>
            </td>
            <td colspan="2" width="50%">
                <asp:DropDownList ID="cboCaracterizacionBacter" runat="server" SkinID="texto_sintamano" 
                 Width="99%" 
                ToolTip="Seleccione por lo menus 1 caracterización Bacteriologica"></asp:DropDownList>                
                 <asp:CompareValidator ID="CompareValidator21" runat="server"
                ErrorMessage="Seleccione Caracterizacion Quimicas" 
                Display="Dynamic" ValueToCompare="-1" Operator="NotEqual"
                ControlToValidate="cboCaracterizacionBacter" 
                ValidationGroup="CalidadaBacteriologicas">*</asp:CompareValidator> 
            </td>
             <td width="25%" align="right">
                <asp:Button ID="btnAgregarCaracterizacionBioQuim" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="CalidadaBacteriologicas" OnClick="btnAgregarCaracterizacionBioQuim_Click" /></td>
        </tr>   
          <tr>            
            <td colspan="2" width="50%">            
            </td>
             <td width="25%" align="right">
                <asp:Button ID="btnCancelar" runat="server" SkinID="boton_copia"
                    Text="Cancelar" ValidationGroup="xxx" OnClick="btnCancelar_Click"  /></td>
            <td width="25%" align="right">
            </td>
        </tr>     
        <tr>
            <td colspan="4" width="100%">                
                <asp:ValidationSummary ID="CalidadaBacteriologicas" runat="server" 
                ValidationGroup="CalidadFisicoQ3" />
            </td>
        </tr>
        
         </asp:PlaceHolder>    
         <tr>
         <td>
        <div style="visibility:hidden">
                <asp:Button ID="btnActualizaGrilla2" runat="server" SkinID="texto_sintamano" Text="Agregar"
                Width="99%" OnClick="btnActualizaGrilla2_Click"></asp:Button>                
        </div>
        </td>
        </tr>
        <%--  Holder 11--%>   
                  
   
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvCalidadFisicoQ" AutoGenerateColumns="True" SkinID="Grilla_simple"
                width="99%"
                    EmptyDataText="No ha Agregado Información Calidad Fisicoquimica" OnRowCreated="grvCalidadFisicoQ_RowCreated" OnRowCommand="grvCalidadFisicoQ_RowCommand">                    
                      <Columns>    
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lkbEliminarReceptoresVertimiento"  ValidationGroup="ning" CommandArgument='<%# Container.DataItemIndex %>' runat="server" CommandName="Eliminar" Text="Eliminar"></asp:LinkButton>
                                <asp:Label runat="server" ID="lblEvtId" Visible="false" Text='<%# Eval("EVT_ID").ToString() %>'></asp:Label>
                                <asp:Label runat="server" ID="lblEcvId" Visible="false" Text= '<%# Eval("ECV_ID").ToString() %>'></asp:Label>                                
                            </ItemTemplate>
                        </asp:TemplateField>                                                                    
                    </Columns>     
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>    
        <tr>
            <td colspan="4" width="100%">
                4.3 OCUPACIONES DE CAUCES
            </td>            
        </tr>      
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>    
        
                  
        <tr>
            <td  width="25%">
                4.3.1 Informacion General de las Fuentes
            </td>
             <td colspan="3" width="75%" align="right">
                <asp:Button ID="btnInfoCauces" runat="server" SkinID="boton_copia"
                    Text="Agregar Información General Fuentes" OnClick="btnInfoCauces_Click" /></td>
        </tr>             
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>               
        <asp:PlaceHolder runat="server" ID="plhInfoCauces" Visible="False">            
        <tr>
            <td width="25%">
                <asp:Label ID="Label48" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre del Drenaje"></asp:Label>
            </td>
            <td colspan="2" width="75%">
                <asp:TextBox ID="txtNombreDrenaje" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" 
                ErrorMessage="Ingrese el Nombre del Drenaje"
                    Display="Dynamic" ControlToValidate="txtNombreDrenaje"
                    ValidationGroup="InfoCauces">*</asp:RequiredFieldValidator>
            </td>             
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label49" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Drenaje"></asp:Label>
            </td>
            <td colspan="2" width="75%">
                <asp:TextBox ID="txtTipoDrenaje" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Colocar el código con que se identifica la Unidad en el Mapa respectivo"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator50" runat="server" 
                ErrorMessage="Ingrese el Tipo de Drenaje"
                    Display="Dynamic" ControlToValidate="txtTipoDrenaje"
                    ValidationGroup="InfoCauces">*</asp:RequiredFieldValidator>
            </td>            
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label50" runat="server" SkinID="etiqueta_negra" 
                Text="Localización - Coordenadas planas (Datum Magna-Sirgas)"></asp:Label>
            </td>
            <td colspan="3" width="75%">
               <uc2:ctrUbicacionPoli runat="Server" ID="UbicacionPoliCauces" />
            </td>            
        </tr> 
        <tr>
            <td align="center" width="25%">
            </td>
            <td align="center" style="width: 75%">
                <asp:Button ID="btnAgregarInfoCauces" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="InfoCauces" OnClick="btnAgregarInfoCauces_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarInfoCauces" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarInfoCauces_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
                ValidationGroup="InfoCauces" />
            </td>
        </tr>
        </asp:PlaceHolder>    
        <%--Variables Climaticas  --%>
    
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvInfoCauces" AutoGenerateColumns="False" SkinID="Grilla_simple"
                width="99%"
                    EmptyDataText="No ha agregado información de cuencas hidrográficas" OnRowDeleting="grvInfoCauces_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="true" />
                        <asp:BoundField DataField="EFO_ID" HeaderText="Codigo" />
                        <asp:BoundField DataField="EFO_NOMBRE_DRENAJE" HeaderText="Nombre del Drenaje" />
                        <asp:BoundField DataField="EFO_TIPO_DRENAJE" HeaderText="Tipo de Drenaje" />
                        <asp:TemplateField HeaderText="Localización - Coordenadas planas (Datum Magna-Sirgas)">
                        <ItemTemplate>
                            <uc2:ctrUbicacionPoli ID="ctrGrvUbicCauces" DataGridObject="true" NombreTabla="EIH_COOR_INFO_FUENT_OCUP" NombreCampo="EFO_ID" ValorCampo='<%# Eval("EFO_ID") %>' ValorCampo2='<%# Eval("EFO_ID") %>' runat="server"/>
                        </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        
        
      
     <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>   
        <tr>
            <td  width="25%">
                4.3.2 Ocupacion de los Causes de las Fuentes
            </td>
             <td colspan="3" width="75%" align="right">
                <asp:Button ID="btnOcupacionFuentes" runat="server" SkinID="boton_copia"
                    Text="Agregar Información " onclick="btnOcupacionFuentes_Click" /></td>
        </tr>  
        
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>  
                       
        <asp:PlaceHolder runat="server" ID="plhOcupacionFuentes" Visible="False">
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label51" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Ocupación"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList  ID="cboTipoOcupacion" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Colocar el código de Tipo de Ocupacion"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator52" runat="server" 
                ErrorMessage="Ingrese la Información Tipo de Ocupación"
                    Display="Dynamic" ControlToValidate="cboTipoOcupacion"
                    ValidationGroup="OcupacionFuentes">*</asp:RequiredFieldValidator>
            </td>             
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label54" runat="server" SkinID="etiqueta_negra" 
                Text="Información Velocidad de Socavacion"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtInfoVelocSoca" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Información Velocidad de Socavacion"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator53" runat="server" 
                ErrorMessage="Ingrese la Información Velocidad de Socavacion"
                    Display="Dynamic" ControlToValidate="txtInfoVelocSoca"
                    ValidationGroup="OcupacionFuentes">*</asp:RequiredFieldValidator>
            </td>            
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label56" runat="server" SkinID="etiqueta_negra" 
                Text="Dimensiones Longitud (m)"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtDimensionesLongitud" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Informacion Dimensiones Longitud "></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator54" runat="server" 
                ErrorMessage="Ingrese el La Informacion Dimensiones Longitud (m)"
                    Display="Dynamic" ControlToValidate="txtDimensionesLongitud"
                    ValidationGroup="OcupacionFuentes">*</asp:RequiredFieldValidator>
            </td>            
        </tr> 
        <tr>
            <td width="25%">
                <asp:Label ID="Label57" runat="server" SkinID="etiqueta_negra" 
                Text="Dimensiones Ancho (m)"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtDimensionesAncho" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Dimensiones Longitud "></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator55" runat="server" 
                ErrorMessage="Ingrese la Información Dimensiones Longitud "
                    Display="Dynamic" ControlToValidate="txtDimensionesAncho"
                    ValidationGroup="OcupacionFuentes">*</asp:RequiredFieldValidator>
            </td>            
        </tr>         
 
        <tr>
            <td width="25%">
                <asp:Label ID="Label58" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Obra"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList  ID="cboTipoObraOcupacion" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Colocar Tipo de Obra"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator56" runat="server" 
                ErrorMessage="IngreseLa Información Tipo de Obra"
                    Display="Dynamic" ControlToValidate="cboTipoObraOcupacion"
                    ValidationGroup="OcupacionFuentes">*</asp:RequiredFieldValidator>
            </td>            
        </tr> 
        <tr>
            <td width="25%">
                <asp:Label ID="Label59" runat="server" SkinID="etiqueta_negra" 
                Text="Dimensiones de la Obra Longitud (m)"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtDimensionesObra" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Dimensiones de la Obra" 
                    ></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator57" runat="server" 
                ErrorMessage="Ingrese Información Dimensiones de la Obra"
                    Display="Dynamic" ControlToValidate="txtDimensionesObra"
                    ValidationGroup="OcupacionFuentes">*</asp:RequiredFieldValidator>
            </td>            
        </tr> 
        <tr>
            <td width="25%">
                <asp:Label ID="Label149" runat="server" SkinID="etiqueta_negra" 
                Text="Dimensiones de la Obra Ancho(m)"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtDimensionObraAnho" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Dimensiones de la Obra" 
                    ></asp:TextBox>
            </td>            
        </tr> 
        <tr>
            <td width="25%">
                <asp:Label ID="Label60" runat="server" SkinID="etiqueta_negra" 
                Text="Localización - Coordenadas planas (Datum Magna-Sirgas)"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <uc2:ctrUbicacionPoli runat="server" ID="ctrUbicacionPoliOcu" />
            </td>            
        </tr> 
        <tr>
            <td align="center" width="25%">
            </td>
            <td align="center" style="width: 75%">
                <asp:Button ID="btnAgregarOcupacionFuentes" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="OcupacionFuentes" 
                    onclick="btnAgregarOcupacionFuentes_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarOcupacionFuentes" runat="server" SkinID="boton_copia"
                    Text="Cancelar" onclick="btnCancelarOcupacionFuentes_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary3" runat="server" 
                ValidationGroup="OcupacionFuentes" />
            </td>
        </tr>
     </asp:PlaceHolder>   
        <%-- Cuencas --%>
    
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvOcupacionFuentes" AutoGenerateColumns="False"
                width="99%"
                    EmptyDataText="No ha agregado información de cuencas hidrográficas" 
                    onrowdeleting="grvOcupacionFuentes_RowDeleting" 
                 
                    SkinID="Grilla_simple">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField HeaderText="Codigo" DataField="EOC_ID" />
                        <asp:BoundField HeaderText="Tipo de Ubicacion" 
                            DataField="ETO_NOMBRE_OCUPACION" />
                        <asp:BoundField HeaderText="Informacion Velocidad de Socavacion" 
                            DataField="EOC_INFO_VEL_SOC_ESTIMADA" />
                        <asp:BoundField HeaderText="Dimensiones Longitud(m)" 
                            DataField="EOC_DIMENSION_LOG" />                        
                        <asp:BoundField HeaderText="Dimensiones Ancho(m)" 
                            DataField="EOC_DIMENSION_ANCHO" />                        
                        <asp:BoundField HeaderText="Tipo de Obra" DataField="ETB_TIPO_NOMBRE" />                        
                        <asp:BoundField HeaderText="Dimensiones de la Obra Longitud(m)" 
                            DataField="EOC_DIMENSION_OBRA_LOG" />                        
                        <asp:BoundField HeaderText="Dimensiones de la Obra Ancho(m)" 
                            DataField="EOC_DIMENSION_OBRA_ANCHO" />                        
                       <asp:TemplateField HeaderText="Ubicacion - Coordenadas planas (Datum Magna-Sirgas)">
                        <ItemTemplate>
                            <uc2:ctrUbicacionPoli ID="ctrGrvOcupCauces" DataGridObject="true" NombreTabla="EIH_COOR_OCUP_CAU_FUENTES" NombreCampo="EOC_ID" ValorCampo='<%# Eval("EOC_ID") %>' ValorCampo2='<%# Eval("EOC_ID") %>' runat="server"/>
                        </ItemTemplate>
                        </asp:TemplateField>                     
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        
        
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>  
       
        <tr>
            <td colspan="4" width="100%">
                4.4 Aprovechamiento de Materiales de Construcción
            </td>             
        </tr> 
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>                 
        <tr>
            <td  width="25%">
                4.4.1 Fuentes Autorizadas
            </td>
             <td colspan="3" width="75%" align="right">
                <asp:Button ID="btnAgrefarInfoFuentesAutorizadas" runat="server" SkinID="boton_copia"
                    Text="Agregar Información Fuentes Autorizadas" 
                     onclick="btnAgrefarInfoFuentesAutorizadas_Click" /></td>
        </tr>  
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>                
        <asp:PlaceHolder runat="server" ID="plhFuentesAut" Visible="False">
       
        <tr>
            <td width="25%">
                <asp:Label ID="Label61" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre de la Fuente"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtNombreFuenteConstr" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese el nombre de la fuente autorizada"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator59" runat="server" 
                ErrorMessage="Ingrese Información Fuente Autorizada"
                    Display="Dynamic" ControlToValidate="txtNombreFuenteConstr"
                    ValidationGroup="FuentesAut">*</asp:RequiredFieldValidator>
            </td>             
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label62" runat="server" SkinID="etiqueta_negra" 
                Text="Aut. Min. Resolucion"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtAutMinResolucion" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Resolución de Autoridad Minera"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator60" runat="server" 
                ErrorMessage="Ingrese Información Resolución de Autoridad Minera"
                    Display="Dynamic" ControlToValidate="txtAutMinResolucion"
                    ValidationGroup="FuentesAut">*</asp:RequiredFieldValidator>
            </td>            
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label76" runat="server" SkinID="etiqueta_negra" 
                Text="Aut. Min. Fecha de Expedicion"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtAutMinFechaExp" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Fecha de Expedicion Autoridad Minera"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator66" runat="server" 
                ErrorMessage="Ingresar Información Fecha de Expedicion Autoridad Minera"
                    Display="Dynamic" ControlToValidate="txtAutMinFechaExp"
                    ValidationGroup="FuentesAut">*</asp:RequiredFieldValidator>
            </td>            
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label77" runat="server" SkinID="etiqueta_negra" 
                Text="Aut. Min. Vigencia"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtAutMinVigencia" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese Informacion Vigencia Autoridad Minera"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator67" runat="server" 
                ErrorMessage="Ingrese Informacion Vigencia Autoridad Minera"
                    Display="Dynamic" ControlToValidate="txtAutMinVigencia"
                    ValidationGroup="FuentesAut">*</asp:RequiredFieldValidator>
            </td>            
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label78" runat="server" SkinID="etiqueta_negra" 
                Text="Aut. Amb. Resolucion"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtAutAmbResolicion" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese Informacion Resolucion Autoridad Ambiental Resolución"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator68" runat="server" 
                ErrorMessage="Ingrese Informacion Resolucion Autoridad Ambiental Resolución"
                    Display="Dynamic" ControlToValidate="txtAutAmbResolicion"
                    ValidationGroup="FuentesAut">*</asp:RequiredFieldValidator>
            </td>            
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label79" runat="server" SkinID="etiqueta_negra" 
                Text="Aut. Amb. Fecha de Expedicion"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtAutAmbFechaExpedicion" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese Información Fecha Expedicion Auntoridad Ambiental"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator69" runat="server" 
                ErrorMessage="Ingrese Información Fecha Expedicion Auntoridad Ambiental"
                    Display="Dynamic" ControlToValidate="txtAutAmbFechaExpedicion"
                    ValidationGroup="FuentesAut">*</asp:RequiredFieldValidator>
            </td>            
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label81" runat="server" SkinID="etiqueta_negra" 
                Text="Aut. Amb. Vigencia"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtAutAmbVigencia" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese Información Vigencia"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator70" runat="server" 
                ErrorMessage="Ingrese Información Vigencia"
                    Display="Dynamic" ControlToValidate="txtAutAmbVigencia"
                    ValidationGroup="FuentesAut">*</asp:RequiredFieldValidator>
            </td>            
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label64" runat="server" SkinID="etiqueta_negra" 
                Text="Volumen (m3)"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtVolumenConstruccion" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese Información Volumen"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator61" runat="server" 
                ErrorMessage="Ingrese Información Volumen"
                    Display="Dynamic" ControlToValidate="txtVolumenConstruccion"
                    ValidationGroup="FuentesAut">*</asp:RequiredFieldValidator>
            </td>            
        </tr> 
        <tr>
            <td width="25%">
                <asp:Label ID="Label72" runat="server" SkinID="etiqueta_negra" 
                Text="Localizacion"></asp:Label>
            </td>
             <td colspan="3" width="75%">
                <uc2:ctrUbicacionPoli runat="server" ID="ctrUbicacionPoliConst" />
            </td>            
        </tr>         
        <tr>
            <td align="center" width="25%">
            </td>
            <td align="center" style="width: 75%">
                <asp:Button ID="btnAgregarFuentesAut" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="FuentesAut" 
                    onclick="btnAgregarfuentesAutorizadas_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarFuentesAut" runat="server" SkinID="boton_copia"
                    Text="Cancelar" onclick="btnCancelarfuentesAutorizadas_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary4" runat="server" 
                ValidationGroup="FuentesAut" />
            </td>
        </tr>
        </asp:PlaceHolder> 
        <%--  Holder 11--%>

        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvFuentesAut" AutoGenerateColumns="False"
                width="99%"
                    EmptyDataText="No ha agregado información de cuencas hidrográficas" 
                    onrowdeleting="grvfuentesAutorizadas_RowDeleting" SkinID="Grilla_simple">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField HeaderText="Codigo" DataField="EFM_ID" />
                        <asp:BoundField HeaderText="Nombre de la Fuente" 
                            DataField="EFM_NOMBRE_FUENTE" />
                        <asp:BoundField HeaderText="Aut. minera Resolucion" 
                            DataField="EFM_RESOL_AUTOR_MIN" />
                        <asp:BoundField HeaderText="Aut. minera Fecha" 
                            DataField="EFM_FECHA_EXP_AUTOR_MIN" />                        
                        <asp:BoundField HeaderText="Aut. minera Vigencia" 
                            DataField="EFM_VIGENCIA_AUTOR_MIN" />                        
                        <asp:BoundField HeaderText="Aut. Ambiental Resolucion" 
                            DataField="EFM_RESOL_AUTOR_AMB" />                        
                        <asp:BoundField HeaderText="Aut. ambiental Fecha de expedicion" 
                            DataField="EFM_FECHA_EXP_AUTOR_AMB" />                        
                        <asp:BoundField HeaderText="Aut. Ambiental Vigencia" 
                            DataField="EFM_VIGENCIA_AUTOR_AMB" />                        
                        <asp:BoundField DataField="EFM_VOLUMEN" HeaderText="Volumen" />
                        <asp:TemplateField HeaderText="Ubicacion - Coordenadas planas (Datum Magna-Sirgas)">
                        <ItemTemplate>
                            <uc2:ctrUbicacionPoli ID="ctrFuentesAutorizadas" DataGridObject="true" NombreTabla="EIH_COOR_FUENT_AUTO_APROV" NombreCampo="EFM_ID" ValorCampo='<%# Eval("EFM_ID") %>' ValorCampo2='<%# Eval("EFM_ID") %>' runat="server"/>
                        </ItemTemplate>
                        </asp:TemplateField>                     
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    
    <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>  
        <tr>
            <td  width="25%">
                4.4.2 Materiales de Construcción
            </td>
             <td colspan="3" width="75%" align="right">
                <asp:Button ID="btnInformacionMateriales" runat="server" SkinID="boton_copia"
                    Text="Agregar Materiales de Construcción" 
                     onclick="btnMaterialesConstruccion_Click" /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>                  
        <asp:PlaceHolder runat="server" ID="plhMaterialesConstruccion" Visible="False"> 
              
        <tr>
             <td colspan="4" width="100%">
                Aluviales o de Cartera
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label73" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad de Area"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboUnidadArea" runat="server" SkinID="texto_sintamano" 
                 Width="99%" ToolTip="Seleccione Una Unidad de Area"></asp:DropDownList>               
            </td>             
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label74" runat="server" SkinID="etiqueta_negra" 
                Text="Disponible Titulo Minero"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:CheckBox ID="chkDisponibleTitulo" runat="server"  Checked="false" 
                MaxLength="200" Width="26%" 
                ToolTip="Ingrese Información Disponible Titulo Minero"></asp:CheckBox>
                
            </td>            
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label75" runat="server" SkinID="etiqueta_negra" 
                Text="Materiales de Construccion Tipo de Fuente"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList  ID="cboMatConTip" runat="server"  
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese Información Materiales de Construccion Tipo de Fuente"></asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator65" runat="server" 
                ErrorMessage="Ingrese Información Materiales de Construccion Tipo de Fuente"
                    Display="Dynamic" ControlToValidate="cboMatConTip"
                    ValidationGroup="MaterialesConstruccion">*</asp:RequiredFieldValidator>
            </td>            
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label82" runat="server" SkinID="etiqueta_negra" 
                Text="Materiales de Construccion Volumen"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtMatConVol" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingresar Información Materiales de Construccion Volumen"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator71" runat="server" 
                ErrorMessage="Ingresar Información Materiales de Construccion Volumen"
                    Display="Dynamic" ControlToValidate="txtMatConVol"
                    ValidationGroup="MaterialesConstruccion">*</asp:RequiredFieldValidator>
            </td>            
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label83" runat="server" SkinID="etiqueta_negra" 
                Text="Area a Intervenir"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtAreaInterven" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingresar Información Area a Intervenir"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator72" runat="server" 
                ErrorMessage="Ingresar Información Area a Intervenir"
                    Display="Dynamic" ControlToValidate="txtAreaInterven"
                    ValidationGroup="MaterialesConstruccion">*</asp:RequiredFieldValidator>
            </td>            
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label105" runat="server" SkinID="etiqueta_negra" 
                Text="Localización - Coordenadas planas (Datum Magna-Sirgas) "></asp:Label>
            </td>
            <td colspan="3" width="75%">
               <uc2:ctrUbicacionPoli runat="server" ID="ctrUbicacionPoliConstr" />
            </td>            
        </tr>
        <tr>
            <td align="center" width="25%">
            </td>
            <td align="center" style="width: 75%">
                <asp:Button ID="btnAgregarMaterialesConstruccion" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="MaterialesConstruccion" 
                    onclick="btnAgregarMaterialesConstruccion_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarMaterialesConstruccion" runat="server" SkinID="boton_copia"
                    Text="Cancelar" onclick="btnCancelarMaterialesConstruccion_Click"  />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary5" runat="server" 
                ValidationGroup="MaterialesConstruccion" />
            </td>
        </tr>
        </asp:PlaceHolder>  
        <%--Cuencas --%>
    
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvMaterialesConstruccion" AutoGenerateColumns="False" SkinID="Grilla_simple"
                width="99%"
                    EmptyDataText="No ha agregado información de cuencas hidrográficas" 
                    onrowdeleting="grvMaterialesConstruccion_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField HeaderText="Codigo" DataField="EMC_ID" />
                        <asp:BoundField HeaderText="Disponible Titulo Minero" 
                            DataField="EMC_DISP_TITULO_MINERO" />
                        <asp:BoundField HeaderText="Materiales de Construccion Tipo de Fuente" 
                            DataField="EMC_TIPO_FUENTE" />
                        <asp:BoundField HeaderText="Materiales de Construccion Volumen (m3)" 
                            DataField="EMC_VOLUMEN" />                        
                        <asp:BoundField HeaderText="Area a Intervenir" 
                            DataField="EMC_AREA_INTERVENIR" />                        
                         <asp:TemplateField HeaderText="Ubicacion - Coordenadas planas (Datum Magna-Sirgas)">
                        <ItemTemplate>
                            <uc2:ctrUbicacionPoli ID="ctrGrvMaterialesCons" DataGridObject="true" NombreTabla="EIH_COOR_MAT_CONSTRUC" NombreCampo="EMC_ID" ValorCampo='<%# Eval("EMC_ID") %>' ValorCampo2='<%# Eval("EMC_ID") %>' runat="server"/>
                        </ItemTemplate>
                        </asp:TemplateField>                                                 
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr> 
   
        <%--Holder 12--%>
        
                 <tr>
            <td colspan="4" width="100%">
                4.5 Aprobechamiento Forestal
            </td>             
        </tr>           
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>  
        <tr>
            <td  width="25%">
                4.5.1 Coberturas Vegetales a Aprovechar
            </td>
             <td  colspan="3" width="75%" align="right">
                <asp:Button ID="btnCoberVeget" runat="server" SkinID="boton_copia"
                    Text="Agregar Información Coberturas Vegetales" OnClick="btnCoberVeget_Click" /></td>
        </tr> 
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>                 
        <asp:PlaceHolder runat="server" ID="plhCobertVegetales" Visible="False">
          
        <tr>
            <td width="25%">
                <asp:Label ID="Label106" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Unidad de Cobertura a Intervenir o Posible Intervencion"></asp:Label>
            </td>
             <td colspan="3" width="75%">
                <asp:TextBox ID="txtTipoCobertura" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese Informacion Tipo de Unidad de Cobertura a Intervenir o Posible Intervencion"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" 
                ErrorMessage="Ingrese Informacion Tipo de Unidad de Cobertura a Intervenir o Posible Intervencion"
                    Display="Dynamic" ControlToValidate="txtTipoCobertura"
                    ValidationGroup="CobertVegetales">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label107" runat="server" SkinID="etiqueta_negra" 
                Text="Area a intervenir (m2)"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtAreaInterv" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese Información Area a intervenir"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator74" runat="server" 
                ErrorMessage="Ingrese Información Area a intervenir"
                    Display="Dynamic" ControlToValidate="txtAreaInterv"
                    ValidationGroup="CobertVegetales">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender13" Mask="9999999999999" runat="server" MaskType="Number" TargetControlID="txtAreaInterv"  AutoComplete="false" />                                
            </td>             
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label108" runat="server" SkinID="etiqueta_negra" 
                Text="Localización - Coordenadas planas (Datum Magna-Sirgas) "></asp:Label>
            </td>
              <td colspan="3" width="75%">
                  <uc3:ctrPoligonos ID="ctrPoligonos1" runat="server" />
            </td>
            
        </tr>             
        <tr>
            <td align="center" width="25%">
            </td>
            <td align="center" style="width: 75%">
                <asp:Button ID="btnAgregarCobertVegetales" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="CobertVegetales" OnClick="btnAgregarCobertVegetales_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarCobertVegetales" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarCobertVegetales_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary6" runat="server" 
                ValidationGroup="CobertVegetales" />
            </td>
        </tr>
         </asp:PlaceHolder>     
        <%--Calidad de agua - fuentes superficiales --%>
    
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvCobertVegetales" AutoGenerateColumns="False" SkinID="Grilla_simple"
                width="99%"
                    EmptyDataText="No ha agregado Coberturas vegetales ha aprovechar" OnRowDeleting="grvCobertVegetales_RowDeleting">                                        
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField DataField="EVA_ID" HeaderText="Codigo"/>
                        <asp:BoundField DataField="EVA_TIPO_UNIDAD" HeaderText="Tipo de Unidad de Cobertura a Intervenir o Posible Intervencion" />
                        <asp:BoundField DataField="EVA_AREA_INTERV" HeaderText="Area a Intervenir (m2)" />
                        <asp:TemplateField HeaderText="Localización" >                        
                        <ItemTemplate>
                            <uc3:ctrPoligonos ID="ctrPoligonos2" runat="server" DataGridObject="true" NombreTabla="EIV_DET_COBERTURA_VEG_APROV" NombreCampo="EVA_ID" ValorCampo='<%# Eval("EVA_ID") %>' ValorCampo2='<%# Eval("EVA_ID") %>' />                                           
                            <asp:Label id="lblEvaId" runat="server" text='<%# Eval("EVA_ID") %>' visible="false"></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>  
          <tr>
            <td width="25%">
                4.5.2 Aprovechamiento a Realizar
            </td>
             <td colspan="3" width="75%" align="right">
                <asp:Button ID="btnAprobRealizar" runat="server" SkinID="boton_copia"
                    Text="Agregar Aprovechamiento a Realizar" OnClick="btnAprobRealizar_Click"/></td>
        </tr>   
      <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>               
        <asp:PlaceHolder runat="server" ID="plhAprobRealicar" Visible="False">
         
        <tr>
            <td width="25%">
                <asp:Label ID="Label111" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Muestreo "></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtTipoMuestreo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese Información Tipo de Muestreo"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" 
                ErrorMessage="Ingrese Información Tipo de Muestreo"
                    Display="Dynamic" ControlToValidate="txtTipoMuestreo"
                    ValidationGroup="AprobRealicar">*</asp:RequiredFieldValidator>
            </td>  
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label112" runat="server" SkinID="etiqueta_negra" 
                Text="No. Parcelas o Unidades de Muestreo"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtParcelasUnid" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="No. Parcelas o Unidades de Muestreo"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator77" runat="server" 
                ErrorMessage="Ingrese Información No. Parcelas o Unidades de Muestreo"
                    Display="Dynamic" ControlToValidate="txtParcelasUnid"
                    ValidationGroup="AprobRealicar">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender14" Mask="9999999999999" runat="server" MaskType="Number" TargetControlID="txtParcelasUnid"  AutoComplete="false" />                                
            </td>             
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label113" runat="server" SkinID="etiqueta_negra" 
                Text="Total Area Muestreada (Ha)"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtTotalMuestra" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Para Hidrocarburo Vol/Ha"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator78" runat="server" 
                ErrorMessage="Ingrese Información total Area Muestreada"
                    Display="Dynamic" ControlToValidate="txtTotalMuestra"
                    ValidationGroup="AprobRealicar">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender15" Mask="9999999999999" runat="server" MaskType="Number" TargetControlID="txtTotalMuestra"  AutoComplete="false" />                                
            </td>            
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label36" runat="server" SkinID="etiqueta_negra" 
                Text="Error de Muestreo (%)"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtErrorMuestreoAprov" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese Información Error de Muestreo"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" 
                ErrorMessage="Ingrese Información Error de Muestreo"
                    Display="Dynamic" ControlToValidate="txtErrorMuestreoAprov"
                    ValidationGroup="AprobRealicar">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender19" Mask="9999999999999" runat="server" MaskType="Number" TargetControlID="txtErrorMuestreoAprov"  AutoComplete="false" />                                
            </td>            
        </tr>        
        <tr>
            <td width="25%">
                <asp:Label ID="Label114" runat="server" SkinID="etiqueta_negra" 
                Text="Probabilidad del Error (%)"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtProbError" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese Información Probabilidad del Error"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator79" runat="server" 
                ErrorMessage="Ingrese Información Probabilidad del Error"
                    Display="Dynamic" ControlToValidate="txtProbError"
                    ValidationGroup="AprobRealicar">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender16" Mask="9999999999999" runat="server" MaskType="Number" TargetControlID="txtProbError"  AutoComplete="false" />                                
            </td>            
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label115" runat="server" SkinID="etiqueta_negra" 
                Text="Volumen Total a Aprovechar (m3) ó (m3/Ha)"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtVolTotApro" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese Infomación Volumen Total a Aprovechar"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator80" runat="server" 
                ErrorMessage="Ingrese Infomación Volumen Total a Aprovechar"
                    Display="Dynamic" ControlToValidate="txtVolTotApro"
                    ValidationGroup="AprobRealicar">*</asp:RequiredFieldValidator>    
                    <cc1:MaskedEditExtender ID="MaskedEditExtender17" Mask="9999999999999" runat="server" MaskType="Number" TargetControlID="txtVolTotApro"  AutoComplete="false" />                                            
            </td>            
        </tr>        
        <tr>
            <td width="25%">
                <asp:Label ID="Label116" runat="server" SkinID="etiqueta_negra" 
                Text="Volumen Comercial a Aprovechar (m3) ó (m3/Ha)"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtVolComApro" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Para Hidrocarburo Vol/Ha"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator81" runat="server" 
                ErrorMessage="Ingrese Información Volumen Comercial a Aprovechar"
                    Display="Dynamic" ControlToValidate="txtVolComApro"
                    ValidationGroup="AprobRealicar">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender18" Mask="9999999999999" runat="server" MaskType="Number" TargetControlID="txtVolComApro"  AutoComplete="false" />                                
            </td>            
        </tr>
        <tr>
            <td align="center" width="25%">
            </td>
            <td align="center" style="width: 75%">
                <asp:Button ID="btnAgregarAprobRealicar" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="AprobRealicar" OnClick="btnAgregarAprobRealicar_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarAprobRealicar" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarAprobRealicar_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary7" runat="server" 
                ValidationGroup="AprobRealicar" />
            </td>
        </tr>
         </asp:PlaceHolder> 
        <%--Holder 12--%>
          <tr>
            <td colspan="4" width="100%">
                Volumen a Aprovechar por Unidad de Cobertura
            </td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvAprobRealicar" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de aprovechamiento a realizar" OnRowDeleting="grvAprobRealicar_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField DataField="EVC_ID" HeaderText="Codigo" />
                        <asp:BoundField DataField="EVC_TIPO_MUESTREO" HeaderText="Tipo de Muestreo" />
                        <asp:BoundField DataField="EVC_NO_PARCELAS" HeaderText="No. Parcelas o Unidades de Muestreo" />
                        <asp:BoundField DataField="EVC_TAREA_MUESTREADA" HeaderText="Total del Area Muestreada (Ha)" />                        
                        <asp:BoundField DataField="EVC_ERROR_MUESTREO" HeaderText="Error del Muestreo (%)" />
                        <asp:BoundField DataField="EVC_PROB_ERROR" HeaderText="Probabilidad de Error (%)" />
                        <asp:BoundField DataField="EVC_VOL_APROVECHAR" HeaderText="Volumen Total a Aprobechar (m3) ó (m3/Ha)" />
                        <asp:BoundField DataField="EVC_VOL_COMER_APROVECHAR" HeaderText="Volumen Comercial a Aprobechar (m3) ó (m3/Ha)" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
            
         <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>        
    
       <tr>
            <td width="25%">
                4.5.3 Aprovechamiento Forestal Asociado a Obras Complementarias
            </td>                        
            <td width="25%" align="right">
              <asp:Label ID="Label15" runat="server" SkinID="etiqueta_negra" 
                Text="Aplica"></asp:Label>
            </td>                        
             <td width="50%" colspan="2">             
                <asp:DropDownList ID="cboAplica" runat="server" SkinID="lista_desplegable" OnSelectedIndexChanged="cboAplica_SelectedIndexChanged">
                    <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                    <asp:ListItem Value="0">Si</asp:ListItem>
                    <asp:ListItem Value="1">No</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
             <td width="100%" colspan="4" align="right">
                <asp:Button ID="btnAprovForet" runat="server" SkinID="boton_copia"
                    Text="Agregar Aprovechamiento Forestal" OnClick="btnAprovForet_Click" Visible="False"   /></td>
        </tr> 
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>                 
        <asp:PlaceHolder runat="server" ID="plhAprovForest" Visible="False">
                       
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label117" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Obra"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtTipoObra" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingresar InformaciónTipo de Obra"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" 
                ErrorMessage="Ingresar Información Tipo de Obra"
                    Display="Dynamic" ControlToValidate="txtTipoObra"
                    ValidationGroup="AprovForest">*</asp:RequiredFieldValidator>
            </td>             
        </tr>     
        <tr>
            <td width="25%">
                <asp:Label ID="Label119" runat="server" SkinID="etiqueta_negra" 
                Text="Predio"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtPredio" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese Información Predio"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator83" runat="server" 
                ErrorMessage="Ingrese Información Predio"
                    Display="Dynamic" ControlToValidate="txtPredio"
                    ValidationGroup="AprovForest">*</asp:RequiredFieldValidator>
            </td>            
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label120" runat="server" SkinID="etiqueta_negra" 
                Text="Área (m2)"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtAreaAprovForest" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese  Información Area"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator84" runat="server" 
                ErrorMessage="Ingrese  Información Area"
                    Display="Dynamic" ControlToValidate="txtAreaAprovForest"
                    ValidationGroup="AprovForest">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender20" Mask="9999999999999" runat="server" MaskType="Number" TargetControlID="txtAreaAprovForest"  AutoComplete="false" />                                
            </td>            
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label121" runat="server" SkinID="etiqueta_negra" 
                Text="Volumen Total (m3)"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtVolumenTotAprovForest" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingresar Información Volumen Total"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator85" runat="server" 
                ErrorMessage="Ingresar Información Volumen Total"
                    Display="Dynamic" ControlToValidate="txtVolumenTotAprovForest"
                    ValidationGroup="AprovForest">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender21" Mask="9999999999999" runat="server" MaskType="Number" TargetControlID="txtVolumenTotAprovForest"  AutoComplete="false" />                                
            </td>            
        </tr>        
        <tr>
            <td width="25%">
                <asp:Label ID="Label122" runat="server" SkinID="etiqueta_negra" 
                Text="Volumen Comercial (m3)"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtVolCoberAprovForest" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingresar Información Volumen Comercial"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator86" runat="server" 
                ErrorMessage="Ingresar Información Volumen Comercial"
                    Display="Dynamic" ControlToValidate="txtVolCoberAprovForest"
                    ValidationGroup="AprovForest">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender22" Mask="9999999999999" runat="server" MaskType="Number" TargetControlID="txtVolCoberAprovForest"  AutoComplete="false" />                                
            </td>            
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label123" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad de Cobertura"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtUnidadCobertura" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese Información Unidad de Cobertura"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator87" runat="server" 
                ErrorMessage="Ingrese Información Unidad de Cobertura"
                    Display="Dynamic" ControlToValidate="txtUnidadCobertura"
                    ValidationGroup="AprovForest">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender23" Mask="9999999999999" runat="server" MaskType="Number" TargetControlID="txtUnidadCobertura"  AutoComplete="false" />                                
            </td>            
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label124" runat="server" SkinID="etiqueta_negra" 
                Text="Localización - Coordenadas planas (Datum Magna-Sirgas)"></asp:Label>
            </td>
            <td colspan="3" width="75%">
               <uc2:ctrUbicacionPoli runat="server" ID="ctrUbicacionPoliAprovForest" />
            </td>            
        </tr>
        <tr>
            <td align="center" width="25%">
            </td>
            <td align="center" style="width: 75%">
                <asp:Button ID="btnAgregarAprovForest" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="AprovForest" OnClick="btnAgregarAprovForest_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarAprovForest" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarAprovForest_Click"  />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary8" runat="server" 
                ValidationGroup="AprovForest" />
            </td>
        </tr>
        </asp:PlaceHolder>
        <%--Holder 12--%>    
        
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvAprovForest" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de Aprovechamiento Forestal" OnRowDeleting="grvAprovForest_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField DataField="EFO_ID" HeaderText="Codigo" />
                        <asp:BoundField DataField="EFO_TIPO_OBRA" HeaderText="Tipo de Obra" />
                        <asp:BoundField DataField="EFO_PREDIO" HeaderText="Predio" />
                        <asp:BoundField DataField="EFO_AREA" HeaderText="Área (m2)" />                        
                        <asp:BoundField DataField="EFO_VOLUMEN_TOTAL" HeaderText="Volumen Total (m3)" />                        
                        <asp:BoundField DataField="EFO_VOLUMEN_COMERCIAL" HeaderText="Volumen Comercial (m3)" />
                        <asp:BoundField DataField="EFO_UNIDAD_COBERTURA" HeaderText="Unidad de Cobertura" />
                        <asp:TemplateField HeaderText="Localización - Coordenadas planas (Datum Magna-Sirgas)">
                        <ItemTemplate>
                            <uc2:ctrUbicacionPoli ID="ctrCoorForestGrid" runat="server" DataGridObject="true" NombreTabla="EIH_COOR_APROV_FOREST" NombreCampo="EFO_ID" ValorCampo='<%# Eval("EFO_ID") %>' ValorCampo2='<%# Eval("EFO_ID") %>' />                                           
                            <asp:Label id="lblEfoId" runat="server" text='<%# Eval("EFO_ID") %>' visible="false"></asp:Label>                       
                        </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        

        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>      
        <tr>
            <td colspan="4">
                4.6 EMISIONES ATMOSFÉRICAS
            </td>                        
        </tr>                
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>   
        <tr>
            <td width="25%">
                4.6.1 Emisiones
            </td>                                    
             <td colspan="3" width="75%" align="right">
                <asp:Button ID="btnEmisiones" runat="server" SkinID="boton_copia"
                    Text="Agregar Emisiones" OnClick="btnEmisiones_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>                  
        <asp:PlaceHolder runat="server" ID="plhEmisiones" Visible="False">
                                      
        <tr>
            <td width="25%">
                <asp:Label ID="Label126" runat="server" SkinID="etiqueta_negra" 
                Text="Descripción del Punto de Descarga Previsto"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtDescPuntoDesc" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingresar Información Descripción del Punto de Descarga Previsto"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator89" runat="server" 
                ErrorMessage="Ingresar Información Descripción del Punto de Descarga Previsto"
                    Display="Dynamic" ControlToValidate="txtDescPuntoDesc"
                    ValidationGroup="Emisiones">*</asp:RequiredFieldValidator>
            </td>             
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label127" runat="server" SkinID="etiqueta_negra" 
                Text="Emisión Estimada"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtEmisionEstimada" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingresar información Emisión Estimada"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator90" runat="server" 
                ErrorMessage="Ingresar información Emisión Estimada"
                    Display="Dynamic" ControlToValidate="txtEmisionEstimada"
                    ValidationGroup="Emisiones">*</asp:RequiredFieldValidator>
            </td>            
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label128" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Fuente"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboTipoFuente" runat="server" SkinID="lista_desplegable" 
                 Width="99%"></asp:DropDownList>    
                <asp:CompareValidator ID="CompareValidator22" runat="server"
                    ErrorMessage="Seleccione Tipo Fuente" 
                    Display="Dynamic"
                    ControlToValidate="cboTipoFuente" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="Emisiones">*</asp:CompareValidator>                   
            </td>            
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label129" runat="server" SkinID="etiqueta_negra" 
                Text="Mecanismos de Control Particulas"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtMecParticulas" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese Información Mecanismos de Control Particulas"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator92" runat="server" 
                ErrorMessage="Ingrese Información Mecanismos de Control Particulas"
                    Display="Dynamic" ControlToValidate="txtMecParticulas"
                    ValidationGroup="Emisiones">*</asp:RequiredFieldValidator>
            </td>            
        </tr>        
        <tr>
            <td width="25%">
                <asp:Label ID="Label130" runat="server" SkinID="etiqueta_negra" 
                Text="Mecanismos de control Gases y Vapores"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtMecControlGas" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese Información Mecanismos de control Gases y Vapores"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator93" runat="server" 
                ErrorMessage="Ingrese Información Mecanismos de control Gases y Vapores"
                    Display="Dynamic" ControlToValidate="txtMecControlGas"
                    ValidationGroup="Emisiones">*</asp:RequiredFieldValidator>
            </td>            
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label131" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Ducto"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboTipoDucto" runat="server" SkinID="lista_desplegable" 
                 Width="99%"></asp:DropDownList> 
                 <asp:CompareValidator ID="CompareValidator23" runat="server"
                    ErrorMessage="Seleccione Tipo Ducto" 
                    Display="Dynamic"
                    ControlToValidate="cboTipoDucto" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="Emisiones">*</asp:CompareValidator>               
            </td>         
        </tr>
        
        <tr>
            <td align="center" width="25%">
            </td>
            <td align="center" style="width: 75%">
                <asp:Button ID="btnAgregarEmisiones" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="Emisiones" OnClick="btnAgregarEmisiones_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarEmisiones" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarEmisiones_Click"  />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary23" runat="server" 
                ValidationGroup="Emisiones" />
            </td>
        </tr>
        </asp:PlaceHolder>
       <%--Holder 12--%>
        
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvEmisiones" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado Emisiones" OnRowDeleting="grvEmisiones_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField DataField="EEA_ID" HeaderText="Codigo" />
                        <asp:BoundField DataField="EEA_DESC_PTO_DESC_PREV" HeaderText="Descripcion del Punto de Descarga Previsto" />
                        <asp:BoundField DataField="EEA_EMISION_ESTIMADA" HeaderText="Emisión estimada" />
                        <asp:BoundField DataField="ETF_TIPO_FUENTE_EMISIONES" HeaderText="Tipo de Fuente" />                        
                        <asp:BoundField DataField="EEA_MEC_CONTROL_PART" HeaderText="Mecanismo de Control Particulas" />           
                        <asp:BoundField DataField="EEA_MEC_CONTROL_GASV" HeaderText="Gases y Vapores" />
                        <asp:BoundField DataField="ETD_NOMBRE_DUCTO" HeaderText="Tipo de Ducto" />                        
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>   
        
           <tr>
            <td width="25%">
                4.6.2 Ruido
            </td>                                    
             <td width="75%" colspan="3" align="right">
                <asp:Button ID="btnRuido" runat="server" SkinID="boton_copia"
                    Text="Agregar Ruido" OnClick="btnRuido_Click"   /></td>
        </tr>   
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>                
        <asp:PlaceHolder runat="server" ID="plhRuido" Visible="False">
                                    
        <tr>
            <td width="25%">
                <asp:Label ID="Label125" runat="server" SkinID="etiqueta_negra" 
                Text="Sitios de Emisión de Ruido"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtEmisionRuido" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese Información Sitios de Emisión de Ruido"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator95" runat="server" 
                ErrorMessage="Ingrese Información Sitios de Emisión de Ruido"
                    Display="Dynamic" ControlToValidate="txtEmisionRuido"
                    ValidationGroup="Ruido">*</asp:RequiredFieldValidator>
            </td>             
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label132" runat="server" SkinID="etiqueta_negra" 
                Text="Niveles de Presión Sonoros Estimados"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtNivelesPresion" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese Información Niveles de Presión Sonoros Estimados"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator96" runat="server" 
                ErrorMessage="Ingrese Información Niveles de Presión Sonoros Estimados"
                    Display="Dynamic" ControlToValidate="txtNivelesPresion"
                    ValidationGroup="Ruido">*</asp:RequiredFieldValidator>
            </td>            
        </tr>   
        
        <tr>
            <td align="center" width="25%">
            </td>
            <td align="center" style="width: 75%">
                <asp:Button ID="btnAgregarRuido" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="Ruido" OnClick="btnAgregarRuido_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarRuido" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarRuido_Click"  />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary26" runat="server" 
                ValidationGroup="Ruido" />
            </td>
        </tr>     
        </asp:PlaceHolder>   
        <%--Holder 12--%>    
        
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvRuido" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de cuencas hidrográficas" OnRowDeleting="grvRuido_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField DataField="EER_ID" HeaderText="Codigo" />
                        <asp:BoundField DataField="EER_SITIOS_EMI_RUIDO" HeaderText="Sitios de Emision de Ruidos" />
                        <asp:BoundField DataField="EER_NIV_PRES_RUIDO" HeaderText="Niveles de Presión Sonoros Estimados" />                        
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        
        
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>   
        <tr>
            <td colspan="4">
                4.7 GENERACION DE RESIDUOS SOLIDOS
            </td>                        
        </tr>                
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>        
               
                <tr>
            <td width="25%">
                4.7.1 Residuos no Peligrosos
            </td>                                    
             <td width="75%" colspan="3" align="right">
                <asp:Button ID="btnResiduosnoPeligrosos" runat="server" SkinID="boton_copia"
                    Text="Agregar Residuos No Peligrosos" OnClick="btnResiduosnoPeligrosos_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>                   
        <asp:PlaceHolder runat="server" ID="plhResiduosnoPeligrosos" Visible="False">
                                  
        <tr>
            <td width="25%">
                <asp:Label ID="Label133" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Residuos"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtTipoResiduos" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese Información Tipo de Residuos"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator97" runat="server" 
                ErrorMessage="Ingrese Información Tipo de Residuos"
                    Display="Dynamic" ControlToValidate="txtTipoResiduos"
                    ValidationGroup="ResiduosnoPeligrosos">*</asp:RequiredFieldValidator>
            </td>             
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label134" runat="server" SkinID="etiqueta_negra" 
                Text="Sitio de Almacenamiento"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:TextBox ID="txtSitioAlmacenNo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese Información Sitio de Almacenamiento"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator98" runat="server" 
                ErrorMessage="Ingrese Información Sitio de Almacenamiento"
                    Display="Dynamic" ControlToValidate="txtSitioAlmacenNo"
                    ValidationGroup="ResiduosnoPeligrosos">*</asp:RequiredFieldValidator>
            </td>            
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label135" runat="server" SkinID="etiqueta_negra" 
                Text="Quien los manejara?"></asp:Label>
            </td>
            <td colspan="3" width="75%">
                <asp:DropDownList ID="cboQuienManejaNo" runat="server" SkinID="lista_desplegable" 
                MaxLength="200" Width="99%"></asp:DropDownList>  
                <asp:CompareValidator ID="CompareValidator24" runat="server"
                    ErrorMessage="Seleccione Quien Los Maneja" 
                    Display="Dynamic"
                    ControlToValidate="cboQuienManejaNo" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="ResiduosnoPeligrosos">*</asp:CompareValidator>                
            </td>            
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="Label136" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Aprovechamiento Previsto"></asp:Label>
            </td>
            <td style="width: 75%" colspan="3">
                <asp:DropDownList ID="cboTipoAprovPreNo" runat="server" SkinID="lista_desplegable" 
                 Width="99%" 
                 ToolTip="Ingrese Información Tipo de Aprovechamiento Previsto" AutoPostBack="True" OnSelectedIndexChanged="cboTipoAprovPreNo_SelectedIndexChanged"></asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator25" runat="server"
                    ErrorMessage="Seleccione Tipo de Aprovechamiento Previsto" 
                    Display="Dynamic"
                    ControlToValidate="cboTipoAprovPreNo" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="ResiduosnoPeligrosos">*</asp:CompareValidator>    
            </td>  
        </tr>          
        <tr id="trTipoTraNo" runat="server" visible="false">
            <td width="25%">
                <asp:Label ID="Label139" runat="server" SkinID="etiqueta_negra" 
                Text="Otro"></asp:Label>
            </td>
            <td width="25%" colspan="3">
                <asp:TextBox ID="txtTipoAprovPreNoOtro" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese Información Tipo de Aprovechamiento Previsto Otro"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator103" runat="server" 
                ErrorMessage="Ingrese Información Tipo de Aprovechamiento Previsto Otro"
                    Display="Dynamic" ControlToValidate="txtTipoAprovPreNoOtro"
                    ValidationGroup="ResiduosnoPeligrosos">*</asp:RequiredFieldValidator>                    
            </td>            
        </tr>  
        <tr >
            <td width="25%">
                <asp:Label ID="Label140" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Tratamiento Previsto"></asp:Label>
            </td>
            <td style="width: 75%" colspan="3">
                <asp:DropDownList ID="cboTipoTraPrevNo" runat="server" SkinID="lista_desplegable" 
                Width="99%" 
                ToolTip="Ingrese Información Tipo de Tratamiento Previsto" AutoPostBack="True" OnSelectedIndexChanged="cboTipoTraPrevNo_SelectedIndexChanged"></asp:DropDownList>   
                <asp:CompareValidator ID="CompareValidator26" runat="server"
                    ErrorMessage="Seleccione Tipo de Tratamiento Previsto" 
                    Display="Dynamic"
                    ControlToValidate="cboTipoTraPrevNo" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="ResiduosnoPeligrosos">*</asp:CompareValidator>                         
            </td>                        
        </tr>
        <tr id="trTraPreNo" runat="server" visible="false">
            <td width="25%">
                <asp:Label ID="Label141" runat="server" SkinID="etiqueta_negra" 
                Text="Otro"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtTipoTraPrevNoOtro" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese Información Tipo de Tratamiento Previsto Otro"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator105" runat="server" 
                ErrorMessage="Ingrese Información Tipo de Tratamiento Previsto Otro"
                    Display="Dynamic" ControlToValidate="txtTipoTraPrevNoOtro"
                    ValidationGroup="ResiduosnoPeligrosos">*</asp:RequiredFieldValidator>
            </td>            
        </tr>     
        <tr>
            <td width="25%">
                <asp:Label ID="Label142" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Disposición Previsto"></asp:Label>
            </td>
            <td style="width: 75%" colspan="3">
                <asp:DropDownList ID="cboTipoDespPrevNo" runat="server" SkinID="lista_desplegable" 
                Width="99%" ToolTip="Ingresar Información Tipo de Disposición Previsto" AutoPostBack="True" OnSelectedIndexChanged="cboTipoDespPrevNo_SelectedIndexChanged"></asp:DropDownList>                
                <asp:CompareValidator ID="CompareValidator27" runat="server"
                    ErrorMessage="Seleccione Tipo de Disposición Previsto" 
                    Display="Dynamic"
                    ControlToValidate="cboTipoTraPrevNo" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="cboTipoDespPrevNo">*</asp:CompareValidator>      
            </td>            
        </tr>
        <tr id="trDespPrevNo" runat="server" visible="false">
            <td width="25%">
                <asp:Label ID="Label143" runat="server" SkinID="etiqueta_negra" 
                Text="Otro"></asp:Label>
            </td>
            <td width="75%" colspan="3">
                <asp:TextBox ID="txtTipoDespPrevNoOtro" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingresar Información Tipo de Disposición Previsto Otro"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator107" runat="server" 
                ErrorMessage="Ingresar Información Tipo de Disposición Otro"
                    Display="Dynamic" ControlToValidate="txtTipoDespPrevNoOtro"
                    ValidationGroup="ResiduosnoPeligrosos">*</asp:RequiredFieldValidator>
            </td>            
        </tr>    
        <tr>
            <td align="center" width="25%">
            </td>
            <td align="center" style="width: 75%">
                <asp:Button ID="btnAgreagarResiduosnoPeligrosos" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="ResiduosnoPeligrosos" OnClick="btnAgreagarResiduosnoPeligrosos_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarResiduosnoPeligrosos" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarResiduosnoPeligrosos_Click"  />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary27" runat="server" 
                ValidationGroup="ResiduosnoPeligrosos" />
            </td>
        </tr>    
        </asp:PlaceHolder>    
   <%--Holder 12--%>
        
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvResiduosnoPeligrosos" AutoGenerateColumns="False"
                width="99%" SkinID="Grilla_simple"
                    EmptyDataText="No ha agregado información de Residuos No Peligrosos" OnRowDeleting="grvResiduosnoPeligrosos_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField DataField="EMN_ID" HeaderText="Codigo" />
                        <asp:BoundField DataField="EMN_TIPO_RESIDUOS" HeaderText="Tipo de Residuos" />
                        <asp:BoundField DataField="EMN_SITIO_ALMACENAMIENTO" HeaderText="Sitio de Almacenamiento" />
                        <asp:BoundField DataField="EQM_QUIEN_MANEJA" HeaderText="Quien los manejara?" />                        
                        <asp:BoundField DataField="TIPO_APRO" HeaderText="Tipo de Aprovechamiento Previsto" />           
                        <asp:BoundField DataField="TIPO_TRA" HeaderText="Tipo de Tratamiento Previsto" />
                        <asp:BoundField DataField="TIPO_DISP" HeaderText="Tipo de Disposicion Previsto" />                        
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>   
        
             <tr>
            <td width="25%" >
                4.7.2 Residuos Peligrosos
            </td>                                    
             <td width="75%" colspan="3" align="right">
                <asp:Button ID="btnResiduosPeligrosos" runat="server" SkinID="boton_copia"
                    Text="Agregar Residuos Peligrosos" OnClick="btnResiduosPeligrosos_Click"  /></td>
        </tr>   
        <tr>
            <td class="titleUpdate" colspan="4" width="100%">
            </td>
        </tr>                
        <asp:PlaceHolder runat="server" ID="plhResiduosPeligrosos" Visible="False">
                                      
        <tr>
            <td style="width: 25%">
                <asp:Label ID="Label21" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Residuos"></asp:Label>
            </td>
            <td  colspan="3">
                <asp:TextBox ID="txtTipoResiduos2" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese Información Tipo de Residuos"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" 
                ErrorMessage="Ingrese Información Tipo de Residuos"
                    Display="Dynamic" ControlToValidate="txtTipoResiduos2"
                    ValidationGroup="ResiduosPeligrosos">*</asp:RequiredFieldValidator>
            </td>             
        </tr>
        <tr>
            <td style="width: 25%">
                <asp:Label ID="Label35" runat="server" SkinID="etiqueta_negra" 
                Text="Sitio de Almacenamiento"></asp:Label>
            </td>
            <td  colspan="3">
                <asp:TextBox ID="txtSitioAlmacen" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese Información Sitio de Almacenamiento"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" 
                ErrorMessage="Ingrese Información Sitio de Almacenamiento"
                    Display="Dynamic" ControlToValidate="txtSitioAlmacen"
                    ValidationGroup="ResiduosPeligrosos">*</asp:RequiredFieldValidator>
            </td>            
        </tr>
        <tr>
            <td style="width: 25%">
                <asp:Label ID="Label39" runat="server" SkinID="etiqueta_negra" 
                Text="Quien los manejara?"></asp:Label>
            </td>
            <td  colspan="3">
                <asp:DropDownList ID="cboQuienManeja" runat="server" SkinID="lista_desplegable" 
                Width="99%"></asp:DropDownList>   
                <asp:CompareValidator ID="CompareValidator28" runat="server"
                    ErrorMessage="Seleccione Quien los Maneja" 
                    Display="Dynamic"
                    ControlToValidate="cboQuienManeja" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="ResiduosPeligrosos">*</asp:CompareValidator>                   
            </td>            
        </tr>
        <tr>
            <td style="width: 25%">
                <asp:Label ID="Label84" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Aprovechamiento Previsto"></asp:Label>
            </td>
            <td  colspan="3">
                <asp:DropDownList ID="cboTipoAprovPre" runat="server" SkinID="lista_desplegable" 
                Width="99%" 
                ToolTip="Ingrese Información Tipo de Aprovechamiento Previsto" AutoPostBack="True" OnSelectedIndexChanged="cboTipoAprovPre_SelectedIndexChanged"></asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator30" runat="server"
                    ErrorMessage="Seleccione Información Tipo de Aprovechamiento Previsto" 
                    Display="Dynamic"
                    ControlToValidate="cboTipoAprovPre" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="ResiduosPeligrosos">*</asp:CompareValidator> 
            </td>  
          </tr>
          <tr id="trTipoApro" runat="server" visible="false">          
            <td style="width: 25%">
                <asp:Label ID="Label85" runat="server" SkinID="etiqueta_negra" 
                Text="Otro"></asp:Label>
            </td>
            <td  colspan="3">
                <asp:TextBox ID="txtTipoAprovPreNo" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese Información Tipo de Aprovechamiento Previsto Otro"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" 
                ErrorMessage="Ingrese Información Tipo de Aprovechamiento Previsto Otro"
                    Display="Dynamic" ControlToValidate="txtTipoAprovPreNoOtro"
                    ValidationGroup="ResiduosPeligrosos">*</asp:RequiredFieldValidator>               
            </td>            
        </tr>  
        <tr>
            <td style="width: 25%">
                <asp:Label ID="Label109" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Tratamiento Previsto"></asp:Label>
            </td>
            <td  colspan="3">
                <asp:DropDownList ID="cboTipoTraPre" runat="server" SkinID="lista_desplegable" 
                Width="99%" 
                ToolTip="Ingrese Información Tipo de Tratamiento Previsto" AutoPostBack="True" OnSelectedIndexChanged="cboTipoTraPre_SelectedIndexChanged"></asp:DropDownList>  
                <asp:CompareValidator ID="CompareValidator29" runat="server"
                    ErrorMessage="Seleccione Información Tipo de Tratamiento Previsto" 
                    Display="Dynamic"
                    ControlToValidate="cboTipoTraPre" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="ResiduosPeligrosos">*</asp:CompareValidator>           
            
            </td>  
        </tr>
        <tr id="trTipoTra" runat="server" visible="false">             
            <td style="width: 25%">
                <asp:Label ID="Label137" runat="server" SkinID="etiqueta_negra" 
                Text="Otro"></asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtTipoTraPrevOtro" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingrese Información Tipo de Tratamiento Previsto Otro"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" 
                ErrorMessage="Ingrese Información Tipo de Tratamiento Previsto Otro"
                    Display="Dynamic" ControlToValidate="txtTipoTraPrevNoOtro"
                    ValidationGroup="ResiduosPeligrosos">*</asp:RequiredFieldValidator>
            </td>            
        </tr>     
        <tr>
            <td style="width: 25%">
                <asp:Label ID="Label138" runat="server" SkinID="etiqueta_negra" 
                Text="Tipo de Disposición Previsto"></asp:Label>
            </td>
            <td colspan="3">
                <asp:DropDownList ID="cboTipoDespPrev" runat="server" SkinID="lista_desplegable" 
                Width="99%" ToolTip="Ingresar Información Tipo de Disposición Previsto" AutoPostBack="True" OnSelectedIndexChanged="cboTipoDespPrev_SelectedIndexChanged"></asp:DropDownList>                
            <asp:CompareValidator ID="CompareValidator31" runat="server"
                    ErrorMessage="Seleccione Información Tipo de Disposición Previsto" 
                    Display="Dynamic"
                    ControlToValidate="cboTipoDespPrev" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="ResiduosPeligrosos">*</asp:CompareValidator>    
            </td>  
        </tr>
        <tr id="trTipoDisp" runat="server" visible="false">            
            <td style="width: 25%">
                <asp:Label ID="Label144" runat="server" SkinID="etiqueta_negra" 
                Text="Otro"></asp:Label>
            </td>
            <td  colspan="3">
                <asp:TextBox ID="txtTipoDespPrevOtro" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%" 
                ToolTip="Ingresar Información Tipo de Disposición Previsto Otro"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator46" runat="server" 
                ErrorMessage="Ingresar Información Tipo de Disposición Otro"
                    Display="Dynamic" ControlToValidate="txtTipoDespPrevOtro"
                    ValidationGroup="ResiduosPeligrosos">*</asp:RequiredFieldValidator>
            </td>            
        </tr>    
        <tr>
            <td align="center" width="25%">
            </td>
            <td align="center" >
                <asp:Button ID="btnAgregarResiduosPeligrosos" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="ResiduosPeligrosos" OnClick="btnAgregarResiduosPeligrosos_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarResiduosPeligrosos" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarResiduosPeligrosos_Click"  />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary28" runat="server" 
                ValidationGroup="ResiduosPeligrosos" />
            </td>
        </tr>    
       
        </asp:PlaceHolder> 
        
        <tr>
            <td colspan="4" width="100%">
                <asp:GridView runat="server" ID="grvResiduosPeligrosos" AutoGenerateColumns="False"
                width="99%" SkinID="GRILLA_SIMPLE"
                    EmptyDataText="No ha agregado información Residuos Peligrosos" OnRowDeleting="grvResiduosPeligrosos_RowDeleting" >
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField DataField="EMP_ID" HeaderText="Codigo" />
                        <asp:BoundField DataField="EMP_TIPO_RESIDUOS" HeaderText="Tipo de Residuos" />
                        <asp:BoundField DataField="EMP_SITIO_ALMACENAMIENTO" HeaderText="Sitio de Almacenamiento" />
                        <asp:BoundField DataField="EQM_QUIEN_MANEJA" HeaderText="Quien los manejara?" />                        
                        <asp:BoundField DataField="TIPO_APRO" HeaderText="Tipo de Aprovechamiento Previsto" />           
                        <asp:BoundField DataField="TIPO_TRA" HeaderText="Tipo de Tratamiento Previsto" />
                        <asp:BoundField DataField="TIPO_DISP" HeaderText="Tipo de Disposicion Previsto" />                                        
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        
        
        <tr>
            <td  colspan="4">
                <asp:Button ID="btnInfoAdicFuentSup" runat="server" SkinID="boton_copia"
                    Text="Información del estudio" />
            </td>
        </tr>  
    </tbody>
    </table>
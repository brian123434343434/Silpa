<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrFicha2.ascx.cs" Inherits="ResumenEIA_ctrFicha2" %>
<%@ Register src="../Controles/ctrCoordenadasFicha2.ascx" tagname="ctrCoordenadasFicha2" tagprefix="uc1" %>
<%@ Register src="../Controles/ctrCoordenadasPto.ascx" tagname="ctrCoordenadasPto" tagprefix="uc2" %>
<%@ Register src="../Controles/ctrCoordenadas.ascx" tagname="ctrCoordenadas" TagPrefix ="uc3" %>


<table style="width: 100%">    
        <tr>
            <td class="titleUpdate" colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan="5">
                2. INFORMACIÓN DEL PROYECTO</td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="5">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan = "5" >2.1 Ubicación e Identificación del Proyecto</td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="5">
            </td>
        </tr>
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="lblNombreProyecto" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre del Proyecto:"></asp:Label></td>
            <td colspan = "3" width="60%">
                <asp:TextBox ID="txtNombreProyecto" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNombreProyecto" runat="server" 
                ErrorMessage="Ingrese el Nombre del Proyecto"
                    Display="Dynamic" ControlToValidate="txtNombreProyecto"
                    ValidationGroup="Tab2">*</asp:RequiredFieldValidator></td>
        </tr>
        
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="lblSectorProductivo" runat="server" SkinID="etiqueta_negra" 
                Text="Sector Productivo al que Pertenece el Proyecto:"></asp:Label>
            </td>
            <td width="60%" colspan="3">
                <asp:DropDownList ID="cboSectoProductivo" runat="server" SkinID="lista_desplegable" AutoPostBack="True" 
                    onselectedindexchanged="cboSectoProductivo_SelectedIndexChanged">
                </asp:DropDownList><asp:CompareValidator ID="crvSectorProductivo" runat="server"
                    ErrorMessage="Seleccione el sector productivo al que pertenece el proyecto" 
                    Display="Dynamic"
                    ControlToValidate="cboSectoProductivo" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="Tab2">*</asp:CompareValidator>            
                <asp:TextBox ID="txtOtroSectorProductivo" runat="server" 
                SkinID="texto_sintamano" Visible="false" width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator id="rqrOtroSector" runat="server" ValidationGroup="Tab2" 
                ControlToValidate="txtOtroSectorProductivo" Display="Dynamic" ErrorMessage="Ingrese el Otro Sector Productivo" Visible="false">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhHidrocarburos" Visible="False">
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="lblNombreBloqueHidro" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre del Bloque:"></asp:Label></td>
            <td width="60%" colspan = "3">
                <asp:TextBox ID="txtNombreBloqueHidro" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNombreBloqueHidro" runat="server" 
                ErrorMessage="Ingrese el Nombre del bloque"
                    Display="Dynamic" ControlToValidate="txtNombreProyecto"
                    ValidationGroup="Tab2">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revNombreBloqueHidro" runat="server" 
                    ErrorMessage="No se admiten numeros, caracteres especiales o espacios en 
                    blanco en el campo Nombre del bloque"
                    Display="Dynamic" ControlToValidate="txtNombreBloqueHidro" 
                    ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚÑñ]{1,30}$"
                    ValidationGroup="Tab2">*</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="lblUnidadAreaH" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad de Área:"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:DropDownList ID="cboUnidadAreaH" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList><asp:CompareValidator ID="crvUnidadArea" runat="server"
                    ErrorMessage="Seleccione la Unidad de Área" 
                    Display="Dynamic"
                    ControlToValidate="cboUnidadAreaH" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="Tab2">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="lblAreaTotalH" runat="server" SkinID="etiqueta_negra" 
                Text="Área Total:"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:TextBox ID="txtAreaTotalH" runat="server" 
                SkinID="texto_sintamano" 
                MaxLength="100" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAreaTotalH" runat="server" 
                    ErrorMessage="Ingrese el Área Total del Proyecto"
                    Display="Dynamic" ControlToValidate="txtAreaTotalH"
                    ValidationGroup="Tab2">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="lblAreaIntervH" runat="server" SkinID="etiqueta_negra" 
                Text="Área a Intervenir:"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:TextBox ID="txtAreaIntervH" runat="server" SkinID="texto_sintamano" 
                MaxLength="100" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAreaIntervH" runat="server" 
                    ErrorMessage="Ingrese el Área a intervenir del proyecto"
                    Display="Dynamic" ControlToValidate="txtAreaIntervH"
                    ValidationGroup="Tab2">*</asp:RequiredFieldValidator></td>
        </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="plhMineria" Visible="False">
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="lblNombreConcesionM" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre de la Concensión:"></asp:Label></td>
            <td width="60%" colspan = "3">
                <asp:TextBox ID="txtNombreConcesionM" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNombreConcesionM" runat="server" 
                ErrorMessage="Ingrese el Nombre de la Concesión"
                    Display="Dynamic" ControlToValidate="txtNombreConcesionM"
                    ValidationGroup="Tab2">*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revNombreConcesionM" runat="server" 
                    ErrorMessage="No se admiten numeros, caracteres especiales o espacios en 
                    blanco en el campo Nombre de la Concesión"
                    Display="Dynamic" ControlToValidate="txtNombreConcesionM" 
                    ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚÑñ]{1,30}$"
                    ValidationGroup="Tab2">*</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="lblUnidadAreaM" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad de Área:"></asp:Label></td>
            <td colspan="3" width="60%" >
                <asp:DropDownList ID="CboUnidadAreaM" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList><asp:CompareValidator ID="crvUnidadAreaM" runat="server"
                    ErrorMessage="Seleccione la Unidad de Área" 
                    Display="Dynamic"
                    ControlToValidate="CboUnidadAreaM" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="Tab2">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="lblAreaTotalM" runat="server" SkinID="etiqueta_negra" 
                Text="Área Total:"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:TextBox ID="txtAreaTotalM" runat="server" 
                SkinID="texto_sintamano" Width="99%"
                MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAreaTotalM" runat="server" 
                    ErrorMessage="Ingrese el Área Total del Proyecto"
                    Display="Dynamic" ControlToValidate="txtAreaTotalM"
                    ValidationGroup="Tab2">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>        
            <td width="40%" colspan="2">
                <asp:Label ID="lblAreaIntervenirM" runat="server" SkinID="etiqueta_negra" 
                Text="Área a Intervenir:"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:TextBox ID="txtAreaIntervenirM" runat="server" SkinID="texto_sintamano" 
                MaxLength="100" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAreaIntervenirM" runat="server" 
                    ErrorMessage="Ingrese el Área a intervenir del proyecto"
                    Display="Dynamic" ControlToValidate="txtAreaIntervenirM"
                    ValidationGroup="Tab2">*</asp:RequiredFieldValidator></td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="lblDireccionProyecto" runat="server" SkinID="etiqueta_negra" 
                Text="Dirección del Proyecto:"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:TextBox ID="txtDireccionProyecto" runat="server" SkinID="texto_sintamano" 
                MaxLength="200" Width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvDireccionProyecto" runat="server" 
                ErrorMessage="Ingrese la Dirección del Proyecto"
                    Display="Dynamic" ControlToValidate="txtDireccionProyecto"
                    ValidationGroup="Tab2">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td colspan="5" align="right">
                   <asp:Button ID="btnAsignarInforProy" runat="server" SkinID="boton_copia"
                            Text="Asignar" OnClick="btnAsignarInforProy_Click" ValidationGroup="Tab2" />
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:ValidationSummary ID="valResumenRepresentante" runat="server" 
                    ValidationGroup="Tab2" />
            </td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan = "3" width="75%">
                2.1.1 Localización del Proyecto</td>
            <td  align="right" colspan="2">
                        <asp:Button ID="btnAgregarLocProyecto" runat="server" SkinID="boton_copia"
                            Text="Nueva Localización" onclick="btnAgregarLocProyecto_Click" />
            </td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="5">
            </td>
        </tr>
            
       <asp:PlaceHolder runat="server" ID="plhInfoLocalizacion" Visible="false">
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="lblDeptoLocalizacion" runat="server" SkinID="etiqueta_negra" Text="Departamento:"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:DropDownList ID="cboDeptoLocalizacion" runat="server" SkinID="lista_desplegable"
                    AutoPostBack="True" 
                    onselectedindexchanged="cboDeptoLocalizacion_SelectedIndexChanged">
                </asp:DropDownList><asp:CompareValidator ID="crvDeptoLocalizacion" runat="server"
                    ErrorMessage="Seleccione el departamento de la localización del proyecto" 
                    Display="Dynamic"
                    ControlToValidate="cboDeptoLocalizacion" ValueToCompare="-1" 
                    Operator="NotEqual"
                    ValidationGroup="LocalizacionProy">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="lblMunicLocalizacion" runat="server" SkinID="etiqueta_negra" Text="Municipio:"></asp:Label></td>
            <td colspan="3" width="60%">
                <asp:DropDownList ID="cboMunicipioLocalizacion" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList><asp:CompareValidator ID="crvMunicipioLocalizacion" runat="server"
                    ErrorMessage="Seleccione el municipio de la localización del proyecto" 
                    Display="Dynamic"
                    ControlToValidate="cboMunicipioLocalizacion" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="LocalizacionProy">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="lblUnidadPoliticoAdmin" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad Político Administrativa:"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:DropDownList ID="cboUnidadPoliticoAdmin" runat="server" SkinID="lista_desplegable">
                </asp:DropDownList><asp:CompareValidator ID="crvUnidadPoliticoAdmin" runat="server"
                    ErrorMessage="Seleccione la Unidad Político Administrativa" 
                    Display="Dynamic"
                    ControlToValidate="cboUnidadPoliticoAdmin" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="LocalizacionProy">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="lblNombreUPoliticoAdmin" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre Unidad Político Administrativa:"></asp:Label></td>
            <td colspan="3" width="60%">
                <asp:TextBox ID="txtNombreUPoliticoAdmin" runat="server" 
                SkinID="texto_sintamano" width="99%"
                MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ErrorMessage="Ingrese el Nombre de la Unidad Político Administrativa"
                    Display="Dynamic" ControlToValidate="txtNombreUPoliticoAdmin"
                    ValidationGroup="LocalizacionProy">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="lblNombrePredio" runat="server" SkinID="etiqueta_negra" 
                Text="Predio:"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:TextBox ID="txtNombrePredio" runat="server" SkinID="texto_sintamano" 
                MaxLength="100" width="99%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNombrePredio" runat="server" 
                    ErrorMessage="Ingrese el Nombre del Predio"
                    Display="Dynamic" ControlToValidate="txtNombrePredio"
                    ValidationGroup="LocalizacionProy">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td align="center" colspan="2">
            </td>
            <td align="center" width="50%">
                <asp:Button ID="btnAgregarLocalizacion" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="LocalizacionProy" 
                    onclick="btnAgregarLocalizacion_Click"  />
            </td>
            <td align="center" colspan="2">
                <asp:Button ID="btnCancelarLocalizacion" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarLocalizacion_Click"  />
            </td>
        </tr>
           <tr>
            <td colspan="4">
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
                    ValidationGroup="LocalizacionProy" />
            </td>
        </tr>   
        </asp:PlaceHolder>
        <tr>
            <td colspan="5" width="100%">
                <asp:GridView runat="server" ID="grvLocalizacionProyecto"
                EmptyDataText="No se han agregado localizaciones del proyecto" 
                    AutoGenerateColumns="False" SkinID="Grilla_simple" Width="100%" >
                    <Columns>
                        <asp:BoundField DataField="MUN_ID" HeaderText="Código Divipola" />
                        <asp:BoundField HeaderText="Departamento" DataField="DEP_NOMBRE" />
                        <asp:BoundField HeaderText="Municipio" DataField="MUN_NOMBRE" />
                        <asp:BoundField HeaderText="Unidad Político Administrativa" 
                            DataField="EPA_UNIDAD_POL_ADMIN" />
                        <asp:BoundField HeaderText="Nombre de la Unidad Político Administrativa" 
                            DataField="ELP_NOMBRE_UNIDAD_POL_ADMIN" />
                        <asp:BoundField HeaderText="Predio" DataField="ELP_PREDIO" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        
        <tr>
            <td colspan="5" >
            </td>
        </tr>
        
        <tr>
            <td class="titleUpdate" colspan="5" >
            </td>
        </tr>
        <tr>
            <td colspan = "5" width="100%">2.1.2 Coordenadas Planas (Datum Magna-Sirgas)</td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="5" >
            </td>
        </tr>
        <tr>
            <td colspan = "2" rowspan="3" width="50%">
                <asp:Label ID="Label2" runat="server" SkinID="etiqueta_negra" 
                Text="Presentación del Área de Influencia Directa"></asp:Label>
            </td>
            <td width="25%">
                <asp:Label ID="lblOrigenCoordenadas" runat="server" SkinID="etiqueta_negra" 
                Text="Origen"></asp:Label>
            </td>
            <td width="25%" colspan="2">
                <asp:DropDownList ID="cboOrigenCoordenadas" runat="server" SkinID="lista_desplegable" AutoPostBack="True"></asp:DropDownList>
                 <asp:CompareValidator ID="crvOrigenCoordenadas" runat="server"
                    ErrorMessage="Seleccione el origen de las coordenadas del proyecto" 
                    Display="Dynamic"
                    ControlToValidate="cboOrigenCoordenadas" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="Tab2_2">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="25%">
                <asp:Label ID="lblEscalaTrabajoCoord" runat="server" SkinID="etiqueta_negra" 
                Text="Escala de Trabajo"></asp:Label>
            </td>
            <td width="25%" colspan="2">
                <asp:DropDownList ID="cboEscalaTrabajoCoord" runat="server" SkinID="lista_desplegable"
                 AutoPostBack="True" OnSelectedIndexChanged="cboEscalaTrabajoCoord_SelectedIndexChanged"></asp:DropDownList>
                 <asp:CompareValidator ID="crvEscalaTrabajoCoord" runat="server"
                    ErrorMessage="Seleccione la Escala de Trabajo de las coordenadas del proyecto" 
                    Display="Dynamic"
                    ControlToValidate="cboEscalaTrabajoCoord" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="Tab2_2">*</asp:CompareValidator></td>
        </tr>
           <tr >
            <td width="25%">
                <asp:Label ID="lblOtraEscala" runat="server" SkinID="etiqueta_negra"  Visible="false"
                Text="Otro"></asp:Label>
            </td>
            <td width="25%" colspan="2">
             <asp:TextBox ID="txtOtraEscala" runat="server" Visible="false"
                SkinID="texto_sintamano" width="99%"
                MaxLength="100"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvOtraescala" runat="server" 
                    ErrorMessage="Ingrese el Otra Escala"
                    Display="Dynamic" ControlToValidate="txtOtraEscala"
                    ValidationGroup="Tab2_2">*</asp:RequiredFieldValidator></td>
                
        </tr>
        <tr>
            <td colspan = "2" width="50%">
                <asp:Label ID="Label93" runat="server" SkinID="etiqueta_negra" 
                Text="Coordenadas donde se ubica el Proyecto"></asp:Label>
            </td>
            <td colspan="3" width="50%">
                <uc3:ctrCoordenadas ID="ctrCoorUbicacionProyecto" runat="server" />                
            </td>
        </tr>      
        <tr>
            <td colspan="5" align="right"><asp:Button ID="btnAsifnarCoordenadas" runat="server" SkinID="boton_copia"
                    Text="Asignar Coordenadas" OnClick="btnAsifnarCoordenadas_Click" ValidationGroup="Tab2_2" /></td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                    ValidationGroup="Tab2_2" />
            </td>
        </tr>        
        <tr>
            <td class="titleUpdate" colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan = "3" width="60%">2.1.3 Autoridad Ambiental con Jurisdicción en Area del Proyecto</td>
            <td width="40%" colspan="2" align="right">
                <asp:Button ID="btnAgregarAutoridad" runat="server" SkinID="boton_copia"
                    Text="Nueva Autoridad" onclick="btnAgregarAutoridad_Click"  /></td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="5">
            </td>
        </tr>
      <asp:PlaceHolder runat="server" ID="plhAutoridadesAmbientales" Visible="false">

            <tr>
                <td width="40%" colspan="2">
                    <asp:Label ID="Label26" runat="server" SkinID="etiqueta_negra" Text="Autoridad Ambiental 
                    con Jurisdicción en el Area del Proyecto:"></asp:Label></td>
                <td width="60%" colspan="3">
                    <asp:DropDownList ID="cboAutoridadAmbiental" runat="server" SkinID="lista_desplegable">
                    </asp:DropDownList><asp:CompareValidator ID="CompareValidator1" runat="server"
                        ErrorMessage="Seleccione la autoridad ambiental" 
                        Display="Dynamic"
                        ControlToValidate="cboAutoridadAmbiental" ValueToCompare="-1" 
                        Operator="NotEqual"
                        ValidationGroup="AutoridadesAmb">*</asp:CompareValidator></td>
            </tr>
            <tr>                        
                <td width="40%" colspan="2">
                    <asp:Label ID="Label27" runat="server" SkinID="etiqueta_negra" Text="Número de Expediente del Proyecto:"></asp:Label></td>
                <td width="60%" colspan="3">
                    <asp:TextBox ID="txtExpedienteProyecto" runat="server"  width="99%"
                    SkinID="texto_sintamano" MaxLength="100"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="Ingrese número de expediente del proyecto"
                        Display="Dynamic" ControlToValidate="txtExpedienteProyecto"
                        ValidationGroup="AutoridadesAmb">*</asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="center" width="25%"></td>
                <td align="center" width="25%">
                    <asp:Button ID="btnAgregarAutorAmb" runat="server" SkinID="boton_copia"
                        Text="Agregar" ValidationGroup="AutoridadesAmb" 
                        onclick="btnAgregarAutorAmb_Click" />
                </td>
                <td align="center" width="25%">
                    <asp:Button ID="btnCancelarAutorAmb" runat="server" SkinID="boton_copia"
                        Text="Cancelar" OnClick="btnCancelarAutorAmb_Click" />
                </td>
                <td align="center" width="25%"></td>
            </tr>
        <tr>
            <td colspan="4">
                <asp:ValidationSummary ID="ValidationSummary3" runat="server" 
                    ValidationGroup="AutoridadesAmb" />
            </td>
        </tr>    
        </asp:PlaceHolder>

        <tr>
            <td colspan="5">
                <asp:GridView runat="server" ID="grvAutoridadesAmbientales" AutoGenerateColumns="False"
                width="99%"
                    
                    EmptyDataText="No ha agregado autoridades ambientales con jurisdiscción en el área del proyecto" 
                    onrowdeleting="grvAutoridadesAmbientales_RowDeleting" 
                    SkinID="Grilla_simple">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField HeaderText="Autoridad Ambiental con Jurisdicción en el Area del Proyecto" 
                            DataField="EAA_AUTORIDAD_AMBIENTAL" />
                        <asp:BoundField HeaderText="Número de Expediente del Proyecto" 
                            DataField="EAJ_EXP_PROYECTO" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="5">
            </td>
        </tr>        
        <tr>
            <td class="titleUpdate" colspan="5">
            </td>
        </tr>        
        <tr>
            <td colspan = "5" width="100%">2.2 Infraestructura del Proyecto</td>
        </tr>
        <tr>
            <td colspan = "5" width="100%" class="titleUpdate"></td>
        </tr>
        <tr>
            <td colspan = "2" width="40%">2.2.1 Infraestructura Lineal</td>
            <td width="60%" align="right" colspan="3">
                <asp:Button ID="btnNuevoInfracLinealProy" runat="server" SkinID="boton_copia"
                    Text="Nueva Infraestructura Lineal del Proyecto" 
                    onclick="btnNuevoInfracLinealProy_Click"  />
            </td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="5">
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhInfracLinealProy" Visible="false">
         
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="Label28" runat="server" SkinID="etiqueta_negra" Text="Unidad de Longitud:"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:DropDownList ID="cboUnidLongInfracLineal" runat="server" SkinID="lista_desplegable"
                    AutoPostBack="True" 
                    onselectedindexchanged="cboUnidLongInfracLineal_SelectedIndexChanged">
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator2" runat="server"
                    ErrorMessage="Seleccione la unidad de longitud para la infraestructura lineal" 
                    Display="Dynamic"
                    ControlToValidate="cboUnidLongInfracLineal" ValueToCompare="-1" 
                    Operator="NotEqual"
                    ValidationGroup="InfracLinealProyecto">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="Label29" runat="server" SkinID="etiqueta_negra" Text="Unidad de Área:"></asp:Label></td>
            <td width="60&" colspan="3">
                <asp:DropDownList ID="cboUnidAreaInfracLineal" runat="server" SkinID="lista_desplegable"
                    AutoPostBack="True" 
                    onselectedindexchanged="cboUnidAreaInfracLineal_SelectedIndexChanged">
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator3" runat="server"
                    ErrorMessage="Seleccione la unidad de área para la infraestructura lineal" 
                    Display="Dynamic"
                    ControlToValidate="cboUnidAreaInfracLineal" ValueToCompare="-1" 
                    Operator="NotEqual"
                    ValidationGroup="InfracLinealProyecto">*</asp:CompareValidator>
                    
            </td>
        </tr>        
     
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="Label30" runat="server" SkinID="etiqueta_negra" 
                Text="Descripción de la Infraestructura:"></asp:Label>
            </td>
            <td width="60%" colspan = "3">
                <asp:DropDownList ID="cboDescInfracLinealProy" runat="server" SkinID="lista_desplegable"
                    AutoPostBack="True" OnSelectedIndexChanged="cboDescInfracLinealProy_SelectedIndexChanged">
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator4" runat="server"
                    ErrorMessage="Seleccione la descripción de la infraestructura lineal del proyecto" 
                    Display="Dynamic"
                    ControlToValidate="cboDescInfracLinealProy" ValueToCompare="" 
                    Operator="NotEqual"
                    ValidationGroup="InfracLinealProyecto">*</asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" 
                    ErrorMessage="Seleccione la descripción de la infraestructura lineal del proyecto"
                    Display="Dynamic" ControlToValidate="cboDescInfracLinealProy"
                    ValidationGroup="InfracLinealProyecto">*</asp:RequiredFieldValidator>                           
                <asp:TextBox ID="txtOtraDescInfracLinealProy" runat="server"  width="99%"
                SkinID="texto_sintamano" Visible="False" Text=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="lblLongitudInfracLinealProy" runat="server" SkinID="etiqueta_negra" 
                Text="Longitud():"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:TextBox ID="txtLongitudInfracLinealProy" runat="server"  width="99%"
                SkinID="texto_sintamano"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                ErrorMessage="Ingrese la longitud"
                    Display="Dynamic" ControlToValidate="txtLongitudInfracLinealProy"
                    ValidationGroup="InfracLinealProyecto">*</asp:RequiredFieldValidator>                
            </td>
        </tr>
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="lblAreaIntervenida" runat="server" SkinID="etiqueta_negra" 
                Text="Área Intervenida():"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:TextBox ID="txtAreaInfracLinealProy" runat="server" width="99%"
                SkinID="texto_sintamano"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ErrorMessage="Ingrese el Área"
                    Display="Dynamic" ControlToValidate="txtAreaInfracLinealProy"
                    ValidationGroup="InfracLinealProyecto">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender1" Mask="99999999.9999" runat="server" MaskType="Number" TargetControlID="txtAreaInfracLinealProy"  AutoComplete="false" />                                
                <asp:CompareValidator runat="server" ID="CompareValidator5" ControlToValidate="txtAreaInfracLinealProy" 
                    ValidationGroup="InfracLinealProyecto" Display="Dynamic" ErrorMessage="El área intervenida de la infraestructura lineal 
                    del proyecto debe ser un valor númerico" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator></td>
        </tr>
        <tr>
            <td colspan="5" width="100%">
                <asp:Label ID="Label34" runat="server" SkinID="etiqueta_negra" 
                Text="Coordenadas de Inicio (m):"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="100%" colspan="5">
                <uc2:ctrCoordenadasPto ID="ctrCoorPtoInicialInfracLineal" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="5" width="100%">
                <asp:Label ID="Label35" runat="server" SkinID="etiqueta_negra" 
                Text="Coordenadas de Finalización(m):"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="100%" colspan="5">
                <uc2:ctrCoordenadasPto ID="ctrCoorPtoFinalInfracLineal" runat="server" />
            </td>
        </tr>    
            
        <tr>       
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarInfracLinealProy" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="InfracLinealProyecto" 
                    onclick="btnAgregarInfracLinealProy_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarInfracLinealProy" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarInfracLinealProy_Click" />
            </td>            
        	<td colspan="3"></td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:ValidationSummary ID="ValidationSummary4" runat="server" 
                    ValidationGroup="InfracLinealProyecto" />
            </td>
        </tr>    
       </asp:PlaceHolder>
        <tr>
            <td colspan="5" >
                <asp:GridView runat="server" ID="grvInfracLinealProy" AutoGenerateColumns="False"
                    EmptyDataText="No ha agregado registros de infraestructura lineal al proyecto"                     
                    onrowdeleting="grvInfracLinealProy_RowDeleting" SkinID="Grilla_simple">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField HeaderText="Descripción de la Infraestructura" 
                            DataField="EDL_DESCRIP_INFRA_LINEAL" />
                        <asp:BoundField HeaderText="Longitud ()" DataField="EHL_LONGITUD" />
                        <asp:BoundField HeaderText="Área Interv. ()" DataField="EHL_AREA" />
                        <asp:BoundField HeaderText="Coordenada Norte Inicio" 
                            DataField="EHL_COOR_NORTE_INICIO" />
                        <asp:BoundField HeaderText="Coordenada Este Inicio" 
                            DataField="EHL_COOR_ESTE_INICIO" />
                        <asp:BoundField HeaderText="Coordenada Norte Finalización" 
                            DataField="EHL_COOR_NORTE_FIN" />
                        <asp:BoundField HeaderText="Coordenada Este Finalización" 
                            DataField="EHL_COOR_ESTE_FIN" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan = "3" width="75%"></td>
            <td width="25%" align="right" colspan="2">
                </td>
            </tr>
        <tr>
            <td colspan = "5" width="75%" class="titleUpdate" style="width: 100%">
			</td>
            </tr>
        <tr>
            <td colspan = "2" width="40%">2.2.1 Infraestructura No Lineal</td>
            <td width="60%" align="right" colspan="3">
                <asp:Button ID="btnNuevaInfracNoLinealProy" runat="server" SkinID="boton_copia"
                    Text="Nueva Infraestructura No Lineal del Proyecto" 
                    onclick="btnNuevaInfracNoLinealProy_Click"  />
            </td>
            </tr>
        <tr>
            <td class="titleUpdate" colspan="5">
            </td>
        </tr>
    <asp:PlaceHolder ID ="plhInfracNoLinealProy"  runat="server"  Visible ="false" >
    
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="Label39" runat="server" SkinID="etiqueta_negra" Text="Unidad de Área:"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:DropDownList ID="cboUnidAreaInfracNoLinealProy" runat="server" SkinID="lista_desplegable"
                    AutoPostBack="True" OnSelectedIndexChanged="cboUnidAreaInfracNoLinealProy_SelectedIndexChanged">
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator7" runat="server"
                    ErrorMessage="Seleccione la unidad de área para la infraestructura No lineal" 
                    Display="Dynamic"
                    ControlToValidate="cboUnidAreaInfracNoLinealProy" ValueToCompare="-1" 
                    Operator="NotEqual"
                    ValidationGroup="InfracNoLinealProyecto">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="Label40" runat="server" SkinID="etiqueta_negra" 
                Text="Descripción de la Infraestructura:"></asp:Label></td>
            <td width="40%" colspan = "3">
                <asp:DropDownList ID="cboDescInfracNoLinealProy" runat="server" SkinID="lista_desplegable"
                    AutoPostBack="True" OnSelectedIndexChanged="cboDescInfracNoLinealProy_SelectedIndexChanged">
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator8" runat="server"
                    ErrorMessage="Seleccione la descripción de la infraestructura lineal del proyecto" 
                    Display="Dynamic"
                    ControlToValidate="cboDescInfracNoLinealProy" ValueToCompare="" 
                    Operator="NotEqual"
                    ValidationGroup="InfracNoLinealProyecto">*</asp:CompareValidator>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" 
                    ErrorMessage="Seleccione Descripción de Infraestructura"
                    Display="Dynamic" ControlToValidate="cboDescInfracNoLinealProy"
                    ValidationGroup="InfracNoLinealProyecto">*</asp:RequiredFieldValidator>
                    
                <asp:TextBox ID="txtOtraDescInfracNoLineal" runat="server"  width="99%"
                SkinID="texto_sintamano" Visible="false"></asp:TextBox>
                </td>            
        </tr>
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="Label42" runat="server" SkinID="etiqueta_negra" 
                Text="Área Intervenida():"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:TextBox ID="txtAreaInfracNoLinealProy" runat="server" width="99%"
                SkinID="texto_sintamano"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ErrorMessage="Ingrese el Área a Intervenir"
                    Display="Dynamic" ControlToValidate="txtAreaInfracLinealProy"
                    ValidationGroup="InfracNoLinealProyecto">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender3" Mask="99999999.9999" runat="server" MaskType="Number" TargetControlID="txtAreaInfracLinealProy"  AutoComplete="false" />                                
                <asp:CompareValidator runat="server" ID="CompareValidator10" ControlToValidate="txtAreaInfracNoLinealProy" 
                    ValidationGroup="InfracNoLinealProyecto" Display="Dynamic" 
                    ErrorMessage="El área intervenida de la infraestructura no lineal 
                    del proyecto debe ser un valor númerico" Operator="DataTypeCheck" Type="Double">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="Label4" runat="server" SkinID="etiqueta_negra" 
                Text="Coordenadas:"></asp:Label></td>
            <td colspan="3" width="60%">
                <uc3:ctrCoordenadas ID="ctrCoorInfracNoLineal" runat="server" Visible="True" />
            </td>
        </tr>
        <tr>
            <td colspan="2" width="50%">
                &nbsp;</td>
            <td colspan="3" width="50%">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarInfranNoLineal" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="InfracNoLinealProyecto"  OnClick="btnAgregarInfranNoLineal_click"/>
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarInfranNoLineal" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarInfracNoLinealProy_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:ValidationSummary ID="ValidationSummary5" runat="server" 
                    ValidationGroup="InfracNoLinealProyecto" />
            </td>
        </tr>  
        </asp:PlaceHolder>
        <tr>
            <td colspan="5">
                <asp:GridView runat="server" ID="grvInfracNoLinealProy" AutoGenerateColumns="False"
                    EmptyDataText="No ha agregado registros de infraestructura no lineal al proyecto" 
                    SkinID="Grilla_simple" Width="100%" OnRowDeleting="grvInfracNoLinealProy_RowDeleting">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField DataField="EHN_ID" HeaderText="Codigo" />
                        <asp:BoundField HeaderText="Descripción de la Infraestructura" 
                            DataField="EDN_DESCRIP_INFRA_NO_LINEAL" />
                        <asp:BoundField HeaderText="Área Interv. ()" DataField="EHN_AREA" />
                        <asp:TemplateField HeaderText="Coordenadas">
                            <ItemTemplate >
                                <uc3:ctrCoordenadas DataGridObject="true" NombreTabla="EIH_COOR_INFRAC_NO_LINEAL" NombreCampo="EHN_ID" ValorCampo='<%# Eval("EHN_ID") %>' ValorCampo2='<%# Eval("EHN_ID") %>' ID="cregrvCoordenadasNo" runat="server"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="5">
           </td>
        </tr>        
        <tr>
            <td class="titleUpdate" colspan="5">
            </td>
        </tr>        
        <tr>
            <td colspan = "5" width="100%">2.3 Movimiento de Tierras</td>
        </tr>
        <tr>
            <td colspan = "5" width="100%" class="titleUpdate"></td>
        </tr>
        <tr>
            <td  width="20%">2.3.1 Infraestructura Lineal</td>
            <td width="80%" align="right" colspan="4">
                <asp:Button ID="btnMovTierraInfracLineal" runat="server" SkinID="boton_copia"
                    Text="Nuevo movimiento de tierras para Infraestructura Lineal" 
                    onclick="btnMovTierraInfracLineal_Click"  />
            </td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="5">
            </td>
        </tr>
       <asp:PlaceHolder runat="server" ID="plhMovTierraInfracLineal" Visible="false">       
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="Label44" runat="server" SkinID="etiqueta_negra" 
                Text="Infraestructura Asociada:"></asp:Label>
            </td>
            <td width="60%" colspan = "3">
                <asp:DropDownList ID="cboInfracAsocMovTierLineal" runat="server" SkinID="lista_desplegable"
                    AutoPostBack="True" OnSelectedIndexChanged="cboInfracAsocMovTierLineal_SelectedIndexChanged">
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator6" runat="server"
                    ErrorMessage="Seleccione la infraestructura asociada al movimiento de tierras" 
                    Display="Dynamic"
                    ControlToValidate="cboInfracAsocMovTierLineal" ValueToCompare="-1" 
                    Operator="NotEqual"
                    ValidationGroup="MovTierrasInfracLineal">*</asp:CompareValidator>            
                <asp:TextBox ID="txtInfracAsocMovTierLineal" runat="server"  width="99%"
                SkinID="texto_sintamano" Visible="false" Text=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Label ID="Label65" runat="server" SkinID="etiqueta_negra" 
                Text="Movimiento de tierras:"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="Label45" runat="server" SkinID="etiqueta_negra" 
                Text="Corte:"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:TextBox ID="txtCorteMovTierLineal" runat="server" width="99%"
                SkinID="texto_sintamano"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ErrorMessage="Ingrese el corte del movimiento de tierra"
                    Display="Dynamic" ControlToValidate="txtCorteMovTierLineal"
                    ValidationGroup="MovTierrasInfracLineal">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender4" Mask="99999999.9999" runat="server" MaskType="Number" TargetControlID="txtCorteMovTierLineal"  AutoComplete="false" />                                
                <asp:CompareValidator runat="server" ID="CompareValidator9" ControlToValidate="txtCorteMovTierLineal" 
                    ValidationGroup="MovTierrasInfracLineal" Display="Dynamic" 
                    ErrorMessage="El corte del movimiento de tierra debe ser un valor númerico" 
                    Operator="DataTypeCheck" Type="Double"></asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="Label66" runat="server" SkinID="etiqueta_negra" 
                Text="Relleno:"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:TextBox ID="txtRellenoMovTierLineal" runat="server" width="99%"
                SkinID="texto_sintamano"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                    ErrorMessage="Ingrese el relleno del movimiento de tierra"
                    Display="Dynamic" ControlToValidate="txtRellenoMovTierLineal"
                    ValidationGroup="MovTierrasInfracLineal">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender5" Mask="99999999.9999" runat="server" MaskType="Number" TargetControlID="txtRellenoMovTierLineal"  AutoComplete="false" />                                
                <asp:CompareValidator runat="server" ID="CompareValidator11" ControlToValidate="txtRellenoMovTierLineal" 
                    ValidationGroup="MovTierrasInfracLineal" Display="Dynamic" 
                    ErrorMessage="El relleno del movimiento de tierra debe ser un valor númerico" 
                    Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                    </td>
        </tr>
        <tr>          
            <td width="40%" colspan="2">
                <asp:Label ID="Label68" runat="server" SkinID="etiqueta_negra" 
                Text="Material sobrante:"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:TextBox ID="txtMatSobMovTierLineal" runat="server" width="99%"
                SkinID="texto_sintamano" ></asp:TextBox>
            <cc1:MaskedEditExtender ID="MaskedEditExtender6" Mask="99999999.9999" runat="server" MaskType="Number" TargetControlID="txtMatSobMovTierLineal"  AutoComplete="false" />                                
                <asp:CompareValidator runat="server" ID="CompareValidator30" ControlToValidate="txtMatSobMovTierLineal" 
                    ValidationGroup="MovTierrasInfracLineal" Display="Dynamic" 
                    ErrorMessage="El Material Sobrante debe ser un valor númerico" 
                    Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                </td>
        </tr>
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregarMovTierrasInfracL" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="MovTierrasInfracLineal" 
                    onclick="btnAgregarMovTierrasInfracL_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarMovTierraInfracLineal" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarMovTierraInfracLineal_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
         <tr>
            <td colspan="5">
                <asp:ValidationSummary ID="ValidationSummary7" runat="server" 
                    ValidationGroup="MovTierrasInfracLineal" />
            </td>
        </tr>  
        </asp:PlaceHolder>
        
        <tr>
            <td colspan="5">
                <asp:GridView runat="server" ID="grvMovTierraInfracLinealProy" AutoGenerateColumns="False"
                    EmptyDataText="No ha agregado registros de infraestructura lineal al proyecto" 
                    onrowdeleted="grvMovTierraInfracLinealProy_RowDeleted" 
                    onrowdeleting="grvMovTierraInfracLinealProy_RowDeleting" 
                    SkinID="Grilla_simple">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField HeaderText="Corte del Movimiento de Tierra"  DataField ="EML_MOV_TIERRAS_CORT"/>
                        <asp:BoundField HeaderText="Relleno del Movimiento de Tierra" 
                            DataField="EML_MOV_TIERRAS_RELLENO" />
                        <asp:BoundField HeaderText="Material Sobrante del Movimiento de Tierra" 
                            DataField="EML_MOV_TIERRAS_MAT_SOBR" />
                        <asp:BoundField HeaderText="Infraestructura Asociada" 
                            DataField="EIL_TIPO_INFRAC_ASOC_L" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan = "5"  style="width: 100%"></td>
        </tr>
        <tr>
            <td colspan = "5" style="width: 100%" class="titleUpdate"></td>
        </tr>
        <tr>
            <td width="30%">2.3.2 Infraestructura No Lineal</td>
            <td width="80%" align="right" colspan="4">
                <asp:Button ID="btnMovTierraInfracNoLineal" runat="server" SkinID="boton_copia"
                    Text="Nuevo movimiento de tierras para Infraestructura No Lineal" 
                    onclick="btnMovTierraInfracNoLineal_Click"  />
            </td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="5">
                </td>
        </tr>
        <asp:PlaceHolder ID ="plhMovTierraInfracNoLineal" Visible ="false" runat ="server">
        
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="Label54" runat="server" SkinID="etiqueta_negra" 
                Text="Infraestructura Asociada:"></asp:Label></td>
            <td width="60%" colspan = "3">
                <asp:DropDownList ID="cboInfracAsocMovTierNoLineal" runat="server" SkinID="lista_desplegable"
                    AutoPostBack="True">
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator12" runat="server"
                    ErrorMessage="Seleccione la infraestructura asociada al movimiento de tierras" 
                    Display="Dynamic"
                    ControlToValidate="cboInfracAsocMovTierNoLineal" ValueToCompare="-1" 
                    Operator="NotEqual"
                    ValidationGroup="MovTierrasInfracNoLineal">*</asp:CompareValidator>            
                <asp:TextBox ID="txtInfracAsocMovTierNoLineal" runat="server"  width="99%"
                SkinID="texto_sintamano" Visible="false" Text=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  colspan="5">
                <asp:Label ID="Label55" runat="server" SkinID="etiqueta_negra" 
                Text="Movimiento de tierras:"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="Label56" runat="server" SkinID="etiqueta_negra" 
                Text="Corte:"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:TextBox ID="txtCorteMovTierNoLineal" runat="server" width="99%"
                SkinID="texto_sintamano"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                    ErrorMessage="Ingrese el corte del movimiento de tierra"
                    Display="Dynamic" ControlToValidate="txtCorteMovTierNoLineal"
                    ValidationGroup="MovTierrasInfracNoLineal">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender7" Mask="99999999.9999" runat="server" MaskType="Number" TargetControlID="txtCorteMovTierNoLineal"  AutoComplete="false" />                                
                <asp:CompareValidator runat="server" ID="CompareValidator13" ControlToValidate="txtCorteMovTierNoLineal" 
                    ValidationGroup="MovTierrasInfracNoLineal" Display="Dynamic" 
                    ErrorMessage="El corte del movimiento de tierra debe ser un valor númerico" 
                    Operator="DataTypeCheck" Type="Double">*</asp:CompareValidator></td>
        </tr>
        <tr>        
            <td width="40%" colspan="2">
                <asp:Label ID="Label57" runat="server" SkinID="etiqueta_negra" 
                Text="Relleno:"></asp:Label></td>
            <td width="60%"  colspan="3">
                <asp:TextBox ID="txtRellenoMovTierNoLineal" runat="server" width="99%"
                SkinID="texto_sintamano"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                    ErrorMessage="Ingrese el relleno del movimiento de tierra"
                    Display="Dynamic" ControlToValidate="txtRellenoMovTierNoLineal"
                    ValidationGroup="MovTierrasInfracNoLineal">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender8" Mask="99999999.9999" runat="server" MaskType="Number" TargetControlID="txtRellenoMovTierNoLineal"  AutoComplete="false" />                                
                <asp:CompareValidator runat="server" ID="CompareValidator14" ControlToValidate="txtRellenoMovTierNoLineal" 
                    ValidationGroup="MovTierrasInfracNoLineal" Display="Dynamic" 
                    ErrorMessage="El relleno del movimiento de tierra debe ser un valor númerico" 
                    Operator="DataTypeCheck" Type="Double">*</asp:CompareValidator></td>
        </tr>
        <tr>            
            <td width="40%" colspan="2">
                <asp:Label ID="Label58" runat="server" SkinID="etiqueta_negra" 
                Text="Material sobrante:"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:TextBox ID="txtMatSobMovTierNoLineal" runat="server" width="99%"
                SkinID="texto_sintamano" ></asp:TextBox>
            <cc1:MaskedEditExtender ID="MaskedEditExtender9" Mask="99999999.9999" runat="server" MaskType="Number" TargetControlID="txtMatSobMovTierNoLineal"  AutoComplete="false" />                                
                  <asp:CompareValidator runat="server" ID="CompareValidator29" ControlToValidate="txtMatSobMovTierNoLineal" 
                    ValidationGroup="MovTierrasInfracNoLineal" Display="Dynamic" 
                    ErrorMessage="El Material Sobrante debe ser numerico:" 
                    Operator="DataTypeCheck" Type="Double">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td align="center" width="25%"></td>
            <td align="center" style="width: 25%">
                <asp:Button ID="btnAgregarMovTierrasInfracNoL" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="MovTierrasInfracNoLineal" 
                    onclick="btnAgregarMovTierrasInfracNoL_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelarMovTierraInfracLineaNol" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarMovTierraInfracNoLineal_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:ValidationSummary ID="ValidationSummary8" runat="server" 
                    ValidationGroup="MovTierrasInfracNoLineal" />
            </td>
        </tr>  
        </asp:PlaceHolder>
        <tr>
            <td colspan="5">
                <asp:GridView runat="server" ID="grvMovTierraInfracNoLinealProy" AutoGenerateColumns="False"                
                    
                    EmptyDataText="No ha agregado registros de infraestructura no lineal al proyecto" 
                    onrowdeleting="grvMovTierraInfracNoLinealProy_RowDeleting" 
                    SkinID="Grilla_simple">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField HeaderText="Corte del Movimiento de Tierra"  DataField ="EMN_MOV_TIERRAS_CORT"/>
                        <asp:BoundField HeaderText="Relleno del Movimiento de Tierra" 
                            DataField="EMN_MOV_TIERRAS_RELLENO" />
                        <asp:BoundField HeaderText="Material Sobrante del Movimiento de Tierra" 
                            DataField="EMN_MOV_TIERRAS_MAT_SOBR" />
                        <asp:BoundField HeaderText="Infraestructura Asociada" 
                            DataField="EIN_TIPO_INFRAC_ASOC_NL" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan = "5" width="75%" style="width: 100%"></td>
        </tr>
        <tr>
            <td colspan = "5" width="75%" style="width: 100%" class="titleUpdate"></td>
        </tr>
        <tr>
            <td colspan = "2" width="40%">2.3.3 Áreas de Disposición de Material</td>
            <td width="60%" align="right" colspan="3">
                <asp:Button ID="btnNuevaAreaDispMaterial" runat="server" SkinID="boton_copia"
                    Text="Nueva Área de Disposición de Material" 
                    onclick="btnNuevaAreaDispMaterial_Click"  />
            </td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="5">
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhAreasDispoMatProy" Visible="false">
        
        <tr>
            <td width="25%">
                <asp:Label ID="Label59" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre:"></asp:Label></td>
            <td width="100%" colspan="4">
                <asp:TextBox ID="txtNombAreasDispoMatProy" runat="server" width="99%"
                SkinID="texto_sintamano"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                    ErrorMessage="Ingrese el nombre del área de disposición de material"
                    Display="Dynamic" ControlToValidate="txtNombAreasDispoMatProy"
                    ValidationGroup="AreasDispoMatProy">*</asp:RequiredFieldValidator>
           </td>
        </tr>
        <tr>
            <td colspan="2" width="50%">
                <asp:Label ID="Label1" runat="server" SkinID="etiqueta_negra" 
                Text="Coordenadas de Inicio (m):"></asp:Label>
            </td>
            <td colspan="3" width="100%">
                <uc3:ctrCoordenadas ID="ctrCoordenadasAreaMaterial" runat="server" />
            </td>
        </tr>
       
        <tr>
            <td align="center" width="25%" style="height: 26px"></td>
            <td align="center" style="width: 25%; height: 26px">
                <asp:Button ID="btnAgregarAreasDispoMatProy" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="AreasDispoMatProy" 
                    onclick="btnAgregarAreasDispoMatProy_Click" />
            </td>
            <td align="center" width="25%" style="height: 26px">
                <asp:Button ID="btnCancelarAreasDispoMatProy" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarAreasDispoMatProy_Click" />
            </td>
            <td align="center" width="25%" colspan="2" style="height: 26px"></td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:ValidationSummary ID="ValidationSummary6" runat="server" 
                    ValidationGroup="AreasDispoMatProy" />
            </td>
        </tr>  
        
        </asp:PlaceHolder>
        <tr>
            <td colspan="5">
                <asp:GridView runat="server" ID="grvAreasDispoMatProy" AutoGenerateColumns="False"
                
                    
                    EmptyDataText="No ha agregado registros de áreas de disposicón de material" 
                    SkinID="Grilla_simple" Width="100%">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField HeaderText="Codigo" DataField="EAD_ID" Visible="False" />
                        <asp:BoundField HeaderText="Nombre" DataField="EAD_NOMBRE_AREA" />
                       <asp:TemplateField HeaderText="Coordenadas">
                            <ItemTemplate >
                                <uc3:ctrCoordenadas DataGridObject="true" NombreTabla="EIH_COOR_DISP_MATER" NombreCampo="EAD_ID" ValorCampo='<%# Eval("EAD_ID") %>' ValorCampo2='<%# Eval("EAD_ID") %>' ID="cregrvCoordenadasNo2" runat="server"/>
                            </ItemTemplate>
                        </asp:TemplateField>     
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2" width="40%">
                <asp:Label ID="Label74" runat="server" SkinID="etiqueta_negra" 
                Text="Volumen total de corte (m3):"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:TextBox ID="txtVolTotCorteMovTierra" runat="server" width="99%"
                SkinID="texto_sintamano"></asp:TextBox>
            <cc1:MaskedEditExtender ID="MaskedEditExtender10" Mask="99999999.9999" runat="server" MaskType="Number" TargetControlID="txtVolTotCorteMovTierra"  AutoComplete="false" />                                
                <asp:CompareValidator runat="server" ID="CompareValidator15" 
                ControlToValidate="txtVolTotCorteMovTierra" 
                    ValidationGroup="Tab2_3" Display="Dynamic" 
                    ErrorMessage="El Volumen del corte del movimiento de tierra debe ser un valor númerico" 
                    Operator="DataTypeCheck" Type="Double">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td colspan="2" width="40%">
                <asp:Label ID="Label75" runat="server" SkinID="etiqueta_negra" 
                Text="Volumen total de relleno (m3):"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:TextBox ID="txtVolTotRellenoMovTierra" runat="server" width="99%"
                SkinID="texto_sintamano"></asp:TextBox>
            <cc1:MaskedEditExtender ID="MaskedEditExtender11" Mask="99999999.9999" runat="server" MaskType="Number" TargetControlID="txtVolTotRellenoMovTierra"  AutoComplete="false" />                                
                <asp:CompareValidator runat="server" ID="CompareValidator16" 
                ControlToValidate="txtVolTotRellenoMovTierra" 
                    ValidationGroup="Tab2_3" Display="Dynamic" 
                    ErrorMessage="El volumen del relleno del movimiento de tierra debe ser un valor númerico" 
                    Operator="DataTypeCheck" Type="Double">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td colspan="2" width="40%">
                <asp:Label ID="Label76" runat="server" SkinID="etiqueta_negra" 
                Text="Material Total Sobrante (m3):"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:TextBox ID="txtVolTotSobranteMovTierra" runat="server" width="99%"
                SkinID="texto_sintamano"></asp:TextBox>
                <cc1:MaskedEditExtender ID="MaskedEditExtender19" Mask="99999999.9999" runat="server" MaskType="Number" TargetControlID="txtVolTotSobranteMovTierra"  AutoComplete="false" />                                
                <asp:CompareValidator runat="server" ID="CompareValidator31" 
                ControlToValidate="txtVolTotSobranteMovTierra" 
                    ValidationGroup="Tab2_3" Display="Dynamic" 
                    ErrorMessage="El Material Total Sobrante debe ser un valor númerico" 
                    Operator="DataTypeCheck" Type="Double">*</asp:CompareValidator></td>
            </td>
        </tr>
        <tr>
            <td colspan="5" align="right">
              <asp:Button ID="btnAsignarVal" runat="server" SkinID="boton_copia"
                    Text="Asignar Valores" ValidationGroup="Tab2_3"
                    onclick="btnAsignarVal_Click"/>
            </td>
        </tr>      
        <tr>
            <td colspan="5">
                <asp:ValidationSummary ID="ValidationSummary10" runat="server" 
                    ValidationGroup="Tab2_3" />
            </td>
        </tr>  
        <tr>
            <td class="titleUpdate" colspan="5">
            </td>
        </tr>        
        <tr>
            <td colspan = "5" width="100%" style="height: 23px">2.4 Información Adicional del Proyecto</td>
        </tr>
        <tr>
            <td colspan = "5" width="100%" class="titleUpdate"></td>
        </tr>
        <tr>
            <td width="20%">2.4.1 Infraestructuras y Servicios Interceptados por el Proyecto
</td>
            <td width="80%" align="right" colspan="4">
                <asp:Button ID="btnInfracIntercProy" runat="server" SkinID="boton_copia"
                    Text="Nueva Infraestructura o Servicio Interceptados" 
                    onclick="btnInfracIntercProy_Click"/>
            </td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="5">
            </td>
        </tr>
        <asp:PlaceHolder runat="server" ID="plhInfracIntercProy" Visible="false">
        
        <tr>
            <td width="20%" colspan="2">
                <asp:Label ID="Label77" runat="server" SkinID="etiqueta_negra" 
                Text="Infraestructura Asociada:"></asp:Label></td>
            <td width="60%" colspan = "3">
                <asp:DropDownList ID="cboTipoInfracIntercProy" runat="server" SkinID="lista_desplegable"
                    AutoPostBack="True" OnSelectedIndexChanged="cboTipoInfracIntercProy_SelectedIndexChanged">
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator17" runat="server"
                    ErrorMessage="Seleccione el tipo de infraestructura interceptada por el proyecto" 
                    Display="Dynamic"
                    ControlToValidate="cboTipoInfracIntercProy" ValueToCompare="-1" 
                    Operator="NotEqual"
                    ValidationGroup="InfracIntercProy">*</asp:CompareValidator>                                
                <asp:TextBox ID="txtTipoInfracIntercProy" runat="server"  width="99%"
                SkinID="texto_sintamano" Visible="false" Text=""></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="Label78" runat="server" SkinID="etiqueta_negra" 
                Text="Longitud (m):"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:TextBox ID="txtLongInfracIntercProy" runat="server" width="99%"
                SkinID="texto_sintamano"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                    ErrorMessage="Longitud de la infraestructura interceptada por el proyecto es requerida"
                    Display="Dynamic" ControlToValidate="txtLongInfracIntercProy"
                    ValidationGroup="InfracIntercProy">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender12" Mask="99999999.9999" runat="server" MaskType="Number" TargetControlID="txtLongInfracIntercProy"  AutoComplete="false" />                                
                <asp:CompareValidator runat="server" ID="CompareValidator18" ControlToValidate="txtLongInfracIntercProy" 
                    ValidationGroup="InfracIntercProy" Display="Dynamic" 
                    ErrorMessage="Ancho de la infraestructura interceptada por el proyecto debe ser un valor númerico" 
                    Operator="DataTypeCheck" Type="Double">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="Label79" runat="server" SkinID="etiqueta_negra" 
                Text="Ancho (m):"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:TextBox ID="txtAncInfracIntercProy" runat="server" width="99%"
                SkinID="texto_sintamano"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                    ErrorMessage="Ancho de la infraestructura interceptada por el proyecto es requerida"
                    Display="Dynamic" ControlToValidate="txtAncInfracIntercProy"
                    ValidationGroup="InfracIntercProy">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender13" Mask="99999999.9999" runat="server" MaskType="Number" TargetControlID="txtAncInfracIntercProy"  AutoComplete="false" />                                
                <asp:CompareValidator runat="server" ID="CompareValidator19" ControlToValidate="txtAncInfracIntercProy" 
                    ValidationGroup="InfracIntercProy" Display="Dynamic" 
                    ErrorMessage="Ancho de la infraestructura interceptada por el proyecto debe ser un valor númerico" 
					Operator="DataTypeCheck" Type="Double">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="Label3" runat="server" SkinID="etiqueta_negra" 
                Text="Coordenadas de ubicación:"></asp:Label>
            </td>
            <td width="60%" colspan="3">
                <uc2:ctrCoordenadasPto runat="server" ID="ctrCoorUbiInfracIntercProy" />
            </td>
        </tr>
 
        <tr>
            <td align="center" width="20%"></td>
            <td align="center" width="20%">
                <asp:Button ID="btnAgregarInfracIntercProy" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="InfracIntercProy" 
                    onclick="btnAgregarInfracIntercProy_Click" />
            </td>
            <td align="center" width="20%">
                <asp:Button ID="btnCancelarInfracIntercProy" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarInfracIntercProy_Click" />
            </td>
            <td align="center" width="20%"></td>
            <td align="center" width="20%"></td>
        </tr>
        </asp:PlaceHolder>
        <tr>
            <td colspan="5">
                <asp:GridView runat="server" ID="grvInfracIntercProy" AutoGenerateColumns="False"
                
                    
                    EmptyDataText="No ha agregado registros de áreas de disposicón de material" 
                    onrowdeleting="grvInfracIntercProy_RowDeleting" SkinID="Grilla_simple" 
                    Width="100%">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField HeaderText="Tipo" DataField="EII_TIPO_INFRAC_INTERCEP" />
                        <asp:BoundField HeaderText="Longitud (m)" DataField="EIA_LONGITUD" />
                        <asp:BoundField HeaderText="Ancho (m)" DataField="EIA_ANCHO" />
                        <asp:BoundField HeaderText="Coordenada norte de ubicación" 
                            DataField="EIA_COOR_NORTE_UBICACION" />
                        <asp:BoundField HeaderText="Coordenada este de ubicación" 
                            DataField="EIA_COOR_ESTE_UBICACION" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan = "5" width="75%" style=" width: 100%;"></td>
        </tr>
        <tr>
            <td colspan = "5" width="75%" style=" width: 100%;" class="titleUpdate"></td>
        </tr>
        <tr>
            <td  width="20%">2.4.2 Asentamientos humanos e infraestructuras sociales, culturales y económicas
            </td>
            <td width="80%" align="right" colspan="4" >
                <asp:Button ID="btnNuevoAsentInfrac" runat="server" SkinID="boton_copia"
                    Text="Nuevo Asentamientos humanos e infraestructuras" 
                    onclick="btnNuevoAsentInfrac_Click"  />
            </td>
        </tr>
        <tr>
            <td class="titleUpdate" colspan="5">
            </td>
        </tr>
         <asp:PlaceHolder runat="server" ID="plhAsentInfrac" Visible="false">         
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="Label87" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad de Longitud:"></asp:Label></td>
            <td width="60%" colspan = "3">
                <asp:DropDownList ID="cboUnidLongAsentInfrac" runat="server" SkinID="lista_desplegable"
                    AutoPostBack="True" 
                    onselectedindexchanged="cboUnidLongAsentInfrac_SelectedIndexChanged">
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator23" runat="server"
                    ErrorMessage="Seleccione la unidad de longitud" 
                    Display="Dynamic"
                    ControlToValidate="cboUnidLongAsentInfrac" ValueToCompare="-1" 
                    Operator="NotEqual"
                    ValidationGroup="AsentInfrac">*</asp:CompareValidator></td>
        </tr>
       
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="Label82" runat="server" SkinID="etiqueta_negra" 
                Text="Nombre:"></asp:Label></td>
            <td width="60%" colspan = "3">
               <asp:TextBox ID="txtNombreAsentInfrac" runat="server" width="99%"
                SkinID="texto_sintamano"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                    ErrorMessage="Nombre es requerido"
                    Display="Dynamic" ControlToValidate="txtNombreAsentInfrac"
                    ValidationGroup="AsentInfrac">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="lbldisAsesnt" runat="server" SkinID="etiqueta_negra" 
                Text="Distancia ():"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:TextBox ID="txtDistanciaAsentInfrac" runat="server" width="99%"
                SkinID="texto_sintamano"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                    ErrorMessage="La información de Distancia es requerida"
                    Display="Dynamic" ControlToValidate="txtDistanciaAsentInfrac"
                    ValidationGroup="AsentInfrac">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender14" Mask="99999999.9999" runat="server" MaskType="Number" TargetControlID="txtDistanciaAsentInfrac"  AutoComplete="false" />                                
                <asp:CompareValidator runat="server" ID="CompareValidator21" ControlToValidate="txtDistanciaAsentInfrac" 
                    ValidationGroup="AsentInfrac" Display="Dynamic" 
                    ErrorMessage="La distancia debe ser un valor númerico" 
                    Operator="DataTypeCheck" Type="Double">*</asp:CompareValidator></td>
        </tr><tr>
            <td width="40%" colspan="2">
                <asp:Label ID="Label84" runat="server" SkinID="etiqueta_negra" 
                Text="¿Intervenir?"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:DropDownList ID="cboIntervenirAsentInfrac" runat="server" SkinID="lista_desplegable">
                    <asp:ListItem Selected="True" Text="Seleccione..." Value="-1"></asp:ListItem>
                    <asp:ListItem Text="Si" Value="S"></asp:ListItem>
                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator20" runat="server"
                    ErrorMessage="Seleccione si deben ser intervenidos" 
                    Display="Dynamic"
                    ControlToValidate="cboIntervenirAsentInfrac" ValueToCompare="-1" 
                    Operator="NotEqual"
                    ValidationGroup="AsentInfrac">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="Label85" runat="server" SkinID="etiqueta_negra" 
                Text="Cercanos o Afectados por el Proyecto"></asp:Label>
            </td><td width="60%" colspan="3">
                <asp:TextBox ID="txtCercanAsentInfrac" runat="server" width="99%"
                SkinID="texto_sintamano"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                    ErrorMessage="La información de Cercanos o Afectados por el Proyecto es requerida"
                    Display="Dynamic" ControlToValidate="txtCercanAsentInfrac"
                    ValidationGroup="AsentInfrac">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="center" width="20%"></td>
            <td align="center" width="20%">
                <asp:Button ID="btnAgregarAsentInfrac" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="AsentInfrac" 
                    onclick="btnAgregarAsentInfrac_Click" />
            </td>
            <td align="center" width="20%">
                <asp:Button ID="btnCancelarAsentInfrac" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarAsentInfrac_Click" />
            </td>
            <td align="center" width="20%"></td>
            <td align="center" width="20%"></td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:ValidationSummary ID="ValidationSummary9" runat="server" 
                    ValidationGroup="AsentInfrac" />
            </td>
        </tr>  
        </asp:PlaceHolder>
        <tr>
            <td colspan="5">
                <asp:GridView runat="server" ID="grvAsentInfrac" AutoGenerateColumns="False"
                
                    
                    EmptyDataText="No ha agregado registros de asentamientos e infraestructura" 
                    onrowdeleting="grvAsentInfrac_RowDeleting" SkinID="Grilla_simple" 
                    Width="100%">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField HeaderText="Nombre" DataField="EAH_NOMBRE" />
                        <asp:BoundField HeaderText="Distancia ()" DataField="EAH_DISTANCIA" />
                        <asp:BoundField HeaderText="¿Intervenir?" DataField="EAH_INTERVENCION" />
                        <asp:BoundField HeaderText="Cercanos o Afectados por el Proyecto" 
                            DataField="EAH_AFECTADOS_PROYECTO" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan = "5" width="100%"></td>

        </tr>
        <tr>
            <td colspan = "5" width="100%" class="titleUpdate"></td>

        </tr>
        <tr>
            <td colspan = "4" width="100%" style="height: 23px">2.4.3 Estimación de Recursos de Mano de Obra Prevista
            </td>

        </tr>
        <tr>
            <td class="titleUpdate" colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan ="2" width="40%">
                <asp:Label ID="Label86" runat="server" SkinID="etiqueta_negra" 
                Text="Cantidad de Mano de Obra Calificada:"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:TextBox ID="txtCantManoObraCalf" runat="server" width="99%"
                SkinID="texto_sintamano"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" 
                    ErrorMessage="La información de Cantidad de Mano de Obra Calificada es requerida"
                    Display="Dynamic" ControlToValidate="txtCantManoObraCalf"
                    ValidationGroup="Tab2_4">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender15" Mask="99999999.9999" runat="server" MaskType="Number" TargetControlID="txtCantManoObraCalf"  AutoComplete="false" />                                
                <asp:CompareValidator runat="server" ID="CompareValidator22" 
                    ControlToValidate="txtCantManoObraCalf" 
                    ValidationGroup="Tab2_4" Display="Dynamic" 
                    ErrorMessage="La información de Cantidad de Mano de Obra Calificada debe ser un valor númerico" 
                    Operator="DataTypeCheck" Type="Double">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td colspan ="2" width="40%">
                <asp:Label ID="Label88" runat="server" SkinID="etiqueta_negra" 
                Text="Cantidad de Mano de Obra No Calificada:"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:TextBox ID="txtCantManoObraNoCalf" runat="server" width="99%"
                SkinID="texto_sintamano"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" 
                    ErrorMessage="La información de Cantidad de Mano de Obra no Calificada es requerida"
                    Display="Dynamic" ControlToValidate="txtCantManoObraNoCalf"
                    ValidationGroup="Tab2_4">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender16" Mask="99999999.9999" runat="server" MaskType="Number" TargetControlID="txtCantManoObraNoCalf"  AutoComplete="false" />                                
                <asp:CompareValidator runat="server" ID="CompareValidator24" ControlToValidate="txtCantManoObraNoCalf" 
                    ValidationGroup="Tab2_4" Display="Dynamic" 
                    ErrorMessage="La información de Cantidad de Mano de Obra no Calificada debe ser un valor númerico" 
                    Operator="DataTypeCheck" Type="Double">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td colspan="5" align="right">
              <asp:Button ID="Button1" runat="server" SkinID="boton_copia"
                    Text="Asignar Valores" ValidationGroup="Tab2_4"
                    onclick="btnAsignarVal3_Click"/>
            </td>
        </tr>      
        <tr>
            <td colspan="5">
                <asp:ValidationSummary ID="ValidationSummary11" runat="server" 
                    ValidationGroup="Tab2_4" />
            </td>
        </tr>  
        <tr>
            <td colspan = "5" width="100%" class="titleUpdate"></td>

        </tr>
        <tr>
            <td colspan = "5"  >2.4.4 Otra Información
            </td>

        </tr>
        <tr>
            <td class="titleUpdate" colspan="5">
            </td>
        </tr>
        <tr>
            <td colspan ="2" width="40%">
                <asp:Label ID="Label89" runat="server" SkinID="etiqueta_negra" 
                Text="Costo del Proyecto (Col $):"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:TextBox ID="txtCostoProyecto" runat="server" width="99%"
                SkinID="texto_sintamano"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" 
                    ErrorMessage="La información de costo del proyecto es requerida"
                    Display="Dynamic" ControlToValidate="txtCostoProyecto"
                    ValidationGroup="Tab2_5">*</asp:RequiredFieldValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender17" Mask="99999999.9999" runat="server" MaskType="Number" TargetControlID="txtCostoProyecto"  AutoComplete="false" />                                
                <asp:CompareValidator runat="server" ID="CompareValidator25" 
                    ControlToValidate="txtCostoProyecto" 
                    ValidationGroup="Tab2_5" Display="Dynamic" 
                    ErrorMessage="La información de costo del proyecto debe ser un valor númerico" 
                    Operator="DataTypeCheck" Type="Double">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td  colspan="5">
                <asp:Label ID="Label91" runat="server" SkinID="etiqueta_negra" 
                Text="Duración del Proyecto:"></asp:Label></td>
        </tr>
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="Label92" runat="server" SkinID="etiqueta_negra" 
                Text="Unidad:"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:DropDownList ID="cboUnidadDurProyecto" runat="server" Font-Size="8.5pt" Font-Names = "Tahoma">
                </asp:DropDownList><asp:CompareValidator ID="CompareValidator28" runat="server"
                    ErrorMessage="Seleccione la unidad de longitud" 
                    Display="Dynamic"
                    ControlToValidate="cboUnidadDurProyecto" ValueToCompare="-1" 
                    Operator="NotEqual"
                    ValidationGroup="Tab2_5">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td width="40%" colspan="2">
                <asp:Label ID="Label5" runat="server" SkinID="etiqueta_negra" 
                Text="Duración Proyecto:"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:TextBox ID="txtDuracionProyecto" runat="server" width="99%"
                SkinID="texto_sintamano"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" 
                    ErrorMessage="La información de la duración del proyecto es requerida"
                    Display="Dynamic" ControlToValidate="txtDuracionProyecto"
                    ValidationGroup="Tab2_5">*</asp:RequiredFieldValidator>
            <cc1:MaskedEditExtender ID="MaskedEditExtender18" Mask="99999999.9999" runat="server" MaskType="Number" TargetControlID="txtDuracionProyecto"  AutoComplete="false" />                                
                <asp:CompareValidator runat="server" ID="CompareValidator27" ControlToValidate="txtDuracionProyecto" 
                    ValidationGroup="Tab2_5" Display="Dynamic" 
                    ErrorMessage="La información de la duración del proyecto debe ser un valor númerico" 
                    Operator="DataTypeCheck" Type="Double">*</asp:CompareValidator></td>
        </tr>
        <tr>
            <td colspan ="2" width="40%">
                <asp:Label ID="Label90" runat="server" SkinID="etiqueta_negra" 
                Text="Monto Base para Inversión del 1% (Col $):"></asp:Label></td>
            <td width="60%" colspan="3">
                <asp:TextBox ID="txtCostoBaseInversion" runat="server" width="99%"
                SkinID="texto_sintamano"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" 
                    ErrorMessage="La información de Monto Base para Inversión del 1% es requerida"
                    Display="Dynamic" ControlToValidate="txtCostoBaseInversion"
                    ValidationGroup="Tab2_5">*</asp:RequiredFieldValidator>
               <cc1:MaskedEditExtender ID="MaskedEditExtender2" Mask="99999999.9999" runat="server" MaskType="Number" TargetControlID="txtCostoBaseInversion"  AutoComplete="false" />                                
                <asp:CompareValidator runat="server" ID="CompareValidator26" ControlToValidate="txtCostoBaseInversion" 
                    ValidationGroup="Tab2_5" Display="Dynamic" 
                    ErrorMessage="La información de Cantidad de Monto Base para Inversión del 1% debe ser un valor númerico" 
                    Operator="DataTypeCheck" Type="Double">*</asp:CompareValidator></td>
        </tr>          
        <tr>
            <td colspan="5" align="right">
              <asp:Button ID="Button2" runat="server" SkinID="boton_copia"
                    Text="Asignar Valores" ValidationGroup="Tab2_5"
                    onclick="btnAsignarVal4_Click"/>
            </td>
        </tr>      
        <tr>
            <td colspan="5">
                <asp:ValidationSummary ID="ValidationSummary12" runat="server" 
                    ValidationGroup="Tab2_5" />
            </td>
        </tr>  
    </table>
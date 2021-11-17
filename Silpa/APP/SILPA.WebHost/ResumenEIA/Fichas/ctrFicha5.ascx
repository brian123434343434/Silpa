<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrFicha5.ascx.cs" Inherits="ResumenEIA_Fichas_Ficha5" %>



<%@ Register src="ctrFicha5Insersion.ascx" tagname="ctrFicha5Insersion" tagprefix="uc1" %>
<style type="text/css">
   
    .estiloSeleccion
    {
        font-family: Arial, Helvetica, sans-serif;
        font-style: italic;
        font-size: x-small;
        font-weight: bold;
    }
</style>



<table style="width:100%;" >
    <tr>
        <td class="etiqueta_negra" colspan="3">
            <asp:Label ID="Label1" runat="server" 
                Text="5. EVALUACION AMBIENTAL"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="titleUpdate" colspan="3">
            &nbsp;</td>
    </tr>
    <tr>
        <td  colspan="2">
            <asp:Label ID="Label2" runat="server" 
                Text="Seleccione tipò de evaluacion ambiental:   "></asp:Label>
            <asp:DropDownList ID="cboTipoEvaluacion" runat="server" AutoPostBack="True" 
                onselectedindexchanged="cboTipoEvaluacion_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:Button ID="btnAgregar" runat="server" Text="Agregar evaluacion" 
                onclick="btnAgregar_Click" SkinID="boton_copia" />
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td  colspan="2">
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="3">
                    <asp:PlaceHolder ID="plhInsersion" runat="server" Visible="False"> 
        <table width="100%" >

    <tr>
        <td colspan="2" style="text-align: center; ">
            &nbsp;</td>
        <td style="text-align: center; "  >
            &nbsp;</td>
        <td style="text-align: center; " >
            &nbsp;</td>
    </tr>
    <tr>
        <td  valign="top" >
                        <asp:Label ID="lblCodigo" runat="server" SkinID="etiqueta_negra" 
                            Text="Codigo del Mapa"  Width="150px"></asp:Label>
                        </td>
        <td  valign="top" >
                            <asp:TextBox ID="txtCodigo" runat="server" SkinID="texto_sintamano" 
                                ></asp:TextBox>
                            </td>
        <td  valign="top"  >
                            <asp:Label ID="lbl2" runat="server" SkinID="etiqueta_negra" 
                                Text="Unidad Geotecnica a intervenir" Width="200px"></asp:Label>
                            </td>
        <td valign="top" >
                            <asp:TextBox ID="txt2" runat="server" SkinID="texto_sintamano" 
                                ></asp:TextBox>
                            </td>
    </tr>
    <tr>
        <td  >
                            <asp:Label ID="lbl3" runat="server" SkinID="etiqueta_negra" 
                                Text="Actividades del Proyecto que lo Interviene" ></asp:Label>
                            </td>
        <td >
                            <asp:TextBox ID="txt3" runat="server" SkinID="texto_sintamano" 
                                 ></asp:TextBox>
                            </td>
        <td  >
                            <asp:Label ID="lbl4" runat="server" SkinID="etiqueta_negra" 
                                Text="Area a intervenir"  Width="100px"></asp:Label></td>
        <td >
                            <asp:TextBox ID="txt4" runat="server" SkinID="texto_sintamano" ></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td  >
                            <asp:Label ID="lbl5" runat="server" SkinID="etiqueta_negra" 
                                Text="% area total a intervenir"></asp:Label></td>
        <td colspan="1" >
                            <asp:TextBox ID="txt5" runat="server" SkinID="texto_sintamano" ></asp:TextBox></td>
        <td  >
                            <asp:Label ID="lbl6" runat="server" SkinID="etiqueta_negra" 
                                Text="Impacto potencial al generar" ></asp:Label></td>
        <td >
                            <asp:TextBox ID="txt6" runat="server" SkinID="texto_sintamano" ></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td  >
                            <asp:Label ID="lbl7" runat="server" Text="Tipo de Impacto" 
                                 SkinID="etiqueta_negra"></asp:Label></td>
        <td colspan="1" >
                            <asp:DropDownList ID="cbo7" runat="server" SkinID="lista_desplegable" 
                                 Height="16px" >
                            </asp:DropDownList></td>
        <td style="text-align: center"  >
          <asp:Button ID="btnRegistrar" runat="server"  SkinID="boton_copia" 
                Text="Agregar" onclick="btnRegistrar_Click" Visible="true" />
        </td>
        <td style="text-align: left">
          <asp:Button ID="btnCancelar" runat="server"  SkinID="boton_copia" 
                Text="Cancelar" onclick="btnCancelar_Click" Visible="true" 
                ValidationGroup="Tab5" style="text-align: center"/>
        </td>
    </tr>
    </table>
    </asp:PlaceHolder>
       
        </td>
    </tr>
    <tr>
        <td colspan="3">
            </td>
    </tr>
    <tr>
        <td  colspan="2">
            <span>5.1 geotecnia</span></td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td  colspan="3" class="titleUpdate">
            </td>
    </tr>
    <tr>
        <td  colspan="3">
        <asp:GridView ID="grid1" runat="server" AutoGenerateColumns="False"
             
                EmptyDataText="la seccion geotecnia no tiene registros asociados" 
                onselectedindexchanged="grid1_SelectedIndexChanged" 
                onrowdeleting="grid1_RowDeleting">
            <Columns>
                <asp:CommandField ShowDeleteButton="True" />
                <asp:BoundField DataField="ETV_TIPOS_EVALUACIONES_AMB" 
                    HeaderText="Tipo de evaluacion" />
                <asp:BoundField AccessibleHeaderText="Codigo del Mapa" DataField="EEA_COD_MAPA" 
                    HeaderText="Codigo del Mapa" />
                <asp:BoundField AccessibleHeaderText="Unidad Geotecnica a Intervenir" DataField="EEA_INFO_EVAL"
                    HeaderText="Unidad Geotecnica a Intervenir" />
                <asp:BoundField AccessibleHeaderText="Infraestructura del Proyecto que la Intervienen"
                    DataField="EEA_INFRAC_INTERVIENE" 
                    HeaderText="Infraestructura del Proyecto que la Intervienen" />
                <asp:BoundField AccessibleHeaderText="Area (Ha) Intervenir" DataField="EEA_AREA"
                    HeaderText="Area (Ha) Intervenir" />
                <asp:BoundField AccessibleHeaderText="% del Area Total a Intervenir" DataField="EEA_PORC_AREA_TOTAL"
                    HeaderText="% del Area Total a Intervenir" />
                <asp:BoundField AccessibleHeaderText="Impacto Potencial a Generar" DataField="EEA_IMPACTO_POTENCIAL"
                    HeaderText="Impacto Potencial a Generar" />
                <asp:BoundField DataField="ETA_TIPO_IMPACTO_AMBIENTAL" 
                    HeaderText="Tipo de Impacto" />
            </Columns>
            <EmptyDataTemplate>
                no se han ingresado valores&nbsp; en la categoria
            </EmptyDataTemplate>
        </asp:GridView>
        </td>
    </tr>
    <tr>
        <td class="titleUpdate" colspan="3" width="100%">
                    </td>
    </tr>
    <tr>
        <td >
            5.2 suelos</td>
        <td colspan="2">
            &nbsp;</td>
    </tr>
      <td  colspan="3" class="titleUpdate">
            </td>
    <tr>
        <td  colspan="3">
               <asp:GridView ID="grid2" runat="server" AutoGenerateColumns="False"
             
                EmptyDataText="la seccion geotecnia no tiene registros asociados" 
                onselectedindexchanged="grid1_SelectedIndexChanged" 
                   onrowdeleting="grid2_RowDeleting">
            <Columns>
                <asp:CommandField ShowDeleteButton="True" />
                <asp:BoundField DataField="ETV_TIPOS_EVALUACIONES_AMB" 
                    HeaderText="Tipo de evaluacion" />
                <asp:BoundField AccessibleHeaderText="Codigo del Mapa" DataField="EEA_COD_MAPA" 
                    HeaderText="Codigo del Mapa" />
                <asp:BoundField AccessibleHeaderText="Unidad Geotecnica a Intervenir" DataField="EEA_INFO_EVAL"
                    HeaderText="Unidad de suelos a intervenir" />
                <asp:BoundField AccessibleHeaderText="Infraestructura del Proyecto que la Intervienen"
                    DataField="EEA_INFRAC_INTERVIENE" 
                    HeaderText="Infraestructura del Proyecto que la Intervienen" />
                <asp:BoundField AccessibleHeaderText="Area (Ha) Intervenir" DataField="EEA_AREA"
                    HeaderText="Area (Ha) Intervenir" />
                <asp:BoundField AccessibleHeaderText="% del Area Total a Intervenir" DataField="EEA_PORC_AREA_TOTAL"
                    HeaderText="% del Area Total a Intervenir" />
                <asp:BoundField AccessibleHeaderText="Impacto Potencial a Generar" DataField="EEA_IMPACTO_POTENCIAL"
                    HeaderText="Impacto Potencial a Generar" />
                <asp:BoundField DataField="ETA_TIPO_IMPACTO_AMBIENTAL" 
                    HeaderText="Tipo de Impacto" />
            </Columns>
            <EmptyDataTemplate>
                no se han ingresado valores&nbsp; en la categoria
            </EmptyDataTemplate>
        </asp:GridView></td>
    </tr>
    <tr>
        <td class="titleUpdate" colspan="3">          </td>
    </tr>
    <tr>
        <td >
            5.3 Calidad de Fuentes de Agua</td>
        <td  colspan="2">
            &nbsp;</td>
    </tr>
      <td  colspan="3" class="titleUpdate">
            </td>
    <tr>
        <td  colspan="3">
        <asp:GridView ID="grid3" runat="server" AutoGenerateColumns="False"
             
                EmptyDataText="la seccion geotecnia no tiene registros asociados" 
                onselectedindexchanged="grid1_SelectedIndexChanged" 
                onrowdeleting="grid3_RowDeleting">
            <Columns>
                <asp:CommandField ShowDeleteButton="True" />
                <asp:BoundField DataField="ETV_TIPOS_EVALUACIONES_AMB" 
                    HeaderText="Tipo de evaluacion" />
                <asp:BoundField AccessibleHeaderText="No" DataField="EEA_COD_MAPA" 
                    HeaderText="No" />
                <asp:BoundField AccessibleHeaderText="Unidad Geotecnica a Intervenir" DataField="EEA_INFO_EVAL"
                    HeaderText="Fuentes de agua a intervenir" />
                <asp:BoundField AccessibleHeaderText="Infraestructura del Proyecto que la Intervienen"
                    DataField="EEA_INFRAC_INTERVIENE" 
                    HeaderText="Infraestructura del Proyecto que la Intervienen" />
                <asp:BoundField AccessibleHeaderText="Area (Ha) Intervenir" DataField="EEA_AREA"
                    HeaderText="Area (Ha) Intervenir" />
                <asp:BoundField AccessibleHeaderText="% del Area Total a Intervenir" DataField="EEA_PORC_AREA_TOTAL"
                    HeaderText="% del Area Total a Intervenir" />
                <asp:BoundField AccessibleHeaderText="Impacto Potencial a Generar" DataField="EEA_IMPACTO_POTENCIAL"
                    HeaderText="Impacto Potencial a Generar" />
                <asp:BoundField DataField="ETA_TIPO_IMPACTO_AMBIENTAL" 
                    HeaderText="Tipo de Impacto" />
            </Columns>
            <EmptyDataTemplate>
                no se han ingresado valores&nbsp; en la categoria
            </EmptyDataTemplate>
        </asp:GridView>
        </td>
    </tr>
    <tr>
        <td class="titleUpdate" colspan="3">
            </td>
    </tr>
    <tr>
        <td >
            5.4 calidad del aire</td>
        <td  colspan="2">
            &nbsp;</td>
        
    </tr>
      <td  colspan="3" class="titleUpdate">
            </td>
    <tr>
        <td  colspan="3">
          <asp:GridView ID="grid4" runat="server" AutoGenerateColumns="False"
             
                EmptyDataText="la seccion geotecnia no tiene registros asociados" 
                onselectedindexchanged="grid1_SelectedIndexChanged" 
                onrowdeleting="grid4_RowDeleting">
            <Columns>
                <asp:CommandField ShowDeleteButton="True" />
                <asp:BoundField DataField="ETV_TIPOS_EVALUACIONES_AMB" 
                    HeaderText="Tipo de evaluacion" />
                <asp:BoundField AccessibleHeaderText="No" DataField="EEA_COD_MAPA" 
                    HeaderText="No" />
                <asp:BoundField AccessibleHeaderText="Unidad Geotecnica a Intervenir" DataField="EEA_INFO_EVAL"
                    HeaderText="Fuentes de Emisiones de Gases, Vapores o Ruido" />
                <asp:BoundField AccessibleHeaderText="Infraestructura del Proyecto que la Intervienen"
                    DataField="EEA_INFRAC_INTERVIENE" 
                    HeaderText="Infraestructura del Proyecto que la Intervienen" />
                <asp:BoundField AccessibleHeaderText="Area (Ha) Intervenir" DataField="EEA_AREA"
                    HeaderText="Area (Ha) Intervenir" />
                <asp:BoundField AccessibleHeaderText="Impacto Potencial a Generar" DataField="EEA_IMPACTO_POTENCIAL"
                    HeaderText="Impacto Potencial a Generar" />
                <asp:BoundField DataField="ETA_TIPO_IMPACTO_AMBIENTAL" 
                    HeaderText="Tipo de Impacto" />
            </Columns>
            <EmptyDataTemplate>
                no se han ingresado valores&nbsp; en la categoria
            </EmptyDataTemplate>
        </asp:GridView>
        </td>
    </tr>
    <tr>
        <td class="titleUpdate" colspan="3">            </td>
    </tr>
    <tr>
        <td >
            5.5 Ecosistemas Terrestres</td>
        <td  colspan="2">
            &nbsp;</td>
    </tr>
      <td  colspan="3" class="titleUpdate">
            </td>
    <tr>
        <td  colspan="3">
           <asp:GridView ID="grid5" runat="server" AutoGenerateColumns="False"
             
                EmptyDataText="la seccion geotecnia no tiene registros asociados" 
                onselectedindexchanged="grid1_SelectedIndexChanged" 
                onrowdeleting="grid5_RowDeleting">
            <Columns>
                <asp:CommandField ShowDeleteButton="True" />
                <asp:BoundField DataField="ETV_TIPOS_EVALUACIONES_AMB" 
                    HeaderText="Tipo de evaluacion" />
                <asp:BoundField AccessibleHeaderText="Codigo del Mapa" DataField="EEA_COD_MAPA" 
                    HeaderText="Codigo del Mapa" />
                <asp:BoundField AccessibleHeaderText="Ecosistema a Intervenir" DataField="EEA_INFO_EVAL"
                    HeaderText="Ecosistema a Intervenir" />
                <asp:BoundField AccessibleHeaderText="Infraestructura del Proyecto que la Intervienen"
                    DataField="EEA_INFRAC_INTERVIENE" 
                    HeaderText="Infraestructura del Proyecto que la Intervienen" />
                <asp:BoundField AccessibleHeaderText="Area (Ha) Intervenir" DataField="EEA_AREA"
                    HeaderText="Area (Ha) Intervenir" />
                <asp:BoundField AccessibleHeaderText="% del Area Total a Intervenir" DataField="EEA_PORC_AREA_TOTAL"
                    HeaderText="% del Area Total a Intervenir" />
                <asp:BoundField DataField="ETA_TIPO_IMPACTO_AMBIENTAL" 
                    HeaderText="Tipo de Impacto" />
            </Columns>
            <EmptyDataTemplate>
                no se han ingresado valores&nbsp; en la categoria
            </EmptyDataTemplate>
        </asp:GridView>
        </td>
    </tr>
    <tr>
        <td >
            &nbsp;</td>
        <td  colspan="2">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="titleUpdate" colspan="3">
           </td>
    </tr>
    <tr>
        <td >
            5.6 covertura vegetal</td>
        <td  colspan="2">
            &nbsp;</td>
    </tr>
      <td  colspan="3" class="titleUpdate">
            </td>
    <tr>
        <td  colspan="3">
           <asp:GridView ID="grid6" runat="server" AutoGenerateColumns="False"
             
                EmptyDataText="la seccion geotecnia no tiene registros asociados" 
                onselectedindexchanged="grid1_SelectedIndexChanged" 
                onrowdeleting="grid6_RowDeleting">
            <Columns>
                <asp:CommandField ShowDeleteButton="True" />
                <asp:BoundField DataField="ETV_TIPOS_EVALUACIONES_AMB" 
                    HeaderText="Tipo de evaluacion" />
                <asp:BoundField AccessibleHeaderText="Codigo del Mapa" DataField="EEA_COD_MAPA" 
                    HeaderText="Codigo del Mapa" />
                <asp:BoundField AccessibleHeaderText="Unidad de Cobertura Vegetal a Intervenir" DataField="EEA_INFO_EVAL"
                    HeaderText="Unidad de Cobertura Vegetal a Intervenir" />
                <asp:BoundField AccessibleHeaderText="Infraestructura del Proyecto que la Intervienen"
                    DataField="EEA_INFRAC_INTERVIENE" 
                    HeaderText="Infraestructura del Proyecto que la Intervienen" />
                <asp:BoundField AccessibleHeaderText="Area (Ha) Intervenir" DataField="EEA_AREA"
                    HeaderText="Area (Ha) Intervenir" />
                <asp:BoundField AccessibleHeaderText="% del Area Total a Intervenir" DataField="EEA_PORC_AREA_TOTAL"
                    HeaderText="% del Area Total a Intervenir" />
                <asp:BoundField AccessibleHeaderText="Impacto Potencial a Generar" DataField="EEA_IMPACTO_POTENCIAL"
                    HeaderText="Impacto Potencial a Generar" />
                <asp:BoundField DataField="ETA_TIPO_IMPACTO_AMBIENTAL" 
                    HeaderText="Tipo de Impacto" />
            </Columns>
            <EmptyDataTemplate>
                no se han ingresado valores&nbsp; en la categoria
            </EmptyDataTemplate>
        </asp:GridView>
        </td>
    </tr>
    <tr>
        <td  colspan="3" class ="titleUpdate">
            </td>
    </tr>
    <tr>
        <td >
            5.7 ecosistemas acuaticos</td>
        <td  colspan="2">
            &nbsp;</td>
    </tr>
      <td  colspan="3" class="titleUpdate">
            </td>
    <tr>
        <td  colspan="3">
          <asp:GridView ID="grid7" runat="server" AutoGenerateColumns="False"
             
                EmptyDataText="la seccion geotecnia no tiene registros asociados" 
                onselectedindexchanged="grid1_SelectedIndexChanged" 
                onrowdeleting="grid7_RowDeleting">
            <Columns>
                <asp:CommandField ShowDeleteButton="True" />
                <asp:BoundField DataField="ETV_TIPOS_EVALUACIONES_AMB" 
                    HeaderText="Tipo de evaluacion" />
                <asp:BoundField AccessibleHeaderText="Codigo del Mapa" DataField="EEA_COD_MAPA" 
                    HeaderText="Codigo del Mapa" />
                <asp:BoundField AccessibleHeaderText="Ecosistema a Intervenir" DataField="EEA_INFO_EVAL"
                    HeaderText="Ecosistema a Intervenir" />
                <asp:BoundField AccessibleHeaderText="Infraestructura del Proyecto que la Intervienen"
                    DataField="EEA_INFRAC_INTERVIENE" 
                    HeaderText="Infraestructura del Proyecto que la Intervienen" />
                <asp:BoundField AccessibleHeaderText="Area (Ha) Intervenir" DataField="EEA_AREA"
                    HeaderText="Area (Ha) Intervenir" />
                <asp:BoundField AccessibleHeaderText="% del Area Total a Intervenir" DataField="EEA_PORC_AREA_TOTAL"
                    HeaderText="% del Area Total a Intervenir" />
                <asp:BoundField AccessibleHeaderText="Impacto Potencial a Generar" DataField="EEA_IMPACTO_POTENCIAL"
                    HeaderText="Impacto Potencial a Generar" />
                <asp:BoundField DataField="ETA_TIPO_IMPACTO_AMBIENTAL" 
                    HeaderText="Tipo de Impacto" />
            </Columns>
            <EmptyDataTemplate>
                no se han ingresado valores&nbsp; en la categoria
            </EmptyDataTemplate>
        </asp:GridView>
        </td>
    </tr>
    <tr>
        <td class="titleUpdate" colspan="3">
            </td>
    </tr>
    <tr>
        <td >
            5.8 ecositemas marinos</td>
        <td  colspan="2">
            &nbsp;</td>
    </tr>
      <td  colspan="3" class="titleUpdate">
            </td>
    <tr>
        <td  colspan="3">
           <asp:GridView ID="grid8" runat="server" AutoGenerateColumns="False"
             
                EmptyDataText="la seccion geotecnia no tiene registros asociados" 
                onselectedindexchanged="grid1_SelectedIndexChanged" 
                onrowdeleting="grid8_RowDeleting">
            <Columns>
                <asp:CommandField ShowDeleteButton="True" />
                <asp:BoundField DataField="ETV_TIPOS_EVALUACIONES_AMB" 
                    HeaderText="Tipo de evaluacion" />
                <asp:BoundField AccessibleHeaderText="Codigo del Mapa" DataField="EEA_COD_MAPA" 
                    HeaderText="Codigo del Mapa" />
                <asp:BoundField AccessibleHeaderText="Ecosistema a Intervenir" DataField="EEA_INFO_EVAL"
                    HeaderText="Ecosistema a Intervenir" />
                <asp:BoundField AccessibleHeaderText="Infraestructura del Proyecto que la Intervienen"
                    DataField="EEA_INFRAC_INTERVIENE" 
                    HeaderText="Infraestructura del Proyecto que la Intervienen" />
                <asp:BoundField AccessibleHeaderText="Area (Ha) Intervenir" DataField="EEA_AREA"
                    HeaderText="Area (Ha) Intervenir" />
                <asp:BoundField AccessibleHeaderText="% del Area Total a Intervenir" DataField="EEA_PORC_AREA_TOTAL"
                    HeaderText="% del Area Total a Intervenir" />
                <asp:BoundField AccessibleHeaderText="Impacto Potencial a Generar" DataField="EEA_IMPACTO_POTENCIAL"
                    HeaderText="Impacto Potencial a Generar" />
                <asp:BoundField DataField="ETA_TIPO_IMPACTO_AMBIENTAL" 
                    HeaderText="Tipo de Impacto" />
            </Columns>
            <EmptyDataTemplate>
                no se han ingresado valores&nbsp; en la categoria
            </EmptyDataTemplate>
        </asp:GridView>
        </td>
    </tr>
    <tr>
        <td class="titleUpdate" colspan="3">
            </td>
    </tr>
    <tr>
        <td >
            5.9 Componente socioeconomico</td>
        <td  colspan="2">
            &nbsp;</td>
    </tr>
      <td  colspan="3" class="titleUpdate">
            </td>
    <tr>
        <td  colspan="3">
         <asp:GridView ID="grid9" runat="server" AutoGenerateColumns="False"
             
                EmptyDataText="la seccion geotecnia no tiene registros asociados" 
                onselectedindexchanged="grid1_SelectedIndexChanged" 
                onrowdeleting="grid9_RowDeleting">
            <Columns>
                <asp:CommandField ShowDeleteButton="True" />
                <asp:BoundField DataField="ETV_TIPOS_EVALUACIONES_AMB" 
                    HeaderText="Tipo de evaluacion" />
                <asp:BoundField AccessibleHeaderText="No" DataField="EEA_COD_MAPA" 
                    HeaderText="No" />
                <asp:BoundField AccessibleHeaderText="Dimensión Socioeconómica" DataField="EEA_INFO_EVAL"
                    HeaderText="Dimensión Socioeconómica" />
                <asp:BoundField AccessibleHeaderText="Actividades del Proyecto que la Afectan"
                    DataField="EEA_INFRAC_INTERVIENE" 
                    HeaderText="Actividades del Proyecto que la Afectan" />
                <asp:BoundField AccessibleHeaderText="Variable a ser Afectada" DataField="EEA_PORC_AREA_TOTAL"
                    HeaderText="Variable a ser Afectada" />
                <asp:BoundField AccessibleHeaderText="Impacto Potencial a Generar" DataField="EEA_IMPACTO_POTENCIAL"
                    HeaderText="Impacto Potencial a Generar" />
                <asp:BoundField DataField="ETA_TIPO_IMPACTO_AMBIENTAL" 
                    HeaderText="Tipo de Impacto" />
            </Columns>
            <EmptyDataTemplate>
                no se han ingresado valores&nbsp; en la categoria
            </EmptyDataTemplate>
        </asp:GridView>
        </td>
    </tr>
    <tr>
        <td class="titleUpdate" colspan="3">
            </td>
    </tr>
    <tr>
        <td >
            5.10 otros componentes adicionales</td>
        <td  colspan="2">
            &nbsp;</td>
    </tr>
      <td  colspan="3" class="titleUpdate">
            </td>
    <tr>
        <td  colspan="3">
        <asp:GridView ID="grid10" runat="server" AutoGenerateColumns="False"
             
                EmptyDataText="la seccion geotecnia no tiene registros asociados" 
                onselectedindexchanged="grid1_SelectedIndexChanged" 
                onrowdeleting="grid10_RowDeleting">
            <Columns>
                <asp:CommandField ShowDeleteButton="True" />
                <asp:BoundField DataField="ETV_TIPOS_EVALUACIONES_AMB" 
                    HeaderText="Tipo de evaluacion" />
                <asp:BoundField AccessibleHeaderText="No" DataField="EEA_COD_MAPA" 
                    HeaderText="No" />
                <asp:BoundField AccessibleHeaderText="Componente a  ser afectado" DataField="EEA_INFO_EVAL"
                    HeaderText="Componente a  ser afectado" />
                <asp:BoundField AccessibleHeaderText="Actividades del Proyecto que la Afectan"
                    DataField="EEA_INFRAC_INTERVIENE" 
                    HeaderText="Actividades del Proyecto que la Afectan" />
                <asp:BoundField AccessibleHeaderText="Variable a ser Afectada" DataField="EEA_PORC_AREA_TOTAL"
                    HeaderText="Variable a ser Afectada" />
                <asp:BoundField AccessibleHeaderText="Impacto Potencial a Generar" DataField="EEA_IMPACTO_POTENCIAL"
                    HeaderText="Impacto Potencial a Generar" />
                <asp:BoundField DataField="ETA_TIPO_IMPACTO_AMBIENTAL" 
                    HeaderText="Tipo de Impacto" />
            </Columns>
            <EmptyDataTemplate>
                no se han ingresado valores&nbsp; en la categoria
            </EmptyDataTemplate>
        </asp:GridView>
        </td>
    </tr>
    </table>

&nbsp;




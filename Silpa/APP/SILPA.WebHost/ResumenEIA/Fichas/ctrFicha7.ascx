<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctrFicha7.ascx.cs" Inherits="ResumenEIA_Fichas_ctrFicha7" %>
<%@ Register src="../Controles/ctrProyectos.ascx" tagname="ctrProyectos" tagprefix="uc1"  %>
<style type="text/css">
.auto-style1 {
	text-align: right;
}
</style>
<table width="100%">

    <tr>
        <td colspan="4">
            7. PLAN DE MANEJO AMBIENTAL</td>
    </tr>
    <tr>
        <td colspan="4" class="titleUpdate">
            &nbsp;</td>
    </tr>
   
    <tr>
        <td style="height: 22px;" colspan="4" class="auto-style1">
        <asp:Button id="btnAgregarProgramaManejo" runat="server" 
                Text="Agregar Programa de Manejo" SkinID="boton_copia" 
                onclick="btnAgregarProgramaManejo_Click" />
        </td>
    </tr>
    <asp:PlaceHolder ID ="plhProgramaManejo" Visible ="false" runat ="server" >
    <tr>
        <td style="height: 22px;" colspan="4" >
        </td>
    </tr>
    <tr>
        <td  colspan="4" class="titleUpdate" >
        </td>
    </tr>
    <tr>
        <td width="40%">
        <asp:Label id="lblNombreProgramaManejo" runat="server" 
                Text="Nombre Programa de manejo" SkinID="etiqueta_negra" Width="30%"></asp:Label>
        </td>
        <td  colspan="3">
            <asp:TextBox id="txtNombreProgramaManejo" runat="server" Width="100%" 
                SkinID="texto_sintamano"></asp:TextBox>
            </td>
    </tr>
    <tr>
        <td >
        </td>
        <td style="text-align: center;" colspan="2">
            <asp:Button id="btnAgregar" runat="server" Text="Agregar" 
                SkinID="boton_copia" onclick="btnAgregar_Click" />
&nbsp;</td>
        <td style="text-align: center;" width="50%">
			<asp:Button id="btnCancelar" runat="server" Text="cancelar" 
                SkinID="boton_copia" onclick="btnCancelar_Click" />
            </td>
    </tr>
    </asp:PlaceHolder>
    <tr>
        <td colspan="4" class="titleUpdate">
            </td>
    </tr>
    <tr>
        <td style="height: 22px;" colspan="4" >
            &nbsp;</td>
    </tr>
    <tr class="class=&quot;titleUpdate&quot;">
        <td colspan="4" class="titleUpdate">
            </td>
    </tr>
    <tr>
    <td colspan="2">
        
        Programa de manejo
        <asp:DropDownList ID="cboProgramaManejo" runat="server" 
            onselectedindexchanged="cboProgramaManejo_SelectedIndexChanged" 
            SkinID="lista_desplegable" Width="30%"> 
        </asp:DropDownList>
        
    </td>
    <td colspan="2" style="text-align: right" width="50%">
        
        <asp:Button id="btnAgregarProyectoManejo" runat="server" 
                Text="Agregar Proyecto de manejo" SkinID="boton_copia" 
                onclick="btnAgregarProyectoManejo_Click" />
        
    </td>
    </tr>
    <asp:PlaceHolder ID="plhProyecto" runat="server" Visible="False"> 
    <tr width="100%">
        <td style="height: 23px;" colspan="4">        
            <uc1:ctrProyectos ID="ctrProyectos1" runat="server" />
        </td>
    </tr>
    <tr>
        <td style="height: 23px; text-align: center;" colspan="2">
            <asp:Button ID="btnAgregarProyecto" runat="server" Text="Agregar" 
                onclick="btnAgregarProyecto_Click"  SkinID="boton_copia"  />
        </td>
        <td style="height: 23px; text-align: center;" colspan="2" width="50%">
            <asp:Button ID="btnCancelarProyecto" runat="server" Text="Cancelar" 
                onclick="btnCancelarProyecto_Click" SkinID="boton_copia"  />
        </td>
    </tr>
   </asp:PlaceHolder>
   <tr>
       <td colspan="4" width="100%">
           <asp:GridView ID="grvProyectos" runat="server" AutoGenerateColumns="False" 
               SkinID="Grilla_simple" onrowdeleted="grvProyectos_RowDeleted" 
               onrowdeleting="grvProyectos_RowDeleting">
               <Columns>
                            <asp:CommandField DeleteText="Eliminar Proyecto" ShowDeleteButton="True" />
                            <asp:BoundField DataField="EMA_NOMBRE_PROGRAMA" 
                                HeaderText="programa de Manejo" />                            
                            <asp:BoundField DataField="EPM_NOMBRE_PROYECTO" HeaderText="Nombre Proyecto" />
                            
                            <asp:BoundField DataField="EEA_ETAPA_APLICACION_PROY" 
                                HeaderText="Etapa aplicacion de proyecto" />
                            <asp:BoundField DataField="ETM_TIPO_MEDIDA_PROY_MAN_AMB" 
                                HeaderText="Tipo Medida" />
                            <asp:BoundField DataField="EPM_OBJETIVO" HeaderText="Objetivo" />
                            <asp:BoundField DataField="EPM_METAS" HeaderText="Metas" />
                            <asp:BoundField DataField="EPM_IMPACTOS_MANEJAR" 
                                HeaderText="Impactos a Manejar" />
                            <asp:BoundField DataField="EPM_INDICADORES_SEG_MONIT" 
                                HeaderText="Indicadores de seguimiento y monitoreo" />
                            </Columns>
           </asp:GridView>
       </td>
   </tr> 
</table>

<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPASinMenu.master" AutoEventWireup="true" CodeFile="ReceptorVertimientoSuelos.aspx.cs" Inherits="ResumenEIA_InfoAdicional_ReceptorVertimientoSuelos" Title="Receptores de Vertimiento de Suelos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager runat="server">
        </asp:ScriptManager>
    <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" Text="Receptores del Vertimiento de Suelos" SkinID="titulo_principal_blanco"></asp:Label>
    </div>    
    <table align="center">
    <tr>        
    <td>

    <div class="div-contenido"  >
   
        <table width="80%">
            <tr id="trMetales" runat="server">
                <td >
                    <asp:Label ID="lblMetal" runat="server" SkinID="etiqueta_negra" 
                    Text="Especifique aquí Metal"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMetal" runat="server" SkinID="texto_sintamano" 
                    MaxLength="200" Width="98%" 
                    ToolTip="Ingrese Metal"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator111" runat="server" 
                    ErrorMessage="Ingrese Información Limite de Detección"
                        Display="Dynamic" ControlToValidate="txtMetal"
                        ValidationGroup="Validacion">*</asp:RequiredFieldValidator>
                </td>   
            </tr>
            <tr id="trMetodoDeter" runat="server">
                <td >
                    <asp:Label ID="Label1" runat="server" SkinID="etiqueta_negra" 
                    Text="Método de Determinación"></asp:Label>
                </td>
                <td >
                    <asp:DropDownList ID="cboMetodoDeterminacion" runat="server" SkinID="lista_desplegable" 
                    MaxLength="200" Width="99%" 
                    ToolTip="Ingrese Información Predio"></asp:DropDownList>    
                     <asp:CompareValidator ID="CompareValidator3" runat="server"
                    ErrorMessage="Seleccione Metodo de Determinación" 
                    Display="Dynamic"
                    ControlToValidate="cboMetodoDeterminacion" 
                    ValueToCompare="-1" Operator="NotEqual"
                    ValidationGroup="Validacion">*</asp:CompareValidator>
                </td>   
            </tr>
            <tr id="trLimiteDetec" runat="server">
                <td >
                    <asp:Label ID="Label119" runat="server" SkinID="etiqueta_negra" 
                    Text="Límite de Detección"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLimiteDetección" runat="server" SkinID="texto_sintamano" 
                    MaxLength="200" Width="98%" 
                    ToolTip="Ingrese Limite de Detección"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator83" runat="server" 
                    ErrorMessage="Ingrese Información Limite de Detección"
                        Display="Dynamic" ControlToValidate="txtLimiteDetección"
                        ValidationGroup="Validacion">*</asp:RequiredFieldValidator>
                </td>   
            </tr>
            <tr id="trInfo" runat="server">
                <td >
                    <asp:Label ID="Label3" runat="server" SkinID="etiqueta_negra" 
                    Text="Información Adicional"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtInfo" runat="server" SkinID="texto_sintamano" 
                    MaxLength="200" Width="98%" 
                    ToolTip="Ingrese Metal"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ErrorMessage="Ingrese Información Adicional"
                        Display="Dynamic" ControlToValidate="txtInfo"
                        ValidationGroup="Validacion">*</asp:RequiredFieldValidator>
                </td>   
            </tr>
        </table>
                
        <table width="80%">
       <tr>
        <td>
            <fieldset id="pruebaField" runat="server">        
                <legend>Sitios</legend>            
                <div id="Campos" runat="Server">              
                </div>        
            </fieldset>
        </td>
        </tr>        
        </table>
        <table width="80%">
        <tr>
        </tr>
           	<tr>
            <td align="center" width="25%"></td>
            <td align="center" width="25%">
                <asp:Button ID="btnAgregar" runat="server" SkinID="boton_copia"
                    Text="Agregar" ValidationGroup="Validacion" OnClick="btnAgregar_Click" />
            </td>
            <td align="center" width="25%">
                <asp:Button ID="btnCancelar" runat="server" SkinID="boton_copia"
                    Text="Cancelar" OnClick="btnCancelarSitMonitRuido_Click" />
            </td>
            <td align="center" width="25%"></td>
        </tr>
        <tr>
            <td colspan="4" width="100%">
                <asp:ValidationSummary ID="ValidationSummary10" runat="server" 
                ValidationGroup="Validacion" />
            </td>
        </tr>
        </table>
    </div>
    </td>
    </tr>    
    </table>        
</asp:Content>


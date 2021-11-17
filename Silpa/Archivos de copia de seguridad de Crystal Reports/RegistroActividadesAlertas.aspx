<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="RegistroActividadesAlertas.aspx.cs" Inherits="ReporteTramite_RegistroActividadesAlertas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
 

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript">
    //This code must be placed below the ScriptManager, otherwise the "Sys" cannot be used because it is undefined. 
    Sys.Application.add_init(function() {
        // Store the color validation Regex in a "static" object off of 
        // AjaxControlToolkit.ColorPickerBehavior.  If this _colorRegex object hasn't been 
        // created yet, initialize it for the first time. 
        if (!AjaxControlToolkit.ColorPickerBehavior._colorRegex) {
            AjaxControlToolkit.ColorPickerBehavior._colorRegex = new RegExp('^[A-Fa-f0-9]{6}$');
        }
    });
    function colorChanged(sender) {
        sender.get_element().style.color =
       "#" + sender.get_selectedColor();
    } 
    </script> 
    <asp:ScriptManager runat="server" ID="Scriptmanager" ></asp:ScriptManager>    
    <asp:UpdatePanel runat="server" ID="upInsertar">
    <ContentTemplate>    
    <div align="left">
    <table width="100%" >
        <tr>
            <td align="center">
                <asp:Label runat="server" ID="titulo" SkinID="subtitulo" Text="Configuración Detalle mis Tramites"></asp:Label>
            </td>
            <td>
                <br />
                <br />
                <br />
            </td>
        </tr>
    </table>
    <table width="100%">  
        <tr>
            <td><asp:Label runat="server" ID="Label1" Text="Trámite" SkinID="etiqueta_negra"></asp:Label> </td>
            <td  Width="70%">
                <asp:DropDownList runat="server" ID="cboTramite" SkinID="lista_desplegable" 
                    AutoPostBack="true" onselectedindexchanged="cboTramite_SelectedIndexChanged"></asp:DropDownList>  
                <asp:RequiredFieldValidator id="rfvTramite" runat="server" ControlToValidate="cboTramite"
                    SetFocusOnError="true" Display="Dynamic" ErrorMessage="Seleccione Tramite"
                    ValidationGroup="Guardar" >*</asp:RequiredFieldValidator>    
            </td>
        </tr>
        <tr>
            <td><asp:Label runat="server" ID="Label12" Text="Nombre" SkinID="etiqueta_negra"></asp:Label> </td>
            <td><asp:TextBox runat="server" ID="txtNombre" SkinID="texto_sintamano" Width="95%"></asp:TextBox>  
                <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombre"
                    SetFocusOnError="true" Display="Dynamic" ErrorMessage="Seleccione Nombre ha mostrar"
                    ValidationGroup="Guardar" >*</asp:RequiredFieldValidator>    
            </td>
        </tr>        
        <tr>
            <td><asp:Label runat="server" ID="Label3" Text="Dia Limite" SkinID="etiqueta_negra"></asp:Label> </td>
            <td><asp:TextBox runat="server" ID="txtDiaLimite" SkinID="texto_sintamano" ></asp:TextBox>  
              <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ControlToValidate="txtNombre"
                    SetFocusOnError="true" Display="Dynamic" ErrorMessage="Ingrese dia limite"
                    ValidationGroup="Guardar" >*</asp:RequiredFieldValidator>    
              <cc1:MaskedEditExtender runat="server" ID="mascara1" TargetControlID="txtDiaLimite" MaskType ="Number" Mask="999999999" PromptCharacter=""></cc1:MaskedEditExtender>
            </td>
        </tr>
        <tr>
            <td><asp:Label runat="server" ID="Label4" Text="Carácter" SkinID="etiqueta_negra"></asp:Label> </td>
            <td><asp:DropDownList runat="server" ID="cboCaracter" SkinID="lista_desplegable">
                <asp:ListItem Text="Opcional" Value="0" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Obligatorio" Value="1"></asp:ListItem>
            </asp:DropDownList>  </td>
        </tr>        
        <tr>
            <td><asp:Label runat="server" ID="Label11" Text="Actividades" SkinID="etiqueta_negra"></asp:Label> </td>
            <td><asp:DropDownList runat="server" ID="cboActividad" SkinID="lista_desplegable" 
                    AutoPostBack="true" onselectedindexchanged="cboActividad_SelectedIndexChanged">
            </asp:DropDownList>  
                <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" ControlToValidate="cboActividad"
                    SetFocusOnError="true" Display="Dynamic" ErrorMessage="Seleccione Actividad"
                    ValidationGroup="Guardar" >*</asp:RequiredFieldValidator>    
            </td>
        </tr>
        <tr>
            <td><asp:Label runat="server" ID="Label6" Text="Documento de Sila o Vital" SkinID="etiqueta_negra"></asp:Label> </td>
            <td><asp:DropDownList runat="server" ID="cboDocumentosSila" SkinID="lista_desplegable">            
            </asp:DropDownList>  
            <asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" ControlToValidate="cboDocumentosSila"
                    SetFocusOnError="true" Display="Dynamic" ErrorMessage="Seleccione Documento"
                    ValidationGroup="Guardar" >*</asp:RequiredFieldValidator>    
            </td>
        </tr>        
        <tr>
            <td><asp:Label runat="server" ID="Label8" Text="Color Letra" SkinID="etiqueta_negra"></asp:Label> </td>
            <td>
                <table cellpadding="0" cellspacing="0" >
                    <tr>
                    <td>
                        <asp:TextBox runat="server" ID="txtColor" SkinID="texto_sintamano">            
                        </asp:TextBox>
                        &nbsp;
                    </td>
                    <td>
                        <asp:ImageButton runat="server" ToolTip="Color" SkinID="icoColor" ID="imgColor" />
                        <cc1:ColorPickerExtender ID="ColorPickerExtender1" runat="server"
                            TargetControlID="txtColor"  
                            PopupButtonID="imgColor"
                            SampleControlID="Sample1"
                            SelectedColor="000000" />  
                            &nbsp;
                    </td>            
                    <td>
                        <asp:Label id ="Sample1" runat="server" width="20px" height="20px" ></asp:Label>                        
                    </td>
                    </tr>                    
                </table>
             </td>
        </tr>
        <tr>
            <td><asp:Label runat="server" ID="Label10" Text="Negrilla" SkinID="etiqueta_negra"></asp:Label> </td>
            <td><asp:DropDownList runat="server" ID="cboNegrilla" SkinID="lista_desplegable">            
                <asp:ListItem Text="Si" Value ="1"></asp:ListItem>
                <asp:ListItem Text="No" Value ="0" Selected="True"></asp:ListItem>
            </asp:DropDownList>  </td>
        </tr>
        <tr>
        <td><asp:Label runat="server" ID="Label9" Text="Color fondo" SkinID="etiqueta_negra"></asp:Label> </td>
        <td>
            <table cellpadding="0" cellspacing="0">
                <tr>
                <td>
                    <asp:TextBox runat="server" ID="txtColorFondo" SkinID="texto_sintamano">            
                    </asp:TextBox>
                    &nbsp;
                </td>
                <td>
                    <asp:ImageButton runat="server" ToolTip="Color" SkinID="icoColor" ID="imgColorFondo" />
                    <cc1:ColorPickerExtender ID="ColorPickerExtender2" runat="server"
                        TargetControlID="txtColorFondo"  
                        PopupButtonID="imgColorFondo"
                        SampleControlID="Sample2"
                        SelectedColor="000000" />  
                        &nbsp;
                </td>            
                <td>                  
                    <asp:Label id ="Sample2" runat="server" width="20px" height="20px"></asp:Label>                        
                </td>
                </tr>                    
            </table>
         </td>           
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label13" Text="Correo Electronico" SkinID="etiqueta_negra"></asp:Label>
        </td>
        <td>
            <table cellpadding="0" cellspacing="0" width="100%" >
            <tr>
            <td width="20%">
            <asp:RadioButtonList runat="server" id="rblCorrreo" AutoPostBack="true" width="100%" >
                <asp:ListItem Text="Correo" Value="0" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Autoridad Ambiental" Value="1"></asp:ListItem>
                <asp:ListItem Text="Solicitante" Value="2"></asp:ListItem>
            </asp:RadioButtonList>
            </td>
            <td>                
                <asp:TextBox runat="server" ID="txtCorreoElectronico" SkinID="texto"></asp:TextBox>
            </td>
            </tr>            
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="Label14" Text="Dias Inicia Alerta" SkinID="etiqueta_negra"></asp:Label>
        </td>
        <td>
            <asp:TextBox runat="server" ID="txtDiaIniciaAlerta" SkinID="texto_sintamano"></asp:TextBox>
            <cc1:MaskedEditExtender runat="server" ID="MaskedEditExtender1" TargetControlID="txtDiaIniciaAlerta" MaskType ="Number" Mask="999999999" PromptCharacter=""></cc1:MaskedEditExtender>
        </td>
    </tr>
    <tr>
        <td colspan="2">        
            <asp:ValidationSummary runat="server" ValidationGroup="Guardar" />
        </td>
    </tr>
    <tr>
        <td></td>
        <td align="right">
            <table width="100%">
                <tr>
                <td align="right">
                <asp:Label ID="Label2" runat="server" Text="G u a r d a r" SkinID="titulo_principal">
                </asp:Label>    
                </td>
                <td align="left">
                <asp:ImageButton ID="btnConsultar" runat="server" ToolTip="Guardar" SkinID="icoGuardar" 
                                 OnClick="btnGuardarClick" ValidationGroup="Guardar" />         
                    </td>
                </tr>
            </table>
        </td>
    </tr>        
    </table>
 
    <table>
        <tr> 
            <td>
                <asp:GridView runat="server" ID="grdListaActividades" AutoGenerateColumns="false"
                    EmptyDataText="No existen datos registrados en ésta tabla" SkinID="Grilla" 
                    onrowcommand="grdListaActividades_RowCommand" 
                    onrowdatabound="grdListaActividades_RowDataBound" >
                    <Columns>
                        <asp:BoundField DataField="TRA_NOMBRE" HeaderText="Tramite"></asp:BoundField> 
                        <asp:BoundField DataField="NOMBRE" HeaderText="Nombre"></asp:BoundField> 
                        <asp:BoundField DataField="DIA_LIMITE" HeaderText="Dia Limite"></asp:BoundField> 
                        <asp:BoundField DataField="CARACTER" HeaderText="Caracter"></asp:BoundField>                         
                        <asp:BoundField DataField="ACT_NOMBRE" HeaderText="Actividad"></asp:BoundField> 
                        <asp:BoundField DataField="DOC_NOMBRE" HeaderText="Documento"></asp:BoundField>    
                        <asp:BoundField DataField="TIPO_CORREO_TEXTO" HeaderText="Usuario Correo"></asp:BoundField>    
                        <asp:BoundField DataField="CORREO_ELECTRONICO" HeaderText="Correo Electronico"></asp:BoundField>                            
                        <asp:BoundField DataField="DIAS_INICIO_ALERTA" HeaderText="Dias Inicio Alerta"></asp:BoundField>                            
                        <asp:TemplateField HeaderText="Color Letra">
                            <ItemTemplate>
                                <asp:Label id ="divColorLetra" runat="server" width="20px" height="20px"></asp:Label>                                                                                    
                                <asp:Label runat="server" ID="lblColorLetra"  Text='<%# Eval("COLOR_LETRA") %>' Visible="false"></asp:Label>                                
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Center"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Negrilla">
                            <ItemTemplate>
                                <asp:CheckBox runat="server" ID="chkNegrilla" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Color Fondo">
                            <ItemTemplate>
                                <asp:Label id ="divColorFondo" runat="server" width="20px" height="20px"></asp:Label>                                                    
                                <asp:Label runat="server" ID="lblColorFondo"  Text='<%# Eval("COLOR_FONDO") %>' Visible="false"></asp:Label>                                
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Center"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Opciones">
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="imgEditar" CommandName="Editar" CommandArgument='<%# Container.DataItemIndex %>' SkinID="icoEditar" ToolTip="Editar" />
                                <asp:ImageButton runat="server" ID="imgEliminar" CommandName="Eliminar" CommandArgument=<%# Container.DataItemIndex %> SkinID="icoEliminar" ToolTip="Eliminar"/>
                                <asp:Label runat="server" ID="lblTramite" Text='<%# Eval("TRA_ID") %>' Visible="false"></asp:Label>
                                <asp:Label runat="server" ID="lblActividad" Text='<%# Eval("ACT_ID") %>' Visible="false"></asp:Label>
                                <asp:Label runat="server" ID="lblDocumento" Text='<%# Eval("DOC_ID") %>' Visible="false"></asp:Label>
                                <asp:Label runat="server" ID="lblTipoCorreo" Text='<%# Eval("TIPO_CORREO") %>' Visible="false"></asp:Label>
                                <asp:Label runat="server" ID="lblIdSolicitante" Text='<%# Eval("ID_SOLICITANTE") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle  HorizontalAlign="Center"/>
                        </asp:TemplateField>
                    </Columns>                
                </asp:GridView>
            </td>       
        </tr>
    </table>    
       </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="CorreoServidor.aspx.cs" Inherits="Administracion_Tablasbasicas_CorreoServidor" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="div-titulo">
        <asp:Label ID="lblTitulo" runat="server" Text="CORREO VITAL" SkinID="titulo_principal_blanco"></asp:Label>
    </div>
    <div class="div-contenido" style="height: 400px">
        <table width="100%">
            <tr>
                <td colspan="4">
                    <asp:UpdatePanel ID="updConsultar" runat="server">
                        <contenttemplate>
<asp:Panel id="pnlMaestro" runat="server" Width="100%">
                                <table width="60%">
                                    <tr>
                                        <td style="width: 15%" align="left">
                                            <asp:Label ID="lblNombreParametro" SkinID="etiqueta_negra" runat="server" Text="Descripción"></asp:Label>
                                        </td>
                                        <td style="width: 30%" align="left">
                                            <asp:TextBox ID="txtNombreParametro" SkinID="texto" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="right">
                                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" SkinID="boton_copia" OnClick="btnBuscar_Click" />
                                            <asp:Button ID="btnagregar" runat="server" Text="Agregar" SkinID="boton_copia" OnClick="btnagregar_Click" />
                                            <asp:Button id="btnVolver" runat="server" SkinID="boton_copia" Text="Cancelar" PostBackUrl="~/Administracion/Tablasbasicas/TablasBasicas.aspx"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel> <asp:Panel id="pnlConsultar" runat="server" Width="900" ScrollBars="Auto" Visible="true">
                                <asp:GridView 
                                        ID="grdDatos" 
                                        runat="server" 
                                        Width="100%" 
                                        OnPageIndexChanging="grdDatos_PageIndexChanging"
                                        DataKeyNames="CORREO_SERVIDOR_ID" 
                                        OnRowCommand="grdDatos_RowCommand" 
                                        EmptyDataText="No existen datos registrados en ésta tabla"
                                        AllowSorting="True" 
                                        AllowPaging="True" 
                                        AutoGenerateColumns="False" 
                                        PageSize="10">
                                        <Columns>
                                            <asp:BoundField DataField="CORREO_SERVIDOR_ID" HeaderText="Id Servidor"></asp:BoundField>
                                            <asp:BoundField DataField="NOMBRE_SERVIDOR" HeaderText="Nombre Correo Servidor"></asp:BoundField>
                                            <asp:BoundField DataField="HOST" HeaderText="Host"></asp:BoundField>
                                            <asp:BoundField DataField="USUARIO" HeaderText="Usuario" />
                                            <asp:BoundField DataField="CONTRASENA" HeaderText="Contraseña" Visible="false"/>
                                            <asp:BoundField DataField="PUERTO" HeaderText="Puerto"></asp:BoundField>
                                            <asp:BoundField DataField="SEPARADOR" HeaderText="Separador"></asp:BoundField>
                                            <asp:TemplateField HeaderText="Editar">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkModificar" CommandName="Modificar" runat="server" CommandArgument='<%# Container.DataItemIndex %>'>Modificar Registro</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Eliminar">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEliminar" CommandName="Eliminar" runat="server" CommandArgument='<%# Container.DataItemIndex %>'
                                                        OnClientClick="return confirm('Esta seguro de Eliminar este registro?')">Eliminar Registro</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
  
                            </asp:Panel> <asp:Panel id="pnlEditar" runat="server" Width="75%" Visible="false">
                            <TABLE width="100%"><TBODY><TR><TD style="WIDTH: 10%" align=left>
                            <asp:Label id="lblNombreServidor" runat="server" Text="Nombre Servidor"></asp:Label> 
                            </TD><TD style="WIDTH: 25%" align=left>
                            <asp:TextBox id="txtNombreServidor" runat="server" Width="190px"></asp:TextBox>
                             </TD><TD></TD><TD style="WIDTH: 173px">
                             <asp:Label id="lblID" runat="server" Visible="False"></asp:Label>
                              </TD></TR><TR><TD style="WIDTH: 10%" align=left>
                              <asp:Label id="lblHost" runat="server" Text="Host"></asp:Label>
                              </TD><TD style="WIDTH: 25%" align=left colSpan=3>
                              <asp:TextBox id="txtHost" runat="server" Width="193px"></asp:TextBox>
                              </TD></TR>
                              
                              <TR>
                              <TD style="WIDTH: 10%" align=left>
                              <asp:Label id="lblusuario" runat="server" Text="Usuario"></asp:Label>
                              </TD><TD style="WIDTH: 25%" align=left colSpan=3>
                              <asp:TextBox id="txtUsuario" runat="server" Width="193px"></asp:TextBox>
                              </TD></TR>
                              
                              <TR>
                              <TD style="WIDTH: 10%" align=left>
                              <asp:Label id="lblpuerto" runat="server" Text="Puerto"></asp:Label>
                              </TD><TD style="WIDTH: 25%" align=left colSpan=3>
                              <asp:TextBox id="txtPuerto" runat="server" Width="193px"></asp:TextBox>
                              </TD></TR>
                              
                              <TR><TD style="WIDTH: 10%; HEIGHT: 26px" align=left>
                              <asp:Label id="lblContrasena" runat="server" Text="Contraseña"></asp:Label>
                              </TD><TD style="WIDTH: 25%; HEIGHT: 26px" align=left colSpan=3>
                              <asp:TextBox id="txtContrasena" runat="server" Width="194px" TextMode="Password"></asp:TextBox>
                              <asp:RequiredFieldValidator id="rfvContrasenaUpd" runat="server" ControlToValidate="txtContrasena" ErrorMessage="Debe ingresar una contraseña." __designer:wfdid="w5"></asp:RequiredFieldValidator>
                              </TD></TR><TR><TD style="WIDTH: 10%; HEIGHT: 26px" align=left>
                              <asp:Label id="lblContrasenaConf" runat="server" Text="Confirme la Contraseña" __designer:wfdid="w3"></asp:Label>
                              </TD><TD style="WIDTH: 25%; HEIGHT: 26px" align=left colSpan=3>
                              <asp:TextBox id="txtConfContrasena" runat="server" Width="194px" TextMode="Password" __designer:wfdid="w4"></asp:TextBox>
                               </TD></TR><TR><TD style="WIDTH: 10%" align=left>
                               <asp:Label id="lblSeparador" runat="server" Text="Separador"></asp:Label>
                               </TD><TD style="WIDTH: 25%" align=left colSpan=3>
                               <asp:TextBox id="txtSeparador" runat="server" Width="194px"></asp:TextBox>
                               </TD></TR><TR><TD align=right colSpan=4>
                               <asp:Button id="btnActualizar" onclick="btnActualizar_Click" runat="server" SkinID="boton_copia" Text="Aceptar"></asp:Button>
                                <asp:Button id="btnCancelar" onclick="btnCancelar_Click" runat="server" SkinID="boton_copia" Text="Cancelar" CausesValidation="False"></asp:Button> 
                                </TD></TR></TBODY></TABLE>
                                
                                </asp:Panel>
                                
                                 <asp:Panel id="pnlNuevo" runat="server" Visible="false">
                                 <TABLE><TBODY><TR>
                                 <TD align=left>
                                 <asp:Label id="lblNombreServidor_Nuevo" runat="server" Text="Nombre Servidor"></asp:Label> 
                                 </TD>
                                 
                                 <TD align=left colSpan=2>
                                 <asp:TextBox id="txtNombreServidor_Nuevo" runat="server" Width="191px"></asp:TextBox>
                                  </TD><TD></TD></TR>
                                  <TR>
                                  <TD align=left>
                                  <asp:Label id="lblHost_Nuevo" runat="server" Text="Host"></asp:Label> 
                                  </TD><TD align=left colSpan=2>
                                  <asp:TextBox id="txtHost_Nuevo" runat="server" Width="193px"></asp:TextBox>
                                  </TD><TD></TD></TR>
                                  
                                  <TR>
                                  <TD align=left>
                                  <asp:Label id="lblUsuario_Nuevo" runat="server" Text="Usuario"></asp:Label> 
                                  </TD><TD align=left colSpan=2>
                                  <asp:TextBox id="txtUsuario_Nuevo" runat="server" Width="193px"></asp:TextBox>
                                  </TD><TD></TD></TR>
                                  
                                  <TR>
                                  <TD align=left>
                                  <asp:Label id="lblPuerto_Nuevo" runat="server" Text="Puerto"></asp:Label> 
                                  </TD><TD align=left colSpan=2>
                                  <asp:TextBox id="txtPuerto_Nuevo" runat="server" Width="193px"></asp:TextBox>
                                  </TD><TD></TD></TR>
                                  
                                  <TR><TD align=left>
                                  <asp:Label id="lblContrasena_Nuevo" runat="server" Text="Contraseña"></asp:Label> 
                                  </TD><TD align=left colSpan=2>
                                  <asp:TextBox id="txtContrasena_Nuevo" runat="server" Width="194px" TextMode="Password"></asp:TextBox>
                                  </TD><TD>
                                  <asp:RequiredFieldValidator id="rfvContrasena" runat="server" ControlToValidate="txtContrasena_Nuevo" ErrorMessage="Debe ingresar una contraseña" __designer:wfdid="w1"></asp:RequiredFieldValidator>
                                  </TD></TR><TR><TD align=left>
                                  <asp:Label id="lblConfContrasena" runat="server" Text="Confirme la Contraseña"></asp:Label>
                                  </TD><TD align=left colSpan=2>
                                  <asp:TextBox id="txtConfContrasena_Nvo" runat="server" Width="194px" TextMode="Password"></asp:TextBox>
                                  </TD><TD></TD></TR><TR><TD align=left>
                                  <asp:Label id="lblSeparador_Nuevo" runat="server" Text="Separador"></asp:Label> 
                                  </TD><TD align=left colSpan=2>
                                  <asp:TextBox id="txtSeparador_Nuevo" runat="server" Width="194px"></asp:TextBox>
                                  </TD><TD></TD></TR><TR><TD align=right colSpan=4>&nbsp;
                                  <asp:Button id="btnRegistrar" onclick="btnRegistrar_Click" runat="server" SkinID="boton_copia" Text="Aceptar"></asp:Button>
                                   <asp:Button id="btnCancelarReg" onclick="btnCancelarReg_Click" runat="server" SkinID="boton_copia" Text="Cancelar" CausesValidation="False"></asp:Button>
                                    </TD></TR></TBODY></TABLE>
                                    </asp:Panel>
                                    
                                     <asp:Label id="lblMensajeError" runat="server" ForeColor="Red" Font-Bold="True">
                                     </asp:Label> 
</contenttemplate>
                        <triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                        </triggers>
                    </asp:UpdatePanel>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

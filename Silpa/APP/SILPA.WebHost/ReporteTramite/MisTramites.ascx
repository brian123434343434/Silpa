<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MisTramites.ascx.cs" Inherits="ReporteTramite_MisTramites" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>    
   
    <div class="stilesLarge">
        <asp:UpdatePanel ID="uppPanel" runat="server">
            <ContentTemplate>
                <asp:Panel ID="uppConsultaReporte" runat="server">
                <table width="100%">
                    <tr>
                        <td valign="top" style="width:50%">
                            <fieldset>
                                <legend>Información General</legend>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 40%">
                                            <asp:Label ID="lblNombreProyecto" runat="server" Text="Nombre del Proyecto, Obra o Actividad :" SkinID="etiqueta_negra"
                                                 ></asp:Label>
                                        </td >
                                        <td style="width: 60%" valign=top>
                                            <asp:TextBox ID="txtNombreProyecto" runat="server"
                                                    SkinID="texto_sintamano" Width="99%"></asp:TextBox>
                                        </td>                                        
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LblNumExpediente" runat="server"  SkinID="etiqueta_negra"
                                                Text="Número del Expediente:" ></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtNumeroExpediente" runat="server" SkinID="texto_sintamano" Width="99%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label3" runat="server"  SkinID="etiqueta_negra"
                                                Text="Número VITAL: " ></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtSilpaNumero" runat="server" SkinID="texto_sintamano" 
                                                 Width="99%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label4" runat="server" SkinID="etiqueta_negra" Text="Tipo de Trámite:"
                                                 ></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlTipoTramite" runat="server" SkinID="lista_desplegable2" Width="99%"></asp:DropDownList>    
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label7" runat="server" SkinID="etiqueta_negra" Text="Solicitante:"
                                                 ></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboSolicitantes" runat="server" SkinID="lista_desplegable2" Width="99%"></asp:DropDownList>    
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label5" runat="server" SkinID="etiqueta_negra" Text="Autoridad Ambiental:"
                                                 ></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboAutoridadAmbiental" SkinID="lista_desplegable2" runat="server" Width="99%"> </asp:DropDownList>
                                        </td>
                                     </tr>
                                     <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" SkinID="etiqueta_negra" Text="Estado Resolución:"
                                                 ></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboEstadoResolucion" SkinID="lista_desplegable2" runat="server" Width="99%">
                                                <asp:ListItem Selected="True" Value="0">Todos</asp:ListItem>
                                                <asp:ListItem Value="2">Negado</asp:ListItem>
                                                <asp:ListItem Value="1">Otorgado</asp:ListItem>
                                                 </asp:DropDownList>
                                        </td>
                                     </tr>
                                     <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" SkinID="etiqueta_negra" Text="Estado Tramite:"
                                                 ></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboEstadoTramite" SkinID="lista_desplegable2" runat="server" Width="99%">
                                                <asp:ListItem Selected="True" Value="0">Todos</asp:ListItem>
                                                <asp:ListItem Value="2">En Proceso</asp:ListItem>
                                                <asp:ListItem Value="1">Finalizado</asp:ListItem>
                                                 </asp:DropDownList>
                                        </td>
                                     </tr>
                                </table>
                             </fieldset>
                            <fieldset>
                                <legend>Fecha Creación</legend>   
                                    <table style="width: 100% ">
                                    <tr>
                                        <td style="width: 50%">
                                            <asp:Label ID="lblFechaInicial" runat="server"  SkinID="etiqueta_negra"
                                                Text="Fecha Desde (dd/mm/aaaa):"></asp:Label><br />
                                            <asp:TextBox ID="txtFechaInicial" runat="server" MaxLength="10" SkinID="texto_corto"></asp:TextBox><cc1:CalendarExtender
                                                ID="calFechaInicial" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaInicial">
                                            </cc1:CalendarExtender>
                                            
                                        </td>
                                        <td style="width: 50%">
                                            <asp:Label ID="lblFechaFinal" runat="server"  SkinID="etiqueta_negra" 
                                                Text="Fecha Hasta (dd/mm/aaaa):" ></asp:Label><br /> 
                                            <asp:TextBox ID="txtFechaFinal" runat="server" MaxLength="10" SkinID="texto_corto"></asp:TextBox>
                                            <asp:CompareValidator ID="covCompararFechas" runat="server" ControlToCompare="txtFechaInicial" 
                                                ControlToValidate="txtFechaFinal" Display="Dynamic" ErrorMessage='El valor del campo "Fecha Hasta", debe ser posterior al valor del campo "Fecha Desde".'
                                                Operator="GreaterThan" Type="Date" Height="13px" Width="1px">*</asp:CompareValidator>
                                            <cc1:CalendarExtender ID="calFechaFinal" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaFinal">
                                            </cc1:CalendarExtender>
                                        </td>
                                    </tr>
                              </table>                          
                            </fieldset>
                            <table width="100%">
                            <tr>
                            <td align="right">
                            <asp:Label ID="Label6" runat="server" Text="B u s q u e d a &nbsp;  d e &nbsp;  T r á m i t e" SkinID="titulo_principal">
                            </asp:Label>    
                            </td>
                            <td align="left">
                            <asp:ImageButton ID="btnConsultar" runat="server" ToolTip="Consultar Tramite" SkinID="icoConsultar" 
                                             OnClick="btnConsultar_Click"  />         
                                </td>
                            </tr>
                            </table>
                            
                        </td>
                        <td valign="top" style="width:50%">
                             <fieldset>
                                <legend>Ubicación</legend>
                                <table style="width: 100%">
                                     <tr>
                                        <td style="width: 40%">
                                            <asp:Label ID="lblDepartamento" runat="server"  SkinID="etiqueta_negra"
                                                Text="Departamento" ></asp:Label></td>
                                        <td style="width: 60%">
                                            <asp:DropDownList ID="cboDepartamento" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboDepartamento_SelectedIndexChanged"
                                                 Width="99%">
                                                <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                                            </asp:DropDownList></td>
                                     </tr>
                                     <tr>
                                        <td >
                                            <asp:Label ID="lblMunicipio" runat="server"  SkinID="etiqueta_negra"
                                                Text="Municipio" ></asp:Label></td>
                                        <td >
                                            <asp:DropDownList ID="cboMunicipio" runat="server" AutoPostBack="True" SkinID="lista_desplegable2" Width="99%">
                                                <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                                            </asp:DropDownList></td>
                                     </tr>
                                     <tr>
                                        <td colspan="2">Cuenca</td>
                                     </tr>
                                     <tr>
                                         <td >
                                             <asp:Label ID="LblArea" runat="server"  SkinID="etiqueta_negra" Text="Area Hidrográfica:"
                                                 ></asp:Label></td>
                                         <td >
                                             <asp:DropDownList ID="CboArea" runat="server" AutoPostBack="True" Width="99%" OnSelectedIndexChanged="CboArea_SelectedIndexChanged">
                                                 <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                                             </asp:DropDownList></td>
                                     </tr>
                                <tr>
                                    <td >
                                        <asp:Label ID="LblZona" runat="server"  SkinID="etiqueta_negra" Text="Zona Hidrográfica:"
                                           ></asp:Label></td>
                                    <td >
                                        <asp:DropDownList ID="CboZona" runat="server" AutoPostBack="True" Width="99%" OnSelectedIndexChanged="CboZona_SelectedIndexChanged">
                                            <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                                        </asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td >
                                        <asp:Label ID="LblSubZona" runat="server"  SkinID="etiqueta_negra"
                                            Text="Sub Zona Hidrográfica:" ></asp:Label></td>
                                    <td >
                                        <asp:DropDownList ID="CboSubZona" runat="server" AutoPostBack="True" SkinID="lista_desplegable2" Width="99%">
                                            <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                                        </asp:DropDownList></td>
                                </tr>
                             </table>
                             </fieldset>
                             <fieldset>
                                <legend>Sector</legend>
                                <table style="width: 100% ">
                                    <tr> 
                                        <td>
                                         <asp:DropDownList ID="cboSectores" runat="server" SkinID="lista_desplegable2" Width="99%">                                     
                                         </asp:DropDownList>
                                         <asp:ImageButton id="btnAddSector" SkinID="icoAdicionar" runat="server" ToolTip="Adicionar Sector" OnClick="btnAddSector_Click"/>
                                         <asp:label id="lblSectoresSeleccionados" runat="server" Text="" Visible="false"></asp:label>
                                        </td>                                                          
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ListBox ID="lstSectorSel" runat="server" SkinID="texto_sintamano" Width="99%"></asp:ListBox>
                                        </td>
                                    </tr>
                                    <tr>                            
                                        <td>
                                            <asp:ImageButton id="btnElimSector" SkinID="icoEliminar" runat="server" ToolTip="Desagregar Sector" OnClick="btnElimSector_Click"/>
                                        </td>
                                    </tr>
                                </table>
                              </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">                                                           
                                <table style="width: 100%">
                                    <tr>
                                        <td align="center">                                                                            
                                        </td>                                        
                                    </tr>                                 
                                </table>                             
                        </td>                          
                    </tr>
                </table>                   
                </asp:Panel>
                <asp:Panel width= "100%" ID="pnConsulta" runat="server" ScrollBars="Auto" >            
                    <asp:GridView ID="grdReporte" runat="server" AutoGenerateColumns="False"  OnRowCommand="grdReporte_RowCommand" 
                    SkinID="Grilla_simple_peq"  AllowPaging="True"  AllowSorting="True" OnPageIndexChanging="grdReporte_PageIndexChanging" 
                    Width="100%" RowStyle-Height="60px" 
                    OnSelectedIndexChanged="grdReporte_SelectedIndexChanged" PageSize="5">                                                            
                    <HeaderStyle HorizontalAlign="Right"  />
                            <Columns>                                                                                            
                                <asp:TemplateField HeaderText="Resultado">
                                    <ItemTemplate>
                                    
                                        <asp:Label ID="Label8" runat="server" Text="Nº Vital:" SkinID="etiqueta_negra14N"></asp:Label>
                                        <asp:LinkButton CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' Text='<%# Bind("SOL_NUMERO_SILPA") %>' ID="lbVerDetalles" runat="server" CommandName="DETALLE" Font-Size="14px"></asp:LinkButton>
                                        
                                        <asp:Label ID="Label9" runat="server" Text="Tipo Tramite:" SkinID="etiqueta_negra12"></asp:Label>
                                        <asp:Label Text='<%# Bind("NOMBRE_TIPO_TRAMITE") %>' ID="LinkButton1" runat="server"  SkinID="etiqueta_negra12"></asp:Label>
                                        
                                        <asp:Label ID="Label10" runat="server" Text="Fecha Inicio:" SkinID="etiqueta_negra9"></asp:Label>
                                        <asp:Label ID="Label11" runat="server" Text='<%# Bind("SOL_FECHA_CREACION") %>' SkinID="etiqueta_negra9"></asp:Label>                                        
                                          
                                        <asp:Label ID="Label12" runat="server" Text="Autoridad Ambiental:" SkinID="etiqueta_negra14"></asp:Label>
                                        <asp:Label Text='<%# Bind("AUT_NOMBRE") %>' ID="Label13" runat="server"  SkinID="etiqueta_negra14"></asp:Label>
                                        
                                           
                                        <asp:Label ID="Label14" runat="server" Text="Ubicación:" SkinID="etiqueta_negra"></asp:Label>
                                        <asp:Label ID="Label15" runat="server" Text='<%# Bind("UBICACION") %>' SkinID="etiqueta_negra"></asp:Label>                                                                                
                                          
                                        <asp:Label ID="Label18" runat="server" Text="Nombre Proyecto:" SkinID="etiqueta_negra12"></asp:Label>
                                        <asp:Label Text='<%# Bind("NOMBRE") %>' ID="Label19" runat="server"  SkinID="etiqueta_negra12"></asp:Label>
                                        
                                        <asp:Label ID="Label20" runat="server" Text="Expediente:" SkinID="etiqueta_negra14"></asp:Label>
                                        <asp:Label ID="Label21" runat="server" Text='<%# Bind("EXPEDIENTE") %>'  SkinID="etiqueta_negra14"></asp:Label>    
                                        
                                        <asp:Label ID="Label22" runat="server" Text="Cuenca:" SkinID="etiqueta_negra"></asp:Label>
                                        <asp:Label ID="Label23" runat="server" Text='<%# Bind("CUENCA") %>'  SkinID="etiqueta_negra"></asp:Label>    
                                        
                                        <asp:Label ID="Label24" runat="server" Text="Sector:" SkinID="etiqueta_negra9"></asp:Label>
                                        <asp:Label Text='<%# Bind("SECTOR") %>' ID="Label25" runat="server"  SkinID="etiqueta_negra9"></asp:Label>
                                        
                                        
                                        <asp:Label ID="lbNumeroSilpa" runat="server" Text='<%# Bind("SOL_NUMERO_SILPA") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblIdSol" runat="server" Text='<%# Bind("SOL_ID_SOLICITANTE") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblExpCod" runat="server" Text='<%# Bind("EXPEDIENTE") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblTipoTramite" runat="server" Text='<%# Bind("NOMBRE_TIPO_TRAMITE") %>' Visible="false"></asp:Label>
                                      
                                    </ItemTemplate>
                                </asp:TemplateField>                             
                            </Columns>
                    </asp:GridView>
                </asp:Panel>                
                <asp:Panel ID="pnlReporte" runat="server" Visible="False" Width="278px">
                    <asp:Label ID="lblMensajeError" runat="server" SkinID="etiqueta_roja_error" Style="text-align: center"
                    Text="Existe un error con el reporte, por favor comuniquese con  el administrador" Visible="False" Width="545px"></asp:Label>
                </asp:Panel>
            </ContentTemplate>                        
        </asp:UpdatePanel>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        
    </div>    
   
   
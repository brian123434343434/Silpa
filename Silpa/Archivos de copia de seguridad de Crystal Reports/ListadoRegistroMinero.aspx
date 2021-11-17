<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPASinMenuFlash.master" AutoEventWireup="true" CodeFile="ListadoRegistroMinero.aspx.cs" Inherits="RegistroMinero_ListadoRegistroMinero" %>

<%@ Register Src="~/ReporteTramite/GoogleMapForASPNet.ascx" TagName="GoogleMapForASPNet" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlaceHolder" runat="Server">
    <%--<link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>--%>

    <script src='<%= ResolveClientUrl("~/js/jquery-1.8.2.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/js/basicos.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/js/ListadoRegistroMinero.js") %>' type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        table {
            /*border: 1px solid #000;*/
        }
        table tr td {
            border: 0px solid #ddd !important;
            padding: 4px;
        }
        .Button{
            background-color: #ddd;
        }

        .divWaiting
        {
	        background-color:Gray;
            /*background-color: #FAFAFA;*/
	        filter:alpha(opacity=70);
	        /*opacity:0.7;*/
            position: absolute;
            z-index: 2147483647 !important;
            opacity: 0.8;
            overflow: hidden;
            text-align: center; top: 0; left: 0;
            height: 100%;
            width: 100%;
            padding-top:20%;
        } 
    </style>

    <asp:ScriptManager ID="sm" runat="server">
    </asp:ScriptManager>
    
    <div class="table-responsive">
        <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
            <tr>
                <td>
                    <asp:Label ID="lblTipoFalta0" runat="server" SkinID="etiqueta_negra9" Text="Tipo Registro:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboTipoRegistro" runat="server" Width="400px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDepartamentoOcurrencia" runat="server" SkinID="etiqueta_negra9"
                        Text="Autoridad Ambiental:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboAutoridadAmbiental" runat="server" Width="400px">
                        <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMunicipioOcurrencia0" runat="server" SkinID="etiqueta_negra9" Text="Titular:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNombreOperador" runat="server" SkinID="texto" CausesValidation="True" Width="400px" 
                        ValidationGroup="Operador" MaxLength="150"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMunicipioOcurrencia1" runat="server" SkinID="etiqueta_negra9" Text="Identificación:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtIdentifAutoridad" runat="server" SkinID="texto" CausesValidation="True" Width="400px" 
                        ValidationGroup="Operador" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCorregimientoOcurrencia" runat="server" SkinID="etiqueta_negra9"
                        Text="Cod. Registro Minero:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCodRegMineria" runat="server" SkinID="texto" MaxLength="50" Width="400px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSancionAplicadaSec0" runat="server" SkinID="etiqueta_negra9" Text="Nombre Proyecto:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNombreProyecto" runat="server" SkinID="texto" MaxLength="100" Width="400px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSancionAplicadaSec2" runat="server" SkinID="etiqueta_negra9" Text="Nombre Mina:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNombreMina" runat="server" SkinID="texto" MaxLength="150" Width="400px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMunicipioOcurrencia3" runat="server" SkinID="etiqueta_negra9" Text="Departamento:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboDepartamento" runat="server" AutoPostBack="True" Width="400px" 
                        ValidationGroup="Ubicacion" OnSelectedIndexChanged="cboDepartamento_SelectedIndexChanged">
                        <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMunicipioOcurrencia4" runat="server" SkinID="etiqueta_negra9" Text="Municipio:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboMunicipio" runat="server" ValidationGroup="Ubicacion" Width="400px">
                        <asp:ListItem Value="-1">Seleccione...</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding-top: 20px; padding-bottom: 10px; padding-left: 10px; padding-right: 20px; text-align: right; vertical-align: middle;">
                    <asp:Button ID="btnBuscarRegMinero" runat="server" Text="Buscar" SkinID="boton" OnClick="btnBuscarRegMinero_Click" />
                </td>
            </tr>
        </table>

        <div style="padding: 0; text-align: left; vertical-align: top;">
            <asp:GridView ID="GridRegistroMin" runat="server" Width="100%" 
                AutoGenerateColumns="False" CellPadding="3" SkinID="Grilla"
                EnableModelValidation="True" AllowPaging="True" BackColor="White"
                BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" Font-Names="Tahoma" Font-Size="9px" 
                OnPageIndexChanging="GridRegistroMin_PageIndexChanging" 
                onrowdatabound="GridRegistroMin_RowDataBound">
                <HeaderStyle BackColor="#BDBE9A" Font-Bold="True" ForeColor="#000000" Font-Size="9pt" />
                <FooterStyle BackColor="#BDBE9A" Font-Bold="True" ForeColor="#000000" Font-Size="9pt" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Font-Size="9px" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" Font-Size="9px" />
                <EditRowStyle BackColor="#333333" Font-Size="9px" />
                <AlternatingRowStyle BackColor="White" ForeColor="#333333" Font-Size="9px" />
                <Columns>
                    <asp:BoundField DataField="NOMBRE_TIPO_REGISTRO_MINERO" HeaderText="Tipo Registro"></asp:BoundField>
                    <asp:BoundField DataField="NOMBRE_AUTORIDAD" HeaderText="Autoridad Ambiental"></asp:BoundField>
                    <asp:TemplateField HeaderText="Titulares">
                        <ItemTemplate>
                            <asp:Label ID="lblTitulares" EnableTheming="false" runat="server" Text='<%# Bind("TITULARES") %>' SkinID="etiqueta_negra9"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CODIGO_TITULOMINERO" HeaderText="Cod. Registro Minero" />
                    <asp:BoundField DataField="NOMBRE_PROYECTO" HeaderText="Proyecto" />
                    <asp:BoundField DataField="NOMBRE_MINA" HeaderText="Mina" />
                        <asp:TemplateField HeaderText="Coordenadas" Visible="true">
                        <ItemTemplate>
                            <asp:Image ID="ImgCoordenada" Visible="false" runat="server" />
                            <asp:Label ID="lcoordenada" runat="server" Text='<%# Bind("EXISTLOC") %>' SkinID="etiqueta_negra9"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <a href="javascript:popup('ubicacion.aspx?regID=<%# Eval("REGISTROMINERO_ID") %>');" class="a_orange">Mapa</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <a target="_blank" href="FichaRegistroMinero.aspx?idreg=<%# Eval("REGISTROMINERO_ID") %>" class="a_green">Ver Detalle</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <a target="_blank" href="RegistroMinero.aspx?idreg=<%# Eval("REGISTROMINERO_ID") %>" class="a_blue">Editar</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkGraficar" runat="server" EnableTheming="false" />
                            <asp:Label ID="lblRegistroMineroID" runat="server" Text='<%# Bind("REGISTROMINERO_ID") %>' Visible="false" SkinID="etiqueta_negra9"></asp:Label>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkTodos" runat="server"/>
                        </HeaderTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
            <tr>
                <td colspan="2" style="padding-top: 20px; padding-bottom: 10px; padding-left: 10px; padding-right: 20px; text-align: left; vertical-align: middle;">
                    <asp:Label runat="server" ID="NumRegistros" SkinID="etiqueta_negra9"></asp:Label>
                    <asp:Button ID="Button1" runat="server" Text="Ver Todos" SkinID="boton" OnClick="imb_ver_todos_Click" />
                    <%--<asp:ImageButton ID="imb_ver_todos" OnClick="imb_ver_todos_Click" runat="server"
                            ToolTip="Haga clic aqu&#237; para ver el listado completo" Width="76px" BorderWidth="0px"
                            Height="16px" ImageUrl="../images/ver_todos.gif" meta:resourcekey="imb_ver_todosResource1"></asp:ImageButton>--%>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding: 10px; text-align: left; vertical-align: middle;">
                    <asp:ImageButton runat="server" ID="btnMapas" SkinID="icoMapa" ToolTip="Ver Ubicacion Registros Mineros" OnClick="btnMapas_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
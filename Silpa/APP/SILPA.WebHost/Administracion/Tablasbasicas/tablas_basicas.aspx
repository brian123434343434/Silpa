<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="tablas_basicas.aspx.cs" Inherits="adm_parametros_tablas_basicas" Title="SILA - Parametrización Tablas Basicas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<%-- <atlas:UpdatePanel ID="atl_pnl_cnosulta" Mode="Always" runat="server"> --%>
    <ContentTemplate>
        <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr background="../images/btn_fondo.gif">
                <td width="6"><img src="../images/btn_lado1.gif" width="6" height="23" /></td>
                <td width="8" background="../images/btn_fondo.gif"><img src="../images/pestana1.gif" width="8" height="23" border="0" /></td>
                <td width="200" align="center" background="../images/pestana_fondo.gif"><asp:Label CssClass="titulos" ID="lbl_tablas_basicas" runat="server" Text="Tablas básicas" ToolTip="Buscar permisos" /></td>
                <td width="8" background="../images/pestana_fondo.gif"><img src="../images/pestana2.gif" width="8" height="23" border="0" /></td>
                <td background="../images/btn_fondo.gif">&nbsp;</td>
                <td width="6"><img src="../images/btn_lado2.gif" width="6" height="23" /></td>
            </tr>
        </table>        
        <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
             <tr>
               <td valign="top" background="../images/cuadro2.gif" style="width: 6px"><img src="../images/cuadro1.gif" width="6" height="6" /></td>
             
                            <td height="22" align="right">
                                <asp:Button CssClass="boton" ID="btn_cargar" runat="server" Text="Cargar tablas básicas"
                                    ToolTip="Haga clic aqu&iacute; para cargar tabals básicas" OnClick="btn_cargar_Click"
                                    ValidationGroup="Crear" />
                            </td>
               <td valign="top" background="../images/cuadro6.gif" style="width: 6px"><img src="../images/cuadro7.gif" width="6" height="6" /></td>
            </tr>
            <tr>
                <td valign="top" background="../images/cuadro2.gif" style="width: 6px"></td>
                <td>
                    <asp:Repeater ID="rpt_modulos" runat="server">
                        <ItemTemplate>
                            <table width="100%" border="0" align="center">
                                <tr>
                                    <td>
                                        <table width="100%" border="0" align="left">
                                            <tr align="left">
                                                <td align="left">
                                                    <asp:Label ID="lbl_mod_id" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MOD_ID")%>' Visible ="false" ></asp:Label>
                                                    <asp:Label ID="lbl_mod_nombre" CssClass="titulo_tablas" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MOD_NOMBRE")%>' ></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="center">
                                                <td>
                                                    <asp:GridView ID="dgv_tablas_basicas" runat="server" AutoGenerateColumns="False" CellPadding="2" CellSpacing="1" GridLines="None" ShowFooter="True" Width="97%"  DataKeyNames="LBA_ID">
                                                        <FooterStyle CssClass="texto_tablas_paginador" />      
                                                        <Columns>
                                                            <asp:BoundField AccessibleHeaderText="ID" DataField="LBA_ID" HeaderText="ID" Visible="False" ItemStyle-HorizontalAlign="Left" />
                                                            <asp:HyperLinkField AccessibleHeaderText="Tablas b&#225;sicas" DataTextField="LBA_NOMBRE" HeaderText="Tablas b&#225;sicas" ItemStyle-HorizontalAlign="Left"  DataNavigateUrlFields="LBA_URL" DataNavigateUrlFormatString="{0}" SortExpression="LBA_NOMBRE" />
                                                          
                                                        </Columns>
                                                        <RowStyle CssClass="texto_tablas" />
                                                        <PagerStyle CssClass="texto_tablas_paginador" HorizontalAlign="Left" />      
                                                        <HeaderStyle CssClass="titulo_tablas" />      
                                                        <AlternatingRowStyle CssClass="texto_tablas_dos" />
	                                                    <PagerSettings FirstPageImageUrl="../images/pagina_primera.gif" FirstPageText="Primera"
                                                                LastPageImageUrl="../images/pagina_ultima.gif" LastPageText="Ultima" Mode="NextPreviousFirstLast"
                                                                NextPageImageUrl="../images/pagina_siguiente.gif" NextPageText="Siguiente" PreviousPageImageUrl="../images/pagina_anterior.gif"
                                                                PreviousPageText="Anterior" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>    
                            &nbsp;
                            &nbsp;
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
                <td valign="top" background="../images/cuadro6.gif" style="width: 6px"></td>
            </tr>            
             <tr >
               <td valign="top" background="../images/cuadro2.gif" style="width: 6px"></td>
             
                            <td height="22" align="right">
                                <asp:Button CssClass="boton" ID="btn_cargar2" runat="server" Text="Cargar tablas básicas"
                                    ToolTip="Haga clic aqu&iacute; para cargar tabals básicas" OnClick="btn_cargar_Click"
                                    ValidationGroup="Crear" />
                            </td>
            <td valign="top" background="../images/cuadro6.gif" style="width: 6px"></td>
          
             </tr>
        </table>
        <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td width="6"><img src="../images/cuadro3.gif" width="6" height="6" /></td>
                <td background="../images/cuadro4.gif"><img src="../images/cuadro4.gif" width="3" height="6" /></td>
                <td style="width: 6px"><img src="../images/cuadro5.gif" width="6" height="6" /></td>
            </tr>
        </table>

    </ContentTemplate>
    &nbsp; &nbsp; &nbsp;
    <td background="../images/cuadro2.gif" style="width: 6px; height: 24px" valign="top">
    </td>
        <td align="right" style="height: 24px">
        </td>
            <td background="../images/cuadro6.gif" style="width: 6px; height: 24px" valign="top">
            </td>
<%-- 
</atlas:UpdatePanel>
--%>    


</asp:Content>


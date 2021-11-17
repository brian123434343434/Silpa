<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="AprobarInscritosAudienciaPublica.aspx.cs" Inherits="Informacion_Publicaciones" Title="Consultar Inscritos a Audiencia Pública" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="SubHeaderBack">
	<div class="pageHeader">
		<div class="subBannerPhoto"></div>
		<p>&nbsp;</p>
        <p>
            <asp:Label ID="lbl_titulo_principal" runat="server" SkinID="titulo_principal_blanco" Text="Lista de inscritos por aprobar para intervención en audiencia pública número: "></asp:Label>&nbsp;</p>
        <p>
            &nbsp;</p>
        <p>
            <table style="width: 373px">
                <tr>
                    <td>
            <asp:XmlDataSource ID="ListaInscritos" runat="server" DataFile="~/AudienciaPublica/InscritosAudienciaPorAprobar.xml">
            </asp:XmlDataSource>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                CellPadding="4" DataSourceID="ListaInscritos" ForeColor="#333333" GridLines="None"
                Width="596px">
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#E3EAEB" />
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                    <asp:BoundField DataField="cedula" HeaderText="Cedula" SortExpression="cedula" />
                    <asp:BoundField DataField="numsolicitud" HeaderText="N&#250;mero Solicitud" SortExpression="numsolicitud" />
                    <asp:TemplateField HeaderText="Aprobar">
                        <ItemTemplate>
                            <asp:CheckBox ID="ckAprobar" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="width: 100px; height: 12px">
                        </div>
                    </td>
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="Enviar Aprobaciones" OnClick="Button1_Click" />
                    </td>
                    <td>
                        <div style="width: 100px; height: 19px">
                        </div>
                    </td>
                </tr>
            </table>
        </p>
        <p>
            &nbsp;
        </p>
        <p>
            &nbsp;</p>
        <p>
            &nbsp;</p>
	</div>
</div>
<div class="copy">
    <br />
    &nbsp;</div>
</asp:Content>


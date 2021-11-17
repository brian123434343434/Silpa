<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="DocumentosFirma.aspx.cs" Inherits="FirmaDigital_DocumentosFirma" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="div-titulo">
        <asp:Label ID="Label2" runat="server" Text="Firmar Documento" SkinID="titulo_principal_blanco"></asp:Label>
    </div>
    <div class="div-contenido">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tbody>
                        <tr>
                            <td style="height: 17px" align="center" colspan="2">
                                <asp:Label ID="lblTitulo" runat="server" Text="Firmar Documento" SkinID="titulo_principal"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 17px" align="center" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="height: 17px">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 17px" align="center" colspan="2">
                                <asp:GridView ID="grdConsulta" runat="server" Width="100%" Height="100%" SkinID="Grilla"
                                    AutoGenerateColumns="False" DataSourceID="sds_firma">
                                    <Columns>
                                        <asp:BoundField DataField="DFI_ID" HeaderText="No. ID documento"></asp:BoundField>
                                        <asp:BoundField DataField="DFI_FECHA_CREACION" HeaderText="Fecha Creaci&#243;n Documento">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DFI_FECHA_FIRMA_DIGITAL" HeaderText="Fecha Firma Digital Documento">
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DFI_NOMBRE" HeaderText="Descripci&#243;n"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Estado Documento">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("EFI_ID") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("EFI_ID") %>' Visible="False"></asp:Label>
                                                <asp:FormView ID="FormView1" runat="server" DataSourceID="sds_estado">
                                                    <EditItemTemplate>
                                                        EFI_NOMBRE:
                                                        <asp:TextBox ID="EFI_NOMBRETextBox" runat="server" Text='<%# Bind("EFI_NOMBRE") %>' />
                                                        <br />
                                                        <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                                                            Text="Update" />
                                                        &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False"
                                                            CommandName="Cancel" Text="Cancel" />
                                                    </EditItemTemplate>
                                                    <InsertItemTemplate>
                                                        EFI_NOMBRE:
                                                        <asp:TextBox ID="EFI_NOMBRETextBox" runat="server" Text='<%# Bind("EFI_NOMBRE") %>' />
                                                        <br />
                                                        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert"
                                                            Text="Insert" />
                                                        &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False"
                                                            CommandName="Cancel" Text="Cancel" />
                                                    </InsertItemTemplate>
                                                    <ItemTemplate>
                                                        &nbsp;<asp:Label ID="EFI_NOMBRELabel" runat="server" Text='<%# Bind("EFI_NOMBRE") %>' />
                                                        <br />
                                                    </ItemTemplate>
                                                </asp:FormView>
                                                <asp:SqlDataSource ID="sds_estado" runat="server" ConnectionString="<%$ ConnectionStrings:SILPAConnectionString %>"
                                                    SelectCommand="SELECT [EFI_NOMBRE] FROM [GEN_ESTADO_FIRMA] WHERE ([EFI_ID] = @EFI_ID)">
                                                    <SelectParameters>
                                                        <asp:ControlParameter ControlID="Label1" Name="EFI_ID" PropertyName="Text" Type="Int64" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# string.Concat("~/FirmaDigital/FirmaDocumentoSila.aspx?url=",DataBinder.Eval(Container, "DataItem.DFI_UBICACION")) %>'
                                                    Text="Ver"></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="sds_firma" runat="server" ConnectionString="<%$ ConnectionStrings:SILPAConnectionString %>"
                                    SelectCommand="SELECT * FROM [GEN_DOCUMENTO_FIRMA] WHERE (([DFI_ID] &lt;= @DFI_ID) AND ([DFI_ID] &gt;= @DFI_ID2))">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="37" Name="DFI_ID" Type="Int64" />
                                        <asp:Parameter DefaultValue="34" Name="DFI_ID2" Type="Int64" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

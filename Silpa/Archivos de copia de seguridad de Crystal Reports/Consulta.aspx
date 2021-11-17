<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="Consulta.aspx.cs" Inherits="Sensibilizacion_Consulta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="div-titulo">
    <asp:Label ID="lbl_titulo" SkinID="Titulo_principal_blanco" runat="server" Text="Sensibilización"></asp:Label>
    </div>
    <div class="div-contenido">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="ID_CONTENIDO" DataSourceID="sds_sensiblizacion" SkinID="Grilla">
            <Columns>
                <asp:BoundField DataField="ID_CONTENIDO" HeaderText="Código" ReadOnly="True" 
                    SortExpression="ID_CONTENIDO" />
                <asp:TemplateField HeaderText="Módulo" SortExpression="ID_MODULO">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ID_MODULO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ID_MODULO") %>' 
                            Visible="False"></asp:Label>
                        <asp:FormView ID="FormView1" runat="server" DataSourceID="sds_modulo">
                            <EditItemTemplate>
                                MODULO:
                                <asp:TextBox ID="MODULOTextBox" runat="server" Text='<%# Bind("MODULO") %>' />
                                <br />
                                <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                                    CommandName="Update" Text="Update" />
                                &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                                    CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                MODULO:
                                <asp:TextBox ID="MODULOTextBox" runat="server" Text='<%# Bind("MODULO") %>' />
                                <br />
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                                    CommandName="Insert" Text="Insert" />
                                &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                                    CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                            </InsertItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="MODULOLabel" runat="server" Text='<%# Bind("MODULO") %>' />
                                <br />
                            </ItemTemplate>
                        </asp:FormView>
                        <asp:SqlDataSource ID="sds_modulo" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:SILPAConnectionString %>" 
                            SelectCommand="SELECT [MODULO] FROM [SEN_MODULO] WHERE ([ID_MODULO] = @ID_MODULO)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="Label1" Name="ID_MODULO" PropertyName="Text" 
                                    Type="Decimal" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tipo de Contenido" 
                    SortExpression="ID_TIPO_CONTENIDO">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" 
                            Text='<%# Bind("ID_TIPO_CONTENIDO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("ID_TIPO_CONTENIDO") %>' 
                            Visible="False"></asp:Label>
                        <asp:FormView ID="FormView2" runat="server" DataSourceID="sds_contenido">
                            <EditItemTemplate>
                                TIPO_CONTENIDO:
                                <asp:TextBox ID="TIPO_CONTENIDOTextBox" runat="server" 
                                    Text='<%# Bind("TIPO_CONTENIDO") %>' />
                                <br />
                                <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                                    CommandName="Update" Text="Update" />
                                &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" 
                                    CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                TIPO_CONTENIDO:
                                <asp:TextBox ID="TIPO_CONTENIDOTextBox" runat="server" 
                                    Text='<%# Bind("TIPO_CONTENIDO") %>' />
                                <br />
                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                                    CommandName="Insert" Text="Insert" />
                                &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" 
                                    CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                            </InsertItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="TIPO_CONTENIDOLabel" runat="server" 
                                    Text='<%# Bind("TIPO_CONTENIDO") %>' />
                                <br />
                            </ItemTemplate>
                        </asp:FormView>
                        <asp:SqlDataSource ID="sds_contenido" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:SILPAConnectionString %>" 
                            SelectCommand="SELECT [TIPO_CONTENIDO] FROM [SEN_TIPO_CONTENIDO] WHERE ([ID_TIPO_CONTENIDO] = @ID_TIPO_CONTENIDO)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="Label2" Name="ID_TIPO_CONTENIDO" 
                                    PropertyName="Text" Type="Decimal" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ruta" SortExpression="DIRECCION">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("DIRECCION") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("DIRECCION") %>' 
                            ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ver" SortExpression="DIRECCION">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("DIRECCION") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:HyperLink ID="hpl_documentop" runat="server" NavigateUrl='<%# Eval("DIRECCION") %>'>Ver</asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="sds_sensiblizacion" runat="server" 
            ConnectionString="<%$ ConnectionStrings:SILPAConnectionString %>" 
            SelectCommand="SELECT * FROM [SEN_CONTENIDO_MODULO] ORDER BY [ID_MODULO]">
        </asp:SqlDataSource>
    </div>
</asp:Content>


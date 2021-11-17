<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true" CodeFile="admAutoridad.aspx.cs" Inherits="Administracion_Tablasbasicas_admAutoridad" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceHolder" runat="Server">
   <%-- <style type="text/css">
        .button{
            background:url("../img/bgBtn.jpg") repeat-x scroll center top #007B8C;
            border:medium none;
            color:#FFFFFF;
            cursor:pointer;
            font-family:Verdana,Arial,Helvetica,sans-serif;
            font-size:10px;
            font-weight:bold;
            padding:2px 5px;
            vertical-align:middle;
        }
        
        body, .footerMenu {
            color: #003399;
        }
        
        .footerMenuText, .etiqueta_azul{
            color: #003399 !important;
        }
        .contentWrong{
            margin: auto;
            /*min-width: 1000px;*/
        }
        .div-titulo-hack{
            width: 1000px;
        }
        .cssHeaderBg, .ContentArea1, .ContentCopy1{
            width:100%;
            margin:0;
            padding:0;
        }
        
        .bodyContainerXX{
            width: 100%;
            background: none repeat scroll 0 0 #FFFFFF;
        }
        
        .titulo_tablas1, .gridViewHeader_css1 {
            background-color:#CFDEED;
            color:#666666;
            font-family:Verdana,Arial,Helvetica,sans-serif;
            font-size:11px;
            font-weight:bold;
            text-align:center;
            text-decoration:none;
        }
        
        .texto_tablas1, .rowStyle_css1 {
            background-color:#E5E5E5;
            color:#666666;
            font-family:Verdana,Arial,Helvetica,sans-serif;
            font-size:11px;
            font-weight:normal;
            text-decoration:none;
        }
        
        .texto_tablas_dos1, .alternatingRowStyle_css1 {
            background-color:#EFEFEF;
            color:#666666;
            font-family:Verdana,Arial,Helvetica,sans-serif;
            font-size:11px;
            font-weight:normal;
            text-decoration:none;
        }
        
        #<%=dgv_tabla.ClientID%> span{
            font-weight:normal;
        }
    </style>--%>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/jscript" >
	    function confirmar(msg)
	    {
		    if (window.confirm(msg))
		    {
		    }
		    else
		    {
			    event.returnValue = false;
		    }
	    }
	    
////HAVA:19-mar-10
function textValidaAutoridadBase()
{
    var nombreAA = document.getElementById('ctl00_ContentPlaceHolder1_txt_valor').value;
    var nombreAATemp = nombreAA.replace(/^\s+|\s+$/g,"");
    //document.getElementById('ctl00_ContentPlaceHolder1_txt_valor').value = h;
    var existe = nombreAATemp.replace("CORPOBASE", "1");
    if(existe == "1")
    {
      alert("No puede crear otra corporación base CORPOBASE.");
      document.getElementById('ctl00_ContentPlaceHolder1_txt_valor').focus();
    }
}
        window.setTimeout( "document.getElementById( '<%=pnl_formulario.ClientID%>' ).style.width = '100%'", 500 );                                     
    </script>

    <asp:Panel ID="pnl_formulario" runat="server" Height="100%">
        <div class = "div-titulo div-titulo-hack" >
            <asp:Label ID="lblTitulo" runat="server" Text="AUTORIDAD AMBIENTAL" SkinID ="titulo_principal_blanco"></asp:Label>
        </div>
        <%--<table width="98%" border="0" align="center" cellpadding="0" cellspacing="0" style="margin-top:10px">
            <tr background="../images/btn_fondo.gif">
                <td width="6">
                    <img src="../images/btn_lado1.gif" width="6" height="23" /></td>
                <td width="8" background="../images/btn_fondo.gif">
                    <img src="../images/pestana1.gif" width="8" height="23" border="0" /></td>
                <td width="200" align="center" background="../images/pestana_fondo.gif">
                    <asp:Label CssClass="titulos" ID="lbl_tablas_basicas" runat="server" Text="Autoridad Ambiental"
                        ToolTip="Buscar permisos" /></td>
                <td width="8" background="../images/pestana_fondo.gif">
                    <img src="../images/pestana2.gif" width="8" height="23" border="0" /></td>
                <td background="../images/btn_fondo.gif">
                    &nbsp;</td>
                <td width="6">
                    <img src="../images/btn_lado2.gif" width="6" height="23" /></td>
            </tr>
        </table>
        <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td width="6" valign="top" background="../images/cuadro2.gif">
                    <img src="../images/cuadro1.gif" width="6" height="6" /></td>
                <td>
                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1">
                        <tr class="texto_tablas_paginador">
                            <td height="22" align="right">
                                <asp:Button CssClass="button" ID="btn_salvar_valor" runat="server" Text="Salvar valor"
                                    ToolTip="Haga clic aqu&iacute; para agregar un valor" OnClick="btn_salvar_valor_Click"
                                    ValidationGroup="Crear" />
                            </td>
                        </tr>
                    </table>--%>
                    <table border="0" align="center" cellspacing="1">
                        <tr>
                            <td align="right" class="texto_usuario">
                                Nombre:</td>
                            <td>
                                <asp:TextBox Columns="30" CssClass="campos" ID="txt_valor" runat="server" TextMode="SingleLine"
                                    ValidationGroup="Crear" MaxLength="75" onblur ="textValidaAutoridadBase()"/></td>
                            <td>
                                <asp:CheckBox ID="chk_activo" runat="server" CssClass="texto_usuario" Text="Activo" /></td>
                        </tr>
                        <tr>
                            <td align="right" class="texto_usuario">
                                Descripci&oacute;n:</td>
                            <td>
                                <asp:TextBox Columns="30" CssClass="campos" ID="txtDescripcion" runat="server" TextMode="SingleLine"
                                    ToolTip="Escriba aquí el valor a ingresar" ValidationGroup="Crear" MaxLength="140" onblur ="textValidaAutoridadBase()"/>                            
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="texto_usuario">
                                NIT:</td>
                            <td>
                                <asp:TextBox Columns="30" CssClass="campos" ID="txt_nit" runat="server" TextMode="SingleLine"
                                    ValidationGroup="Crear" MaxLength="50" /></td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="texto_usuario" style="height: 26px">
                                Dirección
                            </td>
                            <td style="height: 26px">
                                <asp:TextBox Columns="30" CssClass="campos" ID="txt_direccion" runat="server" TextMode="SingleLine"
                                    ValidationGroup="Crear" MaxLength="75" />
                            </td>
                            <td style="height: 26px">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="texto_usuario">
                                Teléfono:
                            </td>
                            <td>
                                <asp:TextBox Columns="30" CssClass="campos" ID="txt_telefono" runat="server" TextMode="SingleLine"
                                    ValidationGroup="Crear" MaxLength="25" />
                            </td>
                            <td><asp:CheckBox ID="chk_Integrada" runat="server" CssClass="texto_usuario" Text="Autoridad Integrada" /></td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="texto_usuario">
                                Fax:
                            </td>
                            <td>
                                <asp:TextBox Columns="30" CssClass="campos" ID="txt_fax" runat="server" TextMode="SingleLine"
                                    ValidationGroup="Crear" MaxLength="25" />
                            </td>
                            
                            <td><asp:CheckBox ID="chk_radicacion" runat="server" CssClass="texto_usuario" Text="Aplica Radicación" /></td>
                        </tr>
                        <tr>
                            <td align="right" class="texto_usuario">
                                Correo electrónico:
                            </td>
                            <td>
                                <asp:TextBox Columns="30" CssClass="campos" ID="txt_correo" runat="server" TextMode="SingleLine"
                                    ValidationGroup="Crear" MaxLength="75" />
                            </td>
                            <td><asp:CheckBox ID="chk_base" runat="server" CssClass="texto_usuario" Text="Base" /></td>
                        </tr>
                    </table>
                    <table border="0" align="center" cellpadding="0" cellspacing="1">
                        <tr class="texto_tablas_paginador">
                            <td height="22" align="center">
                                <asp:Label ID="lbl_id" runat="server" Visible="False"></asp:Label>
                                &nbsp;<asp:Button SkinID=""  CssClass="button" ID="btn_salvar_valor_dos" runat="server" Text="Guardar"
                                    OnClick="btn_salvar_valor_dos_Click" ValidationGroup="Crear" />
                                    
                                <asp:Label ID="Label7" runat="server" Visible="False"></asp:Label>
                                &nbsp;
                                <asp:Button SkinID=""  CssClass="button" ID="btnCacelar" runat="server" Text="Cancelar" OnClick="btn_Cancelar_Click"/>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" align="center" style="background-color:#ffffff">
                        <tr>
                            <td align="center">
                                &nbsp;<asp:Label ID="lblError" runat="server" BackColor="Transparent" CssClass="texto_usuario"
                                    Font-Bold="True" ForeColor="Red"></asp:Label>
                                <asp:GridView ID="dgv_tabla" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" CellPadding="0" CellSpacing="0" DataSourceID="ods_valores"                                    
                                    GridLines="Both" ShowFooter="True" Width="950px" DataKeyNames="AUT_ID" OnRowDataBound="dgv_tabla_RowDataBound"
                                    OnDataBound="dgv_tabla_DataBound" OnRowDeleting="dgv_tabla_RowDeleting" OnRowDeleted="dgv_tabla_RowDeleted"
                                    OnRowUpdating="dgv_tabla_RowUpdating" OnRowUpdated="dgv_tabla_RowUpdated">
                                    
                                    <PagerSettings FirstPageImageUrl="../images/pagina_primera.gif" FirstPageText="Primera"
                                        LastPageImageUrl="../images/pagina_ultima.gif" LastPageText="Ultima" Mode="NumericFirstLast"
                                        NextPageImageUrl="../images/pagina_siguiente.gif" NextPageText="Siguiente" PreviousPageImageUrl="../images/pagina_anterior.gif"
                                        PreviousPageText="Anterior" />
                                    <FooterStyle CssClass="texto_tablas_paginador" />
                                    <Columns>
                                        <asp:BoundField DataField="AUT_ID" HeaderText="ID" InsertVisible="False" ReadOnly="True"
                                            SortExpression="AUT_ID" Visible="False" />
                                        <asp:TemplateField HeaderText="NOMBRE">
                                            <EditItemTemplate>
                                                <asp:Label ID="lblAUT_ID" runat="server" Text='<%# Bind("AUT_ID") %>' style="display:none;" ></asp:Label>
                                                <asp:RequiredFieldValidator ID="rfv_txtModificarNombre" runat="server" ControlToValidate="TextBox1"
                                                    CssClass="texto_usuario" Display="None" ErrorMessage="El Nombre es requerido"
                                                    ForeColor="Navy" ValidationGroup="modificar"></asp:RequiredFieldValidator>
                                                <asp:TextBox style="width:90px" ID="TextBox1" runat="server" Text='<%# Bind("AUT_NOMBRE") %>' ValidationGroup="modificar"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAUT_ID" runat="server" Text='<%# Bind("AUT_ID") %>' style="display:none;"  ></asp:Label>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("AUT_NOMBRE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DESCRIPCION">
                                            <EditItemTemplate>
                                                <asp:RequiredFieldValidator ID="rfv_txtModificarDescripcion" runat="server" ControlToValidate="TextBox7"
                                                    CssClass="texto_usuario" Display="None" ErrorMessage="La Descripción es requerida"
                                                    ForeColor="Navy" ValidationGroup="modificar"></asp:RequiredFieldValidator>                                            
                                                <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("AUT_DESCRIPCION") %>' ValidationGroup="modificar"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("AUT_DESCRIPCION") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NIT">
                                            <EditItemTemplate>
                                                <asp:RequiredFieldValidator ID="rfv_txtModificarModNit" runat="server" ControlToValidate="TextBox2"
                                                    CssClass="texto_usuario" Display="None" ErrorMessage="El Nit es requerido" ForeColor="Navy"
                                                    ValidationGroup="modificar"></asp:RequiredFieldValidator>
                                                <asp:TextBox style="width:90px" ID="TextBox2" runat="server" Text='<%# Bind("AUT_NIT") %>' ValidationGroup="modificar"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("AUT_NIT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CheckBoxField DataField="AUT_ACTIVO" HeaderText="ACTIVO" />
                                        <asp:TemplateField HeaderText="DIRECCI&#211;N">
                                            <EditItemTemplate>
                                                <asp:RequiredFieldValidator ID="rfv_txtModificarDir" runat="server" ControlToValidate="TextBox6"
                                                    CssClass="texto_usuario" Display="None" ErrorMessage="La dirección es requerida"
                                                    ForeColor="Navy" ValidationGroup="modificar"></asp:RequiredFieldValidator>
                                                <asp:TextBox style="width:90px" ID="TextBox6" runat="server" Text='<%# Bind("AUT_DIRECCION") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("AUT_DIRECCION") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TEL&#201;FONO">
                                            <EditItemTemplate>
                                                <asp:RequiredFieldValidator ID="rfv_txtModificarTel" runat="server" ControlToValidate="TextBox4"
                                                    CssClass="texto_usuario" Display="None" ErrorMessage="El Telefono es requerido"
                                                    ForeColor="Navy" ValidationGroup="modificar"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revNumericoTelefonoMod" runat="server" ControlToValidate="TextBox4"
                                                    ErrorMessage="El campo telefono solo admite valores numericos" ValidationExpression="\d[0-9]*"
                                                    ValidationGroup="modificar" CssClass="texto_usuario" ForeColor="Navy" Display="None">
                                                </asp:RegularExpressionValidator>
                                                <asp:TextBox style="width:90px" ID="TextBox4" runat="server" Text='<%# Bind("AUT_TELEFONO") %>' ValidationGroup="modificar"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("AUT_TELEFONO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FAX">
                                            <EditItemTemplate>
                                                <asp:RequiredFieldValidator ID="rfv_txtModificarFax" runat="server" ControlToValidate="TextBox5"
                                                    CssClass="texto_usuario" Display="None" ErrorMessage="El fax es requerido"
                                                    ForeColor="Navy" ValidationGroup="modificar"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revNumericoTelefonoFax" runat="server" ControlToValidate="TextBox5"
                                                    ErrorMessage="El campo telefono solo admite valores numericos" ValidationExpression="\d[0-9]*"
                                                    ValidationGroup="modificar" CssClass="texto_usuario" ForeColor="Navy" Display="None">
                                                </asp:RegularExpressionValidator>
                                                <asp:TextBox style="width:90px" ID="TextBox5" runat="server" Text='<%# Bind("AUT_FAX") %>' ValidationGroup="modificar"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("AUT_FAX") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CORREO ELECTR&#211;NICO">
                                            <EditItemTemplate>
                                                <asp:RequiredFieldValidator ID="rfv_txtModificarMail" runat="server" ControlToValidate="TextBox3"
                                                    CssClass="texto_usuario" Display="None" ErrorMessage="El Mail es requerido"
                                                    ForeColor="Navy" ValidationGroup="modificar"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="rev_email_mod_exp" runat="server" ControlToValidate="TextBox3"
                                                    ErrorMessage="Su dirección de correo electrónico está incompleta o posee caracteres inválidos"
                                                    ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="modificar"
                                                    CssClass="texto_usuario" ForeColor="Navy" Display="None"></asp:RegularExpressionValidator>
                                                <asp:TextBox style="width:100%" ID="TextBox3" runat="server" Text='<%# Bind("AUT_CORREO") %>' ValidationGroup="modificar"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("AUT_CORREO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="APLICA RADICACION">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_radicacion" runat="server" Enabled="false" Checked='<%# Bind("AUT_APLICA_RADICACION") %>'>
                                                </asp:CheckBox>                                               
                                            </ItemTemplate>
                                              <EditItemTemplate>
                                                <asp:CheckBox ID="chk_radicacion_ed" runat="server" Checked='<%# Bind("AUT_APLICA_RADICACION") %>'>
                                                </asp:CheckBox>                                               
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="INTEGRADA VENTANILLA">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_Integrada" runat="server" Enabled="false" Checked='<%# Bind("AUT_INTEGRADA_VENTANILLA") %>'>
                                                </asp:CheckBox>                                               
                                            </ItemTemplate>
                                              <EditItemTemplate>
                                                <asp:CheckBox ID="chk_Integrada_ed" runat="server" Checked='<%# Bind("AUT_INTEGRADA_VENTANILLA") %>'>
                                                </asp:CheckBox>                                               
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        
                                         <asp:TemplateField HeaderText="Base" >
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_base" runat="server" Enabled="false" Checked='<%# Bind("AUT_BASE") %>'>
                                                </asp:CheckBox>                                               
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="chk_base_ed" runat="server" Checked='<%# Bind("AUT_BASE") %>'>
                                                </asp:CheckBox>                                               
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                                                    Text="Modificar" ValidationGroup="modificar"></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                                    Text="Cancelar"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                                                    Text="Editar"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_eliminar" runat="server" CausesValidation="False" CommandName="Delete"
                                                    Text="Eliminar"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                               
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle CssClass="titulo_tablas" />
                                    <AlternatingRowStyle CssClass="texto_tablas_dos" />
                                </asp:GridView>
                                <asp:ValidationSummary ID="vs_modificar" runat="server" ShowMessageBox="True" ShowSummary="False"
                                    ValidationGroup="modificar" />
                                &nbsp;<asp:ObjectDataSource ID="ods_valores" runat="server" DeleteMethod="Delete" InsertMethod="Insert" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetData" TypeName="AutoridadTableAdapters.AUTORIDAD_AMBIENTALTableAdapter"
                                    UpdateMethod="Update">
                                    <DeleteParameters>
                                        <asp:Parameter Name="Original_AUT_ID" Type="Int32" />
                                    </DeleteParameters>
                                    <UpdateParameters>
                                        <asp:Parameter Name="AUT_ID" Type="Int32" />
                                        <asp:Parameter Name="AUT_NOMBRE" Type="String" />
                                        <asp:Parameter Name="AUT_CARGUE" Type="Boolean" />
                                        <asp:Parameter Name="AUT_ACTIVO" Type="Boolean" />
                                        <asp:Parameter Name="AUT_DIRECCION" Type="String" />
                                        <asp:Parameter Name="AUT_TELEFONO" Type="String" />
                                        <asp:Parameter Name="AUT_FAX" Type="String" />
                                        <asp:Parameter Name="AUT_CORREO" Type="String" />
                                        <asp:Parameter Name="AUT_NIT" Type="String" />
                                        <asp:Parameter Name="AUT_APLICA_RADICACION" Type="Boolean" />
                                        <asp:Parameter Name="AUT_INTEGRADA_VENTANILLA" Type="Boolean" />
                                        <asp:Parameter Name="AUT_BASE" Type="Boolean" />
                                        <asp:Parameter Name="AUT_DESCRIPCION" Type="String" />
                                        <asp:Parameter Name="Original_AUT_ID" Type="Int32" />
                                    </UpdateParameters>
                                    <InsertParameters>
                                        <asp:ControlParameter ControlID="txt_valor" Name="AUT_NOMBRE" PropertyName="Text"
                                            Type="String" ConvertEmptyStringToNull="False" />
                                        <asp:ControlParameter ControlID="chk_activo" Name="AUT_ACTIVO" PropertyName="Checked"
                                            Type="Boolean" ConvertEmptyStringToNull="False" />
                                        <asp:ControlParameter ControlID="txt_direccion" Name="AUT_DIRECCION" PropertyName="Text"
                                            Type="String" ConvertEmptyStringToNull="False" />
                                        <asp:ControlParameter ControlID="txt_telefono" Name="AUT_TELEFONO" PropertyName="Text"
                                            Type="String" ConvertEmptyStringToNull="False" />
                                        <asp:ControlParameter ControlID="txt_fax" Name="AUT_FAX" PropertyName="Text" Type="String"
                                            ConvertEmptyStringToNull="False" />
                                        <asp:ControlParameter ControlID="txt_correo" Name="AUT_CORREO" PropertyName="Text"
                                            Type="String" ConvertEmptyStringToNull="False" />
                                        <asp:ControlParameter ControlID="txt_nit" Name="AUT_NIT" PropertyName="Text" Type="String"
                                            ConvertEmptyStringToNull="False" />
                                        <asp:Parameter DefaultValue="true" Name="AUT_CARGUE" Type="Boolean" />
                                        <asp:ControlParameter ControlID="chk_radicacion" Name="AUT_APLICA_RADICACION" PropertyName="Checked"
                                            Type="Boolean" />
                                        <asp:ControlParameter ControlID="chk_Integrada" Name="AUT_INTEGRADA_VENTANILLA" PropertyName="Checked"
                                            Type="Boolean" />
                                        <asp:ControlParameter ControlID="chk_base" Name="AUT_BASE" PropertyName="Checked"
                                            Type="Boolean" />
                                        <asp:ControlParameter ControlID="txtDescripcion" Name="AUT_DESCRIPCION" PropertyName="Text"
                                            Type="String" />
                                    </InsertParameters>
                                </asp:ObjectDataSource>
                                <asp:ValidationSummary ID="validacionResumen" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="Crear" />
                                <asp:RequiredFieldValidator ID="rfv_valor_req" runat="server" ControlToValidate="txt_valor"
                                    CssClass="texto_usuario" Display="None" ErrorMessage="El nombre de la autoridad ambiental es requerido"
                                    ForeColor="Navy" ValidationGroup="Crear"></asp:RequiredFieldValidator></td>
                        </tr>
                    </table>
                    <table cellspacing="0" cellpadding="1" width="100%" border="0">
                        <tbody>
                            <tr>
                                <td align="left" style="display:none;">
                                    <table cellspacing="2" cellpadding="1" border="0">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbl_pagina" runat="server" ToolTip="Página" Text="Página" CssClass="texto_usuario"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lbl_numero_pagina" runat="server" ToolTip="Usted se encuentra en esta página"
                                                        Text="10" CssClass="texto_usuario"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lbl_de" runat="server" ToolTip="de" Text="de" CssClass="texto_usuario"></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lbl_numero_paginas" runat="server" ToolTip="Número de páginas que contiene este listado"
                                                        Text="30" CssClass="texto_usuario"></asp:Label></td>
                                                <td style="width: 79px">
                                                    <asp:ImageButton ID="imb_ver_todos" OnClick="imb_ver_todos_Click" runat="server"
                                                        ToolTip="Haga clic aquí para ver el listado completo" Width="76" BorderWidth="0"
                                                        Height="16" ImageUrl="../images/ver_todos.gif"></asp:ImageButton></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <asp:Label ID="lbl_total" runat="server" CssClass="texto_usuario"></asp:Label>
                                </td>
                                <td width="68">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                <%--</td>
                <td width="6" valign="top" background="../images/cuadro6.gif">
                    <img src="../images/cuadro7.gif" width="6" height="6" /></td>
            </tr>
        </table>
        <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td width="6">
                    <img src="../images/cuadro3.gif" width="6" height="6" /></td>
                <td background="../images/cuadro4.gif">
                    <img src="../images/cuadro4.gif" width="3" height="6" /></td>
                <td width="6">
                    <img src="../images/cuadro5.gif" width="6" height="6" /></td>
            </tr>
        </table>--%>
        <asp:RequiredFieldValidator ID="rfv_identificacion" runat="server" ControlToValidate="txt_nit"
                    ErrorMessage="El número de identificación es requerido" ValidationGroup="Crear"
                    CssClass="texto_usuario" ForeColor="Navy" Display="None">
                </asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="revNumericoFax"
                    runat="server" ControlToValidate="txt_fax" ErrorMessage="El campo fax solo admite valores numericos"
                    ValidationExpression="\d+" ValidationGroup="Crear" CssClass="texto_usuario" ForeColor="Navy"
                    Display="None"></asp:RegularExpressionValidator><asp:RegularExpressionValidator ID="revNumericoTelefono"
                        runat="server" ControlToValidate="txt_telefono" ErrorMessage="El campo telefono solo admite valores numericos"
                        ValidationExpression="\d[0-9]*" ValidationGroup="Crear" CssClass="texto_usuario"
                        ForeColor="Navy" Display="None">
                    </asp:RegularExpressionValidator><asp:RegularExpressionValidator ID="rev_email_exp"
                        runat="server" ControlToValidate="txt_correo" ErrorMessage="Su dirección de correo electrónico está incompleta o posee caracteres inválidos"
                        ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Crear"
                        CssClass="texto_usuario" ForeColor="Navy" Display="None">
                    </asp:RegularExpressionValidator>
    </asp:Panel>
</asp:Content>
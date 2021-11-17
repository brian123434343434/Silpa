<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPALimpia.master" AutoEventWireup="true"
    CodeFile="ListarEdictoInscripcionAudienciaPublica.aspx.cs" Inherits="ListarEdictoInscripcionAudienciaPublica"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<script language="javascript" type="text/javascript">
    function ValidarCaracteres(textareaControl,maxlength){
         	
        if (textareaControl.value.length > maxlength){
    		textareaControl.value = textareaControl.value.substring(0,maxlength);
    		alert("Debe ingresar hasta un maximo de "+maxlength+" caracteres");
	    }
    }
    </script>
<div class="div-titulo">
        <asp:Label ID="Label2" runat="server" SkinID="titulo_principal_blanco" Text="Documentos Edicto">
        </asp:Label>
    &nbsp; &nbsp;
    <table>
        <tr>
            <td style="width: 3px">
            </td>
            <td>
    <asp:GridView ID="grdVerDocumentos" runat="server" AutoGenerateColumns="False"
        CellPadding="2" DataKeyNames="NombreArchivo,Ubicacion" ForeColor="#333333" GridLines="None"
        OnPageIndexChanging="grdPublicaciones_PageIndexChanging" OnRowCommand="grdPublicaciones_RowCommand" Width="100%" AllowPaging="True" PageSize="3">
        <RowStyle BackColor="#E3EAEB" />
        <Columns>
            <asp:BoundField DataField="NombreArchivo" HeaderText="Nombre Archivo" />
            <asp:ButtonField CommandName="Descargar" HeaderText="Descargar" Text="Descargar" />
        </Columns>
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#7C6F57" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
                <%--<asp:Button ID="btnRegresar" runat="server" BackColor="Teal" Text="Regresar" OnClick="btnRegresar_Click" /></td>--%><td>
            </td>
        </tr>
        <tr>
            <td style="width: 3px">
            </td>
            <td>
                <asp:Button ID="btnRegresar" runat="server" BackColor="Teal" Text="Regresar" OnClick="btnRegresar_Click" SkinID="boton_copia" /></td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="width: 3px">
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
</div> 
<DIV class="div-contenido2">
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
</DIV>

</asp:Content>

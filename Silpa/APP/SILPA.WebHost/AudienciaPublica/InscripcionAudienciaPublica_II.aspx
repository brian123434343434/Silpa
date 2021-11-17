<%@ Page Language="C#" MasterPageFile="~/plantillas/SILPALimpia.master" AutoEventWireup="true"
    CodeFile="InscripcionAudienciaPublica_II.aspx.cs" Inherits="Informacion_Publicaciones"
    Title="Inscripción Audiencia Publica" %>

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
<asp:Label id="lbl_titulo_principal" runat="server" SkinID="titulo_principal_blanco"></asp:Label><BR />
</div> 
<div class="div-contenido2">
    <asp:ScriptManager id="scmManejador" runat="server">
    </asp:ScriptManager>   

    <table width="80%" >
        <tr>
            <td>
                <asp:UpdatePanel id="UpdatePanel1" runat="server">
                <contenttemplate>
<TABLE width="100%"><TBODY><TR><TD width="25%"><asp:Label id="lblPrimerNombre" runat="server" SkinID="etiqueta_negra" Text="Primer Nombre"></asp:Label> </TD><TD width="75%"><asp:TextBox id="txtPrimerNombre" runat="server" SkinID="texto" Width="98%" MaxLength="30" ValidationGroup="grupoenviar"></asp:TextBox> <asp:RequiredFieldValidator id="rfvPrimerNombre" runat="server" Text="*" Display="None" ErrorMessage="Primer nombre es requerido" ControlToValidate="txtPrimerNombre" ValidationGroup="grupoenviar"></asp:RequiredFieldValidator> </TD></TR><TR><TD><asp:Label id="lblSegundoNombre" runat="server" SkinID="etiqueta_negra" Text="Segundo Nombre" Width="150px"></asp:Label> </TD><TD><asp:TextBox id="txtSegundoNombre" runat="server" SkinID="texto" Width="98%" MaxLength="30"></asp:TextBox> </TD></TR><TR><TD><asp:Label id="lblPrimerApellido" runat="server" SkinID="etiqueta_negra" Text="Primer Apellido" Width="150px"></asp:Label> </TD><TD><asp:TextBox id="txtPrimerApellido" runat="server" SkinID="texto" Width="98%" MaxLength="30" ValidationGroup="grupoenviar"></asp:TextBox> <asp:RequiredFieldValidator id="rfvPrimerApellido" runat="server" Text="*" Display="None" ErrorMessage="Primer apellido es requerido" ControlToValidate="txtPrimerApellido" ValidationGroup="grupoenviar"></asp:RequiredFieldValidator> </TD></TR><TR><TD><asp:Label id="lblSegundoApellido" runat="server" SkinID="etiqueta_negra" Text="Segundo Apellido" Width="150px"></asp:Label> </TD><TD><asp:TextBox id="txtsegundoApellido" runat="server" SkinID="texto" Width="98%" MaxLength="30"></asp:TextBox> </TD></TR><TR><TD><asp:Label id="lblTipoDocumento" runat="server" SkinID="etiqueta_negra" Text="Tipo de Documento:" Width="150px"></asp:Label> </TD><TD><asp:DropDownList id="cboTipoDocumento" runat="server" SkinID="lista_desplegable" AutoPostBack="True">
                        </asp:DropDownList> </TD></TR><TR><TD><asp:Label id="lblNumeroDocumento" runat="server" SkinID="etiqueta_negra" Text="N° Documento:" Width="150px"></asp:Label> </TD><TD><asp:TextBox id="txtCedula" runat="server" SkinID="texto_corto" MaxLength="20" ValidationGroup="grupoenviar"></asp:TextBox> <asp:RequiredFieldValidator id="rfvCedula" runat="server" Text="*" Width="2px" Display="None" ErrorMessage="Cedula es requerido" ControlToValidate="txtCedula" ValidationGroup="grupoenviar"></asp:RequiredFieldValidator> </TD></TR><TR><TD><asp:Label id="lblDe" runat="server" SkinID="etiqueta_negra" Text="De:" Width="150px"></asp:Label> </TD><TD><asp:DropDownList id="cboDepartamento" runat="server" SkinID="lista_desplegable" AutoPostBack="True" OnSelectedIndexChanged="cboDepartamento_SelectedIndexChanged">
                        </asp:DropDownList> <asp:TextBox id="txtDe" runat="server" SkinID="texto_corto" Visible="False" MaxLength="30"></asp:TextBox> </TD></TR><TR><TD><asp:Label id="Label1" runat="server" SkinID="etiqueta_negra" Width="146px">  </asp:Label> </TD><TD><asp:DropDownList id="cboMunicipioAudiencia" runat="server" SkinID="lista_desplegable">
                        </asp:DropDownList> </TD></TR><TR><TD><asp:Label id="lblCorreoElectronico" runat="server" SkinID="etiqueta_negra" Text="Correo Electrónico" Width="150px"></asp:Label> </TD><TD><asp:TextBox id="txtEmail" runat="server" SkinID="texto" MaxLength="100" ValidationGroup="grupoenviar"></asp:TextBox> <asp:RequiredFieldValidator id="rfvEmail" runat="server" Text="*" Display="None" ErrorMessage="Correo electrónico es requerido" ControlToValidate="txtEmail" ValidationGroup="grupoenviar"></asp:RequiredFieldValidator> <asp:RegularExpressionValidator id="revEmail" runat="server" Display="None" ErrorMessage="!El correo electronico no es valido!" ControlToValidate="txtEmail" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-&shy;9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" ValidationGroup="grupoenviar"></asp:RegularExpressionValidator> </TD></TR><TR><TD></TD></TR><TR><TD vAlign=top colSpan=2><asp:Label id="lblComunidad" runat="server" SkinID="etiqueta_negra" Text="Entidad o Comunidad" Width="150px"></asp:Label> </TD></TR><TR><TD vAlign=top colSpan=2><asp:TextBox id="txtEntidadComunidad" runat="server" SkinID="texto_sintamano" Width="98%" Height="50px" TextMode="MultiLine"></asp:TextBox> </TD></TR><TR><TD colSpan=2><asp:ValidationSummary id="ValidationSummary1" runat="server" ValidationGroup="grupoenviar"></asp:ValidationSummary> </TD></TR><TR><TD></TD></TR></TBODY></TABLE><cc1:filteredtextboxextender id="ftbexPrimerNombre" runat="server" validchars="ÁÉÍÓÚáéíóúñÑ´ -" targetcontrolid="txtPrimerNombre" filtertype="Custom, UppercaseLetters, LowercaseLetters"></cc1:filteredtextboxextender><cc1:filteredtextboxextender id="ftbexSegundoNombre" runat="server" validchars="ÁÉÍÓÚáéíóúñÑ´ -" targetcontrolid="txtSegundoNombre" filtertype="Custom, UppercaseLetters, LowercaseLetters"></cc1:filteredtextboxextender><cc1:filteredtextboxextender id="ftbexPrimerApellido" runat="server" validchars="ÁÉÍÓÚáéíóúñÑ´ -" targetcontrolid="txtPrimerApellido" filtertype="Custom, UppercaseLetters, LowercaseLetters"></cc1:filteredtextboxextender><cc1:filteredtextboxextender id="ftbexsegundoApellido" runat="server" validchars="ÁÉÍÓÚáéíóúñÑ´ -" targetcontrolid="txtsegundoApellido" filtertype="Custom, UppercaseLetters, LowercaseLetters"></cc1:filteredtextboxextender><cc1:filteredtextboxextender id="ftbexCedula" runat="server" targetcontrolid="txtCedula" filtertype="Numbers"></cc1:filteredtextboxextender><cc1:filteredtextboxextender id="ftbexDe" runat="server" validchars="ÁÉÍÓÚáéíóú´ -" targetcontrolid="txtDe" filtertype="Custom, UppercaseLetters, LowercaseLetters"></cc1:filteredtextboxextender><cc1:filteredtextboxextender id="ftbexEmail" runat="server" validchars="ÁÉÍÓÚáéíóú,.-@-_´" targetcontrolid="txtEmail" filtertype="Custom, Numbers, UppercaseLetters, LowercaseLetters"></cc1:filteredtextboxextender><cc1:filteredtextboxextender id="ftbexEntidadComunidad" runat="server" validchars="Ññ-_,. ;/" targetcontrolid="txtEntidadComunidad" filtertype="Custom, Numbers, UppercaseLetters, LowercaseLetters"></cc1:filteredtextboxextender> 
</contenttemplate>
					</asp:UpdatePanel>

                <table width="100%">                
                <tr>
                    <td align="left" valign="middle">
                        <asp:Label id="lblDocumentosAdjuntos" runat="server" SkinID="etiqueta_negra" Width="150px" Text="Documentos adjuntos"></asp:Label></td>
                    <td align="left" valign="middle">
                    </td>
                </tr>
                <tr>
                    <td vAlign="middle" align="left">
                        <asp:FileUpload id="uplAdjuntarArchivo" runat="server" Width="535px"></asp:FileUpload>
                    </td>
                    <td vAlign="middle" align="left">
                        <asp:Button id="cmdAdjuntar" onclick="cmdAdjuntar_Click" runat="server" SkinID="boton_copia" Width="80px" Text="Adjuntar" CausesValidation="False"></asp:Button> 
                    </td>
                </tr>
                <tr>
                    <td vAlign="middle" align="left" ><asp:ListBox id="lstListaArchivos" runat="server" Width="550px" Height="100px"></asp:ListBox> 
                    </td>
                    <td vAlign="top" align="left">
                        <asp:Button id="cmdEliminar" onclick="cmdEliminar_Click" runat="server" SkinID="boton_copia" Width="80px" Text="Eliminar" CausesValidation="False"></asp:Button>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td vAlign="middle" align="center" colSpan="2">
                        <asp:Label id="lblMensajeArchivos" runat="server" SkinID="etiqueta_roja_error"></asp:Label>
                    </td>
                </tr>                                    
                <tr>
                    <td>
                        <asp:Button id="cmdAceptar" onclick="cmdAceptar_Click" runat="server" SkinID="boton" Text="Enviar" ValidationGroup="grupoenviar"></asp:Button> <asp:Label id="lblNoAUP" runat="server" Width="61px" Enabled="False" Visible="False"></asp:Label> <asp:Label id="lblNoSILPA" runat="server" Enabled="False" Visible="False"></asp:Label> <asp:Label id="lblMensajeError" runat="server" SkinID="etiqueta_roja_error"></asp:Label>
                    <asp:Button ID="btnRegresar1" runat="server" Text="Regresar" SkinID="boton" OnClick="btnRegresar1_Click" ValidationGroup="gruporegresar" /></td>
                </tr>
            </table>
            <%--<asp:Label id="lbl_respuesta" runat="server" SkinID="etiqueta_negra" Text=""></asp:Label>--%>
        </td>

    </tr>
    <TR><TD vAlign=middle align=left style="width: 553px">&nbsp;
    <%--<asp:FileUpload id="uplAdjuntarArchivo" runat="server" Width="535px"></asp:FileUpload>--%></TD><TD vAlign=middle align=left>
<%--    <asp:Button id="cmdAdjuntar" onclick="cmdAdjuntar_Click" runat="server" SkinID="boton_copia" Width="80px" Text="Adjuntar" CausesValidation="False"></asp:Button> </TD></TR><TR><TD vAlign=middle align=left style="width: 553px"><asp:ListBox id="lstListaArchivos" runat="server" Width="550px" Height="100px"></asp:ListBox> </TD><TD vAlign=top align=left>
--%>    <%--<asp:Button id="cmdEliminar" onclick="cmdEliminar_Click" runat="server" SkinID="boton_copia" Width="80px" Text="Eliminar" CausesValidation="False"></asp:Button><BR />&nbsp;</TD></TR><TR><TD vAlign=middle align=center colSpan=2><asp:Label id="lblMensajeArchivos" runat="server" SkinID="etiqueta_roja_error"></asp:Label></TD></TR></TBODY></TABLE>--%>
            </td>

        </tr>
        <tr>
            <td>
                <%--<asp:Button id="cmdAceptar" onclick="cmdAceptar_Click" runat="server" SkinID="boton" Text="Enviar" ValidationGroup="grupouno"></asp:Button>--%>
                    &nbsp;
                <%--<asp:Label id="lblNoAUP" runat="server" Width="61px" Enabled="False" Visible="False">
                </asp:Label> --%>
                <%--<asp:Label id="lblNoSILPA" runat="server" Enabled="False" Visible="False"></asp:Label> <asp:Label id="lblMensajeError" runat="server" SkinID="etiqueta_roja_error"></asp:Label>
                </td>--%>
    
    <asp:Label id="lbl_respuesta" runat="server" SkinID="etiqueta_negra" Text=""></asp:Label>&nbsp; &nbsp; &nbsp; &nbsp;</tr>


    </asp:Content>

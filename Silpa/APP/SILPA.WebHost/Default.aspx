<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" MasterPageFile="~/plantillas/SILPAMenu.master" Theme="skin" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" Runat="Server">
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table>
        <tr>
            <td>
                <asp:Label ID="lblToken" runat="server" Text=""></asp:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td class="MensajeInicio">
                Usted se encuenta en la página principal del Sistema VITAL. Si desea Realizar un trámite, 
		            escoja la opción 'Iniciar Trámite' y seleccione el trámite deseado, para llenar el formulario de solicitud. Si ya tiene trámites en proceso
		            y desea consultar las actividades pendientes o ejecutarlas, seleccione la opción 'Tareas' y luego 'Mis Tareas'. Puede ver la 
		            lista de sus trámites en proceso a través de la opción 'Mis Trámites' y para 
		            Realizar Quejas o Denuncias o Consultar, Diligenciar formulario RUA seleccione la opción correcta en 'Otras Actividades'
                <br />
            </td>
        </tr>
    </table>
</asp:Content>
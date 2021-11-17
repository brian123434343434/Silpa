<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/plantillas/SILPA.master" CodeFile="ModuloAdministradorSUNL.aspx.cs" Inherits="Salvoconducto_ModuloAdministradorSUNL" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="headPlaceHolder" runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/jquery.datetimepicker.css" rel="stylesheet" />
    <style type="text/css">._css3m{display:none}</style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="div-titulo">
        <asp:Label ID="lblTituloPrincipal" runat="server" Text="Menu Administracion Salvoconducto" SkinID="titulo_principal_blanco"></asp:Label>
        &nbsp;
        <a href="#" id="hrCerrarVentana" visible="false" runat="server" onclick="window.close();return false;">Salir</a>
    </div>

    <div class="div-contenido">
            <table style="width:100%; height:100%">
                <tr >
                    <td >
                        <div id="menu">
                            <input type="checkbox" id="css3menu-switcher" class="c3m-switch-input">
                            <ul id="css3menu1" class="topmenu">
                                <li class="switch">
                                    <label onclick="" for="css3menu-switcher"></label>
                                </li>
                                <li class="topfirst"><a href="#" style="height: 30px; line-height: 10px;">Menu</a></li>
                                <li class="toplast"><a href="#" style="height: 30px; line-height: 10px;"><span>Parametrizacion Tablas</span></a>
                                    <ul>
                                        <li class="subfirst"><asp:LinkButton ID="LnkEspecieTipoProducto" runat="server" OnClick="LnkEspecieTipoProducto_Click">Parametrizacion Especies - Clases y tipos de producto</asp:LinkButton> </li>
                                    </ul>
                                </li>
                            </ul>

                        </div>
                    </td>
                </tr>

                <tr >
                    <td style="width:100%; height:100%">
                        <iframe src="<% = Url %>" title="Especies y unidades de medida" style="width: 95%; height: 95%"></iframe>
                    </td>
                </tr>
            </table>
            
            
        
    </div>
    <link href="../App_Themes/skin/EstiloMenuAdminSUNL.css" rel="stylesheet" />
</asp:Content>

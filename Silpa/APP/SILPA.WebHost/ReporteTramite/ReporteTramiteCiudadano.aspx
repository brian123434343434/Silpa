<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReporteTramiteCiudadano.aspx.cs" Inherits="ReporteTramite_ReporteTramiteCiudadano" MasterPageFile="~/ReporteTramite/ResourcesCP/master/ConsultaPublicaSILPA.master" %>

<%@ Register TagPrefix="CP" TagName="MisTramites" Src="~/ReporteTramite/MisTramites.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="input-group input-group-lg">
        <span class="input-group-btn">
            <button title="Inicio" id="homeIcon" class="btn btn-default btn-group-lg" type="button" onclick="location.href ='../../ventanillasilpa/';"><span class="glyphicon glyphicon-home pull-right"></span></button>
        </span>
    </div>
    <div class="div-titulo">
        <asp:Label ID="Label2" runat="server" Text="Mis Tramites Ciudadano">
        </asp:Label>
        <br />
        <br />
    </div>
    <CP:MisTramites ID="idMisTramites" runat="server" />
</asp:Content>


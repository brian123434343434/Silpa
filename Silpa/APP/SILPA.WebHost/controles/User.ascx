<%@ Control Language="C#" AutoEventWireup="true" CodeFile="User.ascx.cs" Inherits="controles_User" %>
<div class="datosUsuario text-right">
    <div class="row">
        <div class="col-6">
            <Label>Último Acceso:</Label>
            <asp:Label ID="lblAcceso" runat="server"></asp:Label>
        </div>
        <div class="col-6">
            <Label> Usuario:</Label>
            <asp:Label ID="lblUsuario" runat="server"></asp:Label>
        </div>
        
    </div>
</div>

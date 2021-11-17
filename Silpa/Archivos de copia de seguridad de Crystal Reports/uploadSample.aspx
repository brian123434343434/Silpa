<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPA.master" AutoEventWireup="true"
    CodeFile="uploadSample.aspx.cs" Inherits="uploadSample" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script>
        function iFrame_OnUploadComplete() {
            document.form1.btnUploaded.click();
        }
    </script>

    <div id="div-titulo">
        <asp:Label ID="lbl_titulo" SkinID="titulo_principal_blanco" runat="server" Text="Ejemplo de Upload"></asp:Label>
    </div>
    <div id="div-contenido">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <iframe name="iUploadFrame" src="myIFrame.htm" frameborder="0" onload="iUploadFrameLoad();">
        </iframe>
        <asp:UpdatePanel ID="upn_1" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblMessage" runat="server" />
                <asp:Button ID="btnUploadCompleted" runat="server" Style="visibility: hidden;" OnClick="btnUploadComplted_Click" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnUploadCompleted" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>

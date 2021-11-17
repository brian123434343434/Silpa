<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPASinMenu.master"
    AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="EntradaEIA_Default"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="Fichas/ctrFicha1.ascx" TagName="ctrFicha1" TagPrefix="uc1" %>
<%@ Register Src="Fichas/ctrFicha2.ascx" TagName="ctrFicha2" TagPrefix="uc2" %>
<%@ Register Src="Fichas/ctrFicha3.ascx" TagName="ctrFicha3" TagPrefix="uc3" %>
<%@ Register Src="Fichas/ctrFicha4.ascx" TagName="ctrFicha4" TagPrefix="uc4" %>
<%@ Register Src="Fichas/ctrFicha5.ascx" TagName="ctrFicha5" TagPrefix="uc5" %>
<%@ Register Src="Fichas/ctrFicha6.ascx" TagName="ctrFicha6" TagPrefix="uc6" %>
<%@ Register Src="Fichas/ctrFicha7.ascx" TagName="ctrFicha7" TagPrefix="uc7" %>
<%@ Register Src="Fichas/ctrFicha8.ascx" TagName="ctrFicha8" TagPrefix="uc8" %>
<%@ Register Src="Fichas/ctrFicha9.ascx" TagName="ctrFicha9" TagPrefix="uc9" %>
<%@ Register Src="Fichas/ctrFicha10.ascx" TagName="ctrFicha10" TagPrefix="uc10" %>
<%@ Register Src="Fichas/ctrFicha11.ascx" TagName="ctrFicha11" TagPrefix="uc11" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlaceHolder" runat="Server">
    <style type="text/css">
        .style1
        {
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="div-titulo">
        <asp:Label ID="lbl_titulo_principal" runat="server" SkinID="titulo_principal_blanco"
            Text="RESUMEN EIA"></asp:Label>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    
  


    <div style="text-align: right">
        <asp:Label runat="server" ID="lblNombreProyecto" Text="Nombre del Proyecto: " SkinID="etiqueta_verde">
        </asp:Label>
                      
    <div style="text-align: right">
        <asp:Label runat="server" ID="lblNumeroVital" Text="Numero Vital Asignado: " SkinID="etiqueta_verde">
        </asp:Label>
    </div>
    </div>
    

    <div class="div-contenido">                 
        <table style="width: 450px">
                    <tbody>
                        <tr>
                            <td style="height: 21px">
                            </td>
                            <td style="height: 21px">
                            </td>
                            <td style="width: 6px; height: 21px">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="IdUser" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lblIdProyecto" runat="server" Text="" Visible="false" ForeColor="Black"></asp:Label>
                          

                        
                                <cc1:TabContainer ID="tbFormularioEIA" runat="server" ForeColor="Black" Font-Names="Arial"
                                    Width="100%" AutoPostBack="True" ActiveTabIndex="0"  OnActiveTabChanged="ActiveTabChanged">
                                    <cc1:TabPanel runat="server" ID="tbFicha1">
                                        <HeaderTemplate>
                                            Ficha 1</HeaderTemplate>
                                        <ContentTemplate>
                                            <uc1:ctrFicha1 ID="ctrFicha1" runat="server" />
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" ID="tbFicha2">
                                        <HeaderTemplate>
                                            Ficha 2</HeaderTemplate>
                                        <ContentTemplate>
                                            <uc2:ctrFicha2 ID="ctrFicha2" runat="server" />
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" ID="tbFicha3">
                                        <HeaderTemplate>
                                            Ficha 3</HeaderTemplate>
                                        <ContentTemplate>
                                            <uc3:ctrFicha3 ID="ctrFicha3" runat="server"/>
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" ID="tbFicha4">
                                        <HeaderTemplate>
                                            Ficha 4</HeaderTemplate>
                                        <ContentTemplate>
                                            <uc4:ctrFicha4 ID="ctrFicha4" runat="server" />
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" ID="tbFicha15">
                                        <HeaderTemplate>
                                            Ficha 5
                                        </HeaderTemplate>
                                        <ContentTemplate>
                                            <uc5:ctrFicha5 runat="server" ID="ctrFicha5" />
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" ID="tbFicha6">
                                        <HeaderTemplate>
                                            Ficha 6</HeaderTemplate>
                                        <ContentTemplate>
                                            <uc6:ctrFicha6 ID="ctrFicha6" runat="server" />
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" ID="tbFicha7">
                                        <HeaderTemplate>
                                            Ficha 7</HeaderTemplate>
                                        <ContentTemplate>
                                            <uc7:ctrFicha7 ID="ctrFicha7" runat="server" />
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" ID="TabPanel1">
                                        <HeaderTemplate>
                                            Ficha 8</HeaderTemplate>
                                        <ContentTemplate>
                                        <uc8:ctrFicha8  runat="server" ID="ctrFicha8" />
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" ID="TabPanel2">
                                        <HeaderTemplate>
                                            Ficha 9</HeaderTemplate>
                                        <ContentTemplate>
                                            <uc9:ctrFicha9 runat="server" ID="ctrFicha9" />
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" ID="TabPanel3">
                                        <HeaderTemplate>
                                            Ficha 10</HeaderTemplate>
                                        <ContentTemplate>
                                            <uc10:ctrFicha10 ID="ctrFicha10" runat="server" />
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                    <cc1:TabPanel runat="server" ID="TabPanel4">
                                        <HeaderTemplate>
                                            Ficha 11</HeaderTemplate>
                                        <ContentTemplate>
                                            <uc11:ctrFicha11 ID="ctrFicha11" runat="server" />
                                        </ContentTemplate>
                                    </cc1:TabPanel>
                                </cc1:TabContainer>
                            
                          

                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnAnterior" Visible="false" runat="server" Text="Anterior" OnClick="btnAnterior_Click" SkinID="boton_copia" />
                                <asp:Button ID="btnSiguiente" runat="server" Text="Siguiente" OnClick="btnSiguiente_Click" SkinID="boton_copia" />
                            </td>
                        </tr>
                        <tr>
                            <br />
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Cambios del Proyecto" Visible="false" SkinID="boton_copia" OnClick="btnGuardar_Click"/>                                
                                <asp:Button ID="btnFinalizar" runat="server" Text="Finalizar Proyecto" Visible="false" SkinID="boton_copia" OnClientClick="return confirm('Al finalizar el proyecto no podra hacer mas cambios. \n¿Esta Seguro que desea Finalizar?')" OnClick="btnFinalizar_Click"/>                                
                                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" Visible="true" SkinID="boton_copia" OnClick="btnRegresar_Click"/>                                
                            </td>
                        </tr>
                    </tbody>
                </table>            
    </div>
   
</asp:Content>

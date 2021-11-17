<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ctlDemandaUso.ascx.cs" Inherits="LicenciasAmbientales_Controles_wucDemanda_Uso" %>
<p class="MsoNormal" style="margin: 0cm 0cm 0pt; tab-stops: 54.0pt">
    <asp:Panel ID="Panel1" runat="server" Height="50px" Width="125px">
        <table style="width: 451px; height: 144px">
            <tr>
                <td colspan="7" style="height: 6px">
                    <asp:Label ID="Label23" runat="server" Text="PARA PERMISO DE CONCESIÓN DE AGUAS  SUPERFIALES, PROSPECCIÓN Y EXPLORACIÓN DE AGUAS SUBTERRÁNEAS Y CONCESIÓN DE AGUAS SUBTERRÁNEAS. "
                        Width="1198px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 219px; height: 6px">
                    <asp:Label ID="Label8" runat="server" Text="DEMANDA / USO" Width="130px"></asp:Label></td>
                <td style="width: 159px; height: 6px">
                </td>
                <td style="height: 6px">
                </td>
                <td style="width: 3px; height: 6px">
                </td>
                <td style="width: 3px; height: 6px">
                </td>
                <td style="width: 3px; height: 6px">
                </td>
                <td style="width: 3px; height: 6px">
                </td>
            </tr>
            <tr>
                <td style="width: 219px; height: 21px">
                    <div>
                        <div>
                            <asp:CheckBox ID="CheckBox1" runat="server" Text="1. Doméstico" Width="137px" />&nbsp;</div>
                    </div>
                </td>
                <td style="width: 159px; height: 21px">
                    <div>
                        <asp:Label ID="Label2" runat="server" Text="No. de personas permanentes:" Width="189px"></asp:Label>
                    </div>
                </td>
                <td style="height: 21px">
                    <div>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></div>
                </td>
                <td style="width: 3px; height: 21px">
                    <div style="text-align: right">
                        <asp:Label ID="Label3" runat="server" Text="Transitorias:"></asp:Label></div>
                </td>
                <td style="width: 3px; height: 21px">
                    <div>
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></div>
                </td>
                <td style="width: 3px; height: 21px">
                </td>
                <td style="width: 3px; height: 21px">
                </td>
            </tr>
            <tr>
                <td style="width: 219px; height: 21px">
                    <div>
                        <div>
                            <asp:CheckBox ID="CheckBox2" runat="server" Text="2.  Pecuario" /></div>
                    </div>
                </td>
                <td style="width: 159px; height: 21px">
                    <div style="text-align: right">
                        &nbsp;<asp:Label ID="Label1" runat="server" Text="Animales:" Width="69px"></asp:Label></div>
                </td>
                <td style="height: 21px">
                    <div>
                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></div>
                </td>
                <td style="width: 3px; height: 21px">
                    <div style="text-align: right">
                        <asp:Label ID="Label4" runat="server" Text="Número:" Width="59px"></asp:Label></div>
                </td>
                <td style="width: 3px; height: 21px">
                    <div>
                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></div>
                </td>
                <td style="width: 3px; height: 21px">
                </td>
                <td style="width: 3px; height: 21px">
                </td>
            </tr>
            <tr>
                <td style="width: 219px; height: 21px">
                    <div>
                        <div>
                            <asp:CheckBox ID="CheckBox3" runat="server" Text="3.  Riego" /></div>
                    </div>
                </td>
                <td style="width: 159px; height: 21px">
                    <div style="text-align: right">
                        &nbsp;<asp:Label ID="Label5" runat="server" Text="Cultivo:" Width="46px"></asp:Label></div>
                </td>
                <td style="height: 21px">
                    <div>
                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></div>
                </td>
                <td style="width: 3px; height: 21px">
                    <div style="text-align: right">
                        <asp:Label ID="Label6" runat="server" Text="Área(Ha):" Width="71px"></asp:Label></div>
                </td>
                <td style="width: 3px; height: 21px">
                    <div>
                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox></div>
                </td>
                <td style="width: 3px; height: 21px">
                </td>
                <td style="width: 3px; height: 21px">
                </td>
            </tr>
            <tr>
                <td style="width: 219px; height: 21px">
                    <asp:Label ID="Label7" runat="server" Height="43px" Text="Tipo de Riego" Width="189px"></asp:Label></td>
                <td colspan="4" style="height: 21px; text-align: center">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                        Width="585px">
                        <asp:ListItem>Goteo</asp:ListItem>
                        <asp:ListItem>Aspersi&#243;n</asp:ListItem>
                        <asp:ListItem>Gravedad</asp:ListItem>
                        <asp:ListItem>Demanda</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td colspan="1" style="height: 21px; text-align: center">
                </td>
                <td colspan="1" style="height: 21px; text-align: center">
                </td>
            </tr>
            <tr>
                <td style="width: 219px; height: 11px">
                </td>
                <td style="width: 159px; height: 11px">
                </td>
                <td style="height: 11px">
                </td>
                <td style="width: 3px; height: 11px">
                </td>
                <td style="width: 3px; height: 11px">
                </td>
                <td style="width: 3px; height: 11px">
                </td>
                <td style="width: 3px; height: 11px">
                </td>
            </tr>
            <tr>
                <td style="width: 219px; height: 11px">
                    <asp:CheckBox ID="CheckBox8" runat="server" Text="4.  Industrial" /></td>
                <td style="width: 159px; height: 11px; text-align: right">
                    <asp:Label ID="Label9" runat="server" Text="Clase de Industria:" Width="112px"></asp:Label></td>
                <td style="height: 11px">
                    <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox></td>
                <td style="width: 3px; height: 11px; text-align: right">
                    <asp:Label ID="Label10" runat="server" Text="Demanda:" Width="67px"></asp:Label></td>
                <td style="width: 3px; height: 11px">
                    <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox></td>
                <td style="width: 3px; height: 11px">
                </td>
                <td style="width: 3px; height: 11px">
                </td>
            </tr>
            <tr>
                <td style="width: 219px; height: 21px">
                    <asp:CheckBox ID="CheckBox4" runat="server" Text="5.  Generación de Energía" /></td>
                <td style="width: 159px; height: 21px; text-align: right">
                    <asp:Label ID="Label11" runat="server" Text="¿ Cual ?" Width="57px"></asp:Label></td>
                <td colspan="3" style="height: 21px">
                    <div>
                        <asp:TextBox ID="TextBox9" runat="server" Width="392px"></asp:TextBox></div>
                </td>
                <td colspan="1" style="height: 21px">
                </td>
                <td colspan="1" style="height: 21px">
                </td>
            </tr>
            <tr>
                <td style="width: 219px; height: 21px">
                    <asp:CheckBox ID="CheckBox5" runat="server" Text="6.  Abastecimiento" /></td>
                <td style="width: 159px; height: 21px; text-align: right">
                    <asp:Label ID="Label12" runat="server" Text="Acueducto:" Width="73px"></asp:Label></td>
                <td style="height: 21px; text-align: right">
                    <asp:CheckBox ID="CheckBox6" runat="server" Text="Veredal  " TextAlign="Left" Width="73px" /></td>
                <td colspan="2" style="height: 21px">
                    <div>
                        <asp:Label ID="Label13" runat="server" Text="Vereda:"></asp:Label>
                        <asp:TextBox ID="TextBox10" runat="server" Width="177px"></asp:TextBox></div>
                </td>
                <td style="width: 3px; height: 21px">
                    <div>
                        <asp:Label ID="Label14" runat="server" Text="No. de Usuarios:" Width="105px"></asp:Label>
                        <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox></div>
                </td>
                <td style="width: 3px; height: 21px">
                </td>
            </tr>
            <tr>
                <td style="width: 219px; height: 21px">
                </td>
                <td style="width: 159px; height: 21px">
                </td>
                <td style="height: 21px; text-align: right">
                    <asp:CheckBox ID="CheckBox7" runat="server" Text="Municipal  " TextAlign="Left" Width="88px" /></td>
                <td colspan="2" style="height: 21px">
                    <div>
                        <asp:Label ID="Label15" runat="server" Text="Municipio:"></asp:Label>
                        <asp:DropDownList ID="DropDownList1" runat="server" Width="163px">
                        </asp:DropDownList></div>
                </td>
                <td style="width: 3px; height: 21px">
                    <asp:Label ID="Label16" runat="server" Text="ESP"></asp:Label><asp:TextBox ID="TextBox12"
                        runat="server"></asp:TextBox></td>
                <td style="width: 3px; height: 21px">
                    <asp:Label ID="Label17" runat="server" Text="No. de Usuarios:" Width="105px"></asp:Label><asp:TextBox
                        ID="TextBox13" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 219px; height: 21px">
                    <asp:CheckBox ID="CheckBox9" runat="server" Text="7.  Otro" /></td>
                <td style="width: 159px; height: 21px; text-align: right">
                    <asp:Label ID="Label18" runat="server" Text="¿ Cual ?" Width="58px"></asp:Label></td>
                <td colspan="2" style="height: 21px">
                    <asp:TextBox ID="TextBox16" runat="server" Width="231px"></asp:TextBox></td>
                <td style="width: 3px; height: 21px">
                </td>
                <td style="width: 3px; height: 21px">
                </td>
                <td style="width: 3px; height: 21px">
                </td>
            </tr>
            <tr>
                <td style="width: 219px; height: 21px">
                    <asp:CheckBox ID="CheckBox11" runat="server" Text="8.  Caudal solicitado (l/s)" /></td>
                <td colspan="2" style="height: 21px">
                    <div>
                        <asp:TextBox ID="TextBox14" runat="server" Width="348px"></asp:TextBox></div>
                </td>
                <td style="width: 3px; height: 21px">
                </td>
                <td style="width: 3px; height: 21px">
                </td>
                <td style="width: 3px; height: 21px">
                </td>
                <td style="width: 3px; height: 21px">
                </td>
            </tr>
            <tr>
                <td style="width: 219px; height: 21px">
                    &nbsp;<asp:CheckBox ID="CheckBox10" runat="server" Text="9. Término por el cual se solicita la concesión"
                        Width="298px" /></td>
                <td colspan="2" style="height: 21px">
                    <span style="font-size: 10pt; font-family: Arial">
                        <div>
                            <asp:TextBox ID="TextBox15" runat="server" Width="347px"></asp:TextBox></div>
                    </span>
                </td>
                <td style="width: 3px; height: 21px">
                </td>
                <td style="width: 3px; height: 21px">
                </td>
                <td style="width: 3px; height: 21px">
                </td>
                <td style="width: 3px; height: 21px">
                </td>
            </tr>
            <tr>
                <td style="width: 219px; height: 21px">
                </td>
                <td style="width: 159px; height: 21px">
                </td>
                <td style="height: 21px">
                </td>
                <td style="width: 3px; height: 21px">
                </td>
                <td style="width: 3px; height: 21px">
                </td>
                <td style="width: 3px; height: 21px">
                </td>
                <td style="width: 3px; height: 21px">
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 21px">
                    <asp:Label ID="Label19" runat="server" Text="DOCUMENTACIÓN QUE DEBE ANEXAR A LA SOLICITUD"
                        Width="420px"></asp:Label></td>
                <td colspan="3" style="height: 21px">
                </td>
                <td style="width: 3px; height: 21px">
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 21px">
                    <asp:Label ID="Label20" runat="server" Text="1. Certificado de tradición y libertad expedido máximo con tres (3) meses de antelación. "
                        Width="540px"></asp:Label></td>
                <td colspan="3" style="height: 21px">
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="392px" /></td>
                <td style="width: 3px; height: 21px">
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 21px">
                    <asp:Label ID="Label21" runat="server" Text=" 2. Plancha IGAC escala 1: 10.000 señalando ubicación predio y pozo."
                        Width="540px"></asp:Label></td>
                <td colspan="3" style="height: 21px">
                    <asp:FileUpload ID="FileUpload2" runat="server" Width="390px" /></td>
                <td style="width: 3px; height: 21px">
                </td>
            </tr>
            <tr>
                <td colspan="3" style="height: 21px">
                    <asp:Label ID="Label22" runat="server" Text=" 3. Características hidrogeológicas de la zona e relación de otros aprovechamientos de aguas subterráneas dentro del área que determine la autoridad ambiental competente. "
                        Width="656px"></asp:Label></td>
                <td colspan="3" style="height: 21px">
                    <asp:FileUpload ID="FileUpload3" runat="server" Width="392px" /></td>
                <td style="width: 3px; height: 21px">
                </td>
            </tr>
        </table>
    </asp:Panel>
    &nbsp;</p>
<p>
    <span style="font-size: 10pt; font-family: 'Futura Bk','sans-serif'; mso-bidi-font-family: Arial;
        mso-ansi-language: ES-CO">
        <?xml namespace="" ns="urn:schemas-microsoft-com:office:office" prefix="o" ?><o:p></o:p></span>&nbsp;</p>

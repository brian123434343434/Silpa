<%@ Page AutoEventWireup="true" CodeFile="EdicionMenu.aspx.cs" Inherits="GrupoYUsuarios_EdicionMenu" Language="C#" MasterPageFile="~/plantillas/SILPA.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="headPlaceHolder" runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>

    <meta http-equiv="Cache-Control" content="Max-age=0" />

    <style type="text/css">
        table {
            /*border: 1px solid #000;*/
        }
        table tr td {
            border: 0px solid #ddd !important;
            padding: 4px;
        }
        .Button{
            background-color: #ddd;
        }

        #tblMenu {
            width: auto;
        }

            #tblMenu table {
                width: auto;
            }

            #tblMenu .imgItemMenu {
                vertical-align: top;
            }

                #tblMenu .imgItemMenu img, .imgAction {
                    cursor: pointer;
                    margin-top: 7px;
                    height: 20px;
                    width: 20px;
                }

            #tblMenu fieldset {
                width: 400px;
            }

                #tblMenu fieldset:hover {
                    width: 400px;
                    background-color: #aaaaa;
                }

        .borderTop {
            width: 100%;
            border-top: 1px solid #000000;
        }

            .borderTop td input, .borderTop td img {
                vertical-align: middle;
            }

        .borderBottom {
            width: 100%;
            border-bottom: 1px solid #000000;
        }

            .borderBottom td input, .borderBottom td img {
                vertical-align: middle;
            }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="div-titulo">
        <asp:Label ID="Label1" runat="server" SkinID="titulo_principal_blanco" Text="Edición de Menú"></asp:Label>
    </div>

    <div class="table-responsive">
        <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important;">
            <tr>
                <td style="text-align: right">Menú:</td>
                <td>
                    <asp:TextBox ID="txtNombreMenu" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtSelectedMenu" runat="server" Style="display: none;"></asp:TextBox>
                    <asp:TextBox ID="txtGrupos" runat="server" Style="display: none;"></asp:TextBox>
                    <asp:TextBox ID="txtMenuId" runat="server" Style="display: none;"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <%=strGrupos%>
                </td>
            </tr>
        </table>
        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important;" class="borderBottom">
            <tr>
                <td style="padding: 20px; text-align: center; vertical-align: middle;">
                    <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; width: 100%;">
                        <tr>
                            <td style="text-align: right; vertical-align: middle;">
                                <img class="imgAction" src='../images/Knob_Add.jpg' onclick='addMenuNivelCero()' alt='' />
                            </td>
                            <td style="padding-right: 30px; padding-left: 30px; text-align: center; vertical-align: middle;">
                                <asp:Button ID="btnSaveMenu1" runat="server" Text="Guardar Menú" OnClick="btnGuardarMenu_Click" />
                            </td>
                            <td style="text-align: left; vertical-align: middle;">
                                <asp:Button ID="btnCancelar" runat="server" Text="Atras" OnClick="btnCancelarMenu_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important;" id="tblMenu"><%=strTableMenu%></table>
        <table style="left: 0 !important; margin: 0 !important; padding: 0 !important;" class="borderTop">
            <tr>
                <td style="padding: 20px; text-align: center; vertical-align: middle;">
                    <table border="0" style="left: 0 !important; margin: 0 !important; padding: 0 !important; width: 100%;">
                        <tr>
                            <td style="text-align: right; vertical-align: middle;">
                                <img class="imgAction" src='../images/Knob_Add.jpg' onclick='addMenuNivelCero()' alt='' />
                            </td>
                            <td style="padding-right: 30px; padding-left: 30px; text-align: center; vertical-align: middle;">
                                <asp:Button ID="btnGuardarMenu" runat="server" Text="Guardar Menú" OnClick="btnGuardarMenu_Click" />
                            </td>
                            <td style="text-align: left; vertical-align: middle;">
                                <asp:Button ID="btnCancelar1" runat="server" Text="Atras" OnClick="btnCancelarMenu_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        window.setTimeout("renameMenus()", 300);

        function updateGroups() {
            var tblGroups = document.getElementById("tblGroups");
            var txtGrupos = document.getElementById("<%=txtGrupos.ClientID%>");
            txtGrupos.value = "";

            for (var kk = 0; kk < tblGroups.rows.length; kk++) {
                var inputs = tblGroups.rows[kk].getElementsByTagName("INPUT");

                for (var jj = 0; jj < inputs.length; jj++) {
                    if (inputs[jj].checked)
                        txtGrupos.value += (txtGrupos.value != "" ? "," : "") + inputs[jj].value;
                }
            }
        }

        function validateSave() {
            var selectedMenu = document.getElementById("<%=txtGrupos.ClientID%>").value;

            if (selectedMenu == "") {
                var strMSG = "Debe seleccionar almenos un grupo.";
                alert(strMSG);
                return false;
            }

            return true;
        }

        function addMenuNivelCero() {

            var tblMenu = document.getElementById("tblMenu");
            var row = tblMenu.insertRow(tblMenu.rows.length);
            var cell = row.insertCell(row.cells.length);

            cell.innerHTML = "<table class='itemMenu'>" +
                    "<tr>" +
                    "    <td class='imgItemMenu'>" +
                    "        <img src='../images/Knob_Add.jpg' onclick='addMenu(this)' /><br />" +
                    "        <img src='../images/Knob_Cancel.jpg' onclick='deleteMenu(this)' />" +
                    "    </td>" +
                    "    <td>" +
                    "        <fieldset>" +
                    "            <legend>Menú</legend>" +
                    "            <table>" +
                    "                <tr>" +
                    "                    <td>Texto:</td>" +
                    "                    <td><input type='text' value=''/></td>" +
                    "                    <td>Valor:</td>" +
                    "                    <td><input type='text' value=''/></td>" +
                    "                </tr>" +
                    "                <tr>" +
                    "                    <td>Url:</td>" +
                    "                    <td><input type='text' value=''/></td>" +
                    "                    <td>Target:</td>" +
                    "                    <td><input type='text' value=''/></td>" +
                    "                </tr>" +
                    "            </table>" +
                    "        </fieldset>" +
                    "        <br/>" +
                    "        <div></div>" +
                    "    </td>" +
                    "</tr>" +
                    "</table>";


            cell.getElementsByTagName("INPUT")[0].focus();
            window.setTimeout("renameMenus()", 300);
        }

        function addMenu(src) {

            var targetDiv = src.parentNode.parentNode.cells[1].getElementsByTagName("DIV")[0];
            var div = document.createElement("DIV", targetDiv);
            var htmlSubMenu = "<table class='itemMenu'>" +
                    "<tr>" +
                    "    <td class='imgItemMenu'>" +
                    "        <img src='../images/Knob_Add.jpg' onclick='addMenu(this)' /><br />" +
                    "        <img src='../images/Knob_Cancel.jpg' onclick='deleteMenu(this)' />" +
                    "    </td>" +
                    "    <td>" +
                    "        <fieldset>" +
                    "            <legend>Menú</legend>" +
                    "            <table>" +
                    "                <tr>" +
                    "                    <td>Texto:</td>" +
                    "                    <td><input type='text' value=''/></td>" +
                    "                    <td>Valor:</td>" +
                    "                    <td><input type='text' value=''/></td>" +
                    "                </tr>" +
                    "                <tr>" +
                    "                    <td>Url:</td>" +
                    "                    <td><input type='text' value=''/></td>" +
                    "                    <td>Target:</td>" +
                    "                    <td><input type='text' value=''/></td>" +
                    "                </tr>" +
                    "            </table>" +
                    "        </fieldset>" +
                    "        <br/>" +
                    "        <div></div>" +
                    "    </td>" +
                    "</tr>" +
                    "</table>";



            div.innerHTML = htmlSubMenu;
            targetDiv.appendChild(div);

            div.getElementsByTagName("INPUT")[0].focus();

            window.setTimeout("renameMenus();", 50);
        }

        function deleteMenu(src) {
            var menuName = src.parentNode.parentNode.cells[1].getElementsByTagName("INPUT")[0].value;
            menuName = (menuName != "" ? menuName : "Vacio");
            if (confirm("Eliminar menú: " + menuName)) {

                src.parentNode.parentNode.parentNode.parentNode.parentNode.innerHTML = "";
                window.setTimeout("renameMenus();", 50);

            }
        }

        function renameMenus() {

            var tblMenu = document.getElementById("tblMenu");
            var excluded = 0;

            for (var j = 0; j < tblMenu.rows.length; j++) {

                if (tblMenu.rows[j].cells[0].getElementsByTagName("TABLE").length == 0) {
                    excluded++;
                    continue;
                }

                var currentMenu = tblMenu.rows[j].cells[0].getElementsByTagName("TABLE")[0];
                var fieldSet = currentMenu.rows[0].cells[1].getElementsByTagName("FIELDSET")[0];
                var inputs = fieldSet.getElementsByTagName("INPUT");

                inputs[0].name = "menu0_" + (j - excluded) + "_text";
                inputs[1].name = "menu0_" + (j - excluded) + "_valor";
                inputs[2].name = "menu0_" + (j - excluded) + "_url";
                inputs[3].name = "menu0_" + (j - excluded) + "_target";

                if (currentMenu.rows[0].cells[1].getElementsByTagName("DIV")[0].getElementsByTagName("TABLE").length > 0)
                    renameSubMenus("0_" + (j - excluded), currentMenu.rows[0].cells[1].getElementsByTagName("DIV")[0]);

            }
        }

        function renameSubMenus(tagNivel, src) {
            //srcDiv.childNodes???
            //debugger;
            for (var k = 0; k < src.childNodes.length; k++) {
                if (src.childNodes[k].getElementsByTagName("TABLE").length > 0) {

                    var currentMenu = src.childNodes[k];//src.childNodes[ k ].getElementsByTagName( "TABLE" )[ 0 ];
                    currentMenu = (currentMenu.tagName == "DIV" ? currentMenu.getElementsByTagName("TABLE")[0] : currentMenu);
                    var fieldSet = currentMenu.rows[0].cells[1].getElementsByTagName("FIELDSET")[0];
                    var inputs = fieldSet.getElementsByTagName("INPUT");

                    inputs[0].name = "menu" + tagNivel + "_" + k + "_text";
                    inputs[1].name = "menu" + tagNivel + "_" + k + "_valor";
                    inputs[2].name = "menu" + tagNivel + "_" + k + "_url";
                    inputs[3].name = "menu" + tagNivel + "_" + k + "_target";

                    if (currentMenu.rows[0].cells[1].getElementsByTagName("DIV")[0].getElementsByTagName("TABLE").length > 0)
                        renameSubMenus(tagNivel + "_" + k, currentMenu.rows[0].cells[1].getElementsByTagName("DIV")[0]);

                }
            }
        }

    </script>
</asp:Content>

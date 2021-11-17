<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReporteTramiteCP.aspx.cs" MasterPageFile="~/plantillas/SILPA.master" Title="Consulta Publica Tramites" MaintainScrollPositionOnPostback="true" ValidateRequest="false" Inherits="ReporteTramite_ReporteTramiteCP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
    <script src='<%= ResolveClientUrl("~/Resources/jquery/3.2.1/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/3.3.7/js/bootstrap.min.js") %>' type="text/javascript"></script>
    <asp:ScriptManager runat="server" ID="scriptManager1"></asp:ScriptManager>
    <script src="../js/ConsultaPublica.js"></script>

    <link href="../App_Themes/Content/jquery.orgchart.css" rel="stylesheet" />
    <link href="../App_Themes/skin/Xcillion/Css/font-awesome.min.css" rel="stylesheet" />
    <%--<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"/>--%>
    <script src="../js/jquery.orgchart.js"></script>

    <style type="text/css">
        .inner-addon {
            position: relative;
        }
            /* style glyph */
            .inner-addon .glyphicon {
                position: absolute;
                padding: 10px;
                pointer-events: none;
            }

        .right-addon .glyphicon {
            right: 0px;
        }

        .right-addon input {
            padding-right: 30px;
        }

        /*.row {*/
        /*margin-top: 40px;
            padding: 0 10px;
        }*/

        .clickable {
            cursor: pointer;
        }

        #panel, #flip {
            padding: 5px;
            text-align: center;
            background-color: #e5eecc;
            border: solid 1px #c3c3c3;
            position: relative;
        }

        #panel {
            position: relative;
            top: 0;
            width: 5px;
            height: 10%;
            padding: 20px;
            background-color: #333;
            color: #fff;
            box-shadow: inset 0 0 5px 5px #222;
        }

        .label-sm {
            font-size: 13px;
            color: rgba(49, 112, 143, 1);
        }

        .label-xsm {
            font-size: 12px;
            color: dimgray;
            /*line-height: 5px;*/
        }

        .lbl-sm {
            font-size: 12px;
            color: black;
        }

        .lbl-title {
            font-size: 14px;
            color: darkred;
            text-decoration: underline;
        }

        .lbl-xsm {
            font-size: 12px;
            /*line-height: 10px;*/
            color: dimgray;
        }

        .containerTab {
            width: 97%;
        }

        #exTab2 h3 {
            color: white;
            background-color: #f9fcfd; /*#c4dce4;*/
            padding: 5px 15px;
        }

        #exTab2 .nav-pills > li > a {
            border-radius: 4px 4px 0 0;
        }

        #exTab2 .tab-content {
            color: white;
            background-color: #337ab7; /*#c4dce4;*/
            padding: 5px 15px;
            border-radius: 4px 4px 0 0;
        }

        .navBar {
            height: 50px;
            padding-right: 80px;
        }

        #helpIcon {
            cursor: pointer;
            /*padding-top: 12px;*/
        }

        .spnClose {
            float: left;
            font-size: 21px;
            font-weight: 700;
            line-height: 1;
            color: #000;
            text-shadow: 0 1px 0 #fff;
            filter: alpha(opacity=20);
            opacity: .2;
        }

        .spnImagenLogo {
            float: right;
            font-size: 9px;
            line-height: 3;
            color: #000;
            text-shadow: 0 1px 0 #fff;
            /*opacity: .2;*/
            display: block;
        }

        .box {
            position: relative;
            height: 98%;
            width: 98%;
            border: solid 1px #8c8d8e;
            overflow: auto;
            overflow-y: scroll;
            background-color: #ffffff;
        }

        body.loading {
            overflow: hidden;
        }

            /* Anytime the body has the loading class, our
   modal element will be visible */
            body.loading .modal {
                display: block;
            }
    </style>
    <style>
        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: 50% 50% no-repeat rgb(249,249,249);
            opacity: .5;
            background-color: grey;
        }

        .containerLoad {
            position: fixed;
            left: 45%;
            top: 40%;
        }


        .tabbable-panel {
            border: 1px solid #eee;
            padding: 10px;
        }

        /* Default mode */
        .tabbable-line > .nav-tabs {
            border: none;
            margin: 0px;
        }

            .tabbable-line > .nav-tabs > li {
                margin-right: 2px;
            }

                .tabbable-line > .nav-tabs > li > a {
                    border: 0;
                    margin-right: 0;
                    color: #737373;
                }

                    .tabbable-line > .nav-tabs > li > a > i {
                        color: #a6a6a6;
                    }

                .tabbable-line > .nav-tabs > li.open, .tabbable-line > .nav-tabs > li:hover {
                    border-bottom: 4px solid #fbcdcf;
                }

                    .tabbable-line > .nav-tabs > li.open > a, .tabbable-line > .nav-tabs > li:hover > a {
                        border: 0;
                        background: none !important;
                        color: #333333;
                    }

                        .tabbable-line > .nav-tabs > li.open > a > i, .tabbable-line > .nav-tabs > li:hover > a > i {
                            color: #a6a6a6;
                        }

                    .tabbable-line > .nav-tabs > li.open .dropdown-menu, .tabbable-line > .nav-tabs > li:hover .dropdown-menu {
                        margin-top: 0px;
                    }

                .tabbable-line > .nav-tabs > li.active {
                    border-bottom: 4px solid #f3565d;
                    position: relative;
                }

                    .tabbable-line > .nav-tabs > li.active > a {
                        border: 0;
                        color: #333333;
                    }

                        .tabbable-line > .nav-tabs > li.active > a > i {
                            color: #404040;
                        }

        .tabbable-line > .tab-content {
            margin-top: -3px;
            background-color: #fff;
            border: 0;
            border-top: 1px solid #eee;
            padding: 15px 0;
        }

        .portlet .tabbable-line > .tab-content {
            padding-bottom: 0;
        }

        /* Below tabs mode */

        .tabbable-line.tabs-below > .nav-tabs > li {
            border-top: 4px solid transparent;
        }

            .tabbable-line.tabs-below > .nav-tabs > li > a {
                margin-top: 0;
            }

            .tabbable-line.tabs-below > .nav-tabs > li:hover {
                border-bottom: 0;
                border-top: 4px solid #fbcdcf;
            }

            .tabbable-line.tabs-below > .nav-tabs > li.active {
                margin-bottom: -2px;
                border-bottom: 0;
                border-top: 4px solid #f3565d;
            }

        .tabbable-line.tabs-below > .tab-content {
            margin-top: -10px;
            border-top: 0;
            border-bottom: 1px solid #eee;
            padding-bottom: 15px;
        }

        .highlight {
            font-weight: bold;
            color: green;
        }
    </style>

    <asp:UpdatePanel ID="upnlAccionesBoton" runat="server">
        <ContentTemplate>
            <div class="div-titulo">
                <%--<asp:Label ID="lblTituloPrincipal" runat="server" Text="CONSULTA DEL TRAMITE" SkinID="titulo_principal_blanco"></asp:Label>--%>
                <label id="lblTituloPrincipal" class="subtitulo-doble-linea"></label>
            </div>
            <div class="inner-addon right-addon">
                <div class="input-group input-group-lg">
                    <input type="text" class="form-control" id="textSearch" placeholder="BUSCAR" onkeypress="AddKeyPress(event)" />
                    <span class="input-group-btn">
                        <button title="buscar" class="btn btn-default btn-group-lg" type="button" onclick="return(botonEnviarParametroBusqueda())"><span class="glyphicon glyphicon-search"></span></button>
                        <button title="Ayuda" id="helpIcon" class="btn btn-default btn-group-lg" type="button" data-toggle="modal" data-target="#modalAyuda"><span class="glyphicon glyphicon-info-sign pull-right"></span></button>
                        <button title="Inicio" id="homeIcon" class="btn btn-default btn-group-lg" type="button" onclick="location.href ='../../ventanillasilpa/';"><span class="glyphicon glyphicon-home pull-right"></span></button>
                    </span>
                </div>
                <div class="tabbable-panel" id="divTipoBusqueda">
                    <div class="tabbable-line">
                        <ul id="ulOrigen" class="nav nav-tabs">
                            <li class="active" id="Todos">
                                <a href="#tab_default_1" data-toggle="tab" onclick="BotonCambio('Todos')">CONSULTE ESTADO SOLICITUD</a></li>
                            <li class="" id="Publicación">
                                <a href="#tab_default_2" data-toggle="tab" onclick="BotonCambio('Publicacion')">BUSCAR AUTOS O RESOLUCIONES</a>
                            </li>
                            <li id="liBusquedaEIA">
                                <a href="#tab_default_3" data-toggle="tab" onclick="BotonCambio('EIA')">BUSQUEDA EIA</a>
                            </li>
                            <li id="liBusquedaEspecifica" onclick="DireccionEspecifica()">
                                <a href="#tab_default_4" data-toggle="tab">BUSQUEDA AVANZADA</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="clickable">
                <br />
                <span class="spnClose" data-dismiss="modal" onclick="closeModal()">Mostrar / Ocultar Resultados:<label id="spBusqueda" class="label-sm"></label></span><br />
                <br />
            </div>
            <div class="alert alert-info" id="divError" style="display: none;">
                <strong>
                    <label id="lblError" class="label-sm"></label>
                </strong>
            </div>
            <div id="exTab2" class="containerTab" style="display: none">
                <ul id="ulTabs" class="nav nav-pills">
                    <li class="active" id="aInfGeneral">
                        <a href="#1" data-toggle="tab">INFORMACIÓN GENERAL</a>
                    </li>
                </ul>
                <div id="panelTabs" class="tab-content clearfix">
                    <div class="tab-pane active" id="1">
                        <h3>
                            <div>
                                <table id="tblInformacionGeneral">
                                    <tr>
                                        <td style='text-align: left; width: 20%'>
                                            <label class="label-sm">NUMERO DE TRAMITE: </label>
                                        </td>
                                        <td style='text-align: left; width: 70%'>
                                            <label class="label-sm" id="lblVitalSolicitud"></label>
                                            <label class="label-sm">::</label>
                                            <label class="label-sm" id="lblcodigo_expediente">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="label-sm">SOLICITANTE: </label>
                                        </td>
                                        <td>
                                            <label class="label-xsm" id="lblSolicitante"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="label-sm">AUTORIDAD AMBIENTAL:</label></td>
                                        <td>
                                            <label class="label-xsm" id="lblAutoridadAmbiental"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <label class="label-sm">NOMBRE EXPEDIENTE:</label><label class="label-xsm" id="lblnombre_expediente"></label></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <label class="label-sm">DESCRIPCION EXPEDIENTE:</label><label class="label-xsm" id="lbldescripcion_expediente"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="label-sm">SECTOR:</label></td>
                                        <td>
                                            <label class="label-xsm" id="lblSector"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="label-sm" id="lblTramite" style="display: none">TRAMITE:</label>
                                        </td>
                                        <td>
                                            <label class="label-xsm" id="lblTramiteValue" style="display: none"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="label-sm">UBICACION:</label>
                                        </td>
                                        <td>
                                            <label class="label-xsm" id="lblUbicacion"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <label class="label-sm">EXPEDIENTES RELACIONADOS</label>

                                            <label class="label-xsm" id="lblExpedientesRelacionados"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div id="chart-container" style="overflow-y: scroll;"></div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </h3>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="panel" id="modalResultados" style="display: none; position: fixed; zoom: 1; top: 30px; left: 10px; z-index: 3; width: 25%; height: 90%">
        <div class="modal-content">
            <div class="modal-header">
                <span class="close" data-dismiss="modal" onclick="closeModal()">X</span>
                <label class="lbl-title">RESULTADOS DE LA BUSQUEDA:</label>
                <div class="panel panel-danger" id="dvOpciones">
                </div>
            </div>
            <div class="modal-body" style="display: block; height: 80%; width: 98%;">
                <div id="divRespuestaResultados" class="box">
                </div>
            </div>
            <div class="modal-footer" id="divPaginador">
                <nav aria-label="Page navigation example">
                    <ul class="pagination justify-content-end" id="ulPaginador">
                    </ul>
                </nav>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalAyuda" role="dialog" data-keyboard="true" data-backdrop="static" style="width: 100%">
        <div class="modal-dialog" style="width: 90%">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="alert alert-success">
                        <span class="close" data-dismiss="modal">
                            <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"></span>
                            X
                        </span>
                        <span class="glyphicon glyphicon-question-sign" aria-hidden="true"></span>
                        <strong>DESCRIPCIÓN SOBRE LA NUEVA FUNCIONALIDAD DE BÚSQUEDA EN VITAL</strong>
                        <span class="sr-only">:</span>
                    </div>
                </div>
                <div class="modal-body" data-toggle="validator">
                    <div class="row" style="align-content: center; text-align: center;">
                        <img src="../App_Themes/Img/Ayuda/Ayuda1.png" />
                    </div>
                    <div class="row" style="align-content: center; text-align: justify;">
                        <br />
                        <label class="label-xsm" aria-hidden="true">El acceso al buscador se encuentra dispuesto en la página principal de VITAL en la url http://vital.anla.gov.co/ventanillasilpa, y desde allí se accede a la funcionalidad que permite a cualquier usuario realizar búsquedas sobre los tramites de las autoridades ambientales que se encuentran operando en VITAL y cuya gestión se ha ejecutado en esta plataforma.</label>
                        <label class="label-xsm" aria-hidden="true">Una vez se accede a la opción BUSCAR en pantalla aparece la siguiente interfaz:</label>
                    </div>
                    <div class="row" style="align-content: center; text-align: center;">
                        <img src="../App_Themes/Img/Ayuda/Ayuda2.png" />
                    </div>
                    <div class="row" style="align-content: center; text-align: justify;">
                        <br />
                        <label class="label-xsm" aria-hidden="true">Allí se dispone un campo que permite ingresar palabras clave como criterios de búsqueda, que serán aplicados sobre la información disponible para producir el resultado de la consulta.</label><br />
                        <label class="label-xsm" aria-hidden="true">
                            Los escenarios de la búsqueda se clasifican en:
                        </label>
                        <br />
                        <label class="label-sm" aria-hidden="true">Buscar Tramites. </label>
                        <label class="label-xsm" aria-hidden="true">
                            El cual permite consultar sobre expedientes o trámites gestionados por la Autoridad Ambiental en la plataforma y en su sistema de gestión si este está integrado en VITAL. Las palabras o frases ingresadas realizaran la búsqueda sobre los siguientes dominios:
                        </label>
                        <br />
                        <label class="label-xsm" aria-hidden="true">
                            Nombre proyecto / Número de Expediente /Numero Vital /Tipo de Tramite /Solicitante /Autoridad Ambiental /Ubicación /Fecha / Expedientes Asociados
                        </label>
                    </div>
                    <div class="row" style="align-content: center; text-align: center;">
                        <img src="../App_Themes/Img/Ayuda/Ayuda3.png" />
                    </div>
                    <div class="row" style="align-content: center; text-align: justify;">
                        <label class="label-sm" aria-hidden="true">Buscar Publicaciones. </label>
                        <br />
                        <label class="label-xsm" aria-hidden="true">El cual permite consultar sobre los actos administrativos que las Autoridades Ambientales integradas en VITAL, han generado y que están debidamente notificados. Las palabras o frases ingresadas realizaran la búsqueda sobre los siguientes dominios:</label><br />
                        <label class="label-xsm" aria-hidden="true">Nombre proyecto /Número de Expediente /Tipo de Documento /Numero de Documento /Tipo de Tramite/ Autoridad Ambiental /Sector /Fecha</label><br />
                    </div>
                    <div class="row" style="align-content: center; text-align: center;">
                        <img src="../App_Themes/Img/Ayuda/Ayuda4.png" />
                    </div>
                    <div class="row" style="align-content: center; text-align: justify;">
                        <label class="label-sm" aria-hidden="true">Palabra o frase contenida en el dominio. El usuario ingresa una palabra o frase con la cual el sistema buscará coincidencias en los campos mencionados de acuerdo con la opción de búsqueda seleccionada.</label><br />
                        <label class="label-xsm" aria-hidden="true">Ejemplo: si el usuario escribe la palabra Guajira, en BUSCAR TRAMITES, el sistema buscará esta palabra al interior de los campos (Nombre proyecto / Número de Expediente /Numero Vital /Tipo de Tramite /Solicitante /Autoridad Ambiental /Ubicación /Fecha / Expedientes asociados) y mostrará como resultado los expedientes o solicitudes correspondientes que contienen en cualquier campo esta palabra</label><br />
                    </div>
                    <div class="row" style="align-content: center; text-align: center;">
                        <img src="../App_Themes/Img/Ayuda/Ayuda5.png" />
                    </div>
                    <div class="row" style="align-content: center; text-align: justify;">
                        <label class="label-sm" aria-hidden="true">Palabra o frase contenida exactamente en el dominio. El usuario ingresa una palabra o frase encerrada entre comillas “”, con lo cual el sistema buscará exactamente el valor ingresado en los campos.</label><br />
                        <label class="label-xsm" aria-hidden="true">Ejemplo: Si el usuario escribe la palabra</label><label class="label-success" aria-hidden="true"> "LAV0099-00-2015" </label>
                        <label class="label-xsm" aria-hidden="true">(encerrada entre comillas), en BUSCAR TRAMITES, el sistema buscará esta palabra exactamente al interior de los campos (Nombre proyecto / Número de Expediente /Numero Vital /Tipo de Tramite /Solicitante /Autoridad Ambiental /Ubicación /Fecha / Expedientes asociados) y mostrará como resultado los expedientes o solicitudes correspondientes que contienen en cualquier campo esta única palabra exactamente.</label><br />
                    </div>
                    <div class="row" style="align-content: center; text-align: center;">
                        <img src="../App_Themes/Img/Ayuda/Ayuda6.png" />
                    </div>
                    <div class="row" style="align-content: center; text-align: justify;">
                        <label class="label-sm" aria-hidden="true">En la gráfica se muestra el resultado de la búsqueda de </label>
                        <label class="label-success" aria-hidden="true">LAV0099-00-2015 </label>
                        <label class="label-xsm" aria-hidden="true">donde el sistema encontró un expediente o tramite, donde en el campo número de trámite, este dato se encuentra exactamente.</label><br />
                        <label class="label-sm" aria-hidden="true">Búsqueda a través de conector</label>
                        <label class="label-success" aria-hidden="true">+</label><label class="label-sm" aria-hidden="true">.Este conector (+) permite ingresar varios palabras o frase de búsqueda.</label>
                    </div>
                    <div class="row" style="align-content: center; text-align: center;">
                        <img src="../App_Themes/Img/Ayuda/Ayuda7.png" /><br />
                    </div>
                    <div class="row" style="align-content: center; text-align: justify;">
                        <label class="label-xsm" aria-hidden="true">Ejemplo: si ingresamos como criterios de búsqueda </label>
                        <label class="label-success" aria-hidden="true">anla+vertimientos</label><label class="label-xsm" aria-hidden="true">
                            , el sistema consultará en los campos pertenecientes al dominio de campos (Nombre proyecto / Número de Expediente /Numero Vital /Tipo de Tramite /Solicitante /Autoridad Ambiental /Ubicación /Fecha / Expedientes asociados) coincidencias con la palabra ANLA y dentro de ese conjunto de registros encontrados, coincidencias con la palabra vertimientos</label>
                    </div>
                    <div class="row" style="align-content: center; text-align: center;">
                        <img src="../App_Themes/Img/Ayuda/Ayuda8.png" /><br />
                    </div>
                    <div class="row" style="align-content: center; text-align: justify;">
                        <label class="label-xsm" aria-hidden="true">Combinación de tipos de búsqueda. El sistema permite combinar a través del conector + consultas exactas o por coincidencias.</label>
                    </div>
                    <div class="row" style="align-content: center; text-align: center;">
                        <img src="../App_Themes/Img/Ayuda/Ayuda9.png" /><br />
                    </div>
                    <div class="row" style="align-content: center; text-align: justify;">
                        <label class="label-xsm" aria-hidden="true">Ejemplo: la búsqueda</label>
                        <label class="label-success" aria-hidden="true">“anla”+Registro Rua Emisiones</label><label class="label-xsm" aria-hidden="true"> ,el sistema consultará un campo del dominio (Nombre proyecto / Número de Expediente /Numero Vital /Tipo de Tramite /Solicitante /Autoridad Ambiental /Ubicación /Fecha / Expedientes asociados), que contenga exactamente la palabra</label>
                        <label class="label-success" aria-hidden="true">“anla”</label><label class="label-xsm" aria-hidden="true">y dentro de ese conjunto de registros encontrados uno que contenga coincidencia con la frase</label><label class="label-success" aria-hidden="true">Registro Rua Emisiones.</label>
                    </div>
                    <div class="row" style="align-content: center; text-align: center;">
                        <img src="../App_Themes/Img/Ayuda/Ayuda10.png" /><br />
                    </div>
                    <div class="row" style="align-content: center; text-align: justify;">
                        <label class="label-xsm" aria-hidden="true">Los tipos de consulta mencionados aplica igualmente para el escenario de BUSCAR PUBLICACIONES con el dominio de búsquedas sobre los campos Nombre proyecto /Número de Expediente /Tipo de Documento /Numero de Documento /Tipo de Tramite / Autoridad Ambiental /Sector /Fecha. Como recomendación la búsqueda de los actos administrativos se debe realizar por su número y siempre encerrado ente comillas.</label><br />
                        <label class="label-xsm" aria-hidden="true">El máximo número de combinaciones a través del conector es 4, es decir solo hasta tres conectores</label><label class="label-success" aria-hidden="true">(+)</label>
                        <label class="label-xsm" aria-hidden="true">; La palabra mínima que el motor reconoce para búsqueda es de 3 caracteres</label>
                        <br />
                        <br />
                        <br />
                        <label class="label-sm" aria-hidden="true">Resultados de Búsqueda.</label><label class="label-xsm" aria-hidden="true">La consulta siempre arrojará registros los cuales se ubican a un lado de la pantalla para facilitar su visualización; con la opción de
                        </label>
                        <label class="label-success" aria-hidden="true">Mostrar / Ocultar Resultados</label><label class="label-xsm" aria-hidden="true">se manipula este componente de agrupación de resultados.</label>
                    </div>
                    <div class="row" style="align-content: center; text-align: center;">
                        <img src="../App_Themes/Img/Ayuda/Ayuda11.png" /><br />
                    </div>
                    <div class="row" style="align-content: center; text-align: justify;">
                        <label class="label-xsm" aria-hidden="true">Si el resultado arroja más de 6 registros estos serán paginados y el usuario podrá navegar a través de esta opción.</label><br />
                        <label class="label-xsm" aria-hidden="true">Al acceder a un registro el sistema presenta la información general del expediente o solicitud en vital y tantas pestañas como estados tenga el tramite </label>
                    </div>
                    <div class="row" style="align-content: center; text-align: center;">
                        <img src="../App_Themes/Img/Ayuda/Ayuda12.png" /><br />
                    </div>
                    <div class="row" style="align-content: center; text-align: justify;">
                        <label class="label-xsm" aria-hidden="true">Al interior de cada pestaña se encontrarán los documentos pertenecientes a esa etapa y que para el caso de los actos administrativos estén debidamente notificados.</label><br />
                    </div>
                    <div class="row" style="align-content: center; text-align: center;">
                        <img src="../App_Themes/Img/Ayuda/Ayuda13.png" /><br />
                    </div>
                    <div class="row" style="align-content: center; text-align: justify;">
                        <label class="label-xsm" aria-hidden="true">Los documentos presentados corresponderán a : actos administrativos notificados, oficios de respuestas, oficios al interior del trámite y correspondencia del usuario; para los primeros tipos excepto la correspondencia del usuario,  el sistema validará que el formato del documento sea PDF,  en caso contrario no mostrará este registro para lo cual el usuario deberá dirigirse a la entidad para buscar el documento de interés.</label><br />
                    </div>
                    <div class="row" style="align-content: center; text-align: justify;">
                        <label class="label-sm" aria-hidden="true">Inclusión de fechas en la búsqueda.</label><label class="label-xsm" aria-hidden="true">Para incluir fechas en las búsquedas estas debe ingresarse con el formato DD MMM AAAA. La consulta arroja como resultado los tramites que tienen como fecha ude ultima actividad el criterio ingresado; ejemplo: al consultar 12 febrero 2018 el buscador genera como resultado aquellos expedientes creados en esta fecha o donde la última actividad creada de cualquier expediente haya sido el 02 febrero 2018.</label><br />
                    </div>
                    <div class="row" style="align-content: center; text-align: center;">
                        <img src="../App_Themes/Img/Ayuda/Ayuda14.png" /><br />
                    </div>
                    <div class="row" style="align-content: center; text-align: center;">
                        <img src="../App_Themes/Img/Ayuda/Ayuda15.png" /><br />
                    </div>
                    <div class="row" style="align-content: center; text-align: justify;">
                        <label class="label-xsm" aria-hidden="true">Esta búsqueda puede combinar otros criterios como por ejemplo</label><label class="label-success" aria-hidden="true"> 12 febrero 2018+anla o febrero 2018+anla o 2018+anla.</label><br />
                        <br />
                    </div>
                    <div class="row" style="align-content: center; text-align: justify;">
                        <label class="label-sm" aria-hidden="true">Búsqueda por campos.</label><label class="label-xsm" aria-hidden="true"> Esta búsqueda se refiere a la consulta habilitada como alternativa actual y que conserva el formato de búsqueda habitual en VITAL. los resultados están limitados solo a los tramites gestionados por VITAL a diferencia de BUSCAR TRAMITES, donde los resultados contienen registros de VITAL y del sistema de gestión de la Autoridad Ambiental.</label><br />
                        <br />
                    </div>
                    <div class="row" style="align-content: center; text-align: center;">
                        <img src="../App_Themes/Img/Ayuda/Ayuda16.png" /><br />
                    </div>
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>

    <div class="loader" style="display: none" id="divCargando">
        <!-- Place at bottom of page -->
        <div class="containerLoad">
            <p>
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/App_Themes/Img/Procesando.gif" />
            </p>
            <label class='label-sm'>Procesando su solictud.</label>
        </div>
    </div>

    <div class="modal fade" id="modalDocumentos" role="dialog" data-keyboard="true" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="alert alert-success">
                        <span class="close" data-dismiss="modal">X</span>
                        <strong>LISTA DE DOCUMENTOS PUBLICADOS:</strong>
                    </div>
                </div>
                <div class="modal-body" data-toggle="validator">
                    <div class="form-inline">
                        <div id="divDinamicoDocumentos" class="form-group form-group-sm"></div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="alert alert-danger" style="display: none" id="divErrorDocumentos">
                        <strong>
                            <label class="control-label-sm" id="lblMensaje"></label>
                        </strong>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="ModalTramite" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">DETALLE DE LA ACTIVIDAD</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-inline">
                        <div id="divDinamicoDetalleaActividad"></div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

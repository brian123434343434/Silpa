<%@ Page Title="" Language="C#" MasterPageFile="~/plantillas/SILPABuscador.master" AutoEventWireup="true" CodeFile="ReporteTramiteCPDetalle.aspx.cs" Inherits="ReporteTramite_ReporteTramiteCPDetalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet" type="text/css" />
     <link href="../Resources/Bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />

    <!-- CSS files (include only one of the two files!) -->
    <!-- If you are not using bootstrap: -->
    <link rel="stylesheet" type="text/css" href="../Resources/datatable-master/css/datatable.min.css" media="screen">
    <!-- If you are using bootstrap: -->
    <link rel="stylesheet" type="text/css" href="../Resources/datatable-master/css/datatable-bootstrap.min.css" media="screen">

    <link rel="stylesheet" type="text/css" href="../Resources/Buscador/css/buscadorVITAL.css" />
    <link rel="stylesheet" type="text/css" href="../Resources/datatable-master/css/paginadorVITAL.css" />
    <link rel="stylesheet" type="text/css" href="../Resources/TimeLine/css/jquery.roadmap.min.css" />
    <link rel="stylesheet" type="text/css" href="../Resources/TimeLine/css/timeLineVITAL.css" />


    <script src='<%= ResolveClientUrl("~/Resources/jquery/1.11.2/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/jquery/fontsize/js/jquery.jfontsize-1.0.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Bootstrap/5.0.1/js/bootstrap.min.js") %>' type="text/javascript"></script>
    <!-- JS files -->
    <script src='<%= ResolveClientUrl("~/Resources/datatable-master/js/datatable.min.js") %>' type="text/javascript"></script>                    
    <!-- Add the following if you want to use the jQuery wrapper (you still need datatable.min.js): -->
    <script src='<%= ResolveClientUrl("~/Resources/datatable-master/js/datatable.jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/TimeLine/js/jquery.roadmap.js") %>' type="text/javascript"></script>
    <script src='<%= ResolveClientUrl("~/Resources/Buscador/js/DetalleConsultaPublica.js") %>' type="text/javascript"></script>


    

    <%--<asp:ScriptManager runat="server" ID="scriptManager1"></asp:ScriptManager>--%>
    <div class="titulo_pagina">
        <asp:Literal id="lblMenjase" runat="server" Visible="false"></asp:Literal>
    </div>
    <div id="InfoDetalle" runat="server" clientidmode="Static" style="display: none;">
        <div id="exTab2" class="containerTab">
                <ul id="ulTabs" class="nav nav-tabs" role="tablist">
                    <li class="nav-item" id="aEstadoTramite">
                        <a class="nav-link active" id="nav-EstadoTramite-tab" data-bs-toggle="tab" data-bs-target="#EstadoTramite" aria-controls="nav-EstadoTramite" role="tab" aria-selected="true" style="/* font-size: 13px; */">¿En qúe va mi tramite?</a>
                    </li>
                    <li class="nav-item" id="aInfGeneral">
                        <a class="nav-link" id="nav-InfoGeneral-tab" data-bs-toggle="tab" data-bs-target="#InfoGeneral" aria-controls="nav-InfoGeneral" role="tab" aria-selected="false">Información general</a>
                    </li>
                </ul>
                <div id="panelTabs" class="tab-content clearfix">
                    <div class="tab-pane active" id="EstadoTramite" role="tabpanel" aria-labelledby="nav-EstadoTramite-tab">
                        <div id="timeline"></div>
                    </div>
                    <div class="tab-pane" id="InfoGeneral" role="tabpanel" aria-labelledby="nav-InfoGeneral-tab">
                        <div class="row">
                            
                            <div id="imgAutoridad" class="col-2">
                                
                            </div>
                            <div class="col-10">
                                <div class="form-group row">
                                    <label class="col-sm-2 label-sm label-etiqueta">Número de trámite:</label>
                                    <div class="col-sm-10 label-detalle" >
                                        <label class="label-sm" id="lblVitalSolicitud"></label>
                                        <label class="label-sm">::</label>
                                        <label class="label-sm" id="lblcodigo_expediente"></label>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2 label-sm label-etiqueta" id="lblTramite" style="display: none">Tipo de trámite:</label>
                                    <div class="col-sm-10 label-detalle">
                                        <label class="label-sm" id="lblTramiteValue" style="display: none"></label>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2 label-sm label-etiqueta">Autoridad ambiental:</label>
                                    <div class="col-sm-10 label-detalle">
                                        <label class="label-sm" id="lblAutoridadAmbiental"></label>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2 label-sm label-etiqueta">Nombre del proyecto:</label>
                                    <div class="col-sm-10 label-detalle">
                                        <label class="label-sm" id="lblnombre_expediente"></label>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2 label-sm label-etiqueta">Ubicación:</label>
                                    <div class="col-sm-10 label-detalle">
                                        <label class="label-sm" id="lblUbicacion"></label>
                                    </div>
                                </div>
                                <div class="form-group row">
                                    <label class="col-sm-2 label-sm label-etiqueta">Desde:</label>
                                    <div class="col-sm-10 label-detalle">
                                        <label class="label-sm" id="lblDesde"></label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
     </div>
    <div class="alert alert-info" id="divError" style="display: none;">
        <strong>
            <label id="lblError" class="label-sm"></label>
        </strong>
    </div>
    <div style="display: none" id="divCargando">
        <div id="container-loader" class="container-loader-buscadorVITAL"></div>
        <div id="loader" class="loader-buscadorVITAL"></div>
    </div>
</asp:Content>


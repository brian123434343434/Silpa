using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Sancionatorio;
using SILPA.AccesoDatos.Generico;
using SILPA.Comun;
using System.Data;
using System.Collections;
using SILPA.AccesoDatos.Notificacion;
using SILPA.AccesoDatos.AdmTablasBasicas;
using SILPA.AccesoDatos.Salvoconducto;

namespace SILPA.LogicaNegocio.Generico
{
    /// <summary>
    /// Clase que permite listar los objetos del negocio
    /// </summary>
    public class Listas
    {
        /// <summary>
        /// Clase que entrega las listas de los objetos
        /// </summary>
        public Listas()
        {
            objConfiguracion = new Configuracion();
        }

        /// <summary>
        /// Objeto de configuración del sitio, con las  variables globales
        /// </summary>
        private Configuracion objConfiguracion;
        /// <summary>
        /// Método que lista las autoridadedes ambientales
        /// </summary>
        /// <param name="intAutoridadId">int: identificador de la autoridad ambiental</param>
        /// <returns>
        /// DataSet -> Campos: 
        /// [AUT_ID - AUT_NOMBRE - AUT_DIRECCION - AUT_TELEFONO - AUT_FAX - 
        /// AUT_CORREO -  AUT_NIT - AUT_CARGUE - APLICA_RADICACION - AUT_ACTIVO]
        /// </returns>
        public DataSet ListarAutoridades(Nullable<int> intAutoridadId)
        {
            AutoridadAmbientalDalc obj = new AutoridadAmbientalDalc();
            return obj.ListarAutoridadAmbiental(intAutoridadId);
        }

        /// <summary>
        /// Obtiene las Autoridades Ambientales autorizadas para el proceso de salvoconducto
        /// </summary>
        /// <param name="intAutoridadId"></param>
        /// <returns></returns>
        public DataSet ListarAutoridadesSUNL(Nullable<int> intAutoridadId)
        {
            AutoridadAmbientalDalc obj = new AutoridadAmbientalDalc();
            return obj.ListarAutoridadAmbientalSUNL(intAutoridadId);
        }

        public DataSet ListarAutoridadAmbientalRegistroMinero(Nullable<int> intAutoridadId)
        {
            AutoridadAmbientalDalc obj = new AutoridadAmbientalDalc();
            return obj.ListarAutoridadAmbientalRegistroMinero(intAutoridadId);
        }

        /// <summary>
        /// Metodo que lista las autoridades ambientales actiivas
        /// </summary>
        /// <returns></returns>
        public DataSet ListarAutoridadesActivas()
        {
            AutoridadAmbientalDalc obj = new AutoridadAmbientalDalc();
            return obj.ListarAutoridadAmbientalActiva();
        }

        /// <summary>
        /// Metodo que lista los tipos De Salvoconductos
        /// </summary>
        /// <returns></returns>
        public DataSet ListarTipoSalvoconducto()
        {
            SalvoconductoDalc obj = new SalvoconductoDalc();
            return obj.ListarTipoSalvoconducto();
        }

        /// <summary>
        /// Metodo que lista los tipos de documentos de acreditación
        /// </summary>
        /// <returns></returns>
        public DataSet ListaAcreditacion()
        {
            AcreditacionDalc obj = new AcreditacionDalc();
            return obj.ListarTipoDocumentoAcreditacion();
        }

        /// <summary>
        /// Método que lista los países
        /// </summary>
        /// <param name="intId"></param>
        /// <returns>
        /// DataSet -> Campos: [PAI_ID - PAI_NOMBRE - PAI_ACTIVO - PAI_CODIGO_INTL]
        /// </returns>
        public DataSet ListarPaises(Nullable<int> intId)
        {
            PaisDalc obj = new PaisDalc();
            return obj.ListarPaises(intId);
            //return null;
        }

        /// <summary>
        /// Método que lista los departamentos sie l pasís es Colombia
        /// </summary>
        /// <param name="intPais">
        /// int: Identificador del país de donde se quieren obtener 
        /// la división politica
        /// </param>
        /// <returns></returns>
        /// <returns>DataSet ->  Campos: [DEP_ID (identificador ) - DEP_NOMBRE (Nombre)-  REG_ID  ( identificador de la region )]);
        public DataSet ListarDepartamentos(Nullable<int> intPais)
        {
            if (intPais == objConfiguracion.IdPaisPredeterminado)
            {
                DepartamentoDalc obj = new DepartamentoDalc();

                /// Obtiene el listado completo de los deptos
                return obj.ListarDepartamentos(null, null);
            }
            else
            {
                return null;
            }
        }
        public DataSet ListarDepartamentosPorAutoridadAmbiental(Nullable<int> intPais, int intAutID)
        {
            if (intPais == objConfiguracion.IdPaisPredeterminado)
            {
                DepartamentoDalc obj = new DepartamentoDalc();

                /// Obtiene el listado completo de los deptos
                return obj.ListarDepartamentosPorAutoridadAmbiental(null, null, intAutID);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Lista los municipios
        /// </summary>
        /// <param name="intId">identificador del Municipio especifico</param>
        /// <param name="intDepto">Identifdicador del Departamento al que pertenece el municipio</param>
        /// <param name="intRegionalId">Identificador de la Regional al que pertenece el municipio</param>
        /// <returns>
        /// DataSet ->  Campos: [MUN_ID - MUN_VALOR_TIQUETE - DEP_ID -MUN_NOMBRE - RGN_ID (identificador de la region ) - UBI_ID
        /// </returns>
        public DataSet ListaMunicipios(Nullable<int> intId, Nullable<int> intDepto, Nullable<int> intRegionalId)
        {
            MunicipioDalc obj = new MunicipioDalc();
            /// Obtiene el listado completo de los municipios
            return obj.ListarMunicipios(intId, intDepto, intRegionalId);
        }

        /// <summary>
        /// Lista las veredas
        /// </summary>
        /// <param name="intMunicipioId">int: Identificador del municipio</param>
        /// <param name="intCorregimientoId">int: identificador del corregimiento</param>
        /// <param name="intId">int: identificador de la vereda</param>
        /// <returns>DataSet -> Campos: [VER_ID - VER_NOMBRE - MUN_ID (Identificador del municipio) - COR_ID]
        /// </returns>
        public DataSet ListarVeredas(Nullable<int> intMunicipioId, Nullable<int> intCorregimientoId, Nullable<int> intId)
        {
            VeredaDalc obj = new VeredaDalc();
            return obj.ListarVeredas(intMunicipioId, intCorregimientoId, intId);
        }

        /// <summary>
        /// Método que lista las Nomenclaturas Asociadas a una direccion
        /// </summary>
        /// <returns>
        /// DataSet -> Campos: 
        /// [NOM_ID - NOM_NOMBRE - NOM_ACTIVA]
        /// </returns>
        public DataSet ListarNomenclaturas()
        {
            NomenclaturaDalc obj = new NomenclaturaDalc();
            return obj.ListarSector();
        }

        /// <summary>
        /// Método que lista las Complementos Asociados a una direccion
        /// </summary>
        /// <returns>
        /// DataSet -> Campos: 
        /// [NOM_ID - NOM_NOMBRE - NOM_ACTIVA]
        /// </returns>
        public DataSet ListasComplementoDireccion()
        {
            ComplementoDireccionDalc obj = new ComplementoDireccionDalc();
            return obj.ListasComplementoDireccion();
        }
        
        /// <summary>
        /// Lista los tipos de persona
        /// </summary>
        /// <param name="intTipopersona">Valor del identificador del tipo de persona, null lista todos los tipo de identificador</param>
        /// <returns>DataSet -> Campos: [ TPE_ID - TPE_NOMBRE ]</returns>
        public DataSet ListarTipoPersona(Nullable<int> intTipopersona)
        {
            TipoPersonaDalc obj = new TipoPersonaDalc();
            return obj.ListarTipoPersona(intTipopersona);
        }

        /// <summary>
        /// Lista los tipos de persona
        /// </summary>
        /// <param name="intTipopersona">Valor del identificador del tipo de persona, null lista todos los tipo de identificador</param>
        /// <param name="intTipoSolicitante">Valor del tipo de solicitante, null lista todos los tipo de identificador</param>
        /// <returns>DataSet -> Campos: [ TPE_ID - TPE_NOMBRE ]</returns>
        public DataSet ListarTipoPersona(Nullable<int> intTipopersona, string formulario)
        {
            TipoPersonaDalc obj = new TipoPersonaDalc();
            return obj.ListarTipoPersona(intTipopersona, formulario);
        }

        /// <summary>
        /// Lista los corregimientos
        /// </summary>
        /// <param name="intMunicipioId">int: identificador del municipio</param>
        /// <param name="intId">int: identificador específico del corregimiento</param>
        /// <returns>
        /// DataSet -> Campos: [ COR_ID -  COR_NOMBRE - MUN_ID]
        /// </returns>
        public DataSet ListarCorregimientos(Nullable<int> intMunicipioId, Nullable<int> intId)
        {
            CorregimientoDalc obj = new CorregimientoDalc();
            return obj.ListarCorregimiento(intMunicipioId, intId);
        }

        /// <summary>
        /// Lista las Areas Hidrograficas..
        /// </summary>
        /// <param name="IntIdAreaHidro"></param>
        /// <returns>DataSet -> Campos [ AHI_ID (identificador), AHI_NOMBRE(nombre activo), AHI_ACTIVO ] </returns>
        public DataSet ListarAreaHidrografica(Nullable<int> IntIdAreaHidro)
        {
            AreaHidrograficaDalc obj = new AreaHidrograficaDalc();
            return obj.ListarAreaHidrografica(IntIdAreaHidro);

        }

        /// <summary>
        /// Lista las Areas Hidrograficas relacionadas a una zona hidrografica especifica.
        /// </summary>
        /// <param name="Id" >Con este valor se lista el Area Hidrografica con el identificador, si es null se listan todas</param>
        /// <returns>DataSet con los registros y las siguientes columnas: AHI_ID, AHI_NOMBRE</returns>
        public DataSet ListarAreaHidrograficaZona(int IntIdZonaHidro)
        {
            AreaHidrograficaDalc obj = new AreaHidrograficaDalc();
            return obj.ListarAreaHidrograficaZona(IntIdZonaHidro);

        }

        /// <summary>
        /// Lista las zonas hidrograficas
        /// </summary>
        /// <param name="intId">identificador de la zona hidrografica específica</param>
        /// <param name="intAreaHidroId">identificador del area hidrografica </param>
        /// <returns>DataSet:  campos ZHI_ID - AHI_ID -  ZHI_NOMBRE </returns>
        public DataSet ListarZonaHidrografica(Nullable<int> intId, Nullable<int> intAreaHidroId)
        {
            ZonaHidrograficaDalc obj = new ZonaHidrograficaDalc();
            return obj.ListarZonaHidrografica(intAreaHidroId, intId);
        }

        /// <summary>
        /// Lista las zona hidrograficas relacionadas a una subzona.
        /// </summary>
        /// <param name="intZonaHidroId" >Valor del identificador de la zona hidrografica por el cual se filtraran los municipios, si es null no existen restricciones</param>
        /// <returns>DataSet con los registros y las siguientes columnas:  ZHI_ID, ZHI_NOMBRE</returns>
        public DataSet ListarZonaHidrograficaSubZona(int intZonaHidroId)
        {
            ZonaHidrograficaDalc obj = new ZonaHidrograficaDalc();
            return obj.ListarZonaHidrograficaSubZona(intZonaHidroId);
        }

        /// <summary>
        /// Lista las Sub Zonas HidroLogicas
        /// </summary>
        /// <param name="intId">indentificador de la sub zona específica</param>
        /// <param name="intZonaHidroId">Identificador de la zona hidrologica</param>
        /// <returns>
        /// DataSet -> Campos [SHI_ID - SHI_NOMBRE - ZHI_ID]
        /// </returns>
        public DataSet ListarSubZonaHidrografica(Nullable<int> intId, Nullable<int> intZonaHidroId)
        {
            SubZonaHidrologicaDalc obj = new SubZonaHidrologicaDalc();
            return obj.ListarSubZonaHidrografica(intZonaHidroId, intId);
        }

        /// <summary>
        /// Lista los Sectores y/o tipos de proyectos
        /// </summary>
        /// <param name="intIdTipoProyecto">Indentificador del Sector y/o tipo de proyecto. Con -1 lista todos los sectores</param>
        /// <param name="intIdSector">Identificador del sector para que los tipos de proyectos sean filtrados</param>
        /// <returns>
        /// DataSet -> Campos [SEC_ID - SEC_NOMBRE ]
        /// </returns>
        public DataSet ListarSectorTipoProyecto(Nullable<int> intIdTipoProyecto, Nullable<int> intIdSector)
        {
            SectorDalc obj = new SectorDalc();
            return obj.ListarSector(intIdTipoProyecto, intIdSector);
        }

        /// <summary>
        /// Retorna el listado de los documentos
        /// </summary>
        /// <returns>DataSet -> TID_ID, TID_NOMBRE, TID_ACTIVO, TID_SIGLA</returns>
        public DataSet ListaTipoIdentificacion()
        {
            AccesoDatos.Generico.TipoIdentificacionDalc _tipoIdentificacion = new AccesoDatos.Generico.TipoIdentificacionDalc();
            return _tipoIdentificacion.ListarTipoIdentificacionPorID(null);
            /*ArrayList list = new ArrayList(Enum.GetNames(typeof(TipoIdentificacionIdentity)));
            return list;*/
            //return Enum.GetNames(typeof(TipoIdentificacion));
        }

        /// <summary>
        /// Retorna el listado de los documentos por tipo persona
        /// </summary>
        /// <returns>DataSet -> PERID, TID_ID, TID_NOMBRE, TID_ACTIVO</returns>
        public DataSet ListaTipoIdentificacionXTipoPersona()
        {
            AccesoDatos.Generico.TipoIdentificacionDalc _tipoIdentificacion = new AccesoDatos.Generico.TipoIdentificacionDalc();
            return _tipoIdentificacion.ListarTipoIdentificacionXTipoPersona();
        }

        /// <summary>
        /// Lista los tipo persona existentes
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet ListaTipoPersona()
        {
            return null;
        }

        /// <summary>
        /// Lista los tipos de dirección
        /// </summary>
        /// <returns>DataSet->Campos: [  - ]</returns>
        public DataSet ListarTipoDireccion()
        {


            return null;
        }

        /// <summary>
        /// Lista los tipos de tramite
        /// </summary>
        /// <param name="intIdTipoTramite">Identificador del tipo de traminte</param>
        /// <returns>DataSet -> Campos [ID_TRAMITE - NOMBRE_TRAMITE - ACTIVO_TRAMITE ]</returns>
        public DataSet ListarTipoTramite(Nullable<int> intIdTipoTramite)
        {
            TipoTramiteDalc obj = new TipoTramiteDalc();
            return obj.ListarTipoTramite(intIdTipoTramite, null);
        }

        public DataSet ListarTipoTramiteVisible()
        {
            TipoTramiteDalc obj = new TipoTramiteDalc();
            return obj.ListarTipoTramiteVisible();
        }
        public DataSet ListarUsuariosPorTramite(int tramiteId)
        {
            TipoTramiteDalc obj = new TipoTramiteDalc();
            return obj.ListarUsuariosPorTramite(tramiteId);
        }
        public DataSet ListarNumeroVitalUsuario(int UsuarioId)
        {
            ExpedienteDalc obj = new ExpedienteDalc();
            return obj.ListaNumeroVitalPorUsuario(UsuarioId);
        }

        public DataSet ListaNumeroVitalPorUsuarioyAA(int UsuarioId, int AAID)
        {
            ExpedienteDalc obj = new ExpedienteDalc();
            return obj.ListaNumeroVitalPorUsuarioyAA(UsuarioId, AAID);
        }
        public DataSet ListaNumeroExpedientePorNumeroVITAL(string numeroVITAL)
        {
            ExpedienteDalc obj = new ExpedienteDalc();
            return obj.ListaNumeroExpedientePorNumeroVITAL(numeroVITAL);
        }

        public DataSet ListaNumeroExpedientePorUsuario(string idApplicationUser)
        {
            ExpedienteDalc obj = new ExpedienteDalc();
            return obj.ListaNumeroExpedientePorUsuario(idApplicationUser);
        }


        /// <summary>
        /// Lista los tipos de acto administrativo
        /// </summary>
        /// <param name="intTipoActoAdministrativo">indentificador de la tipo de acto administrativo</param>
        /// <returns>
        /// DataSet -> Campos [TAAD_ID - TAAD_NOMBRE]
        /// </returns>
        // public DataSet ListarTipoActoAdministrativo(Nullable<int> intTipoActoAdministrativo)
        // {
        //     TipoActoAdministrativoDalc obj = new TipoActoAdministrativoDalc();
        //     return obj.ListarTipoActoAdministrativo(intTipoActoAdministrativo);
        //}


        /// <summary>
        /// Lista todos los tipo de ubicación en la BD o uno en particular.
        /// </summary>
        /// <param name="intId" >Con este valor se lista los tipos de ubicación con el identificador, si es null no existen restricciones</param>
        /// <returns>DataSet con los registros y las siguientes columnas:  UBI_ID, UBI_NOMBRE</returns>
        public DataSet ListarTipoUbicacion(Nullable<int> intId)
        {
            TipoUbicacionDalc obj = new TipoUbicacionDalc();
            return obj.ListarTipoUbicacion(intId);
        }

        /// <summary>
        /// Método que retorna un listado de los recursos sancionatorios
        /// </summary>
        /// <param name="_idRecurso">Identificador del recurso sancionatorio</param>
        /// <returns>Conjunto de Datos: [REC_ID_RECURSO] - [REC_NOMBRE]</returns>
        public DataSet ListarRecursosSancionatorio(Nullable<int> _idRecurso)
        {
            RecursoDalc obj = new RecursoDalc();
            return obj.ListarRecursos(_idRecurso);
        }

        public DataSet ListarSalvoconducto(int _idSalvoconducto, string _numSalvoconducto)
        {
            SILPA.AccesoDatos.Salvoconducto.SalvoconductoDalc obj = new SILPA.AccesoDatos.Salvoconducto.SalvoconductoDalc();


            DataSet ds_salvo = obj.ListarSalvoconducto(_idSalvoconducto, _numSalvoconducto);

            return ds_salvo;
        }

        /// <summary>
        /// Lista la configuracion del formulario por tipo de tramite para el tipo de persona en la BD.
        /// </summary>
        /// <param name="intFormulario" >Valor del identificador del formulario por el cual se filtraran.
        /// <param name="intProceso" >Valor del identificador del proceso por el cual se filtraran.
        /// si es null no existen restricciones.</param>
        /// <returns>DataSet con los registros y las siguientes columnas:  FPE_ID, FPE_NOMBRE, PTR_ID</returns>
        public DataSet ListarFormularioPersona(Nullable<int> intFormulario, Nullable<int> intProceso, string formulario)
        {
            FormularioPersonaDalc obj = new FormularioPersonaDalc();
            return obj.ListarFormularioPersona(intFormulario, intProceso, formulario);
        }

        public List<NotificacionEntity> ListarActosNotificacion(string idUsuario
            //, string expediente, string numeroSILPA, string fechaInicio, string Fechafin 
            )
        {
            PersonaDalc personaDalc = new PersonaDalc();
            PersonaIdentity persona = new PersonaIdentity();
            persona.IdApplicationUser = Convert.ToInt64(idUsuario);
            personaDalc.ObtenerPersona(ref persona);
            NotificacionDalc notificacion = new NotificacionDalc();
            return notificacion.ObtenerActos(new object[] { null, null, null, null, null, null, persona.NumeroIdentificacion, null, null, null, 0 });
        }

        /// <summary>
        /// HAVA: 14-sep-110
        /// Método  que obtiene el listado de los numero silpa relacionados a las personas notificadas.
        /// </summary>
        /// <param name="NumeroIdentificacionUsuario">string: identificador de </param>
        /// <returns>DataSet: listado de numero silpa</returns>
        public DataSet ListarNumeroSilpaNotificacion(string idUsuario)
        {
            NotificacionDalc notDalc = new NotificacionDalc();
            PersonaDalc personaDalc = new PersonaDalc();
            PersonaIdentity persona = new PersonaIdentity();
            persona.IdApplicationUser = Convert.ToInt64(idUsuario);
            personaDalc.ObtenerPersona(ref persona);
            DataSet dsResultado = notDalc.ListarNumeroSilpaNotificacion(persona.NumeroIdentificacion);
            return dsResultado;
        }

        /// <summary>
        /// RFRP: 30-dic-2010
        /// Método  que obtiene el listado de los numero silpa relacionados a las personas notificadas.
        /// </summary>
        /// <param name="NumeroIdentificacionUsuario">string: identificador de </param>
        /// <returns>DataSet: listado de numero silpa</returns>
        public DataSet ListarActosNotificacion(string idUsuario,string numeroVital,string procesoAdministrativo,string actoAdministrativo)
        {
            NotificacionDalc notDalc = new NotificacionDalc();
            PersonaDalc personaDalc = new PersonaDalc();
            PersonaIdentity persona = new PersonaIdentity();
            persona.IdApplicationUser = Convert.ToInt64(idUsuario);
            personaDalc.ObtenerPersona(ref persona);
            DataSet dsResultado = notDalc.ListarActosNotificacion(persona.NumeroIdentificacion,numeroVital,procesoAdministrativo,actoAdministrativo);            
            return dsResultado;
        }


        /// <summary>
        /// HAVA: 14-sep-110
        /// Método  que obtiene el listado de los numero silpa relacionados a las personas notificadas.
        /// </summary>
        /// <param name="NumeroIdentificacionUsuario">string: identificador de </param>
        /// <returns>DataSet: listado de numero silpa</returns>
        public DataSet ListarNumeroExpedienteNotificacion(string idUsuario)
        {
            NotificacionDalc notDalc = new NotificacionDalc();
            PersonaDalc personaDalc = new PersonaDalc();
            PersonaIdentity persona = new PersonaIdentity();
            persona.IdApplicationUser = Convert.ToInt64(idUsuario);
            personaDalc.ObtenerPersona(ref persona);
            DataSet dsResultado = notDalc.ListarNumeroExpedienteNotificacion(persona.NumeroIdentificacion);
            return dsResultado;
        }

        /// <summary>
        /// HAVA: 16-sep-110
        /// Método  que obtiene el listado de los numero silpa relacionados a las personas notificadas.
        /// </summary>
        /// <param name="NumeroIdentificacionUsuario">string: identificador de </param>
        /// <returns>DataSet: listado de numero silpa</returns>
        public DataSet ListarNumeroActoAdministrativoNotificacion(string idUsuario)
        {
            NotificacionDalc notDalc = new NotificacionDalc();
            PersonaDalc personaDalc = new PersonaDalc();
            PersonaIdentity persona = new PersonaIdentity();
            persona.IdApplicationUser = Convert.ToInt64(idUsuario);
            personaDalc.ObtenerPersona(ref persona);
            DataSet dsResultado = notDalc.ListarNumeroActoAdministrativoNotificacion(persona.NumeroIdentificacion);
            return dsResultado;
        }

        

        public List<NotificacionEntity> ListarActosNotificacion(string NumeroSILPA, string Expediente, string Acto, DateTime? fechaInicial, DateTime? fechaFinal, string IDUsuario)
        {
            NotificacionDalc notificacion = new NotificacionDalc();
            return notificacion.ObtenerActosRecurso(new object[] { null, null, null, Acto, Expediente, NumeroSILPA, IDUsuario, fechaInicial, fechaFinal, null, 0 });
        }

        

        public List<NotificacionEntity> ListarActosNotificacionxUsuario(string NumeroSILPA, int IDUsuario)
        {
            NotificacionDalc notificacion = new NotificacionDalc();
            return notificacion.ObtenerActos(new object[] { -1, -1, -1, "", "", NumeroSILPA, "-1", null, null, "", IDUsuario });
        }

        /// <summary>
        /// Obtiene los registros asociados a X numero silpa/vital
        /// </summary>
        /// <param name="EstadoId">Id del estado que se quiere consultar</param>
        /// <param name="NumeroSilpa">Numero silpa/vital a consultar</param>
        /// <returns></returns>
        public DataTable ListarDocumentosPorSolicitud(int estadoId, string numeroSilpa)
        {

            NotificacionDalc notificacion = new NotificacionDalc();
            return notificacion.ObtenerActosPorEstado(estadoId, numeroSilpa, 0);

        }

        /// <summary>
        /// Obtiene los registros asociados a X numero silpa/vital
        /// </summary>
        /// <param name="EstadoId">Id del estado que se quiere consultar</param>
        /// <param name="NumeroSilpa">Numero silpa/vital a consultar</param>
        /// <returns></returns>
        public DataTable ListarDocumentosPorSolicitud(int estadoId, string numeroSilpa, int idUsuario)
        {

            NotificacionDalc notificacion = new NotificacionDalc();
            return notificacion.ObtenerActosPorEstado(estadoId, numeroSilpa, idUsuario);

        }

        public DataTable ListarDocumentosEvaluacion(int estadoId, string numeroSilpa, int idUsuario)
        {

            NotificacionDalc notificacion = new NotificacionDalc();
            return notificacion.ObtenerDocumentosEvaluacion(estadoId, numeroSilpa, idUsuario);

        }
        public DataTable ListarCertificadosEvaluacion(string NumeroVITAL, string NumeroExpediente, string NumeroCertificado, int? SolicitanteID, int? año)
        {
            NotificacionDalc notificacion = new NotificacionDalc();
            return notificacion.ObtenerCertificados(NumeroVITAL, NumeroExpediente, NumeroCertificado, SolicitanteID, año);
        }


        /// <summary>
        /// 21-jun-2010 - aegb
        /// metodo que retorna la lista de la informacion de datos de homologacion de una tabla basica
        /// </summary>
        /// <param name="idTabla"></param>
        /// <returns></returns>
        public string ObtenerDatosHomologacion(int idTabla)
        {

            TablasBasicasDalc datos = new TablasBasicasDalc();
            List<DatosHomologacionEntity> listaDatos = datos.ObtenerDatosHomologacion(idTabla);

            string xmlObject = string.Empty;
            /// serialización
            XmlSerializador ser = new XmlSerializador();
            /// Obtiene el objeto Serializado.
            xmlObject = ser.serializar(listaDatos);

            return xmlObject;
        }


        /// <summary>
        /// 22092020 - FRS
        /// metodo que retorna el Application User como dato complementario de homologacion
        /// </summary>
        /// <param name="idPersona"></param>
        /// <returns>string con el application user</returns>
        public string ObtenerApplicationUserComplementoHomologacion(int idPersona)
        {
            TablasBasicasDalc datos = new TablasBasicasDalc();
            var applicationUser = datos.ObtenerApplicationUserComplementoHomologacion(idPersona);
            return applicationUser;
        }
        
        /// <summary>
        /// 23092020 - FRS
        /// metodo que retorna si una persona se encuentra activa 
        /// </summary>
        /// <param name="idPersona"></param>
        /// <returns>int con el estado del usuario 1=Activo 0=Inactivo</returns>
        public int ObtenerSiPersonaActivaHomologacion(int idPersona) 
        {
            TablasBasicasDalc datos = new TablasBasicasDalc();
            var personaEstado = datos.ObtenerSiPersonaActivaHomologacion(idPersona);
            return personaEstado;
        }


        public DataSet ListarSalvoconductoDetalle(Int64 idFormInstance)
        {
            SILPA.AccesoDatos.Salvoconducto.SalvoconductoDalc obj = new SILPA.AccesoDatos.Salvoconducto.SalvoconductoDalc();
            DataSet ds_salvo = obj.ListarSalvoconductoDetalles(idFormInstance);
            return ds_salvo;
        }

        public DataSet ListarSalvoconductoEspecimen(Int64 idSalvoconducto)
        {
            SILPA.AccesoDatos.Salvoconducto.SalvoconductoDalc obj = new SILPA.AccesoDatos.Salvoconducto.SalvoconductoDalc();
            DataSet ds_salvo = obj.ListarSalvoconductoEspecimen(idSalvoconducto);
            return ds_salvo;
        }

        public DataSet ListarSalvoconductoRuta(Int64 idSalvoconducto)
        {
            SILPA.AccesoDatos.Salvoconducto.SalvoconductoDalc obj = new SILPA.AccesoDatos.Salvoconducto.SalvoconductoDalc();
            DataSet ds_salvo = obj.ListarSalvoconductoRuta(idSalvoconducto);
            return ds_salvo;
        }


        public DataSet ListarSalvoconductoTransporte(Int64 idSalvoconducto)
        {
            SILPA.AccesoDatos.Salvoconducto.SalvoconductoDalc obj = new SILPA.AccesoDatos.Salvoconducto.SalvoconductoDalc();
            DataSet ds_salvo = obj.ListarSalvoconductoTransporte(idSalvoconducto);
            return ds_salvo;
        }

        public DataSet ListarSalvoconducto(int _idSalvoconducto, string _numSalvoconducto, DateTime? FechaInicial, DateTime? FechaFinal, int? tipoSalv, int? tipoAA, int? idUser)
        {
            SILPA.AccesoDatos.Salvoconducto.SalvoconductoDalc obj = new SILPA.AccesoDatos.Salvoconducto.SalvoconductoDalc();

            DataSet ds_salvo = obj.ListarSalvoconducto(_idSalvoconducto,_numSalvoconducto,FechaInicial,FechaFinal,tipoSalv,tipoAA,idUser);

            return ds_salvo;
        }

        public DataSet ListarTipoTramiteVisibleAutoridadAmbiental(Int32 autoridadID)
        {
            TipoTramiteDalc obj = new TipoTramiteDalc();
            return obj.ListarTipoTramiteVisibleAutoridadAmbiental(autoridadID);
        }
    }
}

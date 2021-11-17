using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos.ResumenEIA.Basicas;
//using SILPA.AccesoDatos.Generico;

namespace SILPA.LogicaNegocio.ResumenEIA
{
    /// <summary>
    /// Clase que contiene todas las listas para combos y dropdownlist de Resumen EIA
    /// </summary>
    public partial class Listas
    {
        /// <summary>
        /// Lista los tipos de contribuyentes
        /// </summary>
        /// <returns>Lista de la clase TipoContribuyenteEntity</returns>
        public static List<TipoContribuyenteEntity> ListaTipoContribuyente()
        {
            //TODO: Ojo, leer del DALC
            TipoContribuyenteDalc objTipoContribuyenteDalc = new TipoContribuyenteDalc();
            return objTipoContribuyenteDalc.Listar();
        }
        /// <summary>
        /// Lista los tipos de documentos
        /// </summary>
        /// <returns>Lista de la clase TipoDocumentoEntity</returns>
        public static List<TipoDocumentoEntity> ListaTipoDocumento()
        {
            TipoDocumentoDalc objTipoDocumentoDalc = new TipoDocumentoDalc();
            return objTipoDocumentoDalc.Listar();
        }
        /// <summary>
        /// Lista de los departamentos
        /// </summary>
        /// <returns>Lista de la clase DepartamentoEntity</returns>
        public static List<DepartamentoEntity> ListaDepartamento()
        {
            DepartamentoDalc objDepartamentoDalc = new DepartamentoDalc();
            return objDepartamentoDalc.Listar();
        }
        /// <summary>
        /// Lista de los municipios por departamento
        /// </summary>
        /// <param name="idDepto">Identificador del departamento al que pertenecen los municipìos</param>
        /// <returns>Lista de la clase MunicipioEntity</returns>
        public static List<MunicipioEntity> ListaMunicipio(int idDepto)
        {
            MunicipioDalc objMunicipioDalc = new MunicipioDalc();
            return objMunicipioDalc.Listar(idDepto);
        }
        /// <summary>
        /// Lista de los sectores productivos
        /// </summary>
        /// <returns>Lista de la clase SectorProductivoEntity</returns>
        public static List<SectorProductivoEntity> ListaSectoresProductivos()
        {
            SectorProductivoDalc objSectorProductivoDalc = new SectorProductivoDalc();
            return objSectorProductivoDalc.Listar();
        }
        /// <summary>
        /// Lista de los Tipos de Acuiferos
        /// </summary>
        /// <returns>Lista de la clase TipoAcuiferoEntity</returns>
        public static List<TipoAcuiferoEntity> ListaTiposAcuiferos()
        {
            TipoAcuiferoDalc objTipoAcuiferoDalc = new TipoAcuiferoDalc();
            return objTipoAcuiferoDalc.Listar();
        }
        /// <summary>
        /// Lista de los Gradientes Hidraulicos
        /// </summary>
        /// <returns>Lista de la clase GradienteHidraulicoEntity</returns>
        public static List<GradienteHidraulicoEntity> ListaGradienteHidraulico()
        {
            GradienteHidraulicoDalc objGradienteHidraulicoDalc = new GradienteHidraulicoDalc();
            return objGradienteHidraulicoDalc.Listar();
        }
        /// <summary>
        /// Lista de los Tipos de puentos de Agua
        /// </summary>
        /// <returns>Lista de la clase TipoPtoAguaEntity</returns>
        public static List<TipoPtoAguaEntity> ListaTipoPtoAgua()
        {
            TipoPtoAguaDalc objTipoPtoAguaDalc = new TipoPtoAguaDalc();
            return objTipoPtoAguaDalc.Listar();
        }
        /// <summary>
        /// Lista de los Tipos de variables climaticas
        /// </summary>
        /// <returns>Lista de la clase TipoVarClimaticaEntity</returns>
        public static List<TipoVarClimaticaEntity> ListaTipoVarClimatica()
        {
            TipoVarClimaticaDalc objTipoVarClimatica = new TipoVarClimaticaDalc();
            return objTipoVarClimatica.Listar();
        }
        /// <summary>
        /// Lista de los Laboratorios de estudios de calidad
        /// </summary>
        /// <param name="idTipoLab">Identificador del tipo de estudio: Aguas subterraneas, Aguas Superficiales, Suelos etc. </param>
        /// <returns>Lista de la clase LabEstCalidadEntity</returns>
        public static List<LabEstCalidadEntity> ListaLaboratorio(int? idTipoLab)
        {
            LabEstCalidadDalc objLabEstCalidadDalc = new LabEstCalidadDalc();
            return objLabEstCalidadDalc.Listar(idTipoLab);
        }
        /// <summary>
        /// Lista de los Tipos de muestras para estudios de calidad
        /// </summary>
        /// <returns>Lista de la clase TipoMuestraEntity</returns>
        public static List<TipoMuestraEntity> ListaTipoMuestra()
        {
            TipoMuestraDalc objTipoMuestra = new TipoMuestraDalc();
            return objTipoMuestra.Listar();
        }
        /// <summary>
        /// Lista de los Tipos de periodos para estudios de calidad
        /// </summary>
        /// <returns>Lista de la clase PeriodoMuestraEntity</returns>
        public static List<PeriodoMuestraEntity> ListaPeriodoMuestra()
        {
            PeriodoMuestraDalc objPeriodoMuestra = new PeriodoMuestraDalc();
            return objPeriodoMuestra.Listar();
        }
        /// <summary>
        /// Lista de las caracteristicas de estudios de calidad
        /// </summary>
        /// <param name="idTipoLab">Identificador del tipo de caracteristica: Fisicas, Quimicas etc. </param>
        /// <returns>Lista de la clase LabEstCalidadEntity</returns>
        public static List<CaractEstCalidadEntity> ListaCaracteristica(int? idTipoCaract)
        {
            CaractEstCalidadDalc objCaractEstCalidad = new CaractEstCalidadDalc();
            return objCaractEstCalidad.Listar(idTipoCaract);
        }
        /// <summary>
        /// Lista de los Tipos de fuentes de agua subterraneas
        /// </summary>
        /// <returns>Lista de la clase TipoFuentAguaSubtEntity</returns>
        public static List<TipoFuentAguaSubtEntity> ListaTipoFuentAguaSubt()
        {
            TipoFuentAguaSubtDalc objTipoFuentAguaSubtDalc = new TipoFuentAguaSubtDalc();
            return objTipoFuentAguaSubtDalc.Listar();
        }
        /// <summary>
        /// Lista de los Métodos de determinación de caracteristicas en estudios de Calidad
        /// </summary>
        /// <returns>Lista de la clase TipoFuentAguaSubtEntity</returns>
        public static List<MetDetEstCalidadEntity> ListaMetDetEstCalidad()
        {
            MetDetEstCalidadDalc objMetDetEstCalidadDalc = new MetDetEstCalidadDalc();
            return objMetDetEstCalidadDalc.Listar();
        }
        /// <summary>
        /// Lista de Los tipos de Olas
        /// </summary>
        /// <returns>Lista de la clase TipoOlaEntity</returns>
        public static List<TipoOlaEntity> ListaTipoOla()
        {
            TipoOlaDalc objTipoOlaDalc = new TipoOlaDalc();
            return objTipoOlaDalc.Listar();
        }
        /// <summary>
        /// Lista de Los tipos de fuentes de ruidos
        /// </summary>
        /// <returns>Lista de la clase TipoFuenteRuidoEntity</returns>
        public static List<TipoFuenteRuidoEntity> ListaTipoFuenteRuido()
        {
            TipoFuenteRuidoDalc objTipoFuenteRuido = new TipoFuenteRuidoDalc();
            return objTipoFuenteRuido.Listar();
        }
        /// <summary>
        /// Lista de Los Clasificación de tipos de ecosistemas
        /// </summary>
        /// <returns>Lista de la clase ClasTipoEcosistemaEntity</returns>
        /// 
        public static List<ClasTipoEcosistemaEntity> ListaClasTipoEcosistema()
        {
            ClasTipoEcosistemaDalc objClasTipoEcosistema = new ClasTipoEcosistemaDalc();
            return objClasTipoEcosistema.Listar();
        }
        /// <summary>
        /// Lista de los tipos de ecosistemas terrestres
        /// </summary>
        /// <param name="idClasEcosistema">Identificador de la clasificación de ecosistemas terrestres. </param>
        /// <returns>Lista de la clase TipoEcosistemaEntity</returns>
        public static List<TipoEcosistemaEntity> ListaTipoEcosisTerrestre(int? idClasEcosistema)
        {
            TipoEcosistemaDalc objTipoEcosistema = new TipoEcosistemaDalc();
            return objTipoEcosistema.Listar(idClasEcosistema);
        }
        /// <summary>
        /// Lista de Las escalas de trabajo
        /// </summary>
        /// <returns>Lista de la clase EscalaTrabajoEntity</returns>
        public static List<EscalaTrabajoEntity> ListaEscalaTrabajo()
        {
            EscalaTrabajoDalc objEscalaTrabajo = new EscalaTrabajoDalc();
            return objEscalaTrabajo.Listar();
        }
        /// <summary>
        /// Lista de Las fuentes de información de ecosistemas terrestres
        /// </summary>
        /// <returns>Lista de la clase EscalaTrabajoEntity</returns>
        public static List<FuenteInfoEcoterrEntity> ListaFuenteInfoEcoterr()
        {
            FuenteInfoEcoterrDalc objFuenteInfoEcoterr = new FuenteInfoEcoterrDalc();
            return objFuenteInfoEcoterr.Listar();
        }
        /// <summary>
        /// Lista de Las unidades de area
        /// </summary>
        /// <returns>Lista de la clase EscalaTrabajoEntity</returns>
        public static List<UnidadAreaEntity> ListaUnidadArea()
        {
            UnidadAreaDalc objUnidadArea = new UnidadAreaDalc();
            return objUnidadArea.Listar();
        }
        /// <summary>
        /// Lista de los tipos de oleaje
        /// </summary>
        /// <returns>Lista de la clase TipoOleajeEntity</returns>
        public static List<TipoOleajeEntity> ListaTipoOleaje()
        {
            TipoOleajeDalc objTipoOleaje = new TipoOleajeDalc();
            return objTipoOleaje.Listar();
        }
        /// <summary>
        /// Lista de los tipos de estructuras verticales dominantes en Descripción Fisionómica
        /// </summary>
        /// <returns>Lista de la clase EstrucVertDomEntity</returns>
        public static List<EstrucVertDomEntity> ListaEstrucVertDom()
        {
            EstrucVertDomDalc objEstrucVertDom = new EstrucVertDomDalc();
            return objEstrucVertDom.Listar();
        }
        /// <summary>
        /// Lista de los tipos de estructuras verticales dominantes en Descripción Fisionómica
        /// </summary>
        /// <returns>Lista de la clase PosFitoDominanteEntity</returns>
        public static List<PosFitoDominanteEntity> ListaPosFitoDominante()
        {
            PosFitoDominanteDalc objPosFitoDominante = new PosFitoDominanteDalc();
            return objPosFitoDominante.Listar();
        }
        /// <summary>
        /// Lista de los tipos de estratos
        /// </summary>
        /// <returns>Lista de la clase TipoEstratoEntity</returns>
        public static List<TipoEstratoEntity> ListaTipoEstrato()
        {
            TipoEstratoDalc objTipoEstrato = new TipoEstratoDalc();
            return objTipoEstrato.Listar();
        }
        /// <summary>
        /// Lista de los tipos de especies flora
        /// </summary>
        /// <returns>Lista de la clase TipoEspecieFloraEntity</returns>
        public static List<TipoEspecieFloraEntity> ListaTipoEspecieFlora()
        {
            TipoEspecieFloraDalc objTipoEspecieFlora = new TipoEspecieFloraDalc();
            return objTipoEspecieFlora.Listar();
        }
        /// <summary>
        /// Lista de los tipos de especies fauna
        /// </summary>
        /// <returns>Lista de la clase TipoEspeciesFaunaEntity</returns>
        public static List<TipoEspeciesFaunaEntity> ListaTipoEspeciesFauna()
        {
            TipoEspeciesFaunaDalc objTipoEspeciesFauna = new TipoEspeciesFaunaDalc();
            return objTipoEspeciesFauna.Listar();
        }
        /// <summary>
        /// Lista de los tipos de biota
        /// </summary>
        /// <returns>Lista de la clase TipoBiotaEntity</returns>
        public static List<TipoBiotaEntity> ListaTipoBiota()
        {
            TipoBiotaDalc objTipoBiota = new TipoBiotaDalc();
            return objTipoBiota.Listar();
        }
        /// <summary>
        /// Lista de los tipos de especies marinas
        /// </summary>
        /// <returns>Lista de la clase TipoEspecieMarinaEntity</returns>
        public static List<TipoEspecieMarinaEntity> ListaTipoEspecieMarina()
        {
            TipoEspecieMarinaDalc objTipoEspecieMarina = new TipoEspecieMarinaDalc();
            return objTipoEspecieMarina.Listar();
        }
        /// <summary>
        /// Lista de los tipos de unidades politico administra
        /// </summary>
        /// <returns>Lista de la clase UnidadPolAdminEntity</returns>
        public static List<UnidadPolAdminEntity> ListaUnidadPolAdmin()
        {
            UnidadPolAdminDalc objUnidadPolAdmin = new UnidadPolAdminDalc();
            return objUnidadPolAdmin.Listar();
        }
        /// <summary>
        /// Lista de los tipos de otros tipos de población
        /// </summary>
        /// <returns>Lista de la clase TipoOtraPobEntity</returns>
        public static List<TipoOtraPobEntity> ListaTipoOtraPob()
        {
            TipoOtraPobDalc objTipoOtraPob = new TipoOtraPobDalc();
            return objTipoOtraPob.Listar();
        }
        /// <summary>
        /// Lista de los tipos de otros tipos de institución
        /// </summary>
        /// <returns>Lista de la clase TipoInstitucionEntity</returns>
        public static List<TipoInstitucionEntity> ListaTipoInstitucion()
        {
            TipoInstitucionDalc objTipoInstitucion = new TipoInstitucionDalc();
            return objTipoInstitucion.Listar();
        }
    }
}

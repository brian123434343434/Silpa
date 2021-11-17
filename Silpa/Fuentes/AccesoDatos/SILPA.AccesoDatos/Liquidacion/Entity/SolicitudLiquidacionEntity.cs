using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.AccesoDatos.Generico;

namespace SILPA.AccesoDatos.Liquidacion.Entity
{
    [Serializable]
    public class SolicitudLiquidacionEntity
    {
        #region Propiedades

            /// <summary>
            /// Identificador del identificador de solicitud
            /// </summary>
            public int SolicitudLiquidacionID { get; set; }

            /// <summary>
            /// Identificador del solicitante
            /// </summary>
            public int SolicitanteID { get; set; }

            /// <summary>
            /// Tipo de solicitud de liquidacion
            /// </summary>
            public TipoSolicitudLiquidacionEntity TipoSolicitud { get; set; }

            /// <summary>
            /// Clase de solicitud de liquidación a realizar
            /// </summary>
            public ClaseSolicitudLiquidacionEntity ClaseSolicitud { get; set; }

            /// <summary>
            /// Tramite de liquidación a realizar
            /// </summary>
            public TramiteLiquidacionEntity Tramite { get; set; }

            /// <summary>
            /// Sector al cual se dirige la liquidación
            /// </summary>
            public SectorLiquidacionEntity Sector { get; set; }

            /// <summary>
            /// Autoridad a la cual pertenece la solicitud
            /// </summary>
            public AutoridadAmbientalIdentity AutoridadAmbiental { get; set; }

            /// <summary>
            /// Proyecto a desarrollar
            /// </summary>
            public ProyectoLiquidacionEntity Proyecto { get; set; }

            /// <summary>
            /// Proyecto a desarrollar
            /// </summary>
            public ActividadLiquidacionEntity Actividad { get; set; }

            /// <summary>
            /// Nombre del proyecto
            /// </summary>
            public string NombreProyecto { get; set; }

            /// <summary>
            /// Descripcion del proyecto
            /// </summary>
            public string DescripcionProyecto { get; set; }

            /// <summary>
            /// Valor del proyecto
            /// </summary>
            public decimal ValorProyecto { get; set; }

            /// <summary>
            /// Valor del proyecto en letras
            /// </summary>
            public string ValorProyectoLetras { get; set; }

            /// <summary>
            /// Valor de la modificación
            /// </summary>
            public decimal ValorModificacion { get; set; }

            /// <summary>
            /// Valor de la modificación en letras
            /// </summary>
            public string ValorModificacionLetras { get; set; }

            /// <summary>
            /// Indica si el el proyecto es PINE
            /// </summary>
            public bool? ProyectoPINE { get; set; }

            /// <summary>
            /// Listado de permisos relacionados a la liquidación
            /// </summary>
            public List<PermisoSolicitudLiquidacionEntity> Permisos { get; set; }

            /// <summary>
            /// Listado de regiones asociados a la liquidación
            /// </summary>
            public List<RegionSolicitudLiquidacionEntity> Regiones { get; set; }

            /// <summary>
            /// Listado de ubicaciones relacionadas a la liquidación
            /// </summary>
            public List<UbicacionSolicitudLiquidacionEntity> Ubicaciones { get; set; }

            /// <summary>
            /// Indica si el el proyecto se desarrolla en aguas maritimas
            /// </summary>
            public bool? ProyectoAguasMaritimas { get; set; }

            /// <summary>
            /// Oceano donde se desarrolla el proyecto
            /// </summary>
            public OceanoLiquidacionEntity Oceano { get; set; }

            /// <summary>
            /// Listado de rutas relacionadas a la liquidación
            /// </summary>
            public List<RutaLogisticaSolicitudLiquidacionEntity> Rutas { get; set; }

            /// <summary>
            /// NUmero Vital asociado a la solicitud
            /// </summary>
            public string NumeroVITAL { get; set; }

            /// <summary>
            /// Fecha de radicación en VITAL
            /// </summary>
            public DateTime FechaRadicacionVITAL { get; set; }

            /// <summary>
            /// Estado de la solicitud de liquidación
            /// </summary>
            public EstadoSolicitudLiquidacionEntity EstadoSolicitud { get; set; }

            /// <summary>
            /// Listado de cobros asociados a la solicitud
            /// </summary>
            public List<CobroSolicitudLiquidacionEntity> CobrosSolicitud { get; set; }

            /// <summary>
            /// Fecha de la creación de la solicitud
            /// </summary>
            public DateTime FechaCreacion { get; set; }

            /// <summary>
            /// Fecha de la modificación de la solicitud
            /// </summary>
            public DateTime FechaModificacion { get; set; }

            
        #endregion
    }
}

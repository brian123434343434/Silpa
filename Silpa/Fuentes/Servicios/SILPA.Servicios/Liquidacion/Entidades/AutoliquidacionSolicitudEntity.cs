using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SILPA.Comun;

namespace SILPA.Servicios.Liquidacion.Entidades
{
    public class AutoliquidacionSolicitudEntity : EntidadSerializable
    {
        #region Propiedades

            /// <summary>
            /// Identificador del formulario
            /// </summary>
            public int FormularioID { get; set; }

            /// <summary>
            /// Numero VITAL de la soliciyuda
            /// </summary>
            public string NumeroVITAL { get; set; }

            /// <summary>
            /// Numero de radicado SIGPRO
            /// </summary>
            public string Radicado { get; set; }

            /// <summary>
            /// Fecha de radicación
            /// </summary>
            public DateTime FechaRadicado { get; set; }

            /// <summary>
            /// Nombre del proyecto
            /// </summary>
            public string NombreProyecto { get; set; }

            /// <summary>
            /// Identificador del Proyecto
            /// </summary>
            public int ProyectoID { get; set; }

            /// <summary>
            /// Proyecto
            /// </summary>
            public string Proyecto { get; set; }

            /// <summary>
            /// Identificador de la solicitud realizada
            /// </summary>
            public int SolicitudID { get; set; }

            /// <summary>
            /// Identificador del tarmite realizado
            /// </summary>
            public int TramiteID { get; set; }

            /// <summary>
            /// Identificador del sector
            /// </summary>
            public int SectorID { get; set; }

            /// <summary>
            /// Actividad a Realizar
            /// </summary>
            public string Actividad { get; set; }

            /// <summary>
            /// Identificador de la actividad a realizar
            /// </summary>
            public int ActividadID { get; set; }

            /// <summary>
            /// Valor del proyecto
            /// </summary>
            public decimal ValorProyecto { get; set; }

            /// <summary>
            /// Indica si los permisos son exclusivos de la ANLA
            /// </summary>
            public bool PermisosANLA { get; set; }

            /// <summary>
            /// Listado de permisos solicitados
            /// </summary>
            public List<AutoliquidacionPermisoEntity> Permisos { get; set; }

            /// <summary>
            /// Listado de ubicaciones donde se lleva a cabo el proyecto
            /// </summary>
            public List<AutoliquidacionUbicacionEntity> Ubicaciones { get; set; }

            /// <summary>
            /// Información de respuesta
            /// </summary>
            public AutoliquidacionRespuestaEntity Respuesta { get; set; }

        #endregion

        #region Metodos

            /// <summary>
            /// Agrgar un nuevo permiso al listado
            /// </summary>
            /// <param name="p_objPermiso">AutoliquidacionPermisoEntity con la información del permiso</param>
            public void AdicionarPermiso(AutoliquidacionPermisoEntity p_objPermiso)
            {
                if(p_objPermiso != null){
                    if (this.Permisos == null)
                        this.Permisos = new List<AutoliquidacionPermisoEntity>();
                    this.Permisos.Add(p_objPermiso);
                }
            }

            /// <summary>
            /// Agrgar una nueva ubicación al listado
            /// </summary>
            /// <param name="p_objUbicacion">AutoliquidacionUbicacionEntity con la información de la ubicación</param>
            public void AdicionarUbicacion(AutoliquidacionUbicacionEntity p_objUbicacion)
            {
                if (p_objUbicacion != null)
                {
                    if (this.Ubicaciones == null)
                        this.Ubicaciones = new List<AutoliquidacionUbicacionEntity>();
                    this.Ubicaciones.Add(p_objUbicacion);
                }
            }

        #endregion



    }
}

using System;
using System.Collections.Generic;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.Notificacion;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.RecursoReposicion;
using System.Configuration;
using SoftManagement.Log;

namespace SILPA.LogicaNegocio.Recurso
{

    public class ListarActosRecursosReposicion
    {

        private string _nombreParametroMaxDiasRecurso;

        public string NombreParametro
        {
            get { return _nombreParametroMaxDiasRecurso; }
            set { _nombreParametroMaxDiasRecurso = value;  }
        }

        public List<RecursoReposicionPresentacion> listarActosRecursos(string NumeroSILPA, string Expediente, string Acto, DateTime? fechaInicial, DateTime? fechaFinal, string Usuario)
        {
            Listas _listarActos = new Listas();
            PersonaDalc perDalc = new PersonaDalc();
            PersonaIdentity persona = new PersonaIdentity();
            persona.IdApplicationUser = Convert.ToInt64(Usuario);
            perDalc.ObtenerPersona(ref persona);

            List<NotificacionEntity> _listaActos = _listarActos.ListarActosNotificacion(NumeroSILPA, Expediente, Acto, fechaInicial, fechaFinal, persona.NumeroIdentificacion);
            List<RecursoReposicionPresentacion> listaRet = new List<RecursoReposicionPresentacion>();
            RecursoDalc recursoDalc = new RecursoDalc();
            List<RecursoEntity> recs = null; 
            
            foreach (NotificacionEntity entity in _listaActos)
            {
                if (entity.IdTipoActo.HabilitadoReposicion)
                {
                    if(entity.ListaPersonas.Count>0)
                    {
         
                        RecursoReposicionPresentacion rep = new RecursoReposicionPresentacion();
                        rep.FechaActo = entity.FechaActo;
                        rep.NumeroActoAdministrativo = entity.NumeroActoAdministrativo;
                        rep.NumeroSILPA = entity.NumeroSILPA;
                        rep.IdActoNotificacion = entity.IdActoNotificacion;
                        rep.RutaDocumento = entity.RutaDocumento;
                        rep.ProcesoAdministracion = entity.ProcesoAdministracion;

                        persona = new PersonaIdentity();
                        persona.IdApplicationUser = Convert.ToInt64(Usuario);
                        perDalc.ObtenerPersona(ref persona);
                        string nombre;
                        foreach (PersonaNotificarEntity pe in entity.ListaPersonas)
                        {
                            if (pe.NumeroIdentificacion == entity.IdentificacionFuncionario)
                            {
                                recs = new List<RecursoEntity>();
                                recs = recursoDalc.ObtenerRecursos(new object[] { null, null, null, entity.IdActoNotificacion, pe.NumeroIdentificacion });
                                if (recs.Count == 0)
                                {
                                    nombre = entity.IdTipoActo.Nombre;
                                    rep.FechaNotificacion = pe.FechaNotificado;
                                    rep.CampoBorrar = entity.NombreProyecto;
                                    listaRet.Add(rep);
                                }
                            }
                           
                        }
                    }
                    

                }
            }
            
            return listaRet;
        }

           
        [Obsolete("Este metodo no es requerido ya que la identificación de la persona fue movida al recurso")]    
        private bool tieneRecursoInterpuesto(List<RecursoEntity>recs,PersonaIdentity persona)
        {
           
            List<PersonaNotificarEntity> personasNot;
            if (recs != null)
            {
                foreach (RecursoEntity rec in recs)
                {
                    if (rec.Acto != null)
                    {
                        if (rec.Acto.ListaPersonas != null)
                        {
                            personasNot = rec.Acto.ListaPersonas;
                            foreach (PersonaNotificarEntity personaNot in personasNot)
                            {
                                if (personaNot.NumeroIdentificacion == persona.NumeroIdentificacion)
                                    return true;
                            }
                        }
                    }
                }
            }
            
            return false;
        }

       
        protected bool seEncuentraEnPeriodoDePresentarReposicion(PersonaNotificarEntity entity)
        {

            ParametroDalc parametroDalc = new ParametroDalc();
            List<ParametroEntity> parametros = parametroDalc.obtenerParametros();
            int maxNumeroDias = 0;
            foreach (ParametroEntity par in parametros)
            {
                //entontramos el parametro que nos indica el maximo numero de dias para interponer acto de reposicion
                if (par.NombreParametro.Equals(NombreParametro))
                {
                    maxNumeroDias = Convert.ToInt32(par.Parametro);
                    if (entity.FechaNotificado != null)
                    {
                        if (parametroDalc.obtenerDiasHabilesEntre(entity.FechaNotificado, System.DateTime.Now) <= maxNumeroDias)
                        {
                            return true;
                        }

                    }
                    else
                    {
                        return true;
                    }
                }
            }

            return false;
        }




    }



}


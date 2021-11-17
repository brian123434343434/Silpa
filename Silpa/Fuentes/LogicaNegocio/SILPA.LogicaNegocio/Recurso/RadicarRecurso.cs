using System;
using System.Collections.Generic;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccessoDatos.Generico;
using SILPA.AccesoDatos.Notificacion;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.RecursoReposicion;
using System.Configuration;
using SILPA.AccesoDatos.DAA;
using SoftManagement.Log;

namespace SILPA.LogicaNegocio.Recurso
{

    public class RadicarRecurso
    {

        private RadicacionDocumento radicacion ;
            
        public RadicarRecurso()
        {
            radicacion = new RadicacionDocumento();
        }

        public void radicarRecursoReposicion(
                                                RecursoReposicionPresentacion recursoPresentacion,
                                                RecursoEntity recEntity,
                                                string userId,
                                                int idEstadoRecurso
                                            )
        {

            
            int idAA;
            int idRadicacion;
            #region listas
            //List<RecursoDocumentoEntity> listaRadicada = new List<RecursoDocumentoEntity>();

            //crea un registro de radicacion por cada documento
            //List<string> listaDocsRadicada = new List<string>();
            //List<byte[]> listaDocsBinariosRadicada= new List<byte[]>();
            //List<string> listaNumeroRadicacionRadicada = new List<string>();


            List<RecursoDocumentoEntity> listaRadicar = new List<RecursoDocumentoEntity>();
            //crea un registro de radicacion por todos los docuementos
            List<string> listaDocsRadicar = new List<string>();
            List<byte[]> listaDocsBinariosRadicar = new List<byte[]>();

            #endregion 
            
            #region buscar idAA
            SolicitudDAAEIADalc solicitudDalc = new SolicitudDAAEIADalc();
            SolicitudDAAEIAIdentity solicitud = new SolicitudDAAEIAIdentity();
            solicitud.NumeroSilpa = recursoPresentacion.NumeroSILPA;

            solicitudDalc.ObtenerSolicitud(ref solicitud);
            idAA= Convert.ToInt32(solicitud.IdAutoridadAmbiental);
            #endregion

            #region separarListas

            if (recEntity.ListaDocumentos != null)
            {

                foreach (RecursoDocumentoEntity doc in recEntity.ListaDocumentos)
                {
                    //if (doc.NumeroRadicado == null || doc.NumeroRadicado == string.Empty)
                    if(doc.Archivo!=null && doc.NombreDocumento!=null)
                    {
                        listaDocsRadicar.Add(doc.NombreDocumento);
                        listaDocsBinariosRadicar.Add(doc.Archivo);
                        listaRadicar.Add(doc);
                        //listaDocsRadicada.Add(doc.RutaDocumento);
                        //listaDocsBinariosRadicada.Add(doc.Archivo);
                        //listaNumeroRadicacionRadicada.Add(doc.NumeroRadicado);
                        //listaRadicada.Add(doc);
                    }
                }
            }
            #endregion

            crearRadicaciones(recursoPresentacion, userId, idAA, ref listaRadicar, listaDocsRadicar, listaDocsBinariosRadicar);

            #region ParametrosAdicionales
            
            recEntity.IdActoNotificacion = recursoPresentacion.IdActoNotificacion;
            RecursoEstadoDalc recEstadoDalc = new RecursoEstadoDalc();
            recEntity.Estado = recEstadoDalc.obtenerRecursoEstado(new object[] { idEstadoRecurso, null, null });
            NotificacionDalc notDalc = new NotificacionDalc();
            recEntity.Acto = notDalc.ObtenerActo(new object[] { recursoPresentacion.IdActoNotificacion, null, null, null, null, null, null, null, null, null, null});

            
            PersonaDalc personaDalc = new PersonaDalc();
            PersonaIdentity persona = new PersonaIdentity();
            persona.IdApplicationUser =Convert.ToInt64(userId);
            personaDalc.ObtenerPersona(ref persona);
            recEntity.NumeroIdentificacion = persona.NumeroIdentificacion;
            #endregion
            
            RecursoDalc recursoDalc = new RecursoDalc();
            recursoDalc.InsertarRecurso(ref recEntity);
            
            //asignamos el id del recurso de reposicion a los documentos
            
            foreach (RecursoDocumentoEntity doc in recEntity.ListaDocumentos)
            {
                doc.IdRecursoReposicion = recEntity.IDRecurso;
                doc.RutaDocumento = radicacion._objRadDocIdentity.UbicacionDocumento + doc.NombreDocumento;
            }
            

            insertarDocumentos(recEntity);

            SMLog.Escribir(Severidad.Informativo, "RadicarRecurso E");
      }

        private void crearRadicaciones(RecursoReposicionPresentacion recursoPresentacion, string userId, int idAA, 
                                        ref List<RecursoDocumentoEntity> listaRadicar, List<string> listaDocsRadicar, 
                                        List<byte[]> listaDocsBinariosRadicar)
        {
            string documento;
            byte[] binaryDoc;
            int idRadicacion;

            IEnumerator<string> itDocs;
            IEnumerator<byte[]> itBinaryDocs;
            IEnumerator<RecursoDocumentoEntity> itRepDocsEntity;

            if (listaDocsRadicar.Count > 0) //radicar todos los documentos bajo un solo id de radicacion
            {
                idRadicacion = radicarSinNumeroRadicacion(userId, recursoPresentacion.NumeroSILPA, listaDocsRadicar, listaDocsBinariosRadicar, idAA,recursoPresentacion.NumeroActoAdministrativo);
                foreach (RecursoDocumentoEntity recdoc in listaRadicar)
                {
                    recdoc.IDRadicado = idRadicacion;
                }
            }


        }
        private void insertarDocumentos(RecursoEntity recurso)
        {
            RecursoDocumentoDalc dalc = new RecursoDocumentoDalc();
            foreach (RecursoDocumentoEntity doc in recurso.ListaDocumentos)
            {

                dalc.InsertarRecursoDocumento(doc);
            }
        }        


        //Metodo que realiza la radicacion, en caso de recibir el idRadicacion en le asigna como identificador a la radicacion el que se encuentra en el documento
        //retorna idRadicacion PK de la tabla GEN_RADICACION
        private int radicarDocumento(string userId, string numeroSILPA, string nombreDoc, byte[] binaryFile, string numeroRadicacion, int idAA, string idActoAdministrativo)
        {
            //Se busca el parámetro para obtener el id del trámite -- Interponer Recurso de Reposición
            ParametroDalc parametroDalc = new ParametroDalc();
            ParametroEntity parametro = new ParametroEntity();
            parametro.IdParametro = -1;
            parametro.NombreParametro = "tramite_recurso_reposicion";
            parametroDalc.obtenerParametros(ref parametro);
            radicacion._objRadDocIdentity.FechaRadicacion = System.DateTime.Now;
            radicacion._objRadDocIdentity.LstBteDocumentoAdjunto = new List<byte[]>();
            radicacion._objRadDocIdentity.LstBteDocumentoAdjunto.Add(binaryFile);
            radicacion._objRadDocIdentity.LstNombreDocumentoAdjunto = new List<string>();
            radicacion._objRadDocIdentity.LstNombreDocumentoAdjunto.Add(nombreDoc);
            radicacion._objRadDocIdentity.NumeroSilpa = numeroSILPA;
            radicacion._objRadDocIdentity.IdSolicitante = userId;
            radicacion._objRadDocIdentity.IdAA = idAA;
            radicacion._objRadDocIdentity.ActoAdministrativo = idActoAdministrativo;
            radicacion._objRadDocIdentity.NumeroRadicacionDocumento = numeroRadicacion;
            //radicacion._objRadDocIdentity.RadicadoDocumento
            return radicacion.RadicarDocumento();
                    
        }
        //retorna idRadicacion PK de la tabla GEN_RADICACION

        private int radicarSinNumeroRadicacion(string userId, string numeroSILPA, List<string> listaDocsRadicar, List<byte[]> listaDocsBinariosRadicar, int idAA,string idActoAdministrativo)
        {
            //radicacion._objRadDocIdentity.FechaRadicacion = System.DateTime.Now;
            
            radicacion._objRadDocIdentity.LstBteDocumentoAdjunto = listaDocsBinariosRadicar;
            radicacion._objRadDocIdentity.LstNombreDocumentoAdjunto = listaDocsRadicar;
            radicacion._objRadDocIdentity.NumeroSilpa = numeroSILPA;
            radicacion._objRadDocIdentity.IdSolicitante = userId;
            radicacion._objRadDocIdentity.IdAA = idAA;
            radicacion._objRadDocIdentity.ActoAdministrativo = idActoAdministrativo;
            radicacion._objRadDocIdentity.NumeroRadicacionDocumento = string.Empty;
            return radicacion.RadicarDocumento();

        }

    }

    



}


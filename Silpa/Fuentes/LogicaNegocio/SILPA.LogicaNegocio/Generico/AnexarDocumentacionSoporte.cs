using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Collections;
using SILPA.AccesoDatos.Generico;

namespace SILPA.LogicaNegocio.Generico
{
    public class AnexarDocumentacionSoporte
    {
        public AnexarDocumentacionSoporteIdentity Identity;

        /// <summary>
        /// Objeto radicación
        /// </summary>
        public RadicacionDocumento _objRadicacion;

        public AnexarDocumentacionSoporte
        ( 
            string strNumeroSilpa, 
            string strNumeroRadicacionDocumento,
            string strActoAdministrativo, // Numero del Acto Administrativo
            string strNumeroRadicadoAA,
            int intIdAA,
            List<string> lstString, 
            List<Byte[]> lstByte,
            List<string> lstInfo 
        ) 
        {
            Identity = new AnexarDocumentacionSoporteIdentity();
            Identity.DocumentosAdjuntos= lstString;
            Identity.BytesDocumentosAdjuntos = lstByte;
            Identity.InfoAdicionalDocumento = lstInfo;

            this._objRadicacion = new RadicacionDocumento();
            this._objRadicacion._objRadDocIdentity.DocumentoAdjunto = Identity.BytesDocumentosAdjuntos[0];
            this._objRadicacion._objRadDocIdentity.NombreDocumentoAdjunto = Identity.DocumentosAdjuntos[0];

            this._objRadicacion._objRadDocIdentity.NumeroSilpa = strNumeroSilpa;
            this._objRadicacion._objRadDocIdentity.NumeroRadicacionDocumento = strNumeroRadicacionDocumento;
            this._objRadicacion._objRadDocIdentity.NumeroRadicadoAA = strNumeroRadicadoAA;
            this._objRadicacion._objRadDocIdentity.IdAA = intIdAA;
            this._objRadicacion._objRadDocIdentity.ActoAdministrativo = strActoAdministrativo;
        }

        /// <summary>
        /// Método que permite la radicación  de los documentos anexos
        /// </summary>
        /// <returns>bool: True / False</returns>
        public bool RadicarDocumentosAnexos() 
        {
            try 
            {
                this._objRadicacion.RadicarDocumento();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }


        private void RegistrarDetalleRadicacionDocumento(int intIdRadicacion)
        {

        }


    }
}

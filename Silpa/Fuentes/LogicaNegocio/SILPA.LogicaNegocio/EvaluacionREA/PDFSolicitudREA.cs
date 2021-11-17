using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SILPA.AccesoDatos.EvaluacionREA.Dalc;
using SILPA.AccesoDatos.Excepciones;
using SILPA.LogicaNegocio.Excepciones;
using SoftManagement.Log;

namespace SILPA.LogicaNegocio.EvaluacionREA
{
    public class PDFSolicitudREA
    {
        #region Objetos

            private PDFSolicitudREADalc _objPDFSolicitudREADalc;

        #endregion


        #region Creadoras

            /// <summary>
            /// Creadora
            /// </summary>
            public PDFSolicitudREA()
            {
                //Crear objeto transaccionalidad
                this._objPDFSolicitudREADalc = new PDFSolicitudREADalc();
            }

        #endregion


        #region Metodos Publicos

            /// <summary>
            /// Consultar la información del registro para generación del pdf
            /// </summary>
            /// <param name="p_intRegistroID">int con el identificador del registro</param>
            /// <returns>DataSet con la informacion para la generación del PDF</returns>
            public DataSet ConsultarRegistroPDF(int p_intRegistroID)
            {
                try
                {
                    return this._objPDFSolicitudREADalc.ConsultarRegistroPDF(p_intRegistroID);
                }
                catch (READalcException exc)
                {
                    throw exc;
                }
                catch (Exception exc)
                {
                    //Escribir error
                    SMLog.Escribir(Severidad.Critico, "PDFSolicitudREA :: ConsultarRegistroPDF -> Error Inesperado: " + exc.Message + " " + exc.StackTrace);

                    //Escalar error
                    throw new REAException("PDFSolicitudREA :: ConsultarRegistroPDF -> Error Inesperado: " + exc.Message, exc.InnerException);
                }
            }

        #endregion
    }
}

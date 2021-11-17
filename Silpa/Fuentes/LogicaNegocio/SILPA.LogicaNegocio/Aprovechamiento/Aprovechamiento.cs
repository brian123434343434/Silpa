using SILPA.AccesoDatos.Aprovechamiento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SILPA.LogicaNegocio.Aprovechamiento
{
    public class Aprovechamiento
    {
        private AprovechamientoDalc vAprovechamientoDalc;
        private EspecieAprovechamientoDalc vEspecieAprovechamientoDalc;
        private TipoAprovechamientoDalc vTipoAprovechamientoDalc;
        private CoordenadaAprovechamientoDalc vCoordenadaAprovechamientoDalc;
        private UbicacionArbolAisladoDalc vUbicacionArbolAisladoDalc;
        private TratamientoSilviculturaDalc vTratamientoSilviculturaDalc;
        public Aprovechamiento()
        {
            vAprovechamientoDalc = new AprovechamientoDalc();
            vEspecieAprovechamientoDalc = new EspecieAprovechamientoDalc();
            vTipoAprovechamientoDalc = new TipoAprovechamientoDalc();
            vCoordenadaAprovechamientoDalc = new CoordenadaAprovechamientoDalc();
            //jmartinez salvoconducto fase 2
            vUbicacionArbolAisladoDalc = new UbicacionArbolAisladoDalc();
            vTratamientoSilviculturaDalc = new TratamientoSilviculturaDalc();
        }



        public bool ValidarActoAdministrativo(string str_ActoAdministrativo, int intClaseRecursoID, int int_TipoAprovechamientoID, int int_autID, DateTime FecExp)
        {
            return vAprovechamientoDalc.ValidarActoAdministrativo(str_ActoAdministrativo, intClaseRecursoID, int_TipoAprovechamientoID, int_autID, FecExp);
        }

        /// <summary>
        /// Crea un registro de aprovechamiento
        /// </summary>
        /// <param name="vAprovechamientoIdentity">Objeto Aprovechamient</param>
        /// <returns>ID Aprovechamiento Creado</returns>
        public string CrearAprovechamiento(ref AprovechamientoIdentity vAprovechamientoIdentity)
        {
            try
            {
                string NumeroAprovechamiento = vAprovechamientoDalc.CrearAprovechamiento(ref vAprovechamientoIdentity);
                vEspecieAprovechamientoDalc.EliminarEspeciesAprovechamiento(vAprovechamientoIdentity.AprovechamientoID);
                if (vAprovechamientoIdentity.LstEspecies != null)
                foreach (EspecieAprovechamientoIdentity especie in vAprovechamientoIdentity.LstEspecies)
                {
                    especie.AprovechamientoID = vAprovechamientoIdentity.AprovechamientoID;
                    if (vAprovechamientoIdentity.ModoAdquisicionRecursoID == 8 && vAprovechamientoIdentity.CodigoUbicacionArbolAislado == 2)
                    {
                        especie.AlturaComercial = 0;
                        especie.DiametroAlturaPecho = 0;
                        especie.TratamientoSilviculturaID = 0;
                    }
                    
                    vEspecieAprovechamientoDalc.CrearEspecieAprovechamiento(especie);
                }
                if (vAprovechamientoIdentity.LstCoordenadas != null)
                foreach (CoordenadaAprovechamientoIndentity coordenada in vAprovechamientoIdentity.LstCoordenadas)
                {
                    coordenada.AprovechamientoID = vAprovechamientoIdentity.AprovechamientoID;
                    vCoordenadaAprovechamientoDalc.CrearCoordenadaAprovechamiento(coordenada);
                }
                return NumeroAprovechamiento;
            }
            catch (Exception ex)
            {
                throw new Exception("Se genero el siguiente error al intentar crear el Aprovechamiento: " + ex.Message);
            }
        }
        public void ActualizarRutaArchivoSaldoAprovechamiento(int aprovechamientoID, string rutaArchivo)
        {
            try 
	        {	        
		    vAprovechamientoDalc.ActualizarRutaArchivoSaldoAprovechamiento(aprovechamientoID,rutaArchivo);
	        }
	        catch (Exception ex)
	        {
		        throw new Exception("Se genero el siguiente error al intentar actualizar la ruta del archivo soporte de saldo del aprovechamiento: " + ex.Message);
	        }
            
        }
        public List<EspecieAprovechamientoIdentity> ListaRecursosAprovechamiento(int pAprovechamientoId)
        {
            return vEspecieAprovechamientoDalc.ListaRecursosAprovechamiento(pAprovechamientoId);
        }
        public List<AprovechamientoIdentity> ConsultaAprovechamientoAutoridadSolicitante(int? pAutoridadID, int pUsuarioId, int pClaseRecursoID)
        {
            return vAprovechamientoDalc.ConsultaAprovechamientoAutoridadSolicitante(pAutoridadID, pUsuarioId, pClaseRecursoID);
        }
        

        /// <summary>
        /// Retorna el listado de ubicacion arbol aislado
        /// </summary>
        /// <returns></returns>
        /// Jmartinez Salvoconducto Fase 2
        public List<UbicacionArbolAisladoIdentity> ListarUbicacionArbolAislado()
        {
            return vUbicacionArbolAisladoDalc.ListarUbicacionArbolAislado();
        }


        public List<TratamientoSilviculturaIdentity> ListarTratamientoSilvicultura()
        {
            return vTratamientoSilviculturaDalc.ListarTratamientoSilvicultura();
        }


        public List<TipoAprovechamientoIdentity> ListaTipoAprovechamiento()
        {
            return vTipoAprovechamientoDalc.ListaTipoAprovechamiento();
        }
        public void EliminarCargueAprovechamiento(int AprovechamientoID)
        {
            vAprovechamientoDalc.EliminarCargueAprovechamiento(AprovechamientoID);
        }
        public AprovechamientoIdentity ConsultaAprovechamientoXAprovechamientoId(int pAprovechamientoId)
        {
            return vAprovechamientoDalc.ConsultaAprovechamientoXAprovechamientoId(pAprovechamientoId);
        }
        public DataTable ConsultaAprovechamientosCargados(int? autoridadID, string strNumeroActo, int? intSolicitanteId, DateTime? fechaDesde, DateTime? fechaHasta, int? intClaseRecursoID, int? intFormaOtorgamientoID, int? intModoAdquisicionID, int? intDepartametnoProID, int? intMunicipipoProID, string strNombrePredio)
        {
            return vAprovechamientoDalc.ConsultaAprovechamientosCargados(autoridadID, strNumeroActo, intSolicitanteId,fechaDesde,fechaHasta,intClaseRecursoID,intFormaOtorgamientoID,intModoAdquisicionID,intDepartametnoProID,intMunicipipoProID,strNombrePredio);
        }

        public DataTable ConsultaDocumentosCargados(int? autoridadID, string strNumeroActo, int? intSolicitanteId, DateTime? fechaDesde, DateTime? fechaHasta, int? intClaseRecursoID, int? intFormaOtorgamientoID, int? intModoAdquisicionID, int? intDepartametnoProID, int? intMunicipipoProID, string strNombrePredio, int TipoAprovechamientoID)
        {
            return vAprovechamientoDalc.ConsultaDocumentosCargados(autoridadID, strNumeroActo, intSolicitanteId, fechaDesde, fechaHasta, intClaseRecursoID, intFormaOtorgamientoID, intModoAdquisicionID, intDepartametnoProID, intMunicipipoProID, strNombrePredio, TipoAprovechamientoID);
        }

        public DataTable ConsultaDocumentosCargadosFullInfo(int? autoridadID, string strNumeroActo, int? intSolicitanteId, DateTime? fechaDesde, DateTime? fechaHasta, int? intClaseRecursoID, int? intFormaOtorgamientoID, int? intModoAdquisicionID, int? intDepartametnoProID, int? intMunicipipoProID, string strNombrePredio, int TipoAprovechamientoID)
        {
            return vAprovechamientoDalc.ConsultaDocumentosCargadosFullInfo(autoridadID, strNumeroActo, intSolicitanteId, fechaDesde, fechaHasta, intClaseRecursoID, intFormaOtorgamientoID, intModoAdquisicionID, intDepartametnoProID, intMunicipipoProID, strNombrePredio, TipoAprovechamientoID);
        }

        public DataTable ConsultaAprovechamientosCargadosFullInfo(int? autoridadID, string strNumeroActo, int? intSolicitanteId, DateTime? fechaDesde, DateTime? fechaHasta, int? intClaseRecursoID, int? intFormaOtorgamientoID, int? intModoAdquisicionID, int? intDepartametnoProID, int? intMunicipipoProID, string strNombrePredio )
        {
            return vAprovechamientoDalc.ConsultaAprovechamientosCargadosFullInfo(autoridadID, strNumeroActo, intSolicitanteId, fechaDesde, fechaHasta, intClaseRecursoID, intFormaOtorgamientoID, intModoAdquisicionID, intDepartametnoProID, intMunicipipoProID, strNombrePredio);
        }
    }
}

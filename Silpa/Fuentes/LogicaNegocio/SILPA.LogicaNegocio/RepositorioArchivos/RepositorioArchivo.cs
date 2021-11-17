using SILPA.AccesoDatos.RepositorioArchivos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.BPMProcess;

namespace SILPA.LogicaNegocio.RepositorioArchivos
{
    public class RepositorioArchivo
    {
        public ArchivoDalc vArchivoDalc;
        public RepositorioArchivo()
        {
            vArchivoDalc = new ArchivoDalc();
        }
        public void InsertarArchivo(AccesoDatos.RepositorioArchivos.Archivo pArchivo)
        {
            vArchivoDalc.InsertarArchivo(pArchivo);
        }
        public void EliminarArchivo(AccesoDatos.RepositorioArchivos.Archivo pArchivo)
        {
            vArchivoDalc.EliminarArchivo(pArchivo);
        }
        public List<Archivo> ConsultaArchivos(string nombreArchivo, int? fileID, int? usuarioID)
        {
            return vArchivoDalc.ConsultaArchivos(nombreArchivo, fileID, usuarioID);
        }
        public void AsociarArchivo(string nombreArchivo, int? usuarioID)
        {
            vArchivoDalc.AsociarArchivo(nombreArchivo, usuarioID);
        }
        public DataTable TablaFormularios()
        {
            return vArchivoDalc.TablaFormularios();
        }
        public DataTable TablaTipoArchivoFormulario(int formularioID)
        {
            DataTable dttTipoArchivo = new DataTable();
            dttTipoArchivo.Columns.Add("TIPO");
            dttTipoArchivo.Columns.Add("ID");
            ProcessDalc objProcess = new ProcessDalc();
            List<CampoIdentity> arrayCampos = objProcess.ObtenerCampos(formularioID);

            foreach (CampoIdentity dtrCampo in arrayCampos)
            {
                RadicacionDocumento clsRadicacionDocumento = new RadicacionDocumento();
                if (clsRadicacionDocumento.EsEtiquetaRadicable(dtrCampo.Nombre) && dtrCampo.Tipo == "DropDownList")
                {
                    DataRow dtrTipoArchivo = dttTipoArchivo.NewRow();
                    dtrTipoArchivo["ID"] = dtrCampo.Id;
                    dtrTipoArchivo["TIPO"] = dtrCampo.Nombre.Replace("Adjuntar", "").Trim();
                    dttTipoArchivo.Rows.Add(dtrTipoArchivo);
                }
            }
            return dttTipoArchivo;
        }
    }
}

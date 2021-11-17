using System;
using System.Collections.Generic;
using System.Text;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Sancionatorio;
using SILPA.Comun;
using System.Data;
using System.Collections;
using SILPA.AccesoDatos.Generico;
using SILPA.LogicaNegocio.BPMProcessL;

namespace SILPA.LogicaNegocio.Sancionatorio
{
    public class Queja
    {
        private QuejaDalc DalcQueja;
        public QuejaIdentity IdentityQueja;
        private PersonaQuejaDalc DalcPersona;
        public PersonaQuejaIdentity IdentityPersona;
        public SancionatorioIdentity IdentitySan;
        

        /// <summary>
        /// Objeto de configuración del sitio, con las  variables globales
        /// </summary>
        private Configuracion objConfiguracion;

        public Queja()
        {
            objConfiguracion = new Configuracion();
            DalcQueja = new QuejaDalc();
            IdentityQueja = new QuejaIdentity();
            DalcPersona = new PersonaQuejaDalc();
            IdentityPersona = new PersonaQuejaIdentity();
            IdentitySan = new SancionatorioIdentity();
        }

        /// <summary>
        /// Método que envia los datos de una queja para su almacenamiento
        /// </summary>
        /// <param name="_numeroSilpa">Número SILPA</param>
        /// <param name="_descripcionQueja">Descripcion de la queja</param>
        /// <param name="_idMunicipio">Identificador del municipio de la queja</param>
        /// <param name="_idUbicacion">Identificador del tipo de ubicacion de la queja</param>
        /// <param name="_idCorregimiento">Identificador del corregimiento de la queja</param>
        /// <param name="_idVereda">Identificador de la vereda de la queja</param>
        /// <param name="_idAreaHidrografica">Identificador del area hidrografica de la queja</param>
        /// <param name="_idZonaHidrografica">Identificador de la zona hidrograficade la queja</param>
        /// <param name="_idSubZonaHidrografica">Identificador de la subzona hidrografica de la queja</param>
        /// <param name="_idAutoridadAmbiental">Identificador de la autoridad ambiental de la queja</param>
        /// <param name="_idSector">Identificador del sector de la queja</param>
        /// <returns>identificador de la queja queja almacenada</returns>
        public Int64 InsertarQueja(string _numeroSilpa, string _descripcionQueja, int _idMunicipio,
            Nullable<int> _idUbicacion, Nullable<int> _idCorregimiento, Nullable<int> _idVereda,
            Nullable<int> _idAreaHidrografica, Nullable<int> _idZonaHidrografica,
            Nullable<int> _idSubZonaHidrografica, Nullable<int> _idAutoridadAmbiental)
        {
            try
            {                
                this.IdentityQueja.DescripcionQueja = _descripcionQueja;
                this.IdentityQueja.IdMunicipio = _idMunicipio;
                this.IdentityQueja.IdUbicacion = _idUbicacion;
                this.IdentityQueja.IdCorregimiento = _idCorregimiento;
                this.IdentityQueja.IdVereda = _idVereda;
                this.IdentityQueja.IdAreaHidrografica = _idAreaHidrografica;
                this.IdentityQueja.IdZonaHidrografica = _idZonaHidrografica;
                this.IdentityQueja.IdSubZonaHidrografica = _idSubZonaHidrografica;
                this.IdentityQueja.IdAutoridadAmbiental = _idAutoridadAmbiental;
                //this.IdentityQueja.IdSector = _idSector;
                DalcQueja.InsertarQueja(ref this.IdentityQueja);
                return this.IdentityQueja.IdQueja;
            }
            catch (Exception e)
            {
                throw new Exception("Mensaje de error al insertar la queja: " + e.Message);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="NumeroSilpa"></param>
        /// <param name="primerNombre"></param>
        /// <param name="segundoNombre"></param>
        /// <param name="primerApellido"></param>
        /// <param name="segundoApellido"></param>
        /// <param name="mailQuejoso"></param>
        /// <returns></returns>
        public void InsertarQuejas(
            string NumeroSilpa, string primerNombre, 
            string segundoNombre, string primerApellido, string segundoApellido,string mailQuejoso) 
        {
            DalcQueja.InsertarQueja(NumeroSilpa, primerNombre, segundoNombre, primerApellido, segundoApellido,mailQuejoso);
        }

        public string RetornarNumeroSilpa()
        {
            return this.IdentityQueja.NumeroSilpa;
        }

        /// <summary>
        /// Retorna el número vital copleto
        /// </summary>
        /// <returns></returns>
        public string RetornarNumeroVital()
        {
            return this.IdentityQueja.NumeroVital;
        }

        /// <summary>
        /// Método que envia los datos de una persona enlazada a una queja
        /// </summary>
        /// <param name="_idQueja">Identificador de la queja</param>
        /// <param name="_idTipoPersona">Identificador del tipo de persona</param>
        /// <param name="_primerNombre">Primer nombre de la persona</param>
        /// <param name="_segundoNombre">Segundo nombre de la persona</param>
        /// <param name="_primerApellido">Primer apellido de la persona</param>
        /// <param name="_segundoApellido">Segundo apellido de la persona</param>
        /// <param name="_idTipoIdentificacion">Identificador del tipo de identificacion</param>
        /// <param name="_numeroIdentificacion">Numero de identificacion de la persona</param>
        /// <param name="_idMunicipioOrigen">Identificador municipio de origen del documento</param>
        /// <param name="_direccion">Dirección de la persona</param>
        /// <param name="_idMunicipio">identificador del municipio</param>
        /// <param name="_idCorregimiento">Identificador del corregimiento</param>
        /// <param name="_idVereda">Identificador de la vereda</param>
        /// <param name="_telefono">Telefono de la persona</param>
        /// <param name="_correoElectronico">Correo electronico de la persona</param>
        public void InsertarPersonaQueja(Int64 _idQueja, int _idTipoPersona, string _primerNombre,
            string _segundoNombre, string _primerApellido, string _segundoApellido,
            Nullable<int> _idTipoIdentificacion, string _numeroIdentificacion, Nullable<int> _idMunicipioOrigen,
            string _direccion, Nullable<int> _idMunicipio, Nullable<int> _idCorregimiento,
            Nullable<int> _idVereda, string _telefono, string _correoElectronico)
        {
            try
            {
                this.IdentityPersona.IdQueja = _idQueja;
                this.IdentityPersona.IdTipoPersona = _idTipoPersona;
                this.IdentityPersona.PrimerNombre = _primerNombre;
                this.IdentityPersona.SegundoNombre = _segundoNombre;
                this.IdentityPersona.PrimerApellido = _primerApellido;
                this.IdentityPersona.SegundoApellido = _segundoApellido;
                this.IdentityPersona.IdTipoIdentificacion = _idTipoIdentificacion;
                this.IdentityPersona.NumeroIdentificacion = _numeroIdentificacion;
                this.IdentityPersona.IdMunicipioOrigen = _idMunicipioOrigen;
                this.IdentityPersona.Direccion = _direccion;
                this.IdentityPersona.IdMunicipio = _idMunicipio;
                this.IdentityPersona.IdCorregimiento = _idCorregimiento;
                this.IdentityPersona.IdVereda = _idVereda;
                this.IdentityPersona.Telefono = _telefono;
                this.IdentityPersona.CorreoElectronico = _correoElectronico;
                DalcPersona.InsertarPersonaQueja(ref this.IdentityPersona);
            }
            catch (Exception e)
            {
                throw new Exception("Mensaje de error al insertar la persona: " + e.Message);
            }
        }

        /// <summary>
        /// Método que envia los datos de los recursos de una queja
        /// </summary>
        /// <param name="_idQueja">Identificador de la queja</param>
        /// <param name="_idRecurso">Identificador del recurso</param>
        /// <param name="_otroRecurso">Otro recurso</param>
        public void InsertarQuejaRecurso(Int64 _idQueja, int _idRecurso, string _otroRecurso)
        {            
            try
            {
                QuejaRecursoIdentity objIdentity = new QuejaRecursoIdentity();
                objIdentity.IdQueja = _idQueja;
                objIdentity.IdRecurso = _idRecurso;
                objIdentity.OtroRecurso = _otroRecurso;
                DalcQueja.InsertarQuejaRecurso(ref objIdentity);                
            }
            catch (Exception e)
            {
                throw new Exception("Mensaje de error al insertar el recurso de la queja: " + e.Message);
            }
        }


        public void InsertarCoordenada(Int64 _idQueja, string _coordenadaX, string _coordenadaY)
        {
            try
            {
                CoordenadaIdentity objIdentity = new CoordenadaIdentity();
                objIdentity.IdQueja=_idQueja;
                objIdentity.CoordenadaX=_coordenadaX;
                objIdentity.CoordenadaY = _coordenadaY;
                DalcQueja.InsertarCoordenada(ref objIdentity);
            }
            catch (Exception e)
            {
                throw new Exception("Mensaje de error al insertar la coordenada de la queja: " + e.Message);
            }
        }


        public void EnviarCorreoQueja(string quejaXML)
        {
            IdentitySan = new SancionatorioIdentity();
            IdentitySan = (SancionatorioIdentity)IdentitySan.Deserializar(quejaXML);
            PersonaDalc _objPersonaDalc = new PersonaDalc();
            List<PersonaIdentity> _listaPersona = new List<PersonaIdentity>();

            _listaPersona = _objPersonaDalc.BuscarPersonaNumeroVITAL(IdentitySan.NumeroSilpa);
            //Se envia el correo por cada persona o solicitante
            foreach (PersonaIdentity _objPersona in _listaPersona)
                ICorreo.Correo.EnviarCorreoSancionatorio(IdentitySan, _objPersona);
            
        }

        public DataSet ObtenerUsuarioQueja()
        {
            DataSet ds_usuario = new DataSet();
            ds_usuario=DalcQueja.obtenerUsuarioQueja();
            return ds_usuario;
        }



        public DataSet ObtenerUsuarioAudiencia()
        {
            DataSet ds_usuario = new DataSet();
            ds_usuario = DalcQueja.obtenerUsuarioAudiencia();
            return ds_usuario;
        }

        public DataSet ObtenerUsuarioQueja(string idUsuario)
        {
            DataSet ds_usuario = new DataSet();
            ds_usuario = DalcQueja.obtenerUsuarioQueja(idUsuario);
            return ds_usuario;
        }

        public DataTable ListarQuejasXNumSILPA(string strNumSILPA)
        {
            return DalcQueja.ListarQuejasXNumSILPA(strNumSILPA).Tables[0];
        }



        public void ActualizarNumeroVitalQueja(Int64 IdQueja, string numeroVital)
        {
            this.DalcQueja.ActualizarNumeroVital(IdQueja, numeroVital);
        }


        public string CrearProcesoQueja(string ClientId, Int64 FormularioQueja, Int64 UsuarioQueja, string ValoresXml)
        {
            BpmProcessLn objProceso = new BpmProcessLn();
            return objProceso.crearProceso(ClientId, FormularioQueja, UsuarioQueja, ValoresXml);            
        }
        public string CrearProcesoQueja(string ClientId, Int64 FormularioQueja, Int64 UsuarioQueja, string ValoresXml,string remitente)
        {
            BpmProcessLn objProceso = new BpmProcessLn();
            string numero_silpa = objProceso.crearProceso(ClientId, FormularioQueja, UsuarioQueja, ValoresXml);
            this.DalcQueja.ActualizarRemitente(numero_silpa,remitente);
            return numero_silpa;
        }

        public DataSet ObtenerUsuarioRecurso(string idUsuario)
        {
            DataSet ds_usuario = new DataSet();
            ds_usuario = DalcQueja.obtenerUsuarioRecurso(idUsuario);
            return ds_usuario;
        }
    }
}

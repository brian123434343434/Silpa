using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using SILPA.AccesoDatos;
using SILPA.AccesoDatos.Salvoconducto;
using System.Data;
using SILPA.LogicaNegocio.Generico;
using SILPA.AccesoDatos.Generico;
using SILPA.AccesoDatos.RecursoIdentity;


namespace SILPA.LogicaNegocio.Salvoconducto
{
    public class Salvoconducto
    {
        public SalvoconductoIdentity SalvoIdentity;
        private SalvoconductoDalc SalvoDalc;
        private EspecimenDalc EspDalc;
        private EspecimenIdentity EspIdentity;

        public RecursoIdentity RecurIdentity;
        public RecursosIdentity RecursIdentity;

        /// <summary>
        /// objeto de configuracion
        /// </summary>
        private Configuracion _objConfiguracion;

        /// <summary>
        /// Constructor
        /// </summary>
        public Salvoconducto()
        {
            _objConfiguracion = new Configuracion();
            SalvoIdentity = new SalvoconductoIdentity();
            SalvoDalc = new SalvoconductoDalc();
            EspDalc = new EspecimenDalc();
            EspIdentity = new EspecimenIdentity();
            RecurIdentity = new RecursoIdentity();
            RecursIdentity = new RecursosIdentity();
        }


        /// <summary>
        /// Inserta la Información de Salvodonducto entregada por un Servicio de la AA
        /// </summary>
        /// <param name="strXmlDatos">Datos en XML</param>
        public string InsertarSalvoconducto(string strXmlDatos)
        {
            SalvoIdentity = (SalvoconductoIdentity)SalvoIdentity.Deserializar(strXmlDatos);

            this.SalvoDalc.InsertarSalvoconducto(ref SalvoIdentity);

            RelacionarSalvoconductoEspecimen();
            RelacionarSalvoconductoTransporte();

            //Se consaulta la informacion de salvoconducto para obtener nombres relacionados
            this.SalvoDalc.ConsultarSalvoconducto(ref SalvoIdentity);


            PersonaDalc _objPersonaDalc = new PersonaDalc();
            //PersonaIdentity _objPersona = new PersonaIdentity();
            List<PersonaIdentity> _listaPersona = new List<PersonaIdentity>();

            _listaPersona = _objPersonaDalc.BuscarPersonaNumeroVITAL(SalvoIdentity.NumeroSilpa);
            //Se envia el correo por cada persona o solicitante
            foreach (PersonaIdentity _objPersona in _listaPersona)
                ICorreo.Correo.EnviarCorreoSalvoconducto(SalvoIdentity, _objPersona);
            return SalvoIdentity.NumeroSilpa;
            
        }

        private void RelacionarSalvoconductoTransporte()
        {
            for (int i = 0; i < SalvoIdentity.transporte.Length; i++)
            {
                TransporteIdentity tra = (TransporteIdentity)SalvoIdentity.transporte[i];

                tra.idSalvoconducto = this.SalvoIdentity.idSalvoconducto;

                this.EspDalc.InsertarTransporte(ref tra);

            }
        }


        private void RelacionarSalvoconductoEspecimen()
        {
            for (int i = 0; i < SalvoIdentity.especimen.Length; i++ )
            {
                EspecimenIdentity esp = (EspecimenIdentity)SalvoIdentity.especimen[i];

                esp.idSalvoconducto = this.SalvoIdentity.idSalvoconducto;

                this.EspDalc.InsertarEspecimen(ref esp);

            }
        }


        public string InsertarRecurso(string datosRecursoXML)
        {
            XmlSerializador _ser = new XmlSerializador();
            RecurIdentity = (RecursoIdentity)_ser.Deserializar(new RecursoIdentity(), datosRecursoXML);

            for (int i = 0; i < RecurIdentity.recursos.Length; i++)
            {
                RecursosIdentity rec = (RecursosIdentity)RecurIdentity.recursos[i];

                this.EspDalc.InsertarRecurso(RecurIdentity.CodigoExpediente, RecurIdentity.NumeroVital, ref  rec);

            }
            return "";
        }
    }
}

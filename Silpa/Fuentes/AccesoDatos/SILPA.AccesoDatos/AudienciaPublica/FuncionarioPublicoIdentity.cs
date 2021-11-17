using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.AudienciaPublica
{
  public class FuncionarioPublicoIdentity
    {
    /// <summary>
        /// Constructor sin parametros
        /// </summary>
        public FuncionarioPublicoIdentity() { }

        /// <summary>
        /// Constructor con parametros
        /// </summary>
        /// </summary>
        /// <param name="intCodigoTipoPersona">int: identificador del funcionario público</param>
        /// <param name="strNombreTipoPersona">string: nombre del funcionario público</param>
        /// <param name="strExtensionFuncionario">string: extension del nombre del funcionario público</param>
      public FuncionarioPublicoIdentity
        (
            int intCodigoFuncionario, 
            string strNombreFuncionario,
           string strExtensionFuncionario
        )
        {
            this._codigoFuncionario = intCodigoFuncionario;
            this._nombreFuncionario = strNombreFuncionario;
            this._extensionFuncionario = strExtensionFuncionario;
        }


        #region Declaracion de campos...
        
        /// <summary>
        /// codigo del funcionario público
        /// </summary>
        private int _codigoFuncionario;

        /// <summary>
        /// Nombre del funcionario público
        /// </summary>
        private string _nombreFuncionario;

        /// <summary>
        /// Extension del nombre del funcionario público
        /// </summary>
        private string _extensionFuncionario;

        #endregion

        #region Declaracion de las propiedades ... 
        public int CodigoFuncionario
        {
            get { return this._codigoFuncionario; }
            set { this._codigoFuncionario = value; }
        }

        public string NombreFuncionario
        {
            get { return this._nombreFuncionario; }
            set { this._nombreFuncionario = value; }
        }
      public string ExtensionFuncionario
      {
          get { return this._extensionFuncionario; }
          set { this._extensionFuncionario = value; }
      }   
        #endregion

    }
}


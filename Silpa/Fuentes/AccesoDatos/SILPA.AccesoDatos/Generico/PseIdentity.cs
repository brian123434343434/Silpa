using System;
using System.Collections.Generic;
using System.Text;
using SILPA.Comun;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;

namespace SILPA.AccesoDatos.Generico
{
    public class PseIdentity
    {
        #region Atributos
        int _id;
        int _id_aut;
        string _certificate_sub;
        string _url;
        string _code;
        string _razon_social;
        #endregion

        #region Constructores

        public PseIdentity()
        {
        }

        public PseIdentity(int intId, int intIdAut, string strCertificateSub, string strUrl, 
            string strCode, string strRazonSocial)
        {
            this._id = intId;
            this._id_aut = intIdAut;
            this._certificate_sub = strCertificateSub;
            this._url = strUrl;
            this._code = strCode;
            this._razon_social = strRazonSocial;
        }

        #endregion

        #region Propiedades
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public int Id_aut
        {
            get { return _id_aut; }
            set { _id_aut = value; }
        }
        public string Certificate_sub
        {
            get { return _certificate_sub; }
            set { _certificate_sub = value; }
        }
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }
        public string Razon_social
        {
            get { return _razon_social; }
            set { _razon_social = value; }
        }
        #endregion
    }
}

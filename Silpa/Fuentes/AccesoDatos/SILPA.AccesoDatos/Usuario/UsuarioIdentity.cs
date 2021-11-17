using System;
using System.Collections.Generic;
using System.Text;

namespace SILPA.AccesoDatos.Usuario
{
    public class UsuarioIdentity
    {
        private int _id;
        private string _cod;
        private string _primerNombre;
        private string _password;
        private string _middleName;
        private string _edited;
        private string _lastName;
        private DateTime _lastLogIn;
        private DateTime _lastLogOut;
        private int _idLocality;
        private string _identificationUser;
        private string _positionNameUser;
        private string _eMailUser;
        private int _expiration;
        private string _enabledUser;
        private string _activeUser;
        private string _image;
        private string _changePass;


        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Cod
        {
            get { return _cod; }
            set { _cod = value; }
        }

        public string PrimerNombre
        {
            get { return _primerNombre; }
            set { _primerNombre = value; }
        }

        public string MiddleName
        {
            get { return _middleName; }
            set { _middleName = value; }
        }

        public string LastName1
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Edited
        {
            get { return _edited; }
            set { _edited = value; }
        }

        public DateTime LastLogIn
        {
            get { return _lastLogIn; }
            set { _lastLogIn = value; }
        }

        public DateTime LastLogOut
        {
            get { return _lastLogOut; }
            set { _lastLogOut = value; }
        }

        public int IdLocality
        {
            get { return _idLocality; }
            set { _idLocality = value; }
        }

        public string IdentificationUser
        {
            get { return _identificationUser; }
            set { _identificationUser = value; }
        }

        public string PositionNameUser
        {
            get { return _positionNameUser; }
            set { _positionNameUser = value; }
        }

        public string EMailUser
        {
            get { return _eMailUser; }
            set { _eMailUser = value; }
        }

        public int Expiration
        {
            get { return _expiration; }
            set { _expiration = value; }
        }

        public string EnabledUser
        {
            get { return _enabledUser; }
            set { _enabledUser = value; }
        }

        public string ActiveUser
        {
            get { return _activeUser; }
            set { _activeUser = value; }
        }

        public string Image
        {
            get { return _image; }
            set { _image = value; }
        }

        public string ChangePass
        {
            get { return _changePass; }
            set { _changePass = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

    }
}

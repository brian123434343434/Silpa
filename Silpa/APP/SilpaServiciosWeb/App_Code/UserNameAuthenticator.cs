using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Security.Principal;
using System.IO;
using SILPA.Comun;

namespace Module
{
    public class UserNameAuthenticator : IHttpModule
    {
        private string mensaje001;
        private string mensaje002;

        public void Dispose()
        {
        }

        public void Init(HttpApplication application)
        {
            application.AuthenticateRequest += new EventHandler(this.OnAuthenticateRequest);
            application.EndRequest += new EventHandler(this.OnEndRequest);
        }

        public void OnAuthenticateRequest(object source, EventArgs eventArgs)
        {
            mensaje001 = System.Configuration.ConfigurationManager.AppSettings["WSMessageSecurity_001"].ToString();
            mensaje002 = System.Configuration.ConfigurationManager.AppSettings["WSMessageSecurity_002"].ToString();
            if (System.Configuration.ConfigurationManager.AppSettings.Get("AplicaSeguridad").Equals("N") == true)
            {
                return; 
            }
            HttpApplication app = (HttpApplication)source;

            //the Authorization header is checked if present
            string authHeader = app.Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authHeader))
            {
                string authStr = app.Request.Headers["Authorization"];

               if (authStr == null || authStr.Length == 0)
                    return;

                authStr = authStr.Trim();
                if (authStr.IndexOf("Basic", 0) != 0)
                    return;

                authStr = authStr.Trim();
                string encodedCredentials = authStr.Substring(6);
                byte[] decodedBytes = Convert.FromBase64String(encodedCredentials);
                string s = new ASCIIEncoding().GetString(decodedBytes);

                string[] userPass = s.Split(new char[] { ':' });
                string username = userPass[0];
                string password = userPass[1];
                string passwordCripto = string.Empty;

                //the user is validated against the SqlMemberShipProvider
                //If it is validated then the roles are retrieved from the
                //role provider and a generic principal is created
                //the generic principal is assigned to the user context
                // of the application


                ///Debe realizar la opcion de validacion de los datos del sistema
                SILPA.LogicaNegocio.GrupoYUsuarios.Credenciales cred = new SILPA.LogicaNegocio.GrupoYUsuarios.Credenciales();
                SILPA.AccesoDatos.GrupoYUsuarios.CredencialesEntity credE = new SILPA.AccesoDatos.GrupoYUsuarios.CredencialesEntity();

                //Se obtiene el password de la credencia existente en la base de datos.
                credE = cred.ConsultarCredencial(username,"S");

                if (credE!=null)
                {
                    if (!String.IsNullOrEmpty(credE.Password))
                    {
                        //Se desencripta el password recuperado de la base de datos
                        passwordCripto = SILPA.Comun.EnDecript.Desencriptar(credE.Password);
                    }

                    // Validación de la existencia de la AA y de que el password corresponda
                    //if (credE.Autoridad != null && passwordCripto == password)
                    if (passwordCripto == password && !string.IsNullOrEmpty(passwordCripto))
                    {
                        //return;
                        string[] roles = { "Role Autenticado" };
                        app.Context.User = new GenericPrincipal(new GenericIdentity(username, "Proveedor de Prueba"), roles);
                        //EscribirArchivo(username);
                    }
                    else
                    {
                        DenyAccess(app);
                        return;
                    }
                }
                else
                {
                    DenyAccess(app);
                    return;
                }
            }
            else
            {
                //the authorization header is not present
                //the status of response is set to 401 and it ended
                //the end request will check if it is 401 and add
                //the authentication header so the client knows
                //it needs to send credentials to authenticate

                app.Response.StatusCode = 401;
                //app.Response.StatusDescription = "La Autenticacion no es la aprobada para la aplicacion, por favor pongase en contacto con el administrador";
                app.Response.StatusDescription = this.mensaje001;
                app.Response.Write("401 - " + this.mensaje001);
                app.Response.End();
            }
        }  //End class function

        public void OnEndRequest(object source, EventArgs eventArgs)
        {
            if (HttpContext.Current.Response.StatusCode == 401)
            {

                //if the status is 401 the WWW-Authenticated is added to 
                //the response so client knows it needs to send credentials 

                HttpContext context = HttpContext.Current;
                context.Response.StatusCode = 401;
                context.Response.AddHeader("WWW-Authenticate", "Basic Realm");
            }
        }

        private void DenyAccess(HttpApplication app)
        {
            app.Response.StatusCode = 401;
            //app.Response.StatusDescription = "Las credenciales no son las Correctas por favor intente mas tarde y comuniquese con el Administrador de la Aplicacion de Autoridad ambiental";
            app.Response.StatusDescription = this.mensaje002;
            app.Response.Write("401 - " + this.mensaje002);
            app.CompleteRequest();
        }

        public static void EscribirArchivo(string mensaje)
        {
            using (StreamWriter arc = new StreamWriter(@"D:\TEMP\LogOn.txt", true))
            {
                arc.WriteLine(DateTime.Now.ToString() + "; Logueo Utilizado... " + mensaje);
            }
        }
    }
}

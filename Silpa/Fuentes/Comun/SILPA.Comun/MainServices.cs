using System;
using System.Web.Services.Protocols;
using System.Security.Cryptography;
using Microsoft.Web.Services;
using Microsoft.Web.Services.Security;
using Microsoft.Web.Services.Security.X509;

namespace SILPA.Comun
{
    /// <summary>
    /// Summary description for trustedCertificatePolicy.
    /// </summary>
    public class trustedCertificatePolicy : System.Net.ICertificatePolicy
    {
        public trustedCertificatePolicy() { }

        public bool CheckValidationResult
            (
            System.Net.ServicePoint sp,
            System.Security.Cryptography.X509Certificates.X509Certificate certificate,
            System.Net.WebRequest request, int problem)
        {
            return true;
        }
    }

    /// <summary>
    /// Summary description for MainServices.
    /// </summary>
    public class MainServices : IDisposable
    {
        public string URL;
        public string CertificateStore = "Root";
        public string CertificateSubject;
        private MainServicesProxy proxy = null;

        public MainServices()
        {
        }

        public void Open(string source, string target, string representingParty)
        {
            proxy = new MainServicesProxy();
            // Para permitir SSL

            System.Net.ServicePointManager.CertificatePolicy = new trustedCertificatePolicy();

            // Configura Proxy

            ConfigureProxy(URL, proxy);

            SoapContext requestContext = proxy.RequestSoapContext;
            X509SecurityToken token = GetSecurityToken();
            requestContext.Security.Tokens.Add(token);
            requestContext.Security.Elements.Add(new Signature(token));

            requestContext.Timestamp.Ttl = 0;

            BuildHeader(source, target, representingParty);
        }

        private void BuildHeader(string source, string target, string representingParty)
        {
            if (proxy.messageHeader == null)
                proxy.messageHeader = new MessageHeaderType();
            if (proxy.messageHeader.to == null)
                proxy.messageHeader.to = new PartyIdentificationType();
            if (proxy.messageHeader.from == null)
                proxy.messageHeader.from = new PartyIdentificationType();
            if (proxy.messageHeader.representingParty == null)
                proxy.messageHeader.representingParty = new PartyIdentificationType();

            proxy.messageHeader.to.Item = source;
            proxy.messageHeader.from.Item = target;
            proxy.messageHeader.representingParty.Item = representingParty;
        }

        public void Dispose()
        {
            if (proxy != null)
                proxy.Dispose();
        }

        public getBankListResponseInformationType[] getBankList(getbankListInformationType getBankListInformation)
        {
            return proxy.getBankList(getBankListInformation);
        }

        public createTransactionPaymentResponseInformationType createTransactionPayment(createTransactionPaymentInformationType createTransactionPaymentInformation)
        {
            return proxy.createTransactionPayment(createTransactionPaymentInformation);
        }

        public confirmTransactionPaymentResponseInformationType confirmTransactionPayment(confirmTransactionPaymentInformationType confirmTransactionPaymentInformation)
        {
            return proxy.confirmTransactionPayment(confirmTransactionPaymentInformation);
        }

        public getTransactionInformationResponseBodyType getTransactionInformation(getTransactionInformationBodyType getTransactionInformationBody)
        {
            return proxy.getTransactionInformation(getTransactionInformationBody);
        }

        public finalizeTransactionPaymentResponseInformationType finalizeTransactionPayment(finalizeTransactionPaymentInformationType finalizeTransactionPaymentInformation)
        {
            return proxy.finalizeTransactionPayment(finalizeTransactionPaymentInformation);
        }

        private void ConfigureProxy(string remoteHost, HttpWebClientProtocol protocol)
        {
            if (protocol is Microsoft.Web.Services.WebServicesClientProtocol)
            {
                ((Microsoft.Web.Services.WebServicesClientProtocol)protocol).Url = remoteHost;
            }
            else
            {
                protocol.Url = remoteHost;
            }

            // Cria cookie container para conter a sessão

            proxy.CookieContainer = new System.Net.CookieContainer();
        }

        private X509SecurityToken GetSecurityToken()
        {
            X509CertificateStore store = X509CertificateStore.LocalMachineStore(CertificateStore);
            X509SecurityToken securityToken = null;

            try
            {
                bool open = store.OpenRead();
                if (open == false)
                    throw new ApplicationException("Cannot open certificate store.");

                Microsoft.Web.Services.Security.X509.X509Certificate cert = null;
                X509CertificateCollection matchingCerts = store.FindCertificateBySubjectName(CertificateSubject);
                if (matchingCerts.Count == 0)
                {
                    throw new ApplicationException("No matching certificates were found for the subject name provided.");
                }
                else
                {
                    cert = matchingCerts[0];
                }

                if (!cert.SupportsDigitalSignature || cert.Key == null)
                {
                    throw new ApplicationException("The certificate must support digital signatures and have a private key available.");
                }
                else
                {
                    securityToken = new X509SecurityToken(cert);
                }
            }
            finally
            {
                if (store != null) { store.Close(); }
            }
            return securityToken;
        }

        public void SaveResponse(string filename)
        {
            string soap_xml = proxy.ResponseSoapContext.Envelope.OuterXml;
            System.IO.StreamWriter writer = System.IO.File.CreateText(filename);
            writer.Write(soap_xml);
            writer.Close();
        }
    }
}

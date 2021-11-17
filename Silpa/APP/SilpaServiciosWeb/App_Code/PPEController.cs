using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PSEWebServicesClient3;

/// <summary>
/// Summary description for PPEController.
/// </summary>
public class PPEController
{
    private static MainServices pse_ws = null;

    public static string GetConfiguration(string key)
    {
        AppSettingsReader reader = new AppSettingsReader();
        return reader.GetValue(key, typeof(String)).ToString();
    }

    public static MainServices GetPSEWebservice()
    {
        if (pse_ws == null)
        {
            pse_ws = new MainServices();
            pse_ws.URL = GetConfiguration("PSE_URL");
            pse_ws.Open(GetConfiguration("PPE_CODE"), GetConfiguration("PSE_CODE"), GetConfiguration("BANK_CODE"));
        }
        return pse_ws;
    }

    public static MainServices GetPSEWebservice(string ppeCode, string ppeCertificateSubject)
    {
        if (pse_ws == null)
        {
            lock (typeof(MainServices))
            {
                if (pse_ws == null)
                {
                    try
                    {
                        pse_ws = new MainServices();
                        pse_ws.URL = GetConfiguration("PSE_URL");
                        //pse_ws.CertificateSubject = ppeCertificateSubject;
                        pse_ws.Open(ppeCode, GetConfiguration("PSE_CODE"), GetConfiguration("BANK_CODE"));
                    }
                    catch (Exception ex)
                    {
                        pse_ws = null;
                        throw ex;
                    }
                }
            }
        }
        return pse_ws;
    }
}


using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class BandejaTareas_WorkItem : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        foreach (string key in Session.Keys)
        {
            this.Label1.Text += String.Format("<br/>{0}={1}", key, Session[key]);
        }

        this.Label1.Text += "<BR/>PARAMS :";
        foreach (string paramKey in Request.Params.Keys)
        {
            this.Label1.Text += String.Format("<br/>{0}={1}", paramKey, Request.Params[paramKey]);

        }

        this.Label1.Text += "<BR/>Form :";
        foreach (string paramKey in Request.Form.Keys)
        {
            this.Label1.Text += String.Format("<br/>{0}={1}", paramKey, Request.Form[paramKey]);

        }

        Request.ServerVariables["HTTP_REFERER"]="MiURl";
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}

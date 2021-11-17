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
/// Summary description for BankData
/// </summary>
public class BankData
{
    private getBankListResponseInformationType _bank;

    public BankData(getBankListResponseInformationType bank)
    {
        _bank = bank;
    }

    public string bankcode
    {
        get { return _bank.financialInstitutionCode; }
    }
    public string bankname
    {
        get { return _bank.financialInstitutionName; }
    }
}

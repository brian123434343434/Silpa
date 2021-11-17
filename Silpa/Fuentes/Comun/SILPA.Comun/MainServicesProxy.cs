using System.Diagnostics;
using System.Xml.Serialization;
using System;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.Web.Services;
using Microsoft.Web.Services; 

namespace SILPA.Comun
{
    /// <remarks/>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "MainServicesSoap", Namespace = "http://vesta.com.br/pse/MainServices")]
    public class MainServicesProxy : WebServicesClientProtocol
    {
        public MessageHeaderType messageHeader;

        /// <remarks/>
        public MainServicesProxy()
        {
            this.Url = "http://localhost/PSEWebServices/MainServices.asmx";
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("messageHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.InOut, Required = false)]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://vesta.com.br/pse/MainServices/getBankList", RequestNamespace = "http://www.uc-council.org/smp/schemas/eanucc", ResponseNamespace = "http://www.uc-council.org/smp/schemas/eanucc", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("getBankListResponseInformation")]
        public getBankListResponseInformationType[] getBankList(getbankListInformationType getBankListInformation)
        {
            object[] results = this.Invoke("getBankList", new object[] {
																		   getBankListInformation});
            return ((getBankListResponseInformationType[])(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegingetBankList(getbankListInformationType getBankListInformation, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("getBankList", new object[] {
																	getBankListInformation}, callback, asyncState);
        }

        /// <remarks/>
        public getBankListResponseInformationType[] EndgetBankList(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((getBankListResponseInformationType[])(results[0]));
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("messageHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.InOut, Required = false)]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://vesta.com.br/pse/MainServices/createTransactionPayment", RequestNamespace = "http://www.uc-council.org/smp/schemas/eanucc", ResponseNamespace = "http://www.uc-council.org/smp/schemas/eanucc", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("createTransactionPaymentResponseInformation")]
        public createTransactionPaymentResponseInformationType createTransactionPayment(createTransactionPaymentInformationType createTransactionPaymentInformation)
        {
            object[] results = this.Invoke("createTransactionPayment", new object[] {
																						createTransactionPaymentInformation});
            return ((createTransactionPaymentResponseInformationType)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegincreateTransactionPayment(createTransactionPaymentInformationType createTransactionPaymentInformation, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("createTransactionPayment", new object[] {
																				 createTransactionPaymentInformation}, callback, asyncState);
        }

        /// <remarks/>
        public createTransactionPaymentResponseInformationType EndcreateTransactionPayment(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((createTransactionPaymentResponseInformationType)(results[0]));
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("messageHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.InOut, Required = false)]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://vesta.com.br/pse/MainServices/confirmTransactionPayment", RequestNamespace = "http://www.uc-council.org/smp/schemas/eanucc", ResponseNamespace = "http://www.uc-council.org/smp/schemas/eanucc", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("confirmTransactionPaymentResponseInformation")]
        public confirmTransactionPaymentResponseInformationType confirmTransactionPayment(confirmTransactionPaymentInformationType confirmTransactionPaymentInformation)
        {
            object[] results = this.Invoke("confirmTransactionPayment", new object[] {
																						 confirmTransactionPaymentInformation});
            return ((confirmTransactionPaymentResponseInformationType)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginconfirmTransactionPayment(confirmTransactionPaymentInformationType confirmTransactionPaymentInformation, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("confirmTransactionPayment", new object[] {
																				  confirmTransactionPaymentInformation}, callback, asyncState);
        }

        /// <remarks/>
        public confirmTransactionPaymentResponseInformationType EndconfirmTransactionPayment(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((confirmTransactionPaymentResponseInformationType)(results[0]));
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("messageHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.InOut, Required = false)]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://vesta.com.br/pse/MainServices/getTransactionInformation", RequestNamespace = "http://www.uc-council.org/smp/schemas/eanucc", ResponseNamespace = "http://www.uc-council.org/smp/schemas/eanucc", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("getTransactionInformationResponseBody")]
        public getTransactionInformationResponseBodyType getTransactionInformation(getTransactionInformationBodyType getTransactionInformationBody)
        {
            object[] results = this.Invoke("getTransactionInformation", new object[] {
																						 getTransactionInformationBody});
            return ((getTransactionInformationResponseBodyType)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BegingetTransactionInformation(getTransactionInformationBodyType getTransactionInformationBody, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("getTransactionInformation", new object[] {
																				  getTransactionInformationBody}, callback, asyncState);
        }

        /// <remarks/>
        public getTransactionInformationResponseBodyType EndgetTransactionInformation(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((getTransactionInformationResponseBodyType)(results[0]));
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapHeaderAttribute("messageHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.InOut, Required = false)]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://vesta.com.br/pse/MainServices/finalizeTransactionPayment", RequestNamespace = "http://www.uc-council.org/smp/schemas/eanucc", ResponseNamespace = "http://www.uc-council.org/smp/schemas/eanucc", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("finalizeTransactionPaymentResponseInformation")]
        public finalizeTransactionPaymentResponseInformationType finalizeTransactionPayment(finalizeTransactionPaymentInformationType finalizeTransactionPaymentInformation)
        {
            object[] results = this.Invoke("finalizeTransactionPayment", new object[] {
																						  finalizeTransactionPaymentInformation});
            return ((finalizeTransactionPaymentResponseInformationType)(results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult BeginfinalizeTransactionPayment(finalizeTransactionPaymentInformationType finalizeTransactionPaymentInformation, System.AsyncCallback callback, object asyncState)
        {
            return this.BeginInvoke("finalizeTransactionPayment", new object[] {
																				   finalizeTransactionPaymentInformation}, callback, asyncState);
        }

        /// <remarks/>
        public finalizeTransactionPaymentResponseInformationType EndfinalizeTransactionPayment(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((finalizeTransactionPaymentResponseInformationType)(results[0]));
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uc-council.org/smp/schemas/eanucc")]
    [System.Xml.Serialization.XmlRootAttribute("messageHeader", Namespace = "http://www.uc-council.org/smp/schemas/eanucc", IsNullable = false)]
    public class MessageHeaderType : SoapHeader
    {

        /// <remarks/>
        public PartyIdentificationType to;

        /// <remarks/>
        public PartyIdentificationType from;

        /// <remarks/>
        public PartyIdentificationType representingParty;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uc-council.org/smp/schemas/eanucc")]
    public class PartyIdentificationType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("gln", typeof(string), DataType = "nonNegativeInteger")]
        [System.Xml.Serialization.XmlElementAttribute("alternatePartyIdentification", typeof(AlternatePartyIdentificationType))]
        public object Item;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("additionalPartyIdentification")]
        public AlternatePartyIdentificationType[] additionalPartyIdentification;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uc-council.org/smp/schemas/eanucc")]
    public class AlternatePartyIdentificationType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public AlternatePartyIdentificationListType type;

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uc-council.org/smp/schemas/eanucc")]
    public enum AlternatePartyIdentificationListType
    {

        /// <remarks/>
        BUYER_ASSIGNED,

        /// <remarks/>
        DUNS,

        /// <remarks/>
        DUNS_PLUS_FOUR,

        /// <remarks/>
        SCAC,

        /// <remarks/>
        SELLER_ASSIGNED,

        /// <remarks/>
        UN_LOCATION_CODE,
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uc-council.org/smp/schemas/eanucc")]
    public class finalizeTransactionPaymentResponseInformationType
    {

        /// <remarks/>
        public string trazabilityCode;

        /// <remarks/>
        public finalizeTransactionPaymentResponseReturnCodeList returnCode;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uc-council.org/smp/schemas/eanucc")]
    public enum finalizeTransactionPaymentResponseReturnCodeList
    {

        /// <remarks/>
        SUCCESS,

        /// <remarks/>
        FAIL_INVALIDTRAZABILITYCODE,

        /// <remarks/>
        FAIL_ACCESSDENIED,

        /// <remarks/>
        FAIL_INVALIDSTATE,

        /// <remarks/>
        FAIL_TIMEOUT,
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uc-council.org/smp/schemas/eanucc")]
    public class finalizeTransactionPaymentInformationType
    {

        /// <remarks/>
        public string entityCode;

        /// <remarks/>
        public string trazabilityCode;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uc-council.org/smp/schemas/eanucc")]
    public class getTransactionInformationResponseBodyType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string ticketId;

        /// <remarks/>
        public string trazabilityCode;

        /// <remarks/>
        public string entityCode;

        /// <remarks/>
        public AmountType transactionValue;

        /// <remarks/>
        public AmountType vatValue;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime soliciteDate;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime bankProcessDate;

        /// <remarks/>
        public string transactionCycle;

        /// <remarks/>
        public getTransactionInformationResponseTransactionStateCodeList transactionState;

        /// <remarks/>
        public getTransactionInformationResponseReturnCodeList returnCode;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uc-council.org/smp/schemas/eanucc")]
    public class AmountType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string currencyISOcode;

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public System.Decimal Value;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uc-council.org/smp/schemas/eanucc")]
    public enum getTransactionInformationResponseTransactionStateCodeList
    {

        /// <remarks/>
        OK,

        /// <remarks/>
        NOT_AUTHORIZED,

        /// <remarks/>
        PENDING,

        /// <remarks/>
        FAILED,
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uc-council.org/smp/schemas/eanucc")]
    public enum getTransactionInformationResponseReturnCodeList
    {

        /// <remarks/>
        SUCCESS,

        /// <remarks/>
        FAIL_INVALIDTRAZABILITYCODE,

        /// <remarks/>
        FAIL_ACCESSDENIED,

        /// <remarks/>
        FAIL_TIMEOUT,
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uc-council.org/smp/schemas/eanucc")]
    public class getTransactionInformationBodyType
    {

        /// <remarks/>
        public string entityCode;

        /// <remarks/>
        public string trazabilityCode;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uc-council.org/smp/schemas/eanucc")]
    public class confirmTransactionPaymentResponseInformationType
    {

        /// <remarks/>
        public string trazabilityCode;

        /// <remarks/>
        public confirmTransactionPaymentResponseReturnCodeList returnCode;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uc-council.org/smp/schemas/eanucc")]
    public enum confirmTransactionPaymentResponseReturnCodeList
    {

        /// <remarks/>
        SUCCESS,

        /// <remarks/>
        FAIL_INVALIDTRAZABILITYCODE,

        /// <remarks/>
        FAIL_ACCESSDENIED,

        /// <remarks/>
        FAIL_INVALIDSTATE,

        /// <remarks/>
        FAIL_INVALIDBANKPROCESSINGDATE,

        /// <remarks/>
        FAIL_INVALIDAUTHORIZEDAMOUNT,

        /// <remarks/>
        FAIL_INCONSISTENTDATA,

        /// <remarks/>
        FAIL_TIMEOUT,
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uc-council.org/smp/schemas/eanucc")]
    public class confirmTransactionPaymentInformationType
    {


        /// <remarks/>
        public string trazabilityCode;

        /// <remarks/>
        public string financialInstitutionCode;

        /// <remarks/>
        public string entityCode;

        /// <remarks/>
        public AmountType transactionValue;

        /// <remarks/>
        public AmountType vatValue;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string ticketId;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime soliciteDate;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime bankProcessDate;

        /// <remarks/>
        public confirmTransactionPaymentTransactionStateCodeList transactionState;

        /// <remarks/>
        public string authorizationId;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uc-council.org/smp/schemas/eanucc")]
    public enum confirmTransactionPaymentTransactionStateCodeList
    {

        /// <remarks/>
        OK,

        /// <remarks/>
        NOT_AUTHORIZED,

        /// <remarks/>
        PENDING,

        /// <remarks/>
        FAILED,
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uc-council.org/smp/schemas/eanucc")]
    public class createTransactionPaymentResponseInformationType
    {

        /// <remarks/>
        public string trazabilityCode;

        /// <remarks/>
        public createTransactionPaymentResponseReturnCodeList returnCode;

        /// <remarks/>
        public string bankurl;

        /// <remarks/>
        public string transactionCycle;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uc-council.org/smp/schemas/eanucc")]
    public enum createTransactionPaymentResponseReturnCodeList
    {

        /// <remarks/>
        SUCCESS,

        /// <remarks/>
        FAIL_ENTITYNOTEXISTSORDISABLED,

        /// <remarks/>
        FAIL_BANKNOTEXISTSORDISABLED,

        /// <remarks/>
        FAIL_SERVICENOTEXISTS,

        /// <remarks/>
        FAIL_INVALIDAMOUNT,

        /// <remarks/>
        FAIL_INVALIDSOLICITDATE,

        /// <remarks/>
        FAIL_BANKUNREACHEABLE,

        /// <remarks/>
        FAIL_NOTCONFIRMEDBYBANK,

        /// <remarks/>
        FAIL_CANNOTGETCURRENTCYCLE,

        /// <remarks/>
        FAIL_ACCESSDENIED,

        /// <remarks/>
        FAIL_TIMEOUT,

        /// <remarks/>
        FAIL_DESCRIPTIONNOTFOUND,

        /// <remarks/>
        FAIL_EXCEEDEDLIMIT,
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uc-council.org/smp/schemas/eanucc")]
    public class createTransactionPaymentInformationType
    {

        /// <remarks/>
        public string financialInstitutionCode;

        /// <remarks/>
        public string entityCode;

        /// <remarks/>
        public string serviceCode;

        /// <remarks/>
        public AmountType transactionValue;

        /// <remarks/>
        public AmountType vatValue;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "nonNegativeInteger")]
        public string ticketId;

        /// <remarks/>
        public string entityurl;

        /// <remarks/>
        public userTypeListType userType;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("referenceNumber")]
        public string[] referenceNumber;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime soliciteDate;

        /// <remarks/>
        public string paymentDescription;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uc-council.org/smp/schemas/eanucc")]
    public enum userTypeListType
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Item0,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1,
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uc-council.org/smp/schemas/eanucc")]
    public class getBankListResponseInformationType
    {

        /// <remarks/>
        public string financialInstitutionCode;

        /// <remarks/>
        public string financialInstitutionName;
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.uc-council.org/smp/schemas/eanucc")]
    public class getbankListInformationType
    {

        /// <remarks/>
        public string entityCode;
    }

}

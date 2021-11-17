using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Xml; 
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;  

/// <summary>
/// Summary description for ProtocolFrame
/// </summary>
public class ProtocolFrame
{
    public ProtocolFrame()
    {

    }

    private bool _operationResult;

    public bool OperationResult
    {
        get { return _operationResult; }
        set { _operationResult = value; }
    }

    private int _operationCode;

    public int OperationCode
    {
        get { return _operationCode; }
        set { _operationCode = value; }
    }

    private string _operationName;

    public string OperationName
    {
        get { return _operationName; }
        set { _operationName = value; }
    }

    private int _errorMessageCode;

    public int ErrorMessageCode
    {
        get { return _errorMessageCode; }
        set { _errorMessageCode = value; }
    }

    private string _errorMessage;

    public string ErrorMessage
    {
        get { return _errorMessage; }
        set { _errorMessage = value; }
    }

    private string _responseTypeName;

    public string ResponseTypeName
    {
        get { return _responseTypeName; }
        set { _responseTypeName = value; }
    }

    private Dictionary<string, string> _operationResponse = new Dictionary<string, string>();

    public Dictionary<string, string> OperationResponse
    {
        get { return _operationResponse; }
        set { _operationResponse = value; }
    }

    public void LoadFromXml(string xmlString)
    {
        XmlDocument xml = new XmlDocument(); 
        xml.LoadXml(xmlString);
        OperationResult = Convert.ToBoolean(xml.DocumentElement.SelectSingleNode("operationResult").InnerText);
        OperationCode = Convert.ToInt32(xml.DocumentElement.SelectSingleNode("operationType/code").InnerText);
        OperationName = xml.DocumentElement.SelectSingleNode("operationType/name").InnerText;
        XmlNode responseData = xml.DocumentElement.SelectSingleNode("operationResponse");

         if( responseData.FirstChild != null)
         {
            this.ResponseTypeName = responseData.FirstChild.LocalName;

            foreach(XmlNode dataNode in responseData.FirstChild.ChildNodes)
            {
                OperationResponse.Add(dataNode.LocalName, dataNode.InnerText);
            }
         }

        XmlNode errorCodeNode = xml.DocumentElement.SelectSingleNode("errorMessage/code");
        XmlNode errorMessageNode = xml.DocumentElement.SelectSingleNode("errorMessage/message");

        if (errorCodeNode != null )
        {
            if (!String.IsNullOrEmpty(errorCodeNode.InnerText))
                ErrorMessageCode = Convert.ToInt32(errorCodeNode.InnerText);
        }

        if( errorMessageNode!= null )
        {
            if (!String.IsNullOrEmpty(errorMessageNode.InnerText))
                ErrorMessage = errorMessageNode.InnerText;
        }
    }

    public String GenerateXml() {
        XmlDocument xml= new XmlDocument(); 

        //'crea la declaración de archivo xml
        XmlDeclaration declaration = xml.CreateXmlDeclaration("1.0", null, null);
        xml.AppendChild(declaration);

        //'Crea el nodo raiz
        XmlElement root = xml.CreateElement("response");
        xml.AppendChild(root);

        //'Crea el nodo de operationResult
        XmlElement result = xml.CreateElement("operationResult");
        result.InnerText = this.OperationResult.ToString();
        root.AppendChild(result);

        //'Crea el nodo de operationType
        XmlElement type = xml.CreateElement("operationType");
        XmlElement typeCode = xml.CreateElement("code");
        typeCode.InnerText = OperationCode.ToString();
        XmlElement typeName = xml.CreateElement("name");
        typeName.InnerText = OperationName;
        type.AppendChild(typeCode);
        type.AppendChild(typeName);
        root.AppendChild(type);

        //'respuesta
        XmlElement response = xml.CreateElement("operationResponse");

        String responseTypeNodeText = ResponseTypeName;

        //'Si no se asigna un nombre para el nodo que contiene los datos entonces se pone un nombre por defecto
        if(  String.IsNullOrEmpty(responseTypeNodeText))
            responseTypeNodeText = "Data";

        XmlElement responseType = xml.CreateElement(responseTypeNodeText);

        foreach (KeyValuePair<String, String>  valuePair in OperationResponse)
        {
            XmlElement valueNode = xml.CreateElement(valuePair.Key);
            valueNode.InnerText = valuePair.Value;
            responseType.AppendChild(valueNode);
        }

        response.AppendChild(responseType);
        root.AppendChild(response);

        //'mensakje de eroor
        if (!String.IsNullOrEmpty(ErrorMessageCode.ToString()  ))
        {
            XmlElement errorMessageNode = xml.CreateElement("errorMessage");
            XmlElement errorCode = xml.CreateElement("code");
            errorCode.InnerText = Convert.ToString(ErrorMessageCode);
            XmlElement errorMessageText = xml.CreateElement("message");
            errorMessageText.InnerText = ErrorMessage;

            errorMessageNode.AppendChild(errorCode);
            errorMessageNode.AppendChild(errorMessageText);
            root.AppendChild(errorMessageNode);
        }

        return xml.OuterXml;
    }
}

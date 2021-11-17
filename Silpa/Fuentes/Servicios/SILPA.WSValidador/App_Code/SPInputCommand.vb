Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class SPImputCommand
     Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function GetYourTime(ByVal format As String) As String
        Return DateTime.Now.ToString(format)
    End Function

    ''' <summary>
    ''' Invocar un comando de logica de negocio
    ''' </summary>
    ''' <param name="command">Se debe pedoir el xml con un formato especicado. Comandos soportados: {LOGIN,GETTRANSACTIONS,COMMITTRANSACTIONS}</param>
    ''' <returns>xml con la repsuesta y el error segun el caso.</returns>
    ''' <remarks></remarks>
    <WebMethod(EnableSession:=True)> _
    Public Function inputCommand(ByVal command As String) As String

        Dim operationResponse As New StringBuilder
        Dim verifySum As String = ""
        Dim xmlDesc As XmlDocument
        Dim analisis As AnalisisXML

        Try
            command = command.Replace("ó", "o")
            command = command.Replace("á", "a")
            command = command.Replace("é", "e")
            command = command.Replace("í", "i")
            command = command.Replace("ú", "u")
            command = command.Replace("ñ", "n")
            command = command.Replace("Ñ", "N")
            command = command.Replace(" ", "")

            ' devolver el mensaje original
            operationResponse.AppendLine("				<inputCommand>" & command & "</inputCommand>")

            xmlDesc = New XmlDocument
            xmlDesc.LoadXml(operationResponse.ToString)

            analisis = New AnalisisXML(xmlDesc)

            analisis.RealizarValidacion()

            Return analisis.ConstruirRespuesta(command, analisis.Validacion)
        Catch ex As Exception

            ' limpiar
            operationResponse = New StringBuilder

            ' devolver el mensaje original
            operationResponse.AppendLine("			<Error>")
            operationResponse.AppendLine("				<Message>" & ex.Message & "</Message>")
            operationResponse.AppendLine("			</Error>")

            ' retornar
            inputCommand = operationResponse.ToString
        Finally

            ' liberar
            operationResponse = Nothing
            verifySum = Nothing
        End Try

    End Function


    <WebMethod()> _
    Public Function ReverseTheString(ByVal s As String) As String
        Dim outText As String = ""
        Dim letters As Char() = New Char(s.Length - 1) {}
        s.CopyTo(0, letters, 0, s.Length)
        Array.Reverse(letters)
        For Each c As Char In letters
            outText += c
        Next
        Return outText
    End Function
    <WebMethod()> _
    Public Function GetTimeString() As String
        Return DateTime.Now.ToLongTimeString()
    End Function

End Class

Imports System.IO
Imports System.Net.Mail
Imports System.Security.Cryptography
Imports System.Text
Imports System.Xml.Serialization
Imports System.Xml
Public Class Tools

    Public Sub New()

    End Sub

    Public Shared Function Decrypt(ByVal Cadena As String) As String
        Try

            'Dim bytes As Byte() = ASCIIEncoding.ASCII.GetBytes("ãY£è´t¼")
            Dim bytes As Byte() = ASCIIEncoding.ASCII.GetBytes("12345678")
            If String.IsNullOrEmpty(Cadena) Then
                Throw New ArgumentNullException("Cadena Vacia.")
            End If
            Dim cryptoProvider As New DESCryptoServiceProvider
            Dim memoryStream As New MemoryStream(Convert.FromBase64String(Cadena))
            Dim cryptoStream As New CryptoStream(memoryStream, cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read)
            Dim reader As New StreamReader(cryptoStream)
            Return reader.ReadToEnd()

        Catch ex As Exception
            ' publicar el error
            'GTK.Tools.Errors.Publish(ex, "IQ", "Movimientos", "Xml2DataTable")
        End Try
    End Function

    Public Shared Function Encrypt(ByVal Cadena As String) As String
        'Dim bytes As Byte() = ASCIIEncoding.ASCII.GetBytes("ãY£è´t¼")
        Dim bytes As Byte() = ASCIIEncoding.ASCII.GetBytes("12345678")
        If String.IsNullOrEmpty(Cadena) Then
            Throw New ArgumentNullException("Cadena Vacia.")
        End If
        Dim cryptoProvider As New DESCryptoServiceProvider
        Dim memoryStream As New MemoryStream
        Dim cryptoStream As New CryptoStream(MemoryStream, cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write)
        Dim writer As New StreamWriter(CryptoStream)
        writer.Write(Cadena)
        writer.Flush()
        cryptoStream.FlushFinalBlock()
        writer.Flush()
        Return Convert.ToBase64String(memoryStream.GetBuffer(), 0, memoryStream.Length)
    End Function

    Public Shared Function getNode(ByVal node As XmlNode, ByVal nodeName As String) As XmlNode
        ' verificar si es el nodo que se busca 
        If node.Name.ToUpper().Equals(nodeName.ToUpper()) Then
            ' devolverlo 
            Return node
        Else
            ' para cada uno de sus hijos 
            For Each child As XmlNode In node
                ' buscar en los hijos 
                Dim response As XmlNode = getNode(child, nodeName)

                ' si se encontro 
                If response IsNot Nothing Then
                    Return response
                End If
            Next
        End If
        Return Nothing
    End Function

    Public Shared Function ConsultarConfig(ByVal llave As String) As String
        Return System.Configuration.ConfigurationManager.AppSettings(llave)
    End Function

End Class

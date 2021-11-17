Imports Microsoft.VisualBasic
Imports System.Xml
Imports SILPA
Imports System.Data



Public Class AnalisisXML
    Private xmlDoc As XmlDocument
    Private oCampo As Campo
    Private oComando As Comando
    Private ManejoErrores As Errores

    Public ReadOnly Property Validacion() As ArrayList
        Get
            Return Errores.ListaErrores
        End Get
    End Property

    Public Sub New()
        ManejoErrores = New Errores()
        oCampo = New Campo()
    End Sub

    Public Sub New(ByVal xmlD As XmlDocument)
        xmlDoc = New XmlDocument
        xmlDoc = xmlD
        RealizarAnalisisComando()
        RealizarAnalisisCampo()
    End Sub

    Private Sub RealizarAnalisisComando()
        Dim nodo As XmlNode
        nodo = Tools.getNode(xmlDoc, "operation")
        oComando = New Comando(nodo.ChildNodes(1).InnerText) ' el 1 es el nombre del comando
    End Sub

    Private Sub RealizarAnalisisCampo()
        Dim nodo As XmlNode
        Dim i As Integer
        oCampo = New Campo()
        nodo = Tools.getNode(xmlDoc, "params")
        While (i <= nodo.ChildNodes.Count - 1)
            oCampo.Add(nodo.ChildNodes(i).Name.ToString(), nodo.ChildNodes(i).InnerText)
            i += 1
        End While
    End Sub

    Public Sub RealizarValidacion()
        Dim cadena As String = Tools.ConsultarConfig("CadenaCx")
        Dim validador As New SILPA.Validador.Validador(cadena, SILPA.Validador.Validador.TipoMotor.SQLSERVER)
        Dim negocio As SILPA.Validador.Negocio
        Dim l As ArrayList
        Dim i, j As Integer
        Dim o As Campo
        Dim m As String

        l = oCampo.Lista

        ManejoErrores = New Errores()

        While (i <= l.Count - 1)
            o = l(i)
            negocio = validador.ValidadorVariable(o.IdentCampo, o.ValorContenido)
            If (negocio.Correcto = False) Then
                While (j <= negocio.Mensaje.Count - 1)
                    m = negocio.Mensaje(j)
                    ManejoErrores.AgregarError(m)
                    j += 1
                End While
            End If
            i += 1
        End While
    End Sub

    Public Function ConstruirRespuesta(ByVal comando As String, ByVal lista As ArrayList) As String
        Dim encuentraI As Integer = 0
        Dim i As Integer = 0
        Dim operationResponse As New StringBuilder
        Dim newComando As String = ""
        Dim erroresJr As Errores
        Dim valida As ValidadorNegocio.Validador



        encuentraI = comando.IndexOf("</operation>")
        newComando = comando.Substring(0, encuentraI)

        newComando = newComando.Replace("</request>", "")
        newComando = newComando.Replace("<request>", "")
        newComando = newComando.Replace("</message>", "")
        newComando = newComando.Replace("<message>", "")

        operationResponse.AppendLine("			<message>")
        operationResponse.AppendLine("			    <response>" & newComando & "")
        operationResponse.AppendLine("      </operation>")
        If (lista.Count > 0) Then
            operationResponse.AppendLine("			    <successful>False</successful>")
            operationResponse.AppendLine("              <returns/>")
            operationResponse.AppendLine("			    <errors>")
            While (i <= lista.Count - 1)
                erroresJr = New Errores
                erroresJr = lista(i)

                operationResponse.AppendLine("			        <error>")
                operationResponse.AppendLine("			            <code>" & i.ToString() & "</code>")
                operationResponse.AppendLine("			            <message>" & erroresJr.Err & "</message>")
                operationResponse.AppendLine("			        </error>")
                i += 1
            End While

            valida = New ValidadorNegocio.Validador(comando, SILPA.Validador.Utilidad._utilidad._cadena)
            If valida.RequieRevalidacion = True Then
                operationResponse.AppendLine("			        <error>")
                operationResponse.AppendLine("			            <code>" & i.ToString() & "</code>")
                operationResponse.AppendLine("			            <message>" & MensajeErrorNegocio(valida, comando) & "</message>")
                operationResponse.AppendLine("			        </error>")
            End If


            operationResponse.AppendLine("			    </errors>")
        Else
            operationResponse.AppendLine("			    <successful>True</successful>")
        End If

        operationResponse.AppendLine("		</response>")
        operationResponse.AppendLine("	</message>")
        Return operationResponse.ToString()
    End Function

    Private Function MensajeErrorNegocio(ByVal valid As ValidadorNegocio.Validador, ByVal comando As String) As String
        Dim salida As String
        Dim proc As String
        Dim newComando As String
        Dim inicio, fin As Integer
        Dim xmlDoc As New XmlDocument
        Dim i As Integer
        comando = comando.Replace("ó", "o")
        comando = comando.Replace("á", "a")
        comando = comando.Replace("é", "e")
        comando = comando.Replace("í", "i")
        comando = comando.Replace("ú", "u")
        comando = comando.Replace("ñ", "n")
        comando = comando.Replace("Ñ", "N")
        comando = comando.Replace(" ", "")
        newComando = ""
        proc = valid.Procedimiento
        salida = ""
        inicio = comando.IndexOf("<params>")

        If inicio > 0 Then
            fin = comando.IndexOf("</params>")
            newComando = comando.Substring(inicio, fin - inicio + "</params>".Length)
            xmlDoc.LoadXml(newComando)
            For i = 0 To xmlDoc.ChildNodes.Count - 1
                proc = proc.Replace("<VALOR" & (i + 1).ToString() & ">", xmlDoc.ChildNodes(i).InnerText)
                proc = proc.Replace("(", " ")
                proc = proc.Replace(")", " ")
                proc = "exec " & proc
            Next
            salida = valid.Mensaje(proc)
        End If
        Return salida
    End Function




End Class

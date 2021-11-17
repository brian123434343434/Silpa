Imports Microsoft.VisualBasic

Public Class Comando
    Private descripcionComando As String

    Public ReadOnly Property Operacion() As String
        Get
            Return descripcionComando
        End Get
    End Property

    Public Sub New(ByVal desc As String)
        descripcionComando = desc
    End Sub
End Class

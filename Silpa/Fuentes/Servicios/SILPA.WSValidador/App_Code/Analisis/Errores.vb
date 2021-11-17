Imports Microsoft.VisualBasic

Public Class Errores
    Private mensaje As String
    Public Property Err() As String
        Get
            Return mensaje
        End Get
        Set(ByVal value As String)
            mensaje = value
        End Set
    End Property

    Public Sub New()
        lista = New ArrayList
    End Sub

    Public Sub New(ByVal jr As String)
        mensaje = jr
    End Sub



    Private Shared lista As ArrayList

    Public Shared Property ListaErrores() As ArrayList
        Set(ByVal value As ArrayList)
            lista = value
        End Set
        Get
            Return lista
        End Get
    End Property

    Public Sub AgregarError(ByVal e As String)
        Dim erroresJr As New Errores("Jr")
        erroresJr.Err = e
        Errores.lista.Add(erroresJr)
    End Sub

    Public Shared Sub AgregarErrorJr(ByVal e As String)
        Dim erroresJr As New Errores("Jr")
        erroresJr.Err = e
        Errores.lista.Add(erroresJr)
    End Sub
End Class

Imports Microsoft.VisualBasic

Public Class Campo
    Private listaCampos As ArrayList

    Public Property Lista() As ArrayList
        Get
            Return listaCampos
        End Get
        Set(ByVal value As ArrayList)
            listaCampos = value
        End Set
    End Property



    Private idCampo As String
    Private valContenido As String
    Private descError As String

    Public Property IdentCampo() As String
        Get
            Return idCampo
        End Get
        Set(ByVal value As String)
            idCampo = value
        End Set
    End Property

    Public Property ValorContenido() As String
        Get
            Return valContenido
        End Get
        Set(ByVal value As String)
            valContenido = value
        End Set
    End Property

    Public Property DescripcionError() As String
        Get
            Return descError
        End Get
        Set(ByVal value As String)
            descError = value
        End Set
    End Property


    Public Sub New()
        listaCampos = New ArrayList()
    End Sub

    Public Sub Add(ByVal idCamp As String, ByVal idConten As String)
        Dim obj As Campo
        Try
            obj = New Campo()
            With obj
                .IdentCampo = idCamp
                .ValorContenido = idConten
                .DescripcionError = Nothing
            End With
            listaCampos.Add(obj)
        Catch ex As Exception
            Errores.AgregarErrorJr(ex.Message)
        End Try
    End Sub

End Class

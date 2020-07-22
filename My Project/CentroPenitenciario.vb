Imports Microsoft.VisualBasic

Public Class CentroPenitenciario
    Private noCentro As Integer
    Private nombreCentro As String
    Private direccionCelda As String
    Private nombreArchivo As String

    Public Sub New(noCenter As Integer, name As String, address As String)
        noCentro = noCenter
        nombreCentro = name
        direccionCelda = address
    End Sub

    Public Property DireccionCeldaP() As String
        Get
            Return direccionCelda
        End Get
        Set(value As String)
            direccionCelda = value
        End Set
    End Property
    Public Property NombreCentroP() As String
        Get
            Return nombreCentro
        End Get
        Set(value As String)
            nombreCentro = value
        End Set
    End Property
    Public Property NoCentroP() As Integer
        Get
            Return noCentro
        End Get
        Set(value As Integer)
            noCentro = value
        End Set
    End Property
    Public Property NombreArchivoP() As String
        Get
            Return nombreArchivo
        End Get
        Set(value As String)
            nombreArchivo = value
        End Set
    End Property
End Class

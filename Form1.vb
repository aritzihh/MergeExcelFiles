Public Class Form1
    Dim union As New UnionDeArchivos()
    Private Sub buttonUnion_Click(sender As Object, e As EventArgs) Handles buttonUnion.Click
        'Dim union = New UnionDeArchivos()
        union.unionArchivos()
        End
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles buttonAddFile.Click
        Dim intResult As Integer
        Dim nameFile As String
        Dim labelCenter As Label
        Dim centroPenitenciario As CentroPenitenciario

        intResult = openFileDialog.ShowDialog()
        If intResult = Windows.Forms.DialogResult.OK Then
            nameFile = openFileDialog.FileName
            centroPenitenciario = union.getCenterOfWorkBook(nameFile)
            labelCenter = New Label()
            labelCenter.AutoSize = True
            labelCenter.Text = centroPenitenciario.NombreCentroP
            panelCentrosPenitenciarios.Controls.Add(labelCenter)
            union.addCentro(centroPenitenciario)
        End If
    End Sub

End Class

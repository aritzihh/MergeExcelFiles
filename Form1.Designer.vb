<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.openFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.buttonUnion = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.panelCentrosPenitenciarios = New System.Windows.Forms.FlowLayoutPanel()
        Me.buttonAddFile = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'openFileDialog
        '
        Me.openFileDialog.FileName = "OpenFileDialog1"
        '
        'buttonUnion
        '
        Me.buttonUnion.Location = New System.Drawing.Point(221, 384)
        Me.buttonUnion.Name = "buttonUnion"
        Me.buttonUnion.Size = New System.Drawing.Size(136, 52)
        Me.buttonUnion.TabIndex = 0
        Me.buttonUnion.Text = "Unir "
        Me.buttonUnion.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.panelCentrosPenitenciarios)
        Me.Panel1.Controls.Add(Me.buttonAddFile)
        Me.Panel1.Controls.Add(Me.buttonUnion)
        Me.Panel1.Location = New System.Drawing.Point(3, -1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(536, 444)
        Me.Panel1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(37, 78)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Centros penitenciarios"
        '
        'panelCentrosPenitenciarios
        '
        Me.panelCentrosPenitenciarios.Location = New System.Drawing.Point(32, 103)
        Me.panelCentrosPenitenciarios.Name = "panelCentrosPenitenciarios"
        Me.panelCentrosPenitenciarios.Size = New System.Drawing.Size(477, 249)
        Me.panelCentrosPenitenciarios.TabIndex = 2
        '
        'buttonAddFile
        '
        Me.buttonAddFile.Location = New System.Drawing.Point(342, 32)
        Me.buttonAddFile.Name = "buttonAddFile"
        Me.buttonAddFile.Size = New System.Drawing.Size(124, 32)
        Me.buttonAddFile.TabIndex = 1
        Me.buttonAddFile.Text = "Añadir  archivos"
        Me.buttonAddFile.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(540, 447)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "Form1"
        Me.Text = "Centros penitenciarios"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents openFileDialog As OpenFileDialog
    Friend WithEvents buttonUnion As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents buttonAddFile As Button
    Friend WithEvents panelCentrosPenitenciarios As FlowLayoutPanel
    Friend WithEvents Label1 As Label
End Class

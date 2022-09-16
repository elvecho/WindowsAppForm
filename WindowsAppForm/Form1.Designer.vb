<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla mediante l'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.TesseraCliente = New System.Windows.Forms.TextBox()
        Me.INVIO = New System.Windows.Forms.Button()
        Me.Log = New System.Windows.Forms.TextBox()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.CANCELLA = New System.Windows.Forms.Button()
        Me.label = New System.Windows.Forms.Label()
        Me.NuovoIndirizzoIP = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TesseraCliente
        '
        Me.TesseraCliente.Location = New System.Drawing.Point(72, 49)
        Me.TesseraCliente.Name = "TesseraCliente"
        Me.TesseraCliente.Size = New System.Drawing.Size(245, 20)
        Me.TesseraCliente.TabIndex = 0
        '
        'INVIO
        '
        Me.INVIO.BackColor = System.Drawing.Color.Lime
        Me.INVIO.Location = New System.Drawing.Point(345, 41)
        Me.INVIO.Name = "INVIO"
        Me.INVIO.Size = New System.Drawing.Size(79, 35)
        Me.INVIO.TabIndex = 2
        Me.INVIO.Text = "invio"
        Me.INVIO.UseVisualStyleBackColor = False
        '
        'Log
        '
        Me.Log.Location = New System.Drawing.Point(72, 170)
        Me.Log.Multiline = True
        Me.Log.Name = "Log"
        Me.Log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.Log.Size = New System.Drawing.Size(747, 321)
        Me.Log.TabIndex = 3
        '
        'CANCELLA
        '
        Me.CANCELLA.BackColor = System.Drawing.Color.Red
        Me.CANCELLA.Location = New System.Drawing.Point(445, 41)
        Me.CANCELLA.Name = "CANCELLA"
        Me.CANCELLA.Size = New System.Drawing.Size(81, 35)
        Me.CANCELLA.TabIndex = 4
        Me.CANCELLA.Text = "cancella"
        Me.CANCELLA.UseVisualStyleBackColor = False
        '
        'label
        '
        Me.label.AutoSize = True
        Me.label.Location = New System.Drawing.Point(69, 25)
        Me.label.Name = "label"
        Me.label.Size = New System.Drawing.Size(83, 13)
        Me.label.TabIndex = 5
        Me.label.Text = "Tessera Cliente:"
        '
        'NuovoIndirizzoIP
        '
        Me.NuovoIndirizzoIP.Location = New System.Drawing.Point(72, 114)
        Me.NuovoIndirizzoIP.Name = "NuovoIndirizzoIP"
        Me.NuovoIndirizzoIP.Size = New System.Drawing.Size(245, 20)
        Me.NuovoIndirizzoIP.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(69, 89)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "nuovo indirizzo ip"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(965, 549)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.NuovoIndirizzoIP)
        Me.Controls.Add(Me.label)
        Me.Controls.Add(Me.CANCELLA)
        Me.Controls.Add(Me.Log)
        Me.Controls.Add(Me.INVIO)
        Me.Controls.Add(Me.TesseraCliente)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TesseraCliente As TextBox
    Friend WithEvents INVIO As Button
    Friend WithEvents Log As TextBox
    Friend WithEvents BindingSource1 As BindingSource
    Friend WithEvents CANCELLA As Button
    Friend WithEvents label As Label
    Friend WithEvents NuovoIndirizzoIP As TextBox
    Friend WithEvents Label1 As Label
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CompareScenarioGraphics_Form
    Inherits DevComponents.DotNetBar.Office2007Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CompareScenarioGraphics_Form))
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx
        Me.rbtnGraphicJuncs = New System.Windows.Forms.RadioButton
        Me.rbtnGraphicPipes = New System.Windows.Forms.RadioButton
        Me.PanelEx1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx1.Controls.Add(Me.rbtnGraphicJuncs)
        Me.PanelEx1.Controls.Add(Me.rbtnGraphicPipes)
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(245, 41)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.Color = System.Drawing.Color.DarkTurquoise
        Me.PanelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 0
        '
        'rbtnGraphicJuncs
        '
        Me.rbtnGraphicJuncs.AutoSize = True
        Me.rbtnGraphicJuncs.Location = New System.Drawing.Point(153, 12)
        Me.rbtnGraphicJuncs.Name = "rbtnGraphicJuncs"
        Me.rbtnGraphicJuncs.Size = New System.Drawing.Size(76, 18)
        Me.rbtnGraphicJuncs.TabIndex = 4
        Me.rbtnGraphicJuncs.Text = "Junctions"
        Me.rbtnGraphicJuncs.UseVisualStyleBackColor = True
        '
        'rbtnGraphicPipes
        '
        Me.rbtnGraphicPipes.AutoSize = True
        Me.rbtnGraphicPipes.Location = New System.Drawing.Point(24, 11)
        Me.rbtnGraphicPipes.Name = "rbtnGraphicPipes"
        Me.rbtnGraphicPipes.Size = New System.Drawing.Size(53, 18)
        Me.rbtnGraphicPipes.TabIndex = 3
        Me.rbtnGraphicPipes.Text = "Pipes"
        Me.rbtnGraphicPipes.UseVisualStyleBackColor = True
        '
        'CompareScenarioGraphics_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ClientSize = New System.Drawing.Size(245, 41)
        Me.Controls.Add(Me.PanelEx1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "CompareScenarioGraphics_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CompareScenarioGraphics_Form"
        Me.TitleText = "Compare Graphical"
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents rbtnGraphicJuncs As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnGraphicPipes As System.Windows.Forms.RadioButton
End Class

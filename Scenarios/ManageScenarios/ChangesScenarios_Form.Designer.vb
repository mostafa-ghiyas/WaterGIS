<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChangesScenarios_Form
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ChangesScenarios_Form))
        Me.RibbonBar1 = New DevComponents.DotNetBar.RibbonBar
        Me.GroupPanel1 = New DevComponents.DotNetBar.Controls.GroupPanel
        Me.PanelEx2 = New DevComponents.DotNetBar.PanelEx
        Me.InputDblElevation = New DevComponents.Editors.DoubleInput
        Me.cboIDJuncFeatures = New DevComponents.DotNetBar.Controls.ComboBoxEx
        Me.PanelEx15 = New DevComponents.DotNetBar.PanelEx
        Me.PanelEx12 = New DevComponents.DotNetBar.PanelEx
        Me.PanelEx6 = New DevComponents.DotNetBar.PanelEx
        Me.PanelEx11 = New DevComponents.DotNetBar.PanelEx
        Me.InputDblLengthPipe = New DevComponents.Editors.DoubleInput
        Me.cboMaterial = New DevComponents.DotNetBar.Controls.ComboBoxEx
        Me.PanelEx13 = New DevComponents.DotNetBar.PanelEx
        Me.PanelEx14 = New DevComponents.DotNetBar.PanelEx
        Me.PanelEx7 = New DevComponents.DotNetBar.PanelEx
        Me.InputIntHazen = New DevComponents.Editors.IntegerInput
        Me.InputDblDiameter = New DevComponents.Editors.DoubleInput
        Me.cboIDPipeFeatures = New DevComponents.DotNetBar.Controls.ComboBoxEx
        Me.PanelEx10 = New DevComponents.DotNetBar.PanelEx
        Me.PanelEx9 = New DevComponents.DotNetBar.PanelEx
        Me.PanelEx8 = New DevComponents.DotNetBar.PanelEx
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx
        Me.PanelEx5 = New DevComponents.DotNetBar.PanelEx
        Me.PanelEx4 = New DevComponents.DotNetBar.PanelEx
        Me.btnClose = New DevComponents.DotNetBar.ButtonX
        Me.btnCreateChange = New DevComponents.DotNetBar.ButtonX
        Me.TabControl1 = New DevComponents.DotNetBar.TabControl
        Me.TabControlPanel1 = New DevComponents.DotNetBar.TabControlPanel
        Me.lstChangesPipes = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader5 = New System.Windows.Forms.ColumnHeader
        Me.TabItem1 = New DevComponents.DotNetBar.TabItem(Me.components)
        Me.TabControlPanel2 = New DevComponents.DotNetBar.TabControlPanel
        Me.lstChangesJunctions = New System.Windows.Forms.ListView
        Me.ColumnHeader6 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader7 = New System.Windows.Forms.ColumnHeader
        Me.TabItem2 = New DevComponents.DotNetBar.TabItem(Me.components)
        Me.RibbonBar1.SuspendLayout()
        Me.GroupPanel1.SuspendLayout()
        Me.PanelEx2.SuspendLayout()
        CType(Me.InputDblElevation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx6.SuspendLayout()
        Me.PanelEx11.SuspendLayout()
        CType(Me.InputDblLengthPipe, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx7.SuspendLayout()
        CType(Me.InputIntHazen, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.InputDblDiameter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx4.SuspendLayout()
        CType(Me.TabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabControlPanel1.SuspendLayout()
        Me.TabControlPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'RibbonBar1
        '
        Me.RibbonBar1.AutoOverflowEnabled = True
        '
        '
        '
        Me.RibbonBar1.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.RibbonBar1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.RibbonBar1.ContainerControlProcessDialogKey = True
        Me.RibbonBar1.Controls.Add(Me.GroupPanel1)
        Me.RibbonBar1.Dock = System.Windows.Forms.DockStyle.Left
        Me.RibbonBar1.Location = New System.Drawing.Point(0, 0)
        Me.RibbonBar1.Name = "RibbonBar1"
        Me.RibbonBar1.Size = New System.Drawing.Size(386, 379)
        Me.RibbonBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.Windows7
        Me.RibbonBar1.TabIndex = 1
        '
        '
        '
        Me.RibbonBar1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.RibbonBar1.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        'GroupPanel1
        '
        Me.GroupPanel1.CanvasColor = System.Drawing.SystemColors.Control
        Me.GroupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007
        Me.GroupPanel1.Controls.Add(Me.PanelEx2)
        Me.GroupPanel1.Controls.Add(Me.PanelEx6)
        Me.GroupPanel1.Controls.Add(Me.PanelEx1)
        Me.GroupPanel1.Controls.Add(Me.PanelEx5)
        Me.GroupPanel1.Controls.Add(Me.PanelEx4)
        Me.GroupPanel1.Controls.Add(Me.TabControl1)
        Me.GroupPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupPanel1.Location = New System.Drawing.Point(0, 0)
        Me.GroupPanel1.Name = "GroupPanel1"
        Me.GroupPanel1.Size = New System.Drawing.Size(386, 362)
        '
        '
        '
        Me.GroupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.GroupPanel1.Style.BackColorGradientAngle = 90
        Me.GroupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.GroupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderBottomWidth = 1
        Me.GroupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.GroupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderLeftWidth = 1
        Me.GroupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderRightWidth = 1
        Me.GroupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.GroupPanel1.Style.BorderTopWidth = 1
        Me.GroupPanel1.Style.CornerDiameter = 4
        Me.GroupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.GroupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center
        Me.GroupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.GroupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near
        '
        '
        '
        Me.GroupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square
        '
        '
        '
        Me.GroupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.GroupPanel1.TabIndex = 0
        '
        'PanelEx2
        '
        Me.PanelEx2.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx2.Controls.Add(Me.InputDblElevation)
        Me.PanelEx2.Controls.Add(Me.cboIDJuncFeatures)
        Me.PanelEx2.Controls.Add(Me.PanelEx15)
        Me.PanelEx2.Controls.Add(Me.PanelEx12)
        Me.PanelEx2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelEx2.Location = New System.Drawing.Point(0, 139)
        Me.PanelEx2.Name = "PanelEx2"
        Me.PanelEx2.Size = New System.Drawing.Size(380, 29)
        Me.PanelEx2.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx2.Style.GradientAngle = 90
        Me.PanelEx2.TabIndex = 21
        '
        'InputDblElevation
        '
        '
        '
        '
        Me.InputDblElevation.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.InputDblElevation.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.InputDblElevation.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.InputDblElevation.Increment = 1
        Me.InputDblElevation.Location = New System.Drawing.Point(280, 4)
        Me.InputDblElevation.Name = "InputDblElevation"
        Me.InputDblElevation.ShowUpDown = True
        Me.InputDblElevation.Size = New System.Drawing.Size(93, 22)
        Me.InputDblElevation.TabIndex = 6
        '
        'cboIDJuncFeatures
        '
        Me.cboIDJuncFeatures.DisplayMember = "Text"
        Me.cboIDJuncFeatures.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cboIDJuncFeatures.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboIDJuncFeatures.FormattingEnabled = True
        Me.cboIDJuncFeatures.ItemHeight = 16
        Me.cboIDJuncFeatures.Location = New System.Drawing.Point(92, 3)
        Me.cboIDJuncFeatures.Name = "cboIDJuncFeatures"
        Me.cboIDJuncFeatures.Size = New System.Drawing.Size(94, 22)
        Me.cboIDJuncFeatures.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.cboIDJuncFeatures.TabIndex = 5
        '
        'PanelEx15
        '
        Me.PanelEx15.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx15.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx15.Location = New System.Drawing.Point(197, 4)
        Me.PanelEx15.Name = "PanelEx15"
        Me.PanelEx15.Size = New System.Drawing.Size(80, 21)
        Me.PanelEx15.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx15.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx15.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx15.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx15.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx15.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx15.Style.GradientAngle = 90
        Me.PanelEx15.TabIndex = 4
        Me.PanelEx15.Text = "Elevation"
        '
        'PanelEx12
        '
        Me.PanelEx12.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx12.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx12.Location = New System.Drawing.Point(10, 4)
        Me.PanelEx12.Name = "PanelEx12"
        Me.PanelEx12.Size = New System.Drawing.Size(79, 21)
        Me.PanelEx12.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx12.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx12.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx12.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx12.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx12.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx12.Style.GradientAngle = 90
        Me.PanelEx12.TabIndex = 3
        Me.PanelEx12.Text = "ID Feature"
        '
        'PanelEx6
        '
        Me.PanelEx6.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx6.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx6.Controls.Add(Me.PanelEx11)
        Me.PanelEx6.Controls.Add(Me.PanelEx7)
        Me.PanelEx6.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelEx6.Location = New System.Drawing.Point(0, 26)
        Me.PanelEx6.Name = "PanelEx6"
        Me.PanelEx6.Size = New System.Drawing.Size(380, 87)
        Me.PanelEx6.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx6.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx6.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx6.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx6.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx6.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx6.Style.GradientAngle = 90
        Me.PanelEx6.TabIndex = 20
        '
        'PanelEx11
        '
        Me.PanelEx11.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx11.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx11.Controls.Add(Me.InputDblLengthPipe)
        Me.PanelEx11.Controls.Add(Me.cboMaterial)
        Me.PanelEx11.Controls.Add(Me.PanelEx13)
        Me.PanelEx11.Controls.Add(Me.PanelEx14)
        Me.PanelEx11.Location = New System.Drawing.Point(192, 5)
        Me.PanelEx11.Name = "PanelEx11"
        Me.PanelEx11.Size = New System.Drawing.Size(183, 78)
        Me.PanelEx11.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx11.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx11.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx11.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx11.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx11.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx11.Style.GradientAngle = 90
        Me.PanelEx11.TabIndex = 3
        '
        'InputDblLengthPipe
        '
        '
        '
        '
        Me.InputDblLengthPipe.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.InputDblLengthPipe.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.InputDblLengthPipe.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.InputDblLengthPipe.Increment = 1
        Me.InputDblLengthPipe.Location = New System.Drawing.Point(88, 29)
        Me.InputDblLengthPipe.Name = "InputDblLengthPipe"
        Me.InputDblLengthPipe.ShowUpDown = True
        Me.InputDblLengthPipe.Size = New System.Drawing.Size(93, 22)
        Me.InputDblLengthPipe.TabIndex = 10
        '
        'cboMaterial
        '
        Me.cboMaterial.DisplayMember = "Text"
        Me.cboMaterial.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cboMaterial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboMaterial.FormattingEnabled = True
        Me.cboMaterial.ItemHeight = 16
        Me.cboMaterial.Location = New System.Drawing.Point(88, 5)
        Me.cboMaterial.Name = "cboMaterial"
        Me.cboMaterial.Size = New System.Drawing.Size(93, 22)
        Me.cboMaterial.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.cboMaterial.TabIndex = 9
        '
        'PanelEx13
        '
        Me.PanelEx13.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx13.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx13.Location = New System.Drawing.Point(5, 30)
        Me.PanelEx13.Name = "PanelEx13"
        Me.PanelEx13.Size = New System.Drawing.Size(79, 21)
        Me.PanelEx13.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx13.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx13.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx13.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx13.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx13.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx13.Style.GradientAngle = 90
        Me.PanelEx13.TabIndex = 8
        Me.PanelEx13.Text = "Length"
        '
        'PanelEx14
        '
        Me.PanelEx14.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx14.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx14.Location = New System.Drawing.Point(5, 5)
        Me.PanelEx14.Name = "PanelEx14"
        Me.PanelEx14.Size = New System.Drawing.Size(80, 21)
        Me.PanelEx14.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx14.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx14.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx14.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx14.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx14.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx14.Style.GradientAngle = 90
        Me.PanelEx14.TabIndex = 2
        Me.PanelEx14.Text = "Material"
        '
        'PanelEx7
        '
        Me.PanelEx7.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx7.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx7.Controls.Add(Me.InputIntHazen)
        Me.PanelEx7.Controls.Add(Me.InputDblDiameter)
        Me.PanelEx7.Controls.Add(Me.cboIDPipeFeatures)
        Me.PanelEx7.Controls.Add(Me.PanelEx10)
        Me.PanelEx7.Controls.Add(Me.PanelEx9)
        Me.PanelEx7.Controls.Add(Me.PanelEx8)
        Me.PanelEx7.Location = New System.Drawing.Point(5, 5)
        Me.PanelEx7.Name = "PanelEx7"
        Me.PanelEx7.Size = New System.Drawing.Size(184, 78)
        Me.PanelEx7.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx7.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx7.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx7.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx7.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx7.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx7.Style.GradientAngle = 90
        Me.PanelEx7.TabIndex = 2
        '
        'InputIntHazen
        '
        '
        '
        '
        Me.InputIntHazen.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.InputIntHazen.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.InputIntHazen.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.InputIntHazen.Location = New System.Drawing.Point(87, 53)
        Me.InputIntHazen.Name = "InputIntHazen"
        Me.InputIntHazen.ShowUpDown = True
        Me.InputIntHazen.Size = New System.Drawing.Size(94, 22)
        Me.InputIntHazen.TabIndex = 12
        '
        'InputDblDiameter
        '
        '
        '
        '
        Me.InputDblDiameter.BackgroundStyle.Class = "DateTimeInputBackground"
        Me.InputDblDiameter.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.InputDblDiameter.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2
        Me.InputDblDiameter.Increment = 1
        Me.InputDblDiameter.Location = New System.Drawing.Point(88, 29)
        Me.InputDblDiameter.Name = "InputDblDiameter"
        Me.InputDblDiameter.ShowUpDown = True
        Me.InputDblDiameter.Size = New System.Drawing.Size(94, 22)
        Me.InputDblDiameter.TabIndex = 11
        '
        'cboIDPipeFeatures
        '
        Me.cboIDPipeFeatures.DisplayMember = "Text"
        Me.cboIDPipeFeatures.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cboIDPipeFeatures.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboIDPipeFeatures.FormattingEnabled = True
        Me.cboIDPipeFeatures.ItemHeight = 16
        Me.cboIDPipeFeatures.Location = New System.Drawing.Point(87, 5)
        Me.cboIDPipeFeatures.Name = "cboIDPipeFeatures"
        Me.cboIDPipeFeatures.Size = New System.Drawing.Size(94, 22)
        Me.cboIDPipeFeatures.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.cboIDPipeFeatures.TabIndex = 10
        '
        'PanelEx10
        '
        Me.PanelEx10.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx10.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx10.Location = New System.Drawing.Point(5, 54)
        Me.PanelEx10.Name = "PanelEx10"
        Me.PanelEx10.Size = New System.Drawing.Size(79, 21)
        Me.PanelEx10.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx10.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx10.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx10.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx10.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx10.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx10.Style.GradientAngle = 90
        Me.PanelEx10.TabIndex = 9
        Me.PanelEx10.Text = "Hazen-C"
        '
        'PanelEx9
        '
        Me.PanelEx9.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx9.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx9.Location = New System.Drawing.Point(5, 30)
        Me.PanelEx9.Name = "PanelEx9"
        Me.PanelEx9.Size = New System.Drawing.Size(79, 21)
        Me.PanelEx9.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx9.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx9.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx9.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx9.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx9.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx9.Style.GradientAngle = 90
        Me.PanelEx9.TabIndex = 8
        Me.PanelEx9.Text = "Diameter"
        '
        'PanelEx8
        '
        Me.PanelEx8.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx8.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx8.Location = New System.Drawing.Point(5, 5)
        Me.PanelEx8.Name = "PanelEx8"
        Me.PanelEx8.Size = New System.Drawing.Size(79, 21)
        Me.PanelEx8.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx8.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx8.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx8.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx8.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx8.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx8.Style.GradientAngle = 90
        Me.PanelEx8.TabIndex = 2
        Me.PanelEx8.Text = "ID Feature"
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx1.Location = New System.Drawing.Point(0, 113)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(386, 25)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.Color = System.Drawing.Color.DarkTurquoise
        Me.PanelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 17
        Me.PanelEx1.Text = "Junctions"
        '
        'PanelEx5
        '
        Me.PanelEx5.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx5.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelEx5.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx5.Name = "PanelEx5"
        Me.PanelEx5.Size = New System.Drawing.Size(380, 26)
        Me.PanelEx5.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx5.Style.BackColor1.Color = System.Drawing.Color.DarkTurquoise
        Me.PanelEx5.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx5.Style.GradientAngle = 90
        Me.PanelEx5.TabIndex = 19
        Me.PanelEx5.Text = "Pipes"
        '
        'PanelEx4
        '
        Me.PanelEx4.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx4.Controls.Add(Me.btnClose)
        Me.PanelEx4.Controls.Add(Me.btnCreateChange)
        Me.PanelEx4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelEx4.Location = New System.Drawing.Point(0, 168)
        Me.PanelEx4.Name = "PanelEx4"
        Me.PanelEx4.Size = New System.Drawing.Size(380, 30)
        Me.PanelEx4.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx4.Style.BackColor1.Color = System.Drawing.Color.DarkTurquoise
        Me.PanelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx4.Style.GradientAngle = 90
        Me.PanelEx4.TabIndex = 5
        '
        'btnClose
        '
        Me.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(57, 3)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(83, 23)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        '
        'btnCreateChange
        '
        Me.btnCreateChange.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnCreateChange.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb
        Me.btnCreateChange.Image = Global.WaterEngine_AnalysisNetwork.My.Resources.Resources.Edit
        Me.btnCreateChange.ImageFixedSize = New System.Drawing.Size(18, 18)
        Me.btnCreateChange.Location = New System.Drawing.Point(242, 3)
        Me.btnCreateChange.Name = "btnCreateChange"
        Me.btnCreateChange.Shape = New DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2)
        Me.btnCreateChange.Size = New System.Drawing.Size(91, 23)
        Me.btnCreateChange.TabIndex = 3
        Me.btnCreateChange.Text = "Set Change"
        '
        'TabControl1
        '
        Me.TabControl1.BackColor = System.Drawing.Color.FromArgb(CType(CType(194, Byte), Integer), CType(CType(217, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.TabControl1.CanReorderTabs = True
        Me.TabControl1.Controls.Add(Me.TabControlPanel2)
        Me.TabControl1.Controls.Add(Me.TabControlPanel1)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(0, 198)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedTabFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold)
        Me.TabControl1.SelectedTabIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(380, 158)
        Me.TabControl1.TabIndex = 1
        Me.TabControl1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.FixedWithNavigationBox
        Me.TabControl1.Tabs.Add(Me.TabItem1)
        Me.TabControl1.Tabs.Add(Me.TabItem2)
        Me.TabControl1.Text = "TabControl1"
        '
        'TabControlPanel1
        '
        Me.TabControlPanel1.Controls.Add(Me.lstChangesPipes)
        Me.TabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlPanel1.Location = New System.Drawing.Point(0, 26)
        Me.TabControlPanel1.Name = "TabControlPanel1"
        Me.TabControlPanel1.Padding = New System.Windows.Forms.Padding(1)
        Me.TabControlPanel1.Size = New System.Drawing.Size(380, 132)
        Me.TabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(142, Byte), Integer), CType(CType(179, Byte), Integer), CType(CType(231, Byte), Integer))
        Me.TabControlPanel1.Style.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.TabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.TabControlPanel1.Style.BorderColor.Color = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.TabControlPanel1.Style.BorderSide = CType(((DevComponents.DotNetBar.eBorderSide.Left Or DevComponents.DotNetBar.eBorderSide.Right) _
                    Or DevComponents.DotNetBar.eBorderSide.Bottom), DevComponents.DotNetBar.eBorderSide)
        Me.TabControlPanel1.Style.GradientAngle = 90
        Me.TabControlPanel1.TabIndex = 1
        Me.TabControlPanel1.TabItem = Me.TabItem1
        '
        'lstChangesPipes
        '
        Me.lstChangesPipes.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5})
        Me.lstChangesPipes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstChangesPipes.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.lstChangesPipes.GridLines = True
        Me.lstChangesPipes.Location = New System.Drawing.Point(1, 1)
        Me.lstChangesPipes.Name = "lstChangesPipes"
        Me.lstChangesPipes.RightToLeftLayout = True
        Me.lstChangesPipes.Size = New System.Drawing.Size(378, 130)
        Me.lstChangesPipes.TabIndex = 20
        Me.lstChangesPipes.UseCompatibleStateImageBehavior = False
        Me.lstChangesPipes.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Feature ID"
        Me.ColumnHeader1.Width = 73
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Diameter"
        Me.ColumnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader2.Width = 73
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Length"
        Me.ColumnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader3.Width = 73
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Hazen-C"
        Me.ColumnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader4.Width = 73
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Material"
        Me.ColumnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader5.Width = 73
        '
        'TabItem1
        '
        Me.TabItem1.AttachedControl = Me.TabControlPanel1
        Me.TabItem1.Name = "TabItem1"
        Me.TabItem1.Text = "Pipes"
        '
        'TabControlPanel2
        '
        Me.TabControlPanel2.Controls.Add(Me.lstChangesJunctions)
        Me.TabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControlPanel2.Location = New System.Drawing.Point(0, 26)
        Me.TabControlPanel2.Name = "TabControlPanel2"
        Me.TabControlPanel2.Padding = New System.Windows.Forms.Padding(1)
        Me.TabControlPanel2.Size = New System.Drawing.Size(380, 132)
        Me.TabControlPanel2.Style.BackColor1.Color = System.Drawing.Color.FromArgb(CType(CType(142, Byte), Integer), CType(CType(179, Byte), Integer), CType(CType(231, Byte), Integer))
        Me.TabControlPanel2.Style.BackColor2.Color = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(237, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.TabControlPanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.TabControlPanel2.Style.BorderColor.Color = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.TabControlPanel2.Style.BorderSide = CType(((DevComponents.DotNetBar.eBorderSide.Left Or DevComponents.DotNetBar.eBorderSide.Right) _
                    Or DevComponents.DotNetBar.eBorderSide.Bottom), DevComponents.DotNetBar.eBorderSide)
        Me.TabControlPanel2.Style.GradientAngle = 90
        Me.TabControlPanel2.TabIndex = 2
        Me.TabControlPanel2.TabItem = Me.TabItem2
        '
        'lstChangesJunctions
        '
        Me.lstChangesJunctions.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader6, Me.ColumnHeader7})
        Me.lstChangesJunctions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstChangesJunctions.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.lstChangesJunctions.GridLines = True
        Me.lstChangesJunctions.Location = New System.Drawing.Point(1, 1)
        Me.lstChangesJunctions.Name = "lstChangesJunctions"
        Me.lstChangesJunctions.RightToLeftLayout = True
        Me.lstChangesJunctions.Size = New System.Drawing.Size(378, 130)
        Me.lstChangesJunctions.TabIndex = 21
        Me.lstChangesJunctions.UseCompatibleStateImageBehavior = False
        Me.lstChangesJunctions.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Feature ID"
        Me.ColumnHeader6.Width = 180
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Elevation"
        Me.ColumnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader7.Width = 180
        '
        'TabItem2
        '
        Me.TabItem2.AttachedControl = Me.TabControlPanel2
        Me.TabItem2.Name = "TabItem2"
        Me.TabItem2.Text = "Junctions"
        '
        'ChangesScenarios_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ClientSize = New System.Drawing.Size(387, 379)
        Me.Controls.Add(Me.RibbonBar1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "ChangesScenarios_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ChengesScenarios_Form"
        Me.TitleText = "Alternatives"
        Me.RibbonBar1.ResumeLayout(False)
        Me.GroupPanel1.ResumeLayout(False)
        Me.PanelEx2.ResumeLayout(False)
        CType(Me.InputDblElevation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx6.ResumeLayout(False)
        Me.PanelEx11.ResumeLayout(False)
        CType(Me.InputDblLengthPipe, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx7.ResumeLayout(False)
        CType(Me.InputIntHazen, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.InputDblDiameter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx4.ResumeLayout(False)
        CType(Me.TabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabControlPanel1.ResumeLayout(False)
        Me.TabControlPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RibbonBar1 As DevComponents.DotNetBar.RibbonBar
    Friend WithEvents GroupPanel1 As DevComponents.DotNetBar.Controls.GroupPanel
    Friend WithEvents PanelEx5 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents PanelEx4 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents btnClose As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnCreateChange As DevComponents.DotNetBar.ButtonX
    Friend WithEvents TabControl1 As DevComponents.DotNetBar.TabControl
    Friend WithEvents TabControlPanel1 As DevComponents.DotNetBar.TabControlPanel
    Friend WithEvents lstChangesPipes As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabItem1 As DevComponents.DotNetBar.TabItem
    Friend WithEvents TabControlPanel2 As DevComponents.DotNetBar.TabControlPanel
    Friend WithEvents lstChangesJunctions As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabItem2 As DevComponents.DotNetBar.TabItem
    Friend WithEvents PanelEx6 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents PanelEx11 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents cboMaterial As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents PanelEx13 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents PanelEx14 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents PanelEx7 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents cboIDPipeFeatures As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents PanelEx10 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents PanelEx9 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents PanelEx8 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents PanelEx2 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents InputDblElevation As DevComponents.Editors.DoubleInput
    Friend WithEvents cboIDJuncFeatures As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents PanelEx15 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents PanelEx12 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents InputDblLengthPipe As DevComponents.Editors.DoubleInput
    Friend WithEvents InputIntHazen As DevComponents.Editors.IntegerInput
    Friend WithEvents InputDblDiameter As DevComponents.Editors.DoubleInput
End Class

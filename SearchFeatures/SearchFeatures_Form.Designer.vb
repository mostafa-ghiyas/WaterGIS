<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SearchFeatures_Form
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SearchFeatures_Form))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer
        Me.PanelEx1 = New DevComponents.DotNetBar.PanelEx
        Me.PictureBox3 = New System.Windows.Forms.PictureBox
        Me.cboLayer = New DevComponents.DotNetBar.Controls.ComboBoxEx
        Me.txtFind = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.PanelEx4 = New DevComponents.DotNetBar.PanelEx
        Me.cboField = New DevComponents.DotNetBar.Controls.ComboBoxEx
        Me.rbtnInField = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.rbtnAllField = New DevComponents.DotNetBar.Controls.CheckBoxX
        Me.PanelEx2 = New DevComponents.DotNetBar.PanelEx
        Me.btnClose = New DevComponents.DotNetBar.ButtonX
        Me.btnNewSearch = New DevComponents.DotNetBar.ButtonX
        Me.btnFind = New DevComponents.DotNetBar.ButtonX
        Me.PanelEx5 = New DevComponents.DotNetBar.PanelEx
        Me.PanelEx3 = New DevComponents.DotNetBar.PanelEx
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer
        Me.lstResult = New DevComponents.DotNetBar.Controls.ListViewEx
        Me.columnHeader1 = New System.Windows.Forms.ColumnHeader(2)
        Me.columnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.columnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel_NumberOfFeature = New DevComponents.DotNetBar.PanelEx
        Me.ContextMenu_LstResult = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SelectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.UnSelectToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.ZoomInToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BookmarkToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.PanelEx1.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelEx4.SuspendLayout()
        Me.PanelEx2.SuspendLayout()
        Me.PanelEx3.SuspendLayout()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        Me.ContextMenu_LstResult.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.PanelEx5)
        Me.SplitContainer1.Panel2.Controls.Add(Me.PanelEx3)
        Me.SplitContainer1.Size = New System.Drawing.Size(403, 280)
        Me.SplitContainer1.SplitterDistance = 123
        Me.SplitContainer1.TabIndex = 0
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer3)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.PanelEx2)
        Me.SplitContainer2.Size = New System.Drawing.Size(403, 123)
        Me.SplitContainer2.SplitterDistance = 295
        Me.SplitContainer2.TabIndex = 0
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.PanelEx1)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.PanelEx4)
        Me.SplitContainer3.Size = New System.Drawing.Size(295, 123)
        Me.SplitContainer3.SplitterDistance = 57
        Me.SplitContainer3.TabIndex = 1
        '
        'PanelEx1
        '
        Me.PanelEx1.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx1.Controls.Add(Me.PictureBox3)
        Me.PanelEx1.Controls.Add(Me.cboLayer)
        Me.PanelEx1.Controls.Add(Me.txtFind)
        Me.PanelEx1.Controls.Add(Me.Label2)
        Me.PanelEx1.Controls.Add(Me.Label1)
        Me.PanelEx1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx1.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx1.Name = "PanelEx1"
        Me.PanelEx1.Size = New System.Drawing.Size(295, 57)
        Me.PanelEx1.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx1.Style.GradientAngle = 90
        Me.PanelEx1.TabIndex = 0
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.WaterEngine_AnalysisNetwork.My.Resources.Resources.LayerGroup321
        Me.PictureBox3.Location = New System.Drawing.Point(56, 32)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(23, 22)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 4
        Me.PictureBox3.TabStop = False
        '
        'cboLayer
        '
        Me.cboLayer.DisplayMember = "Text"
        Me.cboLayer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cboLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLayer.FormattingEnabled = True
        Me.cboLayer.ItemHeight = 16
        Me.cboLayer.Location = New System.Drawing.Point(85, 32)
        Me.cboLayer.Name = "cboLayer"
        Me.cboLayer.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboLayer.Size = New System.Drawing.Size(203, 22)
        Me.cboLayer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.cboLayer.TabIndex = 3
        '
        'txtFind
        '
        Me.txtFind.Font = New System.Drawing.Font("Times New Roman", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFind.Location = New System.Drawing.Point(85, 5)
        Me.txtFind.Name = "txtFind"
        Me.txtFind.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFind.Size = New System.Drawing.Size(204, 21)
        Me.txtFind.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 34)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label2.Size = New System.Drawing.Size(43, 17)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Layer"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label1.Size = New System.Drawing.Size(42, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Value"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PanelEx4
        '
        Me.PanelEx4.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx4.Controls.Add(Me.cboField)
        Me.PanelEx4.Controls.Add(Me.rbtnInField)
        Me.PanelEx4.Controls.Add(Me.rbtnAllField)
        Me.PanelEx4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx4.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx4.Name = "PanelEx4"
        Me.PanelEx4.Size = New System.Drawing.Size(295, 62)
        Me.PanelEx4.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx4.Style.GradientAngle = 90
        Me.PanelEx4.TabIndex = 0
        '
        'cboField
        '
        Me.cboField.DisplayMember = "Text"
        Me.cboField.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.cboField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboField.FormattingEnabled = True
        Me.cboField.ItemHeight = 16
        Me.cboField.Location = New System.Drawing.Point(85, 32)
        Me.cboField.Name = "cboField"
        Me.cboField.Size = New System.Drawing.Size(203, 22)
        Me.cboField.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.cboField.TabIndex = 3
        '
        'rbtnInField
        '
        '
        '
        '
        Me.rbtnInField.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.rbtnInField.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.rbtnInField.Location = New System.Drawing.Point(7, 32)
        Me.rbtnInField.Name = "rbtnInField"
        Me.rbtnInField.Size = New System.Drawing.Size(64, 23)
        Me.rbtnInField.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.rbtnInField.TabIndex = 3
        Me.rbtnInField.Text = "In Field"
        '
        'rbtnAllField
        '
        '
        '
        '
        Me.rbtnAllField.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.rbtnAllField.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton
        Me.rbtnAllField.Location = New System.Drawing.Point(7, 4)
        Me.rbtnAllField.Name = "rbtnAllField"
        Me.rbtnAllField.Size = New System.Drawing.Size(71, 23)
        Me.rbtnAllField.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.rbtnAllField.TabIndex = 2
        Me.rbtnAllField.Text = "All Fields"
        '
        'PanelEx2
        '
        Me.PanelEx2.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx2.Controls.Add(Me.btnClose)
        Me.PanelEx2.Controls.Add(Me.btnNewSearch)
        Me.PanelEx2.Controls.Add(Me.btnFind)
        Me.PanelEx2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelEx2.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx2.Name = "PanelEx2"
        Me.PanelEx2.Size = New System.Drawing.Size(104, 123)
        Me.PanelEx2.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx2.Style.GradientAngle = 90
        Me.PanelEx2.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Location = New System.Drawing.Point(6, 88)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(91, 23)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close"
        '
        'btnNewSearch
        '
        Me.btnNewSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnNewSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb
        Me.btnNewSearch.Image = Global.WaterEngine_AnalysisNetwork.My.Resources.Resources._new
        Me.btnNewSearch.Location = New System.Drawing.Point(6, 49)
        Me.btnNewSearch.Name = "btnNewSearch"
        Me.btnNewSearch.Size = New System.Drawing.Size(91, 23)
        Me.btnNewSearch.TabIndex = 6
        Me.btnNewSearch.Text = "New Search"
        '
        'btnFind
        '
        Me.btnFind.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton
        Me.btnFind.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueOrb
        Me.btnFind.Image = Global.WaterEngine_AnalysisNetwork.My.Resources.Resources.ArcMap24
        Me.btnFind.ImageFixedSize = New System.Drawing.Size(18, 18)
        Me.btnFind.Location = New System.Drawing.Point(6, 11)
        Me.btnFind.Name = "btnFind"
        Me.btnFind.Size = New System.Drawing.Size(91, 23)
        Me.btnFind.TabIndex = 5
        Me.btnFind.Text = "Search"
        '
        'PanelEx5
        '
        Me.PanelEx5.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx5.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelEx5.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelEx5.Location = New System.Drawing.Point(0, 0)
        Me.PanelEx5.Name = "PanelEx5"
        Me.PanelEx5.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.PanelEx5.Size = New System.Drawing.Size(403, 26)
        Me.PanelEx5.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx5.Style.BackColor1.Color = System.Drawing.Color.DarkTurquoise
        Me.PanelEx5.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx5.Style.GradientAngle = 90
        Me.PanelEx5.TabIndex = 15
        Me.PanelEx5.Text = "For show menu rigth click on items."
        '
        'PanelEx3
        '
        Me.PanelEx3.CanvasColor = System.Drawing.SystemColors.Control
        Me.PanelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.PanelEx3.Controls.Add(Me.SplitContainer4)
        Me.PanelEx3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelEx3.Location = New System.Drawing.Point(0, 20)
        Me.PanelEx3.Name = "PanelEx3"
        Me.PanelEx3.Size = New System.Drawing.Size(403, 133)
        Me.PanelEx3.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.PanelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground
        Me.PanelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.PanelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.PanelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.PanelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.PanelEx3.Style.GradientAngle = 90
        Me.PanelEx3.TabIndex = 0
        Me.PanelEx3.Text = "PanelEx3"
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.lstResult)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.Panel_NumberOfFeature)
        Me.SplitContainer4.Size = New System.Drawing.Size(403, 133)
        Me.SplitContainer4.SplitterDistance = 104
        Me.SplitContainer4.TabIndex = 0
        '
        'lstResult
        '
        Me.lstResult.Activation = System.Windows.Forms.ItemActivation.OneClick
        '
        '
        '
        Me.lstResult.Border.Class = "ListViewBorder"
        Me.lstResult.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lstResult.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeader1, Me.columnHeader2, Me.columnHeader3})
        Me.lstResult.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.lstResult.FullRowSelect = True
        Me.lstResult.GridLines = True
        Me.lstResult.HoverSelection = True
        Me.lstResult.Location = New System.Drawing.Point(0, 9)
        Me.lstResult.Name = "lstResult"
        Me.lstResult.Size = New System.Drawing.Size(403, 95)
        Me.lstResult.SmallImageList = Me.ImageList1
        Me.lstResult.TabIndex = 4
        Me.lstResult.UseCompatibleStateImageBehavior = False
        Me.lstResult.View = System.Windows.Forms.View.Details
        '
        'columnHeader1
        '
        Me.columnHeader1.Text = "Layer"
        Me.columnHeader1.Width = 132
        '
        'columnHeader2
        '
        Me.columnHeader2.Text = "Feature ID"
        Me.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.columnHeader2.Width = 132
        '
        'columnHeader3
        '
        Me.columnHeader3.Text = "Field"
        Me.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.columnHeader3.Width = 132
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Layer_LYR_File16.png")
        Me.ImageList1.Images.SetKeyName(1, "Layer_LYR_File32.png")
        Me.ImageList1.Images.SetKeyName(2, "Layer_LYR_File48.png")
        '
        'Panel_NumberOfFeature
        '
        Me.Panel_NumberOfFeature.CanvasColor = System.Drawing.SystemColors.Control
        Me.Panel_NumberOfFeature.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.Panel_NumberOfFeature.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel_NumberOfFeature.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel_NumberOfFeature.Location = New System.Drawing.Point(0, 0)
        Me.Panel_NumberOfFeature.Name = "Panel_NumberOfFeature"
        Me.Panel_NumberOfFeature.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Panel_NumberOfFeature.Size = New System.Drawing.Size(403, 25)
        Me.Panel_NumberOfFeature.Style.Alignment = System.Drawing.StringAlignment.Center
        Me.Panel_NumberOfFeature.Style.BackColor1.Color = System.Drawing.Color.DarkTurquoise
        Me.Panel_NumberOfFeature.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2
        Me.Panel_NumberOfFeature.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine
        Me.Panel_NumberOfFeature.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder
        Me.Panel_NumberOfFeature.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText
        Me.Panel_NumberOfFeature.Style.GradientAngle = 90
        Me.Panel_NumberOfFeature.TabIndex = 16
        '
        'ContextMenu_LstResult
        '
        Me.ContextMenu_LstResult.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.ContextMenu_LstResult.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectToolStripMenuItem, Me.UnSelectToolStripMenuItem, Me.ToolStripSeparator1, Me.ZoomInToolStripMenuItem, Me.BookmarkToolStripMenuItem1, Me.ToolStripSeparator2})
        Me.ContextMenu_LstResult.Name = "ContextMenuStrip1"
        Me.ContextMenu_LstResult.Size = New System.Drawing.Size(121, 104)
        '
        'SelectToolStripMenuItem
        '
        Me.SelectToolStripMenuItem.Image = Global.WaterEngine_AnalysisNetwork.My.Resources.Resources.SelectFeature
        Me.SelectToolStripMenuItem.Name = "SelectToolStripMenuItem"
        Me.SelectToolStripMenuItem.Size = New System.Drawing.Size(120, 22)
        Me.SelectToolStripMenuItem.Text = "Select"
        '
        'UnSelectToolStripMenuItem
        '
        Me.UnSelectToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.UnSelectToolStripMenuItem.Image = Global.WaterEngine_AnalysisNetwork.My.Resources.Resources.ClearSelection
        Me.UnSelectToolStripMenuItem.Name = "UnSelectToolStripMenuItem"
        Me.UnSelectToolStripMenuItem.Size = New System.Drawing.Size(120, 22)
        Me.UnSelectToolStripMenuItem.Text = "UnSelect"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(117, 6)
        '
        'ZoomInToolStripMenuItem
        '
        Me.ZoomInToolStripMenuItem.Image = Global.WaterEngine_AnalysisNetwork.My.Resources.Resources.ZoomIn
        Me.ZoomInToolStripMenuItem.Name = "ZoomInToolStripMenuItem"
        Me.ZoomInToolStripMenuItem.Size = New System.Drawing.Size(120, 22)
        Me.ZoomInToolStripMenuItem.Text = "Zoom"
        '
        'BookmarkToolStripMenuItem1
        '
        Me.BookmarkToolStripMenuItem1.Image = Global.WaterEngine_AnalysisNetwork.My.Resources.Resources.Bookmark16
        Me.BookmarkToolStripMenuItem1.Name = "BookmarkToolStripMenuItem1"
        Me.BookmarkToolStripMenuItem1.Size = New System.Drawing.Size(120, 22)
        Me.BookmarkToolStripMenuItem1.Text = "Bookmark"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(117, 6)
        '
        'SearchFeatures_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CaptionFont = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ClientSize = New System.Drawing.Size(403, 280)
        Me.Controls.Add(Me.SplitContainer1)
        Me.DoubleBuffered = True
        Me.EnableGlass = False
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "SearchFeatures_Form"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SearchFeatures_Form"
        Me.TitleText = "Search Features"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        Me.PanelEx1.ResumeLayout(False)
        Me.PanelEx1.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelEx4.ResumeLayout(False)
        Me.PanelEx2.ResumeLayout(False)
        Me.PanelEx3.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.ResumeLayout(False)
        Me.ContextMenu_LstResult.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents PanelEx2 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents btnNewSearch As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnFind As DevComponents.DotNetBar.ButtonX
    Friend WithEvents btnClose As DevComponents.DotNetBar.ButtonX
    Friend WithEvents PanelEx3 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents PanelEx5 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents PanelEx1 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents cboLayer As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents txtFind As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PanelEx4 As DevComponents.DotNetBar.PanelEx
    Friend WithEvents cboField As DevComponents.DotNetBar.Controls.ComboBoxEx
    Friend WithEvents rbtnInField As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents rbtnAllField As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents ContextMenu_LstResult As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SelectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UnSelectToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ZoomInToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BookmarkToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SplitContainer4 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel_NumberOfFeature As DevComponents.DotNetBar.PanelEx
    Private WithEvents lstResult As DevComponents.DotNetBar.Controls.ListViewEx
    Private WithEvents columnHeader1 As System.Windows.Forms.ColumnHeader
    Private WithEvents columnHeader2 As System.Windows.Forms.ColumnHeader
    Private WithEvents columnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
End Class

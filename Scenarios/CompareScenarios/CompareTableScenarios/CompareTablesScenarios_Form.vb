Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Geodatabase
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports System.Windows.Forms

Public Class CompareTablesScenarios_Form
    Private FirstScenarioName, SecondScenarioName As String
    Private m_hookHelper As IHookHelper = Nothing
    Private m_FCPipe As IFeatureClass
    Private TableScenario, TableEdgeCompare, TableJuncCompare, TablePatterns As ITable
    ' Private CalculateHeadloss As CalculateHeadlossJunction_Cls
    Private ResultMessage As String
    Private ProgressBar_MainForm As ProgressBar
    Private WithEvents FrmReporting As Reporting_Form
    Private Coll_ColumnsPipes, Coll_ColumnsJuncs As Collection
    Private FDataSetPipes, FDataSetJuncs As DataSet
    Private Coll_Hours As Collection
    Private ArrayDebiFirst(), ArrayVelocityFirst(), ArrayDebiSecond(), ArrayVelocitySecond() As Double
    Private IndexArray As Integer
    Private PipeCollection, JuncCollection As Collection
    Private NumPipesGraphic, NumJuncGraphic As Integer

    Public Sub New(ByVal hookHelper As IHookHelper)
        InitializeComponent()
        m_hookHelper = hookHelper
    End Sub

    Public WriteOnly Property Get_FirstScenarioName() As String
        Set(ByVal value As String)
            FirstScenarioName = value
        End Set
    End Property

    Public WriteOnly Property Get_SecondScenarioName() As String
        Set(ByVal value As String)
            SecondScenarioName = value
        End Set
    End Property

    Public WriteOnly Property Set_Prog() As ProgressBar
        Set(ByVal value As ProgressBar)
            ProgressBar_MainForm = value
        End Set
    End Property

    Private Sub CompareTablesScenarios_Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        On Error GoTo Err


        Dim RowCustomize As IRow
        ResultMessage = ""

        If m_TableCustomize Is Nothing OrElse m_TableScenarios Is Nothing OrElse m_TableJuncScenarios Is Nothing OrElse m_TableEdgeScenarios Is Nothing Then
            ResultMessage = "Please complete the basic configuration of software"
            Show_AlertCustom(ResultMessage)
            Exit Sub
        End If
        TableScenario = m_TableScenarios
        TableJuncCompare = m_TableJuncScenarios
        TableEdgeCompare = m_TableEdgeScenarios

        RowCustomize = m_TableCustomize.GetRow(1)

        If RowCustomize Is Nothing Then

            DoubleInput_lowVelocity.Value = 2.5
            DoubleInput_lowVelocity.MaxValue = 2.5
            DoubleInput_lowVelocity.MinValue = 0.2

            DoubleInput_HighVelocity.Value = 0.2
            DoubleInput_HighVelocity.MaxValue = 2.5
            DoubleInput_HighVelocity.MinValue = 0.2

            DoubleInput_lowPressure.Value = 50
            DoubleInput_lowPressure.MaxValue = 50
            DoubleInput_lowPressure.MinValue = 20

            DoubleInput_HighPressure.Value = 20
            DoubleInput_HighPressure.MaxValue = 50
            DoubleInput_HighPressure.MinValue = 20

        Else
            DoubleInput_lowVelocity.Value = RowCustomize.Value(m_TableCustomize.FindField("Most_Velocity"))
            DoubleInput_lowVelocity.MaxValue = RowCustomize.Value(m_TableCustomize.FindField("Most_Velocity"))
            DoubleInput_lowVelocity.MinValue = RowCustomize.Value(m_TableCustomize.FindField("Less_Velocity"))

            DoubleInput_HighVelocity.Value = RowCustomize.Value(m_TableCustomize.FindField("Less_Velocity"))
            DoubleInput_HighVelocity.MaxValue = RowCustomize.Value(m_TableCustomize.FindField("Most_Velocity"))
            DoubleInput_HighVelocity.MinValue = RowCustomize.Value(m_TableCustomize.FindField("Less_Velocity"))

            DoubleInput_lowPressure.Value = RowCustomize.Value(m_TableCustomize.FindField("Most_Pressure_Juncs"))
            DoubleInput_lowPressure.MaxValue = RowCustomize.Value(m_TableCustomize.FindField("Most_Pressure_Juncs"))
            DoubleInput_lowPressure.MinValue = RowCustomize.Value(m_TableCustomize.FindField("Less_Pressure_Juncs"))

            DoubleInput_HighPressure.Value = RowCustomize.Value(m_TableCustomize.FindField("Less_Pressure_Juncs"))
            DoubleInput_HighPressure.MaxValue = RowCustomize.Value(m_TableCustomize.FindField("Most_Pressure_Juncs"))
            DoubleInput_HighPressure.MinValue = RowCustomize.Value(m_TableCustomize.FindField("Less_Pressure_Juncs"))
        End If

        ProgressBar(ProgressBar_MainForm.Value, 90 - ProgressBar_MainForm.Value)
        Initialize_ListViewsEdges()
        Initialize_ListViewsJuncs()

        Initialize_CompareScenarios()
        ' ProgressBar(ProgressBar_MainForm.Value, 10)
     
        ProgressBar_MainForm.Value = 0
        Exit Sub
Err:
        If ResultMessage = "" Then
            ResultMessage = "Failure in show result"
        End If
        ProgressBar_MainForm.Value = 0
        Show_AlertCustom(ResultMessage)
    End Sub

    Private Sub ProgressBar(ByVal Value As Integer, ByVal StepProg As Integer)
        ProgressBar_MainForm.Value = Value
        ProgressBar_MainForm.Step = StepProg
        ProgressBar_MainForm.PerformStep()
        Application.DoEvents()
    End Sub

    Private Sub Initialize_ListViewsEdges()
        On Error GoTo Err

        Dim Fields As IFields
        Dim CursorTable As ICursor
        Dim RowCount As Integer
        Dim Row As IRow
        Dim IndexVelocity1Field As Integer
        Dim IndexVelocity2Field As Integer

        Fields = TableEdgeCompare.Fields


        IndexVelocity1Field = TableEdgeCompare.FindField("VelocityPipe1")
        CursorTable = TableEdgeCompare.Search(Nothing, False)
        RowCount = TableEdgeCompare.RowCount(Nothing)

        For i = 0 To RowCount - 1
            Row = CursorTable.NextRow
            Dim NumPipe As Integer = Row.Value(TableEdgeCompare.FindField("NumPipe"))
            Dim Item As New ListViewItem(NumPipe)

            Item.SubItems.Add(CStr(Math.Round(Row.Value(TableEdgeCompare.FindField("LengthPipe1")), 2)))
            Item.SubItems.Add(CStr(Math.Round(Row.Value(TableEdgeCompare.FindField("DiameterPipe1")), 2)))
            Item.SubItems.Add(Row.Value(TableEdgeCompare.FindField("TypePipe1")))
            Item.SubItems.Add(CStr(Math.Round(Row.Value(TableEdgeCompare.FindField("C_HazenPipe1")), 2)))
            Item.SubItems.Add(CStr(Math.Round(Row.Value(TableEdgeCompare.FindField("DischargePipe1")), 2)))
            Item.SubItems.Add(CStr(Math.Round(Row.Value(TableEdgeCompare.FindField("VelocityPipe1")), 3)))
            Item.SubItems.Add(CStr(Math.Round(Row.Value(TableEdgeCompare.FindField("HeadlossALLPipe1")), 4)))

            If Math.Abs(Row.Value(IndexVelocity1Field)) < 0.2 Or Math.Abs(Row.Value(IndexVelocity1Field)) > 3 Then
                Item.BackColor = Color.Plum
            Else
                Item.BackColor = Color.White
            End If

            lstFirstEdgeScenario.Items.Add(Item)
        Next

        IndexVelocity2Field = TableEdgeCompare.FindField("VelocityPipe2")
        CursorTable = TableEdgeCompare.Search(Nothing, False)

        For i = 0 To RowCount - 1
            Row = CursorTable.NextRow
            Dim NumPipe As Integer = Row.Value(TableEdgeCompare.FindField("NumPipe"))
            Dim Item As New ListViewItem(NumPipe)

            Item.SubItems.Add(CStr(Math.Round(Row.Value(TableEdgeCompare.FindField("LengthPipe2")), 2)))
            Item.SubItems.Add(CStr(Math.Round(Row.Value(TableEdgeCompare.FindField("DiameterPipe2")), 2)))
            Item.SubItems.Add(Row.Value(TableEdgeCompare.FindField("TypePipe2")))
            Item.SubItems.Add(CStr(Math.Round(Row.Value(TableEdgeCompare.FindField("C_HazenPipe2")), 2)))
            Item.SubItems.Add(CStr(Math.Round(Row.Value(TableEdgeCompare.FindField("DischargePipe2")), 2)))
            Item.SubItems.Add(CStr(Math.Round(Row.Value(TableEdgeCompare.FindField("VelocityPipe2")), 3)))
            Item.SubItems.Add(CStr(Math.Round(Row.Value(TableEdgeCompare.FindField("HeadlossALLPipe2")), 4)))

            If Math.Abs(Row.Value(IndexVelocity2Field)) < 0.2 Or Math.Abs(Row.Value(IndexVelocity2Field)) > 3 Then
                Item.BackColor = Color.Plum
            Else
                Item.BackColor = Color.White
            End If

            lstSecondEdgeScenario.Items.Add(Item)
        Next

        Exit Sub
Err:
        If ResultMessage = "" Then
            ResultMessage = "Failure in show result"
        End If
    End Sub

    Private Sub Initialize_ListViewsJuncs()
        On Error GoTo Err

        Dim Fields As IFields
        Dim CursorTable As ICursor
        Dim RowCount As Integer
        Dim Row As IRow
        Dim IndexHeadloss1Field As Integer
        Dim IndexHeadloss2Field As Integer

        Fields = TableJuncCompare.Fields


        IndexHeadloss1Field = TableJuncCompare.FindField("HeadLossJunc1")
        CursorTable = TableJuncCompare.Search(Nothing, False)
        RowCount = TableJuncCompare.RowCount(Nothing)

        For i = 0 To RowCount - 1
            Row = CursorTable.NextRow
            Dim NumJunc As Integer = Row.Value(TableJuncCompare.FindField("NumJunc"))
            Dim Item As New ListViewItem(NumJunc)

            Item.SubItems.Add(CStr(Math.Round(Row.Value(TableJuncCompare.FindField("ElevationJunc1")), 2)))
            Item.SubItems.Add(CStr(Math.Round(Row.Value(TableJuncCompare.FindField("DemandJunc1")), 2)))
            Item.SubItems.Add(CStr(Math.Round(Row.Value(TableJuncCompare.FindField("HeadLossJunc1")), 4)))

            If Math.Abs(Row.Value(IndexHeadloss1Field)) < 20 Or Math.Abs(Row.Value(IndexHeadloss1Field)) > 50 Then
                Item.BackColor = Color.Plum
            Else
                Item.BackColor = Color.White
            End If

            lstFirstJuncScenario.Items.Add(Item)
        Next

        CursorTable = TableJuncCompare.Search(Nothing, False)
        IndexHeadloss2Field = TableJuncCompare.FindField("HeadLossJunc2")

        For i = 0 To RowCount - 1
            Row = CursorTable.NextRow
            Dim NumJunc As Integer = Row.Value(TableJuncCompare.FindField("NumJunc"))
            Dim Item As New ListViewItem(NumJunc)

            Item.SubItems.Add(CStr(Math.Round(Row.Value(TableJuncCompare.FindField("ElevationJunc2")), 2)))
            Item.SubItems.Add(CStr(Math.Round(Row.Value(TableJuncCompare.FindField("DemandJunc2")), 2)))
            Item.SubItems.Add(CStr(Math.Round(Row.Value(TableJuncCompare.FindField("HeadLossJunc2")), 4)))

            If Math.Abs(Row.Value(IndexHeadloss2Field)) < 20 Or Math.Abs(Row.Value(IndexHeadloss2Field)) > 50 Then
                Item.BackColor = Color.Plum
            Else
                Item.BackColor = Color.White
            End If

            lstSecondJuncScenario.Items.Add(Item)
        Next
        Exit Sub
Err:
        If ResultMessage = "" Then
            ResultMessage = "Failure in show result"
        End If
    End Sub

    Private Sub Initialize_CompareScenarios()
        On Error GoTo Err
        Dim Row As IRow

        ' lblNameEdgeScenario1.Text = " Name of scenario :" & "      " & FirstScenarioName
        'lblNameJuncScenario1.Text = " Name of scenario :" & "      " & FirstScenarioName
        ' lblNameEdgeScenario2.Text = " Name of scenario :" & "      " & SecondScenarioName
        ' lblNameJuncScenario2.Text = " Name of scenario :" & "      " & SecondScenarioName

        If FirstScenarioName = "Base Scenario" Then
            ' Scenario1
            lblFirstScenarioName.Text = "Base Scenario"
            lblDateScenario1.Text = Date.Today
        Else
            Row = Find_IDScenario(FirstScenarioName)
            If Row Is Nothing Then
                ' ResultMessage = "اشکال در یافتن اطلاعات مربوط به سناریو اول"
                ResultMessage = "Failure in finding first scenario"
                GoTo Err
            End If
            ' Scenario1
            lblFirstScenarioName.Text = FirstScenarioName
            lblDateScenario1.Text = Row.Value(TableScenario.FindField("DateScenario"))
        End If

        If SecondScenarioName = "Base Scenario" Then
            ' Scenario1
            lblSecondScenarioName.Text = "Base Scenario"
            lblDateScenario2.Text = Date.Today
        Else

            Row = Find_IDScenario(SecondScenarioName)
            If Row Is Nothing Then
                ' ResultMessage = "اشکال در یافتن اطلاعات مربوط به سناریو دوم"
                ResultMessage = "Failure in finding second scenario"
                GoTo Err
            End If
            'Scenario2
            lblSecondScenarioName.Text = SecondScenarioName
            lblDateScenario2.Text = Row.Value(TableScenario.FindField("DateScenario"))
        End If
        Exit Sub
Err:
        If ResultMessage = "" Then
            ResultMessage = "Failure in show result"
        End If
    End Sub

    Private Function Find_IDScenario(ByVal NameScenario As String) As IRow
        Dim Row As IRow
        Dim Cursor As ICursor
        Dim RowCount As Integer
        Dim IndexNameScenario As Integer

        Cursor = TableScenario.Search(Nothing, False)
        RowCount = TableScenario.RowCount(Nothing)
        IndexNameScenario = TableScenario.FindField("NameScenario")

        Find_IDScenario = Nothing
        Row = Cursor.NextRow
        Do Until Row Is Nothing
            If Row.Value(IndexNameScenario) = NameScenario Then
                Find_IDScenario = Row
                Exit Function
            End If
            Row = Cursor.NextRow
        Loop

        Marshal.ReleaseComObject(Cursor)
    End Function

    Private Sub Show_AlertCustom(ByVal MessageError As String)
        Dim AlertCustom As New AlertCustom_Cls
        AlertCustom.ShowLoadAlert(Me, MessageError)
    End Sub

    Private Sub CreateDataSetPipes()
        Dim ItemEdge As ListViewItem
        Me.FDataSetPipes = New DataSet
        Dim table As New DataTable
        table.TableName = "EdgeResult"
        Me.FDataSetPipes.Tables.Add(table)

        table.Columns.Add("IDPipes", GetType(String))
        table.Columns.Add("Length", GetType(String))
        table.Columns.Add("Diameter", GetType(String))
        table.Columns.Add("Type", GetType(String))
        table.Columns.Add("Hazen", GetType(String))
        table.Columns.Add("Debi", GetType(String))
        table.Columns.Add("Velocity", GetType(String))
        table.Columns.Add("HeadlossInMeter", GetType(String))
        table.Columns.Add("HeadlossInAllPipe", GetType(String))

        '  For i = 0 To lstResultEdges.Items.Count - 1
        'ItemEdge = lstResultEdges.Items.Item(i)
        '   table.Rows.Add(ItemEdge.SubItems(0).Text, ItemEdge.SubItems(1).Text, ItemEdge.SubItems(2).Text, ItemEdge.SubItems(3).Text, ItemEdge.SubItems(4).Text, ItemEdge.SubItems(5).Text, ItemEdge.SubItems(6).Text, ItemEdge.SubItems(7).Text, ItemEdge.SubItems(8).Text)
        '  Next

    End Sub

    Private Sub CreateDataSetJuncs()
        Dim ItemJunc As ListViewItem
        Me.FDataSetJuncs = New DataSet
        Dim table As New DataTable
        table.TableName = "JuncResult"
        Me.FDataSetJuncs.Tables.Add(table)

        table.Columns.Add("NumJuncs", GetType(String))
        table.Columns.Add("Elevation", GetType(String))
        table.Columns.Add("Demand", GetType(String))
        table.Columns.Add("Pressure", GetType(String))

        '  For i = 0 To lstResultJuncs.Items.Count - 1
        'ItemJunc = lstResultJuncs.Items.Item(i)
        '   table.Rows.Add(ItemJunc.SubItems(0).Text, ItemJunc.SubItems(1).Text, ItemJunc.SubItems(2).Text, ItemJunc.SubItems(3).Text)
        '  Next

    End Sub
  
    Private Sub BtnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CreateDataSetPipes()
        CreateDataSetJuncs()
        If FrmReporting Is Nothing Then
            FrmReporting = New Reporting_Form(m_hookHelper)
            FrmReporting.Set_CollColumnsPipe = Coll_ColumnsPipes
            FrmReporting.Set_CollColumnsJuncs = Coll_ColumnsJuncs
            FrmReporting.Set_FDataSetPipes = FDataSetPipes
            FrmReporting.Set_FDataSetJuncs = FDataSetJuncs
            FrmReporting.Show()
        End If
    End Sub

    Private Sub Fill_ComponentsWithPatterns()
        On Error GoTo Err

        Dim HourMabna As Integer
        Dim RowPattern As IRow
        Dim RowPatternCursor As ICursor
        Dim RowPatternCount As Integer

        IndexArray = 0
        PipeCollection = New Collection
        JuncCollection = New Collection

        TablePatterns = m_TablePattern
        If TablePatterns Is Nothing Then
            ResultMessage = "Please ,First Create a pattern and set that as Default pattern"
            GoTo Err
        End If

        RowPatternCount = TablePatterns.RowCount(Nothing)
        If RowPatternCount = 0 Then
            ResultMessage = "Please ,First Create a pattern and set that as Default pattern"
            GoTo Err
        End If
        RowPatternCursor = TablePatterns.Search(Nothing, False)
        RowPattern = RowPatternCursor.NextRow
        While RowPattern IsNot Nothing
            If RowPattern.HasOID Then
                If RowPattern.Value(TablePatterns.FindField("SetDefault")) = 1 Then
                    Exit While
                End If
            End If
            RowPattern = RowPatternCursor.NextRow
        End While
        If RowPattern Is Nothing Then
            ResultMessage = "Please ,First Create a pattern and set that as Default pattern"
            GoTo Err
        End If

        Coll_Hours = New Collection
        For i = 2 To TablePatterns.Fields.FieldCount - 3
            Coll_Hours.Add(RowPattern.Value(i))
        Next

        FillListViewResultEdges_WithPatterns()
        FillListViewResultJuncs_WithPatterns()
        HourMabna = RowPattern.Value(TablePatterns.FindField("HourMabnaEdit"))

        If HourMabna = 0 Then
            TrackBar.Value = 12
        Else
            If TrackBar.Value = HourMabna Then
                TrackBar.Value = HourMabna - 1
            End If
            TrackBar.Value = HourMabna
        End If

        Exit Sub
Err:
        If ResultMessage = "" Then
            ResultMessage = "Failure in show result"
        End If
        Show_AlertCustom(ResultMessage)
    End Sub

    Private Sub FillListViewResultEdges_WithPatterns()
        On Error GoTo Err

        Dim FactorDebi As Double
        Dim FeatureCursor As IFeatureCursor
        Dim FeatureCount As Integer
        Dim Feature As IFeature
        Dim IndexOIDField As Integer
        Dim ItemDebi, ItemVelocity, ItemHeadloss, ItemAllHeadloss As String


        m_FCPipe = m_FCMainPipe

        lstFirstEdgeScenario.GridLines = True
        lstFirstEdgeScenario.FullRowSelect = True
        lstFirstEdgeScenario.Items.Clear()
        FeatureCursor = m_FCPipe.Search(Nothing, False)
        FeatureCount = m_FCPipe.FeatureCount(Nothing)
        If FeatureCount = 0 Then Exit Sub
        IndexOIDField = m_FCPipe.FindField(m_FCPipe.OIDFieldName)
        For i = 0 To FeatureCount - 1
            Feature = FeatureCursor.NextFeature
            If Feature.HasOID Then
                Dim Item As New ListViewItem(CStr("P_" & Feature.Value(IndexOIDField)))

                Item.SubItems.Add(CStr(Math.Round(Feature.Value(m_IndexMainPipeLength), 2)))
                Item.SubItems.Add(CStr(Feature.Value(m_IndexMainPipeDaimeter)))
                Select Case Feature.Value(m_IndexMainPipeMaterial)
                    Case 1
                        Item.SubItems.Add("Cement")
                    Case 2
                        Item.SubItems.Add("Asbestos")
                    Case 3
                        Item.SubItems.Add("Steel")
                    Case 4
                        Item.SubItems.Add("Ductile")
                    Case 5
                        Item.SubItems.Add("Galvanized")
                    Case 6
                        Item.SubItems.Add("Plyka")
                    Case 7
                        Item.SubItems.Add("Cast-iron")
                    Case 8
                        Item.SubItems.Add("Copper")
                    Case 9
                        Item.SubItems.Add("PE")
                    Case 10
                        Item.SubItems.Add("Other")
                End Select
                Item.SubItems.Add(CStr(Feature.Value(m_IndexMainPipeCHaizen)))
                ItemDebi = CStr(Math.Round(Feature.Value(m_IndexMainPipeDebi), 3))
                Item.SubItems.Add(ItemDebi)
                ReDim Preserve ArrayDebiFirst(IndexArray)
                ArrayDebiFirst(IndexArray) = CDbl(ItemDebi)

                ItemVelocity = CStr(Math.Round(Feature.Value(m_IndexMainPipeVelocity), 3))
                Item.SubItems.Add(ItemVelocity)
                ReDim Preserve ArrayVelocityFirst(IndexArray)
                ArrayVelocityFirst(IndexArray) = CDbl(ItemVelocity)
                IndexArray += 1
                Item.SubItems.Add("")
                Item.SubItems.Add("")
                lstFirstEdgeScenario.Items.Add(Item)
                PipeCollection.Add(Feature)
            End If
        Next

        Exit Sub
Err:
        ResultMessage = "Failure in show result"
    End Sub

    Private Sub FillListViewResultJuncs_WithPatterns()
        On Error GoTo Err

        Dim FeatureCursor As IFeatureCursor
        Dim FeatureCount As Integer
        Dim Feature As IFeature
        Dim IndexOIDField As Integer

        lstFirstJuncScenario.Items.Clear()
        FeatureCursor = m_FCJunction.Search(Nothing, False)
        FeatureCount = m_FCJunction.FeatureCount(Nothing)
        If FeatureCount = 0 Then Exit Sub
        IndexOIDField = m_FCJunction.FindField(m_FCJunction.OIDFieldName)

        For i = 0 To FeatureCount - 1
            Feature = FeatureCursor.NextFeature
            If Feature.HasOID Then
                Dim Item As New ListViewItem(CStr("J_" & Feature.Value(IndexOIDField)))
                Item.SubItems.Add(CStr(Feature.Value(m_IndexJunctionElevation)))
                Item.SubItems.Add(CStr(Math.Round(Feature.Value(m_IndexJunctionCollMasraf), 3)))
                Item.SubItems.Add(CStr(Math.Abs(Math.Round(Feature.Value(m_IndexJunctionPressure), 3))))
                lstFirstJuncScenario.Items.Add(Item)
                If CDbl(Item.SubItems(3).Text) > DoubleInput_lowPressure.Value Or CDbl(Item.SubItems(3).Text) < DoubleInput_HighPressure.Value Then
                    Item.BackColor = Drawing.Color.Plum
                Else
                    Item.BackColor = Drawing.Color.White
                End If
                JuncCollection.Add(Feature)
            End If
        Next

        Exit Sub
Err:
        ResultMessage = "Failure in show result"
    End Sub

    Private Sub TrackBar_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TrackBar.ValueChanged
        On Error GoTo Err

        Dim FactorDebi As Double
        Dim Item As ListViewItem
        Dim b_LowVelocity, b_HighVelocity, b_LowHighVelocity, b_UnCheck As Boolean
        Dim dbl_LowVelocity, dbl_HighVelocity As Double

        FactorDebi = CType(Coll_Hours.Item(TrackBar.Value + 1), Double)

        If FactorDebi = 0 Then
            Show_AlertCustom("Please, Choose another hour ,Beacause value in this hour is 0")
            Exit Sub
        End If

        b_LowVelocity = rbtnLess_Velocity.Checked
        dbl_LowVelocity = DoubleInput_lowVelocity.Value
        b_HighVelocity = rbtnHigh_Velocity.Checked
        dbl_HighVelocity = DoubleInput_HighVelocity.Value
        b_LowHighVelocity = rbtnHigh_Velocity.Checked And rbtnLess_Velocity.Checked
        b_UnCheck = Not (rbtnHigh_Velocity.Checked Or rbtnLess_Velocity.Checked)
        NumPipesGraphic = 0

        For i = 0 To lstFirstEdgeScenario.Items.Count - 1
            Item = lstFirstEdgeScenario.Items.Item(i)

            Item.SubItems(7).Text = ""
            Item.SubItems(8).Text = ""

            Item.SubItems(5).Text = ArrayDebiFirst(i) * FactorDebi
            Item.SubItems(6).Text = ArrayVelocityFirst(i) * FactorDebi
            Item.SubItems(7).Text = CStr(Math.Round((6.78 / Math.Pow((CDbl(Item.SubItems(2).Text) * 0.001), 1.165)) * (Math.Pow(((CDbl(Item.SubItems(6).Text) * FactorDebi) / CDbl(Item.SubItems(4).Text)), 1.85)), 4))
            Item.SubItems(8).Text = CStr(Math.Round(CDbl(Item.SubItems(7).Text) * CDbl(Item.SubItems(1).Text), 3))

            If b_LowHighVelocity Then
                If CDbl(Item.SubItems(6).Text) > dbl_LowVelocity Or CDbl(Item.SubItems(6).Text) < dbl_HighVelocity Then
                    Item.BackColor = Drawing.Color.Plum
                    NumPipesGraphic += 1
                Else
                    Item.BackColor = Drawing.Color.White
                End If
            Else
                If b_LowVelocity Then
                    If CDbl(Item.SubItems(6).Text) > dbl_LowVelocity Then
                        Item.BackColor = Drawing.Color.Plum
                        NumPipesGraphic += 1
                    Else
                        Item.BackColor = Drawing.Color.White
                    End If
                ElseIf b_HighVelocity Then
                    If CDbl(Item.SubItems(6).Text) < dbl_HighVelocity Then
                        Item.BackColor = Drawing.Color.Plum
                        NumPipesGraphic += 1
                    Else
                        Item.BackColor = Drawing.Color.White
                    End If
                ElseIf b_UnCheck Then
                    Item.BackColor = Drawing.Color.White
                End If
            End If
        Next
        lblNumPipeFeaturesFirst.Text = NumPipesGraphic
        Exit Sub
Err:
        Show_AlertCustom("Failure in show result")
    End Sub
End Class
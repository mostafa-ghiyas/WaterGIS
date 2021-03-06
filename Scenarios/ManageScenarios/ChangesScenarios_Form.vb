Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geodatabase
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.Geometry
Imports System.Windows.Forms

Public Class ChangesScenarios_Form
    Private m_Map As IMap
    Private m_Editor As IEngineEditor
    Private m_FCPipe As IFeatureClass
    Private m_TableCompareScenario As ITable
    Private BaseScenarioNode As New TreeNode
    Private ScenarioName As String
    Private m_hookHelper As IHookHelper = Nothing
    Private WithEvents m_ActiveViewEventsSelectionChanged As IActiveViewEvents_Event
    Private ResultMessage As String

    Public Sub New(ByVal hookHelper As IHookHelper)
        InitializeComponent()
        m_hookHelper = hookHelper
    End Sub

    Public WriteOnly Property Get_ScenarioName() As String
        Set(ByVal value As String)
            ScenarioName = value
        End Set
    End Property

    Private Sub ChangesScenarios_Form_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        RemoveHandler m_ActiveViewEventsSelectionChanged.SelectionChanged, AddressOf OnSelectionChanged
        m_ActiveViewEventsSelectionChanged = Nothing
    End Sub


    Private Sub ChangesScenarios_Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        On Error GoTo Err

        If m_hookHelper Is Nothing Then Exit Sub
        m_Map = TryCast(m_hookHelper.FocusMap, IMap)
        If m_Map Is Nothing Then Exit Sub
        ResultMessage = ""

        m_FCPipe = m_FCMainPipe
        If m_FCPipe Is Nothing OrElse m_FCUsers Is Nothing OrElse m_FCJunction Is Nothing Then
            ResultMessage = "Please complete the basic configuration of software"
            GoTo Err
        End If

        m_ActiveViewEventsSelectionChanged = m_Map
        AddHandler m_ActiveViewEventsSelectionChanged.SelectionChanged, AddressOf OnSelectionChanged
        Initialize_CboMaterial()
        Initialize_CboIDPipeFeatures()

        Initialize_CboIDJuncFeatures()
        Initialize_LstChanges()
        Exit Sub
Err:
        If ResultMessage = "" Then
            ResultMessage = "Failure in load parameters for set changes"
        End If
        Show_AlertCustom(ResultMessage)
    End Sub

    Private Sub OnSelectionChanged()
        Dim EnumFeature As IEnumFeature = m_Map.FeatureSelection
        Dim Feature As IFeature
        Dim FeatureGeo As IGeometry
        Dim FindPipe, FindJunc As Boolean
        FindJunc = False
        FindPipe = False

        If m_Map.SelectionCount > 0 Then
            Feature = EnumFeature.Next
            While Feature IsNot Nothing
                FeatureGeo = Feature.ShapeCopy
                If Not FindPipe Then
                    If Feature.FeatureType = esriFeatureType.esriFTComplexEdge Then
                        If Feature.Class Is m_FCMainPipe Then
                            cboIDPipeFeatures.Text = Feature.OID
                            FindPipe = True
                        End If
                    End If
                End If
                If Not FindJunc Then
                    If Feature.FeatureType = esriFeatureType.esriFTSimpleJunction Then
                        If Feature.Class Is m_FCJunction Then
                            cboIDJuncFeatures.Text = Feature.OID
                            FindJunc = True
                        End If
                    End If
                End If
                Feature = EnumFeature.Next
            End While
        End If
    End Sub

    Private Sub Initialize_CboIDPipeFeatures()
        On Error GoTo Err

        Dim FCPipe As IFeatureClass
        Dim FCursor As IFeatureCursor
        Dim Feature As IFeature

        cboIDPipeFeatures.Items.Clear()
        FCPipe = m_FCPipe
        FCursor = FCPipe.Search(Nothing, False)
        Feature = FCursor.NextFeature
        While Feature IsNot Nothing
            If Feature.HasOID Then
                cboIDPipeFeatures.Items.Add(Feature.OID)
            End If
            Feature = FCursor.NextFeature
        End While

        cboIDPipeFeatures.SelectedIndex = 0
        Marshal.ReleaseComObject(FCursor)
        Exit Sub
Err:
        If ResultMessage = "" Then
            ResultMessage = "Failure in load parameters in MainPipe Layer for set changes"
        End If
    End Sub

    Private Sub Initialize_CboIDJuncFeatures()
        On Error GoTo Err

        Dim FCJuncion As IFeatureClass
        Dim FCursor As IFeatureCursor
        Dim Feature As IFeature

        cboIDJuncFeatures.Items.Clear()
        FCJuncion = m_FCJunction
        FCursor = FCJuncion.Search(Nothing, False)

        Feature = FCursor.NextFeature
        While Feature IsNot Nothing
            If Feature.HasOID Then
                cboIDJuncFeatures.Items.Add(Feature.OID)
            End If
            Feature = FCursor.NextFeature
        End While

        cboIDJuncFeatures.SelectedIndex = 0
        Marshal.ReleaseComObject(FCursor)
        Exit Sub
Err:
        If ResultMessage = "" Then
            ResultMessage = "Failure in load parameters in Junction Layer for set changes"
        End If
    End Sub

    Private Sub Initialize_CboMaterial()
        cboMaterial.Items.Add("Cement")
        cboMaterial.Items.Add("Asbestos")
        cboMaterial.Items.Add("Steel")
        cboMaterial.Items.Add("Ductile")
        cboMaterial.Items.Add("Galvanized")
        cboMaterial.Items.Add("Plyka")
        cboMaterial.Items.Add("Cast-iron")
        cboMaterial.Items.Add("Copper")
        cboMaterial.Items.Add("PE")
        cboMaterial.Items.Add("Other")
        cboMaterial.SelectedIndex = 0
    End Sub

    Private Sub Initialize_LstChanges()
        On Error GoTo Err

        Dim IDScenario As Integer
        Dim Row As IRow
        Dim Cursor As ICursor
        Dim RowCount As Integer


        ' Initialize ListViewPipes

        IDScenario = Find_IDScenario()
        If IDScenario = -1 Then Exit Sub

        Cursor = m_TableIntermediateScenario.Search(Nothing, False)
        RowCount = m_TableIntermediateScenario.RowCount(Nothing)

        For i = 0 To RowCount - 1
            Row = Cursor.NextRow

            If Row.Value(m_TableIntermediateScenario.FindField("NumScenario")) = IDScenario Then
                If Row.Value(m_TableIntermediateScenario.FindField("NumPipe")) <> -1 Then
                    Dim IDFeature As Integer = Row.Value(m_TableIntermediateScenario.FindField("NumPipe"))
                    Dim Item As New ListViewItem(IDFeature)
                    Item.SubItems.Add(Row.Value(m_TableIntermediateScenario.FindField("DiameterPipe")))
                    Item.SubItems.Add(Row.Value(m_TableIntermediateScenario.FindField("LengthPipe")))
                    Item.SubItems.Add(Row.Value(m_TableIntermediateScenario.FindField("HazenCPipe")))
                    Item.SubItems.Add(Row.Value(m_TableIntermediateScenario.FindField("MaterialPipe")))
                    lstChangesPipes.Items.Add(Item)
                End If
            End If
        Next


        ' Initialize ListViewJunctions

        Cursor = m_TableIntermediateScenario.Search(Nothing, False)
        For i = 0 To RowCount - 1
            Row = Cursor.NextRow

            If Row.Value(m_TableIntermediateScenario.FindField("NumScenario")) = IDScenario Then
                If Row.Value(m_TableIntermediateScenario.FindField("NumJunc")) <> -1 Then
                    Dim IDFeature As Integer = Row.Value(m_TableIntermediateScenario.FindField("NumJunc"))
                    Dim Item As New ListViewItem(IDFeature)
                    Item.SubItems.Add(Row.Value(m_TableIntermediateScenario.FindField("ElevationJunc")))
                    lstChangesJunctions.Items.Add(Item)
                End If
            End If
        Next


        ' Initialize ListViewJunctions

        lstChangesJunctions.Columns.Add("                   شناسه کنتور کاربر", CInt(lstChangesJunctions.Width / 2), HorizontalAlignment.Left)
        lstChangesJunctions.Columns.Add("                  مصرف کاربر", CInt(lstChangesJunctions.Width / 2), HorizontalAlignment.Left)


        IDScenario = Find_IDScenario()
        If IDScenario = -1 Then Exit Sub

        Cursor = m_TableIntermediateScenario.Search(Nothing, False)
        RowCount = m_TableIntermediateScenario.RowCount(Nothing)
        For i = 0 To RowCount - 1
            Row = Cursor.NextRow
            If Row.Value(m_TableIntermediateScenario.FindField("NumScenario")) = IDScenario Then
                If Row.Value(m_TableIntermediateScenario.FindField("NumUser")) <> -1 Then
                    Dim IDFeature As Integer = Row.Value(m_TableIntermediateScenario.FindField("NumUser"))
                    Dim Item As New ListViewItem(IDFeature)
                    Item.SubItems.Add(Row.Value(m_TableIntermediateScenario.FindField("MasrafUser")))
                    lstChangesJunctions.Items.Add(Item)
                End If
            End If
        Next



        Marshal.ReleaseComObject(Cursor)
        Exit Sub
Err:
        If ResultMessage = "" Then
            'ResultMessage = "اشکال در بارگذاری اطلاعات لازم به منظور اعمال تغییرات"
            ResultMessage = "Failure in load layers"
        End If
    End Sub


    Private Function Find_IDScenario() As Integer
        Dim Row As IRow
        Dim Cursor As ICursor
        Dim RowCount As Integer
        Dim IndexNameScenario As Integer

        Cursor = m_TableScenarios.Search(Nothing, False)
        RowCount = m_TableScenarios.RowCount(Nothing)
        IndexNameScenario = m_TableScenarios.FindField("NameScenario")

        Find_IDScenario = -1
        Row = Cursor.NextRow

        Do Until Row Is Nothing
            If Row.Value(IndexNameScenario) = ScenarioName Then
                Find_IDScenario = Row.Value(m_TableScenarios.FindField(m_TableScenarios.OIDFieldName))
                Exit Function
            End If
            Row = Cursor.NextRow
        Loop
        Marshal.ReleaseComObject(Cursor)
    End Function

    Private Function SearchIn_TableScenarioIntermediate(ByVal IDFeature As Integer, ByVal IndexID As Integer) As IRow
        On Error GoTo Err

        Dim Row As IRow
        Dim Cursor As ICursor
        Dim RowCount As Integer
        Dim IDScenario As Integer
        '  Dim IndexNameScenario As Integer
        SearchIn_TableScenarioIntermediate = Nothing

        Cursor = m_TableIntermediateScenario.Search(Nothing, False)
        RowCount = m_TableIntermediateScenario.RowCount(Nothing)

        IDScenario = Find_IDScenario()
        If IDScenario = -1 Then Exit Function

        For i = 0 To RowCount - 1
            Row = Cursor.NextRow
            If Row.Value(1) = IDScenario Then
                If Row.Value(IndexID) = IDFeature Then
                    SearchIn_TableScenarioIntermediate = Row
                    Exit Function
                End If
            End If
        Next

        Marshal.ReleaseComObject(Cursor)
        Exit Function
Err:
        If ResultMessage = "" Then
            ResultMessage = "Failure in finding Scenario"
        End If
    End Function


    Private Sub Show_AlertCustom(ByVal MessageError As String)
        Dim AlertCustom As New AlertCustom_Cls
        AlertCustom.ShowLoadAlert(Me, MessageError)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnCreateChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateChange.Click
        On Error GoTo Err

        Dim WorkspaceEdit As IWorkspaceEdit
        Dim FCPipe As IFeatureClass
        Dim FCJunction As IFeatureClass
        Dim FCUser As IFeatureClass
        Dim Feature As IFeature
        Dim IndexDiameterField As Integer
        Dim IndexElevationFild As Integer
        Dim IndexHazenField As Integer
        Dim IndexTypeField As Integer
        Dim IndexMasrafField As Integer
        Dim IDScenario As Integer
        Dim Row As IRow

        FCPipe = m_FCPipe
        FCJunction = m_FCJunction
        FCUser = m_FCUsers

        ResultMessage = ""

        IDScenario = Find_IDScenario()
        If IDScenario = -1 Then
            '    ResultMessage = "اشکال در بارگذاری اطلاعات مربوط به سناریو"
            ResultMessage = "Failure in finding Scenario"
            GoTo Err
        End If

        Feature = FCPipe.GetFeature(CInt(cboIDPipeFeatures.Text))
        IndexDiameterField = m_IndexMainPipeDaimeter
        IndexHazenField = m_IndexMainPipeCHaizen
        IndexTypeField = m_IndexMainPipeMaterial
        IndexMasrafField = m_IndexUsersMasraf


        If IndexDiameterField = -1 Then
            ' ResultMessage = " فیلد قطر در بین فیلد های لوله یافت نشد"
            ResultMessage = "There isnot Diameter field" & vbCrLf & "Please complete the basic configuration of software "
            GoTo Err
        End If

        If IndexHazenField = -1 Then
            ' ResultMessage = "فیلد ثابت هیزن در بین فیلد های لوله یافت نشد"
            ResultMessage = "There isnot Hazen-C field" & vbCrLf & "Please complete the basic configuration of software "
            GoTo Err
        End If

        If IndexTypeField = -1 Then
            ' ResultMessage = "فیلد جنس لوله در بین فیلد ها یافت نشد"
            ResultMessage = "There isnot Material field" & vbCrLf & "Please complete the basic configuration of software "
            GoTo Err
        End If

        WorkspaceEdit = CType(FCPipe.FeatureDataset.Workspace, IWorkspaceEdit)

        If InputDblDiameter.Value <> Feature.Value(IndexDiameterField) Or InputDblLengthPipe.Value <> Math.Round(Feature.Value(FCPipe.FindField(FCPipe.LengthField.Name)), 2) Or InputIntHazen.Value <> Feature.Value(IndexHazenField) Or cboMaterial.SelectedIndex <> Feature.Value(IndexTypeField) - 1 Then


            WorkspaceEdit.StartEditing(True)
            WorkspaceEdit.StartEditOperation()

            Row = SearchIn_TableScenarioIntermediate(CInt(cboIDPipeFeatures.Text), m_TableIntermediateScenario.FindField("NumPipe"))
            If Row Is Nothing Then

                Row = m_TableIntermediateScenario.CreateRow
                Row.Value(m_TableIntermediateScenario.FindField("NumScenario")) = IDScenario
                Row.Value(m_TableIntermediateScenario.FindField("NumPipe")) = Feature.OID
                Row.Value(m_TableIntermediateScenario.FindField("DiameterPipe")) = InputDblDiameter.Value
                Row.Value(m_TableIntermediateScenario.FindField("LengthPipe")) = Math.Round(CDbl(InputDblLengthPipe.Value), 2)
                Row.Value(m_TableIntermediateScenario.FindField("HazenCPipe")) = InputIntHazen.Value
                Row.Value(m_TableIntermediateScenario.FindField("MaterialPipe")) = cboMaterial.Text
                Row.Value(m_TableIntermediateScenario.FindField("NumJunc")) = -1
                Row.Value(m_TableIntermediateScenario.FindField("ElevationJunc")) = -1
                Row.Value(m_TableIntermediateScenario.FindField("NumUser")) = -1
                Row.Value(m_TableIntermediateScenario.FindField("MasrafUser")) = -1
                Row.Store()

                Dim Item As New ListViewItem(Feature.OID)
                Item.SubItems.Add(InputDblDiameter.Value)
                Item.SubItems.Add(InputDblLengthPipe.Value)
                Item.SubItems.Add(InputIntHazen.Value)
                Item.SubItems.Add(cboMaterial.Text)
                lstChangesPipes.Items.Add(Item)

            Else

                Row.Value(m_TableIntermediateScenario.FindField("DiameterPipe")) = InputDblDiameter.Value
                Row.Value(m_TableIntermediateScenario.FindField("LengthPipe")) = Math.Round(CDbl(InputDblLengthPipe.Value), 2)
                Row.Value(m_TableIntermediateScenario.FindField("HazenCPipe")) = InputIntHazen.Value
                Row.Value(m_TableIntermediateScenario.FindField("MaterialPipe")) = cboMaterial.Text
                Row.Store()


                For i = 0 To lstChangesPipes.Items.Count - 1
                    Dim Item As ListViewItem = lstChangesPipes.Items.Item(i)
                    If Item.Text = Feature.OID Then
                        Item.SubItems(1).Text = Row.Value(m_TableIntermediateScenario.FindField("DiameterPipe"))
                        Item.SubItems(2).Text = Row.Value(m_TableIntermediateScenario.FindField("LengthPipe"))
                        Item.SubItems(3).Text = Row.Value(m_TableIntermediateScenario.FindField("HazenCPipe"))
                        Item.SubItems(4).Text = Row.Value(m_TableIntermediateScenario.FindField("MaterialPipe"))
                        Exit For
                    End If
                Next
            End If

            WorkspaceEdit.StopEditOperation()
            WorkspaceEdit.StopEditing(True)


        End If

        Feature = FCJunction.GetFeature(CInt(cboIDJuncFeatures.Text))
        IndexElevationFild = m_IndexJunctionElevation

        If IndexElevationFild = -1 Then
            ' ResultMessage = " فیلد ارتفاع در بین فیلد های گره ها یافت نشد"
            ResultMessage = "There isnot Elevation field" & vbCrLf & "Please complete the basic configuration of software "
            GoTo Err
        End If

        If InputDblElevation.Value <> Math.Round(Feature.Value(IndexElevationFild), 2) Then

            WorkspaceEdit.StartEditing(True)
            WorkspaceEdit.StartEditOperation()


            Row = SearchIn_TableScenarioIntermediate(CInt(cboIDJuncFeatures.Text), m_TableIntermediateScenario.FindField("NumJunc"))
            If Row Is Nothing Then

                Row = m_TableIntermediateScenario.CreateRow
                Row.Value(m_TableIntermediateScenario.FindField("NumScenario")) = IDScenario
                Row.Value(m_TableIntermediateScenario.FindField("NumPipe")) = -1
                Row.Value(m_TableIntermediateScenario.FindField("DiameterPipe")) = -1
                Row.Value(m_TableIntermediateScenario.FindField("LengthPipe")) = -1
                Row.Value(m_TableIntermediateScenario.FindField("HazenCPipe")) = -1
                Row.Value(m_TableIntermediateScenario.FindField("MaterialPipe")) = -1
                Row.Value(m_TableIntermediateScenario.FindField("NumJunc")) = Feature.OID
                Row.Value(m_TableIntermediateScenario.FindField("ElevationJunc")) = InputDblElevation.Value
                Row.Value(m_TableIntermediateScenario.FindField("NumUser")) = -1
                Row.Value(m_TableIntermediateScenario.FindField("MasrafUser")) = -1
                Row.Store()

                Dim Item As New ListViewItem(Feature.OID)
                Item.SubItems.Add(InputDblElevation.Value)
                lstChangesJunctions.Items.Add(Item)

            Else

                Row.Value(m_TableIntermediateScenario.FindField("ElevationJunc")) = InputDblElevation.Value
                Row.Store()

                For i = 0 To lstChangesJunctions.Items.Count - 1
                    Dim Item As ListViewItem = lstChangesJunctions.Items.Item(i)
                    If Item.Text = Feature.OID Then
                        Item.SubItems(1).Text = Row.Value(m_TableIntermediateScenario.FindField("ElevationJunc"))
                        Exit For
                    End If
                Next
            End If


            WorkspaceEdit.StopEditOperation()
            WorkspaceEdit.StopEditing(True)
        End If

        Exit Sub
Err:
        If ResultMessage = "" Then
            ResultMessage = "Failure in set changes"
        End If
        Show_AlertCustom(ResultMessage)
    End Sub

    Private Sub cboIDPipeFeatures_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboIDPipeFeatures.SelectedIndexChanged
        On Error GoTo Err

        Dim FCPipe As IFeatureClass
        Dim Feature As IFeature
        Dim IndexDiameterField As Integer
        Dim IndexCHazen As Integer
        Dim IndexTypeField As Integer

        FCPipe = m_FCPipe
        Feature = FCPipe.GetFeature(CInt(cboIDPipeFeatures.Text))
        IndexDiameterField = m_IndexMainPipeDaimeter
        IndexCHazen = m_IndexMainPipeCHaizen
        IndexTypeField = m_IndexMainPipeMaterial

        If IndexCHazen = -1 Then
            ' ResultMessage = "فیلد ثابت هیزن در بین فیلد های لوله یافت نشد"
            ResultMessage = "There isnot Hazen-C field" & vbCrLf & "Please complete the basic configuration of software "
            GoTo Err
        End If
        If IndexDiameterField = -1 Then
            ' ResultMessage = "فیلد قطر در بین فیلد های لوله یافت نشد"
            ResultMessage = "There isnot Diameter field" & vbCrLf & "Please complete the basic configuration of software "
            GoTo Err
        End If
        If IndexTypeField = -1 Then
            '   ResultMessage = "فیلد جنس لوله در بین فیلد ها یافت نشد"
            ResultMessage = "There isnot Material field" & vbCrLf & "Please complete the basic configuration of software "
            GoTo Err
        End If

        InputDblLengthPipe.Value = Math.Round(Feature.Value(FCPipe.FindField(FCPipe.LengthField.Name)), 2)
        InputDblDiameter.Value = Feature.Value(IndexDiameterField)
        InputIntHazen.Value = Feature.Value(IndexCHazen)
        cboMaterial.SelectedIndex = Feature.Value(IndexTypeField) - 1
        Exit Sub
Err:
        If ResultMessage = "" Then
            ' ResultMessage = "اشکال در بارگذاری اطلاعات لوله ها به منظور اعمال تغییرات"
            ResultMessage = "Failure in load MainPipe layer"
        End If
        Show_AlertCustom(ResultMessage)
    End Sub

    Private Sub cboIDJuncFeatures_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboIDJuncFeatures.SelectedIndexChanged
        On Error GoTo Err

        Dim FCJunction As IFeatureClass
        Dim Feature As IFeature
        Dim IndexElevationField As Integer

        FCJunction = m_FCJunction
        Feature = FCJunction.GetFeature(CInt(cboIDJuncFeatures.Text))
        IndexElevationField = m_IndexJunctionElevation

        If IndexElevationField = -1 Then
            ' ResultMessage = "فیلد ارتفاع در بین فیلدهای لوله یافت نشد"
            ResultMessage = "There isnot Elevation field" & vbCrLf & "Please complete the basic configuration of software "
            GoTo Err
        End If

        InputDblElevation.Value = Math.Round(Feature.Value(IndexElevationField), 2)
        Exit Sub
Err:
        If ResultMessage = "" Then
            ' ResultMessage = "اشکال در بارگذاری اطلاعات گره ها به منظور اعمال تغییرات"
            ResultMessage = "Failure in load junction layer"
        End If
        Show_AlertCustom(ResultMessage)
    End Sub
End Class
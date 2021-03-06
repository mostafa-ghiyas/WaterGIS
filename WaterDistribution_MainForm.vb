Imports Lorestan.MapTools
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Controls
Imports DevComponents.DotNetBar
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.SystemUI
Imports WaterEngine_AnalysisNetwork
Imports WaterEngine_DesignNetwork
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geometry


Public Class WaterDistribution_MainForm


#Region "class members"
    Private pLSGenericTools As New ILorestanGISGenericTools
    Private pNSNavaigation As New ILorestanGISNavaigation
    Private pNSEditorTools As New ILorestanGISEditorTools
    Private pNSSelectionTools As New ILorestanGISSelectionTools
    Private pNSGraphicTools As New ILorestanGISGraphicTools
    Private m_mapControl As ESRI.ArcGIS.Controls.IMapControl3 = Nothing
    Private m_pageLayoutControl As ESRI.ArcGIS.Controls.IPageLayoutControl2 = Nothing
    Private m_controlsSynchronizer As ControlsSynchronizer = Nothing
    Private m_CheckInitialize As New CheckInitialize_Cls

    Private pzoomvalue As Integer
    Private ActiveMapTool As Integer
    Private NsActiveMap As Boolean
    Private NsActivePage As Boolean
    Private m_documentFileName As String = String.Empty
    Private VaueProgressBar As Short = 0
    Private m_AlertOnLoad As DevComponents.DotNetBar.Balloon
    Private ScreenRectangle As Rectangle
    Private WithEvents Exclamation_Frm As Exclamation_Form
    Private PEnve As IEnvelope
    Private EngineEditor As IEngineEditor = New EngineEditor

    Enum NsEnumActiveTool
        NoActive
        E_SketchTool
    End Enum
#End Region

#Region "MainForm_Event"

    Private Sub WaterDistribution_MainForm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim CompactDatabase As New CompactDatabase_Cls
        CompactDatabase.CompactDatabase()
        Clear_ClassExtensions()
        m_mapControl = Nothing
        m_pageLayoutControl = Nothing
        MapControl.Dispose()
        TOCControl.Dispose()
        PageLayoutControl.Dispose()
    End Sub


    Private Sub WaterDistribution_MainForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '  ابتدا در قسمت لایسنس ها تیک بستن برنامه در صورت نبودن لایسنس را ور می داریم
        'If m_CheckInitialize.CheckInitialize(AxLicenseControl1.Status) Then
        'Me.Close()
        'Exit Sub
        'End If

        ChkShowToolbarStandard.CheckState = CheckState.Checked
        RBtnShowToolbarAnalysisNetwork.CheckState = CheckState.Checked

        m_mapControl = CType(MapControl.Object, IMapControl3)
        EngineEditor = New EngineEditor
        m_pageLayoutControl = CType(PageLayoutControl.Object, IPageLayoutControl2)
        TOCControl.SetBuddyControl(MapControl)

        m_controlsSynchronizer = New ControlsSynchronizer(m_mapControl, m_pageLayoutControl)

        m_controlsSynchronizer.BindControls(True)

        m_controlsSynchronizer.AddFrameworkControl(TOCControl.Object)

        KeyboardScrolling()
        ScreenRectangle = Screen.GetWorkingArea(Me)

        TabControlMaps.SelectedTab = TabControlMaps.Tabs.Item("TabItem_DataView")
        NsActiveMap = True
        NsActivePage = False
        RibbonControl1.Expanded = True
        BtnChangeVisisbleMenu.Text = "Minimize Ribbon"

        If MapControl.CheckMxFile(My.Application.Info.DirectoryPath & "\Data\Project.mxd") Then
            MapControl.LoadMxFile(My.Application.Info.DirectoryPath & "\Data\Project.mxd")
            Dim mapDoc As IMapDocument = New MapDocumentClass()
            Dim docName As String = My.Application.Info.DirectoryPath & "\Data\Project.mxd"
            mapDoc.Open(docName, String.Empty)
            Dim map As IMap = mapDoc.Map(0)
            m_controlsSynchronizer.PageLayoutControl.PageLayout = mapDoc.PageLayout
            m_controlsSynchronizer.ReplaceMap(map)
            Customize_Tool.OnCreate(MapControl.Object)
            Customize_Tool.Set_MapControlObject = MapControl.Object
            m_AlertOnLoad = New AlertCustom()
            AlertCustom.MessageError = Customize_Tool.ErrorMessage
            ShowLoadAlert()
        End If
    End Sub

    Private Sub KeyboardScrolling()
        MapControl.KeyIntercept = esriKeyIntercept.esriKeyInterceptArrowKeys
        MapControl.AutoKeyboardScrolling = True
        MapControl.AutoMouseWheel = True
        PageLayoutControl.KeyIntercept = esriKeyIntercept.esriKeyInterceptArrowKeys
        PageLayoutControl.AutoKeyboardScrolling = True
        PageLayoutControl.AutoMouseWheel = True
    End Sub

    Private Sub BtnDataView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDataView.Click
        If NsActiveMap Then Exit Sub
        m_controlsSynchronizer.ActivateMap()
        ChkShowToolbar_Layout.Checked = False
        TabControlMaps.SelectedTabIndex = 0

        BtnDataView.Checked = True
        BtnLayoutView.Checked = False

        NsActiveMap = True
        NsActivePage = False

        If BtnPan.Checked Then
            pNSNavaigation.Pan_NSMap(MapControl, Nothing)
        ElseIf BtnZoomIn.Checked Then
            pNSNavaigation.Zoomin_NSMap(MapControl, Nothing)
        ElseIf BtnZoomOut.Checked Then
            pNSNavaigation.ZoomOut_NSMap(MapControl, Nothing)
        ElseIf BtnSelectFeatures.Checked Then
            pNSSelectionTools.SelectFeaturesTool_NSMap(MapControl, Nothing)
        End If
    End Sub

    Private Sub BtnLayoutView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnLayoutView.Click
        If NsActivePage Then Exit Sub

        m_controlsSynchronizer.ActivatePageLayout()
        BtnDataView.Checked = False
        BtnLayoutView.Checked = True
        TabControlMaps.SelectedTabIndex = 1

        NsActiveMap = False
        NsActivePage = True

        If BtnPan.Checked Then
            pNSNavaigation.Pan_NSMap(Nothing, PageLayoutControl)
        ElseIf BtnZoomIn.Checked Then
            pNSNavaigation.Zoomin_NSMap(Nothing, PageLayoutControl)
        ElseIf BtnZoomOut.Checked Then
            pNSNavaigation.ZoomOut_NSMap(Nothing, PageLayoutControl)
        ElseIf BtnSelectFeatures.Checked Then
            pNSSelectionTools.SelectFeaturesTool_NSMap(Nothing, PageLayoutControl)
        End If
        ChkShowToolbar_Layout.Checked = True
        m_controlsSynchronizer.ActivatePageLayout()
    End Sub

    Private Sub RibbonControl1_ExpandedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RibbonControl1.ExpandedChanged
        If NsActiveMap Then
            '  PanelDockContainerDataView.Visible = True
            m_controlsSynchronizer.ActivateMap()
            BtnDataView.Checked = True
            BtnLayoutView.Checked = False
        ElseIf NsActivePage Then
            ' PanelDockContainerDataView.Visible = False
            BtnDataView.Checked = False
        End If
    End Sub

    Private Sub BtnChangeVisisbleMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnChangeVisisbleMenu.Click
        RibbonControl1.Expanded = Not RibbonControl1.Expanded
        If RibbonControl1.Expanded Then
            BtnChangeVisisbleMenu.Text = "Minimize Ribbon"
            BtnChangeVisisbleMenu.Image = My.Resources.hide_overview_map2
        Else
            BtnChangeVisisbleMenu.Text = "Maximize Ribbon"
            BtnChangeVisisbleMenu.Image = My.Resources.show_overview_map2
        End If
    End Sub

    Protected Overrides Sub DestroyHandle()
        AxLicenseControl1 = Nothing
        MapControl = Nothing
        TOCControl = Nothing
        PageLayoutControl = Nothing
        MapControl_CompareScenarios = Nothing
        MyBase.DestroyHandle()
    End Sub

    Private Sub Exclamation_Frm_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Exclamation_Frm.FormClosing
        Exclamation_Frm = Nothing
    End Sub

    Private Sub TabControlMaps_SelectedTabChanged(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.TabStripTabChangedEventArgs) Handles TabControlMaps.SelectedTabChanged
        If TabControlMaps.SelectedTabIndex = 0 Then
            BtnDataView.Checked = True
            BtnLayoutView.Checked = False
        ElseIf TabControlMaps.SelectedTabIndex = 1 Then
            BtnLayoutView.Checked = True
            BtnDataView.Checked = False
        End If
    End Sub

    Private Sub Clear_ClassExtensions()
        Dim Map As IMap
        Dim Layers As IEnumLayer
        Dim Layer As IFeatureLayer
        Dim FeatureClass As IFeatureClass
        Dim pUID As IUID = New UIDClass
        pUID.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}"
        Map = MapControl.Map
        If Map IsNot Nothing Then
            Layers = Map.Layers(pUID, True)
            Layer = Layers.Next
            While Layer IsNot Nothing
                FeatureClass = Layer.FeatureClass
                If Not IClassSchemaEdit_ClassExtension(FeatureClass, "") Then Exit Sub
                Layer = Layers.Next
            End While

        End If

    End Sub

    Public Function IClassSchemaEdit_ClassExtension(ByVal featClass As IObjectClass, ByVal classUIDStr As String) As Boolean

        If TypeOf featClass Is IClassSchemaEdit Then
            Dim classSchemaEdit As IClassSchemaEdit = CType(featClass, IClassSchemaEdit)
            Try
                Dim schemaLock As ISchemaLock = CType(featClass, ISchemaLock)
                schemaLock.ChangeSchemaLock(esriSchemaLock.esriExclusiveSchemaLock)
                Dim classUID As ESRI.ArcGIS.esriSystem.UID = New ESRI.ArcGIS.esriSystem.UIDClass()

                If Not classUIDStr.Equals("") Then
                    classUID.Value = classUIDStr
                    classSchemaEdit.AlterClassExtensionCLSID(classUID, Nothing)
                Else
                    ' classSchemaEdit.AlterClassExtensionCLSID(Nothing, Nothing)
                End If

                schemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock)
                IClassSchemaEdit_ClassExtension = True
            Catch ex As Exception
                m_AlertOnLoad = New AlertCustom()
                AlertCustom.MessageError = "Cannot acquire a schema lock beacause of an existing lock." & vbCrLf & "Try again when GeoDatabase is available."
                IClassSchemaEdit_ClassExtension = False
                ShowLoadAlert()
            End Try
        End If
    End Function

#End Region

#Region "MainMenu_Tools"
    Private Sub BtnAddData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddData.Click
        pLSGenericTools.AddData_NSMap(MapControl)
    End Sub

    Private Sub BtnSaveMXD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSaveMXD.Click
        On Error GoTo Err

        '********************************************************************
        m_documentFileName = My.Application.Info.DirectoryPath & "\Data\Project.mxd"

        'make sure that the current MapDoc is valid first
        If m_documentFileName <> String.Empty AndAlso m_mapControl.CheckMxFile(m_documentFileName) Then
            'create a new instance of a MapDocument class

            Dim mapDoc As IMapDocument = New MapDocumentClass()
            'Open the curent document into the MapDocument
            mapDoc.Open(m_documentFileName, String.Empty)

            'Replace the map with the one of the PageLayout
            mapDoc.ReplaceContents(CType(m_mapControl.Object, IMxdContents))

            'save the document
            mapDoc.Save(mapDoc.UsesRelativePaths, False)

            mapDoc.Close()
            Exit Sub
Err:

        End If
    End Sub

    Private Sub BOpenMXD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BOpenMXD.Click

        LblDescription.Text = "   Load Parameters ......"
        MapControl.MousePointer = esriControlsMousePointer.esriPointerArrowHourglass
        Dim docName As String
        Dim dlg As OpenFileDialog = New OpenFileDialog()
        dlg.Filter = "Map Documents (*.mxd)|*.mxd"
        dlg.Multiselect = False
        dlg.Title = "Open Map Document"
        If dlg.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            docName = dlg.FileName

            Dim mapDoc As IMapDocument = New MapDocumentClass()
            If mapDoc.IsPresent(docName) AndAlso (Not mapDoc.IsPasswordProtected(docName)) Then

                mapDoc.Open(docName, String.Empty)

                ' set the first map as the active view
                Dim map As IMap = mapDoc.Map(0)
                mapDoc.SetActiveView(CType(map, IActiveView))

                m_controlsSynchronizer.PageLayoutControl.PageLayout = mapDoc.PageLayout
                m_controlsSynchronizer.ReplaceMap(map)

                mapDoc.Close()

            End If
        End If
        MapControl.MousePointer = esriControlsMousePointer.esriPointerDefault
        LblDescription.Text = ""
        m_documentFileName = docName
    End Sub

    Private Sub ShowLoadAlert()
        Dim r As Rectangle = Screen.GetWorkingArea(Me)
        AlertCustom.Location = New System.Drawing.Point(r.Right - m_AlertOnLoad.Width, r.Bottom - m_AlertOnLoad.Height)
        AlertCustom.AutoClose = True
        AlertCustom.AlertAnimation = eAlertAnimation.BottomToTop
        AlertCustom.AlertAnimationDuration = 400
        AlertCustom.Show(False)
    End Sub

    Private Sub BtnSaveAsMXD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSaveAsMXD.Click
        m_AlertOnLoad = New AlertCustom()
        AlertCustom.MessageError = "Save As Menu is not available in Demo version"
        ShowLoadAlert()
    End Sub

    Private Sub BtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub
#End Region

#Region "MapControl_Event"

    Private Sub MapControl_OnDoubleClick(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IMapControlEvents2_OnDoubleClickEvent) Handles MapControl.OnDoubleClick
        If BtnDesign_MainPipes.Checked Then
            pNSEditorTools.SaveEditing_NSMap(MapControl)
            pNSEditorTools.StopEditing_NSMap(MapControl)
            BtnDesign_MainPipes.Checked = False
        End If
        If BtnDesign_UserPipes.Checked Then
            pNSEditorTools.SaveEditing_NSMap(MapControl)
            pNSEditorTools.StopEditing_NSMap(MapControl)
            BtnDesign_UserPipes.Checked = False
        End If
        If BtnDesign_MainPipes_ToolbarDesign.Checked Then
            pNSEditorTools.SaveEditing_NSMap(MapControl)
            pNSEditorTools.StopEditing_NSMap(MapControl)
            BtnDesign_MainPipes_ToolbarDesign.Checked = False
        End If
        If BtnDesign_UserPipes_ToolbarDesign.Checked Then
            pNSEditorTools.SaveEditing_NSMap(MapControl)
            pNSEditorTools.StopEditing_NSMap(MapControl)
            BtnDesign_UserPipes_ToolbarDesign.Checked = False
        End If
    End Sub

    Private Sub MapControl_OnMapReplaced(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IMapControlEvents2_OnMapReplacedEvent) Handles MapControl.OnMapReplaced
        If MapControl.Map.LayerCount > 0 Then
            ' Customize_Tool.OnCreate(MapControl.Object)
            ' Customize_Tool.Set_MapControlObject = MapControl.Object
            ' m_AlertOnLoad = New AlertCustom()
            '  AlertCustom.MessageError = Customize_Tool.ErrorMessage
            '  ShowLoadAlert()
        End If
    End Sub

    Private Sub MapControl_OnMouseDown(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent) Handles MapControl.OnMouseDown
        If e.button = 1 Then
            If BtnAnalysis_Identify.Checked Then
                '  PEnve = MapControl.TrackRectangle
                '  NSTool_Identify.Set_Envelope = PEnve
                '  NSTool_Identify.OnMouseDown(e.button, 0, e.mapX, e.mapY)
            End If
            If BtnAnalysis_TraceBarrier.Checked Then
                NSTool_TraceBarriers.OnMouseDown(e.button, 0, e.mapX, e.mapY)
            End If

        End If

        If BtnDesign_Junctions.Checked Then
            NSTool_DesignJunctions.OnMouseDown(e.button, 0, e.x, e.y)
            If Not NSTool_DesignJunctions.Get_EditState Then
                MapControl.CurrentTool = Nothing
                BtnDesign_Junctions.Checked = False
            End If
        End If
        If BtnDesign_UserPipes.Checked Then
            NSTool_DesignUserPipes.OnMouseDown(e.button, 0, e.x, e.y)
            If Not NSTool_DesignUserPipes.Get_EditState Then
                MapControl.CurrentTool = Nothing
                BtnDesign_UserPipes.Checked = False
            End If
        End If
        If BtnDesign_Users.Checked Then
            NSTool_DesignUsers.OnMouseDown(e.button, 0, e.x, e.y)
            If Not NSTool_DesignUsers.Get_EditState Then
                MapControl.CurrentTool = Nothing
                BtnDesign_Users.Checked = False
            End If
        End If
        If BtnDesign_Valves.Checked Then
            NSTool_DesignValves.OnMouseDown(e.button, 0, e.x, e.y)
            If Not NSTool_DesignValves.Get_EditState Then
                MapControl.CurrentTool = Nothing
                BtnDesign_Valves.Checked = False
            End If
        End If

        If BtnDesign_MainPipes.Checked Then
            NSTool_DesignMainPipes.OnMouseDown(e.button, 0, e.x, e.y)
            If Not NSTool_DesignMainPipes.Get_EditState Then
                MapControl.CurrentTool = Nothing
                BtnDesign_MainPipes.Checked = False
            End If
        End If



        If BtnDesign_Junctions_ToolbarDesign.Checked Then
            NSTool_DesignJunctions.OnMouseDown(e.button, 0, e.x, e.y)
            If Not NSTool_DesignJunctions.Get_EditState Then
                MapControl.CurrentTool = Nothing
                BtnDesign_Junctions_ToolbarDesign.Checked = False
            End If
        End If
        If BtnDesign_UserPipes_ToolbarDesign.Checked Then
            NSTool_DesignUserPipes.OnMouseDown(e.button, 0, e.x, e.y)
            If Not NSTool_DesignUserPipes.Get_EditState Then
                MapControl.CurrentTool = Nothing
                BtnDesign_UserPipes_ToolbarDesign.Checked = False
            End If
        End If
        If BtnDesign_Users_ToolbarDesign.Checked Then
            NSTool_DesignUsers.OnMouseDown(e.button, 0, e.x, e.y)
            If Not NSTool_DesignUsers.Get_EditState Then
                MapControl.CurrentTool = Nothing
                BtnDesign_Users_ToolbarDesign.Checked = False
            End If
        End If
        If BtnDesign_Valves_ToolbarDesign.Checked Then
            NSTool_DesignValves.OnMouseDown(e.button, 0, e.x, e.y)
            If Not NSTool_DesignValves.Get_EditState Then
                MapControl.CurrentTool = Nothing
                BtnDesign_Valves_ToolbarDesign.Checked = False
            End If
        End If

        If BtnDesign_MainPipes_ToolbarDesign.Checked Then
            NSTool_DesignMainPipes.OnMouseDown(e.button, 0, e.x, e.y)
            If Not NSTool_DesignMainPipes.Get_EditState Then
                MapControl.CurrentTool = Nothing
                BtnDesign_MainPipes_ToolbarDesign.Checked = False
            End If
        End If
    End Sub

    Private Sub MapControl_OnMouseMove(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseMoveEvent) Handles MapControl.OnMouseMove
        LabelCoordinate.Text = String.Format("{0}     {1}  {2}", e.mapX.ToString("#######.###"), e.mapY.ToString("#######.###"), m_mapControl.MapUnits.ToString().Substring(4))
        LabelSpatialReference.Text = m_mapControl.Map.SpatialReference.Name
    End Sub

    Private Sub MapControl_OnMouseUp(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseUpEvent) Handles MapControl.OnMouseUp
        If e.button = 1 Then
            If BtnDesign_Junctions.Checked Then
                pNSEditorTools.SaveEditing_NSMap(MapControl)
                pNSEditorTools.StopEditing_NSMap(MapControl)
                BtnDesign_Junctions.Checked = False
            End If
            If BtnDesign_Users.Checked Then
                pNSEditorTools.SaveEditing_NSMap(MapControl)
                pNSEditorTools.StopEditing_NSMap(MapControl)
                BtnDesign_Users.Checked = False
            End If
            If BtnDesign_Sources.Checked Then
                pNSEditorTools.SaveEditing_NSMap(MapControl)
                pNSEditorTools.StopEditing_NSMap(MapControl)
                BtnDesign_Sources.Checked = False
            End If
            If BtnDesign_Valves.Checked Then
                pNSEditorTools.SaveEditing_NSMap(MapControl)
                pNSEditorTools.StopEditing_NSMap(MapControl)
                BtnDesign_Valves.Checked = False
            End If

            If BtnDesign_Junctions_ToolbarDesign.Checked Then
                pNSEditorTools.SaveEditing_NSMap(MapControl)
                pNSEditorTools.StopEditing_NSMap(MapControl)
                BtnDesign_Junctions_ToolbarDesign.Checked = False
            End If
            If BtnDesign_Users_ToolbarDesign.Checked Then
                pNSEditorTools.SaveEditing_NSMap(MapControl)
                pNSEditorTools.StopEditing_NSMap(MapControl)
                BtnDesign_Users_ToolbarDesign.Checked = False
            End If
            If BtnDesign_Sources_ToolbarDesign.Checked Then
                pNSEditorTools.SaveEditing_NSMap(MapControl)
                pNSEditorTools.StopEditing_NSMap(MapControl)
                BtnDesign_Sources_ToolbarDesign.Checked = False
            End If
            If BtnDesign_Valves_ToolbarDesign.Checked Then
                pNSEditorTools.SaveEditing_NSMap(MapControl)
                pNSEditorTools.StopEditing_NSMap(MapControl)
                BtnDesign_Valves_ToolbarDesign.Checked = False
            End If
        End If
    End Sub
#End Region

#Region "PageLayoutControl_Event"
    Private Sub PageLayoutControl_OnMouseMove(ByVal sender As Object, ByVal e As ESRI.ArcGIS.Controls.IPageLayoutControlEvents_OnMouseMoveEvent) Handles PageLayoutControl.OnMouseMove
        LabelCoordinate.Text = String.Format("{0} {1} {2}", e.pageX.ToString("###.##"), e.pageY.ToString("###.##"), m_pageLayoutControl.Page.Units.ToString().Substring(4))
    End Sub
#End Region

#Region "ToolBarStandard"
    Private Sub BtnZoomIn_ToolStandard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnZoomIn_ToolStandard.Click
        Clear_DirtyAreaSearch()
        If NsActiveMap Then
            pNSNavaigation.Zoomin_NSMap(MapControl, Nothing)
        ElseIf NsActivePage Then
            pNSNavaigation.Zoomin_NSMap(Nothing, PageLayoutControl)
        End If
    End Sub

    Private Sub BtnZoomOut_ToolStandard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnZoomOut_ToolStandard.Click
        Clear_DirtyAreaSearch()
        If NsActiveMap Then
            pNSNavaigation.ZoomOut_NSMap(MapControl, Nothing)
        ElseIf NsActivePage Then
            pNSNavaigation.ZoomOut_NSMap(Nothing, PageLayoutControl)
        End If
    End Sub

    Private Sub BtnFixZoomIn_ToolStandard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFixZoomIn_ToolStandard.Click
        pNSNavaigation.ZoomInFixed_NSMap(MapControl)
    End Sub

    Private Sub BtnFixZoomOut_ToolStandard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFixZoomOut_ToolStandard.Click
        pNSNavaigation.ZoomOutFixed_NSMap(MapControl)
    End Sub

    Private Sub BtnPan_ToolStandard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPan_ToolStandard.Click
        Clear_DirtyAreaSearch()
        If NsActiveMap Then
            pNSNavaigation.Pan_NSMap(MapControl, Nothing)
        ElseIf NsActivePage Then
            pNSNavaigation.Pan_NSMap(Nothing, PageLayoutControl)
        End If
    End Sub

    Private Sub BtnFullExtent_ToolStandard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFullExtent_ToolStandard.Click
        On Error GoTo Err

        UpdateExtent_Layers()
        pNSNavaigation.FullExtent_NSMap(MapControl)
        Exit Sub
Err:
        pNSNavaigation.FullExtent_NSMap(MapControl)
    End Sub

    Private Sub UpdateExtent_Layers()
        On Error GoTo Err

        Dim Layer As ILayer
        Dim Layers As IEnumLayer
        Dim FeatureLayer As IFeatureLayer
        Dim FeatureClassManage As IFeatureClassManage
        Dim pUID As IUID = New UIDClass
        pUID.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}"
        Layers = MapControl.Map.Layers(pUID, True)
        Layer = Layers.Next
        While Layer IsNot Nothing
            If TypeOf Layer Is IFeatureLayer Then
                FeatureLayer = Layer
                FeatureClassManage = FeatureLayer.FeatureClass
                FeatureClassManage.UpdateExtent()
            End If
            Layer = Layers.Next
        End While
        Exit Sub
Err:
        Exit Sub
    End Sub

    Private Sub BtnSelectFeatures_ToolStandard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSelectFeatures_ToolStandard.Click
        Clear_DirtyAreaSearch()
        If NsActiveMap Then
            pNSSelectionTools.SelectFeaturesTool_NSMap(MapControl, Nothing)
        ElseIf NsActivePage Then
            pNSSelectionTools.SelectFeaturesTool_NSMap(Nothing, PageLayoutControl)
        End If
    End Sub

    Private Sub BtnUnSelectFeatures_ToolStandard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUnSelectFeatures_ToolStandard.Click
        pNSSelectionTools.ClearSelectionCommand_NSMap(MapControl)
    End Sub

    Private Sub Clear_DirtyAreaSearch()
        BtnDesign_EditFeatures.Checked = False
        BtnDesign_Junctions.Checked = False
        BtnDesign_MainPipes.Checked = False
        BtnDesign_Pumps.Checked = False
        BtnDesign_Sources.Checked = False
        BtnDesign_Tunks.Checked = False
        BtnDesign_UserPipes.Checked = False
        BtnDesign_Users.Checked = False
        BtnDesign_Valves.Checked = False

    End Sub
#End Region

#Region "Toolbar_Drawing"


#End Region

#Region "Menu_MapTools#"
    Private Sub RBtnShowToolbarDrawing_CheckedChanged(ByVal sender As System.Object, ByVal e As DevComponents.DotNetBar.CheckBoxChangeEventArgs) Handles RBtnShowToolbarDrawing.CheckedChanged
        ' ToolbarDrawing.Visible = RBtnShowToolbarDrawing.Checked
    End Sub

    Private Sub ChkShowToolbarStandard_CheckedChanged(ByVal sender As System.Object, ByVal e As DevComponents.DotNetBar.CheckBoxChangeEventArgs) Handles ChkShowToolbarStandard.CheckedChanged
        ToolbarStandard.Visible = ChkShowToolbarStandard.Checked
    End Sub

    Private Sub ChkShowToolbar_Layout_CheckedChanged(ByVal sender As System.Object, ByVal e As DevComponents.DotNetBar.CheckBoxChangeEventArgs) Handles ChkShowToolbar_Layout.CheckedChanged
        '   Bar_ToobarLayout.Visible = ChkShowToolbar_Layout.Checked
    End Sub


    Private Customize_Tool As New Customize
    Private NSTool_Identify As New Identify_Tool


    Private Sub BtnZoomIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnZoomIn.Click
        Cleardirtyarea("MapControl_Motivation")
        BtnZoomIn.Checked = True
        If NsActiveMap Then
            pNSNavaigation.Zoomin_NSMap(MapControl, Nothing)
        ElseIf NsActivePage Then
            pNSNavaigation.Zoomin_NSMap(Nothing, PageLayoutControl)
        End If
    End Sub

    Private Sub BtnPan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPan.Click
        Cleardirtyarea("MapControl_Motivation")
        BtnPan.Checked = True
        If NsActiveMap Then
            pNSNavaigation.Pan_NSMap(MapControl, Nothing)
        ElseIf NsActivePage Then
            pNSNavaigation.Pan_NSMap(Nothing, PageLayoutControl)
        End If
    End Sub

    Private Sub BtnZoomOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnZoomOut.Click
        Cleardirtyarea("MapControl_Motivation")
        BtnZoomOut.Checked = True
        If NsActiveMap Then
            pNSNavaigation.ZoomOut_NSMap(MapControl, Nothing)
        ElseIf NsActivePage Then
            pNSNavaigation.ZoomOut_NSMap(Nothing, PageLayoutControl)
        End If
    End Sub

    Private Sub BtnSelectFeatures_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSelectFeatures.Click
        Cleardirtyarea("MapControl_Selection")
        BtnSelectFeatures.Checked = True
        If NsActiveMap Then
            pNSSelectionTools.SelectFeaturesTool_NSMap(MapControl, Nothing)
        ElseIf NsActivePage Then
            pNSSelectionTools.SelectFeaturesTool_NSMap(Nothing, PageLayoutControl)
        End If
    End Sub

    Private Sub BtnFullExtent_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFullExtent.Click
        On Error GoTo Err
        BtnFullExtent.Checked = False
        UpdateExtent_Layers()
        pNSNavaigation.FullExtent_NSMap(MapControl)
        Exit Sub
Err:
        pNSNavaigation.FullExtent_NSMap(MapControl)
    End Sub

    Private Sub BtnFixZoomIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFixZoomIn.Click
        pNSNavaigation.ZoomInFixed_NSMap(MapControl)
        BtnFixZoomIn.Checked = False
    End Sub

    Private Sub BtnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCopy.Click
        pNSEditorTools.CopyCommand_NSMap(MapControl)
    End Sub

    Private Sub BtnCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCut.Click
        pNSEditorTools.CutCommand_NSMap(MapControl)
    End Sub

    Private Sub BtnPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPaste.Click
        pNSEditorTools.PasteCommand_NSMap(MapControl)
    End Sub

    Private Sub BtnFixZoomOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFixZoomOut.Click
        pNSNavaigation.ZoomOutFixed_NSMap(MapControl)
        BtnFixZoomOut.Checked = False
    End Sub

    Private Sub BtnPerviousZoom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPerviousZoom.Click
        pNSNavaigation.LastExtentExtent_NSMap(MapControl)
        BtnPerviousZoom.Checked = False
    End Sub

    Private Sub BtnNextZoomIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNextZoomIn.Click
        pNSNavaigation.ForwardExtent_NSMap(MapControl)
        BtnNextZoomIn.Checked = False
    End Sub

    Private Sub BtnZoomInFeatures_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnZoomInFeatures.Click
        pNSNavaigation.ZoomToSelected_NSMap(MapControl)
    End Sub

    Private Sub BtnUnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUnSelect.Click
        pNSSelectionTools.ClearSelectionCommand_NSMap(MapControl)
    End Sub

    Private Sub BtnSelectAllFeatuers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSelectAllFeatuers.Click
        pNSSelectionTools.SelectAllCommand_NSMap(MapControl)
    End Sub

    Private Sub BtnInversSelection_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnInversSelection.Click
        pNSSelectionTools.SwitchSelectionCommand_NSMap(MapControl)
    End Sub

    Private Sub Cleardirtyarea(ByVal actgrp As String)
        Dim i As Integer
        Dim btnItem As ButtonItem

        If actgrp = "MapControl_Motivation" Then


            For i = 0 To ItemContainer_Motivation1.SubItems.Count - 1
                If TypeOf ItemContainer_Motivation1.SubItems.Item(i) Is ButtonItem Then
                    btnItem = ItemContainer_Motivation1.SubItems.Item(i)
                    If btnItem.OptionGroup = actgrp Then
                        btnItem.Checked = False
                        ActiveMapTool = NsEnumActiveTool.NoActive
                    End If
                End If
            Next

            For i = 0 To ItemContainer_Motivation2.SubItems.Count - 1
                If TypeOf ItemContainer_Motivation2.SubItems.Item(i) Is ButtonItem Then
                    btnItem = ItemContainer_Motivation2.SubItems.Item(i)
                    If btnItem.OptionGroup = actgrp Then
                        btnItem.Checked = False
                        ActiveMapTool = NsEnumActiveTool.NoActive
                    End If
                End If
            Next

            For i = 0 To ItemContainer_Motivation3.SubItems.Count - 1
                If TypeOf ItemContainer_Motivation3.SubItems.Item(i) Is ButtonItem Then
                    btnItem = ItemContainer_Motivation3.SubItems.Item(i)
                    If btnItem.OptionGroup = actgrp Then
                        btnItem.Checked = False
                        ActiveMapTool = NsEnumActiveTool.NoActive
                    End If
                End If
            Next
        End If
        If actgrp = "MapControl_Selection" Then

            For i = 0 To ItemContainer_Selection1.SubItems.Count - 1
                If TypeOf ItemContainer_Selection1.SubItems.Item(i) Is ButtonItem Then
                    btnItem = ItemContainer_Selection1.SubItems.Item(i)
                    If btnItem.OptionGroup = actgrp Then
                        btnItem.Checked = False
                        ActiveMapTool = NsEnumActiveTool.NoActive
                    End If
                End If
            Next

            For i = 0 To ItemContainer_Selection2.SubItems.Count - 1
                If TypeOf ItemContainer_Selection2.SubItems.Item(i) Is ButtonItem Then
                    btnItem = ItemContainer_Selection2.SubItems.Item(i)
                    If btnItem.OptionGroup = actgrp Then
                        btnItem.Checked = False
                        ActiveMapTool = NsEnumActiveTool.NoActive
                    End If
                End If
            Next
        End If
    End Sub


    Private Sub BtnSearchFeatures_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSearchFeatures.Click
        Dim NSTool As New SearchFeatures_Command
        If NsActiveMap Then
            NSTool.OnCreate(MapControl.Object)
            NSTool.OnClick()
        ElseIf NsActivePage Then

        End If
    End Sub

    Private Sub BtnAnalysis_Identify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAnalysis_Identify.Click
        BtnAnalysis_Identify.Checked = Not BtnAnalysis_Identify.Checked
        If BtnAnalysis_Identify.Checked Then
            If NsActiveMap Then
                NSTool_Identify.OnCreate(MapControl.Object)
                NSTool_Identify.Set_ScreenRectangle = ScreenRectangle
                NSTool_Identify.OnClick()
                MapControl.CurrentTool = NSTool_Identify
            ElseIf NsActivePage Then

            End If
        Else
            If NSTool_Identify IsNot Nothing Then NSTool_Identify.Hide_FormIdentify()
            MapControl.CurrentTool = Nothing
        End If
    End Sub

    Private NSTool_GoogleMap As GoogleMap_Command

    Private Sub BtnAnalysis_GoogleMap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAnalysis_GoogleMap.Click
        On Error GoTo Err

        BtnAnalysis_GoogleMap.Checked = Not BtnAnalysis_GoogleMap.Checked
        Dirty_AreaDockBottom()
        BtnAnalysis_SearchTamas.Checked = False
        BtnAnalysis_SearchUsers.Checked = False

        If BtnAnalysis_GoogleMap.Checked Then
            Bar_StatusBar.Visible = False
            If Not Bar_DockWindowBottom.Visible Then Bar_DockWindowBottom.Visible = True
            Bar_DockWindowBottom.Text = "View in GoogleMap"
            DockContainerItem_GoogleMap.Visible = True
            Bar_DockWindowBottom.SelectedDockContainerItem = DockContainerItem_GoogleMap

            If NSTool_GoogleMap Is Nothing Then
                NSTool_GoogleMap = New GoogleMap_Command
            End If
            If NsActiveMap Then
                NSTool_GoogleMap.OnCreate(MapControl.Object)
                NSTool_GoogleMap.Set_ScreenRectangle = ScreenRectangle
                NSTool_GoogleMap.Set_WebBrowser_Map = WebBrowser_Map
                NSTool_GoogleMap.OnClick()
            ElseIf NsActivePage Then

            End If
        Else
            If Bar_DockWindowBottom.Visible Then
                Bar_DockWindowBottom.Visible = False
                Bar_StatusBar.Visible = True
                If NSTool_GoogleMap IsNot Nothing Then NSTool_GoogleMap.Remove_EventHandler()
            End If
        End If

        Exit Sub
Err:
    End Sub

    Private Sub RBtnShowToolbarAnalysisNetwork_CheckedChanged(ByVal sender As System.Object, ByVal e As DevComponents.DotNetBar.CheckBoxChangeEventArgs) Handles RBtnShowToolbarAnalysisNetwork.CheckedChanged
        If RBtnShowToolbarAnalysisNetwork.Checked Then
            ToolbarDesignNetwork.Visible = False
            ToolbarAnalysisNetwork.Visible = True
        End If
    End Sub

    Private Sub RBtnShowToolbarDesignNetwork_CheckedChanged(ByVal sender As System.Object, ByVal e As DevComponents.DotNetBar.CheckBoxChangeEventArgs) Handles RBtnShowToolbarDesignNetwork.CheckedChanged
        If RBtnShowToolbarDesignNetwork.Checked Then
            ToolbarAnalysisNetwork.Visible = True
            ToolbarDesignNetwork.Visible = True
        End If
    End Sub
#End Region

#Region "Menu_Analysis"
    Private NSTool_SearchUser As SearchUser_Command
    Private NSTool_TraceBarriers As New TraceBarriers_Tool

    Private Sub BtnAnalysis_SearchUsers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAnalysis_SearchUsers.Click
        On Error GoTo Err

        BtnAnalysis_SearchUsers.Checked = Not BtnAnalysis_SearchUsers.Checked
        Dirty_AreaDockBottom()
        BtnAnalysis_GoogleMap.Checked = False
        BtnAnalysis_SearchTamas.Checked = False

        If BtnAnalysis_SearchUsers.Checked Then
            Bar_StatusBar.Visible = False
            If Not Bar_DockWindowBottom.Visible Then Bar_DockWindowBottom.Visible = True

            LstResult_SearchUser.Items.Clear()
            Bar_DockWindowBottom.Text = "Search Users"
            DockContainerItem_SearchUsers.Visible = True
            Bar_DockWindowBottom.SelectedDockContainerItem = DockContainerItem_SearchUsers


            If NSTool_SearchUser Is Nothing Then
                NSTool_SearchUser = New SearchUser_Command
            End If
            If NsActiveMap Then
                NSTool_SearchUser.OnCreate(MapControl.Object)
                NSTool_SearchUser.Get_ListViewSearchUser_MainForm = LstResult_SearchUser
                NSTool_SearchUser.OnClick()
            ElseIf NsActivePage Then

            End If
        Else
            If Bar_DockWindowBottom.Visible Then
                Bar_DockWindowBottom.Visible = False
                Bar_StatusBar.Visible = True
            End If
        End If

        Exit Sub
Err:
    End Sub

    Private Sub Bar_DockWindowBottom_Closing(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.BarClosingEventArgs) Handles Bar_DockWindowBottom.Closing

        If NSTool_SearchUser IsNot Nothing Then
            NSTool_SearchUser.CloseForm()
        End If
        If NSTool_GoogleMap IsNot Nothing Then
            NSTool_GoogleMap.Remove_EventHandler()
        End If
        BtnAnalysis_SearchUsers.Checked = False
        BtnAnalysis_SearchTamas.Checked = False
        BtnAnalysis_GoogleMap.Checked = False
    End Sub


    Private Sub btnSearch_MakaneTamas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch_MakaneTamas.Click
        Dim NSTool As New MakaneTamas_Command
        If NsActiveMap Then
            NSTool.OnCreate(MapControl.Object)
            NSTool.Get_ListViewSearchUser_MainForm = LstResult_MakaneTamas
            NSTool.Get_StringSearch = txtSearch_MakaneTamas.Text
            If rbtnName_MakaneTamas.Checked Then
                NSTool.Get_FieldSearchName = True
            Else
                NSTool.Get_FieldSearchName = False
            End If
            NSTool.Set_ScreenRectangle = ScreenRectangle
            NSTool.OnClick()
            ' MapControl.CurrentTool = NSTool
        ElseIf NsActivePage Then

        End If
    End Sub


    Private Sub Bar_DockWindowScenario_Closing(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.BarClosingEventArgs) Handles Bar_DockWindowScenario.Closing
        BtnAnalysis_ManageScenarios.Checked = False
        NSTool_ManageScenarios = Nothing
    End Sub


    Private Sub BtnShowResult_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnShowResult.Click
        On Error GoTo Err
        '  LblDescription.Text = " Please wait ..."

        Dim NSTool As New TableResult_Command
        If NsActiveMap Then
            NSTool.OnCreate(MapControl.Object)
            NSTool.OnClick()
            ' MapControl.CurrentTool = NSTool
        ElseIf NsActivePage Then

        End If
        ' LblDescription.Text = ""
        Exit Sub
Err:
        ' LblDescription.Text = ""
    End Sub

    Private Sub BtnAnalysis_Velocity_HeadlossPipes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAnalysis_Velocity_HeadlossPipes.Click
        On Error GoTo Err
        LblDescription.Text = " Velocity - Headloss Pipes ..."
        ' System.Threading.Thread.Sleep(10)
        Application.DoEvents()

        Dim NSTool As New CalculateVeloccity_Headloss
        If NsActiveMap Then
            NSTool.OnCreate(MapControl.Object)
            NSTool.Set_Prog = ProgressBar1
            NSTool.Set_ScreenRectangle = ScreenRectangle
            NSTool.OnClick()
            ' MapControl.CurrentTool = NSTool
        ElseIf NsActivePage Then

        End If
        ProgressBar1.Value = 0
        LblDescription.Text = ""
        Exit Sub
Err:
        ProgressBar1.Value = 0
        LblDescription.Text = ""
    End Sub

    Private Sub BtnAnalysis_HeadJunctions_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAnalysis_HeadJunctions.Click
        On Error GoTo Err
        LblDescription.Text = " Pressure Junctions ..."
        ' System.Threading.Thread.Sleep(10)
        Application.DoEvents()

        Dim NSTool As New CalculateHeadlossJunction_Command
        If NsActiveMap Then
            NSTool.OnCreate(MapControl.Object)
            NSTool.Set_Prog = ProgressBar1
            NSTool.Set_ScreenRectangle = ScreenRectangle
            NSTool.OnClick()
        ElseIf NsActivePage Then

        End If

        ProgressBar1.Value = 0
        LblDescription.Text = ""
        Exit Sub
Err:
        ProgressBar1.Value = 0
        LblDescription.Text = ""
    End Sub

    Private Sub BtnAnalysis_BareJunctions_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAnalysis_BareJunctions.Click
        On Error GoTo Err
        LblDescription.Text = " Consumptions Junctions ..."
        ' System.Threading.Thread.Sleep(10)
        Application.DoEvents()

        Dim NSTool As New BarVared_Command
        If NsActiveMap Then
            NSTool.OnCreate(MapControl.Object)
            NSTool.Set_Prog = ProgressBar1
            NSTool.Set_ScreenRectangle = ScreenRectangle
            NSTool.OnClick()
            ' MapControl.CurrentTool = NSTool
        ElseIf NsActivePage Then

        End If

        ProgressBar1.Value = 0
        LblDescription.Text = ""
        Exit Sub
Err:
        ProgressBar1.Value = 0
        LblDescription.Text = ""
    End Sub

    Private Sub BtnAnalysis_ManagementEvents_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAnalysis_ManagementEvents.Click
        Dim NSTool As New ManageEvent
        If NsActiveMap Then
            NSTool.OnCreate(MapControl.Object)
            NSTool.OnClick()
            ' MapControl.CurrentTool = NSTool
        ElseIf NsActivePage Then

        End If
    End Sub

    Private Sub BtnAnalysis_RecordNewUsers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAnalysis_RecordNewUsers.Click
        Dim NSTool As New RecordNewUser_Command
        If NsActiveMap Then
            NSTool.OnCreate(MapControl.Object)
            NSTool.OnClick()
            ' MapControl.CurrentTool = NSTool
        ElseIf NsActivePage Then

        End If
    End Sub

    Private Sub BtnAnalysis_SearchTamas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAnalysis_SearchTamas.Click
        On Error GoTo Err

        BtnAnalysis_SearchTamas.Checked = Not BtnAnalysis_SearchTamas.Checked
        Dirty_AreaDockBottom()
        BtnAnalysis_GoogleMap.Checked = False
        BtnAnalysis_SearchUsers.Checked = False

        If BtnAnalysis_SearchTamas.Checked Then
            Bar_StatusBar.Visible = False
            If Not Bar_DockWindowBottom.Visible Then Bar_DockWindowBottom.Visible = True

            LstResult_MakaneTamas.Items.Clear()
            DockContainerItem_MakaneTamas.Visible = True
            Bar_DockWindowBottom.SelectedDockContainerItem = DockContainerItem_MakaneTamas
            Bar_DockWindowBottom.Text = "Find Reporter"
        Else
            If Bar_DockWindowBottom.Visible Then
                Bar_DockWindowBottom.Visible = False
                DockContainerItem_MakaneTamas.Visible = False
                Bar_StatusBar.Visible = True
            End If
        End If
        Exit Sub
Err:
    End Sub


    Private NSTool_ManageScenarios As ManageScenarios_Command
    Private m_NameFirstScenario, m_NamesecondScenario As String


    Private Sub BtnAnalysis_ManageScenarios_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAnalysis_ManageScenarios.Click
        On Error GoTo Err

        BtnAnalysis_ManageScenarios.Checked = Not BtnAnalysis_ManageScenarios.Checked
        Dirty_AreaDockRight()
        BtnAnalysis_CompareGraphicalScenario.Checked = False
        If BtnAnalysis_ManageScenarios.Checked Then
            If Not Bar_DockWindowScenario.Visible Then Bar_DockWindowScenario.Visible = True

            PanelDock_ManageScenarios.Visible = True
            NSTool_ManageScenarios = New ManageScenarios_Command
            If NsActiveMap Then
                NSTool_ManageScenarios.OnCreate(MapControl.Object)
                NSTool_ManageScenarios.Get_TreeView_MainForm = TreeViewScenarios
                NSTool_ManageScenarios.Get_cboFirstScenario_MainForm_MainForm = cboFirstScenario
                NSTool_ManageScenarios.Get_cboSecondScenario_MainForm_MainForm = cboSecondScenario
                NSTool_ManageScenarios.OnClick()
                ' MapControl.CurrentTool = NSTool
            ElseIf NsActivePage Then

            End If
        Else
            If Bar_DockWindowScenario.Visible Then
                Bar_DockWindowScenario.Visible = False
                NSTool_ManageScenarios = Nothing
            End If
        End If

        Exit Sub
Err:
    End Sub


    Private Sub BtnAnalysis_DeleteScenario_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAnalysis_DeleteScenario.Click
        If NSTool_ManageScenarios IsNot Nothing Then
            NSTool_ManageScenarios.Set_ScreenRectangle = ScreenRectangle
            NSTool_ManageScenarios.ToolstripDeleteScenario_Click()
        End If
    End Sub

    Private Sub BtnAnalysis_NewScenario_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAnalysis_NewScenario.Click
        If NSTool_ManageScenarios IsNot Nothing Then
            NSTool_ManageScenarios.Get_ScenarioName = TxtNameScenario.Text
            NSTool_ManageScenarios.Set_ScreenRectangle = ScreenRectangle
            NSTool_ManageScenarios.ToolstripNewScenario_Click()
        End If
    End Sub

    Private Sub btnInsertChangeScenario_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnInsertChangeScenario.Click
        If NSTool_ManageScenarios IsNot Nothing Then
            NSTool_ManageScenarios.Set_ScreenRectangle = ScreenRectangle
            NSTool_ManageScenarios.InsertChanges_Click()
        End If
    End Sub

    Private Sub btnCompareScenarios_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCompareScenarios.Click
        On Error GoTo Err
        LblDescription.Text = " Compare Scenarios ..."
        'System.Threading.Thread.Sleep(10)
        ProgressBar1.Value = 20
        ProgressBar1.PerformStep()
        Application.DoEvents()

        ' System.Threading.Thread.Sleep(10)

        If NSTool_ManageScenarios IsNot Nothing Then
            NSTool_ManageScenarios.Get_cboFirstScenario_MainForm_MainForm = cboFirstScenario
            NSTool_ManageScenarios.Get_cboSecondScenario_MainForm_MainForm = cboSecondScenario
            NSTool_ManageScenarios.Set_ScreenRectangle = ScreenRectangle
            NSTool_ManageScenarios.Set_Prog = ProgressBar1
            If NSTool_ManageScenarios.BtnCompareScenarios_Click() Then


                If RbtnCompareTableScenarios.Checked Then
                    Dim NSTool As New CompareTablesScenarios_Command
                    If NsActiveMap Then
                        NSTool.OnCreate(MapControl.Object)
                        NSTool.Get_FirstScenarioName = cboFirstScenario.Text
                        NSTool.Get_SecondScenarioName = cboSecondScenario.Text
                        NSTool.Set_Prog = ProgressBar1
                        NSTool.OnClick()
                        ' MapControl.CurrentTool = NSTool
                    ElseIf NsActivePage Then

                    End If

                Else
                    If Bar_DockWindowScenario.Visible Then
                        Bar_DockWindowScenario.Visible = False
                        PanelDock_ManageScenarios.Visible = False
                        BtnAnalysis_ManageScenarios.Checked = False
                    End If
                    Bar_DockWinCompareGraphicalScenario.Visible = True
                    PanelDock_CompareGraphicalScenarios.Visible = True
                    Dim NSTool As New CompareGraphicalScenarios_Command
                    If NsActiveMap Then
                        NSTool.OnCreate(MapControl.Object)
                        NSTool.Set_MapControlScenario = MapControl_CompareScenarios.Object
                        NSTool.Set_ScreenRectangle = ScreenRectangle
                        NSTool.Set_Prog = ProgressBar1
                        NSTool.OnClick()
                        ' MapControl.CurrentTool = NSTool
                    ElseIf NsActivePage Then

                    End If
                    BtnAnalysis_CompareGraphicalScenario.Checked = True
                End If
                m_NameFirstScenario = cboFirstScenario.Text
                m_NamesecondScenario = cboSecondScenario.Text
            End If
        End If
        ProgressBar1.Value = 0
        LblDescription.Text = ""
        Exit Sub
Err:
        ProgressBar1.Value = 0
        LblDescription.Text = ""
    End Sub

    Private NSTool_CompareGraphical As CompareGraphicalScenarios_Command

    Private Sub BtnAnalysis_CompareGraphicalScenario_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAnalysis_CompareGraphicalScenario.Click
        On Error GoTo Err
        LblDescription.Text = " Compare Graphical Scenarios ..."
        Application.DoEvents()

        If m_NameFirstScenario = vbNullString OrElse m_NamesecondScenario = vbNullString Then
            If Exclamation_Frm Is Nothing Then Exclamation_Frm = New Exclamation_Form
            Exclamation_Frm.txtExclamation.Text = "Please, First compare two different scenarios."
            LblDescription.Text = " "
            Exclamation_Frm.ShowDialog()
            Exit Sub
        End If

        BtnAnalysis_CompareGraphicalScenario.Checked = Not BtnAnalysis_CompareGraphicalScenario.Checked
        Dirty_AreaDockRight()
        BtnAnalysis_ManageScenarios.Checked = False

        If BtnAnalysis_CompareGraphicalScenario.Checked Then
            ' If Not Bar_DockWindowScenario.Visible Then Bar_DockWindowScenario.Visible = True

            Bar_DockWinCompareGraphicalScenario.Visible = True
            PanelDock_CompareGraphicalScenarios.Visible = True

            If NsActiveMap Then
                NSTool_CompareGraphical = New CompareGraphicalScenarios_Command
                NSTool_CompareGraphical.OnCreate(MapControl.Object)
                NSTool_CompareGraphical.Set_MapControlScenario = MapControl_CompareScenarios.Object
                NSTool_CompareGraphical.Set_ScreenRectangle = ScreenRectangle
                NSTool_CompareGraphical.Set_Prog = ProgressBar1
                NSTool_CompareGraphical.OnClick()
                ' MapControl.CurrentTool = NSTool
            ElseIf NsActivePage Then

            End If
        Else
            If NSTool_CompareGraphical Is Nothing Then
                NSTool_CompareGraphical = New CompareGraphicalScenarios_Command
                NSTool_CompareGraphical.OnCreate(MapControl.Object)
                NSTool_CompareGraphical.Set_Prog = ProgressBar1
                NSTool_CompareGraphical.Set_MapControlScenario = MapControl_CompareScenarios.Object
            End If
            NSTool_CompareGraphical.Removegraphics()
            NSTool_CompareGraphical.Close_FormCompareGraphics()
            If Bar_DockWinCompareGraphicalScenario.Visible Then
                Bar_DockWinCompareGraphicalScenario.Visible = False
                PanelDock_CompareGraphicalScenarios.Visible = False
            End If
        End If
        ProgressBar1.Value = 0
        LblDescription.Text = ""
        Exit Sub
Err:
        ProgressBar1.Value = 0
        LblDescription.Text = ""
    End Sub

    Private Sub BtnAnalysis_CompareTableScenarios_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAnalysis_CompareTableScenarios.Click
        On Error GoTo Err
        LblDescription.Text = " Compare Tabular Scenarios ..."
        Application.DoEvents()

        BtnAnalysis_CompareTableScenarios.Checked = Not BtnAnalysis_CompareTableScenarios.Checked
        Dim NSTool_CompareTablesScenarios As New CompareTablesScenarios_Command
        If BtnAnalysis_CompareTableScenarios.Checked Then

            If m_NameFirstScenario = vbNullString And m_NamesecondScenario = vbNullString Then
                If Exclamation_Frm Is Nothing Then Exclamation_Frm = New Exclamation_Form
                Exclamation_Frm.txtExclamation.Text = "Please, First compare two different scenarios."
                LblDescription.Text = ""
                Exclamation_Frm.ShowDialog()
                Exit Sub
            End If
            If NsActiveMap Then
                NSTool_CompareTablesScenarios.OnCreate(MapControl.Object)
                NSTool_CompareTablesScenarios.Get_FirstScenarioName = m_NameFirstScenario
                NSTool_CompareTablesScenarios.Get_SecondScenarioName = m_NamesecondScenario
                NSTool_CompareTablesScenarios.Set_Prog = ProgressBar1
                NSTool_CompareTablesScenarios.OnClick()
                ' MapControl.CurrentTool = NSTool
            ElseIf NsActivePage Then

            End If
        Else
            If NSTool_CompareTablesScenarios IsNot Nothing Then NSTool_CompareTablesScenarios.Close_Form()
        End If

        ProgressBar1.Value = 0
        LblDescription.Text = ""
        Exit Sub
Err:
        ProgressBar1.Value = 0
        LblDescription.Text = ""
    End Sub


    Private Sub Bar_DockWinCompareGraphicalScenario_Closing(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.BarClosingEventArgs) Handles Bar_DockWinCompareGraphicalScenario.Closing
        BtnAnalysis_CompareGraphicalScenario.Checked = False
        BtnAnalysis_ManageScenarios.Checked = False
    End Sub

    Private Sub Dirty_AreaDockBottom()
        DockContainerItem_MakaneTamas.Visible = False
        DockContainerItem_SearchUsers.Visible = False
        DockContainerItem_GoogleMap.Visible = False
    End Sub

    Private Sub Dirty_AreaDockRight()
        PanelDock_ManageScenarios.Visible = False
        PanelDock_CompareGraphicalScenarios.Visible = False
    End Sub

    Private Sub BtnAnalysis_Backup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAnalysis_Backup.Click

        If MapControl.LayerCount = 0 Then Exit Sub
        BtnAnalysis_Backup.Checked = True
        Dim NSTool_Backup As New BackupData_Command
        If BtnAnalysis_Backup.Checked Then
            NSTool_Backup.OnCreate(MapControl.Object)
            NSTool_Backup.Set_ScreenRectangle = ScreenRectangle
            NSTool_Backup.OnClick()
        End If
        BtnAnalysis_Backup.Checked = False
    End Sub


    Private Sub BtnAnalysis_ShowInformationOnMap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAnalysis_ShowInformationOnMap.Click
        BtnAnalysis_ShowInformationOnMap.Checked = True
        Dim NSTool_ShowInformationOnMap As New ShowInformation_Command
        If BtnAnalysis_ShowInformationOnMap.Checked Then
            NSTool_ShowInformationOnMap.OnCreate(MapControl.Object)
            NSTool_ShowInformationOnMap.Set_ScreenRectangle = ScreenRectangle
            NSTool_ShowInformationOnMap.OnClick()
        End If
        BtnAnalysis_ShowInformationOnMap.Checked = False
    End Sub

    Private Sub BtnAnalysis_NewsEvents_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnAnalysis_NewsEvents.Click
        Dim NSTool As New NewsEvents_Command
        If NsActiveMap Then
            NSTool.OnCreate(MapControl.Object)
            NSTool.OnClick()
        Else

        End If
    End Sub

    Private Sub BtnAnalysis_TraceBarrier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAnalysis_TraceBarrier.Click
        BtnAnalysis_TraceBarrier.Checked = Not BtnAnalysis_TraceBarrier.Checked

        If BtnAnalysis_TraceBarrier.Checked Then
            If NsActiveMap Then
                BtnAnalysis_TraceBarrier.Text = "Delete Barrier"
                NSTool_TraceBarriers.OnCreate(MapControl.Object)
                NSTool_TraceBarriers.OnClick()
                MapControl.CurrentTool = NSTool_TraceBarriers
            ElseIf NsActivePage Then

            End If
        Else
            NSTool_TraceBarriers.Delete_Barrier()
            BtnAnalysis_TraceBarrier.Text = "Set Barrier"
            MapControl.CurrentTool = Nothing
        End If

    End Sub
#End Region

#Region "Menu Cstomize"
    Private Thearding_Customize As System.Threading.Thread
    Private Sub BtnCustomize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnCustomize.Click
        ' LblDescription.Text = " ...... در حال بارگذاری تنظیمات"
        ' TimerProgressBar.Enabled = True
        ' TimerProgressBar.Interval = 600
        ' VaueProgressBar = 10
        ' Thearding_Customize = New Threading.Thread(AddressOf Perform_Customize)
        ' Thearding_Customize.Start()
        Customize_Tool.OnCreate(MapControl.Object)
        Customize_Tool.Set_ScreenRectangle = ScreenRectangle
        Customize_Tool.OnClick()
    End Sub

    Private Sub TimerProgressBar_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerProgressBar.Tick

        If VaueProgressBar < 85 Then
            Thearding_Customize.Suspend()

            ProgressBar1.Value = VaueProgressBar

            VaueProgressBar += 20
            Thearding_Customize.Resume()
        Else
            LblDescription.Text = ""
            ProgressBar1.Value = 0
        End If
    End Sub

    Private Sub Perform_Customize()
        If NsActiveMap Then
            If Customize_Tool IsNot Nothing Then
                Customize_Tool.Set_ScreenRectangle = ScreenRectangle
                Customize_Tool.OnClick()
            End If
        ElseIf NsActivePage Then

        End If
        Thearding_Customize.Abort()
        Thearding_Customize = Nothing
        ' MapControl.MousePointer = esriControlsMousePointer.esriPointerDefault
    End Sub

    Private Sub LblDescription_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LblProgressBar.TextChanged
        If TimerProgressBar.Enabled Then
            TimerProgressBar.Enabled = False
        End If
    End Sub
#End Region

#Region "ToolbarStandard_Scenarios"
    Private Sub BtnZoonIn_ToolbarScenarios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnZoonIn_ToolbarScenarios.Click
        If NsActiveMap Then
            pNSNavaigation.Zoomin_NSMap(MapControl_CompareScenarios, Nothing)
        End If
    End Sub

    Private Sub BtnZoonOut_ToolbarScenarios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnZoonOut_ToolbarScenarios.Click
        If NsActiveMap Then
            pNSNavaigation.ZoomOut_NSMap(MapControl_CompareScenarios, Nothing)
        End If
    End Sub

    Private Sub BtnFixZoonIn_ToolbarScenarios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFixZoonIn_ToolbarScenarios.Click
        If NsActiveMap Then
            pNSNavaigation.ZoomInFixed_NSMap(MapControl_CompareScenarios)
        End If
    End Sub

    Private Sub BtnFixZoonOut_ToolbarScenarios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFixZoonOut_ToolbarScenarios.Click
        If NsActiveMap Then
            pNSNavaigation.ZoomOutFixed_NSMap(MapControl_CompareScenarios)
        End If
    End Sub

    Private Sub BtnPan_ToolbarScenarios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPan_ToolbarScenarios.Click
        If NsActiveMap Then
            pNSNavaigation.Pan_NSMap(MapControl_CompareScenarios, Nothing)
        End If
    End Sub

    Private Sub BtnFullExtent_ToolbarScenarios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFullExtent_ToolbarScenarios.Click
        If NsActiveMap Then
            pNSNavaigation.FullExtent_NSMap(MapControl_CompareScenarios)
        End If
    End Sub

    Private Sub BtnNextZoonIn_ToolbarScenarios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNextZoonIn_ToolbarScenarios.Click
        If NsActiveMap Then
            pNSNavaigation.ForwardExtent_NSMap(MapControl_CompareScenarios)
        End If
    End Sub

    Private Sub BtnPreviousZoonIn_ToolbarScenarios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPreviousZoonIn_ToolbarScenarios.Click
        If NsActiveMap Then
            pNSNavaigation.LastExtentExtent_NSMap(MapControl_CompareScenarios)
        End If
    End Sub
#End Region

#Region "Menu DesignNetwork"

    Private NSTool_ValvePutting As New ValvePutting_Tool
    Private NSTool_ExtractionElevation As New ExtractionElevation_Command

    Private Sub BtnDesign_FlowDirection_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDesign_FlowDirection.Click
        BtnDesign_FlowDirection.Checked = Not BtnDesign_FlowDirection.Checked

        If BtnDesign_FlowDirection.Checked Then
            If NsActiveMap Then
                NSTool_FlowDirection.OnCreate(MapControl.Object)
                NSTool_FlowDirection.Set_ScreenRectangle = ScreenRectangle
                NSTool_FlowDirection.OnClick()
                ' MapControl.CurrentTool = NSTool
            ElseIf NsActivePage Then

            End If
            BtnDesign_FlowDirection.Text = "Hide Flow Direction"
        Else
            NSTool_FlowDirection.Remove_FlowDirection()
            BtnDesign_FlowDirection.Text = "Show Flow Direction"
        End If

    End Sub

    Private Sub BtnDesign_Pattern_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDesign_Pattern.Click
        Dim NSTool As New Pattern_Command
        If NsActiveMap Then
            NSTool.OnCreate(MapControl.Object)
            NSTool.OnClick()
            ' MapControl.CurrentTool = NSTool
        ElseIf NsActivePage Then

        End If
    End Sub

    Private Sub BtnDesign_PuttingValve_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDesign_PuttingValve.Click
        BtnDesign_PuttingValve.Checked = Not BtnDesign_PuttingValve.Checked
        If BtnDesign_PuttingValve.Checked Then
            If NsActiveMap Then
                NSTool_ValvePutting.OnCreate(MapControl.Object)
                NSTool_ValvePutting.OnClick()
                ' MapControl.CurrentTool = NSTool
            ElseIf NsActivePage Then

            End If
        Else
            If NSTool_ValvePutting IsNot Nothing Then NSTool_ValvePutting.Close_Form()
        End If

    End Sub

    Private Sub BtnDesign_ExtractElevation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDesign_ExtractElevation.Click
        BtnDesign_ExtractElevation.Checked = Not BtnDesign_ExtractElevation.Checked
        If BtnDesign_ExtractElevation.Checked Then
            If NsActiveMap Then
                NSTool_ExtractionElevation.OnCreate(MapControl.Object)
                NSTool_ExtractionElevation.OnClick()
                ' MapControl.CurrentTool = NSTool
            ElseIf NsActivePage Then

            End If
        Else
            If NSTool_ExtractionElevation IsNot Nothing Then NSTool_ExtractionElevation.Close_Form()
        End If

    End Sub

    Private NSTool_FlowDirection As New FlowDirection
    Private NSTool_DesignMainPipes As New DesignMainPipes_Tool
    Private NSTool_DesignUsers As New DesignUsers_Tool
    Private NSTool_DesignUserPipes As New DesignUserPipes_Tool
    Private NSTool_DesignSource As New DesignSource_Tool
    Private NSTool_DesignJunctions As New DesignJunctions_Tool
    Private NSTool_DesignValves As New DesignValves_Tool
    Private NSTool_EditAttributeFeat As New EditAttributeFeatures_Command

    Private Sub BtnDesign_MainPipes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDesign_MainPipes.Click
        On Error GoTo Err
        Dim Checked As Boolean = BtnDesign_MainPipes.Checked
        Clear_DirtyAreaSearch()
        BtnDesign_MainPipes.Checked = True

        If EngineEditor.EditState = esriEngineEditState.esriEngineStateNotEditing Then
            If NsActiveMap Then

                Dim Set_ClassExtensions As New cls_Set_ClassExtensions
                Set_ClassExtensions.Set_ScreenRectangle = ScreenRectangle
                ' Set_ClassExtensions.set_MainPipes_ClassExtensions()

                pNSEditorTools.StartEditing_NSMap(MapControl)
                pNSEditorTools.SketchTool_NSMap(MapControl)

                NSTool_DesignMainPipes.OnCreate(MapControl.Object)
                NSTool_DesignMainPipes.Set_ScreenRectangle = ScreenRectangle
                NSTool_DesignMainPipes.OnClick()

            ElseIf NsActivePage Then

            End If
        Else
            pNSEditorTools.SaveEditing_NSMap(MapControl)
            pNSEditorTools.StopEditing_NSMap(MapControl)
            BtnDesign_MainPipes.Checked = False
        End If

        Exit Sub
Err:
        If EngineEditor.EditState = esriEngineEditState.esriEngineStateEditing Then
            EngineEditor.AbortOperation()
            EngineEditor.StopEditing(False)
        End If
        MapControl.CurrentTool = Nothing
        BtnDesign_MainPipes.Checked = False
    End Sub

    Private Sub BtnDesign_EditFeatures_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDesign_EditFeatures.Click
        On Error GoTo Err
        BtnDesign_EditFeatures.Checked = Not BtnDesign_EditFeatures.Checked
        If BtnDesign_EditFeatures.Checked Then
            If NsActiveMap Then
                pNSEditorTools.StartEditing_NSMap(MapControl)
                pNSEditorTools.EditTool_NSMap(MapControl)
            ElseIf NsActivePage Then

            End If
        Else
            If NsActiveMap Then
                pNSEditorTools.SaveEditing_NSMap(MapControl)
                pNSEditorTools.StopEditing_NSMap(MapControl)
            ElseIf NsActivePage Then

            End If
        End If
        Exit Sub
Err:
        If EngineEditor.EditState = esriEngineEditState.esriEngineStateEditing Then
            EngineEditor.AbortOperation()
            EngineEditor.StopEditing(False)
        End If
        MapControl.CurrentTool = Nothing
        BtnDesign_EditFeatures.Checked = False
    End Sub

    Private Sub BtnDesign_Users_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDesign_Users.Click
        On Error GoTo Err
        '  Dim Checked As Boolean = BtnDesign_Users.Checked
        Clear_DirtyAreaSearch()
        BtnDesign_Users.Checked = True
        If EngineEditor.EditState = esriEngineEditState.esriEngineStateNotEditing Then
            If NsActiveMap Then
                Dim Set_ClassExtensions As New cls_Set_ClassExtensions
                Set_ClassExtensions.Set_ScreenRectangle = ScreenRectangle
                '  Set_ClassExtensions.set_User_ClassExtensions()

                pNSEditorTools.StartEditing_NSMap(MapControl)
                pNSEditorTools.SketchTool_NSMap(MapControl)

                NSTool_DesignUsers.OnCreate(MapControl.Object)
                NSTool_DesignUsers.Set_ScreenRectangle = ScreenRectangle
                NSTool_DesignUsers.OnClick()
            ElseIf NsActivePage Then

            End If
        Else
            pNSEditorTools.SaveEditing_NSMap(MapControl)
            pNSEditorTools.StopEditing_NSMap(MapControl)
            BtnDesign_Users.Checked = False
        End If
        Exit Sub
Err:
        If EngineEditor.EditState = esriEngineEditState.esriEngineStateEditing Then
            EngineEditor.AbortOperation()
            EngineEditor.StopEditing(False)
        End If
        MapControl.CurrentTool = Nothing
        BtnDesign_Users.Checked = False
    End Sub

    Private Sub BtnEmaleTaghyirat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnEmaleTaghyirat.Click
        On Error GoTo Err
        LblDescription.Text = " Validation ..."
        ProgressBar1.Value = 20
        'System.Threading.Thread.Sleep(10)
        ProgressBar1.PerformStep()
        Application.DoEvents()

        Dim NSTool As New EmaleTagheirat
        If NsActiveMap Then
            NSTool.OnCreate(MapControl.Object)
            NSTool.Set_Prog = ProgressBar1
            NSTool.Set_ScreenRectangle = ScreenRectangle
            NSTool.OnClick()
            ' MapControl.CurrentTool = NSTool
        ElseIf NsActivePage Then

        End If
        ProgressBar1.Value = 0
        LblDescription.Text = ""
        Exit Sub
Err:
        ProgressBar1.Value = 0
        LblDescription.Text = ""
    End Sub

    Private Sub BtnDesign_Pumps_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDesign_Pumps.Click
        On Error GoTo Err
        Dim Checked As Boolean = BtnDesign_Pumps.Checked
        Clear_DirtyAreaSearch()
        BtnDesign_Pumps.Checked = Not Checked

        Exit Sub
Err:
        If EngineEditor.EditState = esriEngineEditState.esriEngineStateEditing Then
            EngineEditor.AbortOperation()
            EngineEditor.StopEditing(False)
        End If
        MapControl.CurrentTool = Nothing
        BtnDesign_Users.Checked = False

    End Sub

    Private Sub BtnDesign_Tunks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDesign_Tunks.Click
        On Error GoTo Err
        Dim Checked As Boolean = BtnDesign_Tunks.Checked
        Clear_DirtyAreaSearch()
        BtnDesign_Tunks.Checked = Not Checked

        Exit Sub
Err:
        If EngineEditor.EditState = esriEngineEditState.esriEngineStateEditing Then
            EngineEditor.AbortOperation()
            EngineEditor.StopEditing(False)
        End If
        MapControl.CurrentTool = Nothing
        BtnDesign_Users.Checked = False
    End Sub

    Private Sub BtnDesign_UserPipes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDesign_UserPipes.Click
        On Error GoTo Err
        'Dim Checked As Boolean = BtnDesign_UserPipes.Checked
        Clear_DirtyAreaSearch()
        BtnDesign_UserPipes.Checked = True
        If EngineEditor.EditState = esriEngineEditState.esriEngineStateNotEditing Then
            If NsActiveMap Then
                Dim Set_ClassExtensions As New cls_Set_ClassExtensions
                Set_ClassExtensions.Set_ScreenRectangle = ScreenRectangle
                '  Set_ClassExtensions.set_UserPipes_ClassExtensions()

                pNSEditorTools.StartEditing_NSMap(MapControl)
                pNSEditorTools.SketchTool_NSMap(MapControl)

                NSTool_DesignUserPipes.OnCreate(MapControl.Object)
                NSTool_DesignUserPipes.Set_ScreenRectangle = ScreenRectangle
                NSTool_DesignUserPipes.OnClick()
            ElseIf NsActivePage Then

            End If
        Else
            pNSEditorTools.SaveEditing_NSMap(MapControl)
            pNSEditorTools.StopEditing_NSMap(MapControl)
            BtnDesign_UserPipes.Checked = False
        End If

        Exit Sub
Err:
        If EngineEditor.EditState = esriEngineEditState.esriEngineStateEditing Then
            EngineEditor.AbortOperation()
            EngineEditor.StopEditing(False)
        End If
        MapControl.CurrentTool = Nothing
        BtnDesign_UserPipes.Checked = False
    End Sub

    Private Sub BtnDesign_Sources_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDesign_Sources.Click
        On Error GoTo Err
        ' Dim Checked As Boolean = BtnDesign_Sources.Checked
        Clear_DirtyAreaSearch()
        BtnDesign_Sources.Checked = True
        If EngineEditor.EditState = esriEngineEditState.esriEngineStateNotEditing Then
            If NsActiveMap Then
                Dim Set_ClassExtensions As New cls_Set_ClassExtensions
                Set_ClassExtensions.Set_ScreenRectangle = ScreenRectangle
                ' Set_ClassExtensions.set_Source_ClassExtensions()

                pNSEditorTools.StartEditing_NSMap(MapControl)
                pNSEditorTools.SketchTool_NSMap(MapControl)

                NSTool_DesignSource.OnCreate(MapControl.Object)
                NSTool_DesignSource.Set_ScreenRectangle = ScreenRectangle
                NSTool_DesignSource.OnClick()
            ElseIf NsActivePage Then

            End If
        Else
            pNSEditorTools.SaveEditing_NSMap(MapControl)
            pNSEditorTools.StopEditing_NSMap(MapControl)
            BtnDesign_Sources.Checked = False
        End If
        Exit Sub
Err:
        If EngineEditor.EditState = esriEngineEditState.esriEngineStateEditing Then
            EngineEditor.AbortOperation()
            EngineEditor.StopEditing(False)
        End If
        MapControl.CurrentTool = Nothing
        BtnDesign_Sources.Checked = False
    End Sub

    Private Sub BtnDesign_Junctions_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDesign_Junctions.Click
        On Error GoTo Err
        ' Dim Checked As Boolean = BtnDesign_Junctions.Checked
        Clear_DirtyAreaSearch()
        BtnDesign_Junctions.Checked = True
        If EngineEditor.EditState = esriEngineEditState.esriEngineStateNotEditing Then
            If NsActiveMap Then
                Dim Set_ClassExtensions As New cls_Set_ClassExtensions
                Set_ClassExtensions.Set_ScreenRectangle = ScreenRectangle
                ' Set_ClassExtensions.set_Junctions_ClassExtensions()

                pNSEditorTools.StartEditing_NSMap(MapControl)
                pNSEditorTools.SketchTool_NSMap(MapControl)

                NSTool_DesignJunctions.OnCreate(MapControl.Object)
                NSTool_DesignJunctions.Set_ScreenRectangle = ScreenRectangle
                NSTool_DesignJunctions.OnClick()
            ElseIf NsActivePage Then

            End If
        Else
            pNSEditorTools.SaveEditing_NSMap(MapControl)
            pNSEditorTools.StopEditing_NSMap(MapControl)
            BtnDesign_Junctions.Checked = False
        End If

        Exit Sub
Err:
        If EngineEditor.EditState = esriEngineEditState.esriEngineStateEditing Then
            EngineEditor.AbortOperation()
            EngineEditor.StopEditing(False)
        End If
        MapControl.CurrentTool = Nothing
        BtnDesign_Junctions.Checked = False

    End Sub

    Private Sub BtnDesign_Valves_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDesign_Valves.Click
        On Error GoTo Err
        ' Dim Checked As Boolean = BtnDesign_Valves.Checked
        Clear_DirtyAreaSearch()
        BtnDesign_Valves.Checked = True
        If EngineEditor.EditState = esriEngineEditState.esriEngineStateNotEditing Then
            If NsActiveMap Then
                Dim Set_ClassExtensions As New cls_Set_ClassExtensions
                Set_ClassExtensions.Set_ScreenRectangle = ScreenRectangle
                ' Set_ClassExtensions.set_Valves_ClassExtensions()

                pNSEditorTools.StartEditing_NSMap(MapControl)
                pNSEditorTools.SketchTool_NSMap(MapControl)

                NSTool_DesignValves.OnCreate(MapControl.Object)
                NSTool_DesignValves.Set_ScreenRectangle = ScreenRectangle
                NSTool_DesignValves.OnClick()
            ElseIf NsActivePage Then

            End If
        Else
            pNSEditorTools.SaveEditing_NSMap(MapControl)
            pNSEditorTools.StopEditing_NSMap(MapControl)
            BtnDesign_Valves.Checked = False
        End If
        Exit Sub
Err:
        If EngineEditor.EditState = esriEngineEditState.esriEngineStateEditing Then
            EngineEditor.AbortOperation()
            EngineEditor.StopEditing(False)
        End If
        MapControl.CurrentTool = Nothing
        BtnDesign_Valves.Checked = False
    End Sub

    Private Sub BtnDesign_Network_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDesign_Network.Click
        Dim NSTool As New DesignGeometricNetwork_Command
        If NsActiveMap Then
            NSTool.OnCreate(MapControl.Object)
            NSTool.Set_ScreenRectangle = ScreenRectangle
            NSTool.OnClick()
        Else

        End If
    End Sub

    Private Sub BtnDesign_EditAttributeFeat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDesign_EditAttributeFeat.Click
        BtnDesign_EditAttributeFeat.Checked = Not BtnDesign_EditAttributeFeat.Checked

        If BtnDesign_EditAttributeFeat.Checked Then
            NSTool_EditAttributeFeat.OnCreate(MapControl.Object)
            NSTool_EditAttributeFeat.OnClick()
        Else
            If NSTool_EditAttributeFeat IsNot Nothing Then NSTool_EditAttributeFeat.Close_Form()
        End If
    End Sub
#End Region

#Region "About Menu"
    Private Sub BtnAboutSoftware_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAboutSoftware.Click
        Dim AboutSoftware As New AboutSoftware_Form
        AboutSoftware.ShowDialog()
    End Sub

#End Region
   
#Region "Toolbar_Analysis"
    Private Sub BtnVelocity_HeadlossPipes_ToolbarAnalysis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnVelocity_HeadlossPipes_ToolbarAnalysis.Click
        BtnAnalysis_Velocity_HeadlossPipes_Click(sender, e)
    End Sub

    Private Sub BtnHeadJunctions_ToolbarAnalysis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnHeadJunctions_ToolbarAnalysis.Click
        BtnAnalysis_HeadJunctions_Click(sender, e)
    End Sub

    Private Sub BtnBareJunctions_ToolbarAnalysis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBareJunctions_ToolbarAnalysis.Click
        BtnAnalysis_BareJunctions_Click(sender, e)
    End Sub

    Private Sub BtnTraceBarrier__ToolbarAnalysis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnTraceBarrier__ToolbarAnalysis.Click
        BtnAnalysis_TraceBarrier_Click(sender, e)
    End Sub

    Private Sub BtnShowResult_ToolbarAnalysis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnShowResult_ToolbarAnalysis.Click
        BtnShowResult_Click(sender, e)
    End Sub

    Private Sub BtnManageScenario_ToolbarAnalysis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnManageScenario_ToolbarAnalysis.Click
        BtnAnalysis_ManageScenarios_Click(sender, e)
    End Sub

    Private Sub BtnCompareGraphicalScenario_ToolbarAnalysis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCompareGraphicalScenario_ToolbarAnalysis.Click
        BtnAnalysis_CompareGraphicalScenario_Click(sender, e)
    End Sub

    Private Sub BtnCompareTabularScenario_ToolbarAnalysis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCompareTabularScenario_ToolbarAnalysis.Click
        BtnAnalysis_CompareTableScenarios_Click(sender, e)
    End Sub
#End Region
  
#Region "Toolbar_Design"

    Private Sub Clear_DirtyAreaToolbarDesign()
        BtnDesign_MainPipes_ToolbarDesign.Checked = False
        BtnDesign_UserPipes_ToolbarDesign.Checked = False
        BtnDesign_Sources_ToolbarDesign.Checked = False
        BtnDesign_Junctions_ToolbarDesign.Checked = False
        BtnDesign_Users_ToolbarDesign.Checked = False
        BtnDesign_Valves_ToolbarDesign.Checked = False
    End Sub

    Private Sub BtnDesign_MainPipes_ToolbarDesign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDesign_MainPipes_ToolbarDesign.Click
        On Error GoTo Err
        Dim Checked As Boolean = BtnDesign_MainPipes_ToolbarDesign.Checked
        Clear_DirtyAreaToolbarDesign()
        BtnDesign_MainPipes_ToolbarDesign.Checked = True

        If EngineEditor.EditState = esriEngineEditState.esriEngineStateNotEditing Then
            If NsActiveMap Then

                Dim Set_ClassExtensions As New cls_Set_ClassExtensions
                Set_ClassExtensions.Set_ScreenRectangle = ScreenRectangle
                ' Set_ClassExtensions.set_MainPipes_ClassExtensions()

                pNSEditorTools.StartEditing_NSMap(MapControl)
                pNSEditorTools.SketchTool_NSMap(MapControl)

                NSTool_DesignMainPipes.OnCreate(MapControl.Object)
                NSTool_DesignMainPipes.Set_ScreenRectangle = ScreenRectangle
                NSTool_DesignMainPipes.OnClick()

            ElseIf NsActivePage Then

            End If
        Else
            pNSEditorTools.SaveEditing_NSMap(MapControl)
            pNSEditorTools.StopEditing_NSMap(MapControl)
            BtnDesign_MainPipes_ToolbarDesign.Checked = False
        End If

        Exit Sub
Err:
        If EngineEditor.EditState = esriEngineEditState.esriEngineStateEditing Then
            EngineEditor.AbortOperation()
            EngineEditor.StopEditing(False)
        End If
        MapControl.CurrentTool = Nothing
        BtnDesign_MainPipes_ToolbarDesign.Checked = False
    End Sub

    Private Sub BtnDesign_UserPipes_ToolbarDesign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDesign_UserPipes_ToolbarDesign.Click
        On Error GoTo Err
        ' Dim Checked As Boolean = BtnDesign_UserPipes_ToolbarDesign.Checked
        Clear_DirtyAreaToolbarDesign()
        BtnDesign_UserPipes_ToolbarDesign.Checked = True
        If EngineEditor.EditState = esriEngineEditState.esriEngineStateNotEditing Then
            If NsActiveMap Then
                Dim Set_ClassExtensions As New cls_Set_ClassExtensions
                Set_ClassExtensions.Set_ScreenRectangle = ScreenRectangle
                '  Set_ClassExtensions.set_UserPipes_ClassExtensions()

                pNSEditorTools.StartEditing_NSMap(MapControl)
                pNSEditorTools.SketchTool_NSMap(MapControl)

                NSTool_DesignUserPipes.OnCreate(MapControl.Object)
                NSTool_DesignUserPipes.Set_ScreenRectangle = ScreenRectangle
                NSTool_DesignUserPipes.OnClick()
            ElseIf NsActivePage Then

            End If
        Else
            pNSEditorTools.SaveEditing_NSMap(MapControl)
            pNSEditorTools.StopEditing_NSMap(MapControl)
            BtnDesign_UserPipes_ToolbarDesign.Checked = False
        End If

        Exit Sub
Err:
        If EngineEditor.EditState = esriEngineEditState.esriEngineStateEditing Then
            EngineEditor.AbortOperation()
            EngineEditor.StopEditing(False)
        End If
        MapControl.CurrentTool = Nothing
        BtnDesign_UserPipes_ToolbarDesign.Checked = False
    End Sub

    Private Sub BtnDesign_Sources_ToolbarDesign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDesign_Sources_ToolbarDesign.Click
        On Error GoTo Err
        ' Dim Checked As Boolean = BtnDesign_Sources.Checked
        Clear_DirtyAreaToolbarDesign()
        BtnDesign_Sources_ToolbarDesign.Checked = True
        If EngineEditor.EditState = esriEngineEditState.esriEngineStateNotEditing Then
            If NsActiveMap Then
                Dim Set_ClassExtensions As New cls_Set_ClassExtensions
                Set_ClassExtensions.Set_ScreenRectangle = ScreenRectangle
                ' Set_ClassExtensions.set_Source_ClassExtensions()

                pNSEditorTools.StartEditing_NSMap(MapControl)
                pNSEditorTools.SketchTool_NSMap(MapControl)

                NSTool_DesignSource.OnCreate(MapControl.Object)
                NSTool_DesignSource.Set_ScreenRectangle = ScreenRectangle
                NSTool_DesignSource.OnClick()
            ElseIf NsActivePage Then

            End If
        Else
            pNSEditorTools.SaveEditing_NSMap(MapControl)
            pNSEditorTools.StopEditing_NSMap(MapControl)
            BtnDesign_Sources_ToolbarDesign.Checked = False
        End If
        Exit Sub
Err:
        If EngineEditor.EditState = esriEngineEditState.esriEngineStateEditing Then
            EngineEditor.AbortOperation()
            EngineEditor.StopEditing(False)
        End If
        MapControl.CurrentTool = Nothing
        BtnDesign_Sources_ToolbarDesign.Checked = False
    End Sub

    Private Sub BtnDesign_Junctions_ToolbarDesign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDesign_Junctions_ToolbarDesign.Click
        On Error GoTo Err
        ' Dim Checked As Boolean = BtnDesign_Junctions.Checked
        Clear_DirtyAreaToolbarDesign()
        BtnDesign_Junctions_ToolbarDesign.Checked = True
        If EngineEditor.EditState = esriEngineEditState.esriEngineStateNotEditing Then
            If NsActiveMap Then
                Dim Set_ClassExtensions As New cls_Set_ClassExtensions
                Set_ClassExtensions.Set_ScreenRectangle = ScreenRectangle
                ' Set_ClassExtensions.set_Junctions_ClassExtensions()

                pNSEditorTools.StartEditing_NSMap(MapControl)
                pNSEditorTools.SketchTool_NSMap(MapControl)

                NSTool_DesignJunctions.OnCreate(MapControl.Object)
                NSTool_DesignJunctions.Set_ScreenRectangle = ScreenRectangle
                NSTool_DesignJunctions.OnClick()
            ElseIf NsActivePage Then

            End If
        Else
            pNSEditorTools.SaveEditing_NSMap(MapControl)
            pNSEditorTools.StopEditing_NSMap(MapControl)
            BtnDesign_Junctions_ToolbarDesign.Checked = False
        End If

        Exit Sub
Err:
        If EngineEditor.EditState = esriEngineEditState.esriEngineStateEditing Then
            EngineEditor.AbortOperation()
            EngineEditor.StopEditing(False)
        End If
        MapControl.CurrentTool = Nothing
        BtnDesign_Junctions_ToolbarDesign.Checked = False
    End Sub

    Private Sub BtnDesign_Users_ToolbarDesign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDesign_Users_ToolbarDesign.Click
        On Error GoTo Err
        '  Dim Checked As Boolean = BtnDesign_Users.Checked
        Clear_DirtyAreaToolbarDesign()
        BtnDesign_Users_ToolbarDesign.Checked = True
        If EngineEditor.EditState = esriEngineEditState.esriEngineStateNotEditing Then
            If NsActiveMap Then
                Dim Set_ClassExtensions As New cls_Set_ClassExtensions
                Set_ClassExtensions.Set_ScreenRectangle = ScreenRectangle
                '  Set_ClassExtensions.set_User_ClassExtensions()

                pNSEditorTools.StartEditing_NSMap(MapControl)
                pNSEditorTools.SketchTool_NSMap(MapControl)

                NSTool_DesignUsers.OnCreate(MapControl.Object)
                NSTool_DesignUsers.Set_ScreenRectangle = ScreenRectangle
                NSTool_DesignUsers.OnClick()
            ElseIf NsActivePage Then

            End If
        Else
            pNSEditorTools.SaveEditing_NSMap(MapControl)
            pNSEditorTools.StopEditing_NSMap(MapControl)
            BtnDesign_Users_ToolbarDesign.Checked = False
        End If
        Exit Sub
Err:
        If EngineEditor.EditState = esriEngineEditState.esriEngineStateEditing Then
            EngineEditor.AbortOperation()
            EngineEditor.StopEditing(False)
        End If
        MapControl.CurrentTool = Nothing
        BtnDesign_Users_ToolbarDesign.Checked = False
    End Sub

    Private Sub BtnDesign_Valves_ToolbarDesign_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDesign_Valves_ToolbarDesign.Click
        On Error GoTo Err
        ' Dim Checked As Boolean = BtnDesign_Valves.Checked
        Clear_DirtyAreaToolbarDesign()
        BtnDesign_Valves_ToolbarDesign.Checked = True
        If EngineEditor.EditState = esriEngineEditState.esriEngineStateNotEditing Then
            If NsActiveMap Then
                Dim Set_ClassExtensions As New cls_Set_ClassExtensions
                Set_ClassExtensions.Set_ScreenRectangle = ScreenRectangle
                ' Set_ClassExtensions.set_Valves_ClassExtensions()

                pNSEditorTools.StartEditing_NSMap(MapControl)
                pNSEditorTools.SketchTool_NSMap(MapControl)

                NSTool_DesignValves.OnCreate(MapControl.Object)
                NSTool_DesignValves.Set_ScreenRectangle = ScreenRectangle
                NSTool_DesignValves.OnClick()
            ElseIf NsActivePage Then

            End If
        Else
            pNSEditorTools.SaveEditing_NSMap(MapControl)
            pNSEditorTools.StopEditing_NSMap(MapControl)
            BtnDesign_Valves_ToolbarDesign.Checked = False
        End If
        Exit Sub
Err:
        If EngineEditor.EditState = esriEngineEditState.esriEngineStateEditing Then
            EngineEditor.AbortOperation()
            EngineEditor.StopEditing(False)
        End If
        MapControl.CurrentTool = Nothing
        BtnDesign_Valves_ToolbarDesign.Checked = False
    End Sub
#End Region

    Private Sub RBtnShowToolbarDesignNetwork_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBtnShowToolbarDesignNetwork.Click

    End Sub
End Class
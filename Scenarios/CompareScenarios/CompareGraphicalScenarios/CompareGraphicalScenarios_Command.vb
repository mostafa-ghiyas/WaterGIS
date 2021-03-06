Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.esriSystem
Imports System.Windows.Forms

<ComClass(CompareGraphicalScenarios_Command.ClassId, CompareGraphicalScenarios_Command.InterfaceId, CompareGraphicalScenarios_Command.EventsId), _
 ProgId("WaterEngine_AnalysisNetwork.CompareGraphicalScenarios_Command")> _
Public NotInheritable Class CompareGraphicalScenarios_Command
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "27e5610e-b936-4ac4-bfc8-0379a99764b6"
    Public Const InterfaceId As String = "35d81350-cd17-495b-a1ff-88db44b958f7"
    Public Const EventsId As String = "170ae85a-3b32-426a-84cb-a066ef8ec9c8"
#End Region

#Region "COM Registration Function(s)"
    <ComRegisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub RegisterFunction(ByVal registerType As Type)
        ' Required for ArcGIS Component Category Registrar support
        ArcGISCategoryRegistration(registerType)

        'Add any COM registration code after the ArcGISCategoryRegistration() call

    End Sub

    <ComUnregisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub UnregisterFunction(ByVal registerType As Type)
        ' Required for ArcGIS Component Category Registrar support
        ArcGISCategoryUnregistration(registerType)

        'Add any COM unregistration code after the ArcGISCategoryUnregistration() call

    End Sub

#Region "ArcGIS Component Category Registrar generated code"
    Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        ControlsCommands.Register(regKey)

    End Sub
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        ControlsCommands.Unregister(regKey)

    End Sub

#End Region
#End Region


    Private m_hookHelper As IHookHelper
    Private m_MapMain As IMap
    Private m_MapMapControl As IMap
    Private ACViewMapControl, ACViewMain As IActiveView
    Private WithEvents Frm_CompareGraphicScenario As CompareScenarioGraphics_Form
    Private m_MapColtrolCompareScenarios As MapControl
    Private WithEvents RbtnGraphicPipes, RbtnGraphicJuncs As RadioButton
    Private RGBColorGraphic As IRgbColor
    Private m_pActiveViewEvents As IActiveViewEvents_Event
    Private ResultMessage As String
    Private ScreenRectangle As Rectangle
    Private ProgressBar_MainForm As ProgressBar

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        ' TODO: Define values for the public properties
        MyBase.m_category = ""  'localizable text 
        MyBase.m_caption = ""   'localizable text 
        MyBase.m_message = ""   'localizable text 
        MyBase.m_toolTip = "" 'localizable text 
        MyBase.m_name = ""  'unique id, non-localizable (e.g. "MyCategory_MyCommand")

        Try
            'TODO: change bitmap name if necessary
            Dim bitmapResourceName As String = Me.GetType().Name + ".bmp"
            MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
        End Try
    End Sub


    Public Overrides Sub OnCreate(ByVal hook As Object)
        If m_hookHelper Is Nothing Then m_hookHelper = New HookHelperClass

        If Not hook Is Nothing Then
            m_hookHelper.Hook = hook
        End If

        If m_hookHelper.ActiveView Is Nothing Then m_hookHelper = Nothing
    End Sub

    Public WriteOnly Property Set_MapControlScenario() As MapControl
        Set(ByVal value As MapControl)
            m_MapColtrolCompareScenarios = value
        End Set
    End Property

    Public WriteOnly Property Set_ScreenRectangle() As Rectangle
        Set(ByVal value As Rectangle)
            ScreenRectangle = value
        End Set
    End Property

    Public WriteOnly Property Set_Prog() As ProgressBar
        Set(ByVal value As ProgressBar)
            ProgressBar_MainForm = value
        End Set
    End Property

    Public Overrides Sub OnClick()
        On Error GoTo err

        Dim Win32 As IWin32Window

        If m_hookHelper Is Nothing Then Return
        If TypeOf m_hookHelper.FocusMap Is IMap Then
            m_MapMain = TryCast(m_hookHelper.FocusMap, IMap)
        End If

        If m_MapMain Is Nothing Then Return
        If m_MapColtrolCompareScenarios Is Nothing Then Return

        m_pActiveViewEvents = m_MapMain
        AddHandler m_pActiveViewEvents.ContentsChanged, AddressOf On_m_pActiveViewEvents_ContentsChanged
        m_MapMapControl = m_MapColtrolCompareScenarios.Map
        ACViewMapControl = m_MapColtrolCompareScenarios.ActiveView
        ACViewMain = m_MapMain
      
        If Frm_CompareGraphicScenario Is Nothing Then
            Frm_CompareGraphicScenario = New CompareScenarioGraphics_Form
        End If
        RbtnGraphicPipes = Frm_CompareGraphicScenario.rbtnGraphicPipes
        RbtnGraphicJuncs = Frm_CompareGraphicScenario.rbtnGraphicJuncs
        Frm_CompareGraphicScenario.Show(Win32)

        AddLayerToMapControlScenarios()
        FullExtentView()
        RbtnGraphicPipes.Checked = True
        Exit Sub
Err:
        If ResultMessage = "" Then
            ResultMessage = "Failure in show result"
        End If
        ProgressBar_MainForm.Value = 0
        Show_AlertCustom(ResultMessage)

    End Sub

    Private Sub FullExtentView()
        ACViewMapControl.Extent = ACViewMapControl.FullExtent
        ACViewMapControl.Refresh()
        ACViewMain.Extent = ACViewMain.FullExtent
        ACViewMain.Refresh()
    End Sub

    Private Sub On_m_pActiveViewEvents_ContentsChanged()
        AddLayerToMapControlScenarios()
    End Sub

    Private Sub AddLayerToMapControlScenarios()
        On Error GoTo Err

        Dim EnumLayers As IEnumLayer
        Dim Layer As ILayer
        Dim pUID As IUID
        Dim Coll_Layers As Collection

        pUID = New UIDClass
        pUID.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}"
        EnumLayers = m_MapMain.Layers(pUID, True)

        Layer = EnumLayers.Next
        Coll_Layers = New Collection
        While Layer IsNot Nothing
            If Layer.Visible Then
                Coll_Layers.Add(Layer)
            End If
            Layer = EnumLayers.Next
        End While

        For i = Coll_Layers.Count - 1 To 0 Step -1
            Layer = CType(Coll_Layers.Item(i + 1), ILayer)
            If Layer IsNot Nothing Then
                m_MapColtrolCompareScenarios.AddLayer(Layer)
            End If
        Next
        ACViewMapControl.Refresh()
        Removegraphics()
        Exit Sub
Err:
        If ResultMessage = "" Then
            ResultMessage = "اشکال در نمایش نتایج حاصل از مقایسه سناریوها"
        End If
    End Sub

    Public Sub Removegraphics()
        'If Frm_CompareGraphicScenario IsNot Nothing Then Frm_CompareGraphicScenario.Close()
        m_MapMain = m_hookHelper.FocusMap
        m_pActiveViewEvents = m_MapMain
        m_MapMapControl = m_MapColtrolCompareScenarios.Map
        ACViewMapControl = m_MapColtrolCompareScenarios.ActiveView
        ACViewMain = m_MapMain

        Dim ConGraphics As IGraphicsContainer = m_MapMain
        ConGraphics.DeleteAllElements()
        ConGraphics = m_MapMapControl
        ConGraphics.DeleteAllElements()
        ACViewMapControl.PartialRefresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)
        ACViewMain.PartialRefresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)
    End Sub

    Public Sub Close_FormCompareGraphics()
        If Frm_CompareGraphicScenario IsNot Nothing Then Frm_CompareGraphicScenario.Close()
    End Sub

    Private Sub ProgressBar(ByVal Value As Integer)
        ProgressBar_MainForm.Value = Value
        Application.DoEvents()
    End Sub

    Public Sub AddGraphicEdges()
        On Error GoTo Err

        Dim Fields As IFields
        Dim CursorTable As ICursor
        Dim RowCount As Integer
        Dim Row As IRow
        Dim IndexVelocityFieldScenario1 As Integer
        Dim IndexVelocityFieldScenario2 As Integer
        Dim Velocity As Double
        Dim PipeFeature As IFeature
        Dim GeoPipeFeature As IGeometry

        If m_TableEdgeScenarios Is Nothing Then
            ResultMessage = "Please complete the basic configuration of software"
            Show_AlertCustom(ResultMessage)
            Exit Sub
        End If



        Fields = m_TableEdgeScenarios.Fields
        RGBColorGraphic = New RgbColor

        IndexVelocityFieldScenario1 = m_TableEdgeScenarios.FindField("VelocityPipe1")
        IndexVelocityFieldScenario2 = m_TableEdgeScenarios.FindField("VelocityPipe2")
        If IndexVelocityFieldScenario1 = -1 Then Exit Sub
        If IndexVelocityFieldScenario2 = -1 Then Exit Sub

        CursorTable = m_TableEdgeScenarios.Search(Nothing, False)
        RowCount = m_TableEdgeScenarios.RowCount(Nothing)

        ProgressBar(90)

        For i = 0 To RowCount - 1
            Row = CursorTable.NextRow
            Velocity = Math.Abs(Row.Value(IndexVelocityFieldScenario1))
            PipeFeature = m_FCMainPipe.GetFeature(Row.Value(m_TableEdgeScenarios.FindField("NumPipe")))
            If PipeFeature Is Nothing Then Exit Sub

            GeoPipeFeature = PipeFeature.ShapeCopy
            If Not GeoPipeFeature.IsEmpty Then
                If Velocity > 0.2 And Velocity < 3 Then
                    CollorGraphics(0, 0, 255)
                ElseIf Velocity < 0.2 Then
                    CollorGraphics(0, 255, 0)
                Else
                    CollorGraphics(255, 0, 0)
                End If
                AddGraphicToMap(m_MapMapControl, GeoPipeFeature, RGBColorGraphic, RGBColorGraphic)

                Velocity = Math.Abs(Row.Value(IndexVelocityFieldScenario2))
                If Velocity > 0.2 And Velocity < 3 Then
                    CollorGraphics(0, 0, 255)
                ElseIf Velocity < 0.2 Then
                    CollorGraphics(0, 255, 0)
                Else
                    CollorGraphics(255, 0, 0)
                End If
                AddGraphicToMap(m_MapMain, GeoPipeFeature, RGBColorGraphic, RGBColorGraphic)
            End If
        Next
        ACViewMapControl.PartialRefresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)
        ACViewMain.PartialRefresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)

        ProgressBar_MainForm.Value = 0
        Exit Sub
Err:
        If ResultMessage = "" Then
            ' ResultMessage = "اشکال در اضافه کردن گرافیک لوله ها"
            ResultMessage = "Failure in adding graphics on pipes"
        End If

        ProgressBar_MainForm.Value = 0
    End Sub

    Public Sub AddGraphicJuns()
        On Error GoTo Err

        Dim Fields As IFields
        Dim CursorTable As ICursor
        Dim RowCount As Integer
        Dim Row As IRow
        Dim IndexHeadFieldScenario1 As Integer
        Dim IndexHeadFieldScenario2 As Integer
        Dim HeadJunc As Double
        Dim juncFeature As IFeature
        Dim GeojuncFeature As IGeometry

        If m_TableJuncScenarios Is Nothing Then
            ResultMessage = "Please complete the basic configuration of software"
            Show_AlertCustom(ResultMessage)
            Exit Sub
        End If

        Fields = m_TableEdgeScenarios.Fields
        RGBColorGraphic = New RgbColor

        IndexHeadFieldScenario1 = m_TableJuncScenarios.FindField("HeadLossJunc1")
        IndexHeadFieldScenario2 = m_TableJuncScenarios.FindField("HeadLossJunc2")
        If IndexHeadFieldScenario1 = -1 Then Exit Sub
        If IndexHeadFieldScenario2 = -1 Then Exit Sub

        CursorTable = m_TableJuncScenarios.Search(Nothing, False)
        RowCount = m_TableJuncScenarios.RowCount(Nothing)


        ProgressBar(90)

        For i = 0 To RowCount - 1
            Row = CursorTable.NextRow
            HeadJunc = Math.Abs(Row.Value(IndexHeadFieldScenario1))
            juncFeature = m_FCJunction.GetFeature(Row.Value(m_TableJuncScenarios.FindField("NumJunc")))
            If juncFeature Is Nothing Then Exit Sub

            GeojuncFeature = juncFeature.ShapeCopy
            If Not GeojuncFeature.IsEmpty Then
                If HeadJunc > 20 And HeadJunc < 50 Then
                    CollorGraphics(0, 0, 255)
                ElseIf HeadJunc < 20 Then
                    CollorGraphics(0, 255, 0)
                Else
                    CollorGraphics(255, 0, 0)
                End If
                AddGraphicToMap(m_MapMapControl, GeojuncFeature, RGBColorGraphic, RGBColorGraphic)

                HeadJunc = Math.Abs(Row.Value(IndexHeadFieldScenario2))
                If HeadJunc > 20 And HeadJunc < 50 Then
                    CollorGraphics(0, 0, 255)
                ElseIf HeadJunc < 20 Then
                    CollorGraphics(0, 255, 0)
                Else
                    CollorGraphics(255, 0, 0)
                End If
                AddGraphicToMap(m_MapMain, GeojuncFeature, RGBColorGraphic, RGBColorGraphic)
            End If
        Next
        ACViewMapControl.PartialRefresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)
        ACViewMain.PartialRefresh(esriViewDrawPhase.esriViewGraphics, Nothing, Nothing)

        ' ProgressBar(ProgressBar_MainForm.Value, 20)
        ProgressBar_MainForm.Value = 0
        Exit Sub
Err:
        If ResultMessage = "" Then
            ' ResultMessage = "اشکال در اضافه کردن گرافیک لوله ها"
            ResultMessage = "Failure in adding graphics on junctions"
        End If
        ProgressBar_MainForm.Value = 0
    End Sub

    Public Sub AddGraphicToMap(ByVal map As IMap, ByVal geometry As IGeometry, ByVal rgbColor As IRgbColor, ByVal outlineRgbColor As IRgbColor)

        Dim graphicsContainer As IGraphicsContainer = CType(map, IGraphicsContainer) ' Explicit Cast
        Dim element As IElement = Nothing

        If (geometry.GeometryType) = esriGeometryType.esriGeometryPoint Then

            ' Marker symbols
            Dim simpleMarkerSymbol As ISimpleMarkerSymbol = New SimpleMarkerSymbolClass()
            simpleMarkerSymbol.Color = rgbColor
            simpleMarkerSymbol.Outline = True
            simpleMarkerSymbol.OutlineColor = outlineRgbColor
            simpleMarkerSymbol.Size = 5
            simpleMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle

            Dim markerElement As IMarkerElement = New MarkerElementClass()
            markerElement.Symbol = simpleMarkerSymbol
            element = CType(markerElement, IElement) ' Explicit Cast

        ElseIf (geometry.GeometryType) = esriGeometryType.esriGeometryPolyline Then

            '  Line elements
            Dim simpleLineSymbol As ISimpleLineSymbol = New SimpleLineSymbolClass()
            simpleLineSymbol.Color = rgbColor
            simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid
            simpleLineSymbol.Width = 4

            Dim lineElement As ILineElement = New LineElementClass()
            lineElement.Symbol = simpleLineSymbol
            element = CType(lineElement, IElement) ' Explicit Cast

        ElseIf (geometry.GeometryType) = esriGeometryType.esriGeometryPolygon Then

            ' Polygon elements
            Dim simpleFillSymbol As ISimpleFillSymbol = New SimpleFillSymbolClass()
            simpleFillSymbol.Color = rgbColor
            simpleFillSymbol.Style = esriSimpleFillStyle.esriSFSForwardDiagonal
            Dim fillShapeElement As IFillShapeElement = New PolygonElementClass()
            fillShapeElement.Symbol = simpleFillSymbol
            element = CType(fillShapeElement, IElement) ' Explicit Cast

        End If

        If Not (element Is Nothing) Then

            element.Geometry = geometry
            graphicsContainer.AddElement(element, 0)

        End If

    End Sub

    Private Function CollorGraphics(ByVal Red As Integer, ByVal Blue As Integer, ByVal Green As Integer) As IRgbColor

        RGBColorGraphic.Blue = Blue
        RGBColorGraphic.Red = Red
        RGBColorGraphic.Green = Green
        CollorGraphics = RGBColorGraphic

    End Function

    Protected Overrides Sub Finalize()
        m_MapColtrolCompareScenarios = Nothing
        m_pActiveViewEvents = Nothing
        MyBase.Finalize()
    End Sub

    Private Sub RbtnGraphicPipes_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbtnGraphicPipes.CheckedChanged
        On Error GoTo Err

        ResultMessage = ""
        Removegraphics()
        If RbtnGraphicPipes.Checked Then
            AddGraphicEdges()
        End If
        Exit Sub
Err:
        If ResultMessage = "" Then
            ' ResultMessage = "اشکال در اضافه کردن گرافیک لوله ها"
            ResultMessage = "Failure in adding graphics on pipes"
        End If
        Show_AlertCustom(ResultMessage)
    End Sub

    Private Sub RbtnGraphicJuncs_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbtnGraphicJuncs.CheckedChanged
        On Error GoTo Err

        ResultMessage = ""
        Removegraphics()
        If RbtnGraphicJuncs.Checked Then
            AddGraphicJuns()
        End If
        Exit Sub
Err:
        If ResultMessage = "" Then
            ' ResultMessage = "اشکال در اضافه کردن گرافیک گره ها"
            ResultMessage = "Failure in adding graphics on junctions"
        End If
        Show_AlertCustom(ResultMessage)
    End Sub

    Private Sub Show_AlertCustom(ByVal MessageError As String)
        Dim AlertCustom As New AlertCustom_Cls
        AlertCustom.Set_ScreenRectangle = ScreenRectangle
        AlertCustom.ShowLoadAlert(Nothing, MessageError)
    End Sub

    Private Sub Frm_CompareGraphicScenario_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Frm_CompareGraphicScenario.FormClosing
        ProgressBar_MainForm = Nothing
        Frm_CompareGraphicScenario = Nothing
    End Sub
End Class




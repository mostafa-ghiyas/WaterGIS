Imports ESRI.ArcGIS.Carto
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.esriSystem


Public Class SearchFeatures_Form

    Private m_hookHelper As IHookHelper = Nothing
    Private WithEvents Map_Event As Map
    Private layerCollection1 As Collection
    Private layerCollection2 As Collection
    Private fieldCollection As Collection
    Private featureCollection As Collection
    Private fSelectedCollection As Collection
    Public pVarField As String
    Public pVarValue As Integer
    Private item As ListViewItem
    Private pLayer As ILayer
    Private pMap As IMap
    Private pAView As IActiveView
    Private pFLayer As IFeatureLayer2
    Private NumSearch As Integer
    Private ResultMessage As String

    Public Sub New(ByVal hookHelper As IHookHelper)
        InitializeComponent()
        m_hookHelper = hookHelper
    End Sub

    Private Sub SearchFeatures_Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        On Error GoTo Err

        If m_hookHelper Is Nothing Then Exit Sub
        pMap = TryCast(m_hookHelper.FocusMap, IMap)
        If pMap Is Nothing Then Exit Sub


        Map_Event = pMap
        pAView = pMap
        Dim uid As ESRI.ArcGIS.esriSystem.IUID = New ESRI.ArcGIS.esriSystem.UIDClass
        uid.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}" '  IGeoFeatureLayer
        Dim EnumLayer As IEnumLayer = pMap.Layers((CType(uid, ESRI.ArcGIS.esriSystem.UID)), True)
        EnumLayer.Reset()
        cboLayer.Items.Clear()
        cboLayer.Items.Add("All Layers")
        layerCollection1 = New Collection
        Dim layer As ILayer = EnumLayer.Next
        Do While Not layer Is Nothing
            cboLayer.Items.Add(layer.Name)
            layerCollection1.Add(layer)
            layer = EnumLayer.Next
        Loop

        rbtnAllField.Checked = True
        item = Nothing
        fSelectedCollection = New Collection
        cboLayer.SelectedIndex = 0
        Exit Sub
Err:
        If ResultMessage = "" Then
            ResultMessage = "Failure in Load Form"
        End If
        Show_AlertCustom(ResultMessage)
    End Sub

    Private Sub Show_AlertCustom(ByVal MessageError As String)
        Dim AlertCustom As New AlertCustom_Cls
        AlertCustom.ShowLoadAlert(Me, MessageError)
    End Sub

    Private Sub rbtnInField_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnInField.CheckedChanged
        Dim Layers As IEnumLayer
        Dim Layer As IFeatureLayer
        Dim pUID As IUID = New UIDClass
        pUID.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}"

        cboField.Items.Clear()
        fieldCollection = New Collection
        If rbtnInField.Checked Then
            If cboLayer.SelectedIndex <> 0 Then
                If cboLayer.SelectedIndex < 0 Then Exit Sub
                pFLayer = CType(layerCollection1.Item(cboLayer.SelectedIndex), IFeatureLayer2)
                AddField_rbtnInField()
            ElseIf cboLayer.SelectedIndex = 0 Then
                Layers = Map_Event.Layers(pUID, True)
                Layer = Layers.Next
                While Layer IsNot Nothing
                    pFLayer = Layer
                    AddField_rbtnInField()
                    Layer = Layers.Next
                End While
            End If
        End If
    End Sub

    Private Sub AddField_rbtnInField()
        Dim pFClass As IFeatureClass
        Dim pFields As IFields
        Dim pBool As Boolean = True
        cboField.Enabled = True
        pFClass = pFLayer.FeatureClass
        pFields = pFClass.Fields
        For i = 0 To pFields.FieldCount - 1
            For j = 0 To cboField.Items.Count - 1
                If cboField.Items.Item(j) = pFields.Field(i).AliasName Then
                    pBool = False
                    Exit For
                Else
                    pBool = True
                End If
            Next
            If pBool Then
                If pFields.Field(i).Type <> esriFieldType.esriFieldTypeBlob And pFields.Field(i).Type <> esriFieldType.esriFieldTypeGeometry Then
                    If pFields.Field(i).Domain IsNot Nothing Then
                        cboField.Items.Add(pFields.Field(i).AliasName)
                        fieldCollection.Add(pFields.Field(i))
                    End If
                   
                End If
            End If
        Next
    End Sub

    Private Sub rbtnAllField_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtnAllField.CheckedChanged
        If rbtnAllField.Checked Then
            cboField.Items.Clear()
            cboField.Enabled = False
        End If
    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        On Error GoTo Err
        ResultMessage = ""

        NumSearch = 0
        lstResult.Items.Clear()
        If txtFind.Text = "" Then
            ResultMessage = "Please enter a value to search for "
            GoTo Err
        End If
        If cboLayer.Text = "" Then
            ResultMessage = "Please choose a layer "
            GoTo Err
        Else
            If rbtnInField.Checked Then
                SearchInField()
            ElseIf rbtnAllField.Checked Then
                SearchAllField()
            End If
            If ResultMessage <> "" Then GoTo Err
        End If

        Panel_NumberOfFeature.Text = " Number of found features = " & NumSearch
        Exit Sub
Err:
        If ResultMessage = "" Then
            ResultMessage = "Failure in Search features"
        End If
        Show_AlertCustom(ResultMessage)
    End Sub

    Private Sub SearchInField()
        On Error GoTo Err

        featureCollection = New Collection
        layerCollection2 = New Collection
        If Not cboField.SelectedIndex >= 0 Then
            ResultMessage = "Please choose a field "
            GoTo Err
        End If
        If cboLayer.SelectedIndex <> 0 Then
            pFLayer = CType(layerCollection1.Item(cboLayer.SelectedIndex), IFeatureLayer2)
            SearchIn()
        ElseIf cboLayer.SelectedIndex = 0 Then
            For i = 0 To pMap.LayerCount - 1
                If TypeOf pMap.Layer(i) Is IFeatureLayer2 Then
                    pFLayer = pMap.Layer(i)
                    SearchIn()
                End If
            Next
        End If
        Exit Sub
Err:
        If ResultMessage = "" Then
            ResultMessage = "Failure in Search features"
        End If
    End Sub

    Private Sub SearchIn()
        On Error GoTo Err

        Dim pFCursor As IFeatureCursor
        Dim pQFilter As IQueryFilter = New QueryFilter

        Dim pField As IField
        Dim pExistField As Boolean = False
        Dim pExistFeature As Boolean = True
        pLayer = pFLayer

        pField = CType(fieldCollection.Item(cboField.SelectedIndex + 1), IField)

        If pField.Type = esriFieldType.esriFieldTypeGeometry Or pField.Type = esriFieldType.esriFieldTypeString Or pField.Type = esriFieldType.esriFieldTypeBlob Then
            lstResult.Items.Clear()
            Exit Sub
        End If
        For i = 0 To pFLayer.FeatureClass.Fields.FieldCount - 1
            If pField.AliasName = pFLayer.FeatureClass.Fields.Field(i).AliasName Then
                pExistField = True
            End If
        Next
        If pExistField Then
            pVarValue = txtFind.Text
            pQFilter.WhereClause = pField.Name & "=" & CStr(pVarValue)
            pFCursor = pFLayer.Search(pQFilter, False)
            Dim pF As IFeature = pFCursor.NextFeature
            Do While Not pF Is Nothing
                If featureCollection.Count > 0 Then
                    For i = 1 To featureCollection.Count
                        If CType(featureCollection.Item(i), IFeature) Is pF Then
                            pExistFeature = False

                        End If
                    Next
                End If

                If pExistFeature Then
                    Dim item As New ListViewItem(pLayer.Name)
                    item.SubItems.Add(pF.OID)
                    item.SubItems.Add(pField.AliasName)
                    lstResult.Items.Add(item)
                    featureCollection.Add(pF)
                    layerCollection2.Add(pLayer)
                    NumSearch += 1
                End If
                pF = pFCursor.NextFeature
            Loop
        End If

        Exit Sub
Err:
        If ResultMessage = "" Then
            ResultMessage = "Failure in Search features"
        End If
    End Sub

    Private Sub SearchAllField()
        On Error GoTo Err

        featureCollection = New Collection
        layerCollection2 = New Collection
        If cboLayer.Text <> "All Layers" Then
            pFLayer = CType(layerCollection1.Item(cboLayer.SelectedIndex), IFeatureLayer2)
            SearchAll()
        ElseIf cboLayer.Text = "All Layers" Then
            For i = 0 To pMap.LayerCount - 1
                If TypeOf pMap.Layer(i) Is IFeatureLayer2 Then
                    pFLayer = pMap.Layer(i)
                    SearchAll()
                End If
            Next
        End If

        Exit Sub
Err:
        If ResultMessage = "" Then
            ResultMessage = "Failure in Search features"
        End If
    End Sub
    Private Sub SearchAll()
        On Error GoTo Err

        Dim pFCursor As IFeatureCursor
        Dim pQFilter As IQueryFilter = New QueryFilter
        Dim pFClass As IFeatureClass
        Dim pFields As IFields
        Dim pField As IField
        Dim pExistFeature As Boolean = True
        pLayer = pFLayer

        pFClass = pFLayer.FeatureClass
        pFields = pFClass.Fields
        For i = 0 To pFields.FieldCount - 1
            pField = pFields.Field(i)
            pVarValue = CStr(txtFind.Text)
            If pField.Type <> esriFieldType.esriFieldTypeGeometry And pField.Type <> esriFieldType.esriFieldTypeString And pField.Type <> esriFieldType.esriFieldTypeBlob Then

                pQFilter.WhereClause = pField.Name & "=" & pVarValue
                pFCursor = pFLayer.Search(pQFilter, False)
                Dim pF As IFeature
                pF = pFCursor.NextFeature
                Do While Not pF Is Nothing
                    If featureCollection.Count > 0 Then
                        For j = 1 To featureCollection.Count
                            If CType(featureCollection.Item(j), IFeature) Is pF Then
                                pExistFeature = False

                            End If
                        Next
                    End If
                    If pExistFeature Then
                        Dim item As New ListViewItem(pLayer.Name)
                        item.SubItems.Add(pF.OID)
                        item.SubItems.Add(pField.AliasName)
                        lstResult.Items.Add(item)
                        featureCollection.Add(pF)
                        layerCollection2.Add(pLayer)
                        NumSearch += 1
                    End If
                    pF = pFCursor.NextFeature
                Loop
            End If
        Next
        Exit Sub
Err:
        If ResultMessage = "" Then
            ResultMessage = "Failure in Search features"
        End If
    End Sub

    Private Sub btnNewSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewSearch.Click
        cboField.Text = ""
        rbtnAllField.Checked = True
        lstResult.Items.Clear()
        txtFind.Text = ""
        Panel_NumberOfFeature.Text = ""
        pMap.ClearSelection()
        pAView.Refresh()
    End Sub

    Private Sub SelectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectToolStripMenuItem.Click
        Dim pF As IFeature = CType(featureCollection.Item(item.Index + 1), IFeature)
        Dim pLayer As ILayer = CType(layerCollection2.Item(item.Index + 1), ILayer)
        pMap.SelectFeature(pLayer, pF)
        fSelectedCollection.Add(pF)
        pAView.Refresh()
    End Sub

    Private Sub UnSelectToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UnSelectToolStripMenuItem.Click
        Dim pF As IFeature = CType(featureCollection.Item(item.Index + 1), IFeature)
        Dim pLayer As ILayer = CType(layerCollection2.Item(item.Index + 1), ILayer)
        Dim FSelection As IFeatureSelection = pLayer
        Dim SSet As ISelectionSet = FSelection.SelectionSet
        Dim pSelectionEvents As ISelectionEvents
        SSet.RemoveList(1, pF.OID)
        pSelectionEvents = pMap 'QI
        pAView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, Nothing, pAView.Extent)
        pSelectionEvents.SelectionChanged()
    End Sub

    Private Sub cboLayer_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboLayer.SelectedIndexChanged
        cboField.Text = ""
        cboField.Items.Clear()
        rbtnInField_CheckedChanged(sender, e)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        featureCollection = Nothing
        layerCollection1 = Nothing
        layerCollection2 = Nothing
        fieldCollection = Nothing
        Me.Close()
    End Sub

    Private Sub ZoomInToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZoomInToolStripMenuItem.Click
        Dim pF As IFeature = CType(featureCollection.Item(item.Index + 1), IFeature)
        Dim Layer As ILayer = CType(layerCollection2.Item(item.Index + 1), ILayer)

        Dim pEnvelope As IEnvelope
        If pF.Shape.GeometryType <> esriGeometryType.esriGeometryPoint Then
            pEnvelope = pF.Extent
            pEnvelope.Expand(1.5, 1.5, True)

        Else
            pEnvelope = New EnvelopeClass()

            'create a small rectangle
            Dim RECT As ESRI.ArcGIS.Display.tagRECT = New tagRECT()

            'transform rectangle into map units and apply to the tempEnv envelope
            Dim dispTrans As IDisplayTransformation = pAView.ScreenDisplay.DisplayTransformation
            dispTrans.TransformRect(pEnvelope, RECT, 4) 'esriDisplayTransformationEnum.esriTransformToMap);
            pEnvelope.Width = pAView.ScreenDisplay.DisplayTransformation.VisibleBounds.Width / 10
            pEnvelope.Height = pAView.ScreenDisplay.DisplayTransformation.VisibleBounds.Height / 10

            pEnvelope.CenterAt(pF.ShapeCopy)
            pEnvelope.SpatialReference = pMap.SpatialReference

        End If
        pAView.Extent = pEnvelope
        pAView.Refresh()
    End Sub

    Private Sub BookmarkToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BookmarkToolStripMenuItem1.Click
        Dim pLayer As ILayer = CType(layerCollection2.Item(item.Index + 1), ILayer)
        Dim pFL As IFeatureLayer = pLayer
        Dim pF As IFeature = CType(featureCollection.Item(item.Index + 1), IFeature)
        Dim pB As IMapBookmarks = pMap
        Dim pFB As IFeatureBookmark = New FeatureBookmark
        pFB.FeatureClass = pFL.FeatureClass
        pFB.FeatureId = pF.OID
        pFB.Name = txtFind.Text
        pB.AddBookmark(pFB)
    End Sub


    Private Sub Map_Event_ItemAdded(ByVal Item As Object) Handles Map_Event.ItemAdded
        If TypeOf Item Is ILayer Then
            Dim uid As ESRI.ArcGIS.esriSystem.IUID = New ESRI.ArcGIS.esriSystem.UIDClass
            uid.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}" '  IGeoFeatureLayer
            Dim EnumLayer As IEnumLayer = pMap.Layers((CType(uid, ESRI.ArcGIS.esriSystem.UID)), True)
            EnumLayer.Reset()
            cboLayer.Items.Clear()
            cboLayer.Items.Add("All Layers")
            layerCollection1 = New Collection
            Dim layer As ILayer = EnumLayer.Next
            Do While Not layer Is Nothing
                cboLayer.Items.Add(layer.Name)
                layerCollection1.Add(layer)
                layer = EnumLayer.Next
            Loop
        End If
    End Sub

    Private Sub Map_Event_ItemDeleted(ByVal Item As Object) Handles Map_Event.ItemDeleted
        If TypeOf Item Is ILayer Then
            Dim uid As ESRI.ArcGIS.esriSystem.IUID = New ESRI.ArcGIS.esriSystem.UIDClass
            uid.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}" '  IGeoFeatureLayer
            Dim EnumLayer As IEnumLayer = pMap.Layers((CType(uid, ESRI.ArcGIS.esriSystem.UID)), True)
            EnumLayer.Reset()
            cboLayer.Items.Clear()
            cboLayer.Items.Add("All Layers")
            layerCollection1 = New Collection
            Dim layer As ILayer = EnumLayer.Next
            Do While Not layer Is Nothing
                cboLayer.Items.Add(layer.Name)
                layerCollection1.Add(layer)
                layer = EnumLayer.Next
            Loop
        End If
    End Sub

    Private Sub lstResult_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstResult.MouseDown
        item = lstResult.GetItemAt(e.X, e.Y)
        If Not item Is Nothing Then
            If e.Button = Windows.Forms.MouseButtons.Right Then
                lstResult.ContextMenuStrip = ContextMenu_LstResult
            End If
        Else
            lstResult.ContextMenuStrip = Nothing
        End If
    End Sub



End Class
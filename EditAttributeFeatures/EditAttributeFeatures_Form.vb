Imports ESRI.ArcGIS.Controls
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geodatabase
Imports System.Windows.Forms


Public Class EditAttributeForm
    Private m_hookHelper As IHookHelper = Nothing
    Private Map As IMap
    Private m_ActiveViewEventsSelectionChanged As ESRI.ArcGIS.Carto.IActiveViewEvents_SelectionChangedEventHandler
    Private ResultMessage As String
    Private b_ChageSizeFormValves, b_ChageSizeFormUsers As Boolean
    Private WorkspaceEdit As IWorkspaceEdit
    Private ActiveView As IActiveView
    Private SelectedNode As TreeNode

    Public Sub New(ByVal hookHelper As IHookHelper)
        InitializeComponent()
        m_hookHelper = hookHelper
    End Sub


    Private Sub BtnChangeSizeFormValves_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnChangeSizeFormValves.Click
        b_ChageSizeFormValves = Not b_ChageSizeFormValves
        If b_ChageSizeFormValves Then
            Me.Height = 379
            BtnChangeSizeFormValves.Text = "کمتر<<"
        Else
            BtnChangeSizeFormValves.Text = "بیشتر>>"
            Me.Height = 230
        End If
    End Sub

    Private Sub BtnChangeSizeFormUsers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnChangeSizeFormUsers.Click
        b_ChageSizeFormUsers = Not b_ChageSizeFormUsers
        If b_ChageSizeFormUsers Then
            Me.Height = 487
            BtnChangeSizeFormUsers.Text = "کمتر<<"
        Else
            Me.Height = 230
            BtnChangeSizeFormUsers.Text = "بیشتر>>"
        End If
    End Sub

    Private Sub EditAttributeFeatures_Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        On Error GoTo Err

        If m_hookHelper Is Nothing Then Exit Sub
        Map = TryCast(m_hookHelper.FocusMap, IMap)
        If Map Is Nothing Then Exit Sub
        ActiveView = Map

        m_ActiveViewEventsSelectionChanged = New ESRI.ArcGIS.Carto.IActiveViewEvents_SelectionChangedEventHandler(AddressOf OnActiveViewEventsSelectionChanged)
        AddHandler CType(Map, ESRI.ArcGIS.Carto.IActiveViewEvents_Event).SelectionChanged, m_ActiveViewEventsSelectionChanged


        ResultMessage = ""
        If m_Workspace Is Nothing OrElse m_FCValve Is Nothing Then
            ResultMessage = "Please complete the basic configuration of software"
            Show_AlertCustom(ResultMessage)
            Exit Sub
        End If
        WorkspaceEdit = m_Workspace
        Clear_ClassExtensions()
        InitializeTreeviews()
        InitilizeControls()

        If Map.SelectionCount > 0 Then
            OnActiveViewEventsSelectionChanged()
        End If
        Exit Sub
Err:
        If ResultMessage = "" Then
            ResultMessage = "Failure in Load parameters"
        End If
        Show_AlertCustom(ResultMessage)
    End Sub

    Private Sub Show_AlertCustom(ByVal MessageError As String)
        Dim AlertCustom As New AlertCustom_Cls
        AlertCustom.ShowLoadAlert(Me, MessageError)
    End Sub

    Private Sub InitializeTreeviews()
        Dim MainNode As TreeNode
        MainNode = New TreeNode
        MainNode.Text = "Junctions"
        TreeView_Junctions.Nodes.Add(MainNode)

        MainNode = New TreeNode
        MainNode.Text = "MainPipes"
        TreeView_MainPipes.Nodes.Add(MainNode)

        MainNode = New TreeNode
        MainNode.Text = "Sources"
        TreeView_Sources.Nodes.Add(MainNode)

        MainNode = New TreeNode
        MainNode.Text = "UserPipes"
        TreeView_UserPipes.Nodes.Add(MainNode)

        MainNode = New TreeNode
        MainNode.Text = "Users"
        TreeView_Users.Nodes.Add(MainNode)

        MainNode = New TreeNode
        MainNode.Text = "Valves"
        TreeView_Valves.Nodes.Add(MainNode)
    End Sub

    Private Sub OnActiveViewEventsSelectionChanged()
        ClearDirtyLstResults()
        Dim EnumFeature As IEnumFeature
        Dim Feature As IFeature
        Dim Node As TreeNode
        Dim FClassSelected As IFeatureClass

        EnumFeature = Map.FeatureSelection
        Feature = EnumFeature.Next
        While Feature IsNot Nothing
            FClassSelected = CType(Feature.Class, IFeatureClass)
            If FClassSelected.FeatureClassID = m_IDFCMainPipe Then
                Node = New TreeNode
                Node.Text = Feature.OID
                Node.Tag = Feature
                TreeView_MainPipes.Nodes(0).Nodes.Add(Node)
            ElseIf FClassSelected.FeatureClassID = m_IDFCUserPipe Then
                Node = New TreeNode
                Node.Text = Feature.OID
                Node.Tag = Feature
                TreeView_UserPipes.Nodes(0).Nodes.Add(Node)
            ElseIf FClassSelected.FeatureClassID = m_IDFCSource Then
                Node = New TreeNode
                Node.Text = Feature.OID
                Node.Tag = Feature
                TreeView_Sources.Nodes(0).Nodes.Add(Node)
            ElseIf FClassSelected.FeatureClassID = m_IDFCJunction Then
                Node = New TreeNode
                Node.Text = Feature.OID
                Node.Tag = Feature
                TreeView_Junctions.Nodes(0).Nodes.Add(Node)
            ElseIf FClassSelected.FeatureClassID = m_IDFCUsers Then
                Node = New TreeNode
                Node.Text = Feature.OID
                Node.Tag = Feature
                TreeView_Users.Nodes(0).Nodes.Add(Node)
            ElseIf FClassSelected.FeatureClassID = m_IDFCValve Then
                Node = New TreeNode
                Node.Text = Feature.OID
                Node.Tag = Feature
                TreeView_Valves.Nodes(0).Nodes.Add(Node)
            End If
            Feature = EnumFeature.Next
        End While

        TreeView_MainPipes.ExpandAll()
        TreeView_UserPipes.ExpandAll()
        TreeView_Junctions.ExpandAll()
        TreeView_Sources.ExpandAll()
        TreeView_Users.ExpandAll()
        TreeView_Valves.ExpandAll()
    End Sub

    Private Sub ClearDirtyLstResults()
        TreeView_Junctions.Nodes(0).Nodes.Clear()
        TreeView_MainPipes.Nodes(0).Nodes.Clear()
        TreeView_Sources.Nodes(0).Nodes.Clear()
        TreeView_UserPipes.Nodes(0).Nodes.Clear()
        TreeView_Users.Nodes(0).Nodes.Clear()
        TreeView_Valves.Nodes(0).Nodes.Clear()
    End Sub

    Private Sub InitilizeControls()

        ' MainPipes
        CboMaterial.Items.Add("Cement")
        CboMaterial.Items.Add("Asbestos")
        CboMaterial.Items.Add("Steel")
        CboMaterial.Items.Add("Ductile")
        CboMaterial.Items.Add("Galvanized")
        CboMaterial.Items.Add("Plyka")
        CboMaterial.Items.Add("Cast-iron")
        CboMaterial.Items.Add("Copper")
        CboMaterial.Items.Add("PE")
        CboMaterial.Items.Add("Other")

        ' Users
        ' Initialize CboKarbary
        CboKarbary.Items.Add("Residential")
        CboKarbary.Items.Add("Commercial")
        CboKarbary.Items.Add("Commercial - Residential")
        CboKarbary.Items.Add("Building")
        CboKarbary.Items.Add("Others")



        ' Initialize cboDiaDakheliShir
        cboDiaDakheliShir.Items.Add("63")
        cboDiaDakheliShir.Items.Add("75")
        cboDiaDakheliShir.Items.Add("100")
        cboDiaDakheliShir.Items.Add("150")
        cboDiaDakheliShir.Items.Add("200")


        ' Initialize cboTypeVoroodiPipe
        cboTypeVoroodiPipe.Items.Add("Copper")
        cboTypeVoroodiPipe.Items.Add("Other")
        cboTypeVoroodiPipe.Items.Add("Galvanized")
        cboTypeVoroodiPipe.Items.Add("Cast-iron")
        cboTypeVoroodiPipe.Items.Add("Cement")
        cboTypeVoroodiPipe.Items.Add("Asbestos")
        cboTypeVoroodiPipe.Items.Add("Steel")
        cboTypeVoroodiPipe.Items.Add("Ductile")
        cboTypeVoroodiPipe.Items.Add("Plyka")
        cboTypeVoroodiPipe.Items.Add("PE")

        ' Initialize cboTypeKhoroojiPipe
        cboTypeKhoroojiPipe.Items.Add("Copper")
        cboTypeKhoroojiPipe.Items.Add("Other")
        cboTypeKhoroojiPipe.Items.Add("Galvanized")
        cboTypeKhoroojiPipe.Items.Add("Cast-iron")
        cboTypeKhoroojiPipe.Items.Add("Cement")
        cboTypeKhoroojiPipe.Items.Add("Asbestos")
        cboTypeKhoroojiPipe.Items.Add("Steel")
        cboTypeKhoroojiPipe.Items.Add("Ductile")
        cboTypeKhoroojiPipe.Items.Add("Plyka")
        cboTypeKhoroojiPipe.Items.Add("PE")

    End Sub

    Private Sub btnApply_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply.Click

        If TabControl_Features.SelectedTabIndex = 0 Then
            Initialize_MainPipeFeatures()
        ElseIf TabControl_Features.SelectedTabIndex = 1 Then
            ' Initialize_UserPipeFeatures()
        ElseIf TabControl_Features.SelectedTabIndex = 2 Then
            Initialize_ValvesFeatures()
        ElseIf TabControl_Features.SelectedTabIndex = 3 Then
            Initialize_UsersFeatures()
        ElseIf TabControl_Features.SelectedTabIndex = 4 Then
            Initialize_JunctionFeatures()
        ElseIf TabControl_Features.SelectedTabIndex = 5 Then
            Initialize_SourceFeatures()
        End If
    End Sub

    Private Sub TabControl_Features_SelectedTabChanged(ByVal sender As Object, ByVal e As DevComponents.DotNetBar.SuperTabStripSelectedTabChangedEventArgs) Handles TabControl_Features.SelectedTabChanged
        Me.Height = 228
        BtnChangeSizeFormValves.Text = "بیشتر>>"
        BtnChangeSizeFormUsers.Text = "بیشتر>>"
        b_ChageSizeFormValves = False
        b_ChageSizeFormUsers = False
        If TabControl_Features.SelectedTabIndex = 0 Then
            txtIDPipe.Text = ""
            txtDiameter.Text = ""
            txtHaizenC.Text = ""
            CboMaterial.SelectedIndex = -1
        ElseIf TabControl_Features.SelectedTabIndex = 1 Then
            txtIDUserPipes.Text = ""
        ElseIf TabControl_Features.SelectedTabIndex = 2 Then
            txtIDValve.Text = ""
            txtCodeShir.Text = ""
            cboDiaDakheliShir.SelectedIndex = -1
            txtDiameterVoroodiPipe.Text = ""
            txtDiameterKhoroojiPipe.Text = ""
            cboTypeVoroodiPipe.SelectedIndex = -1
            cboTypeKhoroojiPipe.SelectedIndex = -1
        ElseIf TabControl_Features.SelectedTabIndex = 3 Then
            txtIDUser.Text = ""
            txtNameUser.Text = ""
            txtPhoneNumber.Text = ""
            txtEshterakNumber.Text = ""
            txtCodeMeli.Text = ""
            txtMasraf.Text = ""
            CboKarbary.SelectedIndex = -1
            txtMantaghe.Text = ""
            txtKhyaban.Text = ""
            txtKooche.Text = ""
            txtZipeCode.Text = ""
            txtPelak.Text = ""
            txtTedadeTabaghat.Text = ""
            txtDescription.Text = ""
            Me.Height = 358
        ElseIf TabControl_Features.SelectedTabIndex = 4 Then
            txtIDJunction.Text = ""
            txtJunctionElevation.Text = ""
            txtJunctionMasraf.Text = ""
        ElseIf TabControl_Features.SelectedTabIndex = 5 Then
            txtIDSource.Text = ""
            txtSourceElevation.Text = ""
        End If
    End Sub

    Private Sub Flash_Features(ByVal Feature_TreeNode As IFeature)
        ' Dim pFeatureIdentify As IFeatureIdentifyObj = New FeatureIdentifyObject
        '  pFeatureIdentify.Feature = Feature_TreeNode
        '  Dim pIdentifyObj As IIdentifyObj = pFeatureIdentify
        ' pIdentifyObj.Flash(ActiveView.ScreenDisplay)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub Clear_ClassExtensions()
        IClassSchemaEdit_ClassExtension(m_FCMainPipe, "")
        IClassSchemaEdit_ClassExtension(m_FCJunction, "")
        IClassSchemaEdit_ClassExtension(m_FCSource, "")
        IClassSchemaEdit_ClassExtension(m_FCUserPipe, "")
        IClassSchemaEdit_ClassExtension(m_FCUsers, "")
        IClassSchemaEdit_ClassExtension(m_FCValve, "")
    End Sub

    Public Sub IClassSchemaEdit_ClassExtension(ByVal featClass As IObjectClass, ByVal classUIDStr As String)
        'This function shows how you can use the IClassSchemaEdit   
        'interface to alter the COM class extension for an object class.    
        'cast for the IClassSchemaEdit      
        Dim classSchemaEdit As IClassSchemaEdit = CType(featClass, IClassSchemaEdit)
        'set and exclusive lock on the class     
        Try
            Dim schemaLock As ISchemaLock = CType(featClass, ISchemaLock)
            schemaLock.ChangeSchemaLock(esriSchemaLock.esriExclusiveSchemaLock)
            Dim classUID As ESRI.ArcGIS.esriSystem.UID = New ESRI.ArcGIS.esriSystem.UIDClass()

            If Not classUIDStr.Equals("") Then
                classUID.Value = classUIDStr
                classSchemaEdit.AlterClassExtensionCLSID(classUID, Nothing)
            Else
                classSchemaEdit.AlterClassExtensionCLSID(Nothing, Nothing)
            End If

            'release the exclusive lock     
            schemaLock.ChangeSchemaLock(esriSchemaLock.esriSharedSchemaLock)
        Catch ex As Exception
            Show_AlertCustom("Cannot acquire a schema lock beacause of an existing lock." & vbCrLf & "Try again when GeoDatabase is available.")
        End Try

    End Sub

#Region "MainPipes"

    Private Sub TreeView_MainPipes_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView_MainPipes.NodeMouseClick
        On Error GoTo Err

        Dim Feature As IFeature
        Dim Value As Object

        If e.Node Is Nothing Then Exit Sub
        SelectedNode = e.Node
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If TypeOf e.Node.Tag Is IFeature Then
                Feature = TryCast(e.Node.Tag, IFeature)
                If Feature Is Nothing Then Exit Sub

                Flash_Features(Feature)
                txtIDPipe.Text = Feature.OID
                Value = Feature.Value(m_IndexMainPipeDaimeter)

                If Not IsDBNull(Value) Then
                    txtDiameter.Text = Value
                Else
                    txtDiameter.Text = ""
                End If

                Value = Feature.Value(m_IndexMainPipeCHaizen)
                If Not IsDBNull(Value) Then
                    txtHaizenC.Text = Value
                Else
                    txtHaizenC.Text = ""
                End If
                Value = Feature.Value(m_IndexMainPipeMaterial)
                If Not IsDBNull(Value) Then
                    CboMaterial.SelectedIndex = Value - 1
                Else
                    CboMaterial.SelectedIndex = -1
                End If

            End If

        End If
        Exit Sub
Err:
        Show_AlertCustom("Failure in show result")
    End Sub

    Public Sub Initialize_MainPipeFeatures()
        On Error GoTo Err


        Dim NewMainPipeFeature As IFeature
        Dim OID As Integer
        Dim Hazen As Double
        Dim Diameter As Double

        ResultMessage = ""
        OID = CInt(txtIDPipe.Text)
        If Not IsNumeric(OID) Then Exit Sub

        NewMainPipeFeature = m_FCMainPipe.GetFeature(OID)
        If NewMainPipeFeature Is Nothing Then Exit Sub

        WorkspaceEdit.StartEditing(True)
        WorkspaceEdit.StartEditOperation()

        NewMainPipeFeature.Value(m_IndexMainPipeMaterial) = CboMaterial.SelectedIndex + 1
        If Double.TryParse(txtDiameter.Text, Diameter) = 0.0 Then
            Err.Number = 1
            GoTo Err
        End If
        NewMainPipeFeature.Value(m_IndexMainPipeDaimeter) = Diameter
        If Double.TryParse(txtHaizenC.Text, Hazen) = 0.0 Then
            Err.Number = 2
            GoTo Err
        End If
        NewMainPipeFeature.Value(m_IndexMainPipeCHaizen) = Hazen
        NewMainPipeFeature.Store()

        WorkspaceEdit.StopEditOperation()
        WorkspaceEdit.StopEditing(True)
        SelectedNode.Tag = NewMainPipeFeature
        Exit Sub
Err:
        Select Case Err.Number
            Case 1
                ResultMessage = "Please, Enter rigth value for Diameter"
            Case 2
                ResultMessage = "Please, Enter rigth value for Hazen-C"
        End Select
        If ResultMessage = "" Then
            ResultMessage = "Failure in initialize"
        End If
        Show_AlertCustom(ResultMessage)
    End Sub
#End Region

#Region "Users"

    Private Function FindFieldUsers(ByVal NameField As String) As Integer
        On Error GoTo ErrorHandler

        Dim FClassUser As IFeatureClass
        Dim Fields As IFields
        Dim Field As IField

        Fields = m_FCUsers.Fields
        For i As Integer = 0 To Fields.FieldCount - 1
            Field = Fields.Field(i)
            If Field.AliasName = NameField Then
                FindFieldUsers = i
                Exit For
            End If
        Next

        Exit Function
ErrorHandler:
        FindFieldUsers = -1
    End Function

    Private Sub TreeView_Users_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView_Users.NodeMouseClick
        On Error GoTo Err

        Dim Feature As IFeature
        Dim Value As Object

        If e.Node Is Nothing Then Exit Sub
        SelectedNode = e.Node
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If TypeOf e.Node.Tag Is IFeature Then
                Feature = TryCast(e.Node.Tag, IFeature)
                If Feature Is Nothing Then Exit Sub

                Flash_Features(Feature)
                txtIDUser.Text = Feature.OID
                Value = Feature.Value(m_FCUsers.FindField("CodeMely"))
                If Not IsDBNull(Value) Then
                    txtCodeMeli.Text = Value
                Else
                    txtCodeMeli.Text = ""
                End If

                Value = Feature.Value(m_FCUsers.FindField("NumberEshterak"))
                If Not IsDBNull(Value) Then
                    txtEshterakNumber.Text = Value
                Else
                    txtEshterakNumber.Text = ""
                End If
                Value = Feature.Value(m_FCUsers.FindField("Khiyaban"))
                If Not IsDBNull(Value) Then
                    txtKhyaban.Text = Value
                Else
                    txtKhyaban.Text = ""
                End If
                Value = Feature.Value(m_FCUsers.FindField("Koche"))
                If Not IsDBNull(Value) Then
                    txtKooche.Text = Value
                Else
                    txtKooche.Text = ""
                End If
                Value = Feature.Value(m_FCUsers.FindField("Mantaghe"))
                If Not IsDBNull(Value) Then
                    txtMantaghe.Text = Value
                Else
                    txtMantaghe.Text = ""
                End If
                Value = Feature.Value(m_FCUsers.FindField("NameUser"))
                If Not IsDBNull(Value) Then
                    txtNameUser.Text = Value
                Else
                    txtNameUser.Text = ""
                End If
                Value = Feature.Value(m_FCUsers.FindField("Pelak"))
                If Not IsDBNull(Value) Then
                    txtPelak.Text = Value
                Else
                    txtPelak.Text = ""
                End If
                Value = Feature.Value(m_FCUsers.FindField("NumberPhoneUser"))
                If Not IsDBNull(Value) Then
                    txtPhoneNumber.Text = Value
                Else
                    txtPhoneNumber.Text = ""
                End If
                Value = Feature.Value(m_FCUsers.FindField("TedadTabaghat"))
                If Not IsDBNull(Value) Then
                    txtTedadeTabaghat.Text = Value
                Else
                    txtTedadeTabaghat.Text = ""
                End If
                Value = Feature.Value(m_FCUsers.FindField("ZipeCode"))
                If Not IsDBNull(Value) Then
                    txtZipeCode.Text = Value
                Else
                    txtZipeCode.Text = ""
                End If
                Value = Feature.Value(m_FCUsers.FindField("Masraf"))
                If Not IsDBNull(Value) Then
                    txtMasraf.Text = Value
                Else
                    txtMasraf.Text = ""
                End If
                Value = Feature.Value(m_FCUsers.FindField("Descripetion"))
                If Not IsDBNull(Value) Then
                    txtDescription.Text = Value
                Else
                    txtDescription.Text = ""
                End If
                '  Value = Feature.Value(m_FCUsers.FindField("Karbary"))
                Value = Feature.Value(FindFieldUsers("Land Use"))
                If Not IsDBNull(Value) Then
                    CboKarbary.SelectedIndex = Value - 1
                Else
                    CboKarbary.SelectedIndex = -1
                End If
                ' Value = Feature.Value(m_FCUsers.FindField("DaricheHozcheKontor"))

            End If
        End If
        Exit Sub
Err:
        Show_AlertCustom("Failure in show result")
    End Sub

    Public Sub Initialize_UsersFeatures()
        On Error GoTo Err


        Dim NewUserFeature As IFeature
        Dim OID As Integer

        ResultMessage = ""
        OID = CInt(txtIDUser.Text)
        If Not IsNumeric(OID) Then Exit Sub

        NewUserFeature = m_FCUsers.GetFeature(OID)
        If NewUserFeature Is Nothing Then Exit Sub

        WorkspaceEdit.StartEditing(True)
        WorkspaceEdit.StartEditOperation()

        NewUserFeature.Value(m_FCUsers.FindField("CodeMely")) = txtCodeMeli.Text
        NewUserFeature.Value(m_FCUsers.FindField("NumberEshterak")) = txtEshterakNumber.Text
        NewUserFeature.Value(m_FCUsers.FindField("Khiyaban")) = txtKhyaban.Text
        NewUserFeature.Value(m_FCUsers.FindField("Koche")) = txtKooche.Text
        NewUserFeature.Value(m_FCUsers.FindField("Mantaghe")) = txtMantaghe.Text
        NewUserFeature.Value(m_FCUsers.FindField("NameUser")) = txtNameUser.Text
        NewUserFeature.Value(m_FCUsers.FindField("Pelak")) = txtPelak.Text
        NewUserFeature.Value(m_FCUsers.FindField("NumberPhoneUser")) = txtPhoneNumber.Text
        NewUserFeature.Value(m_FCUsers.FindField("TedadTabaghat")) = txtTedadeTabaghat.Text
        NewUserFeature.Value(m_FCUsers.FindField("ZipeCode")) = txtZipeCode.Text
        NewUserFeature.Value(m_FCUsers.FindField("Masraf")) = txtMasraf.Text
        NewUserFeature.Value(m_FCUsers.FindField("Descripetion")) = txtDescription.Text

        '  NewUserFeature.Value(m_FCUsers.FindField("Karbary")) = CboKarbary.SelectedIndex + 1
        '  NewUserFeature.Value(m_FCUsers.FindField("DaricheHozcheKontor")) = CboDaricheHozcheKontor.SelectedIndex
        '  NewUserFeature.Value(m_FCUsers.FindField("HozcheKontor")) = CboHozcheKontor.SelectedIndex
        ' NewUserFeature.Value(m_FCUsers.FindField("MahaleNasbeKontor")) = CboMahaleNasbeKontor.SelectedIndex
        ' NewUserFeature.Value(m_FCUsers.FindField("MaterialUserPipe")) = CboMaterialPipeUsers.SelectedIndex
        ' NewUserFeature.Value(m_FCUsers.FindField("ModelOfKontor")) = CboModelOfKontor.SelectedIndex
        ' NewUserFeature.Value(m_FCUsers.FindField("MoghiyateNasbKontor")) = CboMogheyiateKontor.SelectedIndex
        ' NewUserFeature.Value(m_FCUsers.FindField("ShireGhatvaVasl")) = CboShireGhatvaVasl.SelectedIndex
        ' NewUserFeature.Value(m_FCUsers.FindField("ShireGhatVaVasleGhableazKontor")) = CboShireGhatVaVasleGhableazKontor.SelectedIndex

        ' NewUserFeature.Value(m_FCUsers.FindField("ShireGhatVaVasleBadazKontor")) = CboShireGhatVaVasleBadazKontor.SelectedIndex
        ' NewUserFeature.Value(m_FCUsers.FindField("typeEnsheab")) = CbotypeEnsheabUser.SelectedIndex
        ' NewUserFeature.Value(m_FCUsers.FindField("VaziyateZaheriKontor")) = CboVaziyateZaheriKontor.SelectedIndex
        ' NewUserFeature.Value(m_FCUsers.FindField("VazyiteSakhteHozche")) = CboVazyiteSakhteHozche.SelectedIndex

        NewUserFeature.Value(FindFieldUsers("Street")) = CboKarbary.SelectedIndex + 1
        NewUserFeature.Store()

        WorkspaceEdit.StopEditOperation()
        WorkspaceEdit.StopEditing(True)
        SelectedNode.Tag = NewUserFeature
        Exit Sub
Err:
        If ResultMessage = "" Then
            ResultMessage = "Failure in initialize"
        End If
        Show_AlertCustom(ResultMessage)
    End Sub
#End Region

#Region "Valves"

    Private Sub TreeView_Valves_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView_Valves.NodeMouseClick
        On Error GoTo Err

        Dim Feature As IFeature
        Dim Value As Object

        If e.Node Is Nothing Then Exit Sub
        SelectedNode = e.Node
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If TypeOf e.Node.Tag Is IFeature Then
                Feature = TryCast(e.Node.Tag, IFeature)
                If Feature Is Nothing Then Exit Sub

                Flash_Features(Feature)

                txtIDValve.Text = Feature.OID
                Value = CStr(Feature.Value(FindFieldValves("Code Valve")))
                If Not IsDBNull(Value) Then
                    txtCodeShir.Text = Value
                Else
                    txtCodeShir.Text = ""
                End If

                Value = CStr(Feature.Value(FindFieldValves("قطر لوله خروجی")))
                If Not IsDBNull(Value) Then
                    txtDiameterKhoroojiPipe.Text = Value
                Else
                    txtDiameterKhoroojiPipe.Text = ""
                End If

                Value = Feature.Value(FindFieldValves("قطر لوله ورودی"))
                If Not IsDBNull(Value) Then
                    txtDiameterVoroodiPipe.Text = Value
                Else
                    txtDiameterVoroodiPipe.Text = ""
                End If

                Value = Feature.Value(FindFieldValves("Internal Diameter Valve")) - 1
                If Not IsDBNull(Value) Then
                    cboDiaDakheliShir.SelectedIndex = Value
                Else
                    cboDiaDakheliShir.SelectedIndex = ""
                End If

                Value = Feature.Value(FindFieldValves("جنس لوله خروجی"))
                If Not IsDBNull(Value) Then
                    cboTypeKhoroojiPipe.SelectedIndex = Value
                Else
                    cboTypeKhoroojiPipe.SelectedIndex = ""
                End If

                Value = Feature.Value(FindFieldValves("جنس لوله ورودی"))
                If Not IsDBNull(Value) Then
                    cboTypeVoroodiPipe.SelectedIndex = Value
                Else
                    cboTypeVoroodiPipe.SelectedIndex = ""
                End If

            End If
        End If
        Exit Sub
Err:
        Show_AlertCustom("Failure in show result")
    End Sub

    Public Sub Initialize_ValvesFeatures()
        On Error GoTo Err


        Dim NewValveFeature As IFeature
        Dim OID As Integer

        ResultMessage = ""
        OID = CInt(txtIDValve.Text)
        If Not IsNumeric(OID) Then Exit Sub

        NewValveFeature = m_FCValve.GetFeature(OID)
        If NewValveFeature Is Nothing Then Exit Sub

        WorkspaceEdit.StartEditing(True)
        WorkspaceEdit.StartEditOperation()

        NewValveFeature.Value(FindFieldValves("Code Valve")) = txtCodeShir.Text
        NewValveFeature.Value(FindFieldValves("قطر لوله خروجی")) = txtDiameterKhoroojiPipe.Text
        NewValveFeature.Value(FindFieldValves("قطر لوله ورودی")) = txtDiameterVoroodiPipe.Text

        NewValveFeature.Value(FindFieldValves("Internal Diameter Valve")) = cboDiaDakheliShir.SelectedIndex
        NewValveFeature.Value(FindFieldValves("جنس لوله خروجی")) = cboTypeKhoroojiPipe.SelectedIndex
        NewValveFeature.Value(FindFieldValves("جنس لوله ورودی")) = cboTypeVoroodiPipe.SelectedIndex
        NewValveFeature.Store()

        WorkspaceEdit.StopEditOperation()
        WorkspaceEdit.StopEditing(True)
        SelectedNode.Tag = NewValveFeature
        Exit Sub
Err:
        If ResultMessage = "" Then
            ResultMessage = "Failure in initialize"
        End If
        Show_AlertCustom(ResultMessage)
    End Sub

    Private Function FindFieldValves(ByVal NameField As String) As Integer
        On Error GoTo ErrorHandler

        Dim FClassUser As IFeatureClass
        Dim Fields As IFields
        Dim Field As IField

        Fields = m_FCValve.Fields
        For i As Integer = 0 To Fields.FieldCount - 1
            Field = Fields.Field(i)
            If Field.AliasName = NameField Then
                FindFieldValves = i
                Exit For
            End If
        Next

        Exit Function
ErrorHandler:
        FindFieldValves = -1
    End Function
#End Region

#Region "UsersPipe"

    Private Sub TreeView_UserPipes_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView_UserPipes.NodeMouseClick
        On Error GoTo Err

        Dim Feature As IFeature
        Dim Value As Object

        If e.Node Is Nothing Then Exit Sub
        SelectedNode = e.Node
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If TypeOf e.Node.Tag Is IFeature Then
                Feature = TryCast(e.Node.Tag, IFeature)
                If Feature Is Nothing Then Exit Sub

                Flash_Features(Feature)

                txtIDUserPipes.Text = Feature.OID

            End If
        End If
        Exit Sub
Err:
        Show_AlertCustom("Failure in show result")
    End Sub

#End Region

#Region "Sources"

    Private Sub TreeView_Sources_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView_Sources.NodeMouseClick
        On Error GoTo Err

        Dim Feature As IFeature
        Dim Value As Object

        If e.Node Is Nothing Then Exit Sub
        SelectedNode = e.Node
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If TypeOf e.Node.Tag Is IFeature Then
                Feature = TryCast(e.Node.Tag, IFeature)
                If Feature Is Nothing Then Exit Sub

                Flash_Features(Feature)

                txtIDSource.Text = Feature.OID
                Value = Feature.Value(m_IndexSourceElevation)

                If Not IsDBNull(Value) Then
                    txtSourceElevation.Text = Value
                Else
                    txtSourceElevation.Text = ""
                End If
            End If
        End If
        Exit Sub
Err:
        Show_AlertCustom("Failure in show result")
    End Sub

    Public Sub Initialize_SourceFeatures()
        On Error GoTo Err


        Dim NewSourceFeature As IFeature
        Dim OID As Integer
        Dim Elevation As Double

        ResultMessage = ""
        OID = CInt(txtIDSource.Text)
        If Not IsNumeric(OID) Then Exit Sub

        NewSourceFeature = m_FCSource.GetFeature(OID)
        If NewSourceFeature Is Nothing Then Exit Sub

        WorkspaceEdit.StartEditing(True)
        WorkspaceEdit.StartEditOperation()
        If Double.TryParse(txtSourceElevation.Text, Elevation) = 0.0 Then
            ResultMessage = "Please, Enter a rigth value for elevation field"
            GoTo Err
        End If
        NewSourceFeature.Value(m_IndexSourceElevation) = Elevation
        NewSourceFeature.Store()

        WorkspaceEdit.StopEditOperation()
        WorkspaceEdit.StopEditing(True)
        SelectedNode.Tag = NewSourceFeature
        Exit Sub
Err:
        If ResultMessage = "" Then
            ResultMessage = "Failure in initialize"
        End If
        Show_AlertCustom(ResultMessage)
    End Sub
#End Region

#Region "Junctions"

    Private Sub TreeView_Junctions_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView_Junctions.NodeMouseClick
        On Error GoTo Err

        Dim Feature As IFeature
        Dim Value As Object

        If e.Node Is Nothing Then Exit Sub
        SelectedNode = e.Node
        If e.Button = Windows.Forms.MouseButtons.Left Then
            If TypeOf e.Node.Tag Is IFeature Then
                Feature = TryCast(e.Node.Tag, IFeature)
                If Feature Is Nothing Then Exit Sub

                Flash_Features(Feature)

                txtIDJunction.Text = Feature.OID
                Value = Feature.Value(m_IndexJunctionElevation)
                If Not IsDBNull(Value) Then
                    txtJunctionElevation.Text = Value
                Else
                    txtJunctionElevation.Text = ""
                End If

                Value = Feature.Value(m_IndexJunctionCollMasraf)
                If Not IsDBNull(Value) Then
                    txtJunctionMasraf.Text = Value
                Else
                    txtJunctionMasraf.Text = ""
                End If
            End If
        End If
        Exit Sub
Err:
        Show_AlertCustom("Failure in show result")
    End Sub

    Public Sub Initialize_JunctionFeatures()
        On Error GoTo Err


        Dim NewJunctionFeature As IFeature
        Dim OID As Integer
        Dim Elevation As Double
        Dim Masraf As Double

        ResultMessage = ""
        OID = CInt(txtIDJunction.Text)
        If Not IsNumeric(OID) Then Exit Sub

        NewJunctionFeature = m_FCJunction.GetFeature(OID)
        If NewJunctionFeature Is Nothing Then Exit Sub

        WorkspaceEdit.StartEditing(True)
        WorkspaceEdit.StartEditOperation()

        If Double.TryParse(txtJunctionElevation.Text, Elevation) = 0.0 Then
            ResultMessage = "Please, Enter a rigth value for elevation field"
            GoTo Err
        End If
        If Double.TryParse(txtJunctionMasraf.Text, Masraf) = 0.0 Then
            ResultMessage = "Please, Enter a rigth value for consumption field"
            GoTo Err
        End If
        NewJunctionFeature.Value(m_IndexJunctionElevation) = Elevation
        NewJunctionFeature.Value(m_IndexJunctionCollMasraf) = Masraf
        NewJunctionFeature.Store()

        WorkspaceEdit.StopEditOperation()
        WorkspaceEdit.StopEditing(True)
        SelectedNode.Tag = NewJunctionFeature
        Exit Sub
Err:
        If ResultMessage = "" Then
            ResultMessage = "Failure in initialize"
        End If
        Show_AlertCustom(ResultMessage)
    End Sub

#End Region





End Class
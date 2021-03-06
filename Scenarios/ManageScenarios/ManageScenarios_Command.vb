Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Controls
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geodatabase

<ComClass(ManageScenarios_Command.ClassId, ManageScenarios_Command.InterfaceId, ManageScenarios_Command.EventsId), _
 ProgId("WaterEngine_AnalysisNetwork.ManageScenarios_Command")> _
Public NotInheritable Class ManageScenarios_Command
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "c1241709-ffc8-4216-9181-01454a528aa8"
    Public Const InterfaceId As String = "9dbf2e47-34b2-4880-8eec-1ff7876b4cfa"
    Public Const EventsId As String = "f486cf9e-6941-4fb5-b92c-38e4f7b0320a"
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
    Private TreeView_MainForm As TreeView
    Private cboFirstScenario_MainForm, cboSecondScenario_MainForm As ComboBox
    Private ScenarioName_MainForm As String
    Private m_Map As IMap
    Private m_Editor As IEngineEditor
    Private WithEvents FrmChangeScenario As ChangesScenarios_Form
    Private BaseScenarioNode As New TreeNode
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
        'TODO: Add other initialization code
    End Sub

    Public WriteOnly Property Get_ScenarioName() As String
        Set(ByVal value As String)
            ScenarioName_MainForm = value
        End Set
    End Property

    Public WriteOnly Property Get_TreeView_MainForm() As TreeView
        Set(ByVal value As TreeView)
            TreeView_MainForm = value
        End Set
    End Property

    Public WriteOnly Property Get_cboFirstScenario_MainForm_MainForm() As ComboBox
        Set(ByVal value As ComboBox)
            cboFirstScenario_MainForm = value
        End Set
    End Property

    Public WriteOnly Property Get_cboSecondScenario_MainForm_MainForm() As ComboBox
        Set(ByVal value As ComboBox)
            cboSecondScenario_MainForm = value
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
        On Error GoTo Err
        Dim FCPipe As IFeatureClass

        If m_hookHelper Is Nothing Then Exit Sub
        m_Map = TryCast(m_hookHelper.FocusMap, IMap)
        If m_Map Is Nothing Then Exit Sub
        ResultMessage = ""
        If TreeView_MainForm Is Nothing OrElse cboFirstScenario_MainForm Is Nothing OrElse cboSecondScenario_MainForm Is Nothing Then Exit Sub
        TreeView_MainForm.Nodes.Clear()

        FCPipe = m_FCMainPipe
        If FCPipe Is Nothing OrElse m_TableScenarios Is Nothing Then
            ResultMessage = "Please complete the basic configuration of software"
              GoTo Err 
        End If

        BaseScenarioNode.Text = "Base Scenario"
        TreeView_MainForm.RightToLeftLayout = True
        Initialize_TreeView()
        TreeView_MainForm.Nodes.Add(BaseScenarioNode)

        TreeView_MainForm.ExpandAll()
        Initialize_CboScenarios()
        Exit Sub

Err:
        If ResultMessage = "" Then
            ResultMessage = "Failure in load Scenarios Form"
        End If
        Show_AlertCustom(ResultMessage)
    End Sub

    Private Sub Initialize_CboScenarios()
        On Error GoTo Err

        Dim Row As IRow
        Dim RowCount As Integer
        Dim Cursor As ICursor


        RowCount = m_TableScenarios.RowCount(Nothing)
        Cursor = m_TableScenarios.Search(Nothing, False)

        cboFirstScenario_MainForm.Items.Clear()
        cboSecondScenario_MainForm.Items.Clear()
        cboFirstScenario_MainForm.Items.Add("Base Scenario")
        cboSecondScenario_MainForm.Items.Add("Base Scenario")

        For i = 0 To RowCount - 1
            Row = Cursor.NextRow
            cboFirstScenario_MainForm.Items.Add(Row.Value(1))
            cboSecondScenario_MainForm.Items.Add(Row.Value(1))
        Next

        cboFirstScenario_MainForm.SelectedIndex = 0
        cboSecondScenario_MainForm.SelectedIndex = 0
        Marshal.ReleaseComObject(Cursor)
        Exit Sub
Err:
        ResultMessage = "Failure in show Scenarios Form"
    End Sub

    Private Sub Initialize_TreeView()
        On Error GoTo Err

        Dim Row As IRow
        Dim Cursor As ICursor

        Cursor = m_TableScenarios.Search(Nothing, False)
        Row = Cursor.NextRow
        While Row IsNot Nothing
            If Row.HasOID Then
                Dim ScenarioNode As New TreeNode
                ScenarioNode.Text = Row.Value(1)
                BaseScenarioNode.Nodes.Add(ScenarioNode)
            End If
            Row = Cursor.NextRow
        End While

        Marshal.ReleaseComObject(Cursor)
        Exit Sub
Err:
        ResultMessage = "Failure in show Scenarios Form"
    End Sub

    Public Sub InsertChanges_Click()
        On Error GoTo Err

        Dim ScenarioNode As TreeNode
        Dim Row As IRow
        Dim TypeScenario As String
        Dim Win32 As IWin32Window

        ScenarioNode = TreeView_MainForm.SelectedNode
        ResultMessage = ""
        If ScenarioNode Is Nothing Then
            ResultMessage = "Please First choose one of the scenarios"
            GoTo Err
        End If
        If ScenarioNode.Text = "Base Scenario" Then
            ResultMessage = "You cannot change Base Scenario"
            GoTo Err
        End If

        If FrmChangeScenario Is Nothing Then
            FrmChangeScenario = New ChangesScenarios_Form(m_hookHelper)
        End If
        FrmChangeScenario.Get_ScenarioName = ScenarioNode.Text

        Row = Find_IDScenario(ScenarioNode.Text)
        If Row Is Nothing Then
            ResultMessage = "Failure in finding this scenario"
            GoTo Err
        End If
        TypeScenario = Row.Value(m_TableScenarios.FindField("TypeScenario"))
        FrmChangeScenario.Show(Win32)
        Exit Sub
Err:
        If ResultMessage = vbNullString Then
            ResultMessage = "Failure in show Scenarios Form"
        End If
        Show_AlertCustom(ResultMessage)
    End Sub


    Private Function Find_IDScenario(ByVal NameScenario As String) As IRow
        Dim Row As IRow
        Dim Cursor As ICursor
        Dim RowCount As Integer
        Dim IndexNameScenario As Integer

        Cursor = m_TableScenarios.Search(Nothing, False)
        RowCount = m_TableScenarios.RowCount(Nothing)
        IndexNameScenario = m_TableScenarios.FindField("NameScenario")

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

    Private Sub FrmChangeScenario_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles FrmChangeScenario.FormClosing
        FrmChangeScenario = Nothing
    End Sub

    Public Sub ToolstripNewScenario_Click()
        On Error GoTo Err

        ResultMessage = ""
        If ScenarioName_MainForm = "" Then
            ResultMessage = "Please, Enter a name for new scenario"
            GoTo Err
        End If

        If SearchTableScenarios_ForNewScenario(ScenarioName_MainForm) Then
            ResultMessage = "Please, Enter another name, There is a scenario same name"
            GoTo Err
        End If

        Dim ScenarioNode As New TreeNode

        ScenarioNode.Text = ScenarioName_MainForm

        Fill_TableScenarios(ScenarioName_MainForm, "Physically")
        BaseScenarioNode.Nodes.Add(ScenarioNode)
        TreeView_MainForm.ExpandAll()

        cboFirstScenario_MainForm.Items.Add(ScenarioName_MainForm)
        cboSecondScenario_MainForm.Items.Add(ScenarioName_MainForm)
        If ResultMessage = "" Then
            ResultMessage = "Operation was successful"
            Show_AlertCustom(ResultMessage)
        End If
        Exit Sub
Err:
        If ResultMessage = vbNullString Then
            ResultMessage = "Failure in register new Scenario"
        End If
        Show_AlertCustom(ResultMessage)
    End Sub

    Private Sub Fill_TableScenarios(ByVal NameScenario As String, ByVal TypeScenario As String)
        On Error GoTo ErrMessage

        Dim WorkspaceEdit As IWorkspaceEdit
        Dim Row As IRow
        Dim IndexNameScenario, IndexTypeScenario, IndexDateScenario As Integer


        WorkspaceEdit = m_Workspace
        WorkspaceEdit.StartEditing(True)
        WorkspaceEdit.StartEditOperation()

        IndexNameScenario = m_TableScenarios.FindField("NameScenario")
        IndexTypeScenario = m_TableScenarios.FindField("TypeScenario")
        IndexDateScenario = m_TableScenarios.FindField("DateScenario")

        Row = m_TableScenarios.CreateRow
        Row.Value(IndexNameScenario) = NameScenario
        Row.Value(IndexTypeScenario) = TypeScenario
        Row.Value(IndexDateScenario) = Date.Today
        Row.Store()

        WorkspaceEdit.StopEditOperation()
        WorkspaceEdit.StopEditing(True)

        Exit Sub
ErrMessage:
        If WorkspaceEdit.IsBeingEdited Then
            WorkspaceEdit.AbortEditOperation()
        End If
        ResultMessage = "Failure in register new Scenario"
    End Sub

    Private Function SearchTableScenarios_ForNewScenario(ByVal NameScenario As String) As Boolean
        Dim Row As IRow
        Dim Cursor As ICursor
        Dim IndexNameScenario As Integer

        SearchTableScenarios_ForNewScenario = False
        IndexNameScenario = m_TableScenarios.FindField("NameScenario")
        Cursor = m_TableScenarios.Search(Nothing, False)
        Row = Cursor.NextRow
        While Row IsNot Nothing
            If Row.Value(IndexNameScenario) = NameScenario Then
                SearchTableScenarios_ForNewScenario = True
                Exit While
            End If
            Row = Cursor.NextRow
        End While


    End Function

    Public Sub ToolstripDeleteScenario_Click()
        On Error GoTo Err

        Dim ScenarioNode As TreeNode
        Dim IDScenario As Integer
        Dim Row As IRow


        ScenarioNode = TreeView_MainForm.SelectedNode
        ResultMessage = ""
        If ScenarioNode Is Nothing Then
            ResultMessage = "به منظور حذف سناریو باید ابتدا یکی از سناریوها را انتخاب نمایید"
            GoTo Err
        End If

        If ScenarioNode.Text = "Base Scenario" Then
            ResultMessage = "Base Scenario را نمی توان حذف کرد"
            GoTo Err
        End If

        Row = Find_IDScenario(TreeView_MainForm.SelectedNode.Text)
        If Row Is Nothing Then
            ResultMessage = "اشکال در یافتن اطلاعات مربوط به سناریو مورد نظر"
            GoTo Err
        End If

        IDScenario = Row.Value(m_TableScenarios.FindField(m_TableScenarios.OIDFieldName))

        Delete_TableScenarios(ScenarioNode.Text)
        TreeView_MainForm.Nodes.Remove(ScenarioNode)

        If IDScenario = -1 Then
            ResultMessage = "اشکال در یافتن اطلاعات مربوط به سناریو مورد نظر"
            GoTo Err
        End If

        Delete_TableIntermediateScenario(IDScenario)
        If ResultMessage = "" Then
            ResultMessage = "Operation was successful"
            Show_AlertCustom(ResultMessage)
        End If
        Exit Sub
Err:
        If ResultMessage = vbNullString Then
            ResultMessage = "Failure in delete Scenario"
        End If
        Show_AlertCustom(ResultMessage)
    End Sub

    Private Sub Delete_TableIntermediateScenario(ByVal IDScenario As Integer)
        On Error GoTo ErrMessage

        Dim WorkspaceEdit As IWorkspaceEdit
        Dim Row As IRow
        Dim Cursor As ICursor

        Dim IndexIDScenario As Integer

        IndexIDScenario = m_TableIntermediateScenario.FindField("NumScenario")

        WorkspaceEdit = m_Workspace
        WorkspaceEdit.StartEditing(True)
        WorkspaceEdit.StartEditOperation()

        Cursor = m_TableIntermediateScenario.Search(Nothing, False)
        Row = Cursor.NextRow
        While Row IsNot Nothing
            If Row.Value(IndexIDScenario) = IDScenario Then
                Row.Delete()
                Exit While
            End If
            Row = Cursor.NextRow
        End While

        WorkspaceEdit.StopEditOperation()
        WorkspaceEdit.StopEditing(True)
        Marshal.ReleaseComObject(Cursor)

        Exit Sub
ErrMessage:
        WorkspaceEdit.AbortEditOperation()
        ResultMessage = "Failure in delete Scenario"
    End Sub

    Private Sub Delete_TableScenarios(ByVal NameScenario As String)
        On Error GoTo ErrMessage

        Dim WorkspaceEdit As IWorkspaceEdit
        Dim Row As IRow
        Dim Dataset As IDataset
        Dim Cursor As ICursor

        Dim IndexNameScenario As Integer

        IndexNameScenario = m_TableScenarios.FindField("NameScenario")

        Dataset = CType(m_TableScenarios, IDataset)
        WorkspaceEdit = CType(Dataset.Workspace, IWorkspaceEdit)
        WorkspaceEdit.StartEditing(True)
        WorkspaceEdit.StartEditOperation()

        Cursor = m_TableScenarios.Search(Nothing, False)
        Row = Cursor.NextRow
        While Row IsNot Nothing
            If Row.Value(IndexNameScenario) = NameScenario Then
                Row.Delete()
                Exit While
            End If
            Row = Cursor.NextRow
        End While

        WorkspaceEdit.StopEditOperation()
        WorkspaceEdit.StopEditing(True)
        Marshal.ReleaseComObject(Cursor)

        Exit Sub
ErrMessage:
        WorkspaceEdit.AbortEditOperation()
        ResultMessage = "Failure in delete Scenario"
    End Sub

    Public Function BtnCompareScenarios_Click() As Boolean
        Dim Cls_CompareScen As New CompareScenarios_Cls(m_hookHelper)
        Cls_CompareScen.Get_FirstScenarioName = cboFirstScenario_MainForm.Text
        Cls_CompareScen.Get_SecondScenarioName = cboSecondScenario_MainForm.Text
        Cls_CompareScen.Set_ScreenRectangle = ScreenRectangle
        Cls_CompareScen.Set_Prog = ProgressBar_MainForm
        BtnCompareScenarios_Click = Cls_CompareScen.CompareScenarios()
    End Function

    Private Sub Show_AlertCustom(ByVal MessageError As String)
        Dim AlertCustom As New AlertCustom_Cls
        AlertCustom.Set_ScreenRectangle = ScreenRectangle
        AlertCustom.ShowLoadAlert(Nothing, MessageError)
    End Sub
End Class




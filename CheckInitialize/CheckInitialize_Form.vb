Public Class CheckInitialize_Form

    Private Sub CheckInitialize_Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = String.Format("لطفاً قبل از اجرای نرم افزار پیش نیازهای استفاده از نرم افزار  " & vbNewLine & "را بطور کامل بر روی سیستم خود نصب نمایید")
    End Sub

    Private Sub ButtonX1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonX1.Click
        Me.Close()
    End Sub
End Class
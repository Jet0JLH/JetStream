Public Class JetStream
    Private Sub JetStream_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim tempName As String = InputBox("Name of the new JetSet", "New JetSet", "")
        If tempName = "" Then
            MsgBox("Pleas set a name")
        Else
            Dim tempJetSet As New JetSet
            tempJetSet.TextBoxName.Text = tempName
            FlowLayoutPanel1.Controls.Add(tempJetSet)
        End If
    End Sub
End Class

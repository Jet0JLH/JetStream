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

    Private Sub SaveConfigToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveConfigToolStripMenuItem.Click
        Dim config As New XDocument(<conf></conf>)
        For Each item In FlowLayoutPanel1.Controls
            config.Element("conf").Add(item.generateSave)
        Next
        Select Case SaveFileDialog1.ShowDialog
            Case DialogResult.OK
                Try
                    config.Save(SaveFileDialog1.FileName)
                Catch ex As Exception
                    MsgBox("Error while saving config", MsgBoxStyle.Critical)
                End Try
        End Select
    End Sub

    Private Sub LoadConfigToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadConfigToolStripMenuItem.Click
        Select Case OpenFileDialog1.ShowDialog
            Case DialogResult.OK
                Try
                    Dim config As XDocument = XDocument.Load(OpenFileDialog1.FileName)
                    clearJetSets()
                    For Each item As XElement In config.Element("conf").Elements("jetcountdown")
                        Dim tempJetCountdown As New JetCountdown
                        tempJetCountdown.loadSave(item)
                        FlowLayoutPanel1.Controls.Add(tempJetCountdown)
                    Next
                    For Each item As XElement In config.Element("conf").Elements("jetset")
                        Dim tempJetSet As New JetSet
                        tempJetSet.loadSave(item)
                        FlowLayoutPanel1.Controls.Add(tempJetSet)
                    Next
                Catch ex As Exception
                    MsgBox("Error during loding config", MsgBoxStyle.Critical)
                End Try
        End Select
    End Sub
    Sub clearJetSets()
        For Each item In FlowLayoutPanel1.Controls
            item.Dispose()
        Next
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim tempName As String = InputBox("Name of the new countdown", "New countdown", "")
        If tempName = "" Then
            MsgBox("Pleas set a name")
        Else
            Dim tempJetSet As New JetCountdown
            tempJetSet.TextBoxName.Text = tempName
            FlowLayoutPanel1.Controls.Add(tempJetSet)
        End If
    End Sub
End Class

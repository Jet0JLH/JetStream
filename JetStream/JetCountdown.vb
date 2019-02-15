Public Class JetCountdown
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Select Case MsgBox("Do you realy want to delete this JetSet?", MsgBoxStyle.YesNo, "Realy delete JetSet?")
            Case MsgBoxResult.Yes
                Me.Dispose()
                MsgBox("JetSet was deleted")
            Case MsgBoxResult.No
                MsgBox("JetSet was not deleted")
        End Select
    End Sub

    Dim timerCount As Integer = 1
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If timerCount >= NumericUpDown1.Value Then
            PreviewLabel.Text = expandVars(RichTextBox1.Text)
            Try
                If CheckBox1.Checked Then
                    My.Computer.FileSystem.WriteAllText(TextBox1.Text, PreviewLabel.Text, False)
                End If
            Catch ex As Exception
                CheckBox1.Checked = False
            End Try
            timerCount = 1
        Else
            timerCount += 1
        End If
    End Sub
    Function expandVars(input As String) As String
        Dim outputString As String = input
        With My.Computer.Clock.LocalTime
            Dim secondDiff As String = DateDiff(DateInterval.Second, My.Computer.Clock.LocalTime, DateTimePicker1.Value)
            Dim minuteDiff As String = Math.Truncate(secondDiff / 60)
            Dim hourDiff As String = Math.Truncate(minuteDiff / 60)
            Dim dayDiff As String = Math.Truncate(hourDiff / 24)
            Dim monthDiff As String = Math.Truncate(DateDiff(DateInterval.Month, My.Computer.Clock.LocalTime, DateTimePicker1.Value))
            Dim yearDiff As String = Math.Truncate(DateDiff(DateInterval.Year, My.Computer.Clock.LocalTime, DateTimePicker1.Value))
            Dim onlySecond As String = secondDiff Mod 60
            Dim onlyMinute As String = minuteDiff Mod 60
            Dim onlyHour As String = hourDiff Mod 24
            Dim onlyDay As String = dayDiff Mod 31
            Dim onlyMonth As String = monthDiff Mod 12
            outputString = Microsoft.VisualBasic.Strings.Replace(outputString, "%fullsecond%", secondDiff, , , Constants.vbTextCompare)
            outputString = Microsoft.VisualBasic.Strings.Replace(outputString, "%fullminute%", minuteDiff, , , Constants.vbTextCompare)
            outputString = Microsoft.VisualBasic.Strings.Replace(outputString, "%fullhour%", hourDiff, , , Constants.vbTextCompare)
            outputString = Microsoft.VisualBasic.Strings.Replace(outputString, "%fullday%", dayDiff, , , Constants.vbTextCompare)
            outputString = Microsoft.VisualBasic.Strings.Replace(outputString, "%fullmonth%", monthDiff, , , Constants.vbTextCompare)
            outputString = Microsoft.VisualBasic.Strings.Replace(outputString, "%fullyear%", yearDiff, , , Constants.vbTextCompare)
            outputString = Microsoft.VisualBasic.Strings.Replace(outputString, "%second%", onlySecond, , , Constants.vbTextCompare)
            outputString = Microsoft.VisualBasic.Strings.Replace(outputString, "%minute%", onlyMinute, , , Constants.vbTextCompare)
            outputString = Microsoft.VisualBasic.Strings.Replace(outputString, "%hour%", onlyHour, , , Constants.vbTextCompare)
            outputString = Microsoft.VisualBasic.Strings.Replace(outputString, "%day%", onlyDay, , , Constants.vbTextCompare)
            outputString = Microsoft.VisualBasic.Strings.Replace(outputString, "%month%", onlyMonth, , , Constants.vbTextCompare)

        End With
        Return outputString
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        timerCount = Integer.MaxValue
    End Sub
    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs) Handles TrackBar1.Scroll
        NumericUpDown1.Value = sender.value
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        If sender.value > 200 Then
            TrackBar1.Value = 200
        Else
            TrackBar1.Value = sender.value
        End If
    End Sub

    Private Sub ButtonPath_Click(sender As Object, e As EventArgs) Handles ButtonPath.Click
        Select Case SaveFileDialog1.ShowDialog
            Case DialogResult.OK
                TextBox1.Text = SaveFileDialog1.FileName
        End Select
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        CheckBox1.Checked = False
    End Sub
    Public Function generateSave() As XElement
        Dim tempElement As New XElement(<jetcountdown><name></name><content></content><interval></interval><file></file><active></active><date></date></jetcountdown>)
        tempElement.Element("name").Value = TextBoxName.Text
        tempElement.Element("content").Value = RichTextBox1.Text
        tempElement.Element("interval").Value = NumericUpDown1.Value
        tempElement.Element("file").Value = TextBox1.Text
        tempElement.Element("active").Value = CheckBox1.Checked
        tempElement.Element("date").Value = DateTimePicker1.Value
        Return tempElement
    End Function
    Public Sub loadSave(config As XElement)
        CheckBox1.Checked = False
        TextBoxName.Text = config.Element("name").Value
        RichTextBox1.Text = config.Element("content").Value
        NumericUpDown1.Value = config.Element("interval").Value
        TextBox1.Text = config.Element("file").Value
        DateTimePicker1.Value = config.Element("date").Value
    End Sub
End Class

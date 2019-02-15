Public Class JetSet
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

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Select Case MsgBox("Do you realy want to delete this JetSet?", MsgBoxStyle.YesNo, "Realy delete JetSet?")
            Case MsgBoxResult.Yes
                Me.Dispose()
                MsgBox("JetSet was deleted")
            Case MsgBoxResult.No
                MsgBox("JetSet was not deleted")
        End Select
    End Sub

    Function expandVars(input As String) As String
        Dim outputString As String = input
        With My.Computer.Clock.LocalTime
            outputString = Microsoft.VisualBasic.Strings.Replace(outputString, "%day%", .Day, , , Constants.vbTextCompare)
            outputString = Microsoft.VisualBasic.Strings.Replace(outputString, "%month%", .Month, , , Constants.vbTextCompare)
            outputString = Microsoft.VisualBasic.Strings.Replace(outputString, "%year%", .Year, , , Constants.vbTextCompare)
            outputString = Microsoft.VisualBasic.Strings.Replace(outputString, "%second%", .Second, , , Constants.vbTextCompare)
            outputString = Microsoft.VisualBasic.Strings.Replace(outputString, "%minute%", .Minute, , , Constants.vbTextCompare)
            outputString = Microsoft.VisualBasic.Strings.Replace(outputString, "%hour%", .Hour, , , Constants.vbTextCompare)
            outputString = Microsoft.VisualBasic.Strings.Replace(outputString, "%currentdirectory%", IO.Directory.GetCurrentDirectory, , , Constants.vbTextCompare)
            'outputString = Microsoft.VisualBasic.Strings.Replace(outputString, "%lastRowIndex%", Form1.RichTextBox1.Lines.Count - 1, , , Constants.vbTextCompare) 'Aus mir unerfindlichen Gründen gibt diese Variable immer -1 zurück. Daher erst mal nicht aktiv
            Dim day As String = ""
            Dim month As String = ""
            Dim second As String = ""
            Dim minute As String = ""
            Dim hour As String = ""
            If .Day < 10 Then
                day = "0" & .Day
            Else
                day = .Day
            End If
            If .Month < 10 Then
                month = "0" & .Month
            Else
                month = .Month
            End If
            If .Second < 10 Then
                second = "0" & .Second
            Else
                second = .Second
            End If
            If .Minute < 10 Then
                minute = "0" & .Minute
            Else
                minute = .Minute
            End If
            If .Hour < 10 Then
                hour = "0" & .Hour
            Else
                hour = .Hour
            End If
            outputString = Microsoft.VisualBasic.Strings.Replace(outputString, "%0day%", day, , , Constants.vbTextCompare)
            outputString = Microsoft.VisualBasic.Strings.Replace(outputString, "%0month%", month, , , Constants.vbTextCompare)
            outputString = Microsoft.VisualBasic.Strings.Replace(outputString, "%0second%", second, , , Constants.vbTextCompare)
            outputString = Microsoft.VisualBasic.Strings.Replace(outputString, "%0minute%", minute, , , Constants.vbTextCompare)
            outputString = Microsoft.VisualBasic.Strings.Replace(outputString, "%0hour%", hour, , , Constants.vbTextCompare)
        End With
        Return outputString
    End Function

    Dim timerCount As Integer = 1
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If timerCount >= NumericUpDown1.Value Then
            PreviewLabel.Text = expandVars(RichTextBox1.Text)
            Try
                If CheckBox1.Checked Then
                    My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, PreviewLabel.Text, False)
                End If
            Catch ex As Exception
                CheckBox1.Checked = False
            End Try
            timerCount = 1
        Else
            timerCount += 1
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        CheckBox1.Checked = False
    End Sub

    Private Sub ButtonPath_Click(sender As Object, e As EventArgs) Handles ButtonPath.Click
        Select Case SaveFileDialog1.ShowDialog
            Case DialogResult.OK
                TextBox1.Text = SaveFileDialog1.FileName
        End Select
    End Sub
End Class

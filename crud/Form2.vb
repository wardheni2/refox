Imports MySql.Data.MySqlClient

Public Class Form2
    Dim svr As String = "localhost"
    Dim uid As String = "root"
    Dim pwd As String = "root"
    Dim db As String = "aplikasi-crud"

    Dim myConn As New MySqlConnection


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form3.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        myConn.ConnectionString = "server= " + svr + ";user id=" + uid + "; password=" + pwd + ";database=" + db + ""
        Try
            Dim cmd As MySqlCommand = New MySqlCommand("select * from users where username='" + TextBox1.Text + "' and password='" + TextBox2.Text + "'", myConn)
            Dim sda As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            sda.Fill(dt)
            If (dt.Rows.Count > 0) Then
                Form1.Show()
                Me.Hide()
            Else
                MessageBox.Show("login Gagal", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As MySqlException
            MessageBox.Show("Error SQL: " & ex.Message)
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            TextBox2.PasswordChar = ""
        Else
            TextBox2.PasswordChar = "*"
        End If
    End Sub
End Class
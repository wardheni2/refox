Imports MySql.Data.MySqlClient
Imports System.Security.Cryptography

Public Class Form3
    Dim svr As String = "localhost"
    Dim uid As String = "root"
    Dim pwd As String = "root"
    Dim db As String = "aplikasi-crud"

    Dim myConn As New MySqlConnection
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim username As String = TextBox1.Text
        Dim password As String = TextBox2.Text


        myConn.ConnectionString = "server= " + svr + ";user id=" + uid + "; password=" + pwd + ";database=" + db + ""

        If TextBox2.Text = TextBox3.Text Then
            Try

                Dim cmd As MySqlCommand = New MySqlCommand("INSERT INTO `users` (`username`, `password`) 
            VALUES ('" + username + "', '" + password + "');", myConn)
                Dim sda As MySqlDataAdapter = New MySqlDataAdapter(cmd)
                Dim ds As DataSet = New Data.DataSet
                sda.Fill(ds)
                MessageBox.Show("Register Berhasil", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Form2.Show()
                Me.Hide()

            Catch ex As MySqlException
                MessageBox.Show("SQL Error: " & ex.Message)
            End Try
        Else
            MessageBox.Show("Register Gagal : Password Tidak sama", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
End Class
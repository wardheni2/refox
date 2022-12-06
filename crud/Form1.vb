Imports System.Drawing.Printing
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class Form1
    Dim svr As String = "443"
    Dim uid As String = "root"
    Dim pwd As String = "root"
    Dim db As String = "aplikasi-crud"

    Dim myConn As New MySqlConnection


    Dim edit As Boolean = False


    Private Sub RefreshData_Click(sender As Object, e As EventArgs) Handles RefreshData.Click
        UpdateData()

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label6.Visible = False
        UpdateData()
    End Sub


    Sub UpdateData()
        myConn.ConnectionString = "server= " + svr + ";user id=" + uid + "; password=" + pwd + ";database=" + db + ""

        Dim dt As DataTable = New DataTable()

        'gunakan metode try catch untuk menangkap error ketika menggunakan perintah sql atau sql command
        Try
            Dim cmd As MySqlCommand = New MySqlCommand("Select * from students", myConn)
            Dim sda As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            sda.Fill(dt)
        Catch ex As MySqlException
            MessageBox.Show("Error SQL: " & ex.Message)
        End Try

        'bershikan dulu listview
        ListView1.Items.Clear()

        For Each drow As DataRow In dt.Rows
            ListView1.Items.Add(drow(0).ToString()) 'menambahkan di column pertama
            ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(drow(1).ToString())
            ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(drow(2).ToString())
            ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(drow(3).ToString())

            ListView1.Items(ListView1.Items.Count - 1).SubItems.Add(drow(4).ToString())
        Next
    End Sub

    Private Sub SimpanData_Click(sender As Object, e As EventArgs) Handles SimpanData.Click
        If edit = False Then
            InsertData()
        Else
            SaveEdit()
            edit = False
        End If

        UpdateData()
    End Sub



    Sub InsertData()
        Dim nis As String = TextBox1.Text
        Dim nama As String = TextBox2.Text
        Dim tempat_lahir As String = TextBox3.Text

        Dim tanggal_lahir As Date = DateTimePicker1.Value

        myConn.ConnectionString = "server= " + svr + ";user id=" + uid + "; password=" + pwd + ";database=" + db + ""

        Try
            Dim cmd As MySqlCommand = New MySqlCommand("INSERT INTO `students` (`nis`, `nama`, `tempat_lahir`, `tanggal_lahir`) 
            VALUES ('" + nis + "', '" + nama + "', '" + tempat_lahir + "', '" + tanggal_lahir.ToString("yyyy-MM-dd") + "');", myConn)
            Dim sda As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim ds As DataSet = New Data.DataSet
            sda.Fill(ds)
        Catch ex As MySqlException
            MessageBox.Show("Error SQL: " & ex.Message)
        End Try
    End Sub

    Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.Click
        Dim tt As String
        tt = ListView1.SelectedItems.Item(0).SubItems(0).Text
    End Sub



    Sub DeleteData(ByVal id As String)

        myConn.ConnectionString = "server= " + svr + ";user id=" + uid + "; password=" + pwd + ";database=" + db + ""
        Try
            Dim cmd As MySqlCommand = New MySqlCommand("DELETE FROM `students` WHERE ((`id` = '" + id + "'));", myConn)
            Dim sda As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim ds As DataSet = New Data.DataSet
            sda.Fill(ds)
        Catch ex As MySqlException
            MessageBox.Show("Error SQL: " & ex.Message)
        End Try
    End Sub

    Private Sub HapusData_Click(sender As Object, e As EventArgs) Handles HapusData.Click
        Dim lv As String
        lv = ListView1.SelectedItems.Item(0).SubItems(0).Text
        DeleteData(lv)
        UpdateData()
    End Sub

    Sub GetEditData(ByVal id As String)
        myConn.ConnectionString = "server= " + svr + ";user id=" + uid + "; password=" + pwd + ";database=" + db + ""
        Try
            Dim cmd As MySqlCommand = New MySqlCommand("SELECT * FROM students WHERE id=" + id + ";", myConn)
            Dim sda As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            sda.Fill(dt)

            For Each drow As DataRow In dt.Rows
                Label6.Text = drow(0)
                TextBox1.Text = drow(1) 'menambahkan di column pertama
                TextBox2.Text = drow(2)
                TextBox3.Text = drow(3)
                DateTimePicker1.Value = drow(4)
            Next

        Catch ex As MySqlException
            MessageBox.Show("Error SQL: " & ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim lv As String
        lv = ListView1.SelectedItems.Item(0).SubItems(0).Text
        GetEditData(lv)
        edit = True
    End Sub

    Sub SaveEdit()
        Dim id As String = Label6.Text
        Dim nis As String = TextBox1.Text
        Dim nama As String = TextBox2.Text
        Dim tempat_lahir As String = TextBox3.Text

        Dim tanggal_lahir As Date = DateTimePicker1.Value
        myConn.ConnectionString = "server= " + svr + ";user id=" + uid + "; password=" + pwd + ";database=" + db + ""
        Try
            Dim cmd As MySqlCommand = New MySqlCommand("UPDATE `students` SET
            `nis` = '" + nis + "',
            `nama` = '" + nama + "',
            `tempat_lahir` = '" + tempat_lahir + "',
            `tanggal_lahir` = '" + tanggal_lahir.ToString("yyyy-MM-dd") + "'
            WHERE `id` = '" + Label6.Text + "';", myConn)
            Dim sda As MySqlDataAdapter = New MySqlDataAdapter(cmd)
            Dim dt As DataTable = New DataTable()
            sda.Fill(dt)
        Catch ex As MySqlException
            MessageBox.Show("Error SQL: " & ex.Message)
        End Try
    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage


        Dim font As New Font("Aria", 16, FontStyle.Regular)
        Dim tinggi As Integer

        tinggi = tinggi + 10
        e.Graphics.DrawString("Data Siswa", font, Brushes.Black, 2, tinggi)

        tinggi = tinggi + 20
        e.Graphics.DrawString("SMK Negeri 2 Surakarta", font, Brushes.Black, 2, tinggi)

        tinggi = tinggi + 20
        e.Graphics.DrawString("........................................................", font, Brushes.Black, 2, tinggi)

        tinggi = tinggi + 20
        e.Graphics.DrawString("NIS" & vbTab & vbTab & vbTab & vbTab & "Nama" & vbTab & vbTab & vbTab & vbTab & vbTab & "Tempat Lahir" & vbTab & "Tanggal Lahir", font, Brushes.Black, 2, tinggi)

        tinggi = tinggi + 10
        e.Graphics.DrawString("........................................................", font, Brushes.Black, 2, tinggi)

        tinggi = tinggi + 20
        e.Graphics.DrawString(ListView1.SelectedItems.Item(0).SubItems(1).Text & vbTab &
                              ListView1.SelectedItems.Item(0).SubItems(2).Text & vbTab & vbTab &
                              ListView1.SelectedItems.Item(0).SubItems(3).Text & vbTab &
                              ListView1.SelectedItems.Item(0).SubItems(4).Text, font, Brushes.Black, 2, tinggi)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        PrintDialog1.PrinterSettings = PrintDocument1.PrinterSettings
        If PrintDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            PrintDocument1.PrinterSettings = PrintDialog1.PrinterSettings

            Dim pageSetup As New PageSettings
            With pageSetup
                .Margins.Left = 50
                .Margins.Right = 50
                .Margins.Top = 50
                .Margins.Bottom = 50
                .Landscape = True
            End With
            PrintDocument1.DefaultPageSettings = pageSetup

        End If
        PrintPreviewDialog1.Document = PrintDocument1
        PrintPreviewDialog1.ShowDialog()
    End Sub
End Class

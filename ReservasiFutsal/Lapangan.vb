Imports System.Data.Odbc
Public Class Lapangan
    Sub KondisiAwal()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False

        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button1.Text = "Input"
        Button2.Text = "Edit"
        Button3.Text = "Delete"
        Button4.Text = "Exit"
        Button5.Visible = False

        Call Connection()
        Da = New OdbcDataAdapter("Select * from lapangan", Conn)
        Ds = New DataSet
        Da.Fill(Ds, "lapangan")
        DataGridView1.DataSource = Ds.Tables("lapangan")
        DataGridView1.ReadOnly = True
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Sub SiapIsi()
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox4.Enabled = True
    End Sub

    Sub NomorOtomatis()
        Call Connection()
        Cmd = New OdbcCommand("Select * from lapangan where id_lapangan in (select max(id_lapangan) from lapangan)", Conn)
        Dim UrutanKode As String
        Dim Hitung As Long
        Rd = Cmd.ExecuteReader
        Rd.Read()
        If Not Rd.HasRows Then
            UrutanKode = "Lapangan" + "001"
        Else
            Hitung = Microsoft.VisualBasic.Right(Rd.GetString(0), 3) + 1
            UrutanKode = "Lapangan" + Microsoft.VisualBasic.Right("000" & Hitung, 3)
        End If
        TextBox1.Text = UrutanKode
    End Sub

    'Sub MunculSatuan()
    '    Call Connection()
    '    Cmd = New OdbcCommand("Select distinct satuan from tbl_barang", Conn)
    '    Rd = Cmd.ExecuteReader()
    '    ComboBox1.Items.Clear()
    '    Do While Rd.Read
    '        ComboBox1.Items.Add(Rd.Item("satuan"))
    '    Loop
    'End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Button1.Text = "Input" Then
            Button1.Text = "Simpan"
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Text = "Batal"
            Call NomorOtomatis()
            Call SiapIsi()
            TextBox1.Enabled = False
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
                MsgBox("Masukkan Data Terlebih Dahulu")
            Else
                Call Connection()
                Dim InputData As String = "Insert Into lapangan values ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "')"
                Cmd = New OdbcCommand(InputData, Conn)
                Cmd.ExecuteNonQuery()
                MsgBox("Data Berhasil di Tambahkan")
                Call KondisiAwal()
            End If
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Button2.Text = "Edit" Then
            Button2.Text = "Simpan"
            Button1.Enabled = False
            Button3.Enabled = False
            Button4.Text = "Batal"
            Button5.Visible = True
            Call SiapIsi()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
                MsgBox("Masukkan Data Terlebih Dahulu")
            Else
                Call Connection()
                Dim UpdateData As String = "Update lapangan set harga_lapangan = '" & TextBox2.Text & "',jenis_rumput = '" & TextBox3.Text & "',DP = '" & TextBox4.Text & "', Where id_lapangan = '" & TextBox1.Text & "'"
                Cmd = New OdbcCommand(UpdateData, Conn)
                Cmd.ExecuteNonQuery()
                MsgBox("Data Berhasil di Ubah")
                Call KondisiAwal()
            End If
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If Button3.Text = "Delete" Then
            Button3.Text = "Hapus"
            Button1.Enabled = False
            Button2.Enabled = False
            Button4.Text = "Batal"
            Button5.Visible = True
            Call SiapIsi()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
                MsgBox("Masukkan Data Terlebih Dahulu")
            Else
                Call Connection()
                Dim DeleteData As String = "Delete From lapangan Where id_lapangan = '" & TextBox1.Text & "'"
                Cmd = New OdbcCommand(DeleteData, Conn)
                Cmd.ExecuteNonQuery()
                MsgBox("Data Berhasil di Hapus")
                Call KondisiAwal()
            End If
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If Button4.Text = "Exit" Then
            Me.Close()
        Else
            Call KondisiAwal()
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Call Connection()
        Cmd = New OdbcCommand("Select * From lapangan where id_lapangan = '" & TextBox1.Text & "'", Conn)
        Rd = Cmd.ExecuteReader
        Rd.Read()
        If Not Rd.HasRows Then
            MsgBox("Data tidak ditemukan!")
        Else
            TextBox1.Text = Rd.Item("id_lapangan")
            TextBox1.Enabled = False
            TextBox2.Text = Rd.Item("harga_lapangan")
            TextBox3.Text = Rd.Item("jenis_rumput")
            TextBox4.Text = Rd.Item("DP")
        End If
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
        
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then
            'Call Button5_Click(e, AcceptButton)
        End If
    End Sub

    Private Sub Lapangan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call KondisiAwal()
    End Sub

    Private Sub TextBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True

    End Sub

End Class
Imports System.Data.Odbc
Public Class Admin
    Sub KondisiAwal()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        ComboBox1.Items.Clear()
        ComboBox1.Text = ""
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        ComboBox1.Enabled = False

        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button1.Text = "Input"
        Button2.Text = "Edit"
        Button3.Text = "Delete"
        Button4.Text = "Exit"

        Call Connection()
        Da = New OdbcDataAdapter("Select id_admin 'username' ,nama_admin,level from admin", Conn)
        Ds = New DataSet
        Da.Fill(Ds, "admin")
        DataGridView1.DataSource = Ds.Tables("admin")
        DataGridView1.ReadOnly = True
        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Sub SiapIsi()
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        ComboBox1.Enabled = True
        ComboBox1.Items.Add("ADMIN")
        ComboBox1.Items.Add("USER")
    End Sub

    Private Sub FormMasterAdmin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call KondisiAwal()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Button1.Text = "Input" Then
            Button1.Text = "Simpan"
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Text = "Batal"
            Call SiapIsi()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or ComboBox1.Text = "" Then
                MsgBox("Masukkan Data Terlebih Dahulu")
            Else
                Call Connection()
                Dim InputData As String = "Insert Into admin values ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & ComboBox1.Text & "')"
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
            Call SiapIsi()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or ComboBox1.Text = "" Then
                MsgBox("Masukkan Data Terlebih Dahulu")
            Else
                Call Connection()
                Dim UpdateData As String = "Update admin set nama_admin = '" & TextBox2.Text & "',password = '" & TextBox3.Text & "',level = '" & ComboBox1.Text & "' Where id_admin = '" & TextBox1.Text & "'"
                Cmd = New OdbcCommand(UpdateData, Conn)
                Cmd.ExecuteNonQuery()
                MsgBox("Data Berhasil di Ubah")
                Call KondisiAwal()
            End If
        End If
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            Call Connection()
            Cmd = New OdbcCommand("Select * From admin where id_admin = '" & TextBox1.Text & "'", Conn)
            Rd = Cmd.ExecuteReader
            Rd.Read()
            If Not Rd.HasRows Then
                MsgBox("Data tidak ditemukan!")
            Else
                TextBox1.Text = Rd.Item("id_Admin")
                TextBox2.Text = Rd.Item("nama_admin")
                TextBox3.Text = Rd.Item("password")
                ComboBox1.Text = Rd.Item("level")
            End If
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If Button4.Text = "Exit" Then
            Me.Close()
        Else
            Call KondisiAwal()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If Button3.Text = "Delete" Then
            Button3.Text = "Hapus"
            Button1.Enabled = False
            Button2.Enabled = False
            Button4.Text = "Batal"
            Call SiapIsi()
        Else
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or ComboBox1.Text = "" Then
                MsgBox("Masukkan Data Terlebih Dahulu")
            Else
                Call Connection()
                Dim DeleteData As String = "Delete From admin Where id_admin = '" & TextBox1.Text & "'"
                Cmd = New OdbcCommand(DeleteData, Conn)
                Cmd.ExecuteNonQuery()
                MsgBox("Data Berhasil di Hapus")
                Call KondisiAwal()
            End If
        End If
    End Sub
End Class
Imports System.Data.Odbc
Public Class DataPembokingan

    Sub KondisiAwal()
        Button1.Enabled = True
        LabelHarga.Text = ""
        LabelJenis.Text = ""
        LabelDP.Text = ""
        Call BuatKolom()
        Call launchLapangan()
        ComboBox1.Text = ""
        DateTimePicker1.Text = Today
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = ("dd/MM/yyyy")
        DataGridView1.Columns.Clear()
    End Sub

    Sub BuatKolom()
        'DataGridView1.Columns.Clear()
        'DataGridView1.Columns.Add("id_lapangan", "ID Lapangan")
        'DataGridView1.Columns.Add("tanggalreservasi", "Tanggal Boking")
        'DataGridView1.Columns.Add("waktureservasi", "Jam Boking")
        'DataGridView1.Columns.Add("nama_pelanggan", "Nama Pemesan")
        'DataGridView1.Columns.Add("telepon", "Telepon")
        'DataGridView1.Columns.Add("tanggal_main", "Tanggal Main")
        'DataGridView1.Columns.Add("lama_main", "Lama Main")
        'DataGridView1.Columns.Add("status", "Status")
        'DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

    End Sub

    Sub launchLapangan()
        Call Connection()
        ComboBox1.Items.Clear()
        Cmd = New OdbcCommand("Select * from lapangan", Conn)
        Rd = Cmd.ExecuteReader
        Do While Rd.Read
            ComboBox1.Items.Add(Rd.Item(0))
        Loop
    End Sub

    'Dim sql As String = "Select DATE_FORMAT(tanggalreservasi, '%d/%m/%Y') 'Tanggal Boking', waktureservasi 'Jam Boking', nama_pelanggan 'Nama Pemesan', telepon 'Telepon', DATE_FORMAT(tanggal_main, '%d/%m/%Y') 'Tanggal Main', jam_main 'Jam Main', lama_main 'Lama_Main', status 'Status', dp 'DP', jam_main 'Jam Main' from reservasi where id_lapangan='" & ComboBox1.Text & "' and tanggalreservasi='" & Label3.Text & "'"
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Call Connection()
        Cmd = New OdbcCommand("Select * from lapangan where id_lapangan ='" & ComboBox1.Text & "'", Conn)
        Rd = Cmd.ExecuteReader
        Rd.Read()
        If Rd.HasRows Then
            LabelHarga.Text = Rd!harga_lapangan
            LabelJenis.Text = Rd!jenis_rumput
            LabelDP.Text = Rd!DP

            DataGridView1.Columns.Clear()
            Da = New OdbcDataAdapter("Select DATE_FORMAT(tanggalreservasi, '%d/%m/%Y') 'Tanggal Boking', waktureservasi 'Jam Boking', nama_pelanggan 'Nama Pemesan', telepon 'Telepon', DATE_FORMAT(tanggal_main, '%d/%m/%Y') 'Tanggal Main', jam_main 'Jam Main', lama_main 'Lama Main', status 'Status', dp 'DP' from reservasi where id_lapangan='" & ComboBox1.Text & "' and tanggalreservasi='" & Label3.Text & "'", Conn)
            Ds = New DataSet
            Da.Fill(Ds, "reservasi")

            'DataGridView1.DataSource(Rd!id_lapangan, Rd!tanggalreservasi, Rd!waktureservasi, Rd!nama_pelanggan, Rd!telepon, Rd!tanggal_main, Rd!lama_main, Rd!status)
            DataGridView1.DataSource = Ds.Tables("reservasi")
            DataGridView1.ReadOnly = True
            'DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End If
    End Sub

    Sub launchdata()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub DataPembokingan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call KondisiAwal()
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        Label3.Text = Format(DateTimePicker1.Value, "yyyy-MM-dd")

    End Sub


    Private Sub Label3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label3.TextChanged
        DataGridView1.Columns.Clear()
        Da = New OdbcDataAdapter("Select DATE_FORMAT(tanggalreservasi, '%d/%m/%Y') 'Tanggal Boking', waktureservasi 'Jam Boking', nama_pelanggan 'Nama Pemesan', telepon 'Telepon', DATE_FORMAT(tanggal_main, '%d/%m/%Y') 'Tanggal Main', jam_main 'Jam Main', lama_main 'Lama_Main', status 'Status', dp 'DP' from reservasi where id_lapangan='" & ComboBox1.Text & "' and tanggalreservasi='" & Label3.Text & "'", Conn)
        Ds = New DataSet
        Da.Fill(Ds, "reservasi")

        'DataGridView1.DataSource(Rd!id_lapangan, Rd!tanggalreservasi, Rd!waktureservasi, Rd!nama_pelanggan, Rd!telepon, Rd!tanggal_main, Rd!lama_main, Rd!status)
        DataGridView1.DataSource = Ds.Tables("reservasi")
        DataGridView1.ReadOnly = True
        'DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        End
    End Sub
End Class
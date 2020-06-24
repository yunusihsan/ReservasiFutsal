Imports System.Data.Odbc
Public Class Reservasi
    Sub KondisiAwal()
        LBLHarga.Text = ""
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox5.Text = ""
        LBLJenis.Text = ""
        CheckBox1.Enabled = False
        CheckBox3.Enabled = False
        CheckBox5.Enabled = False
        CheckBox7.Enabled = False
        CheckBox9.Enabled = False
        CheckBox11.Enabled = False
        CheckBox13.Enabled = False
        CheckBox15.Enabled = False
        CheckBox16.Enabled = False
        CheckBox14.Enabled = False
        CheckBox12.Enabled = False
        CheckBox10.Enabled = False
        CheckBox8.Enabled = False
        CheckBox6.Enabled = False
        CheckBox4.Enabled = False
        CheckBox2.Enabled = False
        Call uncheck()
        LBLTangBok.Text = Format(Today, "dd/MM/yyyy")
        LBLAdmin.Text = MenuUtama.STLabel2.Text
        LBLLamaMain.Text = ""
        LBLTotal.Text = ""
        LBLDP.Text = ""
        LBLKembali.Text = ""
        Call MunculLapangan()
        Call NomorOtomatis()
        ComboBox1.Text = ""
        DateTimePicker1.Text = Today
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = ("dd/MM/yyyy")

    End Sub

    Sub uncheck()
        CheckBox1.CheckState = CheckState.Unchecked
        CheckBox3.CheckState = CheckState.Unchecked
        CheckBox5.CheckState = CheckState.Unchecked
        CheckBox7.CheckState = CheckState.Unchecked
        CheckBox9.CheckState = CheckState.Unchecked
        CheckBox11.CheckState = CheckState.Unchecked
        CheckBox13.CheckState = CheckState.Unchecked
        CheckBox15.CheckState = CheckState.Unchecked
        CheckBox16.CheckState = CheckState.Unchecked
        CheckBox14.CheckState = CheckState.Unchecked
        CheckBox12.CheckState = CheckState.Unchecked
        CheckBox10.CheckState = CheckState.Unchecked
        CheckBox8.CheckState = CheckState.Unchecked
        CheckBox6.CheckState = CheckState.Unchecked
        CheckBox4.CheckState = CheckState.Unchecked
        CheckBox2.CheckState = CheckState.Unchecked
    End Sub
    Sub MunculLapangan()
        Call Connection()
        ComboBox1.Items.Clear()
        Cmd = New OdbcCommand("Select * from lapangan", Conn)
        Rd = Cmd.ExecuteReader
        Do While Rd.Read
            ComboBox1.Items.Add(Rd.Item(0))
        Loop
    End Sub

    Dim stat As String
    Dim dp As String
    Sub status()

        If RadioButton1.Checked Then
            stat = "DP"
            dp = TextBox4.Text

        ElseIf RadioButton2.Checked Then
            stat = "Lunas"
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Call uncheck()
        Call Connection()
        Cmd = New OdbcCommand("Select * from lapangan where id_lapangan ='" & ComboBox1.Text & "'", Conn)
        Rd = Cmd.ExecuteReader
        Rd.Read()

        If Rd.HasRows Then
            CheckBox1.Enabled = True
            CheckBox3.Enabled = True
            CheckBox5.Enabled = True
            CheckBox7.Enabled = True
            CheckBox9.Enabled = True
            CheckBox11.Enabled = True
            CheckBox13.Enabled = True
            CheckBox15.Enabled = True
            CheckBox16.Enabled = True
            CheckBox14.Enabled = True
            CheckBox12.Enabled = True
            CheckBox10.Enabled = True
            CheckBox8.Enabled = True
            CheckBox6.Enabled = True
            CheckBox4.Enabled = True
            CheckBox2.Enabled = True
            LBLHarga.Text = Rd!harga_lapangan
            LBLJenis.Text = Rd!jenis_rumput
            LBLDP.Text = Rd!DP
            If RadioButton1.Checked = True Then
                LBLTotal.Text = ""
            ElseIf RadioButton2.Checked = True Then
                LBLTotal.Text = Rd!harga_lapangan
            End If

        End If
        Call checkoff()
    End Sub

    Sub NomorOtomatis()
        Call Connection()
        Cmd = New OdbcCommand("Select * from reservasi where id_reservasi in (select max(id_reservasi) from reservasi)", Conn)
        Dim UrutanKode As String
        Dim Hitung As Long
        Rd = Cmd.ExecuteReader
        Rd.Read()
        If Not Rd.HasRows Then
            UrutanKode = "RE" + Format(Now, "yyMMdd") + "001"
        Else
            Hitung = Microsoft.VisualBasic.Right(Rd.GetString(0), 9) + 1
            UrutanKode = "RE" + Format(Now, "yyMMdd") + Microsoft.VisualBasic.Right("000" & Hitung, 3)
        End If
        LBLIDReser.Text = UrutanKode
    End Sub

    Private Sub Reservasi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call KondisiAwal()
    End Sub

    Sub Rumus()
        Dim HitungTotal As Integer = 0
        If ComboBox1.Text = "" Then
            MsgBox("Pilih Lapangan terlebih dahulu!")
        ElseIf TextBox5.Text = "" Then
            MsgBox("Masukan Jumlah Pembayaran!", MsgBoxStyle.Critical, "WARNING!")
        ElseIf RadioButton2.Checked = True Then
            If Val(TextBox5.Text) < Val(LBLTotal.Text) Then
                MsgBox("Pembayaran Kurang!")
            ElseIf Val(TextBox5.Text) = Val(LBLTotal.Text) Then
                LBLKembali.Text = 0
            ElseIf Val(TextBox5.Text) > Val(LBLTotal.Text) Then
                LBLKembali.Text = Val(TextBox5.Text) - Val(LBLTotal.Text)
                Button17.Focus()
            End If
        ElseIf RadioButton1.Checked = True Then
            If Val(TextBox4.Text) < Val(LBLDP.Text) Then
                MsgBox("DP kurang")
            ElseIf Val(TextBox5.Text) < Val(TextBox4.Text) Then
                MsgBox("Pembayaran Kurang!")
            ElseIf Val(TextBox5.Text) = Val(TextBox4.Text) Then
                LBLKembali.Text = 0
            ElseIf Val(TextBox5.Text) > Val(TextBox4.Text) Then
                LBLTotal.Text = TextBox4.Text
                LBLKembali.Text = Val(TextBox5.Text) - Val(TextBox4.Text)
                Button17.Focus()
            End If
        End If
    End Sub

    Sub rumus1()
        Dim jam As Integer = 0
        For Each ctl As CheckBox In GroupBox1.Controls
            If TypeOf ctl Is System.Windows.Forms.CheckBox Then
                Dim ck As System.Windows.Forms.CheckBox = ctl
                If ck.Checked Then
                    jam += 1
                End If
            End If
        Next
        LBLLamaMain.Text = jam
        Dim hitungtotal1 As Integer = 0
        If ComboBox1.Text = "" Then
            MsgBox("Pilih Lapangan terlebih dahulu!")
        ElseIf RadioButton1.Checked = True Then
            hitungtotal1 = LBLDP.Text * LBLLamaMain.Text
            TextBox4.Text = hitungtotal1
        ElseIf RadioButton2.Checked Then
            hitungtotal1 = LBLHarga.Text * LBLLamaMain.Text
        End If
        LBLTotal.Text = hitungtotal1

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Call rumus1()
    End Sub

    Private Sub Button20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button20.Click
        If TextBox4.Text = "" Then
            TextBox4.Text = "0"
        End If
        Call Rumus()

        'set lama_main
        Dim jam As Integer = 0
            For Each ctl As CheckBox In GroupBox1.Controls
                If TypeOf ctl Is System.Windows.Forms.CheckBox Then
                    Dim ck As System.Windows.Forms.CheckBox = ctl
                    If ck.Checked Then
                        jam += 1
                    End If
                End If
            Next
            LBLLamaMain.Text = jam

    End Sub

    

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        LBLJam.Text = TimeOfDay
    End Sub

    Private Sub Button21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button21.Click
        'Me.Close()
        End
    End Sub

    Private Sub Button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button19.Click
        DataPembokingan.ShowDialog()
    End Sub

    Private Sub Button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button18.Click
        If MsgBox("Apakah anda yakin ingin membatalkan Reservasi?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            'DataGridView1.Columns.Clear()
            Call KondisiAwal()
        End If
    End Sub

    Dim TglMySQL As String
    Dim NewString As String
    Dim Jadwal As String
    Dim finalString As String
    Private Sub Button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button17.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or ComboBox1.Text = "" Or TextBox5.Text = "" Then
            MsgBox("Masukan data terlebih dahulu")
        Else
            'set jam_main
            Dim bose As String = String.Empty
            Dim random As Random = New Random()
            Dim i As Integer = random.Next(1, 16)
            Dim x As Integer = 1
            For Each c As CheckBox In GroupBox1.Controls
                If x <= i Then
                    Dim str As String = "Checkbox : " & x
                    c.Name = str
                    If c.Checked = True Then
                        bose &= c.Text & ", "
                    End If
                End If
            Next
            Dim MyChar() As Char = {","}
            NewString = bose.TrimEnd(MyChar)

            Jadwal = NewString
            finalString = Jadwal.Replace("08:00 - 09:00", "01 ")
            Jadwal = finalString
            finalString = Jadwal.Replace("09:00 - 10:00", "02 ")
            Jadwal = finalString
            finalString = Jadwal.Replace("10:00 - 11:00", "03 ")
            Jadwal = finalString
            finalString = Jadwal.Replace("11:00 - 12:00", "04 ")
            Jadwal = finalString
            finalString = Jadwal.Replace("12:00 - 13:00", "05 ")
            Jadwal = finalString
            finalString = Jadwal.Replace("13:00 - 14:00", "06 ")
            Jadwal = finalString
            finalString = Jadwal.Replace("14:00 - 15:00", "07 ")
            Jadwal = finalString
            finalString = Jadwal.Replace("15:00 - 16:00", "08 ")
            Jadwal = finalString
            finalString = Jadwal.Replace("16:00 - 17:00", "09 ")
            Jadwal = finalString
            finalString = Jadwal.Replace("17:00 - 18:00", "10 ")
            Jadwal = finalString
            finalString = Jadwal.Replace("18:00 - 19:00", "11 ")
            Jadwal = finalString
            finalString = Jadwal.Replace("19:00 - 20:00", "12")
            Jadwal = finalString
            finalString = Jadwal.Replace("20:00 - 21:00", "13 ")
            Jadwal = finalString
            finalString = Jadwal.Replace("21:00 - 22:00", "14 ")
            Jadwal = finalString
            finalString = Jadwal.Replace("22:00 - 23:00", "15 ")
            Jadwal = finalString
            finalString = Jadwal.Replace("23:00 - 00:00", "16 ")
            Jadwal = finalString
            finalString = Jadwal.Replace(",", "")
            Jadwal = finalString

            Call Connection()
            Call status()
            TglMySQL = Format(Today, "yyyy-MM-dd")
            Dim InputData As String = "insert into reservasi values ('" & LBLIDReser.Text & "','" & TglMySQL & "','" & LBLJam.Text & "','" & ComboBox1.Text & "','" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & Label23.Text & "','" & LBLLamaMain.Text & " Jam','" & stat & "','" & dp & "','" & NewString & "','" & Jadwal & "')"
            Cmd = New OdbcCommand(InputData, Conn)
            Cmd.ExecuteNonQuery()
            MsgBox("Data berhasil disimpan")
            Call KondisiAwal()
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        TextBox4.Enabled = False
        LBLTotal.Text = LBLHarga.Text
        LBLKembali.Text = ""
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        TextBox4.Enabled = True
        LBLTotal.Text = ""
        LBLKembali.Text = ""
    End Sub

    Private Sub TextBox4_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox4.EnabledChanged
        TextBox4.Text = "0"
    End Sub

    Sub checkoff()
        RichTextBox1.Text = ""
        Cmd = New OdbcCommand("Select * from reservasi where tanggal_main ='" & Label23.Text & "' and id_lapangan='" & ComboBox1.Text & "'", Conn)
        Rd = Cmd.ExecuteReader
        Do While Rd.Read
            'RichTextBox1.items.add(Rd.Item(12))
            RichTextBox1.AppendText(Rd.Item(12))
        Loop

        CheckBox1.Enabled = True
        CheckBox3.Enabled = True
        CheckBox5.Enabled = True
        CheckBox7.Enabled = True
        CheckBox9.Enabled = True
        CheckBox11.Enabled = True
        CheckBox13.Enabled = True
        CheckBox15.Enabled = True
        CheckBox16.Enabled = True
        CheckBox14.Enabled = True
        CheckBox12.Enabled = True
        CheckBox10.Enabled = True
        CheckBox8.Enabled = True
        CheckBox6.Enabled = True
        CheckBox4.Enabled = True
        CheckBox2.Enabled = True

        Dim k1() As String = {"01"}
        Dim k2() As String = {"02"}
        Dim k3() As String = {"03"}
        Dim k4() As String = {"04"}
        Dim k5() As String = {"05"}
        Dim k6() As String = {"06"}
        Dim k7() As String = {"07"}
        Dim k8() As String = {"08"}
        Dim k9() As String = {"09"}
        Dim k10() As String = {"10"}
        Dim k11() As String = {"11"}
        Dim k12() As String = {"12"}
        Dim k13() As String = {"13"}
        Dim k14() As String = {"14"}
        Dim k15() As String = {"15"}
        Dim k16() As String = {"16"}
        If k1.Count(Function(w) RichTextBox1.Text.ToLower.Contains(w)) > 0 Then
            CheckBox1.Enabled = False
            'CheckBox1.BackColor = Color.Red
            'CheckBox1.Font = New Font(CheckBox1.Font, CheckBox1.Font.Style Or FontStyle.Strikeout)
        End If
        If k2.Count(Function(w) RichTextBox1.Text.ToLower.Contains(w)) > 0 Then
            CheckBox2.Enabled = False
        End If
        If k3.Count(Function(w) RichTextBox1.Text.ToLower.Contains(w)) > 0 Then
            CheckBox3.Enabled = False
        End If
        If k4.Count(Function(w) RichTextBox1.Text.ToLower.Contains(w)) > 0 Then
            CheckBox4.Enabled = False
        End If
        If k5.Count(Function(w) RichTextBox1.Text.ToLower.Contains(w)) > 0 Then
            CheckBox5.Enabled = False
        End If
        If k6.Count(Function(w) RichTextBox1.Text.ToLower.Contains(w)) > 0 Then
            CheckBox6.Enabled = False
        End If
        If k7.Count(Function(w) RichTextBox1.Text.ToLower.Contains(w)) > 0 Then
            CheckBox7.Enabled = False
        End If
        If k8.Count(Function(w) RichTextBox1.Text.ToLower.Contains(w)) > 0 Then
            CheckBox8.Enabled = False
        End If
        If k9.Count(Function(w) RichTextBox1.Text.ToLower.Contains(w)) > 0 Then
            CheckBox9.Enabled = False
        End If
        If k10.Count(Function(w) RichTextBox1.Text.ToLower.Contains(w)) > 0 Then
            CheckBox10.Enabled = False
        End If
        If k11.Count(Function(w) RichTextBox1.Text.ToLower.Contains(w)) > 0 Then
            CheckBox11.Enabled = False
        End If
        If k12.Count(Function(w) RichTextBox1.Text.ToLower.Contains(w)) > 0 Then
            CheckBox12.Enabled = False
        End If
        If k13.Count(Function(w) RichTextBox1.Text.ToLower.Contains(w)) > 0 Then
            CheckBox13.Enabled = False
        End If
        If k14.Count(Function(w) RichTextBox1.Text.ToLower.Contains(w)) > 0 Then
            CheckBox14.Enabled = False
        End If
        If k15.Count(Function(w) RichTextBox1.Text.ToLower.Contains(w)) > 0 Then
            CheckBox15.Enabled = False
        End If
        If k16.Count(Function(w) RichTextBox1.Text.ToLower.Contains(w)) > 0 Then
            CheckBox16.Enabled = False
        End If
    End Sub
    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        Call uncheck()
        Label23.Text = Format(DateTimePicker1.Value, "yyyy-MM-dd")
        Call checkoff()


    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub TextBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox4.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub TextBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub Label23_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label23.TextChanged
        Call Connection()
        
    End Sub


    
End Class
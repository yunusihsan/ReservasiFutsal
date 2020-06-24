Public Class MenuUtama

    Private Sub LogOutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogOutToolStripMenuItem.Click
        Dim choose = MessageBox.Show("Apakah anda yakin ingin Logout ?", "WARNING !", MessageBoxButtons.YesNo)
        If choose = DialogResult.Yes Then
            Me.Hide()
            Login.Show()
            Login.TextBox1.Text = ""
            Login.TextBox2.Text = ""
            Login.TextBox1.Focus()
        End If

    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Dim choose = MessageBox.Show("Anda akan keluar dari sistem !", "WARNING !", MessageBoxButtons.YesNo)
        If choose = DialogResult.Yes Then
            End
        End If
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        STLabel4.Text = TimeOfDay
    End Sub

    Private Sub MenuUtama_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        STLabel6.Text = Today
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Admin.ShowDialog()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Reservasi.ShowDialog()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Lapangan.ShowDialog()
    End Sub
End Class
Imports System.Data.Odbc
Public Class Login

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            Call Button1_Click(e, AcceptButton)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Masukan Username dan Password!", MsgBoxStyle.Critical, "WARNING!")
            TextBox1.Focus()
        Else
            Call Connection()
            Cmd = New OdbcCommand("Select * From admin where id_admin='" & TextBox1.Text & "' and password = '" & TextBox2.Text & "'", Conn)

            Rd = Cmd.ExecuteReader
            Rd.Read()
            If Rd.HasRows Then
                MenuUtama.Show()
                Me.Hide()
                MenuUtama.STLabel2.Text = Rd!Nama_Admin
            Else
                MsgBox("Username atau Password Anda Salah!", MsgBoxStyle.Critical, "WARNING!")
                TextBox1.Clear()
                TextBox2.Clear()
                TextBox1.Focus()
            End If

        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        coba.Show()

    End Sub
End Class

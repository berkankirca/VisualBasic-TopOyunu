Imports System.ComponentModel

Public Class Form1
    Dim kontrol As Boolean 'cismin sağa veya sola gitmesini sağlayan kontrol
    Dim kontrol2 As Boolean 'cismin yukarı aşağı hareketini belirleyen kontrol
    Dim puan As Integer = 0 'oyuna başlangıç puanı
    Dim x As Single = 3 'oyunun başında x ekseninin hızı
    Dim y As Single = 3 'oyunun başında y ekseninin hızı

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        'kontrol cismin sağa veya sola gitmesini sağlayan kontrol
        If kontrol = True Then

            PictureBox1.Left += x
        Else
            PictureBox1.Left -= x
        End If
        '=============================================
        'cisim sola çarparsa sağa doğru yansır==
        If PictureBox1.Left <= Me.ClientRectangle.Left Then
            kontrol = True
        End If
        'cisim sağa çarparsa sola doğru yansır==
        If PictureBox1.Left + PictureBox1.Width >= GroupBox1.Width Then
            kontrol = False
        End If
        '===============================================
        'kontrol2 cismin yukarı aşağı hareketini belirleyen boolean==
        If kontrol2 = True Then
            PictureBox1.Top -= y
        Else
            PictureBox1.Top += y

        End If
        '================================================
        'topun alt üst çarpma kontrolleri
        If PictureBox1.Top <= Me.ClientRectangle.Top Then
            kontrol2 = False
        End If

        'If PictureBox1.Top + PictureBox1.Height >= Me.ClientRectangle.Bottom Then
        '    kontrol2 = True

        'End If
        If PictureBox1.Bounds.IntersectsWith(PictureBox2.Bounds) Then
            kontrol2 = True
        End If
        '======
        'top yere düştüğünde verilen uyarı===
        If PictureBox1.Bottom >= Me.ClientRectangle.Bottom Then
            puan = 0
            Dim cevap As String
            Dim cevap2 As String
            Timer1.Enabled = False
            Timer2.Enabled = False
            x = 3
            y = 3
            'listboxa puanı ekleme ve yeniden oynama sorusu====
            cevap = InputBox("Oyun Bitti!" + " --- " + "Skorunuz:" + " " + CStr(Label2.Text) + "   " + "İsminiz:", "Oyun Bitti!", "İsimsiz")
            ListBox1.Items.Add(cevap + ":  " + CStr(Label2.Text) + "  " + "/" & Now)
            cevap2 = MsgBox("Tekrar Oynamak İster Misiniz?", vbYesNo, "Oyun")
            If cevap2 = vbYes Then
                'oyunu başlat butonu top, puan,hızlandırma sistemi devreye girer====
                Timer1.Enabled = True
                Timer2.Enabled = True
                Me.KeyPreview = True
                PictureBox2.Focus()
                PictureBox1.Location = New Point(Rnd() * 100, Rnd() * 100)
                PictureBox2.Location = New Point(257, 398)
            Else
                Me.Close()
            End If
        End If

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'timer özellikleri==
        Timer1.Enabled = False
        Timer2.Enabled = False
        Timer1.Interval = 10
        Timer2.Interval = 1000
        '====================
        'buton ve label üzerinde yazacak metinler====
        Button2.Text = "Oyunu Başlat"
        Button3.Text = "Oyunu Bitir"
        Label1.Text = "Skor:"
        Label2.Text = "0"
        '=====================================================================
        Me.KeyPreview = False 'oyun başlamadan kontrolleri devre dışı bırakma

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'oyunu başlat butonu top, puan,hızlandırma sistemi devreye girer====
        Timer1.Enabled = True
        Timer2.Enabled = True
        Me.KeyPreview = True
        PictureBox2.Focus()
        PictureBox1.Location = New Point(Rnd() * 100, Rnd() * 100)
        PictureBox2.Location = New Point(257, 398)

    End Sub



    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'oyunu bitir butonu top, puan,hızlandırma sistemi devre dışı kalır===
        Timer1.Enabled = False
        Timer2.Enabled = False
        Me.KeyPreview = False
        puan = 0
        x = 3
        y = 3
        'listboxa puanı ekleme ve yeniden oynama sorusu====
        Dim cevap As String
        Dim cevap2 As String
        cevap = InputBox("Oyun Bitti!" + " --- " + "Skorunuz:" + " " + CStr(Label2.Text) + "   " + "İsminiz:", "Oyun Bitti!", "İsimsiz")
        ListBox1.Items.Add(cevap + ":  " + CStr(Label2.Text) + "  " + "/" & Now)
        cevap2 = MsgBox("Tekrar Oynamak İster Misiniz?", vbYesNo, "Oyun")
        If cevap2 = vbYes Then
            'oyunu başlat butonu top, puan,hızlandırma sistemi devreye girer====
            Timer1.Enabled = True
            Timer2.Enabled = True
            Me.KeyPreview = True
            PictureBox2.Focus()
            PictureBox1.Location = New Point(Rnd() * 100, Rnd() * 100)
            PictureBox2.Location = New Point(257, 398)
        Else
            Me.Close()
        End If


    End Sub
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        'saniyede 2 puan yükselten timer===
        puan = puan + 2
        Label2.Text = puan
        'topun 10 puanda bir hızlanmasını sağlayan timer====
        x = x + 0.1
        y = y + 0.1
    End Sub


    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        'topu sektircek cismin oyundan çıkmadan hareketini sağlamak====
        If e.KeyCode = Keys.Left Then
            If PictureBox2.Left <= GroupBox1.Left Then
            Else PictureBox2.Left -= 20
            End If
        End If
        If e.KeyCode = Keys.Right Then
            If PictureBox2.Left + PictureBox2.Width >= GroupBox1.Width Then
            Else
                PictureBox2.Left += 20
            End If
        End If
    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        'formu kapatma isteğinde sorulacak soru
        Timer1.Enabled = False
        Timer2.Enabled = False
        puan = 0
        Dim cevap3 As String
        cevap3 = MsgBox("Oyundan Çıkmak İstediğinize Emin Misiniz?", vbYesNo, "Oyun")
        If cevap3 = vbYes Then

        Else
            e.Cancel = True

        End If
    End Sub
End Class


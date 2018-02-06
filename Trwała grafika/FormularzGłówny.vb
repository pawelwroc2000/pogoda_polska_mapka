

'#####################################################################################################################
'oblusga grafiki w formie: ver.1.1 - BMP z pamieci kopiowana do picturebox na formie bez użycia graphics ale z kanalem alpha
'#####################################################################################################################
Public Class FormularzGłówny
    ' warstwy bitmap
    Dim main_layer_bmp As New Bitmap(1000, 1000, Imaging.PixelFormat.Format32bppArgb)
    Dim rain_layer_bmp As New Bitmap(100, 100, Imaging.PixelFormat.Format32bppArgb)
    Dim snow_layer_bmp As New Bitmap(100, 100, Imaging.PixelFormat.Format32bppArgb)
    Dim map_layer_bmp As New Bitmap("polska2.jpg")
    ' elementy grafiki
    Dim flake_bmp As New Bitmap("snow_flake.png")
    Dim flake2_bmp As New Bitmap("snow_flake2.png")
    Dim drop1_bmp As New Bitmap("drop1.png")
    Dim sun1_bmp As New Bitmap("sun.png")
    Dim cloud1_bmp As New Bitmap("cloud1.png")
    Dim cloud2_bmp As New Bitmap("cloud2.png")
    Dim cloud3_bmp As New Bitmap("cloud3.png")
    Dim cloud4_bmp As New Bitmap("cloud4.png")
    Dim snow1_bmp As New Bitmap("snow1.png")
    Dim mostlycloudy_bmp As New Bitmap("mostlycloudy.png")
    Dim nightrain_bmp As New Bitmap("nightrain.png")
    Dim aaa_bmp As New Bitmap("aaa.png")
    Dim bbb_bmp As New Bitmap("bbb.png")


    Private Sub FormularzGłówny_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' displayColor = sourceColor×alpha / 255 + backgroundColor×(255 – alpha) / 255 
        ' Add a 50% transparent red pixel over an opaque white pixel:
        ' sourceColor(127,255,0,0) ( Red, 50% transparent)
        ' background Color(255,255,255,255) (Opaque white)

        ' STEPS:
        ' 1.set back layer with map
        For x As Integer = 0 To 999
            For y As Integer = 0 To 999
                main_layer_bmp.SetPixel(CInt(x), CInt(y), Color.FromArgb(255, 255, 255, 255))
            Next
        Next

        For x As Integer = 0 To 600 - 1
            For y As Integer = 0 To 533 - 1
                main_layer_bmp.SetPixel(x, y, map_layer_bmp.GetPixel(x, y))
            Next
        Next

        ' 3.mix both layer and set main layer
        For i As UInteger = 50 To 250 Step 100
            DodajWarstwe(bbb_bmp, i, i, i)
            'DodajWarstwe(flake2_bmp, i, CUInt(i + 380), CUInt(i + 10))
            'DodajWarstwe(drop1_bmp, i, CUInt(i), CUInt(i + 100))
            'DodajWarstwe(sun1_bmp, i, CUInt(i + 50), CUInt(i + 50))
        Next

        For i As UInteger = 50 To 250 Step 50
            'DodajWarstwe(cloud2_bmp, i, CUInt(i + 50), CUInt(i + 50))
        Next

        ' 4.display main layer in picture box
        main_PictureBox.Image = main_layer_bmp
        main_layer_bmp.Save("layers.png", System.Drawing.Imaging.ImageFormat.Png)

    End Sub

    Private Sub DodajWarstwe(ByRef ImageToAdd As Bitmap, ByVal przezroczystosc As Double, ByVal OffsetX As UInteger, ByVal OffsetY As UInteger)
        Dim alpha As Single
        Dim sourceColor As Color
        Dim backColor As Color
        Dim displayColorRed As Double
        Dim displayColorGreen As Double
        Dim displayColorBlue As Double
        Dim x As UInteger
        Dim y As UInteger

        Try
            For x = 0 To CUInt(ImageToAdd.Size.Width - 1)
                For y = 0 To CUInt(ImageToAdd.Size.Height - 1)
                    backColor = main_layer_bmp.GetPixel(CInt(x + OffsetX), CInt(y + OffsetY))
                    sourceColor = ImageToAdd.GetPixel(CInt(x), CInt(y))
                    alpha = CByte((sourceColor.A * przezroczystosc) / 255)
                    displayColorRed = (CInt(sourceColor.R) * alpha / 255) + (backColor.R) * (255 - alpha) / 255
                    displayColorGreen = (CInt(sourceColor.G) * alpha / 255) + (backColor.G) * (255 - alpha) / 255
                    displayColorBlue = (CInt(sourceColor.B) * alpha / 255) + (backColor.B) * (255 - alpha) / 255
                    main_layer_bmp.SetPixel(CInt(x + OffsetX), CInt(y + OffsetY), Color.FromArgb(255, CInt(displayColorRed), CInt(displayColorGreen), CInt(displayColorBlue)))
                Next
            Next
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try



    End Sub

    ' na ruch myszka nad picturebox ustaw pixele w BMP i przypisz BMP do PictureBox1
    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove

        'Try
        '    Dim alpha As Double = (MousePosition.X - Me.Location.X - PictureBox1.Location.X - 18) / 285
        '    Dim color As Color = Drawing.Color.Black

        '    'c = Color.FromArgb(alpha * 255, c.R, c.G, c.B)
        '    color = color.FromArgb(alpha * 255, color.R, color.G, color.B)

        '    BMPS.SetPixel(MousePosition.X - Me.Location.X - PictureBox1.Location.X - 18, MousePosition.Y - Me.Location.Y - PictureBox1.Location.Y - 30, color)
        'Catch ex As Exception
        'End Try
        'PictureBox1.Image = BMPS
    End Sub

    Private Sub SaveToFileButton_Click(sender As Object, e As EventArgs) Handles SaveToFileButton.Click
        ' BMP.Save("bitmapa.png", Imaging.ImageFormat.Png)
        '  Me.PictureBox1.Image.Save("picturebox.png", Imaging.ImageFormat.Png)

        'Dim graph As Graphics = Nothing
        'Dim frmleft As System.Drawing.Point = Me.Bounds.Location
        'Dim img As New Bitmap(Me.Bounds.Width + 0, Me.Bounds.Height + 0)
        'graph = Graphics.FromImage(img)
        'Dim screenx As Integer = frmleft.X
        'Dim screeny As Integer = frmleft.Y
        'graph.CopyFromScreen(screenx - 0, screeny - 0, 0, 0, img.Size)
        'img.Save("filename.png", System.Drawing.Imaging.ImageFormat.Png)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Static Dim i As Byte
        If i < 30 Then
            'PictureBox2.Left = PictureBox2.Left + 10
            'PictureBox2.Top = PictureBox2.Top + 10
            'i = CByte(i + 1)
            'PictureBox2.Refresh()
            'Me.Refresh()
        End If
    End Sub

    Private Function OffsetX() As Integer
        Throw New NotImplementedException
    End Function

End Class






'#####################################################################################################################
'oblusga grafiki w formie: ver.1 - BMP z pamieci kopiowana do picturebox na formie bez użycia graphics
'#####################################################################################################################
'Public Class FormularzGłówny
'    ' utworz bitmape w pamieci
'    Dim BMP As New Bitmap(300, 300, Imaging.PixelFormat.Format24bppRgb)

'    ' na ruch myszka nad picturebox ustaw pixele w BMP i przypisz BMP do PictureBox1
'    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
'        Try
'            BMP.SetPixel(MousePosition.X - Me.Location.X - PictureBox1.Location.X - 18, MousePosition.Y - Me.Location.Y - PictureBox1.Location.Y - 30, Color.White)
'        Catch ex As Exception
'        End Try
'        PictureBox1.Image = BMP
'    End Sub

'    Private Sub FormularzGłówny_Load(sender As Object, e As EventArgs) Handles MyBase.Load

'    End Sub
'End Class


' #####################################################################################################################
' oblusga grafiki w formie: ver.2 - BMP w pamieci jako zmienna, kopiowana do picturebox na formie z użyciem objektu graphics
' #####################################################################################################################
'Public Class FormularzGłówny
'    ' utworz bitmape w pamieci
'    Dim BMP As New Bitmap(300, 300, Imaging.PixelFormat.Format24bppRgb)
'    'stworz obiekt graphics z BMP w pamieci
'    Dim GFX As Graphics = Graphics.FromImage(BMP)


'    Private Sub FormularzGłówny_Load(sender As Object, e As EventArgs) Handles MyBase.Load
'        GFX.Clear(SystemColors.Control)
'    End Sub

'    Private Sub drawButton_Click(sender As Object, e As EventArgs) Handles drawButton.Click
'        GFX.FillRectangle(Brushes.Black, 0, 0, 10, 10)
'    End Sub

'    Private Sub draw2Button_Click(sender As Object, e As EventArgs) Handles draw2Button.Click
'        GFX.FillRectangle(Brushes.Black, 10, 10, 50, 50)
'    End Sub

'    Private Sub LoadBMPtoPictureBox1Button_Click(sender As Object, e As EventArgs) Handles LoadBMPtoPictureBox1Button.Click
'        PictureBox1.Image = BMP
'    End Sub

'    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
'        'BMP.SetPixel(MousePosition.X - Me.Location.X - PictureBox1.Location.X - 4, MousePosition.Y - Me.Location.Y - PictureBox1.Location.Y - 30, Color.White)
'        GFX.DrawString("@#@#@#@#@#@#@#@#@", SystemFonts.DefaultFont, Brushes.Blue, MousePosition.X - Me.Location.X - PictureBox1.Location.X - 18, MousePosition.Y - Me.Location.Y - PictureBox1.Location.Y - 30)
'        PictureBox1.Image = BMP
'    End Sub

'    Private Sub SaveToFileButton_Click(sender As Object, e As EventArgs) Handles SaveToFileButton.Click
'        BMP.Save("bitmapa.bmp", Imaging.ImageFormat.Bmp)
'    End Sub
'End Class



'' #####################################################################################################################
'' oblusga grafiki w formie: ver.3 - malujemy na skojarzonym obiekcie na zadanie lub Paint event , bez BMP w pamieci
'' tworzymy zmienna graphics, nastepnie kojarzymy z danym obiekiem, nastepnie malujemy co chcemy a nastepnie sztucznie
'' trigerujemy Paint event poprzed Me.Invalidate()
'' #####################################################################################################################
'Public Class FormularzGłówny

'    ' stworz wlasny pen
'    Dim pen_one As Pen
'    Dim draw_options As Integer = 0

'    Private Sub FormularzGłówny_Load(sender As Object, e As EventArgs) Handles MyBase.Load

'    End Sub

'    ' rysuj grafike na zadanie przycisku
'    Private Sub drawButton_Click(sender As Object, e As EventArgs) Handles drawButton.Click
'        'Dim objGrafika As Graphics
'        'objGrafika = Me.CreateGraphics()
'        ''objGrafika = txtRysowanie.CreateGraphics()
'        ''objGrafika = PictureBox1.CreateGraphics()
'        '' stworz wlasny pen
'        ''Dim pen_one As New Pen(Color.Black, 10)
'        'objGrafika.DrawLine(Pens.Blue, 0, 0, 100, 100)

'        pen_one = New Pen(Color.Red, 4)
'        draw_options = 1
'        Me.Invalidate()

'    End Sub

'    ' maluj grafike na Paint event formularza
'    Private Sub FormularzGłówny_Paint(sender As Object, e As PaintEventArgs) Handles MyBase.Paint
'        ' malowanie na skojarzonym obiekcie
'        'Dim objGrafika As Graphics
'        'objGrafika = Me.CreateGraphics()
'        'objGrafika = txtRysowanie.CreateGraphics()
'        'objGrafika = PictureBox1.CreateGraphics()

'        ' rysowanie bezposrednio na formie przy pomocy zmiennej e


'        If draw_options = 1 Then
'            'objGrafika.DrawLine(pen_one, 0, 0, 100, 100)
'            e.Graphics.DrawLine(pen_one, 10, 10, 100, 10)
'        End If

'    End Sub
'End Class



' #####################################################################################################################
' oblusga grafiki w formie: ver.4 - BMP z pamieci kopiowana do obszaru rectangle , nie ma uzycia picturebox
' #####################################################################################################################
'Public Class FormularzGłówny

'    ' utworz bitmape w pamieci
'    Dim BMP As New Bitmap(600, 600, Imaging.PixelFormat.Format24bppRgb)
'    'stworz obiekt graphics reprezentujacy utworzona wyzej BMP w pamieci
'    Dim GFX As Graphics = Graphics.FromImage(BMP)

'    Private Sub FormularzGłówny_Load(sender As Object, e As EventArgs) Handles MyBase.Load
'        ' definiujemy prostokat w ktorym bedzie wyswietlana BMP
'        Dim rectProstokąt As New Rectangle(0, 0, 600, 600)
'        GFX.DrawLine(Pens.Black, 0, 0, 50, 50)
'        GFX.DrawRectangle(Pens.Blue, 10, 10, 50, 50)
'        GFX.DrawEllipse(Pens.Orange, rectProstokąt)
'    End Sub

'    Private Sub drawButton_Click(sender As Object, e As EventArgs) Handles drawButton.Click
'        'GFX.FillRectangle(Brushes.Black, 0, 0, 10, 10)
'        Dim objGrafika As Graphics
'        objGrafika = Me.CreateGraphics()
'        'objGrafika = txtRysowanie.CreateGraphics()

'        ' Nie można bezpośrednio modyfikować obiektu e.Graphics.
'        ' objGrafika = e.
'        ' Wyświetl zawartość mapy w formularzu.
'        objGrafika.DrawImage(BMP, 0, 0, BMP.Width, BMP.Height)
'    End Sub

'    Private Sub draw2Button_Click(sender As Object, e As EventArgs) Handles draw2Button.Click
'        GFX.FillRectangle(Brushes.Black, 10, 10, 50, 50)
'    End Sub

'    Private Sub LoadBMPtoPictureBox1Button_Click(sender As Object, e As EventArgs) Handles LoadBMPtoPictureBox1Button.Click
'        PictureBox1.Image = BMP
'    End Sub

'    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
'        'BMP.SetPixel(MousePosition.X - Me.Location.X - PictureBox1.Location.X - 4, MousePosition.Y - Me.Location.Y - PictureBox1.Location.Y - 30, Color.White)
'        GFX.DrawString("XXXXXXXXXX", SystemFonts.DefaultFont, Brushes.Blue, MousePosition.X - Me.Location.X - PictureBox1.Location.X - 18, MousePosition.Y - Me.Location.Y - PictureBox1.Location.Y - 30)
'        PictureBox1.Image = BMP
'    End Sub

'    Private Sub SaveToFileButton_Click(sender As Object, e As EventArgs) Handles SaveToFileButton.Click
'        BMP.Save("bitmapa.bmp", Imaging.ImageFormat.Bmp)
'    End Sub

'    Private Sub FormularzGłówny_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
'        '  Dim objGrafika As Graphics
'        ' Nie można bezpośrednio modyfikować obiektu e.Graphics.
'        '   objGrafika = e.Graphics
'        ' Wyświetl zawartość mapy w formularzu.
'        ' objGrafika.DrawImage(BMP, 0, 0, BMP.Width, BMP.Height)
'    End Sub

'End Class







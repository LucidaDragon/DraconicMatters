Public Class DrawHandler
    Public Shared LiveViewport As Bitmap
    Public Shared Drawables As New List(Of AnimatedSprite)

    Public Shared Sub Init(bmp As Bitmap)
        LiveViewport = bmp.Clone()
    End Sub

    Public Shared Sub Draw()
        LiveViewport = New Bitmap(LiveViewport.Width, LiveViewport.Height)
        For Each sprite In Drawables
            DrawSprite(sprite)
        Next
    End Sub

    Public Shared Sub Register(sprite As AnimatedSprite)
        For i As Integer = 0 To Drawables.Count - 1
            If sprite.CompareTo(Drawables(i)) < 0 Then
                Drawables.Insert(i, sprite)
                Exit Sub
            End If
        Next
        Drawables.Add(sprite)
    End Sub

    Public Shared Sub DrawSprite(sprite As AnimatedSprite)
        DrawBitmap(sprite.Bitmap, sprite.Region, sprite.FixedToScreen, sprite.ScaleToPlayer)
        If sprite.DrawCall IsNot Nothing Then
            sprite.DrawCall.Invoke(LiveViewport)
        End If
    End Sub

    Public Shared Sub DrawBitmap(map As Bitmap, region As Rectangle, Optional fixedToScreen As Boolean = False, Optional matchPlayerSize As Boolean = False)
        If matchPlayerSize Then
            region = New Rectangle(region.X, region.Y, WorldHandler.Current.Player.Width, WorldHandler.Current.Player.Height)
        End If
        Dim source As Bitmap = Resize(map, region.Width, region.Height)
        If Not fixedToScreen Then
            region = New Rectangle(region.X - WorldHandler.Current.Player.XPos, region.Y - WorldHandler.Current.Player.YPos, region.Width, region.Height)
        End If
        For i As Integer = 0 To region.Width - 1
            For j As Integer = 0 To region.Height - 1
                If Between(0, region.X + i, LiveViewport.Width - 1) AndAlso Between(0, region.Y + j, LiveViewport.Height - 1) Then
                    Dim c As Color = source.GetPixel(i, j)
                    If c.A = Byte.MaxValue Then
                        LiveViewport.SetPixel(region.X + i, region.Y + j, c)
                    End If
                End If
            Next
        Next
    End Sub

    Public Shared Function Flip(original As Bitmap) As Bitmap
        Dim result As Bitmap = original.Clone(New Rectangle(0, 0, original.Width, original.Height), Imaging.PixelFormat.Format32bppArgb)
        For i As Integer = 0 To original.Width - 1
            For j As Integer = 0 To original.Height - 1
                result.SetPixel((original.Width - 1) - i, j, original.GetPixel(i, j))
            Next
        Next
        Return result
    End Function

    Public Shared Function Resize(img As Bitmap, width As Integer, height As Integer) As Bitmap
        If img.Width = width And img.Height = height Then
            Return img
        ElseIf width = 0 Or height = 0 Then
            Return img
        End If

        Dim result As New Bitmap(width, height)
        Dim factorX As Double = width / img.Width
        Dim factorY As Double = height / img.Height
        'Dim imgData As Imaging.BitmapData = img.LockBits(New Rectangle(0, 0, img.Width, img.Height), Imaging.ImageLockMode.ReadOnly, img.PixelFormat)
        'Dim resultData As Imaging.BitmapData = result.LockBits(New Rectangle(0, 0, width, height), Imaging.ImageLockMode.WriteOnly, result.PixelFormat)
        Dim c As Color = Nothing
        For i As Integer = 0 To width - 1
            For j As Integer = 0 To height - 1
                c = img.GetPixel(i \ factorX, j \ factorY)
                result.SetPixel(i, j, c)
            Next
        Next
        'img.UnlockBits(imgData)
        'result.UnlockBits(resultData)
        Return result
    End Function

    Private Shared Function Between(a, v, b) As Boolean
        Return a < v And b > v
    End Function
End Class

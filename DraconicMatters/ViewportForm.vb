Public Class ViewportForm
    Public Viewport As New Bitmap(512, 512)
    Public ThreadWatcher As New ThreadWatcher
    Public State As ViewportState = ViewportState.MainMenu
    Private Paused As Boolean = True

    Public Enum ViewportState
        MainMenu = 0
        Level = 1
        Pause = 2
    End Enum

    Private Sub DrawTimer_Tick(sender As Object, e As EventArgs) Handles DrawTimer.Tick
        DrawTimer.Interval = Math.Min(Math.Max(TickHandler.GlobalTick(Paused), 33), 100)
        DrawHandler.Draw()
        ViewportControl1.Invalidate()
    End Sub

    Private Sub ViewportForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ThreadWatcher.Assign()
        WorldHandler.Init({Me, ViewportControl1}, 0, 0, 64, 64)
        DrawHandler.Init(Viewport)

        Dim mountains As New AnimatedSprite With {
            .Speed = 0,
            .FixedToScreen = False
        }
        mountains.FromImage(My.Resources.Mountains)
        mountains.Region = New Rectangle(0, 0, Viewport.Width, Viewport.Height)


        Dim playerSprite As New AnimatedSprite With {
            .FixedToScreen = True,
            .ScaleToPlayer = True,
            .Region = New Rectangle((Viewport.Width / 2) - (WorldHandler.Current.Player.Width / 2), (Viewport.Height / 2) - (WorldHandler.Current.Player.Height / 2), 0, 0),
            .PlayerInvertX = True
        }
        playerSprite.FromZip(My.Resources.Steph)
        playerSprite.Speed = 2
        playerSprite.RotateToPlayer = True

        Paused = False
        Size = New Size(600, 600)
        DrawTimer.Start()
    End Sub

    Private Sub ViewportContrl_MouseClick(sender As Object, e As MouseEventArgs) Handles ViewportControl1.MouseClick
        Paused = False
    End Sub

    Private Sub ViewportPanel_Paint(sender As Object, e As PaintEventArgs) Handles ViewportControl1.Paint
        e.Graphics.InterpolationMode = Drawing2D.InterpolationMode.NearestNeighbor
        If ViewportControl1.Height < ViewportControl1.Width Then
            'e.Graphics.FillRectangle(Brushes.White, New Rectangle((ViewportControl1.Width / 2) - (ViewportControl1.Height / 2), 0, ViewportControl1.Height, ViewportControl1.Height))
            e.Graphics.DrawImage(DrawHandler.LiveViewport, New Rectangle((ViewportControl1.Width / 2) - (ViewportControl1.Height / 2), 0, ViewportControl1.Height, ViewportControl1.Height))
        Else
            'e.Graphics.FillRectangle(Brushes.White, New Rectangle(0, (ViewportControl1.Height / 2) - (ViewportControl1.Width / 2), ViewportControl1.Width, ViewportControl1.Width))
            e.Graphics.DrawImage(DrawHandler.LiveViewport, New Rectangle(0, (ViewportControl1.Height / 2) - (ViewportControl1.Width / 2), ViewportControl1.Width, ViewportControl1.Width))
        End If
    End Sub

    Private Sub EngineSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EngineSettingsToolStripMenuItem.Click
        EngineSettingsForm.Show()
    End Sub
End Class

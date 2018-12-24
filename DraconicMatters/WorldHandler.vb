Public Class WorldHandler
    Implements ITickable

    Public Shared Property Current As New WorldHandler
    Private Shared XmlHost As New Xml.Serialization.XmlSerializer(GetType(WorldHandler))

    Public Shared Sub Init(inputSources() As Control, px As Integer, py As Integer, pwidth As Integer, pheight As Integer)
        Current.Player = New PlayerBounds With {
            .XPos = px,
            .YPos = py,
            .Width = pwidth,
            .Height = pheight
        }
        For Each inputSource In inputSources
            AddHandler inputSource.KeyDown, AddressOf Current.KeyDown
            AddHandler inputSource.KeyUp, AddressOf Current.KeyUp
        Next
    End Sub

    Public Shared Sub FromXml(xml As String)
        Current = XmlHost.Deserialize(New IO.StringReader(xml))
    End Sub

    Public Property Player As PlayerBounds
    Public Property PlayerForward As Boolean = True
    Public ReadOnly Property PlayerOverlapping As Boolean
        Get
            Dim rect As Rectangle = Player
            For Each obj In StaticObjects
                If obj.IntersectsWith(rect) Then
                    Return True
                End If
            Next
            Return False
        End Get
    End Property

    Public Property StaticObjects As New List(Of Rectangle)

    Public Property UpKeys As New List(Of Keys) From {
        Keys.Up,
        Keys.W
    }
    Public Property DownKeys As New List(Of Keys) From {
        Keys.Down,
        Keys.S
    }
    Public Property LeftKeys As New List(Of Keys) From {
        Keys.Left,
        Keys.A
    }
    Public Property RightKeys As New List(Of Keys) From {
        Keys.Right,
        Keys.D
    }

    Public WriteOnly Property AllRate As Double
        Set(value As Double)
            UpRate = value
            DownRate = value
            LeftRate = value
            RightRate = value
        End Set
    End Property
    Public Property UpRate As Double = 50
    Public Property DownRate As Double = 50
    Public Property LeftRate As Double = 50
    Public Property RightRate As Double = 50

    Public Property PressedKeys As New List(Of Keys)

    Public Property TickableWhenPaused As Boolean = False Implements ITickable.TickableWhenPaused

    Sub New()
        TickHandler.Register(Me)
    End Sub

    Public Sub Input(value As Keys, deltaTimeMS As Double)
        deltaTimeMS /= 1000
        If UpKeys.Contains(value) Then
            Dim old As Double = Player.YPos
            Player.YPos -= UpRate * deltaTimeMS
            If PlayerOverlapping Then
                Player.YPos = old
            End If
        ElseIf DownKeys.Contains(value) Then
            Dim old As Double = Player.YPos
            Player.YPos += DownRate * deltaTimeMS
            If PlayerOverlapping Then
                Player.YPos = old
            End If
        ElseIf LeftKeys.Contains(value) Then
            Dim old As Double = Player.XPos
            Player.XPos -= LeftRate * deltaTimeMS
            If PlayerOverlapping Then
                Player.XPos = old
            End If
            PlayerForward = False
        ElseIf RightKeys.Contains(value) Then
            Dim old As Double = Player.XPos
            Player.XPos += RightRate * deltaTimeMS
            If PlayerOverlapping Then
                Player.XPos = old
            End If
            PlayerForward = True
        End If
    End Sub

    Public Sub Tick(deltaTimeMS As Double) Implements ITickable.Tick
        For Each key In PressedKeys
            Input(key, deltaTimeMS)
        Next
    End Sub

    Public Sub KeyDown(sender As Object, e As KeyEventArgs)
        If Not PressedKeys.Contains(e.KeyCode) Then
            PressedKeys.Add(e.KeyCode)
        End If
    End Sub

    Public Sub KeyUp(sender As Object, e As KeyEventArgs)
        PressedKeys.Remove(e.KeyCode)
    End Sub
End Class

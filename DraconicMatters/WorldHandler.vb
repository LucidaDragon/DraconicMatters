Public Class WorldHandler
    Implements ITickable

    Public Shared Property Current As New WorldHandler
    Private Shared XmlHost As New Xml.Serialization.XmlSerializer(GetType(WorldHandler))

    Public Shared Sub Init(px As Integer, py As Integer, pwidth As Integer, pheight As Integer)
        Current.Player = New PlayerBounds With {
            .XPos = px,
            .YPos = py,
            .Width = pwidth,
            .Height = pheight
        }
    End Sub

    Public Shared Sub FromXml(xml As String)
        Current = XmlHost.Deserialize(New IO.StringReader(xml))
    End Sub

    Public Property Player As PlayerBounds
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
    Public Property UpRate As Double = 1
    Public Property DownRate As Double = 1
    Public Property LeftRate As Double = 1
    Public Property RightRate As Double = 1

    Public Property PressedKeys As New List(Of Keys)

    Public Property TickableWhenPaused As Boolean = False Implements ITickable.TickableWhenPaused

    Public Sub Input(value As Keys, deltaTimeMS As Double)
        deltaTimeMS /= 1000
        If UpKeys.Contains(value) Then
            Player.YPos -= UpRate * deltaTimeMS
        ElseIf DownKeys.Contains(value) Then
            Player.YPos += DownRate * deltaTimeMS
        ElseIf LeftKeys.Contains(value) Then
            Player.XPos -= LeftRate * deltaTimeMS
        ElseIf RightKeys.Contains(value) Then
            Player.XPos += RightRate * deltaTimeMS
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

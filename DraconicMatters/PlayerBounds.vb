Public Class PlayerBounds
    Public Property XPos As Double
    Public Property YPos As Double
    Public Property Width As Integer
    Public Property Height As Integer

    Public Shared Widening Operator CType(bound As PlayerBounds) As Rectangle
        Return New Rectangle(bound.XPos, bound.YPos, bound.Width, bound.Height)
    End Operator
End Class

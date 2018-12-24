Public Class TickHandler
    Private Shared LastTick As Date = Date.Now
    Private Shared LastGarbageCollect As Date = Date.Now
    Private Shared Tickables As New List(Of ITickable)

    Public Shared Function GlobalTick(paused As Boolean) As Double
        Dim ms As Double = (Date.Now - LastTick).TotalMilliseconds
        For Each tickable In Tickables
            If tickable.TickableWhenPaused Or Not paused Then
                tickable.Tick(ms)
            End If
        Next
        LastTick = Date.Now
        If (Date.Now - LastGarbageCollect).TotalMilliseconds > 1000 Then
            GC.Collect()
            LastGarbageCollect = Date.Now
        End If
        Return ms
    End Function

    Public Shared Sub Register(tickable As ITickable)
        Tickables.Add(tickable)
    End Sub
End Class

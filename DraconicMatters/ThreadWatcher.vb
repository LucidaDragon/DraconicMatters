''' <summary>
''' Calculates CPU usage of individula threads.
''' </summary>
Public Class ThreadWatcher
    ''' <summary>
    ''' The current CPU usage.
    ''' </summary>
    Public ReadOnly Property CpuUsage As Double
        Get
            Return UsageValue
        End Get
    End Property

    Private MyThread As ProcessThread
    Private LastTick As Date = Now
    Private LastValue As Double = 0
    Private UsageValue As Double = 0

    ''' <summary>
    ''' Assign this thread watcher to the current thread.
    ''' </summary>
    Public Sub Assign()
        For Each procThread As ProcessThread In Process.GetCurrentProcess().Threads
            If procThread.Id = GetCurrentWin32ThreadId() Then
                MyThread = procThread
                Exit Sub
            End If
        Next
        Throw New KeyNotFoundException("Current thread could not be found.")
    End Sub

    ''' <summary>
    ''' Update this thread watcher.
    ''' </summary>
    Public Sub Tick()
        If MyThread IsNot Nothing And Not (Now - LastTick).TotalMilliseconds < 500 Then
            Dim time As Date = Now
            Dim value As Double = MyThread.TotalProcessorTime.Milliseconds
            If (value > LastValue) Or (value = LastValue) Then
                UsageValue = Math.Min((value - LastValue) / ((time - LastTick).TotalMilliseconds / 100), 100)
            End If
            LastTick = time
            LastValue = value
        End If
    End Sub

    ''' <summary>
    ''' Calls to Kernel32 to get the current thread ID.
    ''' </summary>
    <Runtime.InteropServices.DllImport("Kernel32", EntryPoint:="GetCurrentThreadId", ExactSpelling:=True)>
    Public Shared Function GetCurrentWin32ThreadId() As Integer
    End Function
End Class

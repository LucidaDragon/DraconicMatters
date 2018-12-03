Imports System.IO.Compression

Public Class AnimatedSprite
    Implements ITickable
    Implements IComparable(Of AnimatedSprite)

    Public ReadOnly Property Bitmap As Bitmap
        Get
            If Frames.Count > 0 Then
                Return Frames(Math.Floor(TimePos))
            Else
                Return Nothing
            End If
        End Get
    End Property
    Public ReadOnly Property Time As Integer
        Get
            Return TimePos
        End Get
    End Property
    Public Property TickableWhenPaused As Boolean = False Implements ITickable.TickableWhenPaused
    Public Property Speed As Double = 1
    Public Property Frames As New List(Of SerializedBitmap)
    Public Property SpriteZ As Integer = 0
    Public Property Region As New Rectangle
    Public Property DrawCall As Action(Of Bitmap)

    Private TimePos As Double

    Sub New()
        TickHandler.Register(Me)
        DrawHandler.Register(Me)
    End Sub

    Public Sub FromImage(bmp As Bitmap)
        Frames.Add(bmp)
    End Sub

    Public Sub FromArray(maps As List(Of SerializedBitmap))
        Frames.AddRange(maps)
    End Sub

    Public Sub FromZip(data As Byte())
        Dim zip As New ZipArchive(New IO.MemoryStream(data))
        Dim result As New List(Of SerializedBitmap)
        For Each entry In SortEntries(zip.Entries.ToList())
            result.Add(Bitmap.FromStream(entry.Open()))
        Next
        Frames.AddRange(result)
    End Sub

    Private Function SortEntries(unsorted As List(Of ZipArchiveEntry)) As List(Of ZipArchiveEntry)
        Dim less As List(Of ZipArchiveEntry) = Nothing
        Dim equal As List(Of ZipArchiveEntry) = Nothing
        Dim greater As List(Of ZipArchiveEntry) = Nothing
        If unsorted Is Nothing Then
            unsorted = New List(Of ZipArchiveEntry)
        End If

        If unsorted.Count > 1 Then
            Dim pivot As ZipArchiveEntry = unsorted.First
            For Each elem In unsorted
                If elem.Name < pivot.Name Then
                    If less Is Nothing Then
                        less = New List(Of ZipArchiveEntry)
                    End If
                    less.Add(elem)
                End If
                If elem.Name = pivot.Name Then
                    If equal Is Nothing Then
                        equal = New List(Of ZipArchiveEntry)
                    End If
                    equal.Add(elem)
                End If
                If elem.Name > pivot.Name Then
                    If greater Is Nothing Then
                        greater = New List(Of ZipArchiveEntry)
                    End If
                    greater.Add(elem)
                End If
            Next
            Dim result As List(Of ZipArchiveEntry) = SortEntries(less)
            result.AddRange(SortEntries(equal))
            result.AddRange(SortEntries(greater))
            Return result
        Else
            Return unsorted
        End If
    End Function

    Public Sub Tick(deltaTimeMS As Double) Implements ITickable.Tick
        TimePos += (deltaTimeMS / 1000) * Speed
        While TimePos >= Frames.Count
            TimePos -= Frames.Count
        End While
        While TimePos < 0
            TimePos += Frames.Count
        End While
    End Sub

    Public Function CompareTo(other As AnimatedSprite) As Integer Implements IComparable(Of AnimatedSprite).CompareTo
        Return SpriteZ.CompareTo(other.SpriteZ)
    End Function
End Class

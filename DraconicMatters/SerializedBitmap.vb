Public Class SerializedBitmap
    <Xml.Serialization.XmlIgnore>
    Public Property Bitmap As Bitmap
    Public Property Data As String
        Get
            Dim mem As New IO.MemoryStream
            Bitmap.Save(mem, Imaging.ImageFormat.Png)
            Return Convert.ToBase64String(mem.ToArray())
        End Get
        Set(value As String)
            Bitmap = New Bitmap(New IO.MemoryStream(Convert.FromBase64String(value)))
        End Set
    End Property

    Public Shared Widening Operator CType(map As Bitmap) As SerializedBitmap
        Return New SerializedBitmap With {
            .Bitmap = map
        }
    End Operator

    Public Shared Widening Operator CType(map As SerializedBitmap) As Bitmap
        Return map.Bitmap
    End Operator
End Class

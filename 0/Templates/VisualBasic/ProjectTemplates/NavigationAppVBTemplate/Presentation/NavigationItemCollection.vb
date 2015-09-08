Namespace Presentation
    Public Class NavigationItemCollection
        Inherits ObservableCollection(Of NavigationItem)

        Private _parent As NavigationItem

        Public Sub New(parent As NavigationItem)
            _parent = parent
        End Sub

        Protected Overrides Sub ClearItems()
            Dim copy As NavigationItem() = Items.ToArray()

            MyBase.ClearItems()

            For Each i In copy
                i.Parent = Nothing
            Next
        End Sub

        Protected Overrides Sub InsertItem(index As Integer, item As NavigationItem)
            VerifyNewItem(item)

            MyBase.InsertItem(index, item)

            item.Parent = _parent
        End Sub

        Protected Overrides Sub RemoveItem(index As Integer)
            Dim i As NavigationItem = Items.Item(index)
            MyBase.RemoveItem(index)
            i.Parent = Nothing
        End Sub

        Protected Overrides Sub SetItem(index As Integer, item As NavigationItem)
            VerifyNewItem(item)
            Dim i As NavigationItem = Items.Item(index)
            MyBase.SetItem(index, item)

            i.Parent = Nothing
            item.Parent = _parent
        End Sub

        Private Sub VerifyNewItem(item As NavigationItem)
            If item Is Nothing Then
                Throw New ArgumentNullException(NameOf(item))
            End If

            If Not item.Parent Is Nothing Then
                Throw New InvalidOperationException("ItemAlreadyInCollection")
            End If
        End Sub
    End Class
End Namespace



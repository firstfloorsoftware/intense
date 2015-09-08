Namespace Presentation
    Module NavigationItemExtensions

        ''' <summary>
        ''' Returns a collection of items that contain specified item, and the ancestors of specified item.
        ''' </summary>
        ''' <param name="item"></param>
        ''' <returns></returns>
        <Extension>
        Public Function GetAncestorsAndSelf(item As NavigationItem) As IEnumerable(Of NavigationItem)
            If item Is Nothing Then Throw New ArgumentException(NameOf(item))

            Dim result As List(Of NavigationItem) = New List(Of NavigationItem)

            While True
                If item Is Nothing Then Exit While
                result.Add(item)
                item = item.Parent
            End While

            Return result
        End Function

        ''' <summary>
        ''' Determines whether specified item is the root item, that is it has no parent.
        ''' </summary>
        ''' <param name="item"></param>
        ''' <returns></returns>
        <Extension>
        Public Function IsRoot(item As NavigationItem) As Boolean
            If item Is Nothing Then Throw New ArgumentException(NameOf(item))

            Return item.Parent Is Nothing
        End Function

        ''' <summary>
        ''' Determines whether the item is a leaf item, meaning it has no children.
        ''' </summary>
        ''' <param name="item"></param>
        ''' <returns></returns>
        <Extension>
        Public Function IsLeaf(item As NavigationItem) As Boolean
            If item Is Nothing Then Throw New ArgumentException(NameOf(item))

            Return Not item.Items.Any()
        End Function

        ''' <summary>
        ''' Determines whether the item has grandchildren.
        ''' </summary>
        ''' <param name="item"></param>
        ''' <returns></returns>
        <Extension>
        Public Function HasGrandchildren(item As NavigationItem) As Boolean
            If item Is Nothing Then Throw New ArgumentException(NameOf(item))

            Return item.Items.Any(Function(i) Not i.IsLeaf())
        End Function

    End Module
End Namespace


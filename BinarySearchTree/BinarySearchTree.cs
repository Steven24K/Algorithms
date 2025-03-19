public interface IBST<T> where T : IComparable<T>
{
    void Insert(T value);
    bool Remove(T value);
    bool Contains(T value);
    string PreOrderTraversal();
    string InOrderTraversal();
    string PostOrderTraversal();

    //can be 
    List<T>? Traversal(TraversalOrder traversalOrder);
    // Other methods or properties as needed
}
public enum TraversalOrder { PreOrder, InOrder, PostOrder };

public class TreeNode<T> where T : IComparable<T>
{
    public T Value { get; set; }

    public TreeNode<T>? Left { get; set; }
    public TreeNode<T>? Right { get; set; }

    public TreeNode<T>? Parent { get; set; } // is there to make life easy while deleting only.

    public TreeNode(T value, TreeNode<T>? parent = null, TreeNode<T>? left = null, TreeNode<T>? right = null)
    {
        Value = value;
        Left = left;
        Right = right;
        Parent = parent;
    }
}

public class BST<T> : IBST<T> where T : IComparable<T>
{
    public TreeNode<T>? Root { get; set; }

    public void Insert(T value) => Insert(value, Root);
    public void InsertIterative(T value)
    {
        if (Root == null)
        {
            Root = new TreeNode<T>(value); return;
        }
        else
        {
            var node = Root;
            while (true)
            {
                if (value.CompareTo(node.Value) > 0) // right child
                {
                    if (node.Right == null)
                    {
                        node.Right = new TreeNode<T>(value, node);
                        break;
                    }
                    else
                        node = node.Right;

                }
                else if (value.CompareTo(node.Value) < 0) //left child
                {
                    if (node.Left == null)
                    {
                        node.Left = new TreeNode<T>(value, node);
                        break;
                    }
                    else
                        node = node.Left;

                }
                else break;
            }
        }
    }
    private void Insert(T value, TreeNode<T>? node)
    {
        if (Root == null)
        {
            Root = new TreeNode<T>(value); return;
        }
        else
        {
            if (value.CompareTo(node.Value) > 0) // right child
            {
                if (node.Right == null)
                {
                    node.Right = new TreeNode<T>(value, node);
                }
                else
                {
                    Insert(value, node.Right);
                }
            }
            else if (value.CompareTo(node.Value) < 0) //left child
            {
                if (node.Left == null)
                {
                    node.Left = new TreeNode<T>(value, node);
                }
                else
                {
                    Insert(value, node.Left);
                }
            }
        }
    }

    #region Traversal
    public string PostOrderTraversal() => PostOrderTraversal(Root);
    private string PreOrderTraversal(TreeNode<T>? currNode)
    {
        if (currNode == null)
            return "";

        string s = currNode.Value.ToString() + " ";
        s += PreOrderTraversal(currNode.Left);
        s += PreOrderTraversal(currNode.Right);

        return s;
    }

    public string InOrderTraversal() => InOrderTraversal(Root);
    private string InOrderTraversal(TreeNode<T>? currNode)
    {
        if (currNode == null)
            return "";

        string s = "";
        s += InOrderTraversal(currNode.Left);
        s += currNode.Value.ToString() + " ";
        s += InOrderTraversal(currNode.Right);

        return s;
    }


    public string PreOrderTraversal() => PreOrderTraversal(Root);
    private string PostOrderTraversal(TreeNode<T>? currNode)
    {
        if (currNode == null)
            return "";

        string s = "";
        s += PostOrderTraversal(currNode.Left);
        s += PostOrderTraversal(currNode.Right);
        s += currNode.Value.ToString() + " ";

        return s;
    }
    #endregion

    public bool Contains(T value) => Search(Root, value) == null ? false : true;

    private TreeNode<T> Search(TreeNode<T>? node, T value)
    {
        if (node == null) // node does not exist
            return null;

        if (value.CompareTo(node.Value) == 0) // value in the node is the same we are looking for
            return node;

        if (value.CompareTo(node.Value) > 0) // value in the node is smaller than the one we are looking for
            return Search(node.Right, value);

        return Search(node.Left, value);
    }

    #region  Remove Delete
    public bool Remove(T value) => DeleteValue(Root, value);

    //Bad code fix it next year
    private bool DeleteValue(TreeNode<T>? node, T value)
    {
        if (node == null) return false;
        // special case if the value to delete is in the root (and the root has 0 children or 1 child)
        if (value.CompareTo(node.Value) == 0)
        {
            // there are no children:
            if (node.Left == null && node.Right == null)
            {
                node = null;
                return true;
            }
            // there is only left child, the right does not exist
            else if (node.Left != null && node.Right == null)
            {
                node = node.Left;
                node.Parent = null;
                return true;
            }
            // there is only right child, the left does not exist
            else if (node.Left == null && node.Right != null)
            {
                node = node.Right;
                node.Parent = null;
                return true;
            }
        }

        // all other cases. Find first the node corresponding to the value we want to delete
        TreeNode<T> nodeToDelete = Search(node, value);
        // actually perform the deletion
        return delete(node, nodeToDelete);
    }

    private bool delete(TreeNode<T>? node, TreeNode<T> nodeToDelete)
    {

        // CASE 1 : LEAF
        if (nodeToDelete.Left == null && nodeToDelete.Right == null)
        {
            var parent = nodeToDelete.Parent;

            if (isLeft(nodeToDelete, parent))
                parent.Left = null;
            else
                parent.Right = null;

            return true;
        }

        // CASE 2 : ONE CHILD
        if (nodeToDelete.Left == null || nodeToDelete.Right == null)
        {
            var parent = nodeToDelete.Parent;

            if (nodeToDelete.Left != null)
            {
                if (isLeft(nodeToDelete, parent))
                    parent.Left = nodeToDelete.Left;
                else
                    parent.Right = nodeToDelete.Left;
                nodeToDelete.Left.Parent = parent;
            }
            else
            {
                if (isLeft(nodeToDelete, parent))
                    parent.Left = nodeToDelete.Right;
                else
                    parent.Right = nodeToDelete.Right;
                nodeToDelete.Right.Parent = parent;
            }

            return true;
        }

        // CASE 3 : TWO CHILDREN

        // find inordersucc == smallest element of right subtree
        var inOrdSucc = findInOrderSucc(nodeToDelete);

        // copy value to nodeToDelete
        nodeToDelete.Value = inOrdSucc.Value;

        // call recursively delete on inordersucc 
        return delete(node, inOrdSucc);

    }

    // this methods finds the in order successor of the node given as parameter
    private TreeNode<T> findInOrderSucc(TreeNode<T> node)
    {
        var currNode = node.Right;
        while (currNode != null && currNode.Left != null)
            currNode = currNode.Left;

        return currNode;
    }

    // this methods checks if the node given as first parameter is the left child of the node given as second parameter ("parent"). Remember to do a comparison based on the values of the nodes.
    private bool isLeft(TreeNode<T> node, TreeNode<T> parent)
    {
        return parent.Left != null && parent.Left.Value.CompareTo(node.Value) == 0;
    }

    public List<T> Traversal(TraversalOrder traversalOrder)
    {
        throw new NotImplementedException();
    }
    #endregion


}

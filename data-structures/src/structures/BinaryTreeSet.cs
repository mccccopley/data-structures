using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep.structures
{
    public class BinaryTreeSet<ElementType> : ISet<ElementType> where ElementType : IComparable<ElementType>
    {
        public BinaryTreeSet()
        {
            rootNode = new TreeNode<ElementType>();
        }

        public void Output()
        {
            StringBuilder stringBuilder = new StringBuilder();

            this.InOrderTraversal((element) =>
                {
                    stringBuilder.Append(element.ToString());
                    stringBuilder.Append(' ');
                });

            Console.WriteLine(stringBuilder.ToString());

            stringBuilder = null;
        }

        public void Serialize(string filename)
        {
            // use pre-order traversal to store out the tere structure to a file

            using (StreamWriter streamWriter = new StreamWriter(filename))
            {
                this.PreOrderTraversal((element) =>
                    {
                        streamWriter.WriteLine(element.ToString());
                    });
            }
        }

        public void Deserialize(string filename)
        {
            using(StreamReader streamReader = new StreamReader(filename))
            {
                this.Clear();

                string line = streamReader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    ElementType elementValue = (ElementType)TypeDescriptor.GetConverter(typeof(ElementType)).ConvertFromString(line);
                    this.AddElement(elementValue);
                    line = streamReader.ReadLine();
                }
            }
        }

        public void InOrderTraversal(System.Action<ElementType> visitDelegate)
        {
            TreeNode<ElementType> rootNode = this.GetTopMostNode();

            if (rootNode == null)
            {
                return;
            }

            Stack<TreeNode<ElementType>> visitationStack = new Stack<TreeNode<ElementType>>();
            TreeNode<ElementType> currNode = rootNode;

            while (currNode != null || visitationStack.Count > 0)
            {
                if (currNode != null)
                {
                    do
                    {
                        visitationStack.Push(currNode);
                        currNode = currNode.leftChildNode;
                    }
                    while (currNode != null);
                }
                else
                {
                    TreeNode<ElementType> node = visitationStack.Pop();
                    visitDelegate(node.elementValue);

                    currNode = node.rightChildNode;
                }
            }

            visitationStack = null;
        }

        public void PreOrderTraversal(System.Action<ElementType> visitDelegate)
        {
            TreeNode<ElementType> rootNode = this.GetTopMostNode();

            if (rootNode == null)
            {
                return;
            }

            Stack<TreeNode<ElementType>> visitationStack = new Stack<TreeNode<ElementType>>();
            visitationStack.Push(rootNode);

            while (visitationStack.Count > 0)
            {
                TreeNode<ElementType> node = visitationStack.Pop();
                visitDelegate(node.elementValue);

                if (node.rightChildNode != null)
                {
                    visitationStack.Push(node.rightChildNode);
                }

                if (node.leftChildNode != null)
                {
                    visitationStack.Push(node.leftChildNode);
                }
            }

            visitationStack = null;
        }

        public void AddElement(ElementType element)
        {
            TreeNode<ElementType> topMostNode = GetTopMostNode();

            if (topMostNode == null)
            {
                topMostNode = new TreeNode<ElementType>();
                topMostNode.elementValue = element;
                topMostNode.parentNode = rootNode;
                rootNode.leftChildNode = topMostNode;
            }
            else
            {
                TreeNode<ElementType> currNode = topMostNode;

                while (currNode != null)
                {
                    int comparisonResult = currNode.elementValue.CompareTo(element);

                    if (comparisonResult == 0)
                    {
                        return;
                    }
                    else if (comparisonResult < 0)
                    {
                        if (currNode.rightChildNode != null)
                        {
                            currNode = currNode.rightChildNode;
                        }
                        else
                        {
                            currNode.rightChildNode = new TreeNode<ElementType>();
                            currNode.rightChildNode.elementValue = element;
                            currNode.rightChildNode.parentNode = currNode;
                            return;
                        }
                    }
                    else
                    {
                        if (currNode.leftChildNode != null)
                        {
                            currNode = currNode.leftChildNode;
                        }
                        else
                        {
                            currNode.leftChildNode = new TreeNode<ElementType>();
                            currNode.leftChildNode.elementValue = element;
                            currNode.leftChildNode.parentNode = currNode;
                        }
                    }
                }
            }
        }

        public void RemoveElement(ElementType element)
        {
            TreeNode<ElementType> elementNode = FindNode(element);

            if (elementNode == null)
            {
                return;
            }

            if (elementNode.leftChildNode == null && elementNode.rightChildNode == null)
            {
                if (elementNode.parentNode.leftChildNode == elementNode)
                {
                    elementNode.parentNode.leftChildNode = null;
                }
                else
                {
                    elementNode.parentNode.rightChildNode = null;
                }
            }
            else if (elementNode.leftChildNode == null)
            {
                if (elementNode.parentNode.leftChildNode == elementNode)
                {
                    elementNode.parentNode.leftChildNode = elementNode.rightChildNode;
                }
                else
                {
                    elementNode.parentNode.rightChildNode = elementNode.rightChildNode;
                }
            }
            else if (elementNode.rightChildNode == null)
            {
                if (elementNode.parentNode.leftChildNode == elementNode)
                {
                    elementNode.parentNode.leftChildNode = elementNode.leftChildNode;
                }
                else
                {
                    elementNode.parentNode.rightChildNode = elementNode.leftChildNode;
                }
            }
            else
            {
                // 1. take right node tree
                // 2. hang it off the right-most node off the left node tree
                // 3. make the left node tree the replacement

                TreeNode<ElementType> rightNode = elementNode.rightChildNode;
                TreeNode<ElementType> leftNode = elementNode.leftChildNode;
                TreeNode<ElementType> rightMostNodeOffLeft = FindRightmostNode(leftNode);

                rightNode.parentNode = rightMostNodeOffLeft;
                rightMostNodeOffLeft.rightChildNode = rightNode;

                if (elementNode.parentNode.leftChildNode == elementNode)
                {
                    elementNode.parentNode.leftChildNode = leftNode;
                }
                else
                {
                    elementNode.parentNode.rightChildNode = leftNode;
                }
            }
        }

        public void Clear()
        {
            rootNode.leftChildNode = null;
            rootNode.rightChildNode = null;
        }

        public bool HasElement(ElementType element)
        {
            TreeNode<ElementType> elementNode = FindNode(element);
            return elementNode != null;
        }

        public void GetSortedElements(IList<ElementType> orderedList)
        {
            orderedList.Clear();

            TreeNode<ElementType> topMostNode = GetTopMostNode();
            TreeNode<ElementType> currNode = topMostNode;

            Stack<TreeNode<ElementType>> stack = new Stack<TreeNode<ElementType>>();

            while (currNode != null || stack.Count > 0)
            {
                if (currNode != null)
                {
                    stack.Push(currNode);
                    currNode = currNode.leftChildNode;
                }
                else
                {
                    currNode = stack.Pop();
                    orderedList.Add(currNode.elementValue);
                    currNode = currNode.rightChildNode;
                }
            }
        }

        private TreeNode<ElementType> GetTopMostNode()
        {
            return rootNode.leftChildNode;
        }

        private TreeNode<ElementType> FindNode(ElementType element)
        {
            TreeNode<ElementType> topMostNode = GetTopMostNode();
            TreeNode<ElementType> currNode = topMostNode;
            
            while (currNode != null)
            {
                int comparisonResult = currNode.elementValue.CompareTo(element);

                if (comparisonResult == 0)
                {
                    return currNode;
                }
                else if (comparisonResult < 0)
                {
                    currNode = currNode.rightChildNode;
                }
                else
                {
                    currNode = currNode.leftChildNode;
                }
            }

            return null;
        }

        private TreeNode<ElementType> FindLeftmostNode(TreeNode<ElementType> rootNode)
        {
            TreeNode<ElementType> currNode = rootNode;

            while (currNode != null)
            {
                if (currNode.leftChildNode == null)
                {
                    return currNode;
                }

                currNode = currNode.leftChildNode;
            }

            return null;
        }

        private TreeNode<ElementType> FindRightmostNode(TreeNode<ElementType> rootNode)
        {
            TreeNode<ElementType> currNode = rootNode;

            while (currNode != null)
            {
                if (currNode.rightChildNode == null)
                {
                    return currNode;
                }

                currNode = currNode.rightChildNode;
            }

            return null;
        }

        private TreeNode<ElementType> rootNode = null;

        private class TreeNode<NodeElementType> where NodeElementType : IComparable<NodeElementType>
        {
            public NodeElementType elementValue = default(NodeElementType);
            public TreeNode<NodeElementType> leftChildNode = null;
            public TreeNode<NodeElementType> rightChildNode = null;
            public TreeNode<NodeElementType> parentNode = null;
        }
    }

    public class IntBinaryTreeSet : BinaryTreeSet<int> { }
}

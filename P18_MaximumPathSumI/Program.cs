using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P18_MaximumPathSumI
{
    class Program
    {

        static string TreeString = @"759564174782183587102004824765190123750334880277730763679965042806167092414126568340807033414872334732371694295371446525439152975114701133287773177839681757917152381714914358502729486366046889536730731669874031046298272309709873933853600423";

        const int Depth = 15;

        static Node Root { get; set; }

        class Node
        {
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Cost { get; set; }
            public int Depth { get; set; }
            public int Index { get; set; }
        }

        static Dictionary<int, Node> AllNodes = new Dictionary<int, Node>();
        static Node ParseTree(int depth, int index)
        {
            if (AllNodes.ContainsKey(index)) return AllNodes[index];
            if (TreeString.Length <= index) return null;
            return AllNodes[index] = new Node
            {
                Left = ParseTree(depth + 1, index + depth * 2),
                Right = ParseTree(depth + 1, index + depth * 2 + 2),
                Cost = int.Parse(TreeString.Substring(index, 2)),
                Depth = depth,
                Index = index
            };
        }

        static string Print(Node root, Path path)
        {
            if (root == null)
                return "";

            var sb = new StringBuilder();
            var curDepth = 0;
            sb.Append(Enumerable.Repeat(" ", Depth - curDepth).
                Aggregate("", (s, e) => e + s));
            foreach (var node in BFS(root))
            {
                if (curDepth < node.Depth)
                {
                    curDepth++;
                    sb.AppendLine();
                    sb.Append(Enumerable.Repeat(" ", Depth - curDepth).
                        Aggregate("", (s, e) => e + s));
                }

                if (GetNodes(path).Contains(node))
                {
                    sb.Append("-");
                }
                sb.Append(node.Cost.ToString().PadLeft(2, '0'));
                if (GetNodes(path).Contains(node))
                {
                    sb.Append("-");
                }
                sb.Append(" ");
            }

            return sb.ToString();
        }
        static string Print(Node root)
        {
            if (root == null)
                return "";

            var sb = new StringBuilder();
            var curDepth = 0;
            sb.Append(Enumerable.Repeat(" ", Depth - curDepth).
                Aggregate("", (s, e) => e + s));
            foreach (var node in BFS(root))
            {
                if (curDepth < node.Depth)
                {
                    curDepth++;
                    sb.AppendLine();
                    sb.Append(Enumerable.Repeat(" ", Depth - curDepth).
                        Aggregate("", (s, e) => e + s));
                }
                sb.Append(node.Cost.ToString().PadLeft(2, '0'));
                sb.Append(" ");
            }

            return sb.ToString();
        }
        static IEnumerable<Node> GetNodes(Path path)
        {
            var bfsOrder = new Queue<Path>();
            bfsOrder.Enqueue(path);
            while (bfsOrder.Count > 0)
            {
                var curNode = bfsOrder.Dequeue();
                if (curNode == null) continue;
                yield return curNode.Node;
                bfsOrder.Enqueue(curNode.Last);
            }
        }
        static IEnumerable<Node> BFS(Node node)
        {
            var bfsOrder = new Queue<Node>();
            bfsOrder.Enqueue(node);
            var hasSeen = new HashSet<int>();
            while (bfsOrder.Count > 0)
            {
                var curNode = bfsOrder.Dequeue();
                if (curNode == null) continue;
                if (hasSeen.Contains(curNode.Index)) continue;
                hasSeen.Add(curNode.Index);
                yield return curNode;
                bfsOrder.Enqueue(curNode.Left);
                bfsOrder.Enqueue(curNode.Right);
            }
        }

        class Path : IComparable<Path>
        {
            public Path Last { get; set; }
            public Node Node { get; set; }
            public int TotalCost { get; set; }

            public int CompareTo(Path other) => other.TotalCost.CompareTo(TotalCost);
            public override bool Equals(object obj) => ((Path)obj).Node.Index == Node.Index;
            public override int GetHashCode() => Node.Index;
        }

        static void Main(string[] args)
        {
            Root = ParseTree(1, 0);
            Console.WriteLine(Print(Root));

            var path = NewMethod();
            Console.ReadLine();
        }

        private static Path NewMethod()
        {
            var paths = new List<Path>();
            var seen = new HashSet<int>();
            var curNode = new Path
            {
                Node = Root,
                TotalCost = Root.Cost
            };
            paths.Add(curNode);
            while (curNode.Node.Depth != Depth)
            {
                paths.Sort();
                curNode = paths[0];
                paths.RemoveAt(0);
                seen.Add(curNode.Node.Index);
                AddToPath(paths, seen, curNode, curNode.Node.Left);
                AddToPath(paths, seen, curNode, curNode.Node.Right);
            }
            return curNode;
        }

        private static void AddToPath(List<Path> paths, HashSet<int> seen, Path last, Node node)
        {
            if (node == null)
                return;
            if (seen.Contains(node.Index))
                return;

            var newPathNode = new Path
            {
                Last = last,
                Node = node,
                TotalCost = last.TotalCost + node.Cost
            };
            paths.Add(newPathNode);
        }
    }
}

﻿using System.Collections.Generic;

namespace Game
{
    public static class Tree<T> where T : class, ITreeNode<T>
    {
        /// <summary>Returns true if child is a descendent of parent sceneNode.</summary>
        public static bool IsDescendent(T child, T anscestor)
        {
            if (child.Parent == null)
            {
                return false;
            }
            if (child.Parent.Equals(anscestor))
            {
                return true;
            }
            return IsDescendent(child.Parent, anscestor);
        }

        /// <summary>
        /// Returns true if there is a loop that leads back to this node.
        /// </summary>
        public static bool ParentLoopExists(T node)
        {
            var map = new HashSet<T>();
            while (node.Parent != null)
            {
                node = node.Parent;
                if (!map.Add(node))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check if a tree's nodes have Parent and Children pointers matching.
        /// </summary>
        public static bool IsValid(T root)
        {
            foreach (T child in root.Children)
            {
                if (child.Parent != root)
                {
                    return false;
                }
                if (!IsValid(child))
                {
                    return false;
                }
            }
            return true;
        }

        public static List<T> GetAncestors(T node, bool includeSelf = false)
        {
            var list = new List<T>();
            if (includeSelf)
            {
                list.Add(node);
            }
            return GetAncestors(node, list);
        }

        static List<T> GetAncestors(T node, List<T> list)
        {
            list.Add(node);
            return node.Parent != null ? GetAncestors(node.Parent, list) : list;
        }

        public static List<T> GetDescendents(T root, bool includeRoot = true)
        {
            List<T> list = FindByType<T>(root);
            if (!includeRoot)
            {
                list.Remove(root);
            }
            return list;
        }

        public static List<T> GetDescendents(IList<T> roots, bool includeRoots = true)
        {
            List<T> list = new List<T>();
            foreach (T root in roots)
            {
                list.AddRange(GetDescendents(root));
            }
            return list;
        }

        /// <summary>
        /// Returns all nodes in a tree.
        /// </summary>
        public static List<T> GetAll(T node)
        {
            return GetDescendents(FindRoot(node));
        }

        public static T FindRoot(T node)
        {
            return node.Parent == null ? node : FindRoot(node.Parent);
        }

        public static List<TS> FindByType<TS>(T root) where TS : class, T
        {
            var list = new List<TS>();
            var nodeCast = root as TS;
            if (nodeCast != null)
            {
                list.Add(nodeCast);
            }
            foreach (T p in root.Children)
            {
                list.AddRange(FindByType<TS>(p));
            }
            return list;
        }

        /// <summary>
        /// Given a set of nodes, return a set containing only nodes that are not descendents of other nodes within the set.
        /// </summary>
        public static HashSet<T> FindRoots(IList<T> nodes)
        {
            HashSet<T> parents = new HashSet<T>(nodes);
            foreach (T node in nodes)
            {
                parents.ExceptWith(GetDescendents(node, false));
            }
            return parents;
        }

        /// <summary>
        /// Find the number of parents this node has.
        /// </summary>
        public static int Depth(T node)
        {
            if (node.Parent == null)
            {
                return 0;
            }
            return Depth(node.Parent) + 1;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$.Presentation
{
    /// <summary>
    /// Extension helpers for the <see cref="NavigationItem"/> class.
    /// </summary>
    public static class NavigationItemExtensions
    {
        /// <summary>
        /// Returns a collection of ancestors of specified item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IEnumerable<NavigationItem> GetAncestors(this NavigationItem item)
        {
            if (item == null) {
                throw new ArgumentNullException(nameof(item));
            }

            while (true) {
                item = item.Parent;
                if (item != null) {
                    yield return item;
                }
                else {
                    break;
                }
            }
        }

        /// <summary>
        /// Returns a collection of items that contain specified item, and the ancestors of specified item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IEnumerable<NavigationItem> GetAncestorsAndSelf(this NavigationItem item)
        {
            if (item == null) {
                throw new ArgumentNullException(nameof(item));
            }

            while (true) {
                if (item != null) {
                    yield return item;
                }
                else {
                    break;
                }

                item = item.Parent;
            }
        }

        /// <summary>
        /// Retrieves the descendant child items of specified item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IEnumerable<NavigationItem> GetDescendants(this NavigationItem item)
        {
            if (item == null) {
                throw new ArgumentNullException(nameof(item));
            }

            var stack = new Stack<NavigationItem>(item.Items.Reverse());
            return GetDescendantsAndSelf(stack);
        }

        /// <summary>
        /// Returns a collection of items that contain the specified item and all descendant items.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IEnumerable<NavigationItem> GetDescendantsAndSelf(this NavigationItem item)
        {
            if (item == null) {
                throw new ArgumentNullException(nameof(item));
            }

            var stack = new Stack<NavigationItem>();
            stack.Push(item);
            return GetDescendantsAndSelf(stack);
        }

        private static IEnumerable<NavigationItem> GetDescendantsAndSelf(Stack<NavigationItem> stack)
        {
            while (stack.Count > 0) {
                var item = stack.Pop();
                yield return item;

                foreach (var child in item.Items.Reverse()) {
                    stack.Push(child);
                }
            }
        }

        /// <summary>
        /// Determines whether specified item is the root item, that is it has no parent.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool IsRoot(this NavigationItem item)
        {
            if (item == null) {
                throw new ArgumentNullException(nameof(item));
            }

            return item.Parent == null;
        }

        /// <summary>
        /// Determines whether the item is a leaf item, meaning it has no children.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool IsLeaf(this NavigationItem item)
        {
            if (item == null) {
                throw new ArgumentNullException(nameof(item));
            }

            return !item.Items.Any();
        }

        /// <summary>
        /// Determines whether the item has grandchildren.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool HasGrandchildren(this NavigationItem item)
        {
            if (item == null) {
                throw new ArgumentNullException(nameof(item));
            }

            return item.Items.Any(i => !i.IsLeaf());
        }
    }
}

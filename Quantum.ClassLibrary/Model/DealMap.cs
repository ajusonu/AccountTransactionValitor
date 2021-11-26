using System.Collections.Generic;

namespace Quantum.ClassLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public class DealMap
    {
        /// <summary>
        /// Get Deal Map Item based on Index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DealMapItem this[int index] => Items[index];
        /// <summary>
        /// List of DealMapItem Items
        /// </summary>
        public IList<DealMapItem> Items { get; } = new List<DealMapItem>();
        /// <summary>
        /// Item Count
        /// </summary>
        public int Count => Items.Count;
        /// <summary>
        /// Add new Item
        /// </summary>
        /// <param name="item"></param>
        public void Add(DealMapItem item) => Items.Add(item);
    }

    
}


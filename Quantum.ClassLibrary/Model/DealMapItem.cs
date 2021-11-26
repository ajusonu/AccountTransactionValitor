using System;

namespace Quantum.ClassLibrary
{
    /// <summary>
    /// 
    /// </summary>
    public class DealMapItem
    {
        /// <summary>
        /// Comments 
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Nullable Effect date
        /// </summary>
        public DateTime? Effect_dt { get; set; }
        /// <summary>
        /// Trans Type
        /// </summary>
        public TransTypeEnum transtype { get; set; }
        /// <summary>
        /// Flag
        /// </summary>
        public string Flag { get; set; }
    }

    
}


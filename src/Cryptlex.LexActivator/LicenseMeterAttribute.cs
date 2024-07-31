using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptlex
{
    public class LicenseMeterAttribute
    {

        /// <summary>
        /// The name of the meter attribute.
        /// </summary>
        public string Name;

        /// <summary>
        /// The allowed uses of the meter attribute. A value of -1 indicates unlimited allowed uses.
        /// </summary>
        public long AllowedUses;

        /// <summary>
        /// The total uses of the meter attribute.
        /// </summary>
        public ulong TotalUses;

        /// <summary>
        /// The gross uses of the meter attribute.
        /// </summary>
        public ulong GrossUses;

        public LicenseMeterAttribute(string name, long allowedUses, ulong totalUses, ulong grossUses)
        {
            this.Name = name;
            this.AllowedUses = allowedUses;
            this.TotalUses = totalUses;
            this.GrossUses = grossUses;
        }

    }
}


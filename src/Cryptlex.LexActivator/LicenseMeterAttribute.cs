using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptlex
{
    public class LicenseMeterAttribute
    {

        public string Name;

        public long AllowedUses; // make sure to add doc here for -1

        public ulong TotalUses;

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


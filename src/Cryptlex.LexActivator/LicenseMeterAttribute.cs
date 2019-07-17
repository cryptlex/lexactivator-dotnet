using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptlex
{
    public class LicenseMeterAttribute
    {

        public string Name;

        public uint AllowedUses;

        public uint TotalUses;

        public LicenseMeterAttribute(string name, uint allowedUses, uint totalUses)
        {
            this.Name = name;
            this.AllowedUses = allowedUses;
            this.TotalUses = totalUses;
        }

    }
}


using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptlex
{
    public class UserLicense
    {
        /// <summary>
        /// The allowed activations of the license. A value of -1 indicates unlimited number of activations.
        /// </summary>
        public long AllowedActivations;

        /// <summary>
        /// The allowed deactivations of the license. A value of -1 indicates unlimited number of deactivations.
        /// </summary>
        public long AllowedDeactivations;

        /// <summary>
        /// The license key.
        /// </summary>
        public string Key;

        /// <summary>
        /// The license type (node-locked or hosted-floating).
        /// </summary>
        public string Type;
        
        /// <summary>
        /// License metadata with view_permission set to "user".
        /// </summary>
        public List<Metadata> Metadata = new List<Metadata>();
    }
        
    public class Metadata
    {
        public string Key;
        public string Value;
    }
}
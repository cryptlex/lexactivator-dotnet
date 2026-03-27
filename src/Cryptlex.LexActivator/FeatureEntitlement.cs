using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptlex
{
    public class FeatureEntitlement
    {
        /// <summary>
        /// The name of the feature
        /// </summary>
        public string FeatureName;

        /// <summary>
        /// The display name of the feature
        /// </summary>
        public string FeatureDisplayName;

        /// <summary>
        /// Effective value of the feature. Contains the overridden value if set at the license level;
        /// otherwise, the entitlement set value.
        /// </summary>
        public string Value;

        /// <summary>
        ///  Default value of the feature defined in the entitlement set; empty for features not inherited
        /// from an entitlement set.
        /// </summary>
        public string BaseValue;
    
        /// <summary>
        /// Timestamp when the license feature entitlement will expire
        /// </summary>
        public long ExpiresAt;

    }
}
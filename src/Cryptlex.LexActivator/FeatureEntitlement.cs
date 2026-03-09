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
        /// The value of the feature
        /// </summary>
        public string Value;

        /// <summary>
        /// The default value of the feature inherited from the entitlement set.
        /// </summary>
        public string BaseValue;
    
        /// <summary>
        /// Timestamp when the license feature entitlement will expire
        /// </summary>
        public long ExpiresAt;

    }
}
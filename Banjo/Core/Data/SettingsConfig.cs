//-----------------------------------------------------------------------
// <copyright file="SettingsConfig.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Core.Data
{
    /// <summary>Configuration data with settings</summary>
    [DataContract(Name = "Controller", Namespace = "")]
    public abstract class SettingsConfig
    {
        /// <summary>Initializes a new instance of the SettingsConfig class</summary>
        protected SettingsConfig()
        {
            this.Settings = new Dictionary<string, string>();
        }

        /// <summary>Gets the settings</summary>
        [IgnoreDataMember]
        public IDictionary<string, string> Settings { get; private set; }

        /// <summary>Gets or sets the settings (as text)</summary>
        [DataMember(Name = "Settings")]
        private string SettingsText
        {
            get
            {
                return string.Join(";", this.Settings.Select(kvp => "{0}={1}".FormatInvariant(kvp.Key, kvp.Value)).ToArray());
            }

            set
            {
                this.Settings = value
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .Where(s => !string.IsNullOrEmpty(s))
                    .Select(s => s.Split(new[] { '=' }, 2))
                    .ToDictionary(kvp => kvp[0].Trim(), kvp => kvp[1].Trim());
            }
        }
    }
}

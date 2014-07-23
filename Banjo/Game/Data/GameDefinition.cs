//-----------------------------------------------------------------------
// <copyright file="GameDefinition.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Core.Data;
using Core.Resources;

namespace Game.Data
{
    /// <summary>Represents a game definition</summary>
    [DataContract(Namespace = "")]
    public class GameDefinition : SerializedResource<GameDefinition>
    {
        /// <summary>Gets or sets the list of world identifiers</summary>
        [IgnoreDataMember]
        public string[] LevelIds { get; set; }

        /// <summary>Gets the settings</summary>
        [IgnoreDataMember]
        public IDictionary<string, string> Settings { get; internal set; }

        /// <summary>Gets or sets the title</summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>Gets or sets the list of world identifiers (as text)</summary>
        [DataMember(Name = "LevelIds")]
        private string LevelIdsText
        {
            get
            {
                return this.LevelIds == null ?
                    string.Empty :
                    string.Join(";", this.LevelIds);
            }
            
            set
            {
                this.LevelIds = value
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => s.Trim())
                    .Where(s => !string.IsNullOrEmpty(s))
                    .ToArray();
            }
        }

        /// <summary>Gets or sets the settings (as text)</summary>
        [DataMember(Name = "Settings")]
        private string SettingsText
        {
            get
            {
                return this.Settings == null ?
                    string.Empty :
                    string.Join(";", this.Settings.Select(kvp => "{0}={1}".FormatInvariant(kvp.Key, kvp.Value)).ToArray());
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

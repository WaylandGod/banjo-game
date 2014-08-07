//-----------------------------------------------------------------------
// <copyright file="ControllerConfig.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Runtime.Serialization;

namespace Core.Data
{
    /// <summary>Controller configuration</summary>
    [DataContract(Name = "Controller", Namespace = "")]
    public class ControllerConfig : SettingsConfig
    {
        /// <summary>Gets or sets the controller identifier</summary>
        [DataMember]
        public string ControllerId { get; set; }
    }
}

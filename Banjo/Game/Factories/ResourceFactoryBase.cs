//-----------------------------------------------------------------------
// <copyright file="ResourceFactoryBase.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Core;
using Core.Resources.Management;

namespace Game.Factories
{
    /// <summary>Base class for factories that need ResourceLibrary</summary>
    /// <typeparam name="TType">Type created by the factory</typeparam>
    public abstract class ResourceFactoryBase<TType> : FactoryBase<TType>, IFactory<TType>
    {
        /// <summary>Initializes a new instance of the ResourceFactoryBase class</summary>
        /// <param name="resources">Resource library</param>
        protected ResourceFactoryBase(IResourceLibrary resources)
        {
            this.Resources = resources;
        }

        /// <summary>Gets the resource library</summary>
        protected IResourceLibrary Resources { get; private set; }
    }
}

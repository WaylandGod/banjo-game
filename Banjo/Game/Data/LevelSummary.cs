//-----------------------------------------------------------------------
// <copyright file="LevelSummary.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Game.Data
{
    /// <summary>Summary of a world definition</summary>
    public class LevelSummary
    {
        /// <summary>Initializes a new instance of the LevelSummary class</summary>
        /// <param name="definition">World definition</param>
        internal LevelSummary(LevelDefinition definition)
        {
            this.Id = definition.Id;
            this.Title = definition.Title;
            this.Description = definition.Description;
        }

        /// <summary>Prevents a default instance of the LevelSummary class from being created</summary>
        private LevelSummary() { }

        /// <summary>Gets the world identifier</summary>
        public string Id { get; internal set; }

        /// <summary>Gets the world title</summary>
        public string Title { get; internal set; }

        /// <summary>Gets the world description</summary>
        public string Description { get; internal set; }
    }
}

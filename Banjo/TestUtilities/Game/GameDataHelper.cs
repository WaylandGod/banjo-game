//-----------------------------------------------------------------------
// <copyright file="GameDataHelper.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Data;
using Core.Resources;
using Core.Resources.Management;
using Game;
using Game.Data;
using Rhino.Mocks;

namespace TestUtilities.Game
{
    /// <summary>Test helpers for working with game data</summary>
    public static class GameDataHelper
    {
        /// <summary>Creates a test AvatarDefinition</summary>
        /// <returns>Created AvatarDefinition</returns>
        public static AvatarDefinition CreateTestAvatarDefinition()
        {
            return CreateTestAvatarDefinition(NewResourceId(), NewResourceId());
        }

        /// <summary>Creates a test AvatarDefinition</summary>
        /// <param name="id">Avatar identifier</param>
        /// <param name="resourceId">Resource identifier</param>
        /// <returns>Created AvatarDefinition</returns>
        public static AvatarDefinition CreateTestAvatarDefinition(string id, string resourceId)
        {
            var states = Enumerable.Range(0, 3)
                .Select(i => new AvatarState
                {
                    Id = i,
                    Name = "Test State {0}".FormatInvariant(i)
                })
                .ToArray();
            return CreateTestAvatarDefinition(
                id,
                resourceId,
                states.First().Name,
                states);
        }

        /// <summary>Creates a test AvatarDefinition</summary>
        /// <param name="id">Avatar identifier</param>
        /// <param name="resourceId">Resource identifier</param>
        /// <param name="defaultState">Default state</param>
        /// <param name="states">The states</param>
        /// <returns>Created AvatarDefinition</returns>
        public static AvatarDefinition CreateTestAvatarDefinition(
            string id,
            string resourceId,
            string defaultState,
            AvatarState[] states)
        {
            return new AvatarDefinition
            {
                Id = id,
                ResourceId = resourceId,
                DefaultState = defaultState,
                States = states,
            };
        }

        /// <summary>Deep creates a test AvatarDefinition</summary>
        /// <param name="resourceLibrary">Library in which to create resources</param>
        /// <returns>Created AvatarDefinition identifier</returns>
        public static string DeepCreateTestAvatarDefinition(IResourceLibrary resourceLibrary)
        {
            var avatar = CreateTestAvatarDefinition();
            resourceLibrary.AddResource(avatar);

            var content = "<AvatarResource>{0}</AvatarResource>".FormatInvariant(avatar.ResourceId);
            var resource = new GenericNativeResource<string>(avatar.ResourceId, content);
            resourceLibrary.AddResource(resource);

            return avatar.Id;
        }

        /// <summary>Creates a test EntityDefinition</summary>
        /// <returns>Created EntityDefinition</returns>
        public static EntityDefinition CreateTestEntityDefinition()
        {
            return CreateTestEntityDefinition(NewResourceId(), NewResourceId());
        }

        /// <summary>Creates a test EntityDefinition</summary>
        /// <param name="id">Entity identifier</param>
        /// <param name="avatarId">Avatar identifier</param>
        /// <returns>Created EntityDefinition</returns>
        public static EntityDefinition CreateTestEntityDefinition(
            string id,
            string avatarId)
        {
            return CreateTestEntityDefinition(id, avatarId, 1f);
        }

        /// <summary>Creates a test EntityDefinition</summary>
        /// <param name="id">Entity identifier</param>
        /// <param name="avatarId">Avatar identifier</param>
        /// <param name="mass">Entity mass</param>
        /// <returns>Created EntityDefinition</returns>
        public static EntityDefinition CreateTestEntityDefinition(
            string id,
            string avatarId,
            double mass)
        {
            return new EntityDefinition
            {
                Id = id,
                AvatarId = avatarId,
                Mass = mass,
                Controllers = new[] { new ControllerConfig { ControllerId = "controller.testA" } },
            };
        }

        /// <summary>Deep creates a test EntityDefinition</summary>
        /// <param name="resourceLibrary">Library in which to create resources</param>
        /// <returns>Created EntityDefinition identifier</returns>
        public static string DeepCreateTestEntityDefinition(IResourceLibrary resourceLibrary)
        {
            var entity = CreateTestEntityDefinition(
                NewResourceId(),
                DeepCreateTestAvatarDefinition(resourceLibrary));
            resourceLibrary.AddResource(entity);
            return entity.Id;
        }

        /// <summary>Creates a test LevelDefinition</summary>
        /// <returns>Created LevelDefinition</returns>
        public static LevelDefinition CreateTestLevelDefinition()
        {
            return CreateTestLevelDefinition(NewResourceId(), 5);
        }

        /// <summary>Creates a test LevelDefinition</summary>
        /// <param name="id">World identifier</param>
        /// <param name="numEntities">Number of entities</param>
        /// <returns>Created LevelDefinition</returns>
        public static LevelDefinition CreateTestLevelDefinition(
            string id,
            int numEntities)
        {
            var entityIds = Enumerable.Range(0, numEntities).Select(i => NewResourceId()).ToArray();
            return CreateTestLevelDefinition(id, entityIds);
        }

        /// <summary>Creates a test LevelDefinition</summary>
        /// <param name="id">World identifier</param>
        /// <param name="entityIds">EntityDefinition identifiers</param>
        /// <returns>Created LevelDefinition</returns>
        public static LevelDefinition CreateTestLevelDefinition(
            string id,
            string[] entityIds)
        {
            var name = "Test World {0}".FormatInvariant(id);
            var description = "World of Testing {0}".FormatInvariant(id);
            return CreateTestLevelDefinition(id, name, description, entityIds);
        }

        /// <summary>Creates a test LevelDefinition</summary>
        /// <param name="id">World identifier</param>
        /// <param name="title">World title</param>
        /// <param name="description">World description</param>
        /// <param name="entityIds">EntityDefinition identifiers</param>
        /// <returns>Created LevelDefinition</returns>
        public static LevelDefinition CreateTestLevelDefinition(
            string id,
            string title,
            string description,
            string[] entityIds)
        {
            return new LevelDefinition
            {
                Id = id,
                Title = title,
                Description = description,
                Entities = new LevelDefinition.EntityCollection(
                    entityIds.Select(entityId => new LevelDefinition.EntityCollection.Entry(entityId))),
            };
        }

        /// <summary>Deep creates a test LevelDefinition</summary>
        /// <param name="resourceLibrary">Library in which to create resources</param>
        /// <param name="numEntities">Number of entities to create</param>
        /// <returns>Created LevelDefinition identifier</returns>
        public static string DeepCreateTestLevelDefinition(
            IResourceLibrary resourceLibrary,
            int numEntities)
        {
            var entityIds = Enumerable.Range(0, numEntities).Select(i =>
                DeepCreateTestEntityDefinition(resourceLibrary))
                .ToArray();
            var level = CreateTestLevelDefinition(
                NewResourceId(),
                entityIds);
            resourceLibrary.AddResource(level);
            return level.Id;
        }

        /// <summary>Creates a test GameDefinition</summary>
        /// <returns>Created GameDefinition</returns>
        public static GameDefinition CreateTestGameDefinition()
        {
            return CreateTestGameDefinition(NewResourceId(), 10);
        }

        /// <summary>Creates a test GameDefinition</summary>
        /// <param name="id">Game identifier</param>
        /// <param name="numLevels">Number of levels</param>
        /// <returns>Created GameDefinition</returns>
        public static GameDefinition CreateTestGameDefinition(
            string id,
            int numLevels)
        {
            var levelIds = Enumerable.Range(0, numLevels)
                .Select(i => NewResourceId())
                .ToArray();
            return CreateTestGameDefinition(id, levelIds);                
        }

        /// <summary>Creates a test GameDefinition</summary>
        /// <param name="id">Game identifier</param>
        /// <param name="levelIds">World identifiers</param>
        /// <returns>Created GameDefinition</returns>
        public static GameDefinition CreateTestGameDefinition(
            string id,
            IEnumerable<string> levelIds)
        {
            return new GameDefinition
            {
                Id = id,
                Title = "Test Game {0}".FormatInvariant(id),
                LevelIds = levelIds.ToArray(),
            };
        }

        /// <summary>Deep creates a test GameDefinition</summary>
        /// <remarks>Creates a test game definition and all referenced resources</remarks>
        /// <param name="resourceLibrary">Library in which to create resources</param>
        /// <param name="numEntities">Number of entities to create</param>
        /// <param name="numLevels">Number of levels to create</param>
        /// <returns>Created game definition identifier</returns>
        public static string DeepCreateTestGameDefinition(
            IResourceLibrary resourceLibrary,
            int numEntities,
            int numLevels)
        {
            var levelIds = Enumerable.Range(0, numLevels)
                .Select(i =>
                    DeepCreateTestLevelDefinition(resourceLibrary, numEntities))
                .ToArray();
            var game = CreateTestGameDefinition(NewResourceId(), levelIds);
            resourceLibrary.AddResource(game.GetTextResource());
            return game.Id;
        }

        /// <summary>Creates a new, unique resource identifier</summary>
        /// <returns>Resource identifier</returns>
        private static string NewResourceId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}

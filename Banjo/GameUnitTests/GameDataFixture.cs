//-----------------------------------------------------------------------
// <copyright file="GameDataFixture.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using Core.Data;
using Core.Resources;
using Game;
using Game.Data;
using NUnit.Framework;

namespace GameUnitTests
{
    /// <summary>Text fixture for the game data classes</summary>
    [TestFixture]
    public class GameDataFixture
    {
        /// <summary>Per-test initialization</summary>
        [SetUp]
        public void SetUp()
        {
        }

       /// <summary>Test logger implementations are registered correctly</summary>
        [Test]
        public void GenerateTestGameDef()
        {
            var eyemanAvatar = new AvatarDefinition
            {
                Id = "avatar.eyeman",
                ResourceId = "avatar.prefab.eyeman",
                DefaultState = "Idle",
                States = new[]
                {
                    new AvatarState { Id = 0, Name = "Idle" },
                    new AvatarState { Id = 1, Name = "Walk" },
                },
            };
            var eyemanMaterial = new Material { Id = "material.eyeman", Mass = 1, Round = true };
            var eyemanEntity = new EntityDefinition
            {
                Id = "entity.eyeman",
                AvatarId = eyemanAvatar.Id,
                MaterialId = eyemanMaterial.Id,
                Volume = 1,
                Controllers = new[]
                {
                    new ControllerConfig
                    {
                        ControllerId = "some.controller",
                        Settings = { { "Foo", "42" }, { "Bar", "Don't Panic" } }
                    },
                },
            };

            var solidMaterial = new Material { Id = "material.solid", Mass = 10, Round = false };
            var floorAvatar = new AvatarDefinition
            {
                Id = "avatar.floor",
                ResourceId = "avatar.prefab.floor",
                DefaultState = "Idle",
                States = new[] { new AvatarState { Id = 0, Name = "Idle" } },
            };
            var floorTile = new TileDefinition
            {
                Id = "tile.floor",
                AvatarId = floorAvatar.Id,
                MaterialId = solidMaterial.Id,
                Volume = 1,
            };
            var wallAvatar = new AvatarDefinition
            {
                Id = "avatar.wall",
                ResourceId = "avatar.prefab.wall",
                DefaultState = "Idle",
                States = new[] { new AvatarState { Id = 0, Name = "Idle" } },
            };
            var wallTile = new TileDefinition
            {
                Id = "tile.wall",
                AvatarId = wallAvatar.Id,
                MaterialId = solidMaterial.Id,
                Volume = 4,
            };

            var level = new LevelDefinition
            {
                Id = "level.alpha",
                Title = "Test Level Alpha",
                Description = "Alpha testing level",
                Entities =
                {
                    new LevelDefinition.EntityCollection.Entry
                    {
                        EntityId = eyemanEntity.Id, Direction = Vector3.Zero, Position = new Vector3(4, 0, 4),
                        Controllers = new[]
                        {
                            new ControllerConfig
                            {
                                ControllerId = "some.controller",
                                Settings = { { "Foo", "57" }, { "Bar", "Keep Calm" } }
                            },
                        },
                    },
                },
                Tiles = { floorTile.Id, wallTile.Id, },
                Map =
                {
                    Breadth = 16,
                    Depth = 16,
                    TileSpacing = 6,
                    Tiles = new int[]
                    {
                        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                        1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                        1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                        1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,

                        1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 1,
                        1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1,
                        1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1,
                        1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1,

                        1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                        1, 0, 0, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 1,
                        1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                        1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,

                        1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                        1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                        1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                        1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                    },
                }
            };

            var game = new GameDefinition
            {
                Id = "game.eyeblock",
                Title = "EYEBLOCK TAKE 2.0.1b",
                LevelIds = new[] { level.Id },
                Settings = new Dictionary<string, string>
                {
                    { "foo", "bar" },
                    { "hello", "world" },
                    { "answer", "42" },
                },
            };

            var resources = new IResource[]
            {
                eyemanAvatar, eyemanMaterial, eyemanEntity, solidMaterial, floorAvatar, floorTile, wallAvatar, wallTile, level, game
            };
            var resourcesXml = string.Join("\n\n\n", resources.Select(res => res.ToString()).ToArray());
            
            Assert.IsFalse(string.IsNullOrEmpty(resourcesXml));
        }
    }
}

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
            var eyemanEntity = new EntityDefinition
            {
                Id = "entity.eyeman",
                AvatarId = eyemanAvatar.Id,
                Mass = 1,
                Controllers = new[]
                {
                    new ControllerConfig
                    {
                        ControllerId = "some.controller",
                        Settings = { { "Foo", "42" }, { "Bar", "Don't Panic" } }
                    },
                },
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
                        EntityId = eyemanEntity.Id, Direction = Vector3D.Zero, Position = new Vector3D(4f, 0f, 4f),
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
                eyemanAvatar, eyemanEntity, level, game
            };
            var resourcesXml = string.Join("\n\n\n", resources.Select(res => res.ToString()).ToArray());
            
            Assert.IsFalse(string.IsNullOrEmpty(resourcesXml));
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="DefinitionsSerializationFixture.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Linq;
using Core;
using Core.Data;
using Game;
using Game.Data;
using NUnit.Framework;

namespace GameUnitTests
{
    /// <summary>Test fixture for serialization</summary>
    [TestFixture]
    public class DefinitionsSerializationFixture
    {
        /// <summary>Per-test initialization</summary>
        [SetUp]
        public void SetUp()
        {
        }

        /// <summary>Roundtrip game definition</summary>
        [Test]
        public void RoundtripGameDefinition()
        {
            var expected = new GameDefinition
            {
                Id = Guid.NewGuid().ToString(),
                Title = Guid.NewGuid().ToString(),
                LevelIds = new[]
                {
                    Guid.NewGuid().ToString(),
                    Guid.NewGuid().ToString(),
                    Guid.NewGuid().ToString(),
                },
            };

            var xml = expected.ToString();
            Assert.IsFalse(string.IsNullOrEmpty(xml));

            var actual = GameDefinition.FromString(xml);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.LevelIds.Length, actual.LevelIds.Length);
        }

        /// <summary>Roundtrip entity definition</summary>
        [Test]
        public void RoundtripEntityDefinition()
        {
            var expected = new EntityDefinition
            {
                Id = Guid.NewGuid().ToString(),
                AvatarId = Guid.NewGuid().ToString(),
                Mass = 1f,
            };

            var xml = expected.ToString();
            Assert.IsFalse(string.IsNullOrEmpty(xml));

            var actual = EntityDefinition.FromString(xml);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.AvatarId, actual.AvatarId);
            Assert.AreEqual(expected.Mass, actual.Mass);
        }

        /// <summary>Roundtrip world definition</summary>
        [Test]
        public void RoundtripLevelDefinition()
        {
            var entities = new EntityDefinition[]
            {
                    new EntityDefinition
                    {
                        Id = Guid.NewGuid().ToString(),
                        AvatarId = Guid.NewGuid().ToString(),
                        Mass = 1f,
                    },
                    new EntityDefinition
                    {
                        Id = Guid.NewGuid().ToString(),
                        AvatarId = Guid.NewGuid().ToString(),
                        Mass = 1f,
                    },
            };

            var expected = new LevelDefinition
            {
                Id = Guid.NewGuid().ToString(),
                Title = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
                Entities =
                {
                    new LevelDefinition.EntityCollection.Entry
                    {
                        EntityId = entities[0].Id,
                        Position = Vector3D.Zero,
                        Direction = Vector3D.Zero,
                        Velocity = Vector3D.Zero,
                    },
                    new LevelDefinition.EntityCollection.Entry
                    {
                        EntityId = entities[1].Id,
                        Position = Vector3D.Identity,
                        Direction = Vector3D.Identity,
                        Velocity = Vector3D.Zero,
                    },
                },
            };

            var xml = expected.ToString();
            Assert.IsFalse(string.IsNullOrEmpty(xml));

            var actual = LevelDefinition.FromString(xml);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.Entities.Count, actual.Entities.Count);
        }
    }
}

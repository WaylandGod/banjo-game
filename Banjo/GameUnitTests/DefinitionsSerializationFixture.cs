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

        /// <summary>Roundtrip material</summary>
        [Test]
        public void RoundtripMaterial()
        {
            var expected = new Material
            {
                Id = Guid.NewGuid().ToString(),
                Mass = 1f,
                Round = false,
            };

            var xml = expected.ToString();
            Assert.IsFalse(string.IsNullOrEmpty(xml));

            var actual = Material.FromString(xml);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Mass, actual.Mass);
            Assert.AreEqual(expected.Round, actual.Round);
        }

        /// <summary>Roundtrip tile definition</summary>
        [Test]
        public void RoundtripTileDefinition()
        {
            var expected = new TileDefinition
            {
                Id = Guid.NewGuid().ToString(),
                AvatarId = Guid.NewGuid().ToString(),
                MaterialId = Guid.NewGuid().ToString(),
                Direction = Vector3.Zero,
                Volume = 1f,
            };

            var xml = expected.ToString();
            Assert.IsFalse(string.IsNullOrEmpty(xml));

            var actual = TileDefinition.FromString(xml);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.AvatarId, actual.AvatarId);
            Assert.AreEqual(expected.Direction, actual.Direction);
            Assert.AreEqual(expected.MaterialId, actual.MaterialId);
            Assert.AreEqual(expected.Volume, actual.Volume);
        }

        /// <summary>Roundtrip entity definition</summary>
        [Test]
        public void RoundtripEntityDefinition()
        {
            var expected = new EntityDefinition
            {
                Id = Guid.NewGuid().ToString(),
                AvatarId = Guid.NewGuid().ToString(),
                MaterialId = Guid.NewGuid().ToString(),
                Volume = 1f,
            };

            var xml = expected.ToString();
            Assert.IsFalse(string.IsNullOrEmpty(xml));

            var actual = EntityDefinition.FromString(xml);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.AvatarId, actual.AvatarId);
            Assert.AreEqual(expected.MaterialId, actual.MaterialId);
            Assert.AreEqual(expected.Volume, actual.Volume);
        }

        /// <summary>Roundtrip world definition</summary>
        [Test]
        public void RoundtripLevelDefinition()
        {
            var tiles = new[]
            {
                new TileDefinition
                {
                    Id = Guid.NewGuid().ToString(),
                    AvatarId = Guid.NewGuid().ToString(),
                    MaterialId = Guid.NewGuid().ToString(),
                    Volume = 1f,
                    Direction = Vector3.Zero,
                },
                new TileDefinition
                {
                    Id = Guid.NewGuid().ToString(),
                    AvatarId = Guid.NewGuid().ToString(),
                    MaterialId = Guid.NewGuid().ToString(),
                    Volume = 1f,
                    Direction = Vector3.Zero,
                },
            };

            var entities = new EntityDefinition[]
            {
                    new EntityDefinition
                    {
                        Id = Guid.NewGuid().ToString(),
                        AvatarId = Guid.NewGuid().ToString(),
                        MaterialId = Guid.NewGuid().ToString(),
                        Volume = 1f,
                    },
                    new EntityDefinition
                    {
                        Id = Guid.NewGuid().ToString(),
                        AvatarId = Guid.NewGuid().ToString(),
                        MaterialId = Guid.NewGuid().ToString(),
                        Volume = 1f,
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
                        Position = Vector3.Zero,
                        Direction = Vector3.Zero,
                        Velocity = Vector3.Zero,
                    },
                    new LevelDefinition.EntityCollection.Entry
                    {
                        EntityId = entities[1].Id,
                        Position = Vector3.Identity,
                        Direction = Vector3.Identity,
                        Velocity = Vector3.Zero,
                    },
                },
                Tiles = new LevelDefinition.TileCollection
                {
                    tiles[0].Id,
                    tiles[1].Id,
                },
                Map = new LevelDefinition.TileMap
                {
                    TileSpacing = 1f,
                    Breadth = 8,
                    Depth = 8,
                    Tiles = Enumerable.Range(0, 8 * 8).Select(i => i % 2).ToArray(),
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
            Assert.AreEqual(expected.Tiles.Count, actual.Tiles.Count);
            Assert.AreEqual(expected.Map.Breadth, actual.Map.Breadth);
            Assert.AreEqual(expected.Map.Depth, actual.Map.Depth);
            Assert.AreEqual(expected.Map.TileSpacing, actual.Map.TileSpacing);
            Assert.AreEqual(expected.Map.Tiles.Length, actual.Map.Tiles.Length);
        }
    }
}

//-----------------------------------------------------------------------
// <copyright file="EntityFixture.cs" company="Benjamin Woodall">
//     Copyright Benjamin Woodall 2014. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.DependencyInjection;
using Core.Input;
using Core.Programmability;
using Core.Resources.Management;
using Game;
using Game.Data;
using Game.Factories;
using Game.Programmability;
using NUnit.Framework;
using TestUtilities.Core;
using TestUtilities.Game;
using TestUtilities.Game.Factories;

namespace GameUnitTests
{
    /// <summary>Text fixture for vector classes</summary>
    [TestFixture]
    public class EntityFixture
    {
        /// <summary>Random number generator for testing</summary>
        private static readonly Random R = new Random();

        /// <summary>Test controller manager</summary>
        private IControllerManager controllers;

        /// <summary>Test resource library</summary>
        private IResourceLibrary resources;

        /// <summary>Test avatar factory</summary>
        private IAvatarFactory avatarFactory;

        /// <summary>Test entity controller factory</summary>
        private IControllerFactory controllerFactory;

        /// <summary>Per-test initialization</summary>
        [SetUp]
        public void SetUp()
        {
            this.resources = new ResourceLibrary();
            this.controllers = new ControllerManager();
            this.avatarFactory = new TestAvatarFactory(this.resources);
            this.controllerFactory = new ReflectionControllerFactory(this.controllers, this.resources);
            new DependencyContainer()
                .RegisterSingleton<IControllerManager, ControllerManager>()
                .RegisterSingleton<IInputManager, InputManager>();
        }

        /// <summary>Per-test cleanup</summary>
        [TearDown]
        public void TearDown()
        {
            if (this.resources != null)
            {
                this.resources.Dispose();
            }

            this.resources = null;
            GC.Collect();
        }

        /// <summary>Test creating an entity</summary>
        [Test]
        public void CreateEntity()
        {
            var entityDefinition = this.resources.GetResource<EntityDefinition>(GameDataHelper.DeepCreateTestEntityDefinition(this.resources));
            var entity = new TestEntity(
                entityDefinition,
                this.resources,
                this.avatarFactory,
                this.controllerFactory,
                new ControllerConfig[0],
                Vector3.Zero,
                Vector3.Zero,
                Vector3.Zero);
            Assert.IsNotNull(entity);
        }

        /// <summary>Test creating an entity with an initial position</summary>
        [Test]
        public void CreateEntityAtPosition()
        {
            var position = R.NextVector3();
            var entity = new TestEntity(
                this.resources,
                this.avatarFactory,
                this.controllerFactory,
                new ControllerConfig[0],
                position,
                Vector3.Zero,
                Vector3.Zero);
            Assert.IsNotNull(entity);
            Assert.AreEqual(position, entity.Avatar.Position);
        }

        /// <summary>Test creating an entity with controllers</summary>
        [Test]
        public void CreateEntityWithControllers()
        {
            var entityDefinition = this.resources.GetResource<EntityDefinition>(GameDataHelper.DeepCreateTestEntityDefinition(this.resources));
            entityDefinition.Controllers = new[]
            {
                new ControllerConfig
                {
                    ControllerId = "controller.testA",
                    Settings = { { "Foo", "512" }, { "Bar", "Hello World." } }
                },
                new ControllerConfig
                {
                    ControllerId = "controller.testB",
                    Settings = { { "Foo", "42" }, { "Bar", "Don't Panic" } }
                },
            };
            var controllers = new[]
            {
                new ControllerConfig
                {
                    ControllerId = "controller.testB",
                    Settings = { { "Bar", "Keep Calm" } }
                },
                new ControllerConfig
                {
                    ControllerId = "controller.testC",
                    Settings = { { "Foo", "159" }, { "Bar", "Carry On" } }
                },
            };
            var entity = new TestEntity(
                entityDefinition,
                this.resources,
                this.avatarFactory,
                this.controllerFactory,
                controllers,
                Vector3.Zero,
                Vector3.Zero,
                Vector3.Zero);
            Assert.IsTrue(entity.Controllers.All(c => c.Target == entity));
            Assert.IsFalse(entity.Controllers.Any(c => c.Id.ResourceId.StartsWith("controller.testB") && c.Config.GetValue("Bar") == "Don't Panic"));
            Assert.IsTrue(entity.Controllers.Any(c => c.Id.ResourceId.StartsWith("controller.testB") && c.Config.GetValue("Foo") == "42"));
            Assert.IsTrue(entity.Controllers.Any(c => c.Id.ResourceId.StartsWith("controller.testB") && c.Config.GetValue("Bar") == "Keep Calm"));
        }
    }
}

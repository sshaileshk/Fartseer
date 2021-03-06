﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SFML.Graphics;
using SFML.Window;

using FarseerPhysics.Controllers;
using FarseerPhysics.Dynamics;

// this is some odd farseer shit
using Microsoft.Xna.Framework;

namespace Fartseer.Components
{
	public class Player : Actor
	{
		public Player(int initPriority)
			: base(initPriority)
		{
		}

		protected override List<GameComponent> GetInitComponents()
		{
			List<GameComponent> components = new List<GameComponent>();

			components.Add(new CenterView());
			components.Add(new ParticleSpawner(0));

			return components;
		}

		protected override bool Init()
		{
			Weapon weapon = new Weapon(0);
			EquipWeapon(weapon);

			return base.Init();
		}

		public override List<Command> SetupCommands()
		{
			List<Command> commands = base.SetupCommands();

			List<GameComponent> result;
			if (!(result = Game.GetComponents(new ComponentList().Add<EffectManager>().Add<Physics>())).Any())
			{
				Console.WriteLine("Cannot find requested components in {0}", Game.GetType().Name);
				return commands;
			}

			EffectManager effectManager = result.Get<EffectManager>();
			Physics physics = result.Get<Physics>();

			commands.Add(CreateKeyboardCommand(CommandType.Continuous, Keyboard.Key.A, (a) => a.Move(MoveDirection.Left, 5)));
			commands.Add(CreateKeyboardCommand(CommandType.Continuous, Keyboard.Key.D, (a) => a.Move(MoveDirection.Right, 5)));
			commands.Add(CreateKeyboardCommand(CommandType.Once, Keyboard.Key.Space, (a) => a.Move(MoveDirection.Jump, 7)));
			commands.Add(CreateKeyboardCommand(CommandType.Once, Keyboard.Key.E, (a) => effectManager.Explode(Position, 5f, 1f) ));

			commands.Add(CreateKeyboardCommand(CommandType.Once, Keyboard.Key.B, (a) => Game.ToggleDebug() ));

			commands.Add(CreateKeyboardCommand(CommandType.Once, Keyboard.Key.R, (a) =>
			this.GetComponent<ParticleSpawner>().SpawnOneParticle(new Vector2f(16, 16), new Vector2f(0, 0), "boxAlt", 1000, 10f)));

			commands.Add(CreateMouseCommand(CommandType.Once, Mouse.Button.Right, (a, pos) =>
			physics.CreateBody(BodyType.Dynamic, Game.Window.MapPixelToCoords(pos).ToVector2(), new Vector2(32, 32), "boxAlt")));

			return commands;
		}

		public override Body SetupBody(Physics physics)
		{
			Body b = physics.CreateBody(BodyType.Dynamic, new Vector2f(50, 50).ToVector2(), new Vector2(32, 32), "boxAlt");
			b.FixedRotation = true;

			return b;
		}
	}
}

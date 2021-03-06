﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Fartseer.Components;

using SFML.Graphics;
using SFML.Window;

using FarseerPhysics;
using FarseerPhysics.Collision;
using FarseerPhysics.Common;
using FarseerPhysics.Controllers;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

// this is some odd farseer shit
using Microsoft.Xna.Framework;

namespace Fartseer
{
	public static class Extensions
	{
		// VECTORS
		public static Vector2 ToVector2(this Vector2f vec)
		{
			return new Vector2(vec.X, vec.Y);
		}

		public static Vector2f ToVector2f(this Vector2 vec)
		{
			return new Vector2f(vec.X, vec.Y);
		}

		public static Vector2f ToVector2f(this Vector2i vec)
		{
			return new Vector2f(vec.X, vec.Y);
		}

		public static Vector2 ToVector2(this Vector2i vec)
		{
			return new Vector2(vec.X, vec.Y);
		}

		public static float Length(this Vector2f vec)
		{
			return (float)Math.Sqrt((vec.X * vec.X) + (vec.Y * vec.Y));
		}
		public static float Length(this Vector2f vec, bool noSqrt)
		{
			if (noSqrt)
				return (vec.X * vec.X) + (vec.Y * vec.Y);
			else
				return Length(vec);
		}

		public static float Distance(this Vector2f from, Vector2f to)
		{
			return Length(to - from);
		}
		public static float Distance(this Vector2f from, Vector2f to, bool noSqrt)
		{
			if (noSqrt)
				return Length(to - from, true);
			else
				return Length(to - from);
		}

		public static Vector2f Normalize(this Vector2f vec)
		{
			return new Vector2f(vec.X / vec.Length(), vec.Y / vec.Length());
		}

		public static float Dot(this Vector2f from, Vector2f to)
		{
			return from.X * to.X + from.Y * to.Y;
		}

		public static Vector2f RadianToVector(float radian)
		{
			// source: http://stackoverflow.com/questions/18851761/convert-an-angle-in-degrees-to-a-vector
			return new Vector2f((float)Math.Sin(radian), -(float)Math.Cos(radian));
		}

		public static float AngleBetween(this Vector2f vec, Vector2f vec1)
		{
			return MathHelper.ToDegrees((float)Math.Atan2(vec.Y - vec1.Y, vec.X - vec1.X));
		}

		// STRINGS
		public static string Repeat(this string str, int times)
		{
			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < times; i++)
				builder.Append(str);
			return builder.ToString();
		}

		// MISC
		public static bool Matches(this ComponentRestriction compRestrict, GameComponent comp)
		{
			switch (compRestrict)
			{
				case ComponentRestriction.Either:
					return true;

				case ComponentRestriction.Normal:
					return !(comp is DrawableGameComponent); // not drawable = normal

				case ComponentRestriction.Drawable:
					return comp is DrawableGameComponent;

				default:
					return false;
			}
		}

		public static T Get<T>(this List<GameComponent> list) where T : GameComponent
		{
			return (T)list.Find(o => o is T);
		}
	}
}

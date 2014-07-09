﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SFML.Graphics;
using SFML.Window;

namespace Fartseer.Components
{
	public class ImageManager : GameComponent
	{
		Dictionary<string, Image> images;

		public ImageManager()
			: this(0)
		{
		}
		public ImageManager(int initPriority)
			: base(initPriority)
		{
			Enabled = false;
		}

		protected override bool Init()
		{
			images = new Dictionary<string, Image>();

			string path = "texture";
			string[] files = Directory.GetFiles(path, "*.png");
			Console.WriteLine("{0}Found {1} PNG files in \"{2}\"", " ".Repeat(initIndex), files.Length, path);
			foreach (string file in files)
			{
				Image img = new Image(file);
				images.Add(Path.GetFileNameWithoutExtension(file), img);
				Console.WriteLine("{0}\"{1}\" loaded", " ".Repeat(initIndex + 1), file);
			}

			return base.Init();
		}

		public Image GetImage(string name)
		{
			if (images.ContainsKey(name))
				return images[name];

			return null;
		}

		public bool ImageExists(string name)
		{
			return images.ContainsKey(name);
		}
	}
}
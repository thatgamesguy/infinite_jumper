using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace InfiniteJumper.Horizontal
{
	/// <summary>
	/// Responsible from loading the level data from file.
	/// </summary>
	public class LevelLoader
	{
		/// <summary>
		/// The local level data file location.
		/// </summary>
		public static readonly string LOCAL_LOCATION = "Horizontal_Levels/";

		private string _fileName;
		private string[,] levelGrid = new string[LevelChunk.HEIGHT, LevelChunk.WIDTH];

		private bool _levelCached;

		/// <summary>
		/// Initializes a new instance of the <see cref="InfiniteJumper.LevelLoader"/> class. 
		/// </summary>
		/// <param name="fileName">The filename of the level data to load.</param>
		public LevelLoader (string fileName)
		{
			_fileName = fileName;
		}

		/// <summary>
		/// Loads the level data from file and converts it to a 2d array of strings. Returns cached data if level data already loaded.
		/// </summary>
		/// <returns>The level data.</returns>
		public string[,] LoadLevel ()
		{

			if (_levelCached) {
				return levelGrid;
			}

			var stagedString = new List<string[]> ();
		
			
			var fileName = LOCAL_LOCATION + _fileName; 
			TextAsset levelText = Resources.Load (fileName) as TextAsset; 
			
			if (levelText == null) {
				Debug.LogError ("Error loading file: " + fileName);
				return null;
			}
			
			string[] linesFromfile = levelText.text.Split ("\n" [0]);
			
			foreach (string odczyt in linesFromfile) {
				string[] entries = odczyt.Split (' ');
				Array.Reverse (entries);
				
				if (entries.Length > 0) {
					stagedString .Add (entries);
				}
			}

			stagedString.Reverse ();
			
			int x = 0;
			
			foreach (var s in stagedString) {
				for (int y = 0; y < s.Length; y++) {
					
					levelGrid [x, y] = s [y];
				}
				x++;
			}

			_levelCached = true;
			return levelGrid;
			
		}
	}
}

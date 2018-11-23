using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;  

namespace InfiniteJumper.Verical
{
	/// <summary>
	/// Level chunk.
	/// </summary>
	public class LevelChunk : MonoBehaviour
	{
		/// <summary>
		/// The width of the level chunk.
		/// </summary>
		public static readonly int WIDTH = 12;

		/// <summary>
		/// the height of the level chunk.
		/// </summary>
		public static readonly int HEIGHT = 20;

		/// <summary>
		/// The name of the level file stored in the Resources/Levels folder.
		/// </summary>
		public string levelFileName;

		/// <summary>
		/// The chance of this level chunk to spawn. This chance is proportional to the weights of the other <see cref="InfiniteJumper.LevelChunks"/> 
		/// </summary>
		public float weight = 1;

		private string[, ] _levelGrid = new string[HEIGHT, WIDTH];

		private bool _initialised = false;

		/// <summary>
		/// Returns an initialised array of string representing a level chunk.
		/// </summary>
		/// <value>The level grid.</value>
		public string[,] levelGrid {
			get {
				if (!_initialised) {
					Initialise ();
				}
				return _levelGrid;

			}
		}

		/// <summary>
		/// Initialise this instance. Creates new instance of <see cref="InfiniteJumper.LevelLoader"/> .
		/// </summary>
		public void Initialise ()
		{
			_levelGrid = new LevelLoader (levelFileName).LoadLevel ();
			_initialised = true;
		}
	
			
	}
}
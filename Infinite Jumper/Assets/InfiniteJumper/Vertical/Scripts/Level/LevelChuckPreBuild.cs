using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

namespace InfiniteJumper.Verical
{
	/// <summary>
	/// A level chuck that is static and built at start. Used to construct the empty level chuck underneath player at game start.
	/// </summary>
	public class LevelChuckPreBuild : MonoBehaviour
	{
		/// <summary>
		/// The name of the level file stored in the Resources/Levels folder.
		/// </summary>
		public string levelFileName;

		/// <summary>
		/// The tile prefabs. A list of tile prefabs and associated char representation.
		/// </summary>
		public TilePrefab[] tilePrefabs;
	
		void Start ()
		{
			if (tilePrefabs.Length == 0) {
				Debug.LogError ("Please setup tile prefabs");
				enabled = false;
				return;
			}

			var tileLookUp = new Dictionary<char, GameObject> ();

			foreach (var t in tilePrefabs) {
				tileLookUp.Add (t.textRepresentation, t.prefab);
			}

			GameObject obj = new LevelBuilder (tileLookUp).BuildChunkAtPosition (transform.position, new LevelLoader (levelFileName).LoadLevel ());

			obj.transform.SetParent (transform);
		}
	
				
	}
}


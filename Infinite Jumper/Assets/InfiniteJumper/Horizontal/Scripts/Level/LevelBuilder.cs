using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace InfiniteJumper.Horizontal
{
	/// <summary>
	/// Handles constructing (instantiating) the level chuncks.
	/// </summary>
	public class LevelBuilder
	{
		private Dictionary<char, GameObject> _tileLookUp = new Dictionary<char, GameObject> ();

		/// <summary>
		/// Initializes a new instance of the <see cref="InfiniteJumper.LevelBuilder"/> class.
		/// </summary>
		/// <param name="tileLookUp">Tile look up. Dictionary of GameObject prefabs along with their associated char representation in file.</param>
		public LevelBuilder (Dictionary<char, GameObject> tileLookUp)
		{
			_tileLookUp = tileLookUp;
		}

		/// <summary>
		/// Builds the level chunk at specified position.
		/// </summary>
		/// <returns>The chunk at position.</returns>
		/// <param name="position">Position.</param>
		/// <param name="level">Level.</param>
		public GameObject BuildChunkAtPosition (Vector2 position, LevelChunk level)
		{
			return BuildChunkAtPosition (position, level.levelGrid);
		}

		/// <summary>
		/// Builds the level chunk at specified position.
		/// </summary>
		/// <returns>The chunk at position.</returns>
		/// <param name="position">Position.</param>
		/// <param name="levelGrid">Level grid.</param>
		public GameObject BuildChunkAtPosition (Vector2 position, string[,] levelGrid)
		{
			if (levelGrid == null || levelGrid.GetLength (0) == 0 || levelGrid.GetLength (1) == 0) {
				return null;
			}

			GameObject obj = new GameObject ();
			for (int x = 0; x < levelGrid.GetLength (1); x++) {
				for (int y = 0; y < levelGrid.GetLength (0); y++) {
					
					var key = (char)levelGrid [y, x].ToLower () [0];
					if (_tileLookUp.ContainsKey (key)) {
						GameObject prefab = _tileLookUp [key];
						GameObject tile = (GameObject)MonoBehaviour.Instantiate (prefab, new Vector3 (position.x + (LevelChunk.WIDTH - 1) - x, 
						                                                                              				y - 1, -5), Quaternion.identity);
						tile.transform.SetParent (obj.transform);
					}
					
				}
				
			}
			
			return obj;
		}
	}
}

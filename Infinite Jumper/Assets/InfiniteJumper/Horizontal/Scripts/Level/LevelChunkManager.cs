using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace InfiniteJumper.Horizontal
{
	/// <summary>
	/// Builds new <see cref="InfiniteJumper.LevelChunks"/> when required. Stores current position and builds new chunks at current y position.
	/// </summary>
	public class LevelChunkManager : MonoBehaviour
	{
		/// <summary>
		/// The players y position is tracked to decide when to spawn new <see cref="InfiniteJumper.LevelChunks"/>. 
		/// </summary>
		public Transform player;

		/// <summary>
		/// The tile prefabs. A list of tile prefabs and associated char representation.
		/// </summary>
		public TilePrefab[] tilePrefabs;

		/// <summary>
		/// Number of <see cref="InfiniteJumper.LevelChunks"/> to load at start of game.
		/// </summary>
		public int chucksToPreLoad = 3;

		/// <summary>
		/// If true, all <see cref="InfiniteJumper.LevelChunks"/> data is loaded from the relevant files at game start.
		/// </summary>
		public bool preLoadLevelDataFromFile = true;

		private int _currentX = 0;
		private LevelChunk[] _chucks;

		private float _totalWeight;

		private LevelBuilder _levelBuilder;

		void Start ()
		{
			var tileLookUp = new Dictionary<char, GameObject> ();
			foreach (var t in tilePrefabs) {
				tileLookUp.Add (t.textRepresentation, t.prefab);
			}

			_levelBuilder = new LevelBuilder (tileLookUp);

			_chucks = GetComponents<LevelChunk> ();
			foreach (var c in _chucks) {
				if (preLoadLevelDataFromFile)
					c.Initialise ();
				_totalWeight += c.weight;
			}

			for (int i = 0; i < chucksToPreLoad; i++) {
				SpawnWeightAdjustedChunk ();
			}	
		}
	
		void Update ()
		{
			if (NewChuckRequired ()) {
				Debug.Log ("load new chunck");
				SpawnWeightAdjustedChunk ();
			}
		}

		/// <summary>
		/// If player is nearing the top of the current level chunk then it is time to spawn a new one.
		/// </summary>
		/// <returns><c>true</c>, if chuck required was newed, <c>false</c> otherwise.</returns>
		private bool NewChuckRequired ()
		{
			if (!player)
				return false;

			return player.position.x >= (_currentX - (LevelChunk.WIDTH * .8f));
		}

		private Vector2 GetCurrentPosition ()
		{
			return new Vector2 (_currentX, transform.position.y);
		}

		private void IncrementCurrentX ()
		{
			_currentX += LevelChunk.WIDTH;
		}

		private void SpawnWeightAdjustedChunk ()
		{
			var tiles = _levelBuilder.BuildChunkAtPosition (GetCurrentPosition (), _chucks [GetWeightAdjustedIndex ()]);
			tiles.transform.SetParent (transform);
			IncrementCurrentX ();
		}

		/// <summary>
		/// Returns an indec based on the associated weights for each level chunk. A higher weight will result in a higher chance of that
		/// level chuck being spawned.
		/// </summary>
		/// <returns>The weight adjusted index.</returns>
		private int GetWeightAdjustedIndex ()
		{
			if (_chucks.Length == 1) {
				return 0;
			}
			
			var randomIndex = -1;
			var random = Random.value * _totalWeight;
			
			for (int i = 0; i < _chucks.Length; ++i) {
				random -= _chucks [i].weight;
				
				if (random <= 0f) {
					randomIndex = i;
					break;
				}
			}
			
			return randomIndex;
		}
	}
}

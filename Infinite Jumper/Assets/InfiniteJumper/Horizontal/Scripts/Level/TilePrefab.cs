using UnityEngine;
using System.Collections;

namespace InfiniteJumper.Horizontal
{
	/// <summary>
	/// Used to map a GameObject to its character representation in file.
	/// All prefabs should be of the same size.
	/// </summary>
	[System.Serializable]
	public class TilePrefab
	{
		/// <summary>
		/// The tile prefab.
		/// </summary>
		public GameObject prefab;

		/// <summary>
		/// The character representation of the prefab in the level text files.
		/// </summary>
		public char textRepresentation;

	}
}

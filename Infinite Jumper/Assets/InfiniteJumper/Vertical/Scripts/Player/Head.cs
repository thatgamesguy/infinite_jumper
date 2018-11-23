using UnityEngine;
using System.Collections;

namespace InfiniteJumper.Verical
{
	/// <summary>
	/// Handles death of player when <see cref="InfiniteJumper.Darkness"/> enters trigger. Invokes <see cref="InfiniteJumper.LevelChunks.OnDeath"/>.
	/// </summary>
	public class Head : MonoBehaviour
	{
		public Player player;

		void OnTriggerEnter2D (Collider2D other)
		{
			if (other.gameObject.CompareTag ("Darkness")) {
				player.OnDeath ();
			}
		}
	}
}

using UnityEngine;
using System.Collections;

namespace InfiniteJumper.Horizontal
{
	/// <summary>
	/// Handles death of player when <see cref="InfiniteJumper.Darkness"/> enters trigger. Invokes <see cref="InfiniteJumper.LevelChunks.OnDeath"/>.
	/// </summary>
	public class Head : MonoBehaviour
	{
		public Player player;

		void Update ()
		{
			if (transform.position.y < -4) {
				player.OnDeath ();
			}
		}

		void OnTriggerEnter2D (Collider2D other)
		{
			if (other.gameObject.CompareTag ("Darkness")) {
				player.OnDeath ();
			}
		}
	}
}

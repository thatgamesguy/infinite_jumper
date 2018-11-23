using UnityEngine;
using System.Collections;

namespace InfiniteJumper.Verical
{
	/// <summary>
	/// Handles the translation of the gameobject that chases the player from the bottom of the screen.
	/// </summary>
	public class Darkness : MonoBehaviour
	{
		/// <summary>
		/// Base movement speed.
		/// </summary>
		public float movementSpeed;

		/// <summary>
		/// Movement speed is based the movementSpeed and the distance from player.
		/// </summary>
		public Transform player;

		/// <summary>
		/// Called in the event of the players death.
		/// </summary>
		public GameOver gameOverHandler;
		
		public bool canMove = false;

		private bool _deathHappened;
		private float _currentMovement;

		void Start ()
		{
			var renderer = GetComponent<Renderer> ();

			if (renderer) {
				var c = renderer.material.color;

				renderer.material.color = new Color (c.r, c.g, c.b, 0.9f);
			}
		}

		void LateUpdate ()
		{
			if (player) {
				_currentMovement = movementSpeed + ((player.position.y) * 0.008f);
			}
			
			if (player && player.transform.position.y > 5 && !canMove) {
				canMove = true;
			}
			
			if (canMove) {
				var pos = new Vector2 (0f, _currentMovement * Time.deltaTime);
				transform.Translate (pos, Space.World);
			}
			
			if (!player && !_deathHappened) {
				OnDeath ();
			}
			
		}

				
		private void OnDeath ()
		{
			_deathHappened = true;
			gameOverHandler.OnGameOver ();
		}

	}
}

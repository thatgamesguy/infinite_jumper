using UnityEngine;
using System.Collections;

namespace InfiniteJumper.Verical
{
	/// <summary>
	/// Attach to the platform tile prefab. Handles destruction when <see cref="InfiniteJumper.Darkness"/>  enters trigger.
	/// </summary>
	[RequireComponent (typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
	public class Platform : MonoBehaviour
	{
		public GameObject explosionParticleContainer;
		private Collider2D[] _colliders;
		private Rigidbody2D _rigidbody;
		private SpriteRenderer _renderer;

		public bool playerTouched { private get; set; }


		private bool _beingDestroyed = false;
		public bool beingDestroyed { get { return _beingDestroyed; } }

		public bool falling { get; private set; }

		void Awake ()
		{
			_colliders = GetComponents<Collider2D> ();
			_rigidbody = GetComponent<Rigidbody2D> ();
			_renderer = GetComponent<SpriteRenderer> ();
		}

		void OnTriggerEnter2D (Collider2D other)
		{
			if (other.gameObject.CompareTag ("Darkness")) {

				_beingDestroyed = true;

				if (!falling) {
					DisableColliderCollisions ();

					_rigidbody.isKinematic = false;
				}


				_renderer.enabled = false;

				if (explosionParticleContainer) {
					explosionParticleContainer.SetActive (true);
				}
				
				StartCoroutine (Destroy ());

			}
		}

		void OnTriggerExit2D (Collider2D other)
		{
			if (playerTouched && other.gameObject.CompareTag ("Player")) {
				DisableColliderCollisions ();
				_rigidbody.isKinematic = false;

				falling = true;
			}
		}

		private void DisableColliderCollisions ()
		{
			foreach (var c in _colliders) {
				c.isTrigger = true;
			}
		}
			
		private IEnumerator Destroy ()
		{
			yield return new WaitForSeconds (2f);
			Destroy (gameObject);
		}
	}
}
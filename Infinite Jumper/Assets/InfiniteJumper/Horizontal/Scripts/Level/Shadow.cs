using UnityEngine;
using System.Collections;

namespace InfiniteJumper.Horizontal
{
	/// <summary>
	/// Attach to the shadow tile. Destroys this gameobject if its associated platform tile is destroyed.
	/// </summary>
	public class Shadow : MonoBehaviour
	{
		public SpriteRenderer spriteRenderer;

		private Platform _owner;
		private bool _parentSet = false;

		private Color _initialColour;

		void Awake ()
		{
			_initialColour = spriteRenderer.color;
		}
				

		void Update ()
		{
			if (_owner) {

				if (_owner.beingDestroyed) {
					Destroy (transform.parent.gameObject);
				} else if (_owner.falling && spriteRenderer.color.a > 0.15f) {
					spriteRenderer.color = new Color (_initialColour.r, _initialColour.g, _initialColour.b, spriteRenderer.color.a - Time.deltaTime * 2f);
				}
			}

		}

		void OnTriggerEnter2D (Collider2D other)
		{
			if (_owner != null || _parentSet) {
				return;
			}

			if (other.gameObject.CompareTag ("Platform")) {
				transform.parent.SetParent (other.transform);
				_owner = other.gameObject.GetComponent<Platform> ();
			} else if (other.gameObject.CompareTag ("Shadow")) {
				transform.parent.SetParent (other.transform);
				_parentSet = true;
			}
		}

		private IEnumerator FadeOut ()
		{
			while (spriteRenderer.color.a > 0.3) {
				spriteRenderer.color = new Color (_initialColour.r, _initialColour.g, _initialColour.b, spriteRenderer.color.a - .6f);
				yield return new WaitForEndOfFrame (); 
			}

			Destroy (transform.parent.gameObject);

		}
	}
}

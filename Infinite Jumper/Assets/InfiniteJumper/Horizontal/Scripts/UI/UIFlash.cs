using UnityEngine;
using System.Collections;

namespace InfiniteJumper.Horizontal
{
	/// <summary>
	/// User interface flash. Acts as an overlay for the main menu and gameplay scene.
	/// </summary>
	[RequireComponent (typeof(GUITexture))]
	public class UIFlash : MonoBehaviour
	{
		/// <summary>
		/// Overlay behaviour is different for menu and game scene. For menu, the overlay is less translucent.
		/// </summary>
		public bool isMenu;
		private bool gameOver;

		private GUITexture _texture;

		void Start ()
		{
			_texture = GetComponent<GUITexture> ();

			var c = _texture.color;
			_texture.color = new Color (c.r, c.g, c.b, 0.5f);
		}

		void Update ()
		{
			var c = _texture.color;

			if (isMenu != true) {
				if (_texture.color.a > 0 && gameOver == false) {
					_texture.color = new Color (c.r, c.g, c.b, c.a - Time.deltaTime / 2);
				}

				if (_texture.color.a <= 0.35f && gameOver == true) {
					_texture.color = new Color (c.r, c.g, c.b, c.a + Time.deltaTime);
				}

				if (_texture.color.a <= 0 && _texture.enabled == true && gameOver == false) {
					_texture.enabled = false;
				}

				if (_texture.color.a > 0 && _texture.enabled == false && gameOver == false) {
					_texture.enabled = true;
				}
			} else {
				if (_texture.color.a > 0.35) {
					_texture.color = new Color (c.r, c.g, c.b, c.a - Time.deltaTime / 2);
				}
			}
		}

		/// <summary>
		/// Games the over user interface flash.
		/// </summary>
		public void GameOverUIFlash ()
		{
			var c = _texture.color;
			_texture.color = new Color (c.r, c.g, c.b, 0f);

			_texture.enabled = true;

			gameOver = true;

		}
	}
}
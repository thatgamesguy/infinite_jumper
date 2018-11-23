using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace InfiniteJumper.Verical
{
	/// <summary>
	/// Handles game over state. On game over: enables game over ui and saves high score.
	/// </summary>
	public class GameOver : MonoBehaviour
	{

		public UIFlash uiFlash;
		public Score score;
		public GameObject[] gameOverUI;
		public GameObject[] controls;

		private bool _gameOver;

		void Awake ()
		{
			SetGameObjectVisibility (controls, true);
			SetGameObjectVisibility (gameOverUI, false);
		}

		void Update ()
		{
			if (_gameOver && Input.anyKeyDown) {
				SceneManager.LoadScene ("Game_Vertical");
			}
		}

		/// <summary>
		/// Raised on game over. Shows game over ui and saves high score.
		/// </summary>
		public void OnGameOver ()
		{
			_gameOver = true;
			uiFlash.GameOverUIFlash ();

			if (DataPersistence.instance)
				DataPersistence.instance.Save (score.currentScore);

			SetGameObjectVisibility (gameOverUI, true);
			SetGameObjectVisibility (controls, false);
		}

		private void SetGameObjectVisibility (GameObject[] objs, bool visibility)
		{
			foreach (var g in objs) {
				g.SetActive (visibility);
			}
		}

	}
}

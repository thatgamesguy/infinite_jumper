using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace InfiniteJumper.Verical
{
	/// <summary>
	/// Score UI component for main game scene.
	/// </summary>
	[RequireComponent (typeof(Text))]
	public class Score : MonoBehaviour
	{
		private int _currentScore;

		/// <summary>
		/// Gets the current score.
		/// </summary>
		/// <value>The current score.</value>
		public int currentScore { get { return _currentScore; } }

		private Text _text;

		void Awake ()
		{
			_text = GetComponent<Text> ();
		}

		/// <summary>
		/// Increments the score by amount. Updates UI.
		/// </summary>
		/// <param name="amount">Amount.</param>
		public void IncrementScore (int amount)
		{
			_currentScore += amount;
			UpdateUI ();
		}

		/// <summary>
		/// Sets the score. Updates UI.
		/// </summary>
		/// <param name="score">Score.</param>
		public void SetScore (int score)
		{
			_currentScore = score;
			UpdateUI ();
		}

		private void UpdateUI ()
		{
			_text.text = "" + _currentScore;
		}
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace InfiniteJumper.Verical
{
	/// <summary>
	/// Updates highscore UI on main menu.
	/// </summary>
	[RequireComponent (typeof(Text))]
	public class HighScore : MonoBehaviour
	{
		private Text _text;

		private static readonly string HIGHSCORE_PRE_TEXT = "Highscore: ";

		// Use this for initialization
		void Start ()
		{
			_text = GetComponent<Text> ();

			DataPersistence.instance.Load ();

			_text.text = HIGHSCORE_PRE_TEXT + DataPersistence.instance.Score;
		}

	}
}

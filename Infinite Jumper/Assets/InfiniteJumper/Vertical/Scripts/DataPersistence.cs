using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace InfiniteJumper.Verical
{
	/// <summary>
	/// Data persistence. Handles saving and loading of high score.
	/// </summary>
	public class DataPersistence : MonoBehaviour
	{
		/// <summary>
		/// Gets the high score. Loaded from file.
		/// </summary>
		/// <value>The score.</value>
		public int Score { get; private set; }
		
		private static readonly string FILE_PATH_POST = "/leveldata.dat";
		
		private static DataPersistence _instance;

		/// <summary>
		/// Gets the instance of this class. Can be accessed from any script.
		/// </summary>
		/// <value>The instance.</value>
		public static DataPersistence instance { get { return _instance; } }
		
		void Awake ()
		{
			if (!_instance) {
				_instance = this;
				DontDestroyOnLoad (gameObject);
			} else if (_instance != this) {
				_instance = this;
			}
			
		}
		
		/// <summary>
		/// Load the highscore from file.
		/// </summary>
		public void Load ()
		{
			
			if (File.Exists (Application.persistentDataPath + FILE_PATH_POST)) {
				Debug.Log ("Loading Data");
				
				var bf = new BinaryFormatter ();
				var file = File.Open (Application.persistentDataPath + FILE_PATH_POST, FileMode.Open);
				
				ScoreData data = null;
				
				var dataObj = bf.Deserialize (file);
				
				if (dataObj is ScoreData) {
					data = (ScoreData)dataObj;
				} else {
					Debug.Log ("Attempted to load data for vertical game. File load cancelled.");
				}

				
				file.Close ();

				if (data != null) {
					this.Score = data.Round;
				}
			}
			
		}

		/// <summary>
		/// If score greater that store highscore then it is saved to file.
		/// </summary>
		/// <param name="score">Score.</param>
		public void Save (int score)
		{
			if (score > this.Score) {
				Debug.Log ("Saving Data");
				
				var bf = new BinaryFormatter ();
				var file = File.Create (Application.persistentDataPath + FILE_PATH_POST);
				
				var data = new ScoreData (score);
				
				bf.Serialize (file, data);
				file.Close ();
			}
		}
	}

	/// <summary>
	/// Class to store serializable score data.
	/// </summary>
	[System.Serializable]
	class ScoreData
	{
		public int Round { get; private set; }
		
		public ScoreData (int round)
		{
			Round = round;
		}
	}
}


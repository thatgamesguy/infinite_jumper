using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace InfiniteJumper.Horizontal
{
	/// <summary>
	/// Plays background audio clip if present. Loads game scene if any key is pressed.
	/// </summary>
	[RequireComponent (typeof(AudioSource))]
	public class MainMenu : MonoBehaviour
	{
		public AudioClip backgroundAudio;
		public FollowTarget cameraFollow;
		public Darkness darkness;

		private AudioSource _audio;

		// Use this for initialization
		void Start ()
		{
			_audio = GetComponent<AudioSource> ();

			if (backgroundAudio != null) {
				_audio.clip = backgroundAudio;
				_audio.loop = true;
				_audio.Play ();
			}
		}
	
		void Update ()
		{
			if (Input.anyKeyDown) {
				darkness.movementSpeed *= 3f;
				cameraFollow.Disable ();
				StartCoroutine (LoadGame ());
			}
		}

		private IEnumerator LoadGame ()
		{
			yield return new WaitForSeconds (1.5f);
            
            SceneManager.LoadScene ("Game_Horizontal");
		}
	}
}

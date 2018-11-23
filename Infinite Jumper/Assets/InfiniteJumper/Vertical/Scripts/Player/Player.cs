using UnityEngine;
using System.Collections;

namespace InfiniteJumper.Verical
{
	/// <summary>
	/// The main player movement script, also updates current score based on the players y position. 
	/// </summary>
	public class Player : MonoBehaviour
	{

		[Header("Player Movement")]
		/// <summary>
				/// Maximum left/right movement speed.
				/// </summary>
				public float
			maxSpeed = 600f;
		
		/// <summary>
		/// The force applied when the player jumps.
		/// </summary>
		public float jumpForce = 700f;

		[Header ("Player Physics")]
		/// <summary>
				/// The location to perform a check to see if the player is touching the ground.
				/// </summary>
				public Transform
			groundCheck;
				
		/// <summary>
		/// The layer mask for tiles that the player can stand on.
		/// </summary>
		public LayerMask groundMask;

		[Header ("Audio")]
		/// <summary>
				/// Audio clips to play when the player jumps.
				/// </summary>
				public  AudioClip[]
			jumpClips;

		/// <summary>
		/// Clips to play as the player walks on the ground.
		/// </summary>
		public AudioClip[] walkClips;

		/// <summary>
		/// Audio clip to play on death.
		/// </summary>
		public AudioClip[] audioOnDeath;
				
		[Header("Animation Scale")]
		/// <summary>
				/// Player scale when on groud.
				/// </summary>
				public Vector3
			groundScale;
		public Vector3 jumpingScale;

		[Header ("Particles")]
		/// <summary>
				/// The explosion particle container. The gameobject the holds the particle system used when the player dies and explods.
				/// </summary>
				public GameObject
			explosionParticleContainer;

		/// <summary>
		/// The land particle. Any particle system for when the player lands on the ground after a jump.
		/// </summary>
		public ParticleSystem landParticle;

		[Header ("Misc")]
		/// <summary>
				/// Used to update the current score and the corresponding text.
				/// </summary>
				public Score
			score;
		
		/// <summary>
		/// The gameobject that holds the players sprite. This is disabled when the player dies.
		/// </summary>
		public GameObject playerSpriteContainer;
		
		private bool _isGrounded = false;
		private bool _isGameOver = false;
		private	bool _canDoubleJump = false;
		private AudioSource _audio;
		private Rigidbody2D _rigidbody;
		private bool _groundedLastUpdate;
		private float _move = 0f;

		private bool _moveLeft;
		private bool _moveRight;

		private Collider2D _previousCollision;

		void Start ()
		{
			_audio = GetComponent<AudioSource> ();
			_rigidbody = GetComponent<Rigidbody2D> ();
		}

		void Update ()
		{
			if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.LeftArrow)) {
				MoveLeft ();
			}

			if (Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.LeftArrow)) {
				StopMovingLeft ();
			}

			if (Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.RightArrow)) {
				MoveRight ();
			}

			if (Input.GetKeyUp (KeyCode.D) || Input.GetKeyUp (KeyCode.RightArrow)) {
				StopMovingRight ();
			}

			if (Input.GetButtonDown ("Jump")) {
				Jump ();
			}

			int currentScore = (int)Mathf.Floor (transform.position.y) - 2;

			if (currentScore >= 0 && currentScore > score.currentScore && _isGrounded) { 
				score.SetScore (currentScore);
				
			}


		}
	
		void FixedUpdate ()
		{	
			var hit = Physics2D.Linecast (transform.position, groundCheck.position, groundMask);

			_isGrounded = hit.collider != null || _rigidbody.velocity.y == 0;

			if (_isGrounded && hit.collider != null && hit.collider != _previousCollision) {
				var platform = hit.collider.gameObject.GetComponent<Platform> ();

				if (platform) {
					platform.playerTouched = true;
				}
				_previousCollision = hit.collider;
			}


			if (_moveLeft) {
				_move = -1f;
			} else if (_moveRight) {
				_move = 1f;
			}

			if (!_isGameOver && (_moveLeft || _moveRight)) {
				_rigidbody.velocity = new Vector2 (_move * maxSpeed * Time.fixedDeltaTime, _rigidbody.velocity.y);


			} 

					

			if (_isGrounded && !_groundedLastUpdate) {
				landParticle.Emit (50);

			}

			_groundedLastUpdate = _isGrounded;

			if (walkClips.Length > 0 && _isGrounded && (_moveLeft || _moveRight)) {
				_audio.PlayOneShot (walkClips [Random.Range (0, walkClips.Length)]);
			}

		}

		/// <summary>
		/// Move character left in next call to FixedUpdate. Called on button down.
		/// </summary>
		public void MoveLeft ()
		{
			_moveLeft = true;
		}

		/// <summary>
		/// Stop moving the character left. Called on button up.
		/// </summary>
		public void StopMovingLeft ()
		{
			_moveLeft = false;
			_rigidbody.velocity = new Vector2 (0, _rigidbody.velocity.y);
		}

		/// <summary>
		/// Move character right in next call to FixedUpdate. Called on button down.
		/// </summary>
		public void MoveRight ()
		{
			_moveRight = true;
		}

		/// <summary>
		/// Stop moving the character left. Called on button up.
		/// </summary>
		public void StopMovingRight ()
		{
			_moveRight = false;
			_rigidbody.velocity = new Vector2 (0, _rigidbody.velocity.y);
		}

		/// <summary>
		/// Makes the character jump (if on ground) or double jump (if already in the air and has not already double jumped).
		/// </summary>
		public void Jump ()
		{


			if (_isGrounded && !_isGameOver) {
				_rigidbody.AddForce (new Vector2 (0, jumpForce));
				_canDoubleJump = true;
				
				if (jumpClips.Length > 0)
					_audio.PlayOneShot (jumpClips [Random.Range (0, jumpClips.Length)]);

				StartCoroutine (JumpAnimation ());
			} else if (_canDoubleJump && !_isGameOver) {
				_rigidbody.velocity = new Vector2 (_rigidbody.velocity.x, 0);
				_rigidbody.AddForce (new Vector2 (0, jumpForce));
				_canDoubleJump = false;
				
				if (jumpClips.Length > 0)
					_audio.PlayOneShot (jumpClips [Random.Range (0, jumpClips.Length)]);

				StartCoroutine (JumpAnimation ());
			}
		}

		/// <summary>
		/// Called on death event. Plays <see cref="InfiniteJumper.Player.audioOnDeath"/>, disables player sprite and enables on death particles.
		/// Destroys object after a set amount of time.
		/// </summary>
		public void OnDeath ()
		{
			if (audioOnDeath.Length > 0)
				_audio.PlayOneShot (audioOnDeath [Random.Range (0, audioOnDeath.Length)]);

			playerSpriteContainer.SetActive (false);
			explosionParticleContainer.SetActive (true);
					
			StartCoroutine (Destroy ());
		}


		private IEnumerator Destroy ()
		{
			yield return new WaitForSeconds (0.3f);
			Destroy (gameObject);
		}

		private IEnumerator JumpAnimation ()
		{
			transform.localScale = jumpingScale;

			yield return new WaitForSeconds (0.2f);

			transform.localScale = groundScale;

		}
	}
}

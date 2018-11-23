using UnityEngine;
using System.Collections;

namespace InfiniteJumper.Horizontal
{
	/// <summary>
	/// Attach to camera to smoothly track player. 
	/// </summary>
	public class FollowTarget : MonoBehaviour
	{
		public Transform target;
		public float xSmoothing;
		public float xMargin;

		private bool _disabled;
		
		void LateUpdate ()
		{	
			if (target && !_disabled) {
				TrackPlayer ();
			}
		}

		public void Disable ()
		{
			_disabled = true;
		}

		private bool CheckYMargin ()
		{
			return Mathf.Abs (transform.position.x - target.transform.position.x) > xMargin;
		}
	
		private void TrackPlayer ()
		{
			
			var targetX = transform.position.x;
			
			if (CheckYMargin ()) {
				targetX = Mathf.Lerp (transform.position.x, target.transform.position.x, xSmoothing * Time.deltaTime);
			}
			
			transform.position = new Vector3 (targetX, 5f, transform.position.z);
			
		}
	}
}

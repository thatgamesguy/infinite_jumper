using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace InfiniteJumper.Verical
{
	/// <summary>
	/// Simple fade in script for CanvasRenderer.
	/// </summary>
	[RequireComponent (typeof(CanvasRenderer))]
	public class FadeIn : MonoBehaviour
	{

		private CanvasRenderer _cRenderer;

		// Use this for initialization
		void Start ()
		{
			_cRenderer = GetComponent<CanvasRenderer> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
			if (_cRenderer.GetAlpha () > 0) {
				_cRenderer.SetAlpha (_cRenderer.GetAlpha () - 1 * Time.deltaTime);
			}
		}
	}
}

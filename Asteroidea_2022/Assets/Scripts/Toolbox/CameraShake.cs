using System.Collections;
using UnityEngine;

namespace Toolbox
{
	public class CameraShake : MonoBehaviour
	{
		[Header("Camera data"), Tooltip("Transform of the camera to shake, grabs the gameObject's transform if null")]
		[SerializeField] private Transform mainCamera = null;
		[Tooltip("How long the camera should shake for")]
		[SerializeField] private float shakeDuration = 0f;
		[Tooltip("Amplitude of the shake, a larger value shakes the camera harder")]
		[SerializeField] private float shakeAmount = 0.7f;
		[Tooltip("Rate at which shake decreases")]
		[SerializeField] private float decreaseFactor = 1.0f;

		/// If the camera is shaking or not
		private IEnumerator shake;

		public void StartShake()
        {

			if (shake != null) StopCoroutine(shake);
			shake = Shake();
			StartCoroutine(shake);
		}

		private IEnumerator Shake()
        {
			Vector3 originalPos = Vector3.zero;
			float shakeTimer = 0;

			originalPos = mainCamera.localPosition;
			shakeTimer = shakeDuration;

			while (shakeTimer > 0)
			{
				mainCamera.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
				shakeTimer -= Time.deltaTime * decreaseFactor;
				yield return null;
			}

			shakeTimer = 0f;
			mainCamera.localPosition = originalPos;
        }
	}
}
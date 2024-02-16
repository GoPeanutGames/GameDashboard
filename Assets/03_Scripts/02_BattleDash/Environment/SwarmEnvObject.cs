using PeanutDashboard._02_BattleDash.Events;
using PeanutDashboard.Utils.Misc;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PeanutDashboard._02_BattleDash.Environment
{
	public class SwarmEnvObject: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private float _minSpeed;
		
		[SerializeField]
		private float _maxSpeed;
		
		[SerializeField]
		private float _minScale;
		
		[SerializeField]
		private float _maxScale;
		
		[SerializeField]
		private AudioClip _clip;
		
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private float _currentSpeed;
		
		[SerializeField]
		private bool _clipPlayed;

#if !SERVER
		private void Awake()
		{
			_currentSpeed = Random.Range(_minSpeed, _maxSpeed);
			float scale = Random.Range(_minScale, _maxScale);
			this.transform.localScale = new Vector3(scale, scale, 1);
		}

		private void Update()
		{
			this.transform.Translate(Vector3.left * (_currentSpeed * Time.deltaTime));
			if (!_clipPlayed && this.transform.position.x < 0){
				_clipPlayed = true;
				BattleDashAudioEvents.RaisePlaySfxEvent(_clip,1f);
			}
		}
#endif
	}
}
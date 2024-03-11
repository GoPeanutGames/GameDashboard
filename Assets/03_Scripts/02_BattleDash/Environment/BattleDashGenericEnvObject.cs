using PeanutDashboard.Utils.Misc;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PeanutDashboard._02_BattleDash.Environment
{
	public class BattleDashGenericEnvObject: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private float _minSpeed;
		
		[SerializeField]
		private float _maxSpeed;
		
		[Header(InspectorNames.DebugDynamic)]
		[SerializeField]
		private float _currentSpeed;

#if !SERVER
		private void Awake()
		{
			_currentSpeed = Random.Range(_minSpeed, _maxSpeed);
		}

		private void Update()
		{
			this.transform.Translate(Vector3.left * (_currentSpeed * Time.deltaTime));
		}
#endif
	}
}
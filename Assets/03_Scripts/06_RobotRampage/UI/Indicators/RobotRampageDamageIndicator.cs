using System;
using System.Collections;
using PeanutDashboard.Utils.Misc;
using TMPro;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	public class RobotRampageDamageIndicator: MonoBehaviour
	{
		[Header(InspectorNames.SetInInspector)]
		[SerializeField]
		private TMP_Text _text;

		private void Start()
		{
			StartCoroutine(MoveUp());
			StartCoroutine(Destroy());
		}

		private IEnumerator MoveUp()
		{
			while (true){
				this.transform.Translate(0,Time.deltaTime / 2f, 0);
				yield return null;
			}
		}

		private IEnumerator Destroy()
		{
			yield return new WaitForSeconds(2);
			Destroy(this.gameObject);
		}
		
		public void SetIndicator(float damage)
		{
			_text.text = $"{Mathf.FloorToInt(damage)}";
		}
	}
}
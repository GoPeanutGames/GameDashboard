using System;
using System.Collections.Generic;
using UnityEngine;

namespace PeanutDashboard._06_RobotRampage
{
	[Serializable]
	public class UpgradeLevel
	{
		[SerializeField]
		public int level;

		[SerializeField]
		public List<StatData> statDatas;
	}
}
namespace PeanutDashboard._06_RobotRampage
{
	public static class RobotRampageSortOrderService
	{
		private const int SortOrderBackground = 0;
		private const int SortOrderBackgroundDecor = 1;
		private const int SortOrderBackgroundProp = 3;
		private const int SortOrderScrap = 10;
		private const int SortOrderDrop = 12;
		private const int SortOrderShadow = 18;
		private const int SortOrderPlayer = 20;
		private const int SortOrderMob = 21;
		private const int SortOrderWeaponEffect = 30;
		private const int SortOrderWeaponBullet = 40;
		
		public static int GetSortOrderForType(RobotRampageSortOrderType robotRampageSortOrderType)
		{
			switch (robotRampageSortOrderType){
				case RobotRampageSortOrderType.Background:
					return SortOrderBackground;
				case RobotRampageSortOrderType.BackgroundDecor:
					return SortOrderBackgroundDecor;
				case RobotRampageSortOrderType.BackgroundProp:
					return SortOrderBackgroundProp;
				case RobotRampageSortOrderType.Scrap:
					return SortOrderScrap;
				case RobotRampageSortOrderType.Drop:
					return SortOrderDrop;
				case RobotRampageSortOrderType.Shadow:
					return SortOrderShadow;
				case RobotRampageSortOrderType.Player:
					return SortOrderPlayer;
				case RobotRampageSortOrderType.Mob:
					return SortOrderMob;
				case RobotRampageSortOrderType.WeaponEffect:
					return SortOrderWeaponEffect;
				case RobotRampageSortOrderType.WeaponBullet:
					return SortOrderWeaponBullet;
			}
			return 0;
		}
	}
}
namespace PeanutDashboard._02_BattleDash
{
	public static class BattleDashConfig
	{
#if SERVER
		public const float BulletSpeed = 12;
		public const float MovementSpeed = 6;
		public const float MovementAccelTime = 0.2f;
		public const float MovementDecTime = 0.1f;
		public const float MovementDirectionChangeTime = 0.1f;
		public const float ShootingCooldown = 0.75f;
#endif
	}
}
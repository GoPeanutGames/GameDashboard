namespace PeanutDashboard._02_BattleDash
{
	public static class BattleDashConfig
	{
#if SERVER
		public const float DefaultAreaSpeed = 2;
		public const float BulletSpeed = 14;
		public const float BulletLifetime = 8;
		public const float MovementSpeed = 7;
		public const float MovementAccelTime = 0.2f;
		public const float MovementDecTime = 0.1f;
		public const float MovementDirectionChangeTime = 0.1f;
		public const float ShootingCooldown = 1f;
#endif
	}
}
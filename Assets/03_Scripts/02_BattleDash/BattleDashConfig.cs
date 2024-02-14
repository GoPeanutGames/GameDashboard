namespace PeanutDashboard._02_BattleDash
{
	public static class BattleDashConfig
	{
#if SERVER
		public const float DefaultAreaSpeed = 2;
		public const float BulletSpeed = 15;
		public const float BulletLifetime = 5;
		public const int BlummerBulletDamage = 1;
		public const float MovementSpeed = 8;
		public const float MovementAccelTime = 0.2f;
		public const float MovementDecTime = 0.1f;
		public const float MovementDirectionChangeTime = 0.1f;
		public const float ShootingCooldown = 1f;
#endif
	}
}
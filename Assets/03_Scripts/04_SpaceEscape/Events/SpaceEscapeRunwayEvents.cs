using PeanutDashboard._04_SpaceEscape.Model;
using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._04_SpaceEscape.Events
{
    public static class SpaceEscapeRunwayEvents
    {
        private static UnityAction<SpaceEscapeRunwayPartSide> _spawnRunwayAt;
		
        public static event UnityAction<SpaceEscapeRunwayPartSide> SpawnRunwayAt
        {
            add => _spawnRunwayAt += value;
            remove => _spawnRunwayAt -= value;
        }
        
        public static void RaiseSpawnRunwayAtEvent(SpaceEscapeRunwayPartSide partSide)
        {
            if (_spawnRunwayAt == null){
                LoggerService.LogWarning($"{nameof(SpaceEscapeRunwayEvents)}::{nameof(RaiseSpawnRunwayAtEvent)} raised, but nothing picked it up");
                return;
            }
            _spawnRunwayAt.Invoke(partSide);
        }
    }
}
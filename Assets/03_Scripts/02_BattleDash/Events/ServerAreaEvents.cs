using PeanutDashboard.Shared.Logging;
using UnityEngine.Events;

namespace PeanutDashboard._02_BattleDash.Events
{
    public static class ServerAreaEvents
    {
        private static UnityAction<float> _areaDistancePassedPercUpdated;
        private static UnityAction _areaSpawnDoor;
        private static UnityAction _areaSpawnNextArea;
        
        public static event UnityAction<float> AreaDistancePassedPercUpdated
        {
            add => _areaDistancePassedPercUpdated += value;
            remove => _areaDistancePassedPercUpdated -= value;
        }
        
        public static event UnityAction AreaSpawnDoor
        {
            add => _areaSpawnDoor += value;
            remove => _areaSpawnDoor -= value;
        }
        
        public static event UnityAction AreaSpawnNextArea
        {
            add => _areaSpawnNextArea += value;
            remove => _areaSpawnNextArea -= value;
        }
        
        public static void RaiseAreaDistancePassedPercUpdatedEvent(float perc)
        {
            if (_areaDistancePassedPercUpdated == null){
                LoggerService.LogWarning($"{nameof(ServerAreaEvents)}::{nameof(RaiseAreaDistancePassedPercUpdatedEvent)} raised, but nothing picked it up");
                return;
            }
            _areaDistancePassedPercUpdated.Invoke(perc);
        }
        
        public static void RaiseAreaSpawnDoorEvent()
        {
            if (_areaSpawnDoor == null){
                LoggerService.LogWarning($"{nameof(ServerAreaEvents)}::{nameof(RaiseAreaSpawnDoorEvent)} raised, but nothing picked it up");
                return;
            }
            _areaSpawnDoor.Invoke();
        }
        
        public static void RaiseAreaSpawnNextAreaEvent()
        {
            if (_areaSpawnNextArea == null){
                LoggerService.LogWarning($"{nameof(ServerAreaEvents)}::{nameof(RaiseAreaSpawnNextAreaEvent)} raised, but nothing picked it up");
                return;
            }
            _areaSpawnNextArea.Invoke();
        }
    }
}
using PeanutDashboard._04_SpaceEscape.Events;
using PeanutDashboard._04_SpaceEscape.Model;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._04_SpaceEscape.Controllers
{
    public class SpaceEscapeRunwaySpawner : MonoBehaviour
    {
        [Header(InspectorNames.SetInInspector)]
        [SerializeField]
        private GameObject _runwayPartPrefabLeft;
        [SerializeField]
        private GameObject _runwayPartPrefabRight;
        [SerializeField]
        private GameObject _runwayPartPrefabCenter;

        [SerializeField]
        private GameObject _leftSpawnPoint;

        [SerializeField]
        private GameObject _centralSpawnPoint;

        [SerializeField]
        private GameObject _rightSpawnPoint;

        private void OnEnable()
        {
            SpaceEscapeRunwayEvents.SpawnRunwayAt += OnSpawnNewRunwayPart;
        }

        private void OnDisable()
        {
            SpaceEscapeRunwayEvents.SpawnRunwayAt -= OnSpawnNewRunwayPart;
        }

        private void OnSpawnNewRunwayPart(SpaceEscapeRunwayPartSide partSide)
        {
            switch (partSide)
            {
                case SpaceEscapeRunwayPartSide.Left:
                    GameObject runwayPart = Instantiate(_runwayPartPrefabLeft, _leftSpawnPoint.transform.position, Quaternion.identity);
                    runwayPart.GetComponent<SpaceEscapeRunwayPart>().Initialise(SpaceEscapeRunwayPartSide.Left);
                    break;
                case SpaceEscapeRunwayPartSide.Center:
                    GameObject runwayPart2 = Instantiate(_runwayPartPrefabCenter, _centralSpawnPoint.transform.position, Quaternion.identity);
                    runwayPart2.GetComponent<SpaceEscapeRunwayPart>().Initialise(SpaceEscapeRunwayPartSide.Center);
                    break;
                case SpaceEscapeRunwayPartSide.Right:
                    GameObject runwayPart3 = Instantiate(_runwayPartPrefabRight, _rightSpawnPoint.transform.position, Quaternion.identity);
                    runwayPart3.GetComponent<SpaceEscapeRunwayPart>().Initialise(SpaceEscapeRunwayPartSide.Right);
                    break;
            }
        }
        
        private GameObject GetPrefabForSide(SpaceEscapeRunwayPartSide runwayPartSide)
        {
            switch (runwayPartSide){
                case SpaceEscapeRunwayPartSide.Right:
                    return _runwayPartPrefabRight;
                case SpaceEscapeRunwayPartSide.Left:
                    return _runwayPartPrefabLeft;
                case SpaceEscapeRunwayPartSide.Center:
                    return _runwayPartPrefabCenter;
            }
            return null;
        }
    }
}


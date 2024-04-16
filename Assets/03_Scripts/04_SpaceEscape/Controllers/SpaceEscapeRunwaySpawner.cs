using System;
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
        private GameObject _runwayPartPrefab;

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
                    Instantiate(_runwayPartPrefab, _leftSpawnPoint.transform.position, Quaternion.identity);
                    break;
                case SpaceEscapeRunwayPartSide.Center:
                    Instantiate(_runwayPartPrefab, _centralSpawnPoint.transform.position, Quaternion.identity);
                    break;
                case SpaceEscapeRunwayPartSide.Right:
                    Instantiate(_runwayPartPrefab, _rightSpawnPoint.transform.position, Quaternion.identity);
                    break;
            }
        }
    }
}



using PeanutDashboard._04_SpaceEscape.Events;
using PeanutDashboard._04_SpaceEscape.Model;
using PeanutDashboard.Utils.Misc;
using UnityEngine;

namespace PeanutDashboard._04_SpaceEscape.Controllers
{
    public class SpaceEscapeRunwayPart : MonoBehaviour
    {
        [Header(InspectorNames.SetInInspector)]
        [SerializeField]
        private SpaceEscapeRunwayPartSide _startingSide;

        public void Initialise(SpaceEscapeRunwayPartSide partSide)
        {
            _startingSide = partSide;
        }
        
        private void Update()
        {
            this.transform.Translate(Vector3.back *Time.deltaTime * 2f);
            if (this.transform.position.z < -10)
            {
                SpaceEscapeRunwayEvents.RaiseSpawnRunwayAtEvent(_startingSide);
                Destroy(this.gameObject);
            }
        }
    }
}


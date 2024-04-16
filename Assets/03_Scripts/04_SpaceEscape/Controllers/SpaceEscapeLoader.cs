using PeanutDashboard.Shared;
using UnityEngine;

namespace PeanutDashboard._04_SpaceEscape.Controllers
{
    public class SpaceEscapeLoader : MonoBehaviour
    {
        private void Start()
        {
            if (!AddressablesService.Instance.AreAddressablesInitialised()){
                AddressablesService.Instance.InitialiseAddressables();
            }
        }
    }
}

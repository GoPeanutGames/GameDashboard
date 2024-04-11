using PeanutDashboard.Shared;
using UnityEngine;

namespace PeanutDashboard._04_RunnerGame.Controllers
{
    
    public class RunnerGameLoader : MonoBehaviour
    {
        private void Start()
        {
            if (!AddressablesService.Instance.AreAddressablesInitialised()){
                AddressablesService.Instance.InitialiseAddressables();
            }
        }
    }
}

#if STEAMWORKS
using Steamworks;
using UnityEngine;

namespace eviltwo.UnityExtensions.SteamworksNET.Debugs
{
    public class SteamConnectionCheckDebug : MonoBehaviour
    {
        private void Start()
        {
            if (SteamManager.Initialized)
            {
                var userName = SteamFriends.GetPersonaName();
                Debug.Log($"Steam connected. User: {userName}");
            }
            else
            {
                Debug.Log("Steam not connected.");
            }
        }
    }
}
#endif
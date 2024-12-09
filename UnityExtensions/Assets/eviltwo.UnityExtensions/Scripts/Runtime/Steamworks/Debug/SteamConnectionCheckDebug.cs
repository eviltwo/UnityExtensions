using UnityEngine;

#if STEAMWORKS && !DISABLESTEAMWORKS
using Steamworks;
#endif

namespace eviltwo.UnityExtensions.SteamworksNET.Debugs
{
    public class SteamConnectionCheckDebug : MonoBehaviour
    {
#if STEAMWORKS && !DISABLESTEAMWORKS
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
#endif
    }
}

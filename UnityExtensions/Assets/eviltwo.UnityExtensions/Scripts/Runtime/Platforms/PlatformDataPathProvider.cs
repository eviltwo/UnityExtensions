using System.IO;
using UnityEngine;

#if STEAMWORKS && !DISABLESTEAMWORKS
using Steamworks;
#endif

namespace eviltwo.UnityExtensions
{
    /// <summary>
    /// This class checks whether Steamworks is being used and provides the appropriate directory path for save data.
    /// </summary>
    public static class PlatformDataPathProvider
    {
        public static string RootFolderName = "SaveData";
        public static string SteamFolderName = "Steam";
        public static string SharedFolderName = "Shared";

        public static string GetDirectory()
        {
#if STEAMWORKS && !DISABLESTEAMWORKS
            try
            {
                return GetSteamDirectory();
            }
            catch (System.Exception)
            {
                Debug.LogWarning("Due to an error accessing Steamworks, the shared directory will be used. Steamworks may not be initialized.");
            }
#endif

            return GetSharedDirectory();
        }

#if STEAMWORKS && !DISABLESTEAMWORKS
        private static string GetSteamDirectory()
        {
            var userSteamId = SteamUser.GetSteamID().m_SteamID;
            return Path.Combine(Application.persistentDataPath, RootFolderName, SteamFolderName, userSteamId.ToString());
        }
#endif

        private static string GetSharedDirectory()
        {
            return Path.Combine(Application.persistentDataPath, RootFolderName, SharedFolderName);
        }
    }
}

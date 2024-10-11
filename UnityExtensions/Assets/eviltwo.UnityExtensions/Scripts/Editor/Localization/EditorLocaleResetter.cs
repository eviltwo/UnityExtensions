#if LOCALIZATION
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace eviltwo.UnityExtensions.Editor
{
    [InitializeOnLoad]
    public static class EditorLocaleResetter
    {
        static EditorLocaleResetter()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (!EditorLocaleResetterSettings.instance.Enable)
            {
                return;
            }

            if (state == PlayModeStateChange.EnteredEditMode)
            {
                Locale locale = null;
                LocalizationSettings.SelectedLocale = locale;
                Debug.Log("Locale set to " + (locale == null ? "None" : locale.LocaleName));
            }
        }
    }

    [FilePath("EditorLocaleResetterSettings.asset", FilePathAttribute.Location.PreferencesFolder)]
    public class EditorLocaleResetterSettings : ScriptableSingleton<EditorLocaleResetterSettings>
    {
        public bool Enable = true;

        public void Save()
        {
            Save(true);
        }
    }

    public class EditorLocaleResetterSettingsProvider : SettingsProvider
    {
        private const string SettingPath = "Preferences/eviltwo.UnityExtensions/Editor Locale Resetter";
        private static readonly string[] Keywords = { };

        [SettingsProvider]
        public static SettingsProvider CreateProvider()
        {
            return new EditorLocaleResetterSettingsProvider(SettingPath, SettingsScope.User, Keywords);
        }

        public EditorLocaleResetterSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords) : base(path, scopes, keywords)
        {
        }

        public override void OnGUI(string searchContext)
        {
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                var settings = EditorLocaleResetterSettings.instance;
                settings.Enable = EditorGUILayout.Toggle("Enable", settings.Enable);
                if (check.changed)
                {
                    settings.Save();
                }
            }
        }
    }
}
#endif // LOCALIZATION

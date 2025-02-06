# eviltwo.UnityExtensions
Extensions for Unity. It will be expanded according to my game development.

# Contents
## Editor
- [CreateEmptyNeighbor](UnityExtensions/Assets/eviltwo.UnityExtensions/Scripts/Editor/Hierarchy/CreateEmptyNeighbor.cs)
  - Adding menu "GameObject/Create Asset Neighbor" to under "Create Empty Parent".
- [EditorLocaleResetter](UnityExtensions/Assets/eviltwo.UnityExtensions/Scripts/Editor/Localization/EditorLocaleResetter.cs)
  - When the game playback is stopped while using the Localization package, the Locale will be reset.
 
## Runtime
- [FilteredEventRelay](UnityExtensions/Assets/eviltwo.UnityExtensions/Scripts/Runtime/Events/FilteredEventRelay.cs)
- [DontDestroyOnLoad](UnityExtensions/Assets/eviltwo.UnityExtensions/Scripts/Runtime/Objects/DontDestroyOnLoad.cs)
- [SingletonGameObject](UnityExtensions/Assets/eviltwo.UnityExtensions/Scripts/Runtime/Objects/SingletonGameObject.cs)
- [PlatformDataPathProvider](UnityExtensions/Assets/eviltwo.UnityExtensions/Scripts/Runtime/Platforms/PlatformDataPathProvider.cs)
  - Change the save data location depending on whether the game was launched from Steam. Requires Steamworks.NET.
- [DataKeeper](UnityExtensions/Assets/eviltwo.UnityExtensions/Scripts/Runtime/Scenes/DataKeeper.cs)
  - A static class for passing data across scenes.
- [SteamManager](UnityExtensions/Assets/eviltwo.UnityExtensions/Scripts/Runtime/Steamworks/SteamManager.cs)
  - for Steamworks.NET
- [TimeRequestManager](UnityExtensions/Assets/eviltwo.UnityExtensions/Scripts/Runtime/Time/TimeRequestManager.cs)
  - A manager class that handles duplicate time scale changes.
- [AutoSelectionForButtonDevice](UnityExtensions/Assets/eviltwo.UnityExtensions/Scripts/Runtime/UI/AutoSelectionForButtonDevice.cs)
  - Automatically selects the first selectable UI when the keyboard or gamepad is enabled.
- [CancelEventChain](UnityExtensions/Assets/eviltwo.UnityExtensions/Scripts/Runtime/UI/CancelEventChain.cs)
  - Pass the cancel event to the parent UI.
- [CursorActiveControllerForGamepad](UnityExtensions/Assets/eviltwo.UnityExtensions/Scripts/Runtime/UI/CursorActiveControllerForGamepad.cs)
  - Hide the cursor when the gamepad is enabled.
- [LayoutSizeLimiter](UnityExtensions/Assets/eviltwo.UnityExtensions/Scripts/Runtime/UI/LayoutSizeLimiter.cs)
 - Limit the preferredWidth and preferredHeight by the maximum value.
- [ScrollbarNavigationSwitcher](UnityExtensions/Assets/eviltwo.UnityExtensions/Scripts/Runtime/UI/ScrollbarNavigationSwitcher.cs) 
  - enables navigation when the scrollbar is at either end.
- [Stepper](UnityExtensions/Assets/eviltwo.UnityExtensions/Scripts/Runtime/UI/Stepper.cs)
  - A UI where the value increases or decreases by 1 when a button is pressed.

# Install with UPM
```
https://github.com/eviltwo/UnityExtensions.git?path=UnityExtensions/Assets/eviltwo.UnityExtensions
```

# Support My Work
As a solo developer, your financial support would be greatly appreciated and helps me continue working on this project.
- [Asset Store](https://assetstore.unity.com/publishers/12117)
- [Steam](https://store.steampowered.com/curator/45066588)
- [GitHub Sponsors](https://github.com/sponsors/eviltwo)

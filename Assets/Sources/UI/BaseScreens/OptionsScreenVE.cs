using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace DrebotGS.UI
{
  public class OptionsScreenVE : VisualElement
  {
    public new class UxmlFactory : UxmlFactory<OptionsScreenVE, UxmlTraits> { }
    public new class UxmlTraits : VisualElement.UxmlTraits { }

    // OptionsButtons
    private const string _optionsDisplayButton = "options-display-button";
    private const string _optionsLanguageButton = "options-language-button";
    private const string _optionsCloseButton = "options-close-button";

    // ContainersOfDetails
    private const string _optionsDisplayDetails = "options-display-details";
    private const string _optionsLanguageDetails = "options-language-details";
    private VisualElement _displayDetails;
    private VisualElement _languageDetalis;

    // DisplayMonitor
    private const string _switcherLabelDisplayMonitor = "options-details-switcher-label-display-monitor";
    private const string _switcherLeftButtonDisplayMonitor = "options-details-switcher-left-button-display-monitor";
    private const string _switcherRightButtonDisplayMonitor = "options-details-switcher-right-button-display-monitor";
    private Label _switcherDisplayMonitorLabel;
    private Button _switcherDisplayMonitorLeft;
    private Button _switcherDisplayMonitorRight;
    private List<DisplayInfo> _displays = new List<DisplayInfo>();
    private int _currentDisplayMonitorIndex = 0;

    // DisplayMode
    private const string _switcherLabelDisplayMode = "options-details-switcher-label-display-mode";
    private const string _switcherLeftButtonDisplayMode = "options-details-switcher-left-button-display-mode";
    private const string _switcherRightButtonDisplayMode = "options-details-switcher-right-button-display-mode";
    private readonly FullScreenMode[] _fullScreenModes = new[]
{
#if UNITY_STANDALONE_WIN
      FullScreenMode.ExclusiveFullScreen, // Exclusive full screen is only supported on Windows Standalone player
#endif
      FullScreenMode.FullScreenWindow,
      FullScreenMode.Windowed,
#if UNITY_STANDALONE_OSX
        FullScreenMode.MaximizedWindow, // Maximized Window is only supported on macOS player
#endif
    };

    private Label _switcherDisplayModeLabel;
    private Button _switcherDisplayModeLeft;
    private Button _switcherDisplayModeRight;
    private int _currentDisplayModeIndex = 0;

    // ScreenResolution
    private const string _switcherLabelScreenResolution = "options-details-switcher-label-screen-resolution";
    private const string _switcherLeftButtonScreenResolution = "options-details-switcher-left-button-screen-resolution";
    private const string _switcherRightButtonScreenResolution = "options-details-switcher-right-button-screen-resolution";
    private const int _minResolutionWidht = 640;
    private readonly ResolutionComparer _resolutionComparer = new ResolutionComparer();

    private Label _switcherScreenResolutionLabel;
    private Button _switcherScreenResolutionLeft;
    private Button _switcherScreenResolutionRight;
    private Resolution[] _resolution;
    private Resolution[] _emptyResolution = new Resolution[1];
    private Dictionary<(int, int), string> _resolutionNameCache = new Dictionary<(int, int), string>();
    private int _currentScreenResolutionIndex = 0;
    private int _uniqueResolutionCount;

    // VSync
    private const string _switcherLabelVSync = "options-details-switcher-label-vsync";
    private const string _switcherLeftButtonVSync = "options-details-switcher-left-button-vsync";
    private const string _switcherRightButtonVSync = "options-details-switcher-right-button-vsync";
    private readonly byte[] _vSync = new byte[4] { 0, 1, 2, 3 };

    private Label _switcherVSyncLabel;
    private Button _switcherVSyncLeft;
    private Button _switcherVSyncRight;
    private int _currentVSyncIndex = 0;

    public void InitAndBinds()
    {
      BindDetailsElements();
      BindDisplayElements();
    }

    private void BindDetailsElements()
    {
      _displayDetails = this.Q<VisualElement>(_optionsDisplayDetails);
      if (_displayDetails == null)
      { throw new NullReferenceException($"Can not find VisualElement: { _optionsDisplayDetails }."); }
      _languageDetalis = this.Q<VisualElement>(_optionsLanguageDetails);
      if (_languageDetalis == null)
      { throw new NullReferenceException($"Can not find VisualElement: { _optionsLanguageDetails }."); }
    }

    private void BindDisplayElements()
    {
      InitAndBindOptionButtons();
      InitAndBindDisplayMonitor();
      InitAndBindDisplayMode();
      InitAndBindScreenResolution();
      InitAndBindVSync();

      RefreshAllElements();
    }


    #region DisplayElements

    // InitAndBind
    private void InitAndBindOptionButtons()
    {
      var optionDisplayButton = this.Q<Button>(_optionsDisplayButton);
      if (optionDisplayButton != null)
      {
        optionDisplayButton.RegisterCallback<MouseOverEvent>((type) =>
        {
          DisableAllDetailsElements();
          ShowDisplayDetails();
        });
      }
      var optionLanguageButton = this.Q<Button>(_optionsLanguageButton);
      if (optionLanguageButton != null)
      {
        optionLanguageButton.RegisterCallback<MouseOverEvent>((type) =>
        {
          DisableAllDetailsElements();
          ShowLanguageDetails();
        });
      }
      var optionCloseButton = this.Q<Button>(_optionsCloseButton);
      if (optionCloseButton != null)
      {
        optionCloseButton.RegisterCallback<MouseOverEvent>((type) =>
        {
          DisableAllDetailsElements();
        });
      }

      void DisableAllDetailsElements()
      {
        _displayDetails.style.display = DisplayStyle.None;
        _languageDetalis.style.display = DisplayStyle.None;
      }
      void ShowDisplayDetails()
      {
        _displayDetails.style.display = DisplayStyle.Flex;
      }
      void ShowLanguageDetails()
      {
        _languageDetalis.style.display = DisplayStyle.Flex;
      }
    }

    private void InitAndBindDisplayMonitor()
    {
      InitDisplayMonitors();
      BindSwitcherDisplayMonitorLabel();
      BindSwitcherDisplayMonitorLeft();
      BindSwitcherDisplayMonitorRight();

      void InitDisplayMonitors()
      {
        Screen.GetDisplayLayout(_displays);

        for (int i = 0; i < _displays.Count; i++)
        {
          if (Screen.mainWindowDisplayInfo.Equals(_displays[i]))
          {
            _currentDisplayMonitorIndex = i;
          }
        }
      }
      void BindSwitcherDisplayMonitorLabel()
      {
        _switcherDisplayMonitorLabel = _displayDetails.Q<Label>(_switcherLabelDisplayMonitor);
        if (_switcherDisplayMonitorLabel == null)
        { throw new NullReferenceException($"Can not find Label: { _switcherLabelDisplayMonitor }."); }
      }
      void BindSwitcherDisplayMonitorLeft()
      {
        _switcherDisplayMonitorLeft = _displayDetails.Q<Button>(_switcherLeftButtonDisplayMonitor);
        if (_switcherDisplayMonitorLeft != null)
        {
          _switcherDisplayMonitorLeft.clickable.clicked += () =>
          {
            MoveToPrevDisplayMonitor();
            RefreshLabelDisplayMonitor();
          };
        }
      }
      void BindSwitcherDisplayMonitorRight()
      {
        _switcherDisplayMonitorRight = _displayDetails.Q<Button>(_switcherRightButtonDisplayMonitor);
        if (_switcherDisplayMonitorRight != null)
        {
          _switcherDisplayMonitorRight.clickable.clicked += () =>
          {
            MoveToNextDisplayMonitor();
            RefreshLabelDisplayMonitor();
          };
        }
      }
      void MoveToPrevDisplayMonitor()
      {
        _currentDisplayMonitorIndex--;
        if (_currentDisplayMonitorIndex < 0)
          _currentDisplayMonitorIndex = _displays.Count - 1;

        RefreshDisplayMonitor();
      }
      void MoveToNextDisplayMonitor()
      {
        _currentDisplayMonitorIndex++;
        if (_currentDisplayMonitorIndex > _displays.Count - 1)
          _currentDisplayMonitorIndex = 0;

        RefreshDisplayMonitor();
      }
    }

    private void InitAndBindDisplayMode()
    {
      InitDisplayMode();
      BindSwitcherDisplayModeLabel();
      BindSwitcherDisplayModeLeft();
      BindSwitcherDisplayModeRight();

      void InitDisplayMode()
      {
        for (int i = 0; i < _fullScreenModes.Length; i++)
        {
          if (Screen.fullScreenMode == _fullScreenModes[i])
            _currentDisplayModeIndex = i;
        }
      }
      void BindSwitcherDisplayModeLabel()
      {
        _switcherDisplayModeLabel = _displayDetails.Q<Label>(_switcherLabelDisplayMode);
        if (_switcherDisplayModeLabel == null)
        { throw new NullReferenceException($"Can not find Label: { _switcherLabelDisplayMode }."); }
      }
      void BindSwitcherDisplayModeLeft()
      {
        _switcherDisplayModeLeft = _displayDetails.Q<Button>(_switcherLeftButtonDisplayMode);
        if (_switcherDisplayModeLeft != null)
        {
          _switcherDisplayModeLeft.clickable.clicked += () =>
          {
            MoveToPrevDisplayMode();
            RefreshLabelDisplayMode();
          };
        }
      }
      void BindSwitcherDisplayModeRight()
      {
        _switcherDisplayModeRight = _displayDetails.Q<Button>(_switcherRightButtonDisplayMode);
        if (_switcherDisplayModeRight != null)
        {
          _switcherDisplayModeRight.clickable.clicked += () =>
          {
            MoveToNextDisplayMode();
            RefreshLabelDisplayMode();
          };
        }
      }
      void MoveToPrevDisplayMode()
      {
        _currentDisplayModeIndex--;
        if (_currentDisplayModeIndex < 0)
          _currentDisplayModeIndex = _fullScreenModes.Length - 1;

        RefreshFullScreenMode();
      }
      void MoveToNextDisplayMode()
      {
        _currentDisplayModeIndex++;
        if (_currentDisplayModeIndex > _fullScreenModes.Length - 1)
          _currentDisplayModeIndex = 0;

        RefreshFullScreenMode();
      }
    }

    private void InitAndBindScreenResolution()
    {
      InitScreenResolution();
      BindSwitcherDisplayResolutionLabel();
      BindSwitcherDisplayResolutionLeft();
      BindSwitcherDisplayResolutionRight();

      void InitScreenResolution()
      {
        _resolution = Screen.resolutions.Where(x => x.width >= _minResolutionWidht).ToArray();
      }
      void BindSwitcherDisplayResolutionLabel()
      {
        _switcherScreenResolutionLabel = _displayDetails.Q<Label>(_switcherLabelScreenResolution);
        if (_switcherScreenResolutionLabel == null)
        { throw new NullReferenceException($"Can not find Label: { _switcherLabelScreenResolution }."); }
      };
      void BindSwitcherDisplayResolutionLeft()
      {
        _switcherScreenResolutionLeft = _displayDetails.Q<Button>(_switcherLeftButtonScreenResolution);
        if (_switcherScreenResolutionLeft != null)
        {
          _switcherScreenResolutionLeft.clickable.clicked += () =>
          {
            _currentScreenResolutionIndex--;
            if (_currentScreenResolutionIndex < 0)
              _currentScreenResolutionIndex = _uniqueResolutionCount - 1;

            RefresScreenResolution();
            RefreshLabelScreenResolution();
          };
        }
      };
      void BindSwitcherDisplayResolutionRight()
      {
        _switcherScreenResolutionRight = _displayDetails.Q<Button>(_switcherRightButtonScreenResolution);
        if (_switcherScreenResolutionRight != null)
        {
          _switcherScreenResolutionRight.clickable.clicked += () =>
          {
            _currentScreenResolutionIndex++;
            if (_currentScreenResolutionIndex > _uniqueResolutionCount - 1)
              _currentScreenResolutionIndex = 0;

            RefresScreenResolution();
            RefreshLabelScreenResolution();
          };
        }
      };
    }

    private void InitAndBindVSync()
    {
      InitVSync();
      BindSwitcherVSyncLabel();
      BindSwitcherVSyncLeft();
      BindSwitcherVSyncRight();

      void InitVSync()
      {
        _currentVSyncIndex = QualitySettings.vSyncCount == 0 ? 0 : 1;
      }
      void BindSwitcherVSyncLabel()
      {
        _switcherVSyncLabel = _displayDetails.Q<Label>(_switcherLabelVSync);
        if (_switcherVSyncLabel == null)
        { throw new NullReferenceException($"Can not find Label: { _switcherLabelVSync }."); }
      };
      void BindSwitcherVSyncLeft()
      {
        _switcherVSyncLeft = _displayDetails.Q<Button>(_switcherLeftButtonVSync);
        if (_switcherVSyncLeft != null)
        {
          _switcherVSyncLeft.clickable.clicked += () =>
          {
            _currentVSyncIndex--;
            if (_currentVSyncIndex < 0)
              _currentVSyncIndex = _vSync.Length - 1;

            RefreshVSync();
            RefreshLabelVSync();
          };
        }
      };
      void BindSwitcherVSyncRight()
      {
        _switcherVSyncRight = _displayDetails.Q<Button>(_switcherRightButtonVSync);
        if (_switcherVSyncRight != null)
        {
          _switcherVSyncRight.clickable.clicked += () =>
          {
            _currentVSyncIndex++;
            if (_currentVSyncIndex > _vSync.Length - 1)
              _currentVSyncIndex = 0;

            RefreshVSync();
            RefreshLabelVSync();
          };
        }
      };
    }

    // Refresh

    private void RefreshDisplayMonitor()
    {
      Timing.RunCoroutine(RefreshDisplay());

      IEnumerator<float> RefreshDisplay()
      {
        try
        {
          var display = _displays[_currentDisplayMonitorIndex];

          Vector2Int targetCoordinates = new Vector2Int(0, 0);
          if (_fullScreenModes[_currentDisplayModeIndex] == FullScreenMode.Windowed)
          {
            targetCoordinates.x += _resolution[_currentScreenResolutionIndex].width / 2;
            targetCoordinates.y += _resolution[_currentScreenResolutionIndex].height / 2;
          }

          var moveOperation = Screen.MoveMainWindowTo(display, targetCoordinates);
          yield return Timing.WaitUntilDone(moveOperation);
        }
        finally
        {
          RefreshListResolutions();
          RefreshLabelScreenResolution();
          RefresScreenResolution();
        }
      }
    }

    private void RefreshLabelDisplayMonitor()
    {
      if (_switcherDisplayMonitorLabel == null)
        return;
      _switcherDisplayMonitorLabel.text = _displays[_currentDisplayMonitorIndex].name;
    }

    private void RefreshFullScreenMode()
    {
      var newMode = _fullScreenModes[_currentDisplayModeIndex];
      if (Screen.fullScreenMode != newMode)
      {
        if (newMode == FullScreenMode.Windowed)
        {
          Screen.fullScreenMode = newMode;
        }
        else
        {
          Screen.SetResolution(_resolution[_currentScreenResolutionIndex].width, _resolution[_currentScreenResolutionIndex].height, newMode);
        }
      }
    }

    private void RefreshLabelDisplayMode()
    {
      if (_switcherDisplayModeLabel == null)
        return;
      _switcherDisplayModeLabel.text = _fullScreenModes[_currentDisplayModeIndex].ToString();
    }

    private void RefreshListResolutions()
    {
      var resolutions = Screen.resolutions.Where(x => x.width >= _minResolutionWidht).ToArray();
      _uniqueResolutionCount = 0;
      if (resolutions.Length > 0)
      {
        Array.Sort(resolutions, _resolutionComparer);

        int lastUniqueResolution = 0;
        for (int i = 1; i < resolutions.Length; i++)
        {
          if (_resolutionComparer.Compare(resolutions[i], resolutions[lastUniqueResolution]) != 0)
          {
            lastUniqueResolution++;
            if (lastUniqueResolution != i)
              resolutions[lastUniqueResolution] = resolutions[i];
          }
        }
        _uniqueResolutionCount = lastUniqueResolution + 1;
      }
      else
      {
        _uniqueResolutionCount = 1;
        _emptyResolution[0] = Screen.currentResolution;
        resolutions = _emptyResolution;
      }
      _currentScreenResolutionIndex = _uniqueResolutionCount - 1;

      var currentWidth = Screen.width;
      var currentHeight = Screen.height;
      for (int i = 0; i < _uniqueResolutionCount; i++)
      {
        var resolution = resolutions[i];
        if (resolution.width == currentWidth && resolution.height == currentHeight)
          _currentScreenResolutionIndex = i;
      }
      _resolution = resolutions;
    }

    private void RefresScreenResolution()
    {
      Screen.SetResolution(
        _resolution[_currentScreenResolutionIndex].width,
        _resolution[_currentScreenResolutionIndex].height,
        _fullScreenModes[_currentDisplayModeIndex]);
    }

    private void RefreshLabelScreenResolution()
    {
      _switcherScreenResolutionLabel.text = GetCachedResolutionText(
        _resolution[_currentScreenResolutionIndex].width,
        _resolution[_currentScreenResolutionIndex].height);

      string GetCachedResolutionText(int width, int height)
      {
        var key = (width, height);
        if (_resolutionNameCache.TryGetValue(key, out var name))
          return name;

        name = $"{width} x {height}";
        _resolutionNameCache.Add(key, name);
        return name;
      }
    }

    private void RefreshVSync()
    {
      QualitySettings.vSyncCount = _vSync[_currentVSyncIndex];
    }

    private void RefreshLabelVSync()
    {
      _switcherVSyncLabel.text = _vSync[_currentVSyncIndex] == 0 ? "Off" : _vSync[_currentVSyncIndex].ToString();
    }

    #endregion


    private void RefreshAllElements()
    {
      RefreshDisplayMonitor();
      RefreshLabelDisplayMonitor();
      RefreshFullScreenMode();
      RefreshLabelDisplayMode();
      RefreshListResolutions();
      RefresScreenResolution();
      RefreshLabelScreenResolution();
      RefreshVSync();
      RefreshLabelVSync();
    }

    private class ResolutionComparer : IComparer<Resolution>
    {
      public int Compare(Resolution x, Resolution y)
      {
        // Lowest to highest
        if (x.width == y.width)
          return x.height.CompareTo(y.height);

        return x.width < y.width ? -1 : 1;
      }
    }
  }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DrebotGS.Input
{
  [System.Serializable]
  public struct CustomInputContextIcon
  {
    public string customInputContextString;
    public Sprite customInputContextIcon;
  }

  [CreateAssetMenu(fileName = "Device Display Settings", menuName = "DrebotGS/Input/Device Display Settings", order = 1)]
  public class DeviceDisplaySettings : ScriptableObject
  {
    public string deviceDisplayName;

    public bool deviceHasContextIcons;

    public Sprite buttonNorthIcon;
    public Sprite buttonSouthIcon;
    public Sprite buttonWestIcon;
    public Sprite buttonEastIcon;

    public Sprite triggerRightFrontIcon;
    public Sprite triggerRightBackIcon;
    public Sprite triggerLeftFrontIcon;
    public Sprite triggerLeftBackIcon;

    public List<CustomInputContextIcon> customContextIcons = new List<CustomInputContextIcon>();
  }
}
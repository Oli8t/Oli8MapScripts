using BS;
using System.Collections.Generic;
using UnityEngine;

namespace Oli8MapScripts
{
  public partial class CustomTriggerData
  {
    public string triggerType = "Unset";
    public string customReferenceId = "Unset";
    public bool debugLog = false;
  }

  public abstract class CustomTrigger : MonoBehaviour
  {
    public CustomTriggerData data;
    public bool Initialize(CustomTriggerData triggerData, List<Transform> transforms)
    {
      data = triggerData;
      return InitializeTrigger(triggerData, transforms);
    }

    abstract public bool InitializeTrigger(CustomTriggerData triggerData, List<Transform> transforms);

    public void Log(string message)
    {
      if(data.debugLog)
      {
        Debug.Log(message);
      }
    }
  }

  public class CustomTriggerModule : LevelModule
  {
    public List<CustomTriggerData> triggerData = new List<CustomTriggerData>();

    public static Dictionary<string, System.Type> triggerTypeMap = new Dictionary<string, System.Type>();
    public static bool AddTriggerTypeToDictionary(string typeId, System.Type type)
    {
      triggerTypeMap.Add(typeId, type);
      return true;
    }

    public override void OnLevelLoaded(LevelDefinition levelDefinition)
    {
      foreach(var customTriggerData in triggerData)
      {
        string triggerType = customTriggerData.triggerType.ToLower();
        Debug.Log("Attempting to create custom trigger of type " + triggerType);
        if(!triggerTypeMap.ContainsKey(triggerType))
        {
          Debug.LogError("Invalid custom trigger type [" + triggerType + "]");
          continue;
        }

        List<LevelDefinition.CustomReference> customReferences = levelDefinition.customReferences.FindAll(r => r.name == customTriggerData.customReferenceId);
        if(customReferences.Count != 1)
        {
          Debug.LogError("Expected only 1 custom reference with name " + customTriggerData.customReferenceId);
          continue;
        }

        LevelDefinition.CustomReference currentReference = customReferences[0];
        CustomTrigger trigger = currentReference.transforms[0].gameObject.AddComponent(triggerTypeMap[triggerType]) as CustomTrigger;
        Debug.Log("Added trigger of type " + triggerTypeMap[triggerType].Name + " to CustomReference " + customTriggerData.customReferenceId);
        trigger.Initialize(customTriggerData, currentReference.transforms);
      }

      base.OnLevelLoaded(levelDefinition);
    }
  }
}

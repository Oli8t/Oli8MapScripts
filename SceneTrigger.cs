using BS;
using System.Collections.Generic;
using UnityEngine;

namespace Oli8MapScripts
{
  public partial class CustomTriggerData
  {
    public class SceneTriggerData
    {
      public string sceneId = "Unset";
    }

    public SceneTriggerData sceneTriggerData = null;
    private static bool sceneAddedToDictionary = CustomTriggerModule.AddTriggerTypeToDictionary("scene", typeof(SceneTrigger));
  }

  public class SceneTrigger : CustomTrigger
  {
    public override bool InitializeTrigger(CustomTriggerData triggerData, List<Transform> transforms)
    {
      return true;
    }

    void OnTriggerEnter(Collider other)
    {
      if (other.GetComponentInParent<Creature>() == Creature.player)
      {
        GameManager.LoadLevel(data.sceneTriggerData.sceneId);
      }
    }
  }
}

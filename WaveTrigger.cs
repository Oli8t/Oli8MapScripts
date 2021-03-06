﻿using BS;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Oli8MapScripts
{
  public partial class CustomTriggerData
  {
    public class WaveTriggerData
    {
      public string waveSelectorId = "Unset";
      public string waveId = "Unset";
    }

    public WaveTriggerData waveTriggerData = null;
    private static bool waveAddedToDictionary = CustomTriggerModule.AddTriggerTypeToDictionary("wave", typeof(WaveTrigger));
  }

  public class WaveTrigger : CustomTrigger
  {
    public override bool InitializeTrigger(CustomTriggerData data, List<Transform> transforms)
    {
      return true;
    }

    void OnTriggerStay(Collider other)
    {
      try
      {
        if (LoadingCamera.cam.enabled == false &&
          other.GetComponentInParent<Creature>() == Creature.player &&
          Creature.player.health.currentHealth > 0 &&
          LevelDefinition.current != null &&
          LevelDefinition.current.modeRank != null &&
          LevelDefinition.current.modeRank.mode != null &&
          LevelDefinition.current.modeRank.mode.GetModule<LevelModuleWave>() != null &&
          !LevelDefinition.current.modeRank.mode.GetModule<LevelModuleWave>().isRunning)
        {
          var page = GameObject.FindObjectsOfType<UIPageWaves>().Where(p => p.id == data.waveTriggerData.waveSelectorId).First();
          if (page != null)
          {
            LevelDefinition.current.modeRank.mode.GetModule<LevelModuleWave>().StartWave(page.spawnLocation, Catalog.current.GetData<WaveData>(data.waveTriggerData.waveId));
          }
          else
          {
            Debug.LogError("Page not found for wave trigger " + data.customReferenceId);
          }
          GameObject.Destroy(this);
        }
      }
      catch(System.Exception e)
      {

      }
    }
  }
}

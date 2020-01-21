using BS;
using System.Collections.Generic;
using UnityEngine;

namespace Oli8MapScripts
{
  public class RemoveGameObjectsModule : LevelModule
  {
    public string referenceToRemove;

    public override void OnLevelLoaded(LevelDefinition levelDefinition)
    {
      List<LevelDefinition.CustomReference> customReferences = levelDefinition.customReferences.FindAll(r => r.name == referenceToRemove);
      foreach(LevelDefinition.CustomReference reference in customReferences)
      {
        int count = reference.transforms.Count;
        for (int i = count - 1; i >= 0; --i)
        {
          GameObject.Destroy(reference.transforms[i].gameObject);
        }
      }

      base.OnLevelLoaded(levelDefinition);
    }
  }
}

using BS;
using System.Collections;
using UnityEngine;

namespace Oli8MapScripts
{
  public class StartingPlayerGear : LevelModule
  {
    public string containerID;

    public override void OnLevelLoaded(LevelDefinition levelDefinition)
    {
      new GameObject().AddComponent<PlayerGearOverride>().containerID = containerID;

      base.OnLevelLoaded(levelDefinition);
    }
  }

  public class PlayerGearOverride : MonoBehaviour
  {
    public string containerID;

    void Update()
    {
      if(Creature.player != null)
      {
        Creature.player.UnequipWeapons();
        //Creature.player.UnequipApparels();
        Creature.player.container.containerID = containerID;
        Creature.player.container.LoadFromCatalog();
        //Creature.player.EquipApparels();
        Creature.player.EquipWeapons();
        GameObject.Destroy(gameObject);
      }
    }
  }
}

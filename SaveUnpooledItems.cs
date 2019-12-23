using BS;
using UnityEngine;

namespace Oli8MapScripts
{
  public class SaveUnpooledItems : LevelModule
  {
    public override void OnLevelLoaded(LevelDefinition levelDefinition)
    {
      foreach (ItemSpawner spawner in GameObject.FindObjectsOfType<ItemSpawner>())
      {
        if (!spawner.pooled)
        {
          ItemSaver saver = spawner.gameObject.AddComponent<ItemSaver>();
          saver.itemId = spawner.itemId;
          saver.pooled = spawner.pooled;
          saver.spawnOnStart = spawner.spawnOnStart;
          GameObject.Destroy(spawner);
        }
      }

      base.OnLevelLoaded(levelDefinition);
    }
  }

  public class ItemSaver : ItemSpawner
  {
    new protected void Start()
    {
      if(spawnOnStart)
      {
        Item item = Spawn();
        if(item)
        {
          item.disallowDespawn = true;
        }
      }
    }
  }
}

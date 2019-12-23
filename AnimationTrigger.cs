using BS;
using System.Collections.Generic;
using UnityEngine;

namespace Oli8MapScripts
{
  public partial class CustomTriggerData
  {
    public class AnimationTriggerData
    {
      public string animatorValueId = "Unset";
    }

    public AnimationTriggerData animationTriggerData = null;
    private static bool animationAddedToDictionary = CustomTriggerModule.AddTriggerTypeToDictionary("animation", typeof(AnimationTrigger));
  }

  public class AnimationTrigger : CustomTrigger
  {
    public Animator animator = null;
    public bool collisionStayThisFrame = false;
    public bool collisionExitThisFrame = false;

    public override bool InitializeTrigger(CustomTriggerData data, List<Transform> transforms)
    {
      Log("Initializing animation trigger parameter " + data.animationTriggerData.animatorValueId + " for " + data.customReferenceId);
      if(transforms.Count != 2)
      {
        Debug.LogError("AnimationTriggers should have 2 transforms in the CustomReference. [1] Trigger Volume [2] Animator");
        return false;
      }

      animator = transforms[1].gameObject.GetComponent<Animator>();
      if(animator == null)
      {
        Debug.LogError("AnimationTrigger for " + data.customReferenceId + " is missing an Animator on the second transform");
        return false;
      }

      return true;
    }

    void OnTriggerStay(Collider other)
    {
      if (other.GetComponentInParent<Creature>())
      {
        collisionStayThisFrame = true;
      }
    }

    void OnTriggerExit(Collider other)
    {
      if (other.GetComponentInParent<Creature>())
      {
        collisionExitThisFrame = true;
      }
    }

    void LateUpdate()
    {
      if(collisionStayThisFrame)
      {
        animator.SetBool(data.animationTriggerData.animatorValueId, true);
      }
      else if(collisionExitThisFrame)
      {
        animator.SetBool(data.animationTriggerData.animatorValueId, false);
      }

      collisionExitThisFrame = !collisionStayThisFrame;
      collisionStayThisFrame = false;
    }
  }
}

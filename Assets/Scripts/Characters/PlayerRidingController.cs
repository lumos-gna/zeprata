using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRidingController : MonoBehaviour
{
    float mountDefalutHeight;

    Animator animator;
    ObjectSpriteRendererController rendererController;

    public void Init(Animator animator, ObjectSpriteRendererController rendererController)
    {
        this.animator = animator;
        this.rendererController = rendererController;

        if (rendererController.TryGetRenderer("Character", out ObjectSpriteRenderer target))
        {
            mountDefalutHeight = target.transform.localPosition.y;
        }
    }


    public void ToggleMount(bool isMount, RidingItemData data)
    {
        animator.SetBool("isMount", isMount);

        if (rendererController.TryGetRenderer("Character", out ObjectSpriteRenderer character))
        {
            Vector2 tempLocalPos = character.transform.localPosition;

            tempLocalPos.y = isMount ? data.MountHeight : mountDefalutHeight;

            character.transform.localPosition = tempLocalPos;
        }

        if (rendererController.TryGetRenderer("Riding", out ObjectSpriteRenderer riding))
        {
            riding.gameObject.SetActive(isMount);
        }
    }
}

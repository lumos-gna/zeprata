using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoNpc : Npc
{

    protected override void Awake()
    {
        base.Awake();

        dialogueData.OnFinishDialogue = () => UIManager.Instance.EnableShop();
    }
}

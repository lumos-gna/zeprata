using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void InteractStart(Player player);
    public void InteractEnd(Player player);
}

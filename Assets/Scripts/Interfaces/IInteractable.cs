using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractTriggerable
{
    public void Interact(GameObject source);

    public void TriggerEnter(GameObject source);

    public void TriggerExit(GameObject source);
}

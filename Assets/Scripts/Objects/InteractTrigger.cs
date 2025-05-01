using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractTrigger : MonoBehaviour, IInteractTriggerable
{
    public UnityAction<GameObject> OnInteract { private get; set; }
    public UnityAction<GameObject> OnTriggerEnter { private get; set; }
    public UnityAction<GameObject> OnTriggerEixt { private get; set; }

   
    public void Interact(GameObject source) => OnInteract?.Invoke(source);
    public void TriggerEnter(GameObject source) => OnTriggerEnter?.Invoke(source);
    public void TriggerExit(GameObject source) => OnTriggerEixt?.Invoke(source);
}

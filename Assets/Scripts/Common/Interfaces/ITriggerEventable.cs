using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerEventable 
{
    public void OnTriggerEntered(GameObject source);
    public void OnTriggerExited(GameObject source);
}

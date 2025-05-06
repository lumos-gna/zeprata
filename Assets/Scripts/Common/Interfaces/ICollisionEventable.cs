using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollisionEventable
{
    public void OnCollisionEntered(GameObject source);
    public void OnCollisionExited(GameObject source);
}

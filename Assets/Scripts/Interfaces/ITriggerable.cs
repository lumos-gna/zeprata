using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerable<T> where T : MonoBehaviour
{
    public void OnEnter(T target);
    public void OnExit(T target);
    public void OnStay(T target);
}

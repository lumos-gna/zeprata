using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEventController : MonoBehaviour
{
    public event UnityAction<ICollisionEventable> OnCollisionEnter;
    public event UnityAction<ICollisionEventable> OnCollisionExit;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ICollisionEventable eventTarget))
        {
            OnCollisionEnter?.Invoke(eventTarget);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ICollisionEventable eventTarget))
        {
            OnCollisionExit?.Invoke(eventTarget);
        }
    }
}

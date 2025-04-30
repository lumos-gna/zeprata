using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerable
{
    public void OnEnter(Player player);
    public void OnExit(Player player);
}

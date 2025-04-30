using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void Start(Player player);
    public void Next(Player player);
    public void Finish(Player player);
}

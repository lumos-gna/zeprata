using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquipmentManager : Singleton<EquipmentManager>
{
    public event UnityAction<EquipmentItemData> OnEquippedItem;
    public event UnityAction<EquipmentItemData> OnUnEquippedItem;


}

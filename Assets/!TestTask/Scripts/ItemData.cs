using System.Collections;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/ItemData"), Serializable]
public class ItemData : ScriptableObject
{
    public string Id;
    public Sprite Icon;
    public GameObject InWorldPref;
}
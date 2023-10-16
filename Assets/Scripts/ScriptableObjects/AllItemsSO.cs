using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu()]
public class AllItemsSO : ScriptableObject
{
   public string Name;
   public Sprite Sprite;
   public InventoryManager.AllItems ItemType;
}

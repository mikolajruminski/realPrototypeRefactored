using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemDrop : MonoBehaviour
{
    [SerializeField] private Transform itemToDrop;
    [SerializeField] private Transform newParent;
    [SerializeField] private Vector3 offset = new Vector2(0, 0.5f);

    public void Drop(Vector3 position)
    {
        Instantiate(itemToDrop, position + offset, Quaternion.identity, newParent);
    }
}

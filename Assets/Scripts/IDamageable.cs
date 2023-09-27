using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public event EventHandler OnGetDamaged;
    int Health { get; set; }
    void Damage(int damageAmount);
}

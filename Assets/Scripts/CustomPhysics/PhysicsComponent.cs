using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsComponent : WorldComponent
{
    public float mass = 1f;
    public bool clampMaxFallingSpeed;

    public bool hasBottomSupport;

    [HideInInspector] public Vector3 vecocity;
}

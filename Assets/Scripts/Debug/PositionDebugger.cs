using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionDebugger : MonoBehaviour
{
    public PhysicsComponent physComp;
    public Obstacle obstacle;

    [EditorButton]
    public void CheckXCrossing()
    {
        //Debug.Log($"Intersects by X: {WorldComponent.DoestTwoComponentsIntersectEachOther(physComp, obstacle)}");

        

        //Debug.Log($"Intersects by X: {WorldComponent.DoestTwoComponentsIntersectEachOther(physComp, obstacle)}");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;

public class PhysicsEngine : MonoBehaviour
{
    public float gravityForce = 9.81f;
    public float maxClampedFallSpeed = 9.81f;

    List<PhysicsComponent> physicsComponents;
    List<Obstacle> obstacles;


    public float maxCollisionCheckDistance = 5f;


    private void Start()
    {
        physicsComponents = new List<PhysicsComponent>();
        physicsComponents.AddRange(FindObjectsOfType<PhysicsComponent>());
        obstacles = new List<Obstacle>();
        obstacles.AddRange(FindObjectsOfType<Obstacle>());
    }

    private void FixedUpdate()
    {
        CheckGravityForComponents();
        CheckCollisionsForPhysicsComponents();
    }

    void CheckGravityForComponents()
    {
        foreach (PhysicsComponent a in physicsComponents) 
        {
            if(!a.hasBottomSupport)
                CalculateGravityFixedUpdate(a);
        }
    }

    void CalculateGravityFixedUpdate(PhysicsComponent physComp)
    {
        physComp.vecocity.y -= gravityForce * Time.deltaTime;
        if(physComp.clampMaxFallingSpeed && physComp.vecocity.y < - Mathf.Abs(maxClampedFallSpeed)) physComp.vecocity.y = - Mathf.Abs(maxClampedFallSpeed);
        physComp.transform.Translate(physComp.vecocity * Time.deltaTime);
    }

    void CheckCollisionsForPhysicsComponents()
    {
        //CheckCollisionsForPhysComponent();
        CheckCollisionsWithOtherPhysComp();
    }

    //void CheckCollisionsForPhysComponent()
    //{
    //    foreach (PhysicsComponent a in physicsComponents)
    //    {
    //        foreach (WorldComponent b in GetClosestComponents(a, maxCollisionCheckDistance))
    //        {
    //            if (WorldComponent.DoComponentsIntersect(a, b))
    //            {
    //                a.hasBottomSupport = true;
    //                a.vecocity = new Vector3(0, 0, 0);
    //                WorldComponent.FixYPosition(a, b);
    //                break;
    //            }
    //        }
    //        a.hasBottomSupport = false;
    //    }
    //    
    //}

    void CheckCollisionsWithOtherPhysComp()
    {
        foreach(PhysicsComponent physComp in physicsComponents)
        {
            foreach (WorldComponent a in GetClosestComponents(physComp, maxCollisionCheckDistance))
            {
                if (physComp == a) continue;
                if (WorldComponent.DoComponentsIntersect(physComp, a))
                {
                    if (physComp.transform.position.y < a.transform.position.y) continue;
                    else if (physComp.transform.position.y > a.transform.position.y)
                    {
                        physComp.hasBottomSupport = true;
                        physComp.vecocity = new Vector3(0, 0, 0);
                        WorldComponent.FixYPosition(physComp, a);
                        break;
                    }
                }
                physComp.hasBottomSupport = false;
            }
        }
    }

    List<WorldComponent> GetClosestComponents(WorldComponent initial, float maxDistance)
    {
        List<WorldComponent> closestComponents = new List<WorldComponent>();
        foreach(WorldComponent a in obstacles)
        {
            if (a == initial) continue;
            if (Vector3.Distance(initial.transform.position, a.transform.position) > maxDistance) continue;
            closestComponents.Add(a);
        }
        foreach(WorldComponent b in physicsComponents)
        {
            if (b == initial) continue;
            if (Vector3.Distance(initial.transform.position, b.transform.position) > maxDistance) continue;
            closestComponents.Add(b);
        }
        return closestComponents;
    }
}

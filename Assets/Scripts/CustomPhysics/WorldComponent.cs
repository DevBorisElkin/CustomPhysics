using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldComponent : MonoBehaviour
{
    public Vector3 scale = new Vector3(1, 1, 1);
    public bool drawGizmos = false;

    //private void OnDrawGizmos()
    //{
    //    if (drawGizmos) Gizmos.DrawCube(transform.position, scale);
    //}

    public BorderPoints GetBorderPoints()
    {
        float dsx = scale.x / 2;
        float dsy = scale.y / 2;
        float dsz = scale.z / 2;
        Vector3 _1 = new Vector3(transform.position.x - dsx, transform.position.y - dsy, transform.position.z - dsz);
        Vector3 _2 = new Vector3(transform.position.x + dsx, transform.position.y - dsy, transform.position.z - dsz);
        Vector3 _3 = new Vector3(transform.position.x - dsx, transform.position.y - dsy, transform.position.z + dsz);
        Vector3 _4 = new Vector3(transform.position.x + dsx, transform.position.y - dsy, transform.position.z + dsz);
        Vector3 _5 = new Vector3(transform.position.x - dsx, transform.position.y + dsy, transform.position.z - dsz);
        Vector3 _6 = new Vector3(transform.position.x + dsx, transform.position.y + dsy, transform.position.z - dsz);
        Vector3 _7 = new Vector3(transform.position.x - dsx, transform.position.y + dsy, transform.position.z + dsz);
        Vector3 _8 = new Vector3(transform.position.x + dsx, transform.position.y + dsy, transform.position.z + dsz);

        return new BorderPoints(_1, _2, _3, _4, _5, _6, _7, _8);
    }

    public static bool IsPointWithinRange(Vector2 range, float inspected)
    {
        //Debug.Log($"range: {range} | inspectedPoint {inspected}");
        if (inspected >= range.x && inspected <= range.y) return true;
        return false;
    }
    public static bool DoComponentsIntersectByAxis(Vector2 firstRange, Vector2 secondRange)
    {
        if (IsPointWithinRange(firstRange, secondRange.x) ||
            IsPointWithinRange(firstRange, secondRange.y) ||
            IsPointWithinRange(secondRange, firstRange.x) ||
            IsPointWithinRange(secondRange, firstRange.y)) return true;
        return false;
    }

    public static bool DoComponentsIntersect(WorldComponent first, WorldComponent second)
    {
        Vector2 XaxisFirst = new Vector2(first.transform.position.x - first.scale.x / 2, first.transform.position.x + first.scale.x / 2);
        Vector2 XaxisSecond = new Vector2(second.transform.position.x - second.scale.x / 2, second.transform.position.x + second.scale.x / 2);
        Vector2 YaxisFirst = new Vector2(first.transform.position.y - first.scale.y / 2, first.transform.position.y + first.scale.y / 2);
        Vector2 YaxisSecond = new Vector2(second.transform.position.y - second.scale.y / 2, second.transform.position.y + second.scale.y / 2);
        Vector2 ZaxisFirst = new Vector2(first.transform.position.z - first.scale.z / 2, first.transform.position.z + first.scale.z / 2);
        Vector2 ZaxisSecond = new Vector2(second.transform.position.z - second.scale.z / 2, second.transform.position.z + second.scale.z / 2);

        if (DoComponentsIntersectByAxis(XaxisFirst, XaxisSecond) &&
            DoComponentsIntersectByAxis(YaxisFirst, YaxisSecond) &&
            DoComponentsIntersectByAxis(ZaxisFirst, ZaxisSecond)) return true;
        return false;
    }

    public static void FixYPosition(WorldComponent hisPosToFix, WorldComponent second)
    {
        float YUpperPoint = second.transform.position.y + second.scale.y / 2;
        hisPosToFix.transform.position = new Vector3(hisPosToFix.transform.position.x, YUpperPoint + hisPosToFix.scale.y / 2, hisPosToFix.transform.position.z);
    }

    public float GetUpperPoint(WorldComponent comp) => (comp.transform.position.y + comp.scale.y / 2);
    public float GetBottomPoint(WorldComponent comp) => (comp.transform.position.y - comp.scale.y / 2);
}

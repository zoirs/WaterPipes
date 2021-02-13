using System;
using UnityEngine;

public class TubeEndController : MonoBehaviour {
    [SerializeField] private Direction direction;

    public Direction GetDirection(int rotate) {
        return direction.Rotate(rotate);
    }

    public Vector3Int GetVector() {
        Vector3 p = transform.position;
        return new Vector3Int((int) Math.Round(p.x, 0),(int) Math.Round(p.y, 0),(int) Math.Round( p.z, 0));
    }
}
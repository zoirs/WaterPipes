using System;
using UnityEngine;

public class TubeEndController : MonoBehaviour {
    [SerializeField] private Direction direction;
    [SerializeField] private TubeController tubeController;

    private TubeEndController connected;

    public Direction GetDirection(int rotate) {
        return direction.Rotate(rotate);
    }
    
    public Direction GetDirection() {
        return direction.Rotate(tubeController != null ? tubeController.Rotate : 0);
    }

    public Vector3Int GetVector() {
        Vector3 p = transform.position;
        return new Vector3Int((int) Math.Round(p.x, 0),(int) Math.Round(p.y, 0),(int) Math.Round( p.z, 0));
    }
    
    public void Connect(TubeEndController controller) {
        connected = controller;
    }
    
    public void UnConnect() {
        connected = null;
    }

    public bool IsConnected() {
        return connected != null;
    }
    
    public TubeController GetParentTube() {
        return tubeController;
    }
}
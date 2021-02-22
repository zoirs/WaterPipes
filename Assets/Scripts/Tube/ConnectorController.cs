using System;
using Main;
using UnityEngine;

public class ConnectorController : MonoBehaviour {
    [SerializeField] private Direction direction;
    [SerializeField] private GameObject parent;

    private ConnectorController connected;

    public Direction GetDirection(int rotate) {
        return direction.Rotate(rotate);
    }
    
    public Direction GetDirection() {
        if (parent != null) {
            TubeController tubeController = parent.GetComponent<TubeController>();
            if (tubeController != null) {
                return direction.Rotate(tubeController.Rotate);
            }
        }

        return direction.Rotate(0);
    }

    public Vector3Int GetVector() {
        Vector3 p = transform.position;
        return new Vector3Int((int) Math.Round(p.x, 0),(int) Math.Round(p.y, 0),(int) Math.Round( p.z, 0));
    }
    
    public void SetConnect(ConnectorController controller) {
        connected = controller;
    }
    
    public bool TryConnect(ConnectorController connectorController) {
        Vector3Int connectPosition = connectorController.GetVector() + connectorController.GetDirection().GetVector();
        Direction connectDirection = connectorController.GetDirection().Invert();

        if (this.GetVector() == connectPosition && this.GetDirection() == connectDirection) {
            this.SetConnect(connectorController);
            connectorController.SetConnect(this);
            TubeController tubeController = parent.GetComponent<TubeController>();
            if (tubeController != null) {
                tubeController.MarkWater();
            }
            HomeController homeController = parent.GetComponent<HomeController>();
            if (homeController != null) {
                homeController.MarkWater();
            }
            PortalController portalController = parent.GetComponent<PortalController>();
            if (portalController != null) {
                portalController.MarkWater();
            }
            return true;
        }

        return false;
    }
    
    public void UnConnect() {
        connected = null;
    }

    public bool IsConnected() {
        return connected != null;
    }
    
    public TubeController GetParentTube() {
        return parent != null ? parent.GetComponent<TubeController>() : null;
    }
}
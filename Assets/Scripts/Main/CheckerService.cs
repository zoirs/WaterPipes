using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Main {
    public class CheckerService {
        
        [Inject] readonly HomeManager homeManager;
        [Inject] readonly TubeManager tubeManager;
        [Inject] readonly PortalManager portalManager;
        [Inject] readonly WaterManager waterManager;
        [Inject] readonly GameController gameController;

        public void Check() {
            tubeManager.Clear();
            // foreach (WaterController waterController in waterControllers) {
            //     PointController[] points = waterController.GetComponentsInChildren<PointController>();
            //     foreach (PointController point in points) {
            //         Vector3Int wpInt = point.GetVector();
            //         tubeManager.Find(wpInt + Vector3Int.down, Direction.UP);
            //         tubeManager.Find(wpInt + Vector3Int.up, Direction.DOWN);
            //         tubeManager.Find(wpInt + Vector3Int.left, Direction.RIGHT);
            //         tubeManager.Find(wpInt + Vector3Int.right, Direction.LEFT);
            //     }
            // }
            bool result = true;
            foreach (WaterController waterController in waterManager.Objects) {
                ConnectorController[] points = waterController.GetComponentsInChildren<ConnectorController>();
                foreach (ConnectorController point in points) {
                    result &= Find(point);
                }
            }
            Debug.Log("Результат: " + result);
            
            if (result && homeManager.IsComplete()) {
                gameController.State = GameStates.LevelComplete;
            }

            // Vector3 wp = waterControllers[0].transform.position;
            // Vector3Int wpInt = new Vector3Int((int) wp.x, (int) wp.y, (int) wp.z);
            // tubeManager.Find(wpInt + Vector3Int.down, Direction.UP);
            // tubeManager.Find(wpInt + Vector3Int.up, Direction.DOWN);
            // tubeManager.Find(wpInt + Vector3Int.left, Direction.RIGHT);
            // tubeManager.Find(wpInt + Vector3Int.right, Direction.LEFT);
        }

        public bool Find(ConnectorController connectorController) {
        // Vector3Int endPosition = connectorController.GetVector();
        // Vector3Int connectPosition = endPosition + connectorController.GetDirection().GetVector();
        // Direction connectDirection = connectorController.GetDirection().Invert();

        List<HomeController> homes = homeManager.Objects;
        foreach (HomeController home in homes) {
            foreach (ConnectorController freeConnecter in home.GetFreeConnecter()) {
                if (freeConnecter.TryConnect(connectorController)) {
                    return true;
                }
            }
        }

        bool isPortalConnected = false;
        foreach (PortalController portal in portalManager.Objects) {
            foreach (ConnectorController freeConnecter in portal.GetFreeConnecter()) {
                if (freeConnecter.TryConnect(connectorController)) {
                    isPortalConnected = true;
                    break;
                }
            }
        }

        if (isPortalConnected) {
            foreach (PortalController portal in portalManager.Objects) {
                portal.MarkWater();
                ConnectorController[] points = portal.GetComponentsInChildren<ConnectorController>();
                foreach (ConnectorController point in points) {
                    Find(point);
                }
            }
            return true;
        }

        foreach (TubeController tube in tubeManager.Objects) {
            if (connectorController.GetParentTube() != null && connectorController.GetParentTube() == tube) {
                continue;
            }

            List<ConnectorController> freeConnecters = tube.GetFreeConnecter();
            foreach (ConnectorController freeConnecter in freeConnecters) {
                if (freeConnecter.TryConnect(connectorController)) {
                    List<ConnectorController> otherConnecters = tube.GetFreeConnecter();
                    bool allOtherEndsConnected = true;
                    foreach (ConnectorController endController in otherConnecters) {
                        bool result = Find(endController);
                        if (!result) {
                            Debug.Log("Кажется, есть утечка");
                        }

                        allOtherEndsConnected = allOtherEndsConnected & result;
                    }
                    return allOtherEndsConnected;
                }
            }
        }
        // Если это труба от колодца, то не учитываем что ничего не нашли
        return connectorController.GetParentTube() == null;
    }

    }
}
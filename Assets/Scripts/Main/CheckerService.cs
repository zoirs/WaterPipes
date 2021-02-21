using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Main {
    public class CheckerService {
        
        [Inject] readonly HomeManager homeManager;
        [Inject] readonly TubeManager tubeManager;
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
                TubeEndController[] points = waterController.GetComponentsInChildren<TubeEndController>();
                foreach (TubeEndController point in points) {
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

        public bool Find(TubeEndController tubeEndController) {
        Vector3Int endPosition = tubeEndController.GetVector();
        Vector3Int connectPosition = endPosition + tubeEndController.GetDirection().GetVector();
        Direction connectDirection = tubeEndController.GetDirection().Invert();

        List<HomeController> homeManagerHomes = homeManager.Objects;
        foreach (HomeController homeManagerHome in homeManagerHomes) {
            foreach (TubeEndController freeConnecter in homeManagerHome.GetFreeConnecter()) {
                if (freeConnecter.GetVector() == connectPosition &&
                    freeConnecter.GetDirection() == connectDirection) {
                    freeConnecter.Connect(tubeEndController);
                    tubeEndController.Connect(freeConnecter);
                    homeManagerHome.MarkWater();
                    return true;
                }
            }
        }

        foreach (TubeController tube in tubeManager.Objects) {
            if (tubeEndController.GetParentTube() != null && tubeEndController.GetParentTube() == tube) {
                continue;
            }

            List<TubeEndController> freeConnecters = tube.GetFreeConnecter();
            foreach (TubeEndController freeConnecter in freeConnecters) {
                if (freeConnecter.GetVector() == connectPosition && freeConnecter.GetDirection() == connectDirection) {
                    freeConnecter.Connect(tubeEndController);
                    tubeEndController.Connect(freeConnecter);
                    tube.MarkWater();
                    List<TubeEndController> otherConnecters = tube.GetFreeConnecter();
                    bool allOtherEndsConnected = true;
                    foreach (TubeEndController endController in otherConnecters) {
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
        return tubeEndController.GetParentTube() == null;
    }

    }
}
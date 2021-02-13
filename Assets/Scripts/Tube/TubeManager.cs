using System;
using System.Collections.Generic;
using DefaultNamespace.Tube;
using UnityEngine;
using Zenject;

public class TubeManager {
    [Inject] private TubeController.Factory _factoryTube;
    [Inject] private HomeManager _homeManager;

    [Inject] private GameSettingsInstaller.GameSetting setting;
    [Inject] private TubeMapService _tubeMapService;
    [Inject] private GameSettingsInstaller.PrefabSettings prefabs;
    
    private List<TubeController> map = new List<TubeController>();

    public void Create(GameObject prefab) {
        TubeController tubeController = _factoryTube.Create(new TubeCreateParam(prefab));
        map.Add(tubeController);
    }

    public void Clear() {
        foreach (TubeController controller in map) {
            controller.Clear();
        }

        _homeManager.Clear();
    }

    public void Find(Vector3Int pos, Direction directionOnPosition) {
        HomeController home = _homeManager.Find(pos);
        if (home != null) {
            home.MarkWater();
        }

        TubeController tubeController = map.Find(t => {
            // Vector3Int intPosition = new Vector3Int((int) t.transform.position.x, (int) t.transform.position.y, (int) t.transform.position.z);
            // Debug.Log(String.Join("; ", t.GetEndPositions()));
            if (t.GetEndPositions().ContainsKey(directionOnPosition)) {
                return t.GetEndPositions()[directionOnPosition] == pos;
            }

            return false;
            // return intPosition == pos;
        });

        if (tubeController != null) {
            Debug.Log("Нашли трубу на " + pos + ", выходы у этой трубы:" +
                      String.Join("; ", tubeController.GetWaterPosition()) + ", ищем " + directionOnPosition);
        }

        if (tubeController != null) {
            tubeController.MarkWater();
            foreach (KeyValuePair<Direction, Vector3Int> endTube in tubeController.GetEndPositions()) {
                if (endTube.Key == directionOnPosition) {
                    continue;
                }

                // Vector3 wp = tubeController.transform.position;
                Vector3Int wpInt = endTube.Value;
                switch (endTube.Key) {
                    case Direction.DOWN:
                        Find(wpInt + Vector3Int.down, Direction.UP);
                        break;
                    case Direction.RIGHT:
                        Find(wpInt + Vector3Int.right, Direction.LEFT);
                        break;
                    case Direction.UP:
                        Find(wpInt + Vector3Int.up, Direction.DOWN);
                        break;
                    case Direction.LEFT:
                        Find(wpInt + Vector3Int.left, Direction.RIGHT);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            // foreach (Direction directionNext in tubeController.GetWaterPosition()) {
            //     if (directionNext != direction) {
            //         Vector3 wp = tubeController.transform.position;
            //         Vector3Int wpInt = new Vector3Int((int) wp.x, (int) wp.y, (int) wp.z);
            //         switch (directionNext) {
            //             case Direction.DOWN:
            //                 Find(wpInt + Vector3Int.down, Direction.UP);
            //                 break;
            //             case Direction.RIGHT:
            //                 Find(wpInt + Vector3Int.right, Direction.LEFT);
            //                 break;
            //             case Direction.UP:
            //                 Find(wpInt + Vector3Int.up, Direction.DOWN);
            //                 break;
            //             case Direction.LEFT:
            //                 Find(wpInt + Vector3Int.left, Direction.RIGHT);
            //                 break;
            //             default:
            //                 throw new ArgumentOutOfRangeException();
            //         }
            //     }
            // }
        }
        
    }

    public List<TubeController> Map => map;

    public void Start(List<InventoryDto> levelTubes) {
        foreach (InventoryDto item in levelTubes) {
            TubeController tube = _factoryTube.Create(new TubeCreateParam(item.tubeType.GetPrefab(prefabs)));
            tube.transform.position = new Vector3(item.position.x, item.position.y, 0f);
            tube.Rotate = item.rotate;
            map.Add(tube);   
        }
    }
}
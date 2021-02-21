using System;
using System.Collections.Generic;
using DefaultNamespace.Tube;
using ModestTree;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

public class TubeManager : ObjectManager<TubeController, InventoryDto, TubeCreateParam> {
    // [Inject] private TubeController.Factory _factoryTube;
    [Inject] private HomeManager _homeManager;

    // [Inject] private GameSettingsInstaller.GameSetting setting;
    // [Inject] private TubeMapService _tubeMapService;
    [Inject] private GameSettingsInstaller.PrefabSettings prefabs;
    // private List<TubeController> map = new List<TubeController>();

    public TubeManager(TubeMapService tubeMapService,
        GameSettingsInstaller.GameSetting setting,
        GameSettingsInstaller.PrefabSettings prefabSettings, TubeController.Factory factoryTube, DiContainer container)
        : base(tubeMapService, setting, prefabSettings, factoryTube, container) {
        
    }

    // public void Create(GameObject prefab) {
        // TubeController tubeController = _factoryTube.Create(new TubeCreateParam(prefab));
        // map.Add(tubeController);
    // }

    public void Clear() {
        foreach (TubeController controller in Objects) {
            controller.Clear();
        }

        _homeManager.Clear();
    }

    
    // public void Find(Vector3Int pos, Direction directionOnPosition) {
    //     HomeController home = _homeManager.Find(pos);
    //     if (home != null) {
    //         home.MarkWater();
    //     }
    //
    //     TubeController tubeController = map.Find(t => {
    //         Dictionary<Direction,Vector3Int> freeEndPositions = t.GetFreeEndPositions();
    //         if (freeEndPositions.ContainsKey(directionOnPosition)) {
    //             return freeEndPositions[directionOnPosition] == pos;
    //         }
    //
    //         return false;
    //     });
    //
    //     if (tubeController != null) {
    //         Debug.Log("Нашли трубу на " + pos + ", выходы у этой трубы:" +
    //                   String.Join("; ", tubeController.GetWaterPosition()) + ", ищем " + directionOnPosition);
    //     }
    //
    //     if (tubeController != null) {
    //         tubeController.MarkWater();
    //         foreach (KeyValuePair<Direction, Vector3Int> endTube in tubeController.GetFreeEndPositions()) {
    //             if (endTube.Key == directionOnPosition) {
    //                 continue;
    //             }
    //
    //             Vector3Int wpInt = endTube.Value;
    //             switch (endTube.Key) {
    //                 case Direction.DOWN:
    //                     Find(wpInt + Vector3Int.down, Direction.UP);
    //                     break;
    //                 case Direction.RIGHT:
    //                     Find(wpInt + Vector3Int.right, Direction.LEFT);
    //                     break;
    //                 case Direction.UP:
    //                     Find(wpInt + Vector3Int.up, Direction.DOWN);
    //                     break;
    //                 case Direction.LEFT:
    //                     Find(wpInt + Vector3Int.left, Direction.RIGHT);
    //                     break;
    //                 default:
    //                     throw new ArgumentOutOfRangeException();
    //             }
    //         }
    //     }
    //     
    // }

    // public List<TubeController> Map => Objects;

    // public void Start(List<InventoryDto> levelTubes) {
    //     foreach (InventoryDto item in levelTubes) {
    //         TubeController tube = _factoryTube.Create(new TubeCreateParam(item.tubeType.GetPrefab(prefabs)));
    //         tube.transform.position = new Vector3(item.position.x, item.position.y, 0f);
    //         tube.Rotate = item.rotate;
    //         map.Add(tube);   
    //     }
    // }

    // public void Remove(TubeController tubeController) {
    //     TubeController find = Objects.Find(controller => controller == tubeController);
    //     if (find != null) {
    //         Objects.Remove(find);
    //         Object.Destroy(find.gameObject);
    //     }
    // }

    // public override TubeController Create(InventoryDto dto) {
        // return _factoryTube.Create(new TubeCreateParam(dto.GetPrefab(prefabs), dto.rotate, dto.position));
    // }
    
    public override TubeCreateParam Convert(InventoryDto dto) {
        return new TubeCreateParam(dto.GetPrefab(prefabs), dto.rotate, dto.position);
    }

    // public TubeController Create(GameObject prefab) {
    //     TubeController tubeController = _factoryTube.Create(new TubeCreateParam(prefab, 0, Vector2Int.zero));
    //     Objects.Add(tubeController);
    //     return tubeController;
    // }
}
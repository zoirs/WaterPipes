using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WaterManager {
    
    [Inject] private WaterController.Factory _factory;
    [Inject] private TubeManager tubeManager;
    [Inject] private GameSettingsInstaller.GameSetting setting;
    [Inject] private GameSettingsInstaller.PrefabSettings prefabs;
    [Inject] private TubeMapService _tubeMapService;

    private List<WaterController> waterControllers = new List<WaterController>();

    public void Start(List<WaterWellsDto> levelWaterWells) {
        foreach (WaterWellsDto well in levelWaterWells) {
            WaterController item = _factory.Create(new WaterCreateParam(well.wellType.GetPrefab(prefabs)));
            if (setting.isDebug) {
                item.gameObject.AddComponent<DebugMoveController>();
            }

            item.transform.position = new Vector3(well.position.x,well.position.y, 0f);
            _tubeMapService.Busy(item.GetComponentsInChildren<PointController>());
            waterControllers.Add(item);
        }
    }

    public void Check() {
        tubeManager.Clear();
        foreach (WaterController waterController in waterControllers) {
            PointController[] points = waterController.GetComponentsInChildren<PointController>();
            foreach (PointController point in points) {
                Vector3Int wpInt = point.GetVector();
                tubeManager.Find(wpInt + Vector3Int.down, Direction.UP);
                tubeManager.Find(wpInt + Vector3Int.up, Direction.DOWN);
                tubeManager.Find(wpInt + Vector3Int.left, Direction.RIGHT);
                tubeManager.Find(wpInt + Vector3Int.right, Direction.LEFT);
            }
        }
        // Vector3 wp = waterControllers[0].transform.position;
        // Vector3Int wpInt = new Vector3Int((int) wp.x, (int) wp.y, (int) wp.z);
        // tubeManager.Find(wpInt + Vector3Int.down, Direction.UP);
        // tubeManager.Find(wpInt + Vector3Int.up, Direction.DOWN);
        // tubeManager.Find(wpInt + Vector3Int.left, Direction.RIGHT);
        // tubeManager.Find(wpInt + Vector3Int.right, Direction.LEFT);
    }

    public List<WaterController> WaterControllers => waterControllers;

    public void Create(WellType wellType) {
        WaterController waterController = _factory.Create(new WaterCreateParam(wellType.GetPrefab(prefabs)));
        waterController.transform.position = new Vector3(  5,  5f, 0f);
        if (setting.isDebug) {
            waterController.gameObject.AddComponent<DebugMoveController>();
        }
        waterControllers.Add(waterController);
    }
}
using System;
using UnityEngine;
using Zenject;

public class StoneManager : ObjectManager<StoneController, StonesDto, StoneCreateParam> {

    // private GameSettingsInstaller.PrefabSettings prefabs;
    // private GameSettingsInstaller.GameSetting setting;
    // private TubeMapService _tubeMapService;

    public StoneManager(StoneController.Factory factory, TubeMapService tubeMapService,
        GameSettingsInstaller.GameSetting setting, GameSettingsInstaller.PrefabSettings prefabSettings, DiContainer container) : base(
        tubeMapService, setting, prefabSettings, factory, container) {
        // prefabs = prefabSettings;
    }

    // private List<StoneController> items = new List<StoneController>();


    // public void Create() {
    //     StoneController item = _factory.Create();
    //     if (setting.isDebug) {
    //         item.gameObject.AddComponent<DebugMoveController>();
    //     }
    //     item.transform.position = new Vector3(  7,  7f, 0f);
    //     items.Add(item);
    // }

    // public List<StoneController> Items => items;

    // public void Start(List<StonesDto> levelHomes) {
    //     foreach (StonesDto item in levelHomes) {
    //         StoneController stone = _factory.Create();
    //         if (setting.isDebug) {
    //             stone.gameObject.AddComponent<DebugMoveController>();
    //         }
    //         stone.transform.position = new Vector3(item.position.x,item.position.y, 0f);
    //         _tubeMapService.Busy(stone.GetComponentsInChildren<PointController>());
    //         items.Add(stone);   
    //     }
    //
    // }

    // public override StoneController Create(StonesDto dto) {
        // return _factory.Create(new StoneCreateParam(dto.GetPrefab(prefabs), dto.position));
    // }

    public override StoneCreateParam Convert(StonesDto dto) {
        return new StoneCreateParam(dto.GetPrefab(prefabs), dto.position);
    }

    // public override StoneController Create(GameObject prefab) {
        // return _factory.Create(new StoneCreateParam(prefab, Vector2Int.zero));
    // }

    public void CreateDebug() {
        if (!_setting.isDebug) {
            throw new Exception("Unsupport");
        }

        Create(new StonesDto(Constants.CREATE_POSITION));
    }
}
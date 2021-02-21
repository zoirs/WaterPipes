using System;
using UnityEngine;
using Zenject;

public class WaterManager : ObjectManager<WaterController, WaterWellsDto, WaterCreateParam> {
    private readonly GameSettingsInstaller.PrefabSettings prefabs;

    public WaterManager(TubeMapService tubeMapService,
        GameSettingsInstaller.GameSetting setting,
        WaterController.Factory factory,
        GameSettingsInstaller.PrefabSettings prefabs, DiContainer container)
        : base(tubeMapService, setting, prefabs, factory, container) {
        this.prefabs = prefabs;
    }

    // public override WaterController Create(WaterWellsDto dto) {
    // return _factory.Create(new WaterCreateParam(dto.GetPrefab(prefabs), dto.position));
    // }

    public override WaterCreateParam Convert(WaterWellsDto dto) {
        return new WaterCreateParam(dto.GetPrefab(prefabs), dto.position);
    }

    // public override WaterController Create(GameObject prefab) {
    // return _factory.Create(new WaterCreateParam(prefab, Vector2Int.zero));
    // }

    public void CreateDebug(WellType wellType) {
        if (!_setting.isDebug) {
            throw new Exception("Unsupport");
        }

        Create(new WaterWellsDto(wellType, Constants.CREATE_POSITION));
    }
}
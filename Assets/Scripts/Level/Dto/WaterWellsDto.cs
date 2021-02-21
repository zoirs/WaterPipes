using System;
using UnityEngine;

[Serializable]
public class WaterWellsDto : BaseDto {
    public WellType wellType;

    public WaterWellsDto(WellType wellType, Vector2Int position) : base(position) {
        this.wellType = wellType;
    }

    public override GameObject GetPrefab(GameSettingsInstaller.PrefabSettings prefabs) {
       return wellType.GetPrefab(prefabs);
    }
}
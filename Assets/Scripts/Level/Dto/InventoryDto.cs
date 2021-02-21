using System;
using UnityEngine;

[Serializable]

public class InventoryDto : BaseDto {
    public TubeType tubeType;
    public int rotate;

    public InventoryDto(TubeType tubeType, Vector2Int position, int rotate) : base(position) {
        this.tubeType = tubeType;
        this.rotate = rotate;
    }

    public override GameObject GetPrefab(GameSettingsInstaller.PrefabSettings prefabs) {
        return tubeType.GetPrefab(prefabs);
    }
}
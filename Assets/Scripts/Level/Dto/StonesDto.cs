using System;
using UnityEngine;

[Serializable]
public class StonesDto : BaseDto {
    public StonesDto(Vector2Int position) : base(position) { }

    public override GameObject GetPrefab(GameSettingsInstaller.PrefabSettings prefabs) {
        return prefabs.StonePrefab;
    }
}
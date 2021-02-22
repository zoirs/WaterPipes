using System;
using UnityEngine;

[Serializable]
public class PortalDto : BaseDto {
    public PortalDto(Vector2Int position) : base(position) { }

    public override GameObject GetPrefab(GameSettingsInstaller.PrefabSettings prefabs) {
        return prefabs.PortalPrefab;
    }
}
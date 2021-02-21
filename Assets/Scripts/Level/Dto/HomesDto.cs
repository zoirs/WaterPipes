using System;
using UnityEngine;

[Serializable]
public class HomesDto:BaseDto {
    public HomeType homeType;

    public HomesDto(HomeType homeType, Vector2Int position) : base(position) {
        this.homeType = homeType;
    }

    public override GameObject GetPrefab(GameSettingsInstaller.PrefabSettings prefabs) {
        return homeType.GetPrefab(prefabs);
    }
}
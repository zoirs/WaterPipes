using System;
using UnityEngine;

public enum WellType {
    ONE_ONE,
    TWO_TWO
}

public static class WellTypeExtension {
    
    
    public static GameObject GetPrefab(this WellType wellType, GameSettingsInstaller.PrefabSettings prefabs) {
        switch (wellType) {
            case WellType.ONE_ONE:
                return prefabs.WaterPrefab;
            case WellType.TWO_TWO:
                return prefabs.WaterPrefab2_2;
            default:
                throw new ArgumentOutOfRangeException(nameof(wellType), wellType, null);
        }
    }
}
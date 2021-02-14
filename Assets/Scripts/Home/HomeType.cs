using System;
using UnityEngine;

public enum HomeType {
    ONE_ONE,
    TWO_TWO
}

public static class HomeTypeExtension {
    
    
    public static GameObject GetPrefab(this HomeType homeType, GameSettingsInstaller.PrefabSettings prefabs) {
        switch (homeType) {
            case HomeType.ONE_ONE:
                return prefabs.HomePrefab;
            case HomeType.TWO_TWO:
                return prefabs.HomePrefab2_2;
            default:
                throw new ArgumentOutOfRangeException(nameof(homeType), homeType, null);
        }
    }
}

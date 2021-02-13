using System;
using System.Collections.Generic;
using UnityEngine;

public enum TubeType {
    NONE,
    LINE1,
    LINE2,
    LINE3,
    LINE4,
    ANGEL,
    TRIANGLE,
    QUATRO
}

//    3
//    _
// 4 |_| 2 
//    1
public static class TubeTypeExtension {
    
    public static List<Direction> GetWaterDirection(this TubeType tuneType) {
        switch (tuneType) {
            case TubeType.LINE1:
            case TubeType.LINE2:
            case TubeType.LINE3:
            case TubeType.LINE4:
                return new List<Direction>() {Direction.DOWN, Direction.UP};
            case TubeType.ANGEL:
                return new List<Direction>() {Direction.DOWN, Direction.RIGHT};
            case TubeType.TRIANGLE:
                return new List<Direction>() {Direction.DOWN, Direction.RIGHT, Direction.LEFT};
            case TubeType.QUATRO:
                return new List<Direction>() {Direction.DOWN, Direction.RIGHT, Direction.UP, Direction.LEFT};
            default:
                throw new ArgumentOutOfRangeException(nameof(tuneType), tuneType, null);
        }
    }
    
    public static List<Direction> GetWaterDirection(this TubeType tuneType, int rotate) {
        switch (tuneType) {
            case TubeType.LINE1:
            case TubeType.LINE2:
            case TubeType.LINE3:
            case TubeType.LINE4:
                return new List<Direction>() {Direction.DOWN.Rotate(rotate), Direction.UP.Rotate(rotate)};
            case TubeType.ANGEL:
                return new List<Direction>() {Direction.DOWN.Rotate(rotate), Direction.RIGHT.Rotate(rotate)};
            case TubeType.TRIANGLE:
                return new List<Direction>() {Direction.DOWN.Rotate(rotate), Direction.RIGHT.Rotate(rotate), Direction.LEFT.Rotate(rotate)};
            case TubeType.QUATRO:
                return new List<Direction>() {Direction.DOWN.Rotate(rotate), Direction.RIGHT.Rotate(rotate), Direction.UP.Rotate(rotate), Direction.LEFT.Rotate(rotate)};
            default:
                throw new ArgumentOutOfRangeException(nameof(tuneType), tuneType, null);
        }
    }
    
    public static GameObject GetPrefab(this TubeType tuneType, GameSettingsInstaller.PrefabSettings prefabs) {
        switch (tuneType) {
            case TubeType.LINE1:
                return prefabs.Tube1Prefab;
            case TubeType.LINE2:
                return prefabs.Tube2Prefab;
            case TubeType.LINE3:
                return prefabs.Tube3Prefab;
            // case TubeType.LINE4:
                // return prefabs.Tube4Prefab;
            case TubeType.ANGEL:
                return prefabs.TubeAngelPrefab;
            case TubeType.TRIANGLE:
                return prefabs.TubeTrianglePrefab;
            case TubeType.QUATRO:
                return prefabs.TubeQuatroPrefab;
            default:
                throw new ArgumentOutOfRangeException(nameof(tuneType), tuneType, null);
        }
    }
}
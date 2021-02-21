using UnityEngine;

public class TubeCreateParam : CreateParam {
    private readonly int rotation;

    public TubeCreateParam(GameObject prefab, int rotation, Vector2Int position) : base(prefab, position) {
        this.rotation = rotation;
    }

    public int Rotation => rotation;
}
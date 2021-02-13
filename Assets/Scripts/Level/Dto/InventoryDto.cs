using System;

[Serializable]

public class InventoryDto : Base {
    public TubeType tubeType;
    public int rotate;

    public InventoryDto(TubeType tubeType, int rotate) {
        this.tubeType = tubeType;
        this.rotate = rotate;
    }
    
}
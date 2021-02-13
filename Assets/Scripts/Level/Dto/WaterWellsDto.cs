using System;

[Serializable]
public class WaterWellsDto : Base {
    private WellType wellType;

    public WaterWellsDto(WellType wellType) {
        this.wellType = wellType;
    }
}
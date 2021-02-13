using System;

[Serializable]
public class WaterWellsDto : Base {
    public WellType wellType;

    public WaterWellsDto(WellType wellType) {
        this.wellType = wellType;
    }
}
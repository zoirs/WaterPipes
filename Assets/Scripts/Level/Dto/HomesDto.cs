using System;

[Serializable]
public class HomesDto:Base {
    private HomeType homeType;

    public HomesDto(HomeType homeType) {
        this.homeType = homeType;
    }
}
using System;

[Serializable]
public class HomesDto:Base {
    public HomeType homeType;

    public HomesDto(HomeType homeType) {
        this.homeType = homeType;
    }
}
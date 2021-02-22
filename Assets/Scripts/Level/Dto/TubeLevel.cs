using System;
using System.Collections.Generic;

[Serializable]
public class TubeLevel {
        public int wight;
        public int height;
        public List<HomesDto> homes = new List<HomesDto>();
        public List<InventoryDto> inventory = new List<InventoryDto>();
        public List<StonesDto> stones = new List<StonesDto>();
        public List<WaterWellsDto> waterWells = new List<WaterWellsDto>();
        public List<PortalDto> portals = new List<PortalDto>();
}
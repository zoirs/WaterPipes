    using System.IO;
    using UnityEngine;
    using Zenject;

    public class LevelManager {
 
        [Inject]
        private WaterManager _waterManager;
        [Inject]
        private HomeManager _homeManager;
        [Inject] 
        private TubeManager _tubeManager;
        [Inject] 
        private StoneManager _stoneManager;
        [Inject] 
        private InventoryManager _inventoryManager;
        [Inject]
        private GameSettingsInstaller.GameSetting _setting;


        private static Vector2Int q = new Vector2Int(0, 0);

        public void SaveLevel() {
            TubeLevel tubeLevel = new TubeLevel();
            foreach (HomeController homeManagerHome in _homeManager.Objects) {
                HomesDto item = new HomesDto(homeManagerHome.HomeType, (Vector2Int) homeManagerHome.GetVector()  - q);
                tubeLevel.homes.Add(item);
            }
            foreach (TubeController tubeController in _tubeManager.Objects) {
                InventoryDto inventoryDto = new InventoryDto(tubeController.TubeType, (Vector2Int) tubeController.GetVector() - q, tubeController.Rotate);
                tubeLevel.inventory.Add(inventoryDto);
            }
            foreach (WaterController item in _waterManager.Objects) {
                WaterWellsDto wellsDto = new WaterWellsDto(item.WellType, (Vector2Int) item.GetVector() - q);
                tubeLevel.waterWells.Add(wellsDto);
            }
            foreach (StoneController item in _stoneManager.Objects) {
                StonesDto stonesDto = new StonesDto((Vector2Int) item.GetVector() - q);
                tubeLevel.stones.Add(stonesDto);
            }

            string json = JsonUtility.ToJson(tubeLevel);
            Debug.Log(json);
            DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
            string levelName = dir.GetFileSystemInfos("*.txt").Length + ".txt";
            
            string filepath = Application.persistentDataPath + "/"+levelName;

            StreamWriter writer = new StreamWriter(filepath, false);
            writer.WriteLine(JsonUtility.ToJson(tubeLevel));
            writer.Close();
        }

        public void LoadLevel(TubeLevel level) {
            _waterManager.Reload(level.waterWells);
            _homeManager.Reload(level.homes);
            _stoneManager.Reload(level.stones);
            _tubeManager.Reload(level.inventory);
            _inventoryManager.Unlock(level.inventory);
        }
    }
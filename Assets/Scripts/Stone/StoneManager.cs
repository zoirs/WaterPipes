    using System.Collections.Generic;
    using UnityEngine;
    using Zenject;

    public class StoneManager {
 
        
        [Inject] private StoneController.Factory _factory;
        [Inject] private GameSettingsInstaller.GameSetting setting;
        [Inject] private TubeMapService _tubeMapService;
        
        private List<StoneController> items = new List<StoneController>();
        
        
        public void Create() {
            StoneController item = _factory.Create();
            if (setting.isDebug) {
                item.gameObject.AddComponent<DebugMoveController>();
            }
            item.transform.position = new Vector3(  7,  7f, 0f);
            items.Add(item);
        }

        public List<StoneController> Items => items;

        public void Start(List<StonesDto> levelHomes) {
            foreach (StonesDto item in levelHomes) {
                StoneController stone = _factory.Create();
                if (setting.isDebug) {
                    stone.gameObject.AddComponent<DebugMoveController>();
                }
                stone.transform.position = new Vector3(item.position.x,item.position.y, 0f);
                _tubeMapService.Busy(stone.GetComponentsInChildren<PointController>());
                items.Add(stone);   
            }

        }
    }
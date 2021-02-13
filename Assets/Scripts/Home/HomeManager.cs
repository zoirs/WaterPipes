using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HomeManager {
    [Inject] private HomeController.Factory _factory;
    [Inject] private GameSettingsInstaller.GameSetting setting;
    [Inject] private TubeMapService _tubeMapService;

    private List<HomeController> homes = new List<HomeController>();

    public void Start(List<HomesDto> levelHomes) {
        foreach (HomesDto levelHome in levelHomes) {
            HomeController home = _factory.Create();
            if (setting.isDebug) {
                home.gameObject.AddComponent<DebugMoveController>();
            }
            Debug.Log(home.GetComponentsInChildren<PointController>());
            home.transform.position = new Vector3(levelHome.position.x,levelHome.position.y, 0f);
            _tubeMapService.Busy(home.GetComponentsInChildren<PointController>());
            homes.Add(home);   
        }
        // HomeController home2 = _factory.Create();
        // home2.transform.position = new Vector3(7f, 14f, 0f);
        
        // homes.Add(home);
        // homes.Add(home2);
    }

    public void Create() {
        HomeController home = _factory.Create();
        if (setting.isDebug) {
            home.gameObject.AddComponent<DebugMoveController>();
        }
        home.transform.position = new Vector3(6,6, 0f);
        homes.Add(home);
    }

    public void Clear() {
        foreach (HomeController controller in homes) {
            controller.Clear();
        }
    }

    public HomeController Find(Vector3Int pos) {
        return homes.Find(h => h.GetVector() == pos);
    }

    public List<HomeController> Homes => homes;
}
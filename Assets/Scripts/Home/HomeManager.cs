using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class HomeManager : ObjectManager<HomeController, HomesDto, HomeCreateParam> {
    [Inject] private GameSettingsInstaller.GameSetting setting;
    // [Inject] private TubeMapService _tubeMapService;
    [Inject] private GameSettingsInstaller.PrefabSettings prefabs;

    public HomeManager(TubeMapService tubeMapService, 
        GameSettingsInstaller.GameSetting setting,
        GameSettingsInstaller.PrefabSettings prefabSettings,
        HomeController.Factory factory, DiContainer container) : base(tubeMapService, setting, prefabSettings, factory, container) { }

    // private List<HomeController> homes = new List<HomeController>();

    // public void Start(List<HomesDto> levelHomes) {
    //     foreach (HomesDto levelHome in levelHomes) {
    //         HomeController home = _factory.Create(new HomeCreateParam(levelHome.homeType.GetPrefab(prefabs)));
    //         if (setting.isDebug) {
    //             home.gameObject.AddComponent<DebugMoveController>();
    //         }
    //         Debug.Log(home.GetComponentsInChildren<PointController>());
    //         home.transform.position = new Vector3(levelHome.position.x,levelHome.position.y, 0f);
    //         _tubeMapService.Busy(home.GetComponentsInChildren<PointController>());
    //         homes.Add(home);   
    //     }
    //     // HomeController home2 = _factory.Create();
    //     // home2.transform.position = new Vector3(7f, 14f, 0f);
    //     
    //     // homes.Add(home);
    //     // homes.Add(home2);
    // }

    // public void Create(HomeType homeType) {
    //     HomeController home = _factory.Create(new HomeCreateParam(homeType.GetPrefab(prefabs)));
    //     if (setting.isDebug) {
    //         home.gameObject.AddComponent<DebugMoveController>();
    //     }
    //     home.transform.position = new Vector3(6,6, 0f);
    //     _tubeMapService.Busy(home.GetComponentsInChildren<PointController>());
    //     homes.Add(home);
    // }

    public void Clear() {
        foreach (HomeController controller in Objects) {
            controller.Clear();
        }
    }

    public HomeController Find(Vector3Int pos) {
        return Objects.Find(h => {
            PointController[] points = h.GetComponentsInChildren<PointController>();
            return points.Any(point => point.GetVector() == pos);
        });
    }

    // public List<HomeController> Homes => homes;
    // public override HomeController Create(HomesDto dto) {
        // return _factory.Create(new HomeCreateParam(dto.GetPrefab(prefabs), dto.position));
    // }

    public override HomeCreateParam Convert(HomesDto dto) {
        return new HomeCreateParam(dto.GetPrefab(prefabs), dto.position);
    }

    // public override HomeController Create(GameObject prefab) {
        // return _factory.Create(new HomeCreateParam(prefab, Vector2Int.zero));
    // }

    public void CreateDebug(HomeType homeType) {
        if (!_setting.isDebug) {
            throw new Exception("Unsupport");
        }

        Create(new HomesDto(homeType, Constants.CREATE_POSITION));
    }

    public bool IsComplete() {
        foreach (HomeController homeController in Objects) {
            if (!homeController.HasWater) {
                return false;
            }
        }
        return true;
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using Zenject;
using Object = UnityEngine.Object;

public abstract class ObjectManager<CONTROLLER, DTO, CREATE>
    where CONTROLLER : MonoBehaviour
    where DTO : BaseDto {
    private TubeMapService _tubeMapService;
    protected GameSettingsInstaller.GameSetting _setting;

    private List<CONTROLLER> objects = new List<CONTROLLER>();
    protected GameSettingsInstaller.PrefabSettings prefabs;
    private PlaceholderFactory<CREATE, CONTROLLER> _factory;
    private DiContainer _container;
    

    protected ObjectManager(TubeMapService tubeMapService, GameSettingsInstaller.GameSetting setting,
        GameSettingsInstaller.PrefabSettings prefabSettings, PlaceholderFactory<CREATE, CONTROLLER> factory,
        DiContainer container) {
        _tubeMapService = tubeMapService;
        _setting = setting;
        prefabs = prefabSettings;
        _factory = factory;
        _container = container;
    }

    public void Reload(List<DTO> createParams) {
        foreach (CONTROLLER o in objects) {
            _tubeMapService.Free(o.GetComponentsInChildren<PointController>());
            Object.Destroy(o.gameObject);
        }

        objects.Clear();

        bool isTube = typeof(CONTROLLER) == typeof(TubeController);
        foreach (DTO paramDto in createParams) {
            if (!isTube) {
                CONTROLLER go = Create(paramDto);
                _tubeMapService.Busy(go.GetComponentsInChildren<PointController>());
            }
            else if (_setting.isDebug) {
                //трубы создаем только в дебаг режиме
                Create(paramDto);
            }
            // bool isTube = typeof(CONTROLLER) == typeof(TubeController);
            // if (_setting.isDebug && !isTube) {
            //     item.gameObject.AddComponent<DebugMoveController>();
            // }

            // item.transform.position = new Vector3(paramDto.position.x, paramDto.position.y, 0f);
            // _tubeMapService.Busy(item.GetComponentsInChildren<PointController>());
        }
    }

    public CONTROLLER Create(DTO paramDto) {
        CONTROLLER item = _factory.Create(Convert(paramDto));
        objects.Add(item);
        if (_setting.isDebug) {
            GameObject gameObject = item.gameObject;
            _container.InstantiateComponent<Removable>(gameObject);
            bool isTube = typeof(CONTROLLER) == typeof(TubeController);
            if (!isTube) {
                gameObject.AddComponent<DebugMoveController>();
            }
        }

        return item;
    }

    public abstract CREATE Convert(DTO dto);

    // public void Create(WellType wellType) {
    //     WaterController waterController = _factory.Create(new WaterCreateParam(wellType.GetPrefab(prefabs)));
    //     waterController.transform.position = new Vector3(5, 5f, 0f);
    //     if (_setting.isDebug) {
    //         waterController.gameObject.AddComponent<DebugMoveController>();
    //     }
    //
    //     Objects.Add(waterController);
    // }

    public List<CONTROLLER> Objects => objects;

    // public abstract CONTROLLER Create(DTO dto);
    public void Remove(Removable removable) {
        CONTROLLER find = Objects.Find(controller => controller.gameObject == removable.gameObject);
        Debug.Log("remove " + find);
        if (find != null) {
            Objects.Remove(find);
            Object.Destroy(find.gameObject);
        }
    }
}
using DefaultNamespace.Tube;
using Main;
using Map;
using UnityEngine;
using Zenject;

// документация https://github.com/modesttree/Zenject
public class GameInstaller : MonoInstaller {
    [Inject] GameSettingsInstaller.PrefabSettings _prefabs;

    public override void InstallBindings() {
        Container.Bind<ScreenService>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameController>().AsSingle();
        Container.BindInterfacesAndSelfTo<WaterManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<CheckerService>().AsSingle();
        Container.BindInterfacesAndSelfTo<MapService>().AsSingle();
        Container.BindInterfacesAndSelfTo<TubeManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<HomeManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<MoneyService>().AsSingle();
        Container.BindInterfacesAndSelfTo<TubeMapService>().AsSingle();
        Container.BindInterfacesAndSelfTo<InventoryManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<LevelManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<StoneManager>().AsSingle();

        
        // Container.BindFactory<StoneController, StoneController.Factory>()
        //     .FromComponentInNewPrefab(_prefabs.StonePrefab)
        //     .WithGameObjectName("Stone")
        //     .UnderTransformGroup("Stones");

        Container.BindFactory<StoneCreateParam, StoneController, StoneController.Factory>()
            .FromMethod(CreateStone); //рабочий вариант
        Container.BindFactory<TubeCreateParam, TubeController, TubeController.Factory>()
            .FromMethod(CreateTube); //рабочий вариант
        Container.BindFactory<HomeCreateParam, HomeController, HomeController.Factory>()
            .FromMethod(CreateHome); //рабочий вариант
        Container.BindFactory<WaterCreateParam,  WaterController, WaterController.Factory>()
            .FromMethod(CreateWater); //рабочий вариант

        InstallSignals();
    }

    private StoneController CreateStone(DiContainer subContainer, StoneCreateParam createParam) {
        StoneController stone =
            subContainer.InstantiatePrefabForComponent<StoneController>(createParam.Prefab,
                GameObject.Find("Stones").transform);
        stone.transform.position = new Vector3(createParam.Position.x, createParam.Position.y, 0f);
        return stone;
    }

    TubeController CreateTube(DiContainer subContainer, TubeCreateParam createParam) {
        TubeController tube =
            subContainer.InstantiatePrefabForComponent<TubeController>(createParam.Prefab,
                GameObject.Find("Tubes").transform);
        tube.transform.position = new Vector3(createParam.Position.x, createParam.Position.y, 0f);
        tube.Rotate = createParam.Rotation;
        return tube;
    }

    HomeController CreateHome(DiContainer subContainer, HomeCreateParam createParam) {
        HomeController controller =
            subContainer.InstantiatePrefabForComponent<HomeController>(createParam.Prefab,
                GameObject.Find("Homes").transform);
        controller.transform.position = new Vector3(createParam.Position.x, createParam.Position.y, 0f);
        return controller;
    }


    WaterController CreateWater(DiContainer subContainer, WaterCreateParam createParam) {
        WaterController controller =
            subContainer.InstantiatePrefabForComponent<WaterController>(createParam.Prefab,
                GameObject.Find("Waters").transform);
        controller.transform.position = new Vector3(createParam.Position.x, createParam.Position.y, 0f);
        return controller;
    }



    void InstallSignals() {
        // Every scene that uses signals needs to install the built-in installer SignalBusInstaller
        // Or alternatively it can be installed at the project context level (see docs for details)
        SignalBusInstaller.Install(Container);

        // Signals can be useful for game-wide events that could have many interested parties
        Container.DeclareSignal<AddProductSignal>();
        Container.DeclareSignal<ChangeLevelSignal>();
    }
}
﻿using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Tube;
using Line;
using Main;
using Map;
using Train;
using UnityEngine;
using Zenject;

// документация https://github.com/modesttree/Zenject
public class GameInstaller : MonoInstaller {
    [Inject] GameSettingsInstaller.PrefabSettings _prefabs;

    public override void InstallBindings() {
        Container.Bind<ScreenService>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameController>().AsSingle();
        // Container.BindInterfacesAndSelfTo<PeopleManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<TaskManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<WaterManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<TrainManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<MapService>().AsSingle();
        Container.BindInterfacesAndSelfTo<CityManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<TubeManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<HomeManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<MoneyService>().AsSingle();
        Container.BindInterfacesAndSelfTo<LineManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<TubeMapService>().AsSingle();
        Container.BindInterfacesAndSelfTo<InventoryManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<LevelManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<StoneManager>().AsSingle();

        Container.BindFactory<TrainController, TrainController.Factory>()
            .FromComponentInNewPrefab(_prefabs.TrainPrefab)
            .WithGameObjectName("Train")
            .UnderTransformGroup("Trains");

//        Container.BindFactory<DrowLineComponent, DrowLineComponent.Factory>()
//            .FromComponentInNewPrefab(_prefabs.LinePrefab)
//            .WithGameObjectName("Line")
//            .UnderTransformGroup("Lines");

        Container.BindFactory<List<Vector3>, List<Company>, DrowLineComponent, DrowLineComponent.Factory>()
            .FromMethod(CreateLine);

        Container.BindFactory<CityController, CityController.Factory>()
            .FromComponentInNewPrefab(_prefabs.StationPrefab)
            .WithGameObjectName("Station")
            .UnderTransformGroup("Stations");

        Container.BindFactory<WaterController, WaterController.Factory>()
            .FromComponentInNewPrefab(_prefabs.WaterPrefab)
            .WithGameObjectName("Water")
            .UnderTransformGroup("Waters");

        Container.BindFactory<HomeController, HomeController.Factory>()
            .FromComponentInNewPrefab(_prefabs.HomePrefab)
            .WithGameObjectName("Home")
            .UnderTransformGroup("Homes");

        Container.BindFactory<ManufacturerController, ManufacturerController.Factory>()
            .FromComponentInNewPrefab(_prefabs.ManufacturePrefab)
            .WithGameObjectName("Manufacture")
            .UnderTransformGroup("Manufactures");
        
        Container.BindFactory<StoneController, StoneController.Factory>()
            .FromComponentInNewPrefab(_prefabs.StonePrefab)
            .WithGameObjectName("Stone")
            .UnderTransformGroup("Stones");

        // Container.BindFactory<TubeController, TubeController.Factory>()
        //     .FromComponentInNewPrefab(_prefabs.Tube1Prefab)
        //     .WithGameObjectName("Tube1")
        //     .UnderTransformGroup("Tube");

        Container.BindFactory<TubeCreateParam, TubeController, TubeController.Factory>()
            .FromMethod(CreateTube); //рабочий вариант

//        Container.BindFactory<ProductEntity, ProductEntity.Factory>()
//            .FromComponentInNewPrefab(_prefabs.PeoplePrefab)
//            .WithGameObjectName("People")
//            .UnderTransformGroup("Peoples");

        InstallSignals();
    }

    TubeController CreateTube(DiContainer subContainer, TubeCreateParam createParam) {
        TubeController controller =
            subContainer.InstantiatePrefabForComponent<TubeController>(createParam.Prefab,
                GameObject.Find("Tube").transform);
        return controller;
    }


    private DrowLineComponent CreateLine(DiContainer subContainer, List<Vector3> points, List<Company> manufactures) {
        DrowLineComponent component =
            subContainer.InstantiatePrefabForComponent<DrowLineComponent>(_prefabs.LinePrefab,
                new object[] {points, manufactures});
        component.gameObject.transform.parent = GameObject.Find("Lines").transform;
        return component;
    }

    void InstallSignals() {
        // Every scene that uses signals needs to install the built-in installer SignalBusInstaller
        // Or alternatively it can be installed at the project context level (see docs for details)
        SignalBusInstaller.Install(Container);

        // Signals can be useful for game-wide events that could have many interested parties
        Container.DeclareSignal<AddProductSignal>();
        Container.DeclareSignal<TaskCompleteSignal>();
        Container.DeclareSignal<ChangeLevelSignal>();
    }
}
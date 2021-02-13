using System;
using DefaultNamespace;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

[CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public PrefabSettings Prefabs;
    public LineMaterialsSettings LineMaterials;
    public TubeMaterialsSettings TubeMaterials;
    // public PeopleMaterialsSettings PeopleMaterials;
    // public PeopleSettings People;
    public ProductSettings Product;
    public TrainController.Settings TrainSetting;
    public MoneyService.PriceSettings PriceSettings;
    public TaskSetting TaskSettings;
    public ManufactureSettings ManufactureSetting;
    public TubeButtonSettings TubeButton;
    public GameSetting Setting;

    // [Serializable]
    // public class PeopleSettings
    // {
        // public PeopleManager.Settings Spawner;
    // }

    [Serializable]
    public class ManufactureSettings {
        public int produceSpeedSeconds;
    }

    [Serializable]
    public class PrefabSettings
    {
        public GameObject LinePrefab;
        public GameObject TrainPrefab;
        public GameObject StationPrefab;
        public GameObject ManufacturePrefab;
        
        public GameObject Tube1Prefab;
        public GameObject Tube2Prefab;
        public GameObject Tube3Prefab;
        public GameObject TubeAngelPrefab;
        public GameObject TubeTrianglePrefab;
        public GameObject TubeQuatroPrefab;
        
        
        public GameObject WaterPrefab;
        public GameObject HomePrefab;
        public GameObject StonePrefab;
    }
    
    [Serializable]
    public class LineMaterialsSettings
    {
        public Material NotUsed;
        public Material Red;
        public Material Blue;
    }     
    
    [Serializable]
    public class TubeMaterialsSettings
    {
        public Material Empty;
        public Material Move;
        public Material Red;
        public Material Water;
    }  
    
    [Serializable]
    public class ProductSettings
    {
        public Material EmptyMaterial;
        public Material YellowMaterial;
        public Material BrownMaterial;
        public Color YellowColor;
        public Color BrownColor;
        public Texture Yellow;
        public Texture Brown;
    }   
    
    [Serializable]
    public class TubeButtonSettings
    {
        public Texture Line1;
        public Texture Line2;
        public Texture Line3;
        public Texture Line4;
        public Texture Triangle;
        public Texture Quatro;
        public Texture Angle;
    }
    
    // [Serializable]
    // public class PeopleMaterialsSettings
    // {
    //     public Material Simple;
    //     public Material Wait;
    //     public Material WaitAgry;
    //     public Material _area0;
    //     public Material _area1;
    //     public Material _area2;
    //     public Material _area3;
    // }
    
    [Serializable]
    public class TaskSetting {
        public int brownTaskCount;
        public int brownResourceCountFrom;
        public int brownResourceCountTo;
        public int brownResourcePriceFrom;
        public int brownResourcePriceTo;
        
        public int yellowTaskCount;
        public int yellowResourceCountFrom;
        public int yellowResourceCountTo;
        public int yellowResourcePriceFrom;
        public int yellowResourcePriceTo;
    }    
    
    [Serializable]
    public class GameSetting {
        public bool isDebug;
    }
    
    public override void InstallBindings()
    {
        Container.BindInstance(Prefabs);
        Container.BindInstance(LineMaterials);
        // Container.BindInstance(PeopleMaterials);
        // Container.BindInstance(People.Spawner);
        Container.BindInstance(TrainSetting);
        Container.BindInstance(PriceSettings);
        Container.BindInstance(Product);
        Container.BindInstance(TaskSettings);
        Container.BindInstance(ManufactureSetting);
        Container.BindInstance(TubeMaterials);
        Container.BindInstance(TubeButton);
        Container.BindInstance(Setting);
    }
}

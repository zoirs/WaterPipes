using System;
using System.Collections.Generic;
using Line;
using Main;
using Map;
using Train;
using UnityEngine;
using Zenject;

public class GuiHandler : MonoBehaviour, IDisposable, IInitializable {
    [SerializeField] GUIStyle _timeStyle;

    [Inject] private TrainManager _trainManager;
    [Inject] private TubeManager _tubeManager;
    [Inject] private StoneManager _stoneManager;
    [Inject] private HomeManager _homeManager;

    // [Inject] private CityManager _cityManager;
    // [Inject] private LineManager _lineManager;
    [Inject] private MoneyService _moneyService;

    // [Inject] private MapService _mapService;
    // [Inject] private PeopleManager _peopleManager;
    [Inject] private MoneyService.PriceSettings PriceSettings;
    [Inject] private GameSettingsInstaller.TubeButtonSettings TubeButtonSettings;
    [Inject] private GameController _gameController;
    [Inject] private WaterManager _waterManager;
    [Inject] private LevelManager _levelManager;
    [Inject] private InventoryManager _inventoryManager;

    [Inject] private SignalBus _signalBus;
    [Inject] private GameSettingsInstaller.ProductSettings _product;
    [Inject] GameSettingsInstaller.PrefabSettings _prefabs;
    [Inject] private GameSettingsInstaller.GameSetting setting;


    Rect windowRect = new Rect(300, 200, 180, 200);
    private bool show;
    private LineController _selectedLine;
    private List<ManufacturerController> manufactures = new List<ManufacturerController>();
    private List<CityController> tasks = new List<CityController>();

    void OnGUI() {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        {
            PlayingGui();
        }
        GUILayout.EndArea();
    }

    void PlayingGui() {
        if (_selectedLine != null) {
            GUILayout.Window(100,
                new Rect(_selectedLine.getPanelPosition().x, Screen.height - _selectedLine.getPanelPosition().y, 180,
                    50), LineWindow, "Линия");
        }


        for (int i = 0; i < manufactures.Count; i++) {
            ManufacturerController manufacture = manufactures[i];
            GUILayout.Window(i,
                new Rect(manufacture.getPanelPosition().x, Screen.height - manufacture.getPanelPosition().y - 40,
                    15 * manufacture.ProductsCount(), 20),
                (id) => ManufactureWindow(id, manufacture), "");
        }

        for (int i = 0; i < tasks.Count; i++) {
            CityController cityController = tasks[i];
            GUILayout.Window(50 + i,
                new Rect(cityController.getPanelPosition().x, Screen.height - cityController.getPanelPosition().y - 40,
                    100, 20),
                (id) => TaskWindow(id, cityController), "Надо:");
        }

        GUILayout.BeginVertical();
        {
            // GUILayout.Space(30);
            GUILayout.BeginHorizontal();
            {
                GUILayout.Space(30);

                if (show) {
                    GUILayout.Window(0, windowRect, DoMyWindow, "Открыт новый район");
                }

                GUILayout.BeginVertical();

                if (_gameController.State == GameStates.Playing) {
                    int countLine1 = _inventoryManager. InventoryCount(TubeType.LINE1);
                    GUI.enabled = countLine1 > 0;
                    if (GUILayout.Button(new GUIContent(" * " + countLine1, TubeButtonSettings.Line1),
                        GUILayout.Width(100),
                        GUILayout.Height(30))) {
                        _inventoryManager.GetOutOfInventory(TubeType.LINE1);
                    }


                    int countLine2 = _inventoryManager.InventoryCount(TubeType.LINE2);
                    GUI.enabled = countLine2 > 0;
                    if (GUILayout.Button(new GUIContent(" * " + countLine2, TubeButtonSettings.Line2), GUILayout.Width(100),
                        GUILayout.Height(30))) {
                        _inventoryManager.GetOutOfInventory(TubeType.LINE2);
                    }


                    int countLine3 = _inventoryManager. InventoryCount(TubeType.LINE3);
                    GUI.enabled = countLine3 > 0;
                    if (GUILayout.Button(new GUIContent(" * " + countLine3, TubeButtonSettings.Line3), GUILayout.Width(100),
                        GUILayout.Height(30))) {
                        _inventoryManager.GetOutOfInventory(TubeType.LINE3);
                    }

                    int countLine4 = _inventoryManager. InventoryCount(TubeType.LINE3);
                    GUI.enabled = countLine4 > 0;
                    if (GUILayout.Button(new GUIContent(" * " + countLine4, TubeButtonSettings.Line4), GUILayout.Width(100),
                        GUILayout.Height(30))) {
                        _inventoryManager.GetOutOfInventory(TubeType.LINE4);
                    }

                    int countAngle = _inventoryManager. InventoryCount(TubeType.ANGEL);
                    GUI.enabled = countAngle > 0;
                    if (GUILayout.Button(new GUIContent(" * " + countAngle, TubeButtonSettings.Angle), GUILayout.Width(100),
                        GUILayout.Height(30))) {
                        _inventoryManager.GetOutOfInventory(TubeType.ANGEL);
                    }

                    int countTriangle = _inventoryManager. InventoryCount(TubeType.TRIANGLE);
                    GUI.enabled = countTriangle > 0;
                    if (GUILayout.Button(new GUIContent(" * " + countTriangle, TubeButtonSettings.Triangle), GUILayout.Width(100),
                        GUILayout.Height(30))) {
                        _inventoryManager.GetOutOfInventory(TubeType.TRIANGLE);
                    }

                    int countQuatro = _inventoryManager. InventoryCount(TubeType.QUATRO);
                    GUI.enabled = countQuatro > 0;
                    if (GUILayout.Button(new GUIContent(" * " + countQuatro, TubeButtonSettings.Quatro), GUILayout.Width(100),
                        GUILayout.Height(30))) {
                        _inventoryManager.GetOutOfInventory(TubeType.QUATRO);
                    }

                    GUI.enabled = true;

                    if (GUILayout.Button("Проверить")) {
                        _waterManager.Check();
                    }

                    // if (GUILayout.Button("Колодец")) {
                    // _waterManager.Create();
                    // }
                    if (setting.isDebug) {
                        if (GUILayout.Button("Дом 1*1")) {
                            _homeManager.Create(HomeType.ONE_ONE);
                        }

                        if (GUILayout.Button("Дом 2*2")) {
                            _homeManager.Create(HomeType.TWO_TWO);
                        }
                        
                        if (GUILayout.Button("Колодец 1*1")) {
                            _waterManager.Create(WellType.ONE_ONE);
                        }
                        if (GUILayout.Button("Колодец 2*2")) {
                            _waterManager.Create(WellType.TWO_TWO);
                        }

                        if (GUILayout.Button("Камень")) {
                            _stoneManager.Create();
                        }

                        if (GUILayout.Button("Сохранить уровень")) {
                            _levelManager.SaveLevel();
                        }
                    }
                }

                for (var i = 0; i < _trainManager.Trains.Count; i++) {
                    TrainController train = _trainManager.Trains[i];
                    GUILayout.Label("Поезд " + (i + 1), GUILayout.Height(20));
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Скорость: " + train.Speed(), GUILayout.Height(20));
                    if (GUILayout.Button("Увеличить $" + PriceSettings.trainSpeed)) {
                        _moneyService.Minus(PriceSettings.trainSpeed);
                        train.UpgradeSpeed();
                    }

                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Вагонов: 1", GUILayout.Height(20));
                    GUI.enabled = false;
                    if (GUILayout.Button("Увеличить $" + PriceSettings.trainWagon)) {
                        _moneyService.Minus(PriceSettings.trainWagon);
                        //
                    }

                    GUI.enabled = true;

                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Label(train.StateText());
                    if (train.State == TrainState.Depo) {
                        if (GUILayout.Button("На линию")) {
                            train.GoToLine();
                        }
                    }
                    else {
                        if (GUILayout.Button("В депо")) {
                            train.GoToDepo();
                        }
                    }


                    GUILayout.EndHorizontal();
                }

                GUILayout.EndVertical();

                GUILayout.FlexibleSpace();

                GUILayout.BeginVertical();
                if (_gameController.State == GameStates.WaitingToStart) {
                    // GUILayout.Label("Начать игру");
                    if (setting.isDebug) {
                        if (GUILayout.Button("Новый")) {
                            _gameController.CreateEmptyLevel();
                        }
                    }
                    foreach (string level in _gameController.Levels) {
                        

                        if (GUILayout.Button(level)) {
                            _gameController.StartGame(level);
                        }
                    }
                }

                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();
    }

    private void ManufactureWindow(int id, ManufacturerController manufacturerController) {
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        for (int i = 0; i < manufacturerController.ProductsCount(); i++) {
            GUILayout.Label(manufacturerController.ProductType.GetTexture(_product), GUILayout.MaxWidth(10));
        }

        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }

    private void TaskWindow(int id, CityController cityController) {
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();

        int count = cityController.CurentTask.NeedCount - cityController.CurentTask.Progress;
        GUILayout.Label(count.ToString());
        GUILayout.Label(cityController.CurentTask.ProductType.GetTexture(_product));
        GUILayout.Label(" = " + (cityController.CurentTask.Coast * count) + "$");

        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }


    // Make the contents of the window
    void DoMyWindow(int windowID) {
        // This button will size to fit the window
    }

    void LineWindow(int windowID) {
        if (GUILayout.Button("Red")) {
            _selectedLine.LineType = LineType.RED;
            _selectedLine = null;
        }

        if (GUILayout.Button("Blue")) {
            _selectedLine.LineType = LineType.BLUE;
            _selectedLine = null;
        }

        if (GUILayout.Button("Not used")) {
            _selectedLine.LineType = LineType.NOT_USED;
            _selectedLine = null;
        }
    }

    public void Dispose() {
        _signalBus.Unsubscribe<ChangeLevelSignal>(OnDistrictOpen);
    }

    public void Initialize() {
        _signalBus.Subscribe<ChangeLevelSignal>(OnDistrictOpen);
    }

    private void OnDistrictOpen() {
        show = true;
    }

    public void ShowPanelFor(LineController selectedLine) {
        if (_selectedLine == selectedLine) {
            _selectedLine = null;
            return;
        }

        _selectedLine = selectedLine;
    }

    // public void ShowPanelFor(ManufacturerController selectedManufacture) {
    //     _selectedLine = null;
    //     if (manufactures == selectedManufacture) {
    //         manufactures = null;
    //         return;
    //     }
    //
    //     manufactures = selectedManufacture;
    // }

    public void HidePanel() {
        _selectedLine = null;
    }

    public void AddManufacturePanel(ManufacturerController manufacturerController) {
        manufactures.Add(manufacturerController);
        Debug.Log("AddManufacturePanel" + manufactures.Count);
    }

    public void AddCityTaskPanel(CityController cityController) {
        tasks.Add(cityController);
    }

    public void RemoveCityTaskPanel(CityController cityController) {
        tasks.Remove(cityController);
    }
}
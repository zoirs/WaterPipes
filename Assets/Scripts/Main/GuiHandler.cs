using System;
using Main;
using UnityEngine;
using Zenject;

public class GuiHandler : MonoBehaviour {
    [SerializeField] GUIStyle _timeStyle;

    [Inject] private TubeManager _tubeManager;
    [Inject] private StoneManager _stoneManager;
    [Inject] private HomeManager _homeManager;

    [Inject] private GameSettingsInstaller.TubeButtonSettings TubeButtonSettings;
    [Inject] private GameController _gameController;
    [Inject] private WaterManager _waterManager;
    [Inject] private CheckerService _checkerService;
    [Inject] private LevelManager _levelManager;
    [Inject] private InventoryManager _inventoryManager;
    [Inject] private GameSettingsInstaller.PrefabSettings prefabs;
    
    [Inject] private GameSettingsInstaller.GameSetting setting;

    Rect windowRect = new Rect(300, 200, 180, 200);

    void OnGUI() {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        {
            PlayingGui();
        }
        GUILayout.EndArea();
    }

    void PlayingGui() {
        switch (_gameController.State) {
            case GameStates.WaitingToStart:
                WaitPlay();
                break;
            case GameStates.Playing:
                LevelPlay();
                break;
            case GameStates.LevelComplete:
                LevelPlay();
                levelComplete();
                break;
            case GameStates.GameOver:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void levelComplete() {
        GUILayout.Window(100,
            new Rect(100, 100, 280,
                150), windowID => {
                GUILayout.BeginHorizontal();
                {

                    if (GUILayout.Button("Остаться")) {
                        _gameController.State = GameStates.Playing;
                    }

                    if (!setting.isDebug) {
                        if (GUILayout.Button("Следующий")) {
                            _gameController.LoadNext();
                        }
                    }
                }
                GUILayout.EndHorizontal();
                

            }, "Уровень пройден");
    }

    private void WaitPlay() {
        GUILayout.BeginHorizontal();
        {
            GUILayout.BeginVertical();
            if (_gameController.State == GameStates.WaitingToStart) {
                if (setting.isDebug) {
                    if (GUILayout.Button("Новый", GUILayout.Width(100))) {
                        _gameController.CreateEmptyLevel();
                    }
                }

                for (int index = 0; index < _gameController.Levels.Count; index++) {
                    if (GUILayout.Button(index.ToString(), GUILayout.Width(100))) {
                        _gameController.StartGame(index);
                    }
                }
            }

            GUILayout.EndVertical();
        }
        GUILayout.EndHorizontal();
    }

    private void LevelPlay() {
        GUILayout.BeginHorizontal();
        {
            GUILayout.Space(30);

            GUILayout.BeginVertical();

            if (_gameController.State == GameStates.Playing) {
                int countLine1 = _inventoryManager.InventoryCount(TubeType.LINE1);
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


                int countLine3 = _inventoryManager.InventoryCount(TubeType.LINE3);
                GUI.enabled = countLine3 > 0;
                if (GUILayout.Button(new GUIContent(" * " + countLine3, TubeButtonSettings.Line3), GUILayout.Width(100),
                    GUILayout.Height(30))) {
                    _inventoryManager.GetOutOfInventory(TubeType.LINE3);
                }

                int countLine4 = _inventoryManager.InventoryCount(TubeType.LINE4);
                GUI.enabled = countLine4 > 0;
                if (GUILayout.Button(new GUIContent(" * " + countLine4, TubeButtonSettings.Line4), GUILayout.Width(100),
                    GUILayout.Height(30))) {
                    _inventoryManager.GetOutOfInventory(TubeType.LINE4);
                }

                int countLine5 = _inventoryManager.InventoryCount(TubeType.LINE5);
                GUI.enabled = countLine5 > 0;
                if (GUILayout.Button(new GUIContent(" * " + countLine5, TubeButtonSettings.Line5), GUILayout.Width(100),
                    GUILayout.Height(30))) {
                    _inventoryManager.GetOutOfInventory(TubeType.LINE5);
                }

                int countAngle = _inventoryManager.InventoryCount(TubeType.ANGEL);
                GUI.enabled = countAngle > 0;
                if (GUILayout.Button(new GUIContent(" * " + countAngle, TubeButtonSettings.Angle), GUILayout.Width(100),
                    GUILayout.Height(30))) {
                    _inventoryManager.GetOutOfInventory(TubeType.ANGEL);
                }

                int countTriangle = _inventoryManager.InventoryCount(TubeType.TRIANGLE);
                GUI.enabled = countTriangle > 0;
                if (GUILayout.Button(new GUIContent(" * " + countTriangle, TubeButtonSettings.Triangle),
                    GUILayout.Width(100),
                    GUILayout.Height(30))) {
                    _inventoryManager.GetOutOfInventory(TubeType.TRIANGLE);
                }

                int countQuatro = _inventoryManager.InventoryCount(TubeType.QUATRO);
                GUI.enabled = countQuatro > 0;
                if (GUILayout.Button(new GUIContent(" * " + countQuatro, TubeButtonSettings.Quatro),
                    GUILayout.Width(100),
                    GUILayout.Height(30))) {
                    _inventoryManager.GetOutOfInventory(TubeType.QUATRO);
                }

                GUI.enabled = true;

                if (GUILayout.Button("Проверить")) {
                    _checkerService.Check();
                }

                if (setting.isDebug) {
                    GUILayout.BeginHorizontal();

                    if (GUILayout.Button("Дом 1*1")) {
                        _homeManager.CreateDebug(HomeType.ONE_ONE);
                    }

                    if (GUILayout.Button("Дом 2*2")) {
                        _homeManager.CreateDebug(HomeType.TWO_TWO);
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();

                    if (GUILayout.Button("Колодец 1*1")) {
                        _waterManager.CreateDebug(WellType.ONE_ONE);
                    }

                    if (GUILayout.Button("Колодец 2*2")) {
                        _waterManager.CreateDebug(WellType.TWO_TWO);
                    }
                    GUILayout.EndHorizontal();

                    if (GUILayout.Button("Камень")) {
                        _stoneManager.CreateDebug();
                    }

                    if (GUILayout.Button("Сохранить уровень")) {
                        _levelManager.SaveLevel();
                    }
                }
            }


            GUILayout.EndVertical();

            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.Label("Уровень " + (_gameController.CurrentLevel + 1) + " из " + _gameController.Levels.Count);
            GUILayout.EndVertical();

        }
        GUILayout.EndHorizontal();
    }
}
using System;
using System.Collections.Generic;
using DefaultNamespace.Tube;
using UnityEngine;
using Zenject;

public class TubeController : MonoBehaviour {
    [SerializeField] private TubeType _tubeType;
    // [SerializeField]
    // private TubeType _tubeType;
    // [SerializeField]
    // private TubeType _tubeType;

    [Inject] private GameSettingsInstaller.TubeMaterialsSettings _materials;
    [Inject] private TubeMapService _tubeMapService;
    [Inject] private TubeManager _tubeManager;

    private TubeState _state = TubeState.SettedCorrect;

    private MeshRenderer[] _renderer;
    private TubeEndController[] _ends;

    private bool _hasWater;

    private int _rotate = 0;


    // public bool _isChecked { get; private set; }

    Vector3 tapPosition = Vector3.zero;
    Vector3 finishPosition = Vector3.zero;
    Vector3 startPosition = Vector3.zero;
    private FingerMoveController _fingerMoveController;
    private PointController[] _points;


    private void Start() {
        _renderer = GetComponentsInChildren<MeshRenderer>();
        _ends = GetComponentsInChildren<TubeEndController>();
        _points = GetComponentsInChildren<PointController>();
        // transform.position = new Vector3(6, 6, 0);
        // _fingerMoveController = GetComponent<FingerMoveController>();

        if (_tubeMapService.Check(_points)) {
            State = TubeState.SettedCorrect;
            _tubeMapService.Busy(_points);
        } else {
            State = TubeState.SettedWrong;
        }
    }

    public List<Direction> GetWaterPosition() {
        return _tubeType.GetWaterDirection(_rotate);
    }

    public Dictionary<Direction, Vector3Int> GetEndPositions() {
        Dictionary<Direction, Vector3Int> endPositions = new Dictionary<Direction, Vector3Int>();
        foreach (TubeEndController tubeEndController in _ends) {
            endPositions.Add(tubeEndController.GetDirection(_rotate), tubeEndController.GetVector());
        }

        return endPositions;
    }

    private void Update() {
        switch (_state) {
            case TubeState.Inventory:
                break;
            case TubeState.Wait:
            case TubeState.SettedCorrect:
            case TubeState.SettedWrong:

                if (Input.GetMouseButtonDown(1) && ClickOnCurrent()) {
                    _tubeMapService.Free(_points);
                    _tubeManager.Remove(this);
                }

                if (Input.GetMouseButtonDown(0) && ClickOnCurrent()) {
                    tapPosition = Input.mousePosition;
                    startPosition = transform.position;
                    if (_state == TubeState.SettedCorrect) {
                        _tubeMapService.Free(_points);
                    }

                    State = TubeState.Move;
                }

                break;
            case TubeState.Move:
                if (Input.GetMouseButton(0)) {
                    finishPosition = Input.mousePosition;
                    LeftMouseDrag();
                }

                if (Input.GetMouseButtonUp(0)) {
                    Debug.Log(startPosition + " " + transform.position);
                    if (Vector2.Distance(startPosition, transform.position) < 0.2f) {
                        Debug.Log("rotate");
                        RotateTube();
                    }

                    if (_tubeMapService.Check(_points)) {
                        State = TubeState.SettedCorrect;
                        _tubeMapService.Busy(_points);
                    }
                    else {
                        State = TubeState.SettedWrong;
                    }
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public TubeState State {
        get => _state;
        set {
            _state = value;
            if (_state == TubeState.Move) {
                SetMaterial(_materials.Move);
            }
            else if (_state == TubeState.SettedWrong) {
                SetMaterial(_materials.Red);
            }
            else {
                SetMaterial(_materials.Empty);
            }
        }
    }

    public void RotateTube() {
        if (_state == TubeState.Move) {
            transform.Rotate(Vector3.forward, 90);
            _rotate++;
            if (_rotate >= 4) {
                _rotate = _rotate % 4;
            }

            TubeEndController[] parts = GetComponentsInChildren<TubeEndController>();
            foreach (TubeEndController part in parts) {
                Debug.Log("pos " + part.GetVector() + " - " + part.GetDirection(_rotate));
            }
        }
    }

    public class Factory : PlaceholderFactory<TubeCreateParam, TubeController> { }

    public void MarkWater() {
        SetMaterial(_materials.Water);
        _hasWater = true;
    }

    public void Clear() {
        SetMaterial(_materials.Empty);
        _hasWater = false;
    }

    private void SetMaterial(Material meshRendererMaterial) {
        foreach (MeshRenderer meshRenderer in _renderer) {
            meshRenderer.material = meshRendererMaterial;
        }
    }

    void LeftMouseDrag() {
        // вектор направлениея движения в мире в плоскости экрана
        Vector3 direction = Camera.main.ScreenToWorldPoint(finishPosition) -
                            Camera.main.ScreenToWorldPoint(tapPosition);
        if (direction == Vector3.zero) {
            return;
        }

        Vector3 position = startPosition + direction;
        if (Vector2.Distance(position, transform.position) > 1f) {
            transform.position = new Vector3Int((int) position.x, (int) position.y, 0);
            if (_tubeMapService.Check(_points)) {
                SetMaterial(_materials.Move);
            }
            else {
                SetMaterial(_materials.Red);
            }
        }
    }

    private bool ClickOnCurrent() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            Debug.Log(hit.transform.name);
            TubeController tubeController = hit.transform.GetComponent<TubeController>();
            return tubeController == this;
        }

        return false;
    }

    public TubeType TubeType => _tubeType;

    public int Rotate {
        get => _rotate;
        set {
            //только для инициализации при загрузке уровня
            _rotate = value;
            transform.Rotate(Vector3.forward, _rotate * 90);
        }
    }

    public Vector3Int GetVector() {
        Vector3 p = transform.position;
        return new Vector3Int((int) Math.Round(p.x, 0), (int) Math.Round(p.y, 0), (int) Math.Round(p.z, 0));
    }
}
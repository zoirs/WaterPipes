using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HomeController : MonoBehaviour {

    [SerializeField]
    private HomeType homeType;
    
    private MeshRenderer[] _renderer;

    [Inject] private GameSettingsInstaller.TubeMaterialsSettings _materials;
    [Inject] private TubeMapService _tubeMapService;

    private bool _hasWater;
    private TubeEndController[] _ends;

    // временно, для отладки
    // Vector3 tapPosition = Vector3.zero;
    // Vector3 finishPosition = Vector3.zero;
    // Vector3 startPosition = Vector3.zero;
    // private bool inMove = false;

    private void Start() {
        _renderer = GetComponentsInChildren<MeshRenderer>();
        _ends = GetComponentsInChildren<TubeEndController>();
    }
    
    public List<TubeEndController> GetFreeConnecter() {
        List<TubeEndController> result = new List<TubeEndController>();
        foreach (TubeEndController tubeEndController in _ends) {
            if (tubeEndController.IsConnected()) {
                continue;
            }
            result.Add(tubeEndController);
        }
        return result;
    }


    private void Update() {
        
    }
    
    
    // временно, для отладки
    //     if (Input.GetMouseButtonDown(0) && ClickOnCurrent()) {
    //         inMove = true;
    //         tapPosition = Input.mousePosition;
    //         startPosition = transform.position;
    //     }
    //
    //     if (Input.GetMouseButton(0) && inMove) {
    //         finishPosition = Input.mousePosition;
    //         LeftMouseDrag();
    //     }
    //
    //     if (Input.GetMouseButtonUp(0)) {
    //         inMove = false;
    //     }
    //
    // }

    public class Factory : PlaceholderFactory<HomeCreateParam, HomeController> { }

    public void MarkWater() {
        SetMaterial(_materials.Water);
        _hasWater = true;
    }

    public void Clear() {
        SetMaterial(_materials.Empty);
        _hasWater = false;
        foreach (TubeEndController tubeEndController in _ends) {
            tubeEndController.UnConnect();
        }
    }

    private void SetMaterial(Material meshRendererMaterial) {
        foreach (MeshRenderer meshRenderer in _renderer) {
            meshRenderer.material = meshRendererMaterial;
        }
    }
    
    public Vector3Int GetVector() {
        Vector3 p = transform.position;
        return new Vector3Int((int) Math.Round(p.x, 0),(int) Math.Round(p.y, 0),(int) Math.Round( p.z, 0));
    }

    public bool HasWater => _hasWater;

    // временно, для отладки
    // void LeftMouseDrag() {
    //     // вектор направлениея движения в мире в плоскости экрана
    //     Vector3 direction = Camera.main.ScreenToWorldPoint(finishPosition) -
    //                         Camera.main.ScreenToWorldPoint(tapPosition);
    //     if (direction == Vector3.zero) {
    //         return;
    //     }
    //     Vector3 position = startPosition + direction;
    //     if (Vector2.Distance(position, transform.position) > 1f) {
    //         transform.position = new Vector3Int((int) position.x, (int) position.y, 0);
    //     }
    // }
    //
    // // временно, для отладки
    // private bool ClickOnCurrent() {
    //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //     RaycastHit hit;
    //     if (Physics.Raycast(ray, out hit)) {
    //         Debug.Log(hit.transform.name);
    //         HomeController tubeController = hit.transform.GetComponent<HomeController>();
    //         return tubeController == this;
    //     }
    //     return false;
    // }
    public HomeType HomeType => homeType;
}
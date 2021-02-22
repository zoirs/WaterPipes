using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PortalController : MonoBehaviour {
    private ConnectorController[] _ends;
    private bool _hasWater;
    private MeshRenderer[] _renderer;
    [Inject] private GameSettingsInstaller.TubeMaterialsSettings _materials;


    private void Start() {
        _ends = GetComponentsInChildren<ConnectorController>();
        _renderer = GetComponentsInChildren<MeshRenderer>();

    }
    
    public void MarkWater() {
        SetMaterial(_materials.Water);
        _hasWater = true;
    }
    
    public bool HasWater => _hasWater;

    public Vector3Int GetVector() {
        Vector3 p = transform.position;
        return new Vector3Int((int) Math.Round(p.x, 0),(int) Math.Round(p.y, 0),(int) Math.Round( p.z, 0));
    }
    
    public List<ConnectorController> GetFreeConnecter() {
        List<ConnectorController> result = new List<ConnectorController>();
        foreach (ConnectorController tubeEndController in _ends) {
            if (tubeEndController.IsConnected()) {
                continue;
            }
            result.Add(tubeEndController);
        }
        return result;
    }
    
    private void SetMaterial(Material meshRendererMaterial) {
        foreach (MeshRenderer meshRenderer in _renderer) {
            meshRenderer.material = meshRendererMaterial;
        }
    }
    
        public class Factory : PlaceholderFactory<PortalCreateParam, PortalController> { }

        public void Clear() {
            SetMaterial(_materials.Empty);
            _hasWater = false;
            foreach (ConnectorController tubeEndController in _ends) {
                tubeEndController.UnConnect();
            }
        }
}
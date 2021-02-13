﻿using System;
using UnityEngine;
using Zenject;

public class StoneController : MonoBehaviour {
 
        
    // временно, для отладки
    // Vector3 tapPosition = Vector3.zero;
    // Vector3 finishPosition = Vector3.zero;
    // Vector3 startPosition = Vector3.zero;
    // private bool inMove = false;
    

    // временно, для отладки
    // private void Update() {
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
        
    public Vector3Int GetVector() {
        Vector3 p = transform.position;
        return new Vector3Int((int) Math.Round(p.x, 0),(int) Math.Round(p.y, 0),(int) Math.Round( p.z, 0));
    }


    public class Factory : PlaceholderFactory<StoneController> { }
    
    
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
    //         StoneController tubeController = hit.transform.GetComponent<StoneController>();
    //         return tubeController == this;
    //     }
    //     return false;
    // }
}
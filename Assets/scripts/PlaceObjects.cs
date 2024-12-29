using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
public class PlaceObjects : MonoBehaviour
{
    public LayerMask layer;
    public float rotatespeed = 60f;
    private void Start(){
        PositionObjects();
    }
    
    private void Update()
    {
        PositionObjects();
        if(Input.GetMouseButtonDown(0))
        {
            gameObject.GetComponent<AutoGEnerationCars>().enabled = true;
            Destroy(gameObject.GetComponent<PlaceObjects>());
        }
        if(Input.GetKey(KeyCode.LeftShift)){
            transform.Rotate(Vector3.up * Time.deltaTime * rotatespeed);
        }
    }
    private void PositionObjects(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 1000f, layer)){
            transform.position = hit.point;

        }
        
    }
}

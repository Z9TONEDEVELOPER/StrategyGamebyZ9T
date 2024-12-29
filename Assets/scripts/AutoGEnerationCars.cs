using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AutoGEnerationCars : MonoBehaviour
{
    public GameObject car;
    public float time = 5f;
    [NonSerialized]
    public bool isenemy = false;
    private void Start(){
        StartCoroutine(SpawnCar());
    }
    IEnumerator SpawnCar(){
        for(int i =1;i<=5;i++){
            yield return new WaitForSeconds(time);
            Vector3 pos = new Vector3(
                transform.GetChild(0).position.x + UnityEngine.Random.Range(3f,10f),transform.GetChild(0).position.y,transform.GetChild(0).position.z + UnityEngine.Random.Range(3f,10f)
            );
            GameObject spawn = Instantiate(car,pos,Quaternion.identity);
            if(isenemy){
                spawn.tag = "Enemy";
            }
        }
        
    }
}

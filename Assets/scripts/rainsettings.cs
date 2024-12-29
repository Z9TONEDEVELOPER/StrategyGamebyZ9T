using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class rainsettings : MonoBehaviour
{
    private ParticleSystem _psrain;
    public Light dirlight;
    private bool _israin = false;
    private void Start(){
        _psrain = GetComponent<ParticleSystem>();
        StartCoroutine(Weather());

    }
    private void Update(){
        if(_israin && dirlight.intensity > 0.25f){
            LightIntensity(-1);
        }else if (!_israin && dirlight.intensity < 0.5f){
            LightIntensity(1);
        }

    }
    private void LightIntensity(int i){
        dirlight.intensity += 0.1f * Time.deltaTime * i;
    }
    IEnumerator Weather(){
        while(true){
            yield return new WaitForSeconds(UnityEngine.Random.Range(120f,250f));
            if(_israin == true){
                _psrain.Stop();
                _israin = false;
                
                
            }else{
                _psrain.Play();
                _israin = true;
                
                
            }
        }
    }
}

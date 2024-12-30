using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Unity.VisualScripting;
using System;
public class FireCar : MonoBehaviour
{
    [NonSerialized] public int _health = 100;
    public float radius = 70f;
    public GameObject Bullet;
    private Coroutine _coroutine = null;
    private void Update() {
        DetectCollision();
    }
    private void DetectCollision(){
        Collider[] hitscolider = Physics.OverlapSphere(transform.position, radius);
        if(hitscolider.Length == 0 && _coroutine != null){
            StopCoroutine(_coroutine);
            _coroutine = null;
            if(gameObject.CompareTag("Enemy")){
                GetComponent<NavMeshAgent>().SetDestination(gameObject.transform.position);
            }
        }
        foreach(var el in hitscolider){
            if((gameObject.CompareTag("Player") && el.gameObject.CompareTag("Enemy")) || (gameObject.CompareTag("Enemy") && el.gameObject.CompareTag("Player"))){
                if(gameObject.CompareTag("Enemy")){
                    GetComponent<NavMeshAgent>().SetDestination(el.transform.position);
                }
                if(_coroutine == null){
                        _coroutine = StartCoroutine(StartAttack(el));
                    }
            }
        }
    }
    IEnumerator StartAttack(Collider enemytarget){
        GameObject obj = Instantiate(Bullet, transform.GetChild(1).position, Quaternion.identity);
        obj.GetComponent<BulletControler>().position = enemytarget.transform.position;
        yield return new WaitForSeconds(1f);
        StopCoroutine(_coroutine);
        _coroutine = null;
    }
}

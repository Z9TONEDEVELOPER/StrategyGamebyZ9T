using System;
using UnityEngine;

public class BulletControler : MonoBehaviour
{
    [NonSerialized] public Vector3 position;
    public float speed = 30f;
    public int damage = 20;
    private void Update() {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, position, step);
        if(transform.position == position){
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Enemy" || other.tag == "Player"){
            FireCar attack = other.GetComponent<FireCar>();
            attack._health -= damage;
            Transform healthbar = other.transform.GetChild(0).transform;
            healthbar.localScale = new Vector3(healthbar.localScale.x - 0.3f, healthbar.localScale.y, healthbar.localScale.z);
            if(attack._health <= 0){
                Destroy(other.gameObject);
            }
        }
    }

}

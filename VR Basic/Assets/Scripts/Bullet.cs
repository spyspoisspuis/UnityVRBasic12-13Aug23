using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifetime=10f; 
    [SerializeField] private GameObject hitFx;
    private Weapon weapon;
    private Vector3 contactPoint;
    public void Init(Weapon weapon,Vector3 contactPoint) {
        this.weapon = weapon;
        this.contactPoint = contactPoint;
    }
    private void Start() {
        Destroy(gameObject,lifetime);
    }

    private void Update() {
        Vector3 direction = (contactPoint - transform.position).normalized;
        Vector3 movement = Time.deltaTime * weapon.GetShootingForce() * direction;
        transform.Translate(movement,Space.World);
    }

    private void OnCollisionEnter(Collision other) {

        if (other.gameObject.CompareTag("Enemy")) {
            Debug.Log("HitEnemy");
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(weapon.GetDamage());
            GameObject fx = Instantiate(hitFx,contactPoint,Quaternion.identity);
            Destroy(fx,2);
        }

    }



}

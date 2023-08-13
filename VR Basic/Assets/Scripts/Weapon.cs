using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected float shootingForce;
    [SerializeField] protected GameObject bulletSpawnPrefabs;
    [SerializeField] protected Transform bulletSpawnPos;
    [SerializeField] private GameObject shootEffect;

    public void Fire() {
        if (Physics.Raycast(bulletSpawnPos.position, bulletSpawnPos.forward, out RaycastHit hit))
        {
            GameObject bullet = Instantiate(bulletSpawnPrefabs,bulletSpawnPos.position,bulletSpawnPos.rotation);
            
            Vector3 contactPoint = hit.point;
            bullet.GetComponent<Bullet>().Init(this,contactPoint);

            GameObject shootEffectLocal = Instantiate(shootEffect,bulletSpawnPos.position,bulletSpawnPos.rotation);
            Destroy(shootEffectLocal,2);
        }
       
    }

    public float GetShootingForce() {return shootingForce;}

    public float GetDamage() { return damage ;}
}

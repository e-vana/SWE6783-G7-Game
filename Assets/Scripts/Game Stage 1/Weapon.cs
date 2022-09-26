using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;
    public float fireForce;

    public void Fire(float aimAngle)
    {
        Vector3 rotationvector = new Vector3(0, 0, aimAngle);
        Quaternion rotation = Quaternion.Euler(rotationvector);
        firePoint.rotation = rotation;
        GameObject projectile = Instantiate(bullet, firePoint.position, firePoint.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }

}

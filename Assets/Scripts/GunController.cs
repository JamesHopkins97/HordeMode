using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private bool GunShooting = false;

    [SerializeField]
    GameObject bulletObject;

    GameObject player;

    Rigidbody bullet;

    Rigidbody NewBullet;
    // Start is called before the first frame update
    void Start()
    {
        bullet = bulletObject.GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (GunShooting)
        {
            NewBullet = Instantiate(bullet, transform.position, transform.rotation);
            NewBullet.AddForce(transform.TransformDirection(Vector3.forward * 1000));

            GunShooting = false;
        }

    }

    public bool GetFiring()
    {
        return GunShooting;
    }

    public void SetFiring(bool firing)
    {
        GunShooting = firing;
    }
}

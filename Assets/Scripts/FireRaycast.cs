using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRaycast : MonoBehaviour
{
    public GameObject camera;
    public GameObject hitEffectPrefab;

    public ParticleSystem muzzleFlash;
    public AudioSource gunShotSound;
    public GameManager gameManager;

    float range = 100f;
    public float power = 100f;
    public float damage = 10f;

    void Update()
    {
        if (!gameManager.isEndedGame)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Fire();
            }
        }
    }

    void Fire()
    {
        // 파티클을 재생합니다.
        muzzleFlash.Play();

        // 총소리를 재생합니다.
        gunShotSound.PlayOneShot(gunShotSound.clip);

        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range))
        {
            TargetNPC target = hit.transform.GetComponent<TargetNPC>();

            // NPC가 총에 맞으면 데미지를 입힙니다.
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            // 총알에 힘을 줍니다.
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(camera.transform.forward * power);
            }

            // Instantiate : Clones the object original and returns the clone (Original, Postion, Rotation)
            GameObject hitEffect = Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(-hit.normal));
            Destroy(hitEffect, 2f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    private Transform target;
    private EnemyIA targetEnemy;

    [Header("General")]
    public float range = 15f;
    public float StartHealth = 100;
    private float health = 100;
    private bool isDead;

    [Header("Use Bullets (Default)")]
    public GameObject bulletPrefab;

    public AudioClip SoundBullet;
    private AudioSource audioSR;

    public float FireRate = 1f;
    private float fireCountdown = 0;

    [Header("Use Laser")]
    public bool useLaser = false;
    public float damageOverTime = 30f;
    public float slowAmount = 0.5f;
    public LineRenderer lineRenderer;
    public ParticleSystem ImpactEffect;
    public Light ImpactLight;

    [Header("Unity Setup Fiels")]
    public string enemyTag = "Enemy";
    public float SpeedRotate = 5;
    public Transform partToRotate;
    public Transform firePoint;


	void Start ()
    {

        health = StartHealth;

        audioSR = GetComponent<AudioSource>();

        //Appel la function "UpdateTarget" tout les 0.5/s
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortesDistance = Mathf.Infinity;
        GameObject nearesEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortesDistance)
            {
                shortesDistance = distanceToEnemy;
                nearesEnemy = enemy;
            }
        }

        if (nearesEnemy != null && shortesDistance <= range)
        {
            target = nearesEnemy.transform;
            targetEnemy = nearesEnemy.GetComponent<EnemyIA>();
        }
        else
            target = null;
    }
	
	void Update ()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    ImpactEffect.Stop();
                    ImpactLight.enabled = false;

                }
            }

            return;
        }
           

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }

        else
        {
            if(fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1 / FireRate;
            }

            fireCountdown -= Time.deltaTime;
 
        }
	}


    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * SpeedRotate).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0, rotation.y, 0);

    }

    void Laser()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowAmount);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            ImpactEffect.Play();
            ImpactLight.enabled = true;
        }
        
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        ImpactEffect.transform.position = target.position + dir.normalized;

        ImpactEffect.transform.rotation = Quaternion.LookRotation(dir);

    }

    public void TakeDamage(float Amount)
    {
        health -= Amount;

        if (health <= 0f && !isDead)
        {
            isDead = true;
            Destroy(gameObject);
        }

    }

    private void Shoot()
    {

        //joue le sons
        audioSR.PlayOneShot(SoundBullet);

       //Fait apparètre la balle
       GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

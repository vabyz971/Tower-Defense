
using UnityEngine;
using UnityEngine.UI;

public class EnemyIA : MonoBehaviour {

    public float startSpeed = 10f;

    [HideInInspector]
    public float speed = 10;

    public float StartHealth = 100;
    private float health = 100;

    public int Money = 50;

    public GameObject deathEffect;
    public GameObject ExplosionDamage;

    [Header("GUI  Enemy")]
    public Image HeathBar;

    private bool isDead;

    private void Start()
    {
        speed = startSpeed;

        health = StartHealth;

        gameObject.name = gameObject.name +": "+ WaveSpawner.EnemiesAlive;

    }

    public void TakeDamage(float Amount)
    {
        health -= Amount;

        HeathBar.fillAmount = health / StartHealth;

        if(health <= 0f && !isDead)
        {
            Die();
        }
    }
    
    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    void Die()
    {
        isDead = true;
        Debug.Log(WaveSpawner.EnemiesAlive + " Enemie Live");
        WaveSpawner.EnemiesAlive--;
        PlayerStats.Money += Money;

        //Crée un effect a la mort de l'énemie
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 3);

        //Crée une explosion qui endomage les turrels
        GameObject explosion = (GameObject)Instantiate(ExplosionDamage, transform.position, Quaternion.identity);
        Destroy(explosion, 0.5f);

        Destroy(gameObject);
    }

}

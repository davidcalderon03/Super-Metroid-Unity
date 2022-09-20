using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 1000;
    [SerializeField] int damage = 10;
    [SerializeField] List<GameObject> pickups;
    [SerializeField] bool isFrozen;
    PlayerItems player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerItems>();
        isFrozen = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Projectile projectile = other.gameObject.GetComponent<Projectile>();
        if (!projectile) { return; }
        ProcessHit(projectile);

    }
    private void ProcessHit(Projectile projectile)
    {
        health -= projectile.GetDamage();
        if (projectile.GetProjectileType() != 5)
        { projectile.Hit(); }
        if (projectile.GetProjectileType() == 3)
        {
            if (isFrozen) { }
            else { StartCoroutine(ProcessFreeze()); }
        }
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        int max = 1;
        if (player.GetMaxMissiles() != 0) { max += 1; }
        if (player.GetMaxSuperMissiles() != 0) { max += 1; }
        if (player.GetMaxPowerBombs() != 0) { max += 1; }
        Destroy(gameObject);
        int pickup = Random.Range(0, max);
        Vector3 scale = new Vector3(1, 1, 1);
        if (pickup == 0) {
            int sc = Random.Range(1, 4);
            scale = new Vector3((float)(sc*0.25), (float)(sc *0.25), (float)(sc *0.25));
        }

        GameObject newPickup = Instantiate(pickups[pickup], new Vector3(transform.position.x, transform.position.y, -1f), Quaternion.Euler(scale)) as GameObject;
    }
    public int GetDamage()
    {
        return damage;
    }
    IEnumerator ProcessFreeze()
    {
        isFrozen = true;
        GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<Renderer>().material.color = Color.blue;
        gameObject.transform.GetChild(0).gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(5f);
        isFrozen = false;
        GetComponent<Animator>().enabled = true;
        gameObject.GetComponent<Renderer>().material.color = Color.white;
        gameObject.transform.GetChild(0).gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    public bool GetIsFrozen()
    {
        return isFrozen;
    }
}

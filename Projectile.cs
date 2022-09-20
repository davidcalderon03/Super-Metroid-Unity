using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] int projectileType;
    public void Hit()
    {
        Destroy(gameObject);
    }
    public int GetDamage()
    {
        return damage;
    }
    public int GetProjectileType()
    {
        return projectileType;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().gameObject.layer == LayerMask.NameToLayer("Ground") || collision.GetComponent<Collider2D>().gameObject.layer == LayerMask.NameToLayer("Walls"))
        {
            Destroy(gameObject);
        }
    }
}

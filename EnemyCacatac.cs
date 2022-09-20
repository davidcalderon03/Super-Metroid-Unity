using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCacatac : MonoBehaviour
{
    int i=0;
    float direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1f && !GetComponent<Enemy>().GetIsFrozen())
        {
            if (GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Ground"))) {GetComponent<Rigidbody2D>().gravityScale = 0f; GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f); }
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.9f, LayerMask.GetMask("Ground"));

            if (hit.collider == null || i == 300) { i = 0; direction *= -1; }
            i++;
            transform.position += new Vector3(direction * 0.01f, 0f, 0f);
        }
    }
}

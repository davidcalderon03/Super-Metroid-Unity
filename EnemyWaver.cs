using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaver : MonoBehaviour
{
    [SerializeField] float xVelocity;
    [SerializeField] float distance = 50f;
    BoxCollider2D myBoxCollider;
    float originalX;
    // Start is called before the first frame update
    void Start()
    {
        originalX = transform.position.x;
        xVelocity = 1f;
        myBoxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1f && !GetComponent<Enemy>().GetIsFrozen())
        {
            gameObject.transform.position = new Vector3(transform.position.x + (xVelocity * 0.06f), 1.2f*(float)Mathf.Cos(transform.position.x), 0);
            if (transform.position.x >= originalX + distance) { xVelocity = -1f; transform.localScale = new Vector3(xVelocity, 1, 1); }
            if (transform.position.x <= originalX) { xVelocity = 1f; transform.localScale = new Vector3(xVelocity, 1, 1); }
            //RaycastHit2D hit1 = Physics2D.Raycast(transform.position, Vector2.right, 1f, LayerMask.GetMask("Ground"));
            //RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.left, 1f, LayerMask.GetMask("Ground"));
            //RaycastHit2D hit3 = Physics2D.Raycast(transform.position, Vector2.up, 0.1f, LayerMask.GetMask("Ground"));
            //RaycastHit2D hit4 = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
            //if (hit1.collider!=null || hit2.collider!=null) { xVelocity *= -1; transform.localScale = new Vector3(xVelocity, 1, 1); }
            //if (myBoxCollider.IsTouchingLayers(LayerMask.NameToLayer("Ground"))){ transform.position += new Vector3(0f, -3f, 0f); Debug.Log("It is up"); }
            //else if (hit4.collider != null) { transform.position += new Vector3(0f, 0.1f, 0f); Debug.Log("It is down"); }
        }
    }
}

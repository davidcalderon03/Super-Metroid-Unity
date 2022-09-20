using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkree : MonoBehaviour
{
    [SerializeField] float yVelocity;
    BoxCollider2D myBoxCollider;

    // Start is called before the first frame update
    void Start()
    {
        yVelocity = 0;
        myBoxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1f && !GetComponent<Enemy>().GetIsFrozen())
        {
            if (GameObject.Find("Samus") == null) { }
            else if (Mathf.Abs(GameObject.Find("Samus").transform.position.x - gameObject.transform.position.x) < 1) { yVelocity = -0.25f; }

            if (!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { transform.position += new Vector3(0, yVelocity, 0); }
            else { yVelocity = 0f; Destroy(gameObject, 0.5f); }
        }

    }
}

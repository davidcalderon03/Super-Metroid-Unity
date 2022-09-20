using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollider : MonoBehaviour
{
    [SerializeField] int side = 1;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (side == 1) { transform.position = GameObject.FindWithTag("MainCamera").transform.position + new Vector3(-20f, 0f, 0f); }
        if (side == 2) { transform.position = GameObject.FindWithTag("MainCamera").transform.position + new Vector3(20f, 0f, 0f); }  
    }
}

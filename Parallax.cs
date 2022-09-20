using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Material myMaterial;
    [SerializeField] GameObject camera;
    Vector3 startpos;
    // Start is called before the first frame update
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        //camera = GameObject.FindWithTag("Main Camera");
        transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, 0f);
        startpos = camera.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, 0f);
        if (gameObject.name == "Background Fog ") { myMaterial.mainTextureOffset += new Vector2(.001f, .001f);}
        else { myMaterial.mainTextureOffset = new Vector2((transform.position.x - startpos.x) / 100f, (transform.position.y - startpos.y) / 100f); }
    }
}

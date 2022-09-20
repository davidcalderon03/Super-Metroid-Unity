using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] Animator myAnimator;
    [SerializeField] int doorType;
    [SerializeField] GameObject doorPipe;
    [SerializeField] Vector3 newPosition = new Vector3(10f, 0f, 0f);
    bool enter;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        enter = true;
    }

    // Update is called once per frame
    void Update()
    {
          
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.name == "Samus" && myAnimator.GetBool("hit") && enter)
        {
            enter = false;
            if (transform.localRotation.eulerAngles.z == 0f) { StartCoroutine(LoadNextLevel1()); }
            else if (transform.localRotation.eulerAngles.z == 180f) { StartCoroutine(LoadNextLevel2()); }
            else if (transform.localRotation.eulerAngles.z == 90f) { StartCoroutine(LoadNextLevel3()); }
            else if (transform.localRotation.eulerAngles.z == 270f) { StartCoroutine(LoadNextLevel4()); }
        }
        else
        {
            Projectile projectile = collider.gameObject.GetComponent<Projectile>();
            if (!projectile) { return; }
            if (myAnimator.GetBool("hit") == false) { projectile.Hit(); }

            if (doorType == 0 && (collider.GetComponent<Projectile>().GetProjectileType() == 0|| collider.GetComponent<Projectile>().GetProjectileType() == 3|| collider.GetComponent<Projectile>().GetProjectileType() == 4|| collider.GetComponent<Projectile>().GetProjectileType() == 5))
            {
                myAnimator.SetBool("hit", true);
            }
            else if (doorType == 1 && collider.GetComponent<Projectile>().GetProjectileType() == 1)
            {
                myAnimator.SetBool("hit", true);
            }
            else if (doorType == 2 && collider.GetComponent<Projectile>().GetProjectileType() == 2)
            {
                myAnimator.SetBool("hit", true);
            }
        }
    }

    IEnumerator LoadNextLevel1()
    {
        float oldDoorDiff = GameObject.FindWithTag("MainCamera").transform.position.y-this.transform.position.y;
        SceneManager.LoadScene("Transition Scene");
        DontDestroyOnLoad(gameObject);

        transform.position = new Vector3(-9.2f, 7f-oldDoorDiff, 0f);
        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForSecondsRealtime(1f/60f);
            transform.position += new Vector3((30f/60f), 0f, 0f);
            if (transform.position.x > 21f) { transform.position = new Vector3(21f, 7f- oldDoorDiff, 0f); break; }
        }
        SceneManager.LoadScene(sceneName);
        yield return new WaitForSecondsRealtime(0.01f);
        if (GameObject.FindWithTag("Player") != null) { GameObject.FindWithTag("Player").transform.position = newPosition + new Vector3(-1.3f, 0f, 0f); }
        if (GameObject.FindWithTag("Player") != null) { GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().SetDirection(-1f); }
        GameObject.FindWithTag("MainCamera").transform.position = new Vector3(GameObject.FindWithTag("MainCamera").transform.position.x, transform.position.y + oldDoorDiff, 0f);
        Destroy(gameObject);
    }
    IEnumerator LoadNextLevel2()
    {
        float oldDoorDiff = GameObject.FindWithTag("MainCamera").transform.position.y - this.transform.position.y;
        SceneManager.LoadScene("Transition Scene");
        DontDestroyOnLoad(gameObject);

        transform.position = new Vector3(21f, 7f - oldDoorDiff, 0f);
        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForSecondsRealtime(1f / 60f);
            transform.position -= new Vector3((30f / 60f), 0f, 0f);
            if (transform.position.x <-9.2f) { transform.position = new Vector3(-9.2f, 7f-oldDoorDiff, 0f); break; }
        }
        SceneManager.LoadScene(sceneName);
        yield return new WaitForSecondsRealtime(0.01f);
        if (GameObject.FindWithTag("Player") != null) { GameObject.FindWithTag("Player").transform.position = newPosition + new Vector3(+1.3f, 0f, 0f); }
        if (GameObject.FindWithTag("Player") != null) { GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().SetDirection(1f); }
        GameObject.FindWithTag("MainCamera").transform.position = new Vector3(GameObject.FindWithTag("MainCamera").transform.position.x, transform.position.y + oldDoorDiff, 0f);
        Destroy(gameObject);
    }
    IEnumerator LoadNextLevel3()
    {
        float oldDoorDiff = GameObject.FindWithTag("MainCamera").transform.position.x - this.transform.position.x;
        Debug.Log(oldDoorDiff + " hi");
        SceneManager.LoadScene("Transition Scene");
        DontDestroyOnLoad(gameObject);

        transform.position = new Vector3(6f -oldDoorDiff,-1f, 0f);
        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForSecondsRealtime(1f / 60f);
            transform.position += new Vector3(oldDoorDiff/60f,(18f / 60f), 0f);
            if (transform.position.y > 17f) { transform.position = new Vector3(6f, 17f, 0f); break; }
        }
        SceneManager.LoadScene(sceneName);
        yield return new WaitForSecondsRealtime(0.01f);
        if (GameObject.FindWithTag("Player") != null) { GameObject.FindWithTag("Player").transform.position = newPosition + new Vector3(0f, -1.3f, 0f); }
        Destroy(gameObject);
    }
    IEnumerator LoadNextLevel4()
    {
        float oldDoorDiff = GameObject.FindWithTag("MainCamera").transform.position.x - this.transform.position.x;
        Debug.Log(oldDoorDiff + " hi");
        SceneManager.LoadScene("Transition Scene");
        DontDestroyOnLoad(gameObject);

        transform.position = new Vector3(6f - oldDoorDiff, 18f, 0f);
        for (int i = 0; i < 60; i++)
        {
            yield return new WaitForSecondsRealtime(1f / 60f);
            transform.position += new Vector3(oldDoorDiff / 60f, (-18f / 60f), 0f);
            if (transform.position.y < -1f) { transform.position = new Vector3(6f, -1f, 0f); break; }
        }
        SceneManager.LoadScene(sceneName);
        yield return new WaitForSecondsRealtime(0.01f);
        if (GameObject.FindWithTag("Player") != null) { GameObject.FindWithTag("Player").transform.position = newPosition + new Vector3(0f, +2f, 0f); }
        Destroy(gameObject);
    }
}

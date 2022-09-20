using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] int level;
    [SerializeField] int itemNumber;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckExistence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Remove()
    {
        FindObjectOfType<GameManager>().RemoveItem(level, itemNumber);
    }
    IEnumerator CheckExistence()
    {
        yield return new WaitForSeconds(0.01f);
        if (FindObjectOfType<GameManager>().GetItems()[level, itemNumber] == false)
        {
            Destroy(gameObject);
        }
    }
}

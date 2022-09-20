using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextItem : MonoBehaviour
{
    TMPro.TextMeshProUGUI itemText;
    // Start is called before the first frame update
    void Start()
    {
        itemText = gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void MakeText(string text)
    {
        itemText.text = text;
    }
}

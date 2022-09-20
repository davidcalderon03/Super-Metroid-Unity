using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMissiles : MonoBehaviour
{
    TMPro.TextMeshProUGUI missilesText;
    PlayerItems playerItems;

    // Start is called before the first frame update
    void Start()
    {
        missilesText = GetComponent<TMPro.TextMeshProUGUI>();
        playerItems = FindObjectOfType<PlayerItems>();
    }

    // Update is called once per frame
    void Update()
    {
        missilesText.text = playerItems.GetMissiles().ToString();
    }
}

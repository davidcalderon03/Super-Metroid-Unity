using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSuperMissiles : MonoBehaviour
{
    TMPro.TextMeshProUGUI superMissilesText;
    PlayerItems playerItems;

    // Start is called before the first frame update
    void Start()
    {
        superMissilesText = GetComponent<TMPro.TextMeshProUGUI>();
        playerItems = FindObjectOfType<PlayerItems>();
    }

    // Update is called once per frame
    void Update()
    {
        superMissilesText.text = playerItems.GetSuperMissiles().ToString();
    }
}

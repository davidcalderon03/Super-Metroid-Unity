using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPowerBombs : MonoBehaviour
{
    TMPro.TextMeshProUGUI powerBombsText;
    PlayerItems playerItems;

    // Start is called before the first frame update
    void Start()
    {
        powerBombsText = GetComponent<TMPro.TextMeshProUGUI>();
        playerItems = FindObjectOfType<PlayerItems>();
    }

    // Update is called once per frame
    void Update()
    {
        powerBombsText.text = playerItems.GetPowerBombs().ToString();
    }
}

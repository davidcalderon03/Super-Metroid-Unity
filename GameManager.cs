using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Max Health/Item Counts")]
    [SerializeField] int maxEnergy;
    [SerializeField] int maxMissiles;
    [SerializeField] int maxSuperMissiles;
    [SerializeField] int maxPowerBombs;
    [Header("Current Health/Item Counts")]
    [SerializeField] int energy = 99;
    [SerializeField] int missiles = 0;
    [SerializeField] int superMissiles = 0;
    [SerializeField] int powerBombs = 0;
    [Header("Item Acquisitions")]
    [SerializeField] bool hasMorphBall = false;
    [SerializeField] bool hasMorphBallBombs = false;
    [SerializeField] bool hasHighJump;
    [SerializeField] bool hasGrappleBeam = false;
    [SerializeField] bool hasIceBeam = false;
    [SerializeField] bool hasWaveBeam = false;
    [SerializeField] bool hasPlasmaBeam = false;
    [SerializeField] bool[,] items;
    private void Awake()
    {
        SetUpSingleton();
        maxEnergy = 99; maxMissiles = 0; maxSuperMissiles = 0; maxPowerBombs = 0;
        energy = 99; missiles = 0; superMissiles = 0;powerBombs = 0;
        hasMorphBall = false; hasMorphBallBombs = false;hasHighJump = false;  hasGrappleBeam = false;
        items = new bool[100, 10];
        for(int i = 0; i < items.GetLength(0); i++)
        {
            for(int j = 0; j < items.GetLength(1); j++)
            {
                items[i, j] = true;
            }
        }
        for (int row = 0; row < items.GetLength(0); row++)
        {
            string ok = "";
            for (int col = 0; col < items.GetLength(1); col++)
            {
                ok = ok + items[row, col] + "";
            }
            //Debug.Log(ok);
        }
    }
    private void SetUpSingleton()
    {
        if (FindObjectsOfType<GameManager>().Length > 1){Destroy(gameObject); }
        else{DontDestroyOnLoad(gameObject); }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<PlayerItems>() != null && FindObjectOfType<PlayerItems>().GetIsReadyForStatsUpdate()==true)
        {
            PlayerItems samus = FindObjectOfType<PlayerItems>();
            maxEnergy = samus.GetMaxEnergy();
            maxMissiles = samus.GetMaxMissiles();
            maxSuperMissiles = samus.GetMaxSuperMissiles();
            maxPowerBombs = samus.GetMaxPowerBombs();
            energy = samus.GetEnergy();
            missiles = samus.GetMissiles();
            superMissiles = samus.GetSuperMissiles();
            powerBombs = samus.GetPowerBombs();
            hasMorphBall = samus.GetHasMorphBall();
            hasMorphBallBombs = samus.GetHasMorphBallBombs();
            hasHighJump = samus.GetHasHighJump();
            hasGrappleBeam = samus.GetHasGrappleBeam();
            hasIceBeam = samus.GetHasIceBeam();
            hasWaveBeam = samus.GetHasWaveBeam();
            hasPlasmaBeam = samus.GetHasPlasmaBeam();
            if (samus.GetEnergy() <= 0) { StartCoroutine(ResetGame()); }
        }
    }
    public void RemoveItem(int r, int c)
    {
        items[r, c] = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Scene 1");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Start Scene");
        Destroy(gameObject);
    }

    //GETTERS (MAX)
    public int GetMaxEnergy() { return maxEnergy; }
    public int GetMaxMissiles() { return maxMissiles; }
    public int GetMaxSuperMissiles() { return maxSuperMissiles; }
    public int GetMaxPowerBombs() { return maxPowerBombs; }

    //GETTERS (CONDITIONS)
    public bool GetHasMorphBall() { return hasMorphBall; }
    public bool GetHasMorphBallBombs() { return hasMorphBallBombs; }
    public bool GetHasHighJump() { return hasHighJump; }
    public bool GetHasGrappleBeam() { return hasGrappleBeam; }
    public bool GetHasIceBeam() { return hasIceBeam; }
    public bool GetHasWaveBeam() { return hasWaveBeam; }
    public bool GetHasPlasmaBeam() { return hasPlasmaBeam; }

    //GETTERS (CURRENT)
    public int GetEnergy() { return energy; }
    public int GetMissiles() { return missiles; }
    public int GetSuperMissiles() { return superMissiles; }
    public int GetPowerBombs() { return powerBombs; }

    public bool[,] GetItems() { return items; }
}

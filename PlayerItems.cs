using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    [Header("Max Health/Item Counts")]
    [SerializeField] int maxEnergy = 99;
    [SerializeField] int maxMissiles = 0;
    [SerializeField] int maxSuperMissiles = 0;
    [SerializeField] int maxPowerBombs = 0;
    [Header("Current Health/Item Counts")]
    [SerializeField] int energy = 99;
    [SerializeField] int missiles = 0;
    [SerializeField] int superMissiles = 0;
    [SerializeField] int powerBombs = 0;


    [Header("Item Acquisitions")]
    [SerializeField] bool hasMorphBall;
    [SerializeField] bool hasMorphBallBombs;
    [SerializeField] bool hasHighJump;
    [SerializeField] bool hasGrappleBeam;
    [SerializeField] bool hasIceBeam;
    [SerializeField] bool hasWaveBeam;
    [SerializeField] bool hasPlasmaBeam;

    [SerializeField] AudioClip itemSound;
    [SerializeField] AudioClip enterSound;
    [SerializeField] AudioClip powerBeamSound;
    [SerializeField] AudioClip missileSound;
    [SerializeField] AudioClip superMissileSound;
    [SerializeField] [Range(0, 1)] float itemSoundVolume = 0.25f;
    [SerializeField] [Range(0, 1)] float shootSoundVolume = 1f;
    MusicPlayer musicPlayer;
    bool readyForStatsUpdate;
    TextItem itemText;

    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        readyForStatsUpdate = false;
        StartCoroutine(AdaptPlayerStats());
        itemText = FindObjectOfType<TextItem>();
        itemText.gameObject.SetActive(false);

        //AudioSource.PlayClipAtPoint(enterSound, Camera.main.transform.position, itemSoundVolume);
        //StartCoroutine(GamePause());
    }

    // Update is called once per frame
    void Update()
    {
        Die();
        if (energy > maxEnergy) { energy = maxEnergy; }
        if (missiles > maxMissiles) { missiles = maxMissiles; }
        if (superMissiles > maxSuperMissiles) { superMissiles = maxSuperMissiles; }
        if (powerBombs > maxPowerBombs) { powerBombs = maxPowerBombs; }
        if (hasHighJump) { }
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Collider2D>().gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            if (collider.gameObject.name == "Energy Item") { maxEnergy += 100;energy = maxEnergy; }
            else if (collider.gameObject.name == "Missile Item") { maxMissiles += 5; missiles += 5; }
            else if (collider.gameObject.name == "Super Missile Item") { maxSuperMissiles += 5; superMissiles += 5;  }
            else if (collider.gameObject.name == "Power Bomb Item") { maxPowerBombs += 5; powerBombs += 5;  }
            else if (collider.gameObject.name == "Morph Ball Item") { hasMorphBall = true;  }
            else if (collider.gameObject.name == "Morph Ball Bombs Item") { hasMorphBallBombs = true;  }
            else if (collider.gameObject.name == "High Jump Item") { hasHighJump = true;  }
            else if (collider.gameObject.name == "Ice Beam Item") { hasIceBeam = true; }
            else if (collider.gameObject.name == "Wave Beam Item") { hasWaveBeam = true; }
            else if (collider.gameObject.name == "Plasma Beam Item") { hasPlasmaBeam = true; }

            else if (collider.gameObject.name == "Energy Pickup(Clone)") { energy += (int)(collider.gameObject.transform.localScale.x * 50) ; }
            else if (collider.gameObject.name == "Missile Pickup(Clone)") { missiles += 2; }
            else if (collider.gameObject.name == "Super Missile Pickup(Clone)") { superMissiles += 1; }
            else if (collider.gameObject.name == "Power Bomb Pickup(Clone)") { powerBombs += 2; }

            if (collider.GetComponent<Item>() != null) { collider.GetComponent<Item>().Remove(); }
            Destroy(collider.gameObject);
            Debug.Log(collider.gameObject.name.Substring(collider.gameObject.name.Length-7));
            if (collider.gameObject.name.Substring(collider.gameObject.name.Length-7)!="(Clone)") { AudioSource.PlayClipAtPoint(itemSound, Camera.main.transform.position, itemSoundVolume); StartCoroutine(GamePause(collider.gameObject.name)); }
        }
    }
    
    public void UseProjectile(int projectileType)
    {
        if (projectileType == 1) { missiles--; AudioSource.PlayClipAtPoint(missileSound, Camera.main.transform.position, shootSoundVolume); }
        else if (projectileType == 2) { superMissiles--; AudioSource.PlayClipAtPoint(superMissileSound , Camera.main.transform.position, shootSoundVolume); }
        else if (projectileType == 3) { powerBombs--; AudioSource.PlayClipAtPoint(powerBeamSound, Camera.main.transform.position, shootSoundVolume);  }
        else { AudioSource.PlayClipAtPoint(powerBeamSound, Camera.main.transform.position, shootSoundVolume); }
    }
    private void Die()
    {
        if (energy <= 0)
        {
            energy = 0;
            Debug.Log("You died");
        }
    }
    IEnumerator GamePause(string name)
    {
        itemText.gameObject.SetActive(true);
        itemText.MakeText(name);
        Time.timeScale = 0.001f;
        musicPlayer.Pause();
        yield return new WaitForSeconds(0.007f);
        Time.timeScale = 1f;
        itemText.gameObject.SetActive(false);
        musicPlayer.Play();
    }
    IEnumerator AdaptPlayerStats()
    {
        yield return new WaitForSeconds(0.01f);
        GameManager gc = FindObjectOfType<GameManager>();
        musicPlayer = FindObjectOfType<MusicPlayer>();
        maxEnergy = gc.GetMaxEnergy();
        maxMissiles = gc.GetMaxMissiles();
        maxSuperMissiles = gc.GetMaxSuperMissiles();
        maxPowerBombs = gc.GetMaxPowerBombs();
        energy = gc.GetEnergy();
        missiles = gc.GetMissiles();
        superMissiles = gc.GetSuperMissiles();
        powerBombs = gc.GetPowerBombs();
        hasMorphBall = gc.GetHasMorphBall();
        hasMorphBallBombs = gc.GetHasMorphBallBombs();
        hasHighJump = gc.GetHasHighJump();
        hasGrappleBeam = gc.GetHasGrappleBeam();
        hasIceBeam = gc.GetHasIceBeam();
        hasWaveBeam = gc.GetHasWaveBeam();
        hasPlasmaBeam = gc.GetHasPlasmaBeam();
        readyForStatsUpdate = true;
    }

    public bool GetIsReadyForStatsUpdate() { return readyForStatsUpdate; }
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

    //SETTERS
    public void SetEnergy(int newEnergy){energy = newEnergy;}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] List<GameObject> projectiles;
    [SerializeField] float projectileSpeed = 20f;
    Animator myAnimator;
    Coroutine firingCoroutine;
    bool isCrouching = false;
    int allDirection;
    bool isShootingDown;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myAnimator.GetInteger("directionUD") == -1 && !isCrouching) { isCrouching = true; /*gameObject.GetComponent<CapsuleCollider2D>().offset += new Vector2(0, 0.5f); gameObject.transform.position -= new Vector3(0, 0.5f, 0);*/ }
        if (myAnimator.GetInteger("directionUD") == 0 && isCrouching) { /*isCrouching = false; gameObject.GetComponent<CapsuleCollider2D>().offset -= new Vector2(0, 0.5f); gameObject.transform.position += new Vector3(0, 0.5f, 0);*/ }

        if (myAnimator.GetFloat("directionLR") == 1 && myAnimator.GetInteger("diagonalUD") == 1) { allDirection = 7; }
        else if (myAnimator.GetFloat("directionLR") == 1 && myAnimator.GetInteger("diagonalUD") == 0 && myAnimator.GetInteger("directionUD") != 1) { allDirection = 0; }
        else if (myAnimator.GetFloat("directionLR") == 1 && myAnimator.GetInteger("diagonalUD") == -1) { allDirection = 1; }
        else if (myAnimator.GetFloat("directionLR") == -1 && myAnimator.GetInteger("diagonalUD") == -1) { allDirection = 3; }
        else if (myAnimator.GetFloat("directionLR") == -1 && myAnimator.GetInteger("diagonalUD") == 0 && myAnimator.GetInteger("directionUD") !=1) { allDirection = 4; }
        else if (myAnimator.GetFloat("directionLR") == -1 && myAnimator.GetInteger("diagonalUD") == 1) { allDirection = 5; }
        else if (myAnimator.GetInteger("directionUD") == 1) { allDirection = 6; }
        else { allDirection = 2; }
    }
    void OnFire()
    {
        GameObject pro;
        float deltaY;
        int shootX;
        int shootY;
        if (myAnimator.GetInteger("directionUD") == -1){ deltaY = -0.5f;}
        else { deltaY = 0.2f; }
        if (myAnimator.GetInteger("missile") == 1 &&gameObject.GetComponent<PlayerItems>().GetMissiles() > 0){ pro = projectiles[1];}
        else if (myAnimator.GetInteger("missile") == 2 && gameObject.GetComponent<PlayerItems>().GetSuperMissiles() > 0) { pro = projectiles[2]; }
        else {
            if (GetComponent<PlayerItems>().GetHasPlasmaBeam() == true) { pro = projectiles[5]; }
            else if (GetComponent<PlayerItems>().GetHasWaveBeam() == true) { pro = projectiles[4]; }
            else if (GetComponent<PlayerItems>().GetHasIceBeam() == true) { pro = projectiles[3]; }
            else { pro = projectiles[0]; }
        }

        if (allDirection == 7 || allDirection == 0 || allDirection == 1) { shootX = 1; }
        else if (allDirection == 5 || allDirection == 3 || allDirection == 4) { shootX = -1; }
        else { shootX = 0; }
        if (allDirection == 5 || allDirection == 6 || allDirection == 7) { shootY = 1; }
        else if (allDirection == 3 || allDirection == 2 || allDirection == 1) { shootY = -1; }
        else { shootY = 0; }
        if(pro == projectiles[5] || pro== projectiles[4])
        {
            GameObject projectile1 = Instantiate(
                     pro, transform.position + new Vector3(0, deltaY + 0.8f, 0), Quaternion.Euler(0f, 0f, -1 * (45 * allDirection))) as GameObject;
            projectile1.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed * shootX, projectileSpeed * shootY);
            GameObject projectile2 = Instantiate(
                     pro, transform.position + new Vector3(0, deltaY - 0.8f, 0), Quaternion.Euler(0f, 0f, -1 * (45 * allDirection))) as GameObject;
            projectile2.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed * shootX, projectileSpeed * shootY);
        }
        GameObject projectile = Instantiate(
                     pro, transform.position + new Vector3(0, deltaY, 0), Quaternion.Euler(0f, 0f, -1*(45*allDirection))) as GameObject;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed*shootX, projectileSpeed*shootY);
        gameObject.GetComponent<PlayerItems>().UseProjectile(myAnimator.GetInteger("missile"));
    }

}



        /*
        5  6  7
        4     0
        3  2  1
        */

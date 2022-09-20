using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float direction = -1;

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    BoxCollider2D myBoxCollider;
    bool invincibilityFrames;

    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        myAnimator.SetBool("isRunning", false);
        myAnimator.SetFloat("directionLR", 1);
        myAnimator.SetInteger("directionUD", 0);
        myAnimator.SetBool("isDead", false);
        invincibilityFrames = false;
    }
    // Update is called once per frame
    void Update()
    {
        Reshape();
        Move();
        FlipSprite();
        if (gameObject.GetComponent<PlayerItems>().GetMissiles() <= 0 && myAnimator.GetInteger("missile") == 1) { Switch(); }
        else if (gameObject.GetComponent<PlayerItems>().GetSuperMissiles() <= 0 && myAnimator.GetInteger("missile") == 2) { Switch(); }
        else if (gameObject.GetComponent<PlayerItems>().GetPowerBombs() <= 0 && myAnimator.GetInteger("missile") == 3) { Switch(); }

        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, Vector2.down, 2f, LayerMask.GetMask("Ground"));
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.down, 2f, LayerMask.GetMask("Interactable"));
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position, Vector2.down, 2f, LayerMask.GetMask("Enemy Hit"));
        if (myAnimator.GetInteger("directionUD")==-2 || myAnimator.GetInteger("directionUD") == -1||(Mathf.Abs(myRigidbody.velocity.y) < 5f&& (hit1.collider != null || hit2.collider!=null || hit3.collider != null))) { myAnimator.SetInteger("jumpDirection", 0); }
        else if (myRigidbody.velocity.y > 0.1f) { myAnimator.SetInteger("jumpDirection", 1); }
        else{ myAnimator.SetInteger("jumpDirection", -1); }
        if (gameObject.GetComponent<PlayerItems>().GetEnergy() <= 0) { myAnimator.SetInteger("directionUD", 0); myAnimator.SetFloat("directionLR", 0f); }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Mathf.Abs(myRigidbody.velocity.y) < 1f &&(other.GetComponent<Collider2D>().gameObject.layer == LayerMask.NameToLayer("Ground") || other.GetComponent<Collider2D>().gameObject.layer == LayerMask.NameToLayer("Interactable"))) { myAnimator.SetInteger("jumpDirection", 0); myAnimator.SetInteger("jumpType", 0); return; }
        if (other.GetComponent<Collider2D>().gameObject.layer == LayerMask.NameToLayer("Enemy Hit"))
        {
            Enemy enemy = other.transform.parent.gameObject.GetComponent<Enemy>();
            if (!enemy) { return; }
            else if (other.transform.parent.gameObject.GetComponent<Enemy>().GetIsFrozen() && Mathf.Abs(myRigidbody.velocity.y)<1f) { myAnimator.SetInteger("jumpDirection", 0); myAnimator.SetInteger("jumpType", 0); return; }
            ProcessHit(enemy);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        //if (other.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) { myAnimator.SetInteger("jumpType", 0); }
    }
        //KEYBOARD MOVEMENT
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        //Debug.Log(moveInput);
    }
    void OnUp(InputValue value)
    {
        if (myAnimator.GetInteger("directionUD") == -2) { myAnimator.SetInteger("directionUD", -1); }
        else if (myAnimator.GetInteger("directionUD") == -1) { myAnimator.SetInteger("directionUD", 0); }
        else if (myAnimator.GetInteger("directionUD") == 0) { myAnimator.SetInteger("directionUD", 1); }
    }
    void OnUpRelease(InputValue value)
    {
        if (myAnimator.GetInteger("directionUD") == 1) { myAnimator.SetInteger("directionUD", 0);  }
    }
    void OnDown(InputValue value)
    {

        if (myAnimator.GetInteger("directionUD") == 0) { myAnimator.SetInteger("directionUD", -1); }
        else if (myAnimator.GetInteger("directionUD") == -1 && GetComponent<PlayerItems>().GetHasMorphBall()) { myAnimator.SetInteger("directionUD", -2); }
    }
    void OnDiagonal(InputValue value)
    {
        if (value.Get<Vector2>().y > 0) { myAnimator.SetInteger("diagonalUD", 1); }
        else if (value.Get<Vector2>().y < 0) { myAnimator.SetInteger("diagonalUD", -1); }
        else { myAnimator.SetInteger("diagonalUD", 0); }
    }
    /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //KEYBOARD ACTIONS
    void OnSwitch(InputValue value)
    {
        Switch();
    }
    void OnJump(InputValue value)
    {

        if (!value.isPressed || (!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && !myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Interactable")) && !myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Enemy Hit")))) { return; }
        else if (!GetComponent<PlayerItems>().GetHasHighJump()) { myRigidbody.velocity += new Vector2(0f, jumpSpeed); }
        else { myRigidbody.velocity += new Vector2(0f, 1.5f * jumpSpeed); }

        if (myRigidbody.velocity.x<0.1f && myRigidbody.velocity.x > -0.1f) { myAnimator.SetInteger("jumpType", 1); }
        else { myAnimator.SetInteger("jumpType", 2);  }
    }


    //GENERAL METHODS
    private void Reshape()
    {
        GetComponent<CapsuleCollider2D>().size = GetComponent<Renderer>().bounds.size - new Vector3(0.05f, 0.05f, 0f);
    }
    void Move()
    {
        float playerVelocityX = moveInput.x * runSpeed;
        if (myAnimator.GetInteger("jumpType") == 1 && myAnimator.GetInteger("jumpDirection") != 0) { playerVelocityX *= 0.75f; }
        else if (myAnimator.GetInteger("jumpType") == 2 && myAnimator.GetInteger("jumpDirection") != 0) { playerVelocityX *=1.2f ; }
        myRigidbody.velocity = new Vector2(playerVelocityX, myRigidbody.velocity.y);

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
        if (myAnimator.GetInteger("directionUD") == -1 && playerHasHorizontalSpeed) { myAnimator.SetInteger("directionUD", 0); }

    }
    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            myAnimator.SetFloat("directionLR", 1 * Mathf.Sign(myRigidbody.velocity.x));
            direction = 1 * Mathf.Sign(myRigidbody.velocity.x);
        }
    }
    private void ProcessHit(Enemy enemy)
    {
        if (!invincibilityFrames)
        {
            gameObject.GetComponent<PlayerItems>().SetEnergy(gameObject.GetComponent<PlayerItems>().GetEnergy() - enemy.GetDamage());
            if (GetComponent<PlayerItems>().GetEnergy() <= 0)
            {
                myAnimator.SetBool("isDead", true);
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                Destroy(gameObject, 2.1f);
            }
            StartCoroutine(Kickback((transform.position.x - enemy.transform.position.x) / Mathf.Abs(transform.position.x - enemy.transform.position.x)));
        }
    }
    void Switch()
    {
        int grapple = 0;
        if (gameObject.GetComponent<PlayerItems>().GetHasGrappleBeam()) { grapple = 1; }
        int[] counts = new int[] { 1, gameObject.GetComponent<PlayerItems>().GetMissiles(), gameObject.GetComponent<PlayerItems>().GetSuperMissiles(), gameObject.GetComponent<PlayerItems>().GetPowerBombs(), grapple };
        while (true)
        {
            myAnimator.SetInteger("missile", myAnimator.GetInteger("missile") + 1);
            if (myAnimator.GetInteger("missile") > 3) { myAnimator.SetInteger("missile", 0); }
            if (counts[myAnimator.GetInteger("missile")] > 0) { break; }
        }

    }


    //GETTERS
    public float GetDirection() { return direction; }
    public void SetDirection(float newDirection) { myAnimator.SetFloat("directionLR", newDirection); }
    IEnumerator Kickback(float direction)
    {
        invincibilityFrames = true;
        myRigidbody.AddForce(new Vector2(0f, 1000f), ForceMode2D.Force);
        for(int i = 0; i < 30; i++)
        {
            transform.position += new Vector3(direction * 0.15f, 0f);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(2f);
        invincibilityFrames = false;
    }
}

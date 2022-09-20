using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeemerZeela : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] int xDirection = 1;
    [SerializeField] int yDirection = 0;
    [SerializeField] Vector3 lastPosition;
    [SerializeField] bool isMoving = true;
    Rigidbody2D myRG2D;
    Vector3 originalPosition;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        myRG2D = GetComponent<Rigidbody2D>();
        xDirection = 0;
        yDirection = 0;
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        bool isClose = true;
        PlayerItems player = FindObjectOfType<PlayerItems>();
        if (player == null) { }
        else if (Mathf.Abs(player.gameObject.transform.position.x - transform.position.x) < 100 || Mathf.Abs(player.gameObject.transform.position.y - transform.position.y) < 100) { isClose = true; }
        else { isClose = false; transform.position = originalPosition; myRG2D.gravityScale = 10f; xDirection = 0;yDirection = 0; transform.eulerAngles = new Vector3(0f, 0f, 0f); isMoving = true; }

        if (Time.timeScale == 1f && !GetComponent<Enemy>().GetIsFrozen() && isClose) 
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 0.7f, LayerMask.GetMask("Ground"));
            if (xDirection == 1) { hit = Physics2D.Raycast(transform.position, Vector2.down, 0.7f, LayerMask.GetMask("Ground")); }
            else if (yDirection == 1) { hit = Physics2D.Raycast(transform.position, Vector2.right, 0.7f, LayerMask.GetMask("Ground")); }
            else if (xDirection == -1) { hit = Physics2D.Raycast(transform.position, Vector2.up, 0.7f, LayerMask.GetMask("Ground")); }
            else if (yDirection == -1) { hit = Physics2D.Raycast(transform.position, Vector2.left, 0.7f, LayerMask.GetMask("Ground")); }
            else { hit = Physics2D.Raycast(transform.position, Vector2.down, 0.15f, LayerMask.GetMask("Ground")); }


            if (i == 5) { lastPosition = transform.position; }
            if (i == 10) {
                if (Mathf.Abs(lastPosition.x - transform.position.x) < 0.05f && Mathf.Abs(lastPosition.y - transform.position.y) < 0.05f) { isMoving = false; }
                else { isMoving = true; }
                i = 0;
            }
            i++;
            myRG2D.velocity = new Vector2((float)speed * xDirection, (float)speed * yDirection);
            if (myRG2D.gravityScale != 0f && (GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Ground"))||hit.collider!=null)) { myRG2D.gravityScale = 0f; xDirection = 1;yDirection = 0; return; }
            if (myRG2D.gravityScale == 0.0f)
            {
                if (!isMoving && xDirection == 1 && yDirection == 0) { transform.position += new Vector3(0.15f, 0.2f); xDirection = 0; yDirection = 1; transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 90); i = 0; isMoving = true; myRG2D.velocity = Vector3.zero;  myRG2D.AddForce(new Vector2(10f, 0f), ForceMode2D.Force); }
                else if (!isMoving && xDirection == 0 && yDirection == 1) { transform.position += new Vector3(-0.2f, 0.15f); xDirection = -1; yDirection = 0; transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 90); i = 0; isMoving = true; myRG2D.velocity = Vector3.zero; myRG2D.AddForce(new Vector2(0f, 10f), ForceMode2D.Force); }
                else if (!isMoving && xDirection == -1 && yDirection == 0) { transform.position += new Vector3(-0.15f, -0.2f); xDirection = 0; yDirection = -1; transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 90); i = 0; isMoving = true; myRG2D.velocity = Vector3.zero; myRG2D.AddForce(new Vector2(-10f, 0f), ForceMode2D.Force); }
                else if (!isMoving && xDirection == 0 && yDirection == -1) { transform.position += new Vector3(0.2f, -0.15f); xDirection = 1; yDirection = 0; transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 90); i = 0; isMoving = true; myRG2D.velocity = Vector3.zero; myRG2D.AddForce(new Vector2(0f, -10f), ForceMode2D.Force); }
                else if (isMoving && xDirection == 0 && yDirection == 1 && hit.collider == null) { transform.position += new Vector3(0.9f, 0.5f, 0f); transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z - 90); xDirection = 1; yDirection = 0; myRG2D.velocity = Vector3.zero; myRG2D.AddForce(new Vector2(0f, -10f), ForceMode2D.Force); }
                else if (isMoving && xDirection == 1 && yDirection == 0 && hit.collider == null) { transform.position += new Vector3(0.4f, -0.9f, 0f); transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z - 90); xDirection = 0; yDirection = -1; myRG2D.velocity = Vector3.zero; myRG2D.AddForce(new Vector2(-10f, 0f), ForceMode2D.Force); }
                else if (isMoving && xDirection == 0 && yDirection == -1 && hit.collider == null) { transform.position += new Vector3(-0.9f, -0.4f, 0f); transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z - 90); xDirection = -1; yDirection = 0; myRG2D.velocity = Vector3.zero; myRG2D.AddForce(new Vector2(0f, 10f), ForceMode2D.Force); }
                else if (isMoving && xDirection == -1 && yDirection == 0 && hit.collider == null) { transform.position += new Vector3(-0.4f, 0.9f, 0f); transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z - 90); xDirection = 0; yDirection = 1; myRG2D.velocity = Vector3.zero; myRG2D.AddForce(new Vector2(10f, 0f), ForceMode2D.Force); }
            }
        }
    }
}

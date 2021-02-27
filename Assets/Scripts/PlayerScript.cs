using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    float moveSpeed = 5;
    Vector2 movementVector;
    float jumpForce = 5;

    Rigidbody2D rb;

    [SerializeField] GameObject inventory;

    bool jumping = false;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position += new Vector3(movementVector.x, movementVector.y, 0) * Time.deltaTime * moveSpeed;
        //rb.AddForce(movementVector * moveSpeed, ForceMode2D.Force);
    }

    public void OnMovement(InputValue value) {
        movementVector = value.Get<Vector2>();

        if (movementVector.magnitude > 0) {
            GetComponent<Animator>().SetBool("Walking", true);
            transform.localScale = new Vector3(movementVector.x, 1f, 1f);
            FindObjectOfType<Camera>().transform.localScale = new Vector3(movementVector.x, 1f, 1f);
        }
        else {
            GetComponent<Animator>().SetBool("Walking", false);
        }
    }

    public void OnJump() {
        if (!jumping) {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumping = true;
            GetComponent<Animator>().SetBool("Jumping", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            if (jumping) {
                jumping = false;
                GetComponent<Animator>().SetBool("Jumping", false);
            }
            return;
        }

        // If background is same color as obstacle
        if ((Color32)collision.gameObject.GetComponent<SpriteRenderer>().color == (FindObjectOfType<Camera>().backgroundColor)) {
            collision.collider.enabled = false;
        }
        else {
            collision.collider.enabled = true;
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            if (jumping) {
                jumping = false;
                GetComponent<Animator>().SetBool("Jumping", false);
            }
            return;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground"))
            return;

        collision.collider.enabled = true;

        // If background is not same color as obstacle
        if ((Color32)collision.gameObject.GetComponent<SpriteRenderer>().color != FindObjectOfType<Camera>().backgroundColor) {
            collision.collider.enabled = true;
        }
        else {
            collision.collider.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Portal")) {
            Destroy(this);
            GetComponent<Animator>().SetTrigger("LevelComplete");
        }
    }

    public void OnOpenInventory() {
        inventory.SetActive(!inventory.activeInHierarchy);
    }
}

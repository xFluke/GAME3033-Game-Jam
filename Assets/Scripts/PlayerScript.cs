using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    float moveSpeed = 5;
    Vector2 movementVector;
    float jumpForce = 5;

    Rigidbody2D rb;

    [SerializeField] GameObject inventory;

    bool jumping = false;
    bool paused = false;

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
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Portal")) {
            Destroy(this);
            GetComponent<Animator>().SetTrigger("LevelComplete");       
        }
        else if (collision.gameObject.CompareTag("Spike")) {
            Time.timeScale = 0;
            FindObjectOfType<UIManager>().EnableGameOverCanvas();
        }
    }

    public void OnPause() {
        paused = !paused;

        FindObjectOfType<UIManager>().TogglePauseMenu(paused);
        
    }
}

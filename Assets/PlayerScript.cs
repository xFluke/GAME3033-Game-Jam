using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    float moveSpeed = 10;
    Vector2 movementVector;
    float jumpForce = 5;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(movementVector.x, movementVector.y, 0) * Time.deltaTime * moveSpeed;
    }

    public void OnMovement(InputValue value) {
        movementVector = value.Get<Vector2>();
    }

    public void OnJump() {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Ground"))
            return;

        if ((Color32)collision.gameObject.GetComponent<SpriteRenderer>().color == (FindObjectOfType<Camera>().backgroundColor)) {
            Debug.Log("hi");
            collision.collider.enabled = false;
        }
    }

    public void OnChangeColor() {
        FindObjectOfType<ColorManager>().ChangeCameraBGColor();
    }
}

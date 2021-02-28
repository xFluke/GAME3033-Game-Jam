using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    Vector3 startingPos;
    [SerializeField] Vector3 endingPos;

    bool move = false;
    float moveSpeed = 0.005f;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (move) {
            if (transform.position.x < endingPos.x)
                transform.position += new Vector3(moveSpeed, 0f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            move = true;
            Debug.Log("Moving");
        }
    }
}

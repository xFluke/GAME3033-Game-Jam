using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredObject : MonoBehaviour
{
    public ColorState myColorState;

    // Start is called before the first frame update
    void Start() {
        FindObjectOfType<ColorManager>().OnBackgroundColorStateChange.AddListener(UpdateCollider);

        if (transform.childCount > 0) {
            SpriteRenderer[] childrenSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer spriteRenderer in childrenSpriteRenderers) {
                spriteRenderer.color = ColorManager.ColorStateToColor32(myColorState);
            }
        }
        else {
            GetComponent<SpriteRenderer>().color = ColorManager.ColorStateToColor32(myColorState);
        }
    }

    void UpdateCollider(ColorState currentBackgroundColorState) {
        if (currentBackgroundColorState == myColorState) {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}

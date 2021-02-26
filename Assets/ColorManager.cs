using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ColorState
{

}

public class ColorManager : MonoBehaviour
{
    public void ChangeCameraBGColor() {
        Debug.Log("Changing Color");
        StartCoroutine(AnimateBGColorChange(new Color32(255, 208, 189, 255)));
    }

    IEnumerator AnimateBGColorChange(Color32 targetColor) {
        float animationTime = 0.2f;
        float timeLeft = animationTime;

        Color32 currentColor = FindObjectOfType<Camera>().backgroundColor;

        while (timeLeft > Time.deltaTime) {

            currentColor = Color32.Lerp(currentColor, targetColor, Time.deltaTime / timeLeft);

            FindObjectOfType<Camera>().backgroundColor = currentColor;

            timeLeft -= Time.deltaTime;

            yield return new WaitForSeconds(Time.deltaTime);
        }

        FindObjectOfType<Camera>().backgroundColor = targetColor;
    }
}

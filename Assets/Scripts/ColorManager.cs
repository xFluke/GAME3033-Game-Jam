using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public enum ColorState
{
    SALMON,
    GRAY
}

public class ColorManager : MonoBehaviour
{
    [SerializeField] ColorState currentlySelectedColor;

    public UnityEvent<ColorState> OnBackgroundColorStateChange;
    
    public void ChangeCameraBGColor(ColorState colorState) {

        Color32 color32 = ColorStateToColor32(colorState);

        StartCoroutine(AnimateBGColorChange(color32));

        OnBackgroundColorStateChange.Invoke(colorState);
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

    public static Color32 ColorStateToColor32(ColorState colorState) {
        switch (colorState) {
            case ColorState.SALMON:
                return new Color32(255, 208, 189, 255);
            case ColorState.GRAY:
                return new Color32(94, 92, 92, 255);
            default:
                return new Color32(94, 92, 92, 255);
        }
    }
}

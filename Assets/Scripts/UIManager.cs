using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverCanvas;

    public void EnableGameOverCanvas() {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 1;
    }
}

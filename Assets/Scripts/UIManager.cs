using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject pauseCanvas;

    public void EnableGameOverCanvas() {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 1;
    }

    public void TogglePauseMenu(bool b) {
        pauseCanvas.SetActive(b);

        Time.timeScale = b ? 0 : 1;
    }
}

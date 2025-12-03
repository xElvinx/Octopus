using System.Collections;
using System.Collections.Generic;
using MoreMountains.TopDownEngine;
using UnityEngine;
using MoreMountains.Tools;

public class WinCondition : MonoBehaviour
{
    public List<Character> enemies;
    public GameObject winScreen;
    public GameObject menuScreen;
    public bool enabled;
    public PauseButton PauseButton;

    // Update is called once per frame
    void Update()
    {
        int counter = 0;
        foreach (var enemy in enemies)
        {
            if (!enemy.isActiveAndEnabled)
            {
                counter++;
            }
        }

        if (counter >= enemies.Count)
        {
            Time.timeScale = 0;
            winScreen.SetActive(true);

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PauseButton.PauseButtonAction();
            
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        menuScreen.SetActive(false);
    }

}

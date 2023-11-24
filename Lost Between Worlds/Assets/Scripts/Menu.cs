using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Animator controlAnimations;

    public void StartGame()
    {
        PersistentGameManager manager = GameObject.Find("Game Manager").GetComponent<PersistentGameManager>();

        manager.LoadGame((int)SceneIndexes.TITLE_SCREEN, (int)SceneIndexes.PORTALHUB);
    }

    public void ShowControls()
    {
        controlAnimations.SetBool("show", true);
    }

    public void HideControls()
    {
        controlAnimations.SetBool("show", false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

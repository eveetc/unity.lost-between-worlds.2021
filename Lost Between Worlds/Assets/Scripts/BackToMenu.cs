using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public void Back(){
        PersistentGameManager manager = GameObject.Find("Game Manager").GetComponent<PersistentGameManager>();
        int origin = (int) SceneManager.GetActiveScene().buildIndex;
        int dest = (int)SceneIndexes.TITLE_SCREEN;
        manager.LoadGame(origin, dest);
    }
}

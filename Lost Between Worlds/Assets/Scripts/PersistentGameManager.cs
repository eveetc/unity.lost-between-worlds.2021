using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentGameManager : MonoBehaviour
{
    public static PersistentGameManager instance;
    public GameObject loadingScreen;
    public GameObject scenesGUI;
    public UnityEngine.UI.Image loadingBar;
    public GuiManager GUIManager;
    public GameObject GUIManagerObject;
    public bool loadingScene = false; // trigger is called in update function, so without the flag scenes might be run twice
    public AudioSource loadingAudio;
    public AudioListener loadingAudioListener;

    public void Awake()
    {
        instance = this;
        SceneManager.LoadSceneAsync((int)SceneIndexes.TITLE_SCREEN, LoadSceneMode.Additive);
        GUIManager = GameObject.Find("GUIManagerParent").transform.GetChild(0).GetComponent<GuiManager>();
        GUIManagerObject = GameObject.Find("GUIManagerParent").transform.GetChild(0).gameObject;
        loadingAudio = loadingScreen.GetComponent<AudioSource>();
        loadingAudioListener = loadingScreen.GetComponent<AudioListener>();
    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    public void LoadGame(int fromScene, int toScene)
    {
        loadingBar.fillAmount = 0f; //reset for next load
        scenesGUI.gameObject.SetActive(false);

        loadingScreen.gameObject.SetActive(true);
        StartCoroutine("AnimationLoading");
        StartCoroutine(TriggerTransportAnimations(fromScene, toScene));
    }

    IEnumerator TriggerTransportAnimations(int fromScene, int toScene)
    {
        if (!loadingScene)
        {
            loadingScene = true;
            loadingAudioListener.enabled = true;
            loadingAudio.Play();
            SceneManager.UnloadSceneAsync(fromScene);

            yield return new WaitForSecondsRealtime(2);

            AsyncOperation loadScene = SceneManager.LoadSceneAsync(toScene, LoadSceneMode.Additive);
            yield return null;

            loadScene.completed += (AsyncOperation a) =>
            {
                loadingAudio.Stop();
                SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(toScene));
                GUIManagerObject.SetActive(true);
                GUIManager.UpdatePlayer();
                if (toScene == 3 && fromScene == 2)
                {
                    //dannger planet, diffferent spawn position
                    PlayerInteraction PlayerInteraction = GameObject.Find("Player_Astronaut").GetComponent<PlayerInteraction>();
                    PlayerInteraction.ChangeSpawnPosition();
                }
                if (toScene == 4 && fromScene == 3)
                {
                    ItemWorld.SpawnItemWorld(new Vector3(15, 0.5f, 20), new Item { itemType = Item.ItemType.Medkit, amount = 1, damageOrHealing = 10 });
                    ItemWorld.SpawnItemWorld(new Vector3(10, 0.5f, 24), new Item { itemType = Item.ItemType.Medkit, amount = 1, damageOrHealing = 10 });
                    ItemWorld.SpawnItemWorld(new Vector3(-10, 0.5f, 15), new Item { itemType = Item.ItemType.Medkit, amount = 1, damageOrHealing = 10 });
                    ItemWorld.SpawnItemWorld(new Vector3(-10, 0.5f, 5), new Item { itemType = Item.ItemType.Medkit, amount = 1, damageOrHealing = 10 });


                    //REMOVED adding more than one weapon, which was neccesarry for one assignment. But it does not make sense for the game at this point
                    //int randomWeaponSpawn = Random.Range(2, 10);
                    //for (int i = 0; i < randomWeaponSpawn; i++)
                    //{
                    int randomX = Random.Range(-20, 20);
                    int randomZ = Random.Range(-8, 20);
                    int randomDamage = Random.Range(0, 42);
                    ItemWorld.SpawnItemWorld(new Vector3(randomX, 0.5f, randomZ), new Item { itemType = Item.ItemType.Fazer, amount = 1, damageOrHealing = randomDamage });
                    //}
                }

            };

            loadingScreen.gameObject.SetActive(false);
            loadingAudioListener.enabled = false;



            if (toScene != 6 && toScene != 5 && toScene != 1)
            {
                scenesGUI.gameObject.SetActive(true);
            }
            loadingScene = false;
        }

    }


    // adapted from source: https://forum.unity.com/threads/cant-get-image-fillamount-to-lerp.896660/

    IEnumerator AnimationLoading()
    {
        float duration = 2f;
        float elapsedTime = 0;

        while (loadingBar.fillAmount < 1f)
        {
            loadingBar.fillAmount = Mathf.Lerp(0f, 1f, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

}

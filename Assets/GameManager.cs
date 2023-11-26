using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [SerializeField] bool init;
    public bool gameOver;


    public int lastingBuddiesCount;
    public List<Buddy> lastingBuddies;

    public MeteorSpawner[] meteorSpawners;




    public GameObject levelGO;

    public GameObject gameOverCanvasGO;
    public TextMeshProUGUI gameOverTxt;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void Init(Vector3 position) //init by StartManager
    {


        init = true;


        Debug.Log("INIT GAME MANAGER");


        if (levelGO)
        {
            // DEPLACAGE DU TERRAIN
            levelGO.transform.position = new Vector3(position.x + levelGO.transform.localScale.x / 2, position.y, position.z + levelGO.transform.position.z / 2); //place TABLE
            levelGO.SetActive(true); //show LEVEL
            levelGO.GetComponent<AllTheChildrenAreBelongToDie>().generationDeenfanter();
        }

        lastingBuddies.AddRange(FindObjectsByType<Buddy>(FindObjectsSortMode.None)); //REF & COUNT BUDDIES
        UpdateBuddyCount();


        // HANDLE METEOR HERE
        foreach (MeteorSpawner mSpawner in meteorSpawners)
            mSpawner.StartSpawning();

    }


    private void Start()
    {
        if (levelGO) levelGO.SetActive(false); //hide terrain for startManager

        gameOverCanvasGO.SetActive(false);
    }


    public void KillBuddy(Buddy target)
    {
        if (target != null)
        {
            lastingBuddies.Remove(target);
            target.Kill();
        }


        UpdateBuddyCount();
    }

    void UpdateBuddyCount()
    {
        lastingBuddiesCount = lastingBuddies.Count;

        if (init && lastingBuddiesCount <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("GAME OVER !!!!!!!!");
        gameOver = true;
        gameOverTxt.text = "Game Over !";

        // STOP METEOR SPAWNING
        foreach (MeteorSpawner mSpawner in meteorSpawners)
            mSpawner.StopSpawning();

        Reload();

    }

    public void Win()
    {
        Debug.Log("VICTORY !!!!!!!!");
        gameOverTxt.text = "Victory !";

        // STOP METEOR SPAWNING
        foreach (MeteorSpawner mSpawner in meteorSpawners)
            mSpawner.StopSpawning();

        Reload();

    }

    void Reload()
    {
        gameOverCanvasGO.SetActive(true);
        StartCoroutine(Reloading());
    }

    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    [ContextMenu("Kill all")]
    public void KillAllBuddies()
    {
        // lastingBuddies.AddRange(FindObjectsByType<Buddy>(FindObjectsSortMode.None)); //REF & COUNT BUDDIES

        for (int i = 0; i < lastingBuddies.Count; i++)
        {
            KillBuddy(lastingBuddies[i]);
        }
    }

}

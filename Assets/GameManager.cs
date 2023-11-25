using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int lastingBuddiesCount;
    public List<Buddy> lastingBuddies;
    public bool gameOver;

    public static GameManager Instance;

    public bool init; 

    public GameObject levelGO;

    public GameObject gameOverCanvasGO;

    public TextMeshProUGUI gameOverTxt;

 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void Init(Vector3 position) //init by StartManager
    {
        init = true;

        if (levelGO)
        {
            levelGO.transform.position = new Vector3(position.x + levelGO.transform.localScale.x / 2, position.y, position.z + levelGO.transform.position.z / 2); //place TABLE
            levelGO.SetActive(true); //show LEVEL
            levelGO.GetComponent<AllTheChildrenAreBelongToDie>().generationDeenfanter();
        }
       
        lastingBuddies.AddRange(FindObjectsByType<Buddy>(FindObjectsSortMode.None)); //REF & COUNT BUDDIES
        UpdateBuddyCount();
    }


    private void Start()
    {
        if (levelGO)  levelGO.SetActive(false); //hide terrain for startManager

        gameOverCanvasGO.SetActive(false);
    }


    public void KillBuddy(Buddy target)
    {
        lastingBuddies.Remove(target);
        target.Kill();
        

        UpdateBuddyCount();
    }

    void UpdateBuddyCount()
    {
        lastingBuddiesCount = lastingBuddies.Count;

        if(lastingBuddiesCount <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game is over");
        gameOver = true;
        gameOverTxt.text = "Game Over !";
        Reload();

    }

    public void Win()
    {
        gameOverTxt.text = "Victory !";
        Reload();
        //win
        Debug.Log("Victory !");
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

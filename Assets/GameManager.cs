using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int lastingBuddiesCount;
    public List<Buddy> lastingBuddies;
    public bool gameOver;

    public static GameManager Instance;

    public bool init; 

    public GameObject levelGO;

 

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
        levelGO.transform.position = new Vector3(position.x+levelGO.transform.localScale.x/2, position.y, position.z+levelGO.transform.position.z/2); //place TABLE
        levelGO.SetActive(true); //show LEVEL
        levelGO.GetComponent<AllTheChildrenAreBelongToDie>().generationDeenfanter();
        lastingBuddies.AddRange(FindObjectsByType<Buddy>(FindObjectsSortMode.None)); //REF & COUNT BUDDIES
        UpdateBuddyCount();
    }


    private void Start()
    {
        levelGO.SetActive(false); //hide terrain for startManager
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

    }

    public void Win()
    {
        //win
        Debug.Log("Victory !");
    }

}

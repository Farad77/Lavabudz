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

    public void Init(float tableYPos) //init by StartManager
    {
        init = true;
        levelGO.transform.position = new Vector3(levelGO.transform.position.x, tableYPos, levelGO.transform.position.z); //place TABLE
        levelGO.SetActive(true); //show LEVEL
      
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
        target.removeMembres();
        Destroy(target.gameObject);

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

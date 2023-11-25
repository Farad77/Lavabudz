using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int lastingBuddiesCount;
    public List<Buddy> lastingBuddies;
    public bool gameOver;

    public static GameManager Instance; 

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


    private void Start()
    {
        lastingBuddies.AddRange(FindObjectsByType<Buddy>(FindObjectsSortMode.None));
        UpdateBuddyCount();
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

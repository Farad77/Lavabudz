using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalGoal : MonoBehaviour
{
    public List<Buddy> insideBuddies;
    public int insideBuddyCount;
    public bool victory;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Goal detected stuff");
        if (other.GetComponent<Buddy>() != null)
        {
            Debug.Log("Goal detected buddy ");

            Buddy targetBuddy = other.GetComponent<Buddy>();
            if (!insideBuddies.Contains(targetBuddy))
            {
                insideBuddies.Add(targetBuddy);
                UpdateBuddyCount();
            }
        }
    }

    void UpdateBuddyCount()
    {
        insideBuddyCount = insideBuddies.Count;

        if (GameManager.Instance.lastingBuddiesCount!=0&& insideBuddies.Count!=0&&
            insideBuddies.Count == GameManager.Instance.lastingBuddiesCount)
        {
            //Win
            victory = true;
            GameManager.Instance.Win();
        }
    }
    private void Update()
    {
        UpdateBuddyCount();
    }
}

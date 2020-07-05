﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class NextLevel : MonoBehaviour
{
    public string nextLevel;
    private Scene thisLevel;
    private float elapsedTime;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            AnalyticsEvent.LevelComplete(thisLevel.name, thisLevel.buildIndex);

            Dictionary<string, object> customParams = new Dictionary<string, object>();
            customParams.Add("secondsPlayed", elapsedTime);
            customParams.Add("deathCount", collision.GetComponent<PlayerController>().deathCount);

            Analytics.CustomEvent("LevelFinished", new Dictionary<string, object>
        {
            {"SecondsToComplete", elapsedTime},
            {"NumTimesPlayerDied", collision.GetComponent<PlayerController>().deathCount}
        });

            Debug.Log("Change Level");
            SceneManager.LoadScene(nextLevel);
        }
    }
}
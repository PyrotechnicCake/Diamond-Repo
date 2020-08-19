using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class NextLevel : MonoBehaviour
{
    public string nextLevel;
    private Scene thisLevel;
    private float elapsedTime;
    public GameManager gm;
    private SoundManager sm;

    public Animator levelChanger;
    public int time = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        levelChanger = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Animator>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        sm = GameObject.FindGameObjectWithTag("GM").GetComponent<SoundManager>();
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
            //reset respawn number
            collision.GetComponent<PlayerController>().checkpointNum = 0;
            gm.lastCheckpoint = "StartPos";
            //Analytics
            Analytics.CustomEvent("LevelFinished", new Dictionary<string, object>
        {
            {"LevelName", thisLevel},
            { "SecondsToComplete", elapsedTime},
            {"NumTimesPlayerDied", collision.GetComponent<PlayerController>().deathCount}
        });

            Debug.Log("Change Level");
            LoadNext();
        }
    }
    public void LoadNext()
    {
        if(nextLevel == "EndScene")
        {
            levelChanger.SetTrigger("FadeOutLast");
            StartCoroutine(ChangeTime());
        }
        else
        {
            levelChanger.SetTrigger("FadeOut");
            StartCoroutine(ChangeTime());
        }
    }

    IEnumerator ChangeTime()
    {
        yield return new WaitForSeconds(time);
        if(nextLevel == "Dash L2")
        {
            sm.musicSource.clip = sm.inGameMusicTwo;
            sm.musicSource.Play();
            Debug.Log("playing ingame track 2");
        }
        if(nextLevel == "Glide L2")
        {
            sm.musicSource.clip = sm.themeMusic;
            sm.musicSource.Play();
            Debug.Log("playing theme music");
        }
        SceneManager.LoadScene(nextLevel);
    }
}

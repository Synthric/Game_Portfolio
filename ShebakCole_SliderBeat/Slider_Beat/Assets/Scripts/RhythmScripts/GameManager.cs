using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;
    public bool startPlaying, songPlaying;
    public BeatScroller theBS;
    public Text scoreText;
    public int currentScore; //SCORE
    [SerializeField] public int scorePerNote = 100;
    [SerializeField] public int scorePerGoodNote = 200; //Slide SCORING
    [SerializeField] public int scorePerPerfectNote = 300;
    [SerializeField] public int scorePerSustainedTime = 10;
    public Text multiText; //MULTIPLIER
    public int currentMultiplier, multiplierEffector;
    public int multiplierTracker;
    public int[] multiplierThresholds;
    [SerializeField] public float totalNotes, normalHits, goodHits, perfectHits, missedHits, noteCount;
    public static GameManager instance;
    public GameObject resultsScreen, buttonRow, readyObjTxt, notReadyObjTxt;
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText;
    private float tickTimer, songDelay;
    private int tick;

    // Start is called before the first frame update
    void Start()
    {
        songPlaying = false;

        instance = this;

        readyObjTxt.SetActive(false);

        songDelay = 0;

        scoreText.text = "Score: 0";

        currentMultiplier = 1;

        totalNotes = FindObjectsOfType<NoteObject>().Length + FindObjectsOfType<NoteObject1>().Length; //Indicates how many notes are in NoteHolder, more specifically NoteObject script users

        multiplierEffector = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Me Testing
        tickTimer += Time.deltaTime;
        songDelay += Time.deltaTime;
        //End Testing
        noteCount = normalHits + goodHits + perfectHits + missedHits;
        if (!startPlaying)
        {
            if (songDelay >= 3f)
            {
                notReadyObjTxt.SetActive(false);
                readyObjTxt.SetActive(true);
            }
            if (Input.anyKeyDown && songDelay >= 3f)
            {
                startPlaying = true;
                theMusic.Play();
                theBS.hasStarted=true;
                buttonRow.SetActive(false);
                readyObjTxt.SetActive(false);
                //songPlaying = true;
            }
            // if(songDelay >= 3f && !songPlaying)
            }
            if (noteCount == totalNotes && !resultsScreen.activeInHierarchy) //RESULTS SCREEN
            {
                resultsScreen.SetActive(true);
                normalsText.text = "" + normalHits;
                goodsText.text = goodHits.ToString();
                perfectsText.text = perfectHits.ToString();
                missesText.text = missedHits.ToString();

                float totalHit = normalHits + goodHits + perfectHits;
                float percentHit = (totalHit / totalNotes) * 100f;

                percentHitText.text = percentHit.ToString("F1") + "%";

                string rankVal = "F";

                if(percentHit > 40)
                {//BEGIN RANKING
                    rankVal = "D";
                    if(percentHit > 50)
                    {
                        rankVal = "C";
                        if(percentHit > 70)
                        {
                            rankVal = "B";
                            if(percentHit > 85)
                            {
                                rankVal = "A";
                                if(percentHit > 95)
                                {
                                    rankVal = "S";
                                }
                            }
                        }
                    }
                }//END RANKING
                rankText.text = rankVal;
                finalScoreText.text = currentScore.ToString();
            }
        }

        public void NoteHit()
    {
        //Debug.Log("Hit On Time");
        //multiplier
        if (currentMultiplier - 1 < multiplierThresholds.Length && multiplierEffector == 1)
        {
            multiplierTracker++;
            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker && multiplierEffector == 1)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }
        multiText.text = "Multiplier: x" + currentMultiplier;
        //scoring
        //currentScore += scorePerNote * currentMultiplier;                         //REFERENCE SCORING
        scoreText.text = "Score: " + currentScore;
    }

    public void NormalHit()
    {
        multiplierEffector = 1;
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
        normalHits++;
    }

    public void GoodHit()
    {
        multiplierEffector = 1;
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
        goodHits++;
    }

    public void PerfectHit()
    {
        multiplierEffector = 1;
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
        perfectHits++;
    }

    public void SustainedHit()
    {
        multiplierEffector = 0;
         if (tickTimer > .05f)
            {
            currentScore += scorePerSustainedTime * currentMultiplier;
            NoteHit();
            tickTimer = 0;
            }
    }

    public void NoteMissed()
    {
        //Debug.Log("Missed Note");
        multiplierEffector = 1;
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiText.text = "Multiplier: x" + currentMultiplier;
        missedHits++;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // GameManager -> 싱글톤
    public static GameManager instance { get; set; }

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }

    public float noteSpeed;

    public GameObject scoreUI;
    private float score;
    private Text scoreText;

    public GameObject comboUI;
    private float combo;
    private Text comboText;
    private Animator comboAnimator;

    public enum judges { None = 0, BAD, GOOD, PERFECT, MISS}
    public GameObject judgeUI;
    private Sprite[] judgeSprites;
    private Image judgementSpriteRender;
    private Animator judgementSpriteAnimator;

    public GameObject[] trails;
    private SpriteRenderer[] trailSpriteRenderers;


    void Start()
    {
        judgementSpriteRender = judgeUI.GetComponent<Image>();
        judgementSpriteAnimator = judgeUI.GetComponent<Animator>();
        scoreText = scoreUI.GetComponent<Text>();
        comboText = comboUI.GetComponent<Text>();
        comboAnimator = comboUI.GetComponent<Animator>();

        judgeSprites = new Sprite[4];
        judgeSprites[0] = Resources.Load<Sprite>("Sprites/Bad");
        judgeSprites[1] = Resources.Load<Sprite>("Sprites/Good");
        judgeSprites[2] = Resources.Load<Sprite>("Sprites/Miss");
        judgeSprites[3] = Resources.Load<Sprite>("Sprites/Perfect");

        trailSpriteRenderers = new SpriteRenderer[trails.Length];
        for (int i = 0; i < trails.Length; i++)
        {
            trailSpriteRenderers[i] = trails[i].GetComponent<SpriteRenderer>();
        }
    }

   
    void Update()
    {
        if (Input.GetKey(KeyCode.D)) ShineTrail(0);
        if (Input.GetKey(KeyCode.F)) ShineTrail(1);
        if (Input.GetKey(KeyCode.J)) ShineTrail(2);
        if (Input.GetKey(KeyCode.K)) ShineTrail(3);
        for (int i = 0; i < trailSpriteRenderers.Length; i++)
        {
            Color color = trailSpriteRenderers[i].color;
            color.a -= 0.01f;
            trailSpriteRenderers[i].color = color;
        }
    }

    

    public void ShineTrail(int index)
    {
        Color color = trailSpriteRenderers[index].color;
        color.a = 0.32f;
        trailSpriteRenderers[index].color = color;
    }

    void showJudgement()
    {
        string scoreFormat = "000000";
        scoreText.text = score.ToString(scoreFormat);

        judgementSpriteAnimator.SetTrigger("Show");
        if(combo > 1)
        {
            comboText.text = "COMBO" + combo.ToString();
            comboAnimator.SetTrigger("Show");
        }
    }

    public void processJudge(judges judge, int noteType)
    {
        if (judge == judges.None) return;

        if(judge == judges.MISS)
        {
            judgementSpriteRender.sprite = judgeSprites[2];
            combo = 0;
            if (score >= 100) score -= 100;
        }

        else if(judge == judges.BAD)
        {
            judgementSpriteRender.sprite = judgeSprites[0];
            combo = 0;
            if (score >= 50) score -= 50;
        }

        else if (judge == judges.GOOD)
        {
            judgementSpriteRender.sprite = judgeSprites[1];
            combo = 0;
            score += 20;
        }

        else if (judge == judges.PERFECT)
        {
            judgementSpriteRender.sprite = judgeSprites[3];
            combo += 1;
            score += 200;
            score += combo;
        }

        showJudgement();

    }

}

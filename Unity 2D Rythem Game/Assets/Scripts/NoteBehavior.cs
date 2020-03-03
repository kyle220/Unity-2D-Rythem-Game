﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : MonoBehaviour
{
    public int noteType;
    private GameManager.judges judge;
    private KeyCode KeyCode;

    // Start is called before the first frame update
    void Start()
    {
        if (noteType == 1) KeyCode = KeyCode.D;
        else if (noteType == 2) KeyCode = KeyCode.F;
        else if (noteType == 3) KeyCode = KeyCode.J;
        else if (noteType == 4) KeyCode = KeyCode.K;
    }

    public void Initialize()
    {
        judge = GameManager.judges.None;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * GameManager.instance.noteSpeed);
        if (Input.GetKey(KeyCode))
        {
            GameManager.instance.processJudge(judge, noteType);
            if (judge != GameManager.judges.None) gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if(Other.gameObject.tag == "Bad Line")
        {
            judge = GameManager.judges.BAD;
        }
        else if (Other.gameObject.tag == "Good Line")
        {
            judge = GameManager.judges.GOOD;
        }
        else if (Other.gameObject.tag == "Perfect Line")
        {
            judge = GameManager.judges.PERFECT;
        }
        else if (Other.gameObject.tag == "Miss Line")
        {
            judge = GameManager.judges.MISS;
            gameObject.SetActive(false);
            GameManager.instance.processJudge(judge, noteType);
        }

    }

}

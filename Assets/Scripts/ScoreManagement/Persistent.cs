/************************************************
 *  Author: Leticia
 *  Script to keep the Score object to the Scene
 *  "Game Over". Please attach it to the correct
 *  object, so the score is not destroyed when
 *  loading a new scene.
 ***********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistent : MonoBehaviour
{
    private int _score;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    public void SetPersistScore(int score)
    {
        _score = score;
    }

    public int GetPersistScore()
    {
        return _score;
    }
}

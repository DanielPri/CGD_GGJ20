using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HighScores : MonoBehaviour
{
    HighScoreManager _highScoreManager;
    [SerializeField]
    private Text _scoreText = null;
    [SerializeField]
    private Text _nameText = null;
    // Start is called before the first frame update
    void Start()
    {
        _highScoreManager = GameObject.Find("Canvas").GetComponent<HighScoreManager>();
        _highScoreManager.WriteScores(_nameText, _scoreText);
    }
}

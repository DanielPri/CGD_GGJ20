using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System;

public class HighScoreManager : MonoBehaviour
{
    private string _namePlayer = null;
    [SerializeField] private Text _name = null;
    [SerializeField] private Text _textGameOver = null;
    [SerializeField] private Persistent _persistent = null;
    [SerializeField] private bool _isEndOfGame = false;
    private int _score = 0;

    [Serializable] public struct scores{
        public string name;
        public int score;
        public static bool operator <(scores s1, scores s2)
        {
            return s1.score < s2.score;
        }
        public static bool operator >(scores s1, scores s2)
        {
            return s1.score > s2.score;
        }
        public static bool operator <=(scores s1, scores s2)
        {
            return s1.score <= s2.score;
        }
        public static bool operator >=(scores s1, scores s2)
        {
            return s1.score >= s2.score;
        }
    };


    private List<scores> _highScoreList = new List<scores>();

    private void Start()
    {
        if (_isEndOfGame)
        {
            _persistent = FindObjectOfType<Persistent>();
            if(_persistent != null)
            {
                _score = _persistent.GetPersistScore();
                _textGameOver.text = "Score: " + _persistent.GetPersistScore();
                Destroy(FindObjectOfType<Persistent>().gameObject);
            }
            else
            {
                _textGameOver.text = "You shouldn't be here!";
                _score = 0;
            }
        }
     }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SendHighScore()
    {
        if (_namePlayer != null)
        {
            Load();
            scores newScore = new scores();
            newScore.score = _score;
            newScore.name = string.Copy(_namePlayer);
            _highScoreList.Insert(0, newScore);
            _highScoreList.Sort(CompareScores);
            if (_highScoreList.Count > 10)
            {
                _highScoreList.RemoveAt(0);
            }
            Save();
            SceneManager.LoadScene("ScoreBoard");
        }
    }

    public void SetNamePlayer()
    {
        _namePlayer = string.Copy(_name.text);
        Debug.Log(_namePlayer);
    }

    public int CompareScores(scores s1, scores s2)
    {
        if (s1.score < s2.score)
            return -1;
        else if (s1.score == s2.score)
            return 0;
        else if (s1.score > s2.score)
            return 1;

        return 0;

    }
    public void WriteScores(Text name, Text score)
    {
        if (name == null || score == null)
            return;
         if (Load())
        {
            name.text = "";
            score.text = "";
            for (int i = _highScoreList.Count -1; i >= 0; i--)
            {
                name.text = name.text + _highScoreList[i].name.ToUpper() + "\n";
                score.text = score.text + _highScoreList[i].score + "\n";
            }
        }
         else
        {
            name.text = "NONE!";
            score.text = "";
        }
    }

    public void Save()
    {
        FileStream file = new FileStream (Application.persistentDataPath + "/Scores.dat", FileMode.OpenOrCreate);
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, _highScoreList);
        }
        catch (SerializationException e)
        {
            Debug.Log("Error serializing info! " + e.Message);
        }
        finally
        {
            file.Close();
        }
    }

    public bool Load()
    {
        bool deserialized = true;
        FileStream file = new FileStream(Application.persistentDataPath + "/Scores.dat", FileMode.OpenOrCreate);
        try
        {
        BinaryFormatter formatter = new BinaryFormatter();
        _highScoreList = (List<scores>) formatter.Deserialize(file);
        }
        catch (SerializationException e)
        {
            Debug.Log("Error Deserializng! " + e);
            deserialized = false;
        }
        finally
        {
            file.Close();
        }
        return deserialized;

    }

}

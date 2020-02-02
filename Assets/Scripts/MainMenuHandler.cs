using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/**************************************************
 * Script to handle the buttons in the main menu
 * Mainly methods to load the scenes
 * and one to show/hide the credits
 *************************************************/

public class MainMenuHandler : MonoBehaviour
{
    public void LoadCreditScene()
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("BoatCombiner-George");
    }

    public void LoadInstructionsScene()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void LoadHighScoresScene()
    {
        SceneManager.LoadScene("ScoreBoard");
    }
}

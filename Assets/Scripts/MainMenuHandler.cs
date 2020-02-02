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
    [SerializeField] private GameObject _creditsPanel = null;
    [SerializeField] private GameObject _mainMenuComponents = null;

    public void ShowCredits()
    {
        if (_creditsPanel != null && _mainMenuComponents != null)
        {
            _mainMenuComponents.SetActive(false);
            _creditsPanel.SetActive(true);
        }
    }


    public void HideCredits()
    {
        if (_creditsPanel != null && _mainMenuComponents != null)
        {
            _creditsPanel.SetActive(false);
            _mainMenuComponents.SetActive(true);
        }
    }

    public void LoadGameScene()
    {
        // TODO: Change the name to the correct scene
        // NOT WORKING!
        SceneManager.LoadScene("SampleScene");
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

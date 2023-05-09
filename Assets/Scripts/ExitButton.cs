// By Mihails Fedosejevs

// Connecting main Unity Engine, that allows to use  Unity main classes and functions.
using UnityEngine;

// Connecting Unity Engine User Interface Library. It allows to work with visual elements on the screen (Buttons, inscription).
using UnityEngine.UI;

// Creating a public class 'ExitButton', which is derived from 'MonoBehaviour' class in Unity.
public class ExitButton : MonoBehaviour
{
    // Code that should start running at the scene's start is added in the 'Start' method block.
    void Start()
    {
        // Creating Button data type variable calling 'button'. Assign it to current game object.
        Button button = GetComponent<Button>();

        // When button is clicked 'QuitGame' method is running 
        button.onClick.AddListener(QuitGame);
    }
    
    // 'QuitGame' method.
    void QuitGame()
    {
        // When method is running, the game is quitting.
        Application.Quit();
    }
}
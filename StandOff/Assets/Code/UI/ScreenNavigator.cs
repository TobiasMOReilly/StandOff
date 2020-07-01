using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class is responsible for the users navigation on menu screens
/// </summary>
public class ScreenNavigator : MonoBehaviour
{
    #region Inspector Variables
    [Header("Screens")]

    [SerializeField]
    [Tooltip("Link to the Game screen")]
    private GameObject GameScreen;

    [SerializeField]
    [Tooltip("Link to the Game screen")]
    private GameObject InGameMenuScreen;

    [SerializeField]
    [Tooltip("Link to the Main menu screen")]
    private GameObject MainScreen;

    [SerializeField]
    [Tooltip("Link to the Settings screen")]
    private GameObject SettingsScreen;

    [SerializeField]
    [Tooltip("Link to the Character Customization screen")]
    private GameObject CharacterScreen;

    [SerializeField]
    [Tooltip("Link to the Credits screen")]
    private GameObject CreditsScreen;

    [SerializeField]
    [Tooltip("Link to the Join Game screen")]
    private GameObject JoinGameScreen;

    [SerializeField]
    [Tooltip("Link to the Join Game screen")]
    private GameObject ServerSettingsScreen;

    [SerializeField]
    [Tooltip("Link to the Navigation overlay")]
    private GameObject ScreenNavigation;
    #endregion

    #region Private Variables
    //Allows the tracking of screen movement
    private List<GameObject> previousScreens;
    private GameObject currentScreen;
    #endregion

    #region Game Loop Methods
    private void Awake()
    {
        InitScreens();
    }
    #endregion

    #region Navigation Methods
    /// <summary>
    /// Used to initiate the screens on start
    /// </summary>
    private void InitScreens()
    {
        previousScreens = new List<GameObject>();

        previousScreens.Add(MainScreen);

        currentScreen = previousScreens[0];

        currentScreen.SetActive(true);
    }

    /// <summary>
    /// Used to switch between screens.
    /// Can be used on any button that leads to another screen.
    /// </summary>
    /// <param name="targetScreen">The screen to change to</param>
    public void ChangeScreen(GameObject targetScreen)
    {
        //Add current screen to previous screen list
        if (currentScreen != MainScreen)
        {
            previousScreens.Add(currentScreen);
        }
        //dissable current screen
        currentScreen.SetActive(false);
        //set current screen to target screen
        currentScreen = targetScreen;
        //enable current screen
        currentScreen.SetActive(true);
        //Enable navigation
        //ScreenNavigation.SetActive(true);
        ////Check if screen navigation should be disabled
        ToggleScreenNavigation();

        //Log analytics
        AnalyticsManager.ScreenView(currentScreen.name);
    }

    /// <summary>
    /// Used to navigate back through the screens
    /// </summary>
    public void Back()
    {
        //Disable current screen
        currentScreen.SetActive(false);
        //Enable previous screen
        previousScreens[previousScreens.Count - 1].SetActive(true);
        //set current screen
        currentScreen = previousScreens[previousScreens.Count - 1];
        //Set previous screen
        if (previousScreens[previousScreens.Count - 1] != MainScreen)
        {
            previousScreens.RemoveAt(previousScreens.Count - 1);
        }

        ToggleScreenNavigation();
    }

    /// <summary>
    /// Used to check if the sceen navigation should be hidden
    /// </summary>
    private void ToggleScreenNavigation()
    {
        if (currentScreen == MainScreen || currentScreen == GameScreen)
        {
            ScreenNavigation.SetActive(false);
        }
        else
        {
            ScreenNavigation.SetActive(true);
        }
    }

    /// <summary>
    /// Allows for a particulare screen to be activated 
    /// without modifying current screen navigation state.
    /// IE Using the ingame menu
    /// </summary>
    /// <param name="targetScreen">Screen to be toggled</param>
    public void ToggleScreen(GameObject targetScreen)
    {
        targetScreen.SetActive(!targetScreen.activeSelf);
    }

    /// <summary>
    /// Allows the user to exit the game.
    /// trigger analytics to be stored
    /// </summary>
    public void Quit()
    {
        AnalyticsManager.Quit(Time.realtimeSinceStartup);
        Application.Quit();
    }
    #endregion

}

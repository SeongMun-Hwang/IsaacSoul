using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuCanvas : MonoBehaviour
{
    public TextMeshProUGUI retryText;
    public TextMeshProUGUI giveUpBText;
    public List<Button> buttons;
    int buttonIndex = 0;
    Color originColor = new Color(50 / 255f, 50 / 255f, 50 / 255f);
    Color redColor = new Color(209 / 255f, 56 / 255f, 56 / 255f);
    Color greenColor = new Color(56 / 255f, 209 / 255f, 56 / 255f);
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            buttonIndex = buttonIndex == 0 ? 1 : 0;
        }
        if (buttonIndex == 0)
        {
            retryText.color = greenColor;
            giveUpBText.color = originColor;
        }
        else
        {
            giveUpBText.color = redColor;
            retryText.color = originColor;
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            buttons[buttonIndex].onClick.Invoke();
        }
    }
    public void StartButton()
    {
        SceneManager.LoadScene("MainGame");
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}

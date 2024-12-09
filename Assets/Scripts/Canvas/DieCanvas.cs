using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DieCanvas : MonoBehaviour
{
    public TextMeshProUGUI retryText;
    public TextMeshProUGUI giveUpBText;
    public List<Button> buttons;
    int buttonIndex = 0;
    Color originColor = new Color(50 / 255f, 50 / 255f, 50 / 255f);
    Color redColor = new Color(209 / 255f, 56 / 255f, 56 / 255f);
    Color greenColor = new Color(56 / 255f, 209 / 255f, 56 / 255f);
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
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
    public void Retry()
    {
        SceneManager.LoadScene("MainGame");
    }
    public void GiveUp()
    {
        Application.Quit();
    }
    private void OnEnable()
    {
        Time.timeScale = 0f;
    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}

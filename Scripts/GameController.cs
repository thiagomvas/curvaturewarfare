using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameController : MonoBehaviour
{
    public int points;
    [SerializeField] private TextMeshProUGUI scoreTMpro;
    [SerializeField] private TextMeshProUGUI hsTMpro;

    private int prevHS;
    // Start is called before the first frame update
    void Start()
    {
        prevHS = PlayerPrefs.GetInt("HighScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreTMpro != null) scoreTMpro.text = "Score: " + points;
        if (points > prevHS) PlayerPrefs.SetInt("HighScore", points);
        if (hsTMpro != null) hsTMpro.text = "High score: " + prevHS;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void OnApplicationQuit()
    {
        if (points > prevHS) PlayerPrefs.SetInt("HighScore", points);
    }
}

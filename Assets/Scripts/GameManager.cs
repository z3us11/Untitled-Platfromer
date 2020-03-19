using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  //  public static GameManager gameManager;
    public bool gameStart;
    public GameObject SpawnManager;
    [SerializeField] GameObject m_GameOverPane;
    [SerializeField] Button m_RestartBtn;
    [SerializeField] TextMeshProUGUI UI;
    [SerializeField] GameObject[] players;
    // Start is called before the first frame update
    void Start()
    {
        //  gameManager = this; ;
        Time.timeScale = 1f;
        m_RestartBtn.onClick.AddListener(() => Restart());
        StartCoroutine(Count());
    }

    IEnumerator Count()
    {
        m_GameOverPane.SetActive(true);
        UI.text = "3";
        yield return new WaitForSeconds(1);
        UI.text = "2";
        yield return new WaitForSeconds(1);
        UI.text = "1";
        yield return new WaitForSeconds(1);
        m_GameOverPane.SetActive(false);
        StartGame();
    }

   void StartGame()
    {
        players[0].SetActive(true);
        players[1].SetActive(true);

        gameStart = true;
        SpawnManager.SetActive(true);
    }
    public void GameOver(string Text)
    {
        UI.text = Text + " Wins";
        m_RestartBtn.gameObject.SetActive(true);
        m_GameOverPane.SetActive(true);
    }
    void Restart()
    {
        SceneManager.LoadScene("GameScene");
    }
}

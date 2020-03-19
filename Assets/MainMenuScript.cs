using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject manager;
    [SerializeField] GameObject gamevoerPanel;
    [SerializeField] Button play;
    // Start is called before the first frame update
    void Start()
    {
        play.onClick.AddListener(() => Playgame());
    }

    // Update is called once per frame
    void Playgame()
    {
        manager.SetActive(true);
        gamevoerPanel.SetActive(true);
        this.gameObject.SetActive(false);
    }
}

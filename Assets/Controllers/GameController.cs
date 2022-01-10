using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;
    // Start is called before the first frame update
    void Start()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void winGame() {
        winPanel.gameObject.SetActive(true);
    }

    public void loseGame() {
        losePanel.gameObject.SetActive(true);
    }

    public void restartGame() {
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
    }
}


using UnityEngine;

using UnityEngine.SceneManagement;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameData gameData;
    [SerializeField] TMP_Text percentageText;
    [SerializeField] GameObject winFX;
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject inGameMenu;
    [SerializeField] GameObject winMenu;
    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            startMenu.SetActive(true);
            inGameMenu.SetActive(false);
            winMenu.SetActive(false);
        }
        else
        {
            startMenu.SetActive(false);
            inGameMenu.SetActive(true);
            winMenu.SetActive(false);
        }
        gameData.percentage = 0;
    }
    public void ShowGameState()
    {


        if (gameData.percentage > 50)
        {
            OnWin();
        }
        else
        {
            Onllose();
        }

    }
    public void GetScene(int index)
    {
        SceneManager.LoadScene(index);
    }


    private void OnWin()
    {
        inGameMenu.SetActive(false);
        winMenu.SetActive(true);
        Instantiate(winFX, new Vector3(4, 0, -8), Quaternion.identity);
        percentageText.text = "YOU WON!\n" + gameData.percentage.ToString();
    }
    private void Onllose()
    {
        inGameMenu.SetActive(false);
        winMenu.SetActive(true);
        percentageText.text = "YOU lost!\n" + gameData.percentage.ToString();
    }

}





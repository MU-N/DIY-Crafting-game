using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject winFX;

    private void CheckGameState()
    {

    }

    private void OnWin()
    {
        
        Instantiate(winFX, transform.position, Quaternion.identity);
        FindObjectOfType<AudioManager>().Play("Win");
    }
}

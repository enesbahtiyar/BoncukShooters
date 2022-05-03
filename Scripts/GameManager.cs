using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _İsGameOver;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && _İsGameOver == true)
        {
            SceneManager.LoadScene(1); //Current Game Scene
        }
    }

    public void GameOver()
    {
        _İsGameOver = true;
    }


}

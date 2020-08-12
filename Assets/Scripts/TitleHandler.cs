using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleHandler : MonoBehaviour
{

    public AudioClip selectSound;
    public void StartGame()
    {
        AudioSource.PlayClipAtPoint(selectSound, new Vector3(0,0,0));
        SceneManager.LoadScene(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void startGame()
    {
        StartCoroutine(loadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator loadScene(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

}

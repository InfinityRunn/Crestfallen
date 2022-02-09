using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [Header("Scene To Load")] //Input the build index for the scene you are trying to load :)
    [SerializeField] private int buildIndex;
    public float transistionTime = 0.1666667f;
    [SerializeField] private Collider2D doorway;
    [SerializeField] private Collider2D player;

    public Animator transition;

    void Update()
    {
        if (Input.GetButtonDown("Interact") && player.IsTouching(doorway))
        {
            LoadNextScene();
        }
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadScene(buildIndex));
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        //Play animation
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(transistionTime);

        //Load scene
        SceneManager.LoadScene(sceneIndex);

    }
}

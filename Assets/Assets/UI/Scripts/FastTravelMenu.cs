using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FastTravelMenu : MonoBehaviour
{
    public bool fastTravelMenuOpen = false;
    [Header("Needed To Open Menu")] //Mark where the bar is, what the player is, and what the fast travel ui is.
    [SerializeField] private Collider2D barInteract;
    [SerializeField] private Collider2D player;
    [SerializeField] private GameObject fastTravelMenuUI;
    public Animator transition;
    private float transitTime;

    private void Start()
    {
        transitTime = GameObject.Find("LevelLoader").GetComponent<LevelLoader>().transistionTime;
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact") && player.IsTouching(barInteract))
        {
            OpenMenu();
        }

        if (Input.GetButtonDown("Cancel") && fastTravelMenuOpen)
        {
            Resume();
        }
    }
    public void Resume()
    {
    fastTravelMenuUI.SetActive(false);
    Time.timeScale = 1f;
    fastTravelMenuOpen = false;
    }

    void OpenMenu()
    {
    fastTravelMenuUI.SetActive(true);
    Time.timeScale = 0f;
    fastTravelMenuOpen = true;
    }

    IEnumerator FastTravel(int travelIndex)
    {
        //Play animation
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(transitTime);

        //Load scene
        SceneManager.LoadScene(travelIndex);
    }

    public void BarRockBottom()
    {
        StartCoroutine(FastTravel(1));
    }

    public void Bar2()
    {
        StartCoroutine(FastTravel(3));
    }
}

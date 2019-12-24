using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public Transform[] canvas;
    public int currentIndex;
    public Transform advanceButton;
    public Transform retreatButton;

    // Update is called once per frame
    void Update()
    {
        if (currentIndex == 0)
        {
            retreatButton.gameObject.SetActive(false);
        }
        else if (currentIndex == canvas.Length - 1)
        {
            advanceButton.gameObject.SetActive(false);
        }
        else
        {
            retreatButton.gameObject.SetActive(true);
            advanceButton.gameObject.SetActive(true);
        }
    }
    public void Forward()
    {
        currentIndex++;
        ShowCanvas();
    }
    public void Backward()
    {
        currentIndex--;
        ShowCanvas();
    }
    public void GoHome()
    {
        SceneManager.LoadScene("start");
    }

    private void ShowCanvas()
    {
        for (var i = 0; i < canvas.Length; i++)
        {
            canvas[i].gameObject.SetActive(currentIndex == i);
        }

    }
}

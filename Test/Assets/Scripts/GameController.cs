using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private int _correctlyPlacedCounter = 0;
    private int _nbShapesToPlace = 0;

    // Start is called before the first frame update
    void Start()
    {
        var shapeArray = FindObjectsOfType<MoveableShape>();
        _nbShapesToPlace = shapeArray.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlacedCorrectly()
    {
        if (++_correctlyPlacedCounter >= _nbShapesToPlace)
            LevelCompleted();
    }

    void LevelCompleted()
    {
        Debug.Log("Level Completed");
        // Call a static class and set the information, so that it is persistent to the next scene
        // LevelCompletionClass.SetMiniGameName("DragAndDrop")
        // LevelCompletionClass.SetScore("100%");
        // LevelCompletionClass.SetLevel(1);
        SceneManager.LoadScene("LevelCompletionScreen");
    }
}

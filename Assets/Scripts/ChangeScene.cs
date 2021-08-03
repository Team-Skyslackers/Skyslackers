using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string target_scene;
    public bool isSingle;

    public void NextScene()
    {
        if (isSingle) 
            SongManager.scenename = "game";
        else 
            SongManager.scenename = "game2";
        SceneManager.LoadScene(target_scene);
    }
}

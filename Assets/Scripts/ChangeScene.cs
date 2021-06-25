using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string target_scene;

    public void NextScene()
    {
        SceneManager.LoadScene(target_scene);
        Generate.myScore = 0;//reset scores & combo
        Generate.myCombo = 0;
    }
}

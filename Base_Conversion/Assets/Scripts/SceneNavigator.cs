using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    public void LoadTargetScene(int targetScene)
    {
        SceneManager.LoadScene(targetScene);
    }
}

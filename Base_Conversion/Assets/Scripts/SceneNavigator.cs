using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class SceneNavigator : MonoBehaviour
{
    public void LoadTargetScene(int targetScene)
    {
        SceneManager.LoadScene(targetScene);
    }
}

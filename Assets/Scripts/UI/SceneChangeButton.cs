using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChangeButton : MonoBehaviour
{
    [SerializeField] private int indexSceneToLoad = 0;
    public void OnClick()
    {
        SceneManager.LoadScene(indexSceneToLoad);
    }
}

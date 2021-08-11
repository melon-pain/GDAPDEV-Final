using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private string sceneToLoad;

    private void Start()
    {
        fadeImage.canvasRenderer.SetAlpha(0.0f);
    }

    public void LoadGame()
    {
        fadeImage.CrossFadeAlpha(1 , 0.25f, false);
        Invoke("LoadScene", 1.0f);

        Transform parent = this.transform;

        for (int i = 0; i < parent.childCount; i++)
        {
            if (this.gameObject != parent.GetChild(i))
                Destroy(gameObject, 1.0f);
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}

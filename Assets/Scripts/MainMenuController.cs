using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtVersion;
    void Start()
    {
        txtVersion.text = string.Format("v(Alfa): {0}", Application.version);
    }

    public void Btn_NewGame()
    {
        SceneManager.LoadScene("test_game");
    }
}

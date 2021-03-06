using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_GameController : MonoBehaviour
{
    [Header("Info Jugador")]
    [SerializeField] private TextMeshProUGUI txtHP_Player;
    [SerializeField] private Slider sliderHP_Player;

    [Header("Info Enemigo")]
    [SerializeField] private TextMeshProUGUI txtNameEnemy;

    [Space]
    [SerializeField] private TextMeshProUGUI txtHP_Enemy;
    [SerializeField] private Slider sliderHP_Enemy;

    [Header("Panel Game Over")]
    [SerializeField] private Animator animPanelGameOver;

    [Header("Versión del juego")]
    [SerializeField] private TextMeshProUGUI txtVersion;

    public static UI_GameController Instance { get; set; }

    private void Awake()
    {
        Instance = this;

        txtVersion.text = string.Format("v(Alfa): {0}", Application.version);
    }

    public void ShowInfoEnemy(EnemyStats_SO _stats)
    {
        txtNameEnemy.text = string.Format("{0} (Lvl. {1})", _stats.nameEnemy, _stats.level);
        txtHP_Enemy.text = string.Format("{0} / {0}", _stats.hpMax);

        sliderHP_Enemy.value = sliderHP_Enemy.maxValue = _stats.hpMax;
    }

    public void UpdateInfoEnemy(float _hpEnemy, EnemyStats_SO _stats)
    {
        txtHP_Enemy.text = string.Format("{0} / {1}", _hpEnemy, _stats.hpMax);

        sliderHP_Enemy.value = _hpEnemy;
    }

    public void ShowHealthBarPlayer(PlayerStats_SO _stats)
    {
        txtHP_Player.text = string.Format("{0} / {0}", _stats.hpMax);

        sliderHP_Player.maxValue = sliderHP_Player.value = _stats.hpMax;
    }

    public void UpdateHealthBarPlayer(float _hpPlayer, PlayerStats_SO _stats)
    {
        txtHP_Player.text = string.Format("{0} / {1}", _hpPlayer, _stats.hpMax);

        sliderHP_Player.maxValue = sliderHP_Player.value = _stats.hpMax;
        sliderHP_Player.value = _hpPlayer;
    }

    public void Btn_GoMainMenu()
    {
        SceneManager.LoadScene("main_menu");
    }

    public void Btn_ResetScene()
    {
        SceneManager.LoadScene("test_game");
    }

    public void Btn_Options()
    {

    }

    public void PlayerDead()
    {
        animPanelGameOver.SetTrigger("isDead");
    }
}

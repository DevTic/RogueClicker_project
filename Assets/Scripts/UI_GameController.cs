using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_GameController : MonoBehaviour
{
    [Header("Info Enemigo")]
    [SerializeField] private TextMeshProUGUI txtNameEnemy;

    [Space]
    [SerializeField] private TextMeshProUGUI txtHP_Enemy;
    [SerializeField] private Slider sliderHP_Enemy;

    public static UI_GameController Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    public void ShowInfoEnemy(EnemyStats_SO _stats)
    {
        txtNameEnemy.text = string.Format("{0} (Lvl. {1})", _stats.nameEnemy, _stats.level);
        txtHP_Enemy.text = string.Format("{0} / {0}", _stats.hpMax);

        sliderHP_Enemy.maxValue = sliderHP_Enemy.value = _stats.hpMax;
    }

    public void UpdateInfoEnemy(float _hpEnemy, EnemyStats_SO _stats)
    {
        txtHP_Enemy.text = string.Format("{0} / {1}", _hpEnemy, _stats.hpMax);

        sliderHP_Enemy.value = _hpEnemy;
    }

    public void Btn_Options()
    {

    }
}

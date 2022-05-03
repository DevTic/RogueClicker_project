using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyStats_SO[] enemyStats;
    [SerializeField] private SpriteEffectsController spriteEffectsEnemy;
    [SerializeField] private Animator animEnemy;

    [SerializeField] private float hpCurrent;
    [SerializeField] private float hpMax;

    [HideInInspector] public bool isDead;

    private int currentEnemy;

    public static EnemyController Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        isDead = false;
        InitializeNewEnemy();
    }

    private void Start()
    {
        AutoAtack();
        //SetInfoStatsEnemy();
    }

    private void InitializeNewEnemy()
    {
        currentEnemy = SelectRandomEnemy();
        animEnemy.runtimeAnimatorController = enemyStats[currentEnemy].animatorController;

        SetInfoStatsEnemy();
        isDead = false;
    }

    private int SelectRandomEnemy()
    {
        return Random.Range(0, enemyStats.Length);
    }

    public void SetInfoStatsEnemy()
    {
        hpCurrent = hpMax = enemyStats[currentEnemy].hpMax;

        UI_GameController.Instance.ShowInfoEnemy(enemyStats[currentEnemy]);
        spriteEffectsEnemy.ShowEffect_Appear();
    }

    // Empieza a ejecutarse una vez se inicialice la instancia el enemigo
    public void AutoAtack()
    {
        StartCoroutine(DelayAtack());
    }

    IEnumerator DelayAtack()
    {
        while (!isDead && !PlayerController.Instance.isDead)
        {
            yield return new WaitForSeconds(enemyStats[currentEnemy].atkInterval);

            // control para evitar propinarle un golpe cuando ya esta muerto
            if (!isDead && !PlayerController.Instance.isDead)
            {
                PlayerController.Instance.PlayerTakingDamage(enemyStats[currentEnemy].damage);
                GameController.Instance.ShowFloatingText(enemyStats[currentEnemy].damage, false);
            }
        }
    }

    // Enemigo recibiendo daño
    public void EnemyTakingDamage(float damage, bool _critic = false)
    {
        hpCurrent -= damage;

        PlayerController.Instance.PlayerAttack(_critic);

        if (_critic)
            spriteEffectsEnemy.ShowEffect_TintSprite(new Color(1f, .7f, .7f, .5f));
        else
            spriteEffectsEnemy.ShowEffect_TintSprite(new Color(1f, .7f, .7f, .2f));

        if (hpCurrent <= 0)
        {
            hpCurrent = 0;
            Dead();
        }
        UI_GameController.Instance.UpdateInfoEnemy(hpCurrent, enemyStats[currentEnemy]);
    }

    // Muerte del enemigo
    public void Dead()
    {
        isDead = true;

        spriteEffectsEnemy.ShowEffect_Dissolve();
        InitializeNewEnemy();
    }
}

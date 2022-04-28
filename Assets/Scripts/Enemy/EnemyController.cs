using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyStats_SO enemyStats;
    [SerializeField] private SpriteEffectsController spriteEffectsEnemy;

    [SerializeField] private float hpCurrent;
    [SerializeField] private float hpMax;

    public bool isDead;

    public static EnemyController Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        isDead = false;
    }

    private void Start()
    {
        AutoAtack();
        SetInfoStatsEnemy();
    }

    public void SetInfoStatsEnemy()
    {
        hpCurrent = hpMax = enemyStats.hpMax;

        UI_GameController.Instance.ShowInfoEnemy(enemyStats);
        spriteEffectsEnemy.ShowEffect_Appear();
    }

    // Empieza a ejecutarse una vez se inicialice la instancia el enemigo
    public void AutoAtack()
    {
        StartCoroutine(DelayAtack());
    }

    IEnumerator DelayAtack()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(enemyStats.atkInterval);

            // control para evitar propinarle un golpe cuando ya esta muerto
            if (!isDead)
            {
                PlayerController.Instance.PlayerTakingDamage(enemyStats.damage);
                GameController.Instance.ShowFloatingText(enemyStats.damage, false);
            }
        }
    }

    // Enemigo recibiendo daño
    public void EnemyTakingDamage(float damage, bool _critic = false)
    {
        hpCurrent -= damage;

        if (_critic)
            spriteEffectsEnemy.ShowEffect_TintSprite(new Color(1f, .7f, .7f, .3f));
        else
            spriteEffectsEnemy.ShowEffect_TintSprite(new Color(1f, .7f, .7f, .1f));

        if (hpCurrent <= 0)
        {
            hpCurrent = 0;
            Dead();
        }
        UI_GameController.Instance.UpdateInfoEnemy(hpCurrent, enemyStats);
    }

    // Muerte del enemigo
    public void Dead()
    {
        isDead = true;

        // definir que hacer cuando muere ............
        spriteEffectsEnemy.ShowEffect_Dissolve();
    }
}

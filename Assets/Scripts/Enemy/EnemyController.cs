using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyStats_SO enemyStats;

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
        SpriteEffectsController.Instance.ShowEffect_Appear();
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
    public void EnemyTakingDamage(float damage)
    {
        hpCurrent -= damage;

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
        SpriteEffectsController.Instance.ShowEffect_Dissolve();
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Stats", menuName = "Enemy Stats")]
public class EnemyStats_SO : ScriptableObject
{
    public enum TypeEnemy
    {
        Slime,
        Warrior,
        Mage,
        Beast
    }

    [Header("General")]
    public string nameEnemy;
    public float hpMax;
    public TypeEnemy typeEnemy;

    [Header("Parametros de daño al jugador")]
    [Tooltip("Daño normal del enemigo.")]
    [Range(0f, 50f)]
    public float damage = 0f;
    [Tooltip("Establece el intervalo de tiempo entre cada ataque del enemigo.")]
    [Range(0.1f, 5f)]
    public float atkInterval = 0.1f;
    
    [Space]
    [Tooltip("Establece si el enemigo aplica daño pasivo por envenenamiento o sangrado.")]
    public float damagePoison;
    [Tooltip("Establece la duración del envenenamiento o sangrado.")]
    public float durPoison;

    [Space]
    [Range(1, 100)]
    public int level = 1;

    private void Awake()
    {
        //level = 1;
    }
}

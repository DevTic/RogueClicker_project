using UnityEngine;

[CreateAssetMenu(fileName = "New Player Stats", menuName = "Player Stats")]
public class PlayerStats_SO : ScriptableObject
{
    [Header("General")]
    public string nameHero;            // nombre del jugador
    public int hpMax;                  // puntos de vida máximo
    public int def;                    // puntos de defensa

    [Header("Daño del jugador")]
    [Range(0, 9999)]
    public int damage;                 // daño base del jugador
    [Range(0f, 99.9f)]
    public float probCritical;         // probabilidad de realizar un golpe critico

    [Header("Regeneración de vida")]
    [Range(0, 50)]
    public int regenAmount;            // cantidad de puntos de regeneración de vida
    [Range(1f, 10f)]
    public float regenIntervalTime;    // intervalo de tiempo para regenerar la vida

    [Space]
    [Range(1, 100)]
    public int level;                  // nivel del jugador

    private void Awake()
    {
        level = 1;
    }
}

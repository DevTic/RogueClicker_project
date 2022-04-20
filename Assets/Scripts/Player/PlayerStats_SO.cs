using UnityEngine;

[CreateAssetMenu(fileName = "New Player Stats", menuName = "Player Stats")]
public class PlayerStats_SO : ScriptableObject
{
    [Header("General")]
    public float hpMax;

    [Header("Daño del jugador")]
    public float damage;
    public float probCritical;

    [Space]
    [Range(1, 100)]
    public int level;

    private void Awake()
    {
        level = 1;
    }
}

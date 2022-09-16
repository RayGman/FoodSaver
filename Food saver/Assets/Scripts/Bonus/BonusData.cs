using UnityEngine;

[CreateAssetMenu(menuName = "Bonus", fileName = "New Food")]
public class BonusData : ScriptableObject
{
    [Tooltip("Sprite")]
    [SerializeField] private Sprite mainSprite;
    public Sprite MainSprite
    {
        get { return mainSprite; }
        protected set { }
    }

    [Tooltip("Speed")]
    [SerializeField] private float speed;
    public float Speed
    {
        get { return speed; }
        protected set { }
    }

    [Tooltip("Bonus point")]
    [SerializeField] private int bonusPoint;
    public int BonusPoint
    {
        get { return bonusPoint; }
        protected set { }
    }
}

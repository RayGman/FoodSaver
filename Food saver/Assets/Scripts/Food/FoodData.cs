using UnityEngine;

[CreateAssetMenu(menuName = "Food", fileName = "New Food")]
public class FoodData : ScriptableObject
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

    [Tooltip("Damage")]
    [SerializeField] private int damage;
    public int Damage
    {
        get { return damage; }
        protected set { }
    }
}

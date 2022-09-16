using System;
using UnityEngine;

public class Food : MonoBehaviour
{
    public static Action<GameObject> OnFoodOverFly;

    private FoodData data;
    private FoodChallenge challenge;

    private float destroyDistance, speed;
    private int typeDestroyed; // typeDestroyed = 1-упал, 2-клик
    string myName;
    int myDamage;

    [SerializeField] private SpriteRenderer mySprite;

    private void Start()
    {
        challenge = FoodChallenge.foodChallenge;
        destroyDistance = -4.9f;
        speed = 0.1f;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.down * speed);

        if (transform.position.y < destroyDistance && OnFoodOverFly != null)
        {
            typeDestroyed = 1;
            DestroyFood();
        }
    }

    public void Init(FoodData _data)
    {
        data = _data;
        speed = data.Speed;
        
        mySprite.sprite = data.MainSprite;
        
        myName = data.name;
        myDamage = data.Damage;
    }

    private void OnMouseDown()
    {
        typeDestroyed = 2;
        DestroyFood();
    }

    private void DestroyFood()
    {
        OnFoodOverFly?.Invoke(gameObject);
        challenge.DestroyedFood(myName, myDamage, typeDestroyed);       
    }
}

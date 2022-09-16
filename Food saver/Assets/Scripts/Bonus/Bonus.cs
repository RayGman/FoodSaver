using UnityEngine;

public class Bonus : MonoBehaviour
{
    public delegate void ChangePoint(int value);
    public event ChangePoint bonusHealth;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Coin coinScript;

    private BonusData data;

    private float destroyDistance;

    private void Start()
    {
        destroyDistance = -4.9f;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.down * data.Speed);

        if (transform.position.y < destroyDistance)
        {
            DestroyFood();
        }
    }

    public void Init(BonusData _data)
    {
        data = _data;
        GetComponent<SpriteRenderer>().sprite = data.MainSprite;
    }

    private void OnMouseDown()
    {
        if (data.name == "Coin")
        {
            coinScript.ChangeCoin(data.BonusPoint);
        }
        if (data.name == "Health")
        {
            bonusHealth?.Invoke(data.BonusPoint);
        }
        DestroyFood();
    }

    private void DestroyFood()
    {
        gameObject.SetActive(false);
        gameObject.transform.position = spawnPoint.position;
    }
}

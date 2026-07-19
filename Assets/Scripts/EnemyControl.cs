using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public int hp = 10;
    public GameObject bombEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetHit(int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Instantiate(bombEffect,transform.position,transform.rotation);
            Destroy(gameObject);
        }
    }
}

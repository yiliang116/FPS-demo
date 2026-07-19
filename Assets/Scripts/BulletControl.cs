using Unity.VisualScripting;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public float speed = 30;
    public GameObject effectPrefab;
    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag ==  "Des")
        {
            Rigidbody rbody = collision.gameObject.GetComponent<Rigidbody>();
            if(rbody == null)
            {
                rbody = collision.gameObject.AddComponent<Rigidbody>();
            }
            rbody.AddForceAtPosition(transform.forward * 50, collision.contacts[0].point, ForceMode.Impulse);
            Destroy(collision.gameObject, 2f);
        }
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyControl>().GetHit(2);
        }
        var go = Instantiate(effectPrefab, transform.position, Quaternion.LookRotation(collision.contacts[0].normal));
        Destroy(go, 1f);
        Destroy(gameObject);
    }
}

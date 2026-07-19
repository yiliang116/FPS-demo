using JetBrains.Annotations;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    public GameObject FirePoint;
    public GameObject BulletPre;
    public GameObject FirePre;
    public float bulletinterval = 0.1f;
    private float timer = 0f;
    private PlayerControl pc;
    private RecoilControl rc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pc = GetComponent<PlayerControl>();
        rc = GetComponent<RecoilControl>();

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(Input.GetMouseButton(0) && timer >= bulletinterval && !pc.highSpeed)
        {
            timer = 0;
            rc.Fire();
            Instantiate(BulletPre, FirePoint.transform.position, FirePoint.transform.rotation);
            Destroy(Instantiate(FirePre, FirePoint.transform.position, FirePoint.transform.rotation), 0.1f);
        }
    }
}

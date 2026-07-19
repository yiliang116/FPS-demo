using System;
using UnityEngine;

public class RecoilControl : MonoBehaviour
{
    public float X = -3f;
    public float speed = 10f;
    public float returnSpeed = 5f;

    private float targetRotation;
    private float currentRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Update is called once per frame
    void Update()
    {
        targetRotation = Mathf.Lerp(targetRotation, 0, returnSpeed * Time.deltaTime);
        currentRotation = Mathf.Lerp(currentRotation, targetRotation, speed * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation, transform.localEulerAngles.y, 0); 
    }

    public void Fire()
    {
        targetRotation += X;

    }
}

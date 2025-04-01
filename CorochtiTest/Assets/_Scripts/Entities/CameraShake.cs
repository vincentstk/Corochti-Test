using System;
using UnityEngine;
using System.Collections;
using Hiraishin.ObserverPattern;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    #region Component Configs

    [SerializeField]
    private float shakeDuration = 0.5f;

    [SerializeField]
    private float shakeMagnitude = 0.1f;

    [SerializeField]
    private float dampingSpeed = 1.0f;
    
    private Vector3 initialPosition;
    private float currentShakeDuration = 0f;

    private Action<object> OnPlayerHit;
    #endregion

    private void Awake()
    {
        OnPlayerHit = (param) => Shake();
        this.RegisterListener(EventID.GetHit, OnPlayerHit);
    }

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (currentShakeDuration > 0)
        {
            Vector3 randomOffset = Random.insideUnitSphere * shakeMagnitude;
            transform.localPosition = initialPosition + randomOffset;
            currentShakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            // Reset the camera's local position to its initial position.
            currentShakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    private void Shake()
    {
        currentShakeDuration = shakeDuration;
    }

}
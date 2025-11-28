using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticleController : MonoBehaviour
{
    [Header("Settings")] 
    public float defaultSize = 8f;
    public float hoverSize = 12f;
    public float smoothSpeed = 10f;

    private RectTransform rectTransform;
    private Camera playerCamera;
    private float targetSize;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        playerCamera = Camera.main;
        targetSize = defaultSize;
    }

    void Update()
    {
        HandleRaycast();
        AnimateReticle();
    }

    void HandleRaycast()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3f)) // Adjust range as needed
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                targetSize = hoverSize;
                return;
            }
        }

        targetSize = defaultSize;
    }

    void AnimateReticle()
    {
        float newSize = Mathf.Lerp(rectTransform.sizeDelta.x, targetSize, Time.deltaTime * smoothSpeed);
        rectTransform.sizeDelta = new Vector2(newSize, newSize);
    }
}


using System;
using Hiraishin.Utilities;
using Unity.Cinemachine;
using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    public CinemachineCamera camera;
    public SpriteRenderer render;

    // Start is called before the first frame update
    void Awake()
    {
        transform.ResetDefaultTransfrom();

        float worldHeight = camera.Lens.OrthographicSize * 2f;
        float worldWidth = worldHeight / Screen.height * Screen.width;
        float spriteWidth = render.sprite.bounds.size.x;
        float spriteHeight = render.sprite.bounds.size.y;

        transform.localScale = new Vector3(worldWidth / spriteWidth, worldHeight / spriteHeight, 1);
    }

    // temporary solution for cinecamera - this module is reusable from my old code
    private void LateUpdate()
    {
        Vector3 position = camera.transform.position;
        position.z = 0f;
        transform.position = position;
    }
}
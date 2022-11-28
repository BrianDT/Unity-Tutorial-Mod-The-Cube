using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;

    private float maxScale = 2.0f;
    private float minScale = 1.0f;
    private float deltaAngle = 15.0f;
    private Color originalColour;
    private Color alternativeColour;
    private Color startColour;
    private Color endColour;
    private TimeSpan colourCycleTime = TimeSpan.FromSeconds(20);
    private TimeSpan deltaCycleTime = TimeSpan.Zero;

    private Vector3 scaleChange, positionChange;
    private Vector3 rotationCentre = new Vector3(0, 0, 0);

    void Start()
    {
        transform.position = new Vector3(3, 4, 1);
        transform.localScale = Vector3.one * 1.3f;
        
        Material material = Renderer.material;

        this.startColour = this.originalColour = new Color(0.5f, 1.0f, 0.3f, 0.4f);
        this.endColour = this.alternativeColour = new Color(1.0f, 0.1f, 0.2f, 0.6f);
        material.color = originalColour;

        this.scaleChange = new Vector3(0.1f, 0.1f, 0.1f);
        this.minScale = UnityEngine.Random.Range(0.75f, 1.25f);
        this.maxScale = UnityEngine.Random.Range(1.75f, 2.75f);
    }
    
    void Update()
    {
        transform.Rotate(10.0f * Time.deltaTime, 5.0f * Time.deltaTime, 0.0f);

        transform.localScale += (scaleChange * Time.deltaTime);

        if (transform.localScale.y < minScale || transform.localScale.y > maxScale)
        {
            scaleChange = -scaleChange;
        }

        transform.RotateAround(rotationCentre, Vector3.up, deltaAngle * Time.deltaTime);

        this.deltaCycleTime += TimeSpan.FromSeconds(Time.deltaTime);
        if (this.deltaCycleTime > this.colourCycleTime)
        {
            this.deltaCycleTime = TimeSpan.Zero;
            var temp = this.startColour;
            this.startColour = this.endColour;
            this.endColour = temp;
        }

        Renderer.material.color = Color.Lerp(this.startColour, this.endColour, (float)this.deltaCycleTime.TotalSeconds / (float)this.colourCycleTime.TotalSeconds);
    }
}

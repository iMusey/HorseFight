using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public float duration;
    public float timer;
    public float animIndex;
    public Color color;

    public Material material;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // Change color of material using a "MaterialPropertyBlock"
        Renderer renderer = GetComponent<Renderer>();
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        renderer.GetPropertyBlock(block);
        block.SetColor("_NewColor", color);
        renderer.SetPropertyBlock(block);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        animIndex = timer / duration;

        // set parameter in animation window
        animator.SetFloat("Duration", animIndex);


        // destory when duration is up
        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }
}

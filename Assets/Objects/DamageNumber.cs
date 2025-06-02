using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float speed;
    public float duration;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = duration;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Destroy(gameObject);
        }

        // float up
        Vector3 vec = text.transform.position;
        vec.y += speed * Time.deltaTime;
        text.transform.position = vec;

        // fade out
        Color c = text.color;
        c.a = Mathf.Pow(timer,3) / Mathf.Pow(duration,3);
        text.color = c;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent (typeof(Text))]//make sure text is assigned


public class HealthDisplay : MonoBehaviour
{
    [SerializeField]
    public Health health;
    private Text text;

    //called on instance load
    private void Awake()
    {
        text = GetComponent<Text>();
        health = transform.root.GetComponent<Health>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = string.Format("Health: {0}%", Mathf.RoundToInt(health.percent * 100f));
    }
}

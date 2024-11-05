using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquishEffect : MonoBehaviour
{
    private Spring[] spring = new Spring[2];

    [SerializeField] private float angularFrequency = 10;
    [SerializeField] private float dampingRatio = .5f;

    [SerializeField] private float nudgeValue = -2;

    // Start is called before the first frame update
    void Start()
    {
        spring[0] = new Spring(angularFrequency, dampingRatio, transform.localScale.x, true);
        spring[1] = new Spring(angularFrequency, dampingRatio, transform.localScale.y, true);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Spring spring in spring)
        {
            spring.Update();
        }

        transform.localScale = new Vector3(spring[0].Position, spring[1].Position, transform.localScale.z);
    }

    public void OnClick()
    {
        foreach(Spring spring in spring)
        {
            spring.Nudge(nudgeValue);
        }
    }
}

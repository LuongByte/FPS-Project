using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    [Range(0, 100)]
    private float value;
    public float speed;
    public float margin;
    public RectTransform centre, up, down, left, right;
        // Start is called before the first frame update
    void Start()
    {
        value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate crosshair position
        float upVal, downVal, leftVal, rightVal;
        upVal = Mathf.Lerp(up.position.y, centre.position.y + margin + value, speed * Time.deltaTime);
        downVal = Mathf.Lerp(down.position.y, centre.position.y - margin - value, speed * Time.deltaTime);
        leftVal = Mathf.Lerp(left.position.x, centre.position.x - margin - value, speed * Time.deltaTime);
        rightVal = Mathf.Lerp(right.position.x, centre.position.x + margin + value, speed * Time.deltaTime);

        up.position = new Vector2(up.position.x, upVal);
        down.position = new Vector2(down.position.x, downVal);
        left.position = new Vector2(leftVal, centre.position.y);
        right.position = new Vector2(rightVal, centre.position.y);
    }

    public void incVal(float num)
    {
        value += num;
    }

    public void decVal(float num)
    {
        if(value - num > 0)
            value -= num;
        else
            value = 0;
    }
}

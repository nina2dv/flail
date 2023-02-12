using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crank : MonoBehaviour
{
    public float rotateSpeed = 10f;
    private Rope rope;
    private int numLinks;
    public int maxLinks = 15;

    void Awake()
    {
        rope = transform.parent.GetComponent<Rope>();
        numLinks = rope.numLinks;
    }
    
    public void Rotate(int direction)
    {
        if (direction > 0 && rope != null && numLinks <= maxLinks)
        {
            transform.Rotate(0, 0, direction * rotateSpeed);
            rope.addLink();
            numLinks++;
        }
        else if (direction <0 && rope != null && numLinks > 1)
        {
            transform.Rotate(0, 0, direction * rotateSpeed);
            rope.removeLink();
            numLinks--;
        }
    }
}

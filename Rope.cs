using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Rigidbody2D hook;
    public GameObject[] prefabRopeSegs;
    public int numLinks = 11;

    public HingeJoint2D top;
    
    public GameObject first;
    private Camera mainCam;
    private Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        GenerateRope();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    void GenerateRope()
    {
        Rigidbody2D prevBod = hook;
        for(int i =0; i < numLinks; i++)
        {
            // int index = Random.Range(0, prefabRopeSegs.Length);
            GameObject newSeg = Instantiate(prefabRopeSegs[0]);
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBod;

            prevBod = newSeg.GetComponent<Rigidbody2D>();

            

            if (i == 0)
            {
                top = hj;
                
            }
            
            if (i >= numLinks-1)
            {
                //Spike
                
                 for (int k = 0; k < 1; k++)
                {
                    GameObject lastSeg = Instantiate(prefabRopeSegs[1]);
                    lastSeg.transform.parent = transform;
                    lastSeg.transform.position = transform.position;
                    hj = lastSeg.GetComponent<HingeJoint2D>();
                    hj.connectedBody = prevBod;
                    prevBod = lastSeg.GetComponent<Rigidbody2D>();
                    // first = lastSeg;
                }         
            }

        }
    }

    public void addLink()
    {
        //int index = Random.Range(0, prefabRopeSegs.Length);
        GameObject newLink = Instantiate(prefabRopeSegs[0]);
        newLink.transform.parent = transform;
        newLink.transform.position = transform.position;
        HingeJoint2D hj = newLink.GetComponent<HingeJoint2D>();
        hj.connectedBody = hook;

        newLink.GetComponent<RopeSegment>().connectedBelow = top.gameObject;
        top.connectedBody = newLink.GetComponent<Rigidbody2D>();
        top.GetComponent<RopeSegment>().ResetAnchor();
        top = hj;
        // first = newLink;

    }

    public void removeLink()
    {
        HingeJoint2D newTop = top.gameObject.GetComponent<RopeSegment>().connectedBelow.GetComponent<HingeJoint2D>();
        newTop.connectedBody = hook;
        newTop.gameObject.transform.position = hook.gameObject.transform.position;
        newTop.GetComponent<RopeSegment>().ResetAnchor();
        Destroy(top.gameObject);
        top = newTop;
        // first = top.gameObject;
    }
    void Update()
    {
        // RotateSpike();
    }
    void RotateSpike()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        first.transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}

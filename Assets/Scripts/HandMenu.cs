using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandMenu : MonoBehaviour
{

    public Text txtLargeCount;
    public Text txtMediumCount;
    public Text txtSmallCount;
    public Text txtTinyCount;
    public Text txtTotalCount;
    public Text txtTimerText;

    public int largeGrems = 0;
    public int mediumGrems = 0;
    public int smallGrems = 0;
    public int tinyGrems = 0;



    // Start is called before the first frame update
    void Start()
    {
        
    }

 

    // Update is called once per frame
    void Update()
    {
        largeGrems = 0;
        mediumGrems = 0;
        smallGrems = 0;
        tinyGrems = 0;

        transform.position = transform.parent.position + (Vector3.up * 0.125f);

        transform.LookAt(Camera.main.transform);
        transform.Rotate(Vector3.up * 180);

        Gremblo[] grembloCount = GameObject.FindObjectsOfType<Gremblo>();
        for (int i = 0; i < grembloCount.Length; i++)
        {
            switch (grembloCount[i].size)
            {
                case 0:
                    tinyGrems += 1;

                    break;
                case 1:
                    smallGrems += 1;
                    break;
                case 2:
                    mediumGrems += 1;
                    break;
                case 3:
                    largeGrems += 1;
                    break;
            } 
        }

        txtLargeCount.text = largeGrems.ToString();
        txtMediumCount.text = mediumGrems.ToString();
        txtSmallCount.text = smallGrems.ToString();
        txtTinyCount.text = tinyGrems.ToString();
        txtTotalCount.text = grembloCount.Length.ToString();
    }



}

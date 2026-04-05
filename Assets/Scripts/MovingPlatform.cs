using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Rigidbody rb;
    private List<Vector3> points = new List<Vector3>();
    public int speed = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        foreach (Transform childTransform in this.transform)
        {
            if (!childTransform.CompareTag("point")) continue;

            points.Add(childTransform.position);

        }

        if (points.Count == 0) return;


        transform.position = points[0];

        if (points.Count > 1)
        {
            StartCoroutine(Move());
        }




    }

    public bool PingPong;
    IEnumerator Move()
    {

        while (true)
        {
            for (int i = 1; i < points.Count; i++)
            {
                while (Vector3.Distance(rb.position, points[i]) > 0.9f)
                {
                    Debug.Log("Moving");
                    rb.MovePosition(Vector3.MoveTowards(rb.position, points[i], speed * Time.fixedDeltaTime));
                    yield return null;
                }

                yield return new WaitForSecondsRealtime(1f);
            }
            if (PingPong)
            {
                for (int i = points.Count - 1; i > 0; i--)
                {
                    while (Vector3.Distance(rb.position, points[i - 1]) > 0.9f)
                    {
                        Debug.Log("Moving");
                        rb.MovePosition(Vector3.MoveTowards(rb.position, points[i - 1], speed * Time.fixedDeltaTime));
                        yield return null;
                    }

                    yield return new WaitForSecondsRealtime(1f);
                }
            }
            else
            {
                while (Vector3.Distance(rb.position, points[0]) > 0.9f)
                {
                    Debug.Log("Moving");
                    rb.MovePosition(Vector3.MoveTowards(rb.position, points[0], speed * Time.fixedDeltaTime));
                    yield return null;
                }
            }


                yield return new WaitForSecondsRealtime(1f);
        }






    }
}

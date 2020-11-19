using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*协程调用协程————自动寻路*/
   Transform[] wayPoints=new Transform[20];
    private float moveSpeed=1f;

    public IEnumerator PathFinding() {
        for (int i = 0; i < wayPoints.Length; i++)
        {
            yield return StartCoroutine(MoveToTarget(wayPoints[i].position));
        }
    }

    private IEnumerator MoveToTarget(Vector3 position)
    {
        while (Vector3.Distance(transform.position, position) > 0.1f) {
            transform.position = Vector3.MoveTowards(transform.position, position, moveSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();//等待一个物理帧
        }
    }
}

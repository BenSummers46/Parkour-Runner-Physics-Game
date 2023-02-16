using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMapChangeL4 : MonoBehaviour
{
    //Class that will change the layout of the AI arena on level 4
    //In rotation it will set different sets of object to active, therefore actively changing the NavMesh for the AI
    //All obstacles that are changed have a NavMeshObstacle component with the "carve" option enabled.
    [SerializeField] GameObject objectSet1;
    [SerializeField] GameObject objectSet2;

    public float cycleTime = 10f;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        setObj1();
    }

    private void setObj0()
    {
        objectSet1.SetActive(false);
        objectSet2.SetActive(false);

        Invoke("setObj1", cycleTime);
    }

    private void setObj1()
    {
        objectSet1.SetActive(true);

        Invoke("setObj2", cycleTime);
    }

    private void setObj2()
    {
        objectSet1.SetActive(false);
        objectSet2.SetActive(true);

        Invoke("setObj0", cycleTime);
    }
}

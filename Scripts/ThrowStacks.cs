using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowStacks : MonoBehaviour
{
    public static ThrowStacks instance;

    // Game Objects
    public List<GameObject> addedObjects;
    public GameObject Player;

    // UI Panel
    public Text HighScore_Text;
    public GameObject Win_Panel, Next_Button;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        HighScore_Text.text = "0";
    }

    public void Throw()
    {
        var addedObjectsCount = CollisionDetector.instance.AddedObjectsList.Count;
        Road_Movement.moving = false;

        for(int i=0; i<addedObjectsCount; i++)
        {
            addedObjects.Add(CollisionDetector.instance.AddedObjectsList[i]);
            addedObjects[i].GetComponent<Rigidbody>().isKinematic = false;
            addedObjects[i].GetComponent<Rigidbody>().useGravity = true;
            addedObjects[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            addedObjects[i].transform.position = new Vector3(addedObjects[i].transform.position.x, addedObjects[i].transform.position.y+1f, addedObjects[i].transform.position.z+1f);
            addedObjects[i].GetComponent<Rigidbody>().AddForce(new Vector3(addedObjects[i].transform.position.x, addedObjects[i].transform.position.y, addedObjects[i].transform.position.z + Random.Range(400f,600f)));
        }
        Invoke("Throw_Completed", 3f);
    }

    public void Throw_Completed()
    {
        for (int i = 0; i < addedObjects.Count; i++)
        {
            addedObjects[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

        float distance = Mathf.Floor(Mathf.Abs(Player.transform.position.z - addedObjects[addedObjects.Count - 1].transform.position.z));
        
        Camera.main.transform.LookAt(addedObjects[addedObjects.Count - 1].gameObject.transform);
        Camera.main.fieldOfView = 20;
        StartCoroutine(Win_Screen(distance));
    }

    IEnumerator Win_Screen(float distance)
    {
        new WaitForSeconds(2f);
        Win_Panel.SetActive(true);

        int start = 0;
        int block_ct = CollisionDetector.instance.Block_Count;
        int block_pt = block_ct * 100;
        float final_pt = block_pt * distance;

        while (start <= final_pt)
        {
            start += 500;
            HighScore_Text.text = start.ToString();
            yield return new WaitForSeconds(0.005f);
        }

        Next_Button.SetActive(true);
        yield return null;
    }

    
}

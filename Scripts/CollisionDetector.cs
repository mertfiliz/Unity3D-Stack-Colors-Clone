using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollisionDetector : MonoBehaviour
{
    public static CollisionDetector instance;

    // Game Blocks
    public List<GameObject> AddedObjectsList;
    public GameObject AddedObject;
    public int Active_Object_No, Block_Count;
    private bool initial_block;

    // Materials
    public Material Green_Material, Yellow_Material, Red_Material;    
    
    // Audio
    public AudioSource Collect_Sound;
    
    // Map Generation
    public GameObject Spawn_Point_1, Spawn_Point_2, Spawn_Point_3, Spawn_Point_4;
    public GameObject Block_Set_1, Block_Set_2, Block_Set_3;
    public GameObject Special_Red, Special_Yellow, Special_Green;

    // GameOver Panel
    public GameObject GameOver_Panel;
    public Text Plus1_Text;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        initial_block = true;
        Active_Object_No = 0;
        Block_Count = 0;
        Map_Generator();
    }

    private void Map_Generator()
    {
        for(int i=0;i<4;i++)
        {
            var rnd = Random.Range(0, 3);
            if (i == 0)
            {                
                if(rnd == 0)
                {
                    Instantiate(Block_Set_1, Spawn_Point_1.transform.position, Quaternion.identity, GameObject.Find("Road").transform);
                }
                if (rnd == 1)
                {
                    Instantiate(Block_Set_2, Spawn_Point_1.transform.position, Quaternion.identity, GameObject.Find("Road").transform);
                }
                if (rnd == 2)
                {
                    Instantiate(Block_Set_3, Spawn_Point_1.transform.position, Quaternion.identity, GameObject.Find("Road").transform);
                }
            }
            if (i == 1)
            {
                if (rnd == 0)
                {
                    Instantiate(Block_Set_1, Spawn_Point_2.transform.position, Quaternion.identity, GameObject.Find("Road").transform);
                }
                if (rnd == 1)
                {
                    Instantiate(Block_Set_2, Spawn_Point_2.transform.position, Quaternion.identity, GameObject.Find("Road").transform);
                }
                if (rnd == 2)
                {
                    Instantiate(Block_Set_3, Spawn_Point_2.transform.position, Quaternion.identity, GameObject.Find("Road").transform);
                }
            }
            if (i == 2)
            {
                if (rnd == 0)
                {
                    Instantiate(Block_Set_1, Spawn_Point_3.transform.position, Quaternion.identity, GameObject.Find("Road").transform);
                }
                if (rnd == 1)
                {
                    Instantiate(Block_Set_2, Spawn_Point_3.transform.position, Quaternion.identity, GameObject.Find("Road").transform);
                }
                if (rnd == 2)
                {
                    Instantiate(Block_Set_3, Spawn_Point_3.transform.position, Quaternion.identity, GameObject.Find("Road").transform);
                }
            }
            if (i == 3)
            {
                if (rnd == 0)
                {
                    Instantiate(Block_Set_1, Spawn_Point_4.transform.position, Quaternion.identity, GameObject.Find("Road").transform);
                }
                if (rnd == 1)
                {
                    Instantiate(Block_Set_2, Spawn_Point_4.transform.position, Quaternion.identity, GameObject.Find("Road").transform);
                }
                if (rnd == 2)
                {
                    Instantiate(Block_Set_3, Spawn_Point_4.transform.position, Quaternion.identity, GameObject.Find("Road").transform);
                }
            }
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "finishblock")
        {
            PlayerMovement.isLeftMove_Active = false;
            PlayerMovement.isRightMove_Active = false;
            ThrowStacks.instance.Throw();
        }
        else if(coll.gameObject.tag == "left_obs")
        {
            PlayerMovement.isLeftMove_Active = false;
        }      
        else if(coll.gameObject.tag == "right_obs")
        {
            PlayerMovement.isRightMove_Active = false;
        }
    }
    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.tag == "left_obs")
        {
            PlayerMovement.isLeftMove_Active = true;
        }
        else if (coll.gameObject.tag == "right_obs")
        {
            PlayerMovement.isRightMove_Active = true;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (Block_Count <= 0 && !initial_block)
        {
            GameOver();
        }
        else if (col.gameObject.tag == "green_special")
        {
            for (int i = 0; i < AddedObjectsList.Count; i++)
            {
                Active_Object_No = 1;
                AddedObjectsList[i].GetComponent<MeshRenderer>().material = Green_Material;
            }
        }
        else if (col.gameObject.tag == "yellow_special")
        {
            for (int i = 0; i < AddedObjectsList.Count; i++)
            {
                Active_Object_No = 2;
                AddedObjectsList[i].GetComponent<MeshRenderer>().material = Yellow_Material;
            }
        }
        else if(col.gameObject.tag == "red_special")
        {
            for(int i=0; i<AddedObjectsList.Count;i++)
            {
                Active_Object_No = 3;
                AddedObjectsList[i].GetComponent<MeshRenderer>().material = Red_Material;
            }
        }
        else
        { 
            if (Active_Object_No == 0)
            {
                initial_block = false;
                if (col.gameObject.tag == "greenblock")
                {
                    Active_Object_No = 1;
                    AddedObject.GetComponent<MeshRenderer>().material = Green_Material;
                }
                else if (col.gameObject.tag == "yellowblock")
                {
                    Active_Object_No = 2;
                    AddedObject.GetComponent<MeshRenderer>().material = Yellow_Material;
                }
                else if (col.gameObject.tag == "redblock")
                {
                    Active_Object_No = 3;
                    AddedObject.GetComponent<MeshRenderer>().material = Red_Material;
                }
                GameObject ins_block = Instantiate(AddedObject, new Vector3(this.transform.position.x, this.transform.position.y + 0.15f, this.transform.position.z), Quaternion.identity, this.transform);
                AddedObjectsList.Add(ins_block);
                Block_Count++;
                Collect_Sound.Play();
                Plus1_Text.text = "+1";
            }
            else if (Active_Object_No == 1)
            {
                if (col.gameObject.tag == "greenblock")
                {
                    AddedObject.GetComponent<MeshRenderer>().material = Green_Material;
                    GameObject LastBlock = AddedObjectsList[AddedObjectsList.Count - 1].gameObject;
                    GameObject ins_block = Instantiate(AddedObject, new Vector3(this.transform.position.x, LastBlock.transform.position.y + 0.15f, this.transform.position.z), Quaternion.identity, this.transform);
                    AddedObjectsList.Add(ins_block);
                    Block_Count++;
                    Plus1_Text.text = "+1";
                    Collect_Sound.Play();
                }
                else if (col.gameObject.tag == "yellowblock")
                {
                    AddedObject.GetComponent<MeshRenderer>().material = Yellow_Material;
                    GameObject LastBlock = AddedObjectsList[AddedObjectsList.Count - 1].gameObject;
                    Block_Count--;
                    Plus1_Text.text = "-1";
                    AddedObjectsList.Remove(LastBlock);
                    Destroy(LastBlock);       
                }
                else if (col.gameObject.tag == "redblock")
                {
                    AddedObject.GetComponent<MeshRenderer>().material = Red_Material;
                    GameObject LastBlock = AddedObjectsList[AddedObjectsList.Count - 1].gameObject;
                    Block_Count--;
                    Plus1_Text.text = "-1";
                    AddedObjectsList.Remove(LastBlock);
                    Destroy(LastBlock);       
                }
            }
            else if (Active_Object_No == 2)
            {       
                if (col.gameObject.tag == "greenblock")
                {
                    AddedObject.GetComponent<MeshRenderer>().material = Green_Material;
                    GameObject LastBlock = AddedObjectsList[AddedObjectsList.Count - 1].gameObject;
                    Block_Count--;
                    Plus1_Text.text = "-1";
                    AddedObjectsList.Remove(LastBlock);
                    Destroy(LastBlock);
                }
                else if (col.gameObject.tag == "yellowblock")
                {
                    AddedObject.GetComponent<MeshRenderer>().material = Yellow_Material;
                    GameObject LastBlock = AddedObjectsList[AddedObjectsList.Count - 1].gameObject;
                    GameObject ins_block = Instantiate(AddedObject, new Vector3(this.transform.position.x, LastBlock.transform.position.y + 0.15f, this.transform.position.z), Quaternion.identity, this.transform);
                    AddedObjectsList.Add(ins_block);
                    Block_Count++;
                    Plus1_Text.text = "+1";
                    Collect_Sound.Play();
                }
                else if (col.gameObject.tag == "redblock")
                {
                    AddedObject.GetComponent<MeshRenderer>().material = Red_Material;
                    GameObject LastBlock = AddedObjectsList[AddedObjectsList.Count - 1].gameObject;
                    Block_Count--;
                    Plus1_Text.text = "-1";
                    AddedObjectsList.Remove(LastBlock);
                    Destroy(LastBlock);
                }
            }
            else if (Active_Object_No == 3)
            {
                if (col.gameObject.tag == "greenblock")
                {
                    AddedObject.GetComponent<MeshRenderer>().material = Green_Material;
                    GameObject LastBlock = AddedObjectsList[AddedObjectsList.Count - 1].gameObject;
                    Block_Count--;
                    Plus1_Text.text = "-1";
                    AddedObjectsList.Remove(LastBlock);
                    Destroy(LastBlock);
                }
                else if (col.gameObject.tag == "yellowblock")
                {
                    AddedObject.GetComponent<MeshRenderer>().material = Yellow_Material;
                    GameObject LastBlock = AddedObjectsList[AddedObjectsList.Count - 1].gameObject;
                    Block_Count--;
                    Plus1_Text.text = "-1";
                    AddedObjectsList.Remove(LastBlock);
                    Destroy(LastBlock);
                }
                else if (col.gameObject.tag == "redblock")
                {
                    AddedObject.GetComponent<MeshRenderer>().material = Red_Material;
                    GameObject LastBlock = AddedObjectsList[AddedObjectsList.Count - 1].gameObject;
                    GameObject ins_block = Instantiate(AddedObject, new Vector3(this.transform.position.x, LastBlock.transform.position.y + 0.15f, this.transform.position.z), Quaternion.identity, this.transform);
                    AddedObjectsList.Add(ins_block);
                    Block_Count++;
                    Plus1_Text.text = "+1";
                    Collect_Sound.Play();
                }
            }
            Plus1_Text.gameObject.SetActive(true);
            Plus1_Text.transform.position = Camera.main.WorldToScreenPoint(new Vector3(this.transform.position.x, this.transform.position.y + 2f, this.transform.position.z));
            Invoke("Hide_Plus1", 0.2f);
            Destroy(col.gameObject);
            Check_GameOver();
        }        
    }

    private void Hide_Plus1()
    {
        Plus1_Text.gameObject.SetActive(false);
    }

    public void Check_GameOver()
    {
        if(Block_Count <= 0 && !initial_block)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Road_Movement.moving = false;
        PlayerMovement.isLeftMove_Active = false;
        PlayerMovement.isRightMove_Active = false;
        GameOver_Panel.SetActive(true);
    }
}

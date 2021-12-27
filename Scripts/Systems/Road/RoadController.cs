using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    public GameObject peacePrefab;
    public GameObject goal;
    public float speed = 3;
    public float distanceToMaintain = 1;
    public Vector3 offCamera = new Vector3(0,0,0);
    public Material grey , white;

    bool isPlaying = false;


    void Awake()
    {
        GameManager.gameStart += Init;
        GameManager.gameOverListener += () => {isPlaying = false;};
    }

    void Init()
    {
        AddPeaces();         

        isPlaying = true;
    }
    

    void Update()
    {
        if(isPlaying)
            ChainMove();
    }

    private void ChainMove()
    {
        if(transform.childCount == 0) return;

        Transform head = transform.GetChild(0);
        //move the head peace 
        float step = speed * Time.deltaTime;
        head.position = Vector3.MoveTowards(head.position , offCamera  , step);
        
        if(head.position.z <= offCamera.z){
            //we have reached off camera

            head.gameObject.SetActive(false);
            RearrangeConnections();
        }
    }

    private void RearrangeConnections()
    {
        transform.GetChild(0).SetAsLastSibling();

        for (var i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Peace peace = child.gameObject.GetComponent<Peace>();
            peace.distanceToMaintain = distanceToMaintain;
            peace.connectedTo = i > 0 ? transform.GetChild(i - 1) : null;

        }

    }

    private void AddPeaces()
    {

        
        for (var i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(true);
            child.gameObject.GetComponent<MeshRenderer>().material = i % 2 == 0 ? grey : white ;

            Peace peace = child.gameObject.GetComponent<Peace>();
            peace.connectedTo = i > 0 ? transform.GetChild(i - 1) : null;
            peace.distanceToMaintain = distanceToMaintain;

            if(i == transform.childCount - 1){
                GameObject goalClone = GameObject.Instantiate(goal , child.position , Quaternion.identity);
                goalClone.transform.parent = child;
            }

            if(i > 25){
                float randX = Random.Range(-1.2f*i , 1.2f*i);
                float randY = Random.Range(-i , i);
                child.position = new Vector3(randX,randY,i);
                
            
                
            }
            else
            {
                child.position = new Vector3(0,0,i);
                if(child.childCount > 0){
                    foreach(Transform t in child){
                        if(t.TryGetComponent<ColorCollision>(out ColorCollision collision)){
                            collision.MyColor = ColorCollision.player.MyColor;
                        }
                    }
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //public GameObject player;
    private GameObject battleArenaGameobject;
    private GameObject SpwanPlayerGameobject;
    private Rigidbody rb;
    public float moveSpeed;
    public float howclose;
    private float distan;
    public float rotationSpeed;
    private Vector3 movement;
    private Animator anim;
    private Transform CurrentPlayer;
    private Transform SelectPlayer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
       // battleArenaGameobject = GameObject.Find("BattleArena");
        SpwanPlayerGameobject = GameObject.Find("Spawn");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log("CHILD: " + SpwanPlayerGameobject.transform.childCount);

          for (int i = 0; i < SpwanPlayerGameobject.transform.childCount; i++)
          {
                CurrentPlayer = SpwanPlayerGameobject.transform.GetChild(i);
              //Debug.Log("CHILD: "+spawnPositions[i].childCount);
              if (CurrentPlayer.childCount==1){
                // Debug.Log("Player selection number is " + SpwanPlayerGameobject.transform[i].childCount);
                SelectPlayer = CurrentPlayer.GetChild(0);

                  Vector3 targetPosition = SelectPlayer.position;
                  Vector3 direction = targetPosition - transform.position;
                  distan = Vector3.Distance(targetPosition, transform.position);
                  //  Vector3 targetPosition = targetObject.transform.position;
                  //float angle = Mathf.Atan2(direction.y, direction.x)* Mathf.Rad2Deg;
                  //rb.rotation = Quaternion.Euler(0f, angle, 0f);


                  // calculate rotation to be done
                  Quaternion targetRotation = Quaternion.LookRotation(direction);

                  //NOTE :: If you don't want rotation along any axis you can set it to zero is as :-
                  // Setting Rotation along z axis to zero
                  targetRotation.z = 0;

                  // Setting Rotation along x axis to zero
                  targetRotation.x = 0;

                  // Apply rotation
                  if (distan <= howclose)
                  {
                      transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                  }

                  direction.Normalize();
                  movement = direction;

                  if (distan <= howclose)
                  {
                    anim.SetBool("isWalk", true);
                    moveCharacter(movement);
                }
                else
                {
                    anim.SetBool("isWalk", false);
                }
              }
          }
    }
    void moveCharacter(Vector3 direction)
    {
       
        rb.MovePosition((Vector3)transform.position+(direction*moveSpeed*Time.deltaTime));
    }

}


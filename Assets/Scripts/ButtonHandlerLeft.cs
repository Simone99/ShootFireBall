using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ButtonHandlerLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
   public GameObject player;
   public float speed;
   private bool buttonPressed;
   private Animator animator;

   void Start()
   {
       animator = player.GetComponent<Animator>();
   }

   public void Update(){
       if(buttonPressed && !GameManager.gamePaused()){
           player.transform.position += Vector3.left * speed * Time.deltaTime;
       }
   }

   public void OnPointerDown(PointerEventData eventData){
       buttonPressed = true;
       if(!GameManager.gamePaused()){
           animator.SetInteger("isMoving", -1);
       }
   }
 
   public void OnPointerUp(PointerEventData eventData){
       buttonPressed = false;
       if(!GameManager.gamePaused()){
           animator.SetInteger("isMoving", 0);
       }
   }
}

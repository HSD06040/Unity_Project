using System.Collections;
using System.Collections.Generic;
using DunGen;
using Unity.VisualScripting;
using UnityEngine;

public class doorRotate : MonoBehaviour
{

    
    [SerializeField]
    private GameObject door;
    private bool IsLock = false;

    private Door doorComponent;
    private Doorway thisDoorway;
    private float openAngle = 90f;
    private float closeAngle = 0f;
    private float rotationSpeed = 360f;
    private Coroutine rotateCoroutine;

    void Start()
    {
        doorComponent = GetComponent<Door>();
        thisDoorway = GetComponentInParent<Doorway>();   
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag != "Player"){
            return;
        }

        if(IsLock) return;

        doorComponent.IsOpen = true;
        Vector3 Location = transform.position - other.transform.position;
        float dot = Vector3.Dot(Location, transform.forward);


        Open(dot < 0f);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.tag != "Player"){ return; }

        if(IsLock) return;

        doorComponent.IsOpen = false;
        Close();
    }

    void Open(bool IsBack){
        Doorway connected = thisDoorway.ConnectedDoorway;
        if(connected != null){
            Tile nextRoom = connected.Tile;
            if(nextRoom != null){
                nextRoom.gameObject.SetActive(true);
            }
        }
        RotateDoor(IsBack? -openAngle: openAngle);
    }

    void Close(){
        RotateDoor(closeAngle);
    }

    void RotateDoor(float targetAngle){
        if(rotateCoroutine != null){
            StopCoroutine(rotateCoroutine);
        }

        rotateCoroutine = StartCoroutine(RotateCoroutine(targetAngle));
    }

    public void LockDoor(){
        IsLock = true;
    }

    public void UnLockDoor(){
        IsLock = false;
    }



    IEnumerator RotateCoroutine(float targetAngle){
        float startAngle = door.transform.localRotation.y;
        float AbsAngle = Mathf.Abs(Mathf.DeltaAngle(startAngle, targetAngle));
        float duration = AbsAngle / rotationSpeed;

        float time = 0f;
        while(time < duration){
            time += Time.deltaTime;
            time = Mathf.Min(time, duration);

            float normalizeTime = time / duration;
            float curAngle = Mathf.Lerp(startAngle, targetAngle, normalizeTime);
            door.transform.localRotation = Quaternion.Euler(0,curAngle,0);
            yield return null;
        }

        rotateCoroutine = null;
    }




}

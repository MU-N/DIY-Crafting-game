using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeHoles : MonoBehaviour
{
    [SerializeField] GameData gameData;
    [SerializeField] GameEvent gameState;
    [SerializeField] LayerMask cubeLayer;
    [SerializeField] LayerMask spriteLayer;
    Dictionary<int, int> isSelected = new Dictionary<int, int>();

    int amountToBeSelected;
    int playerSelectTrue;
    int playerSelectFalse;

    void Start()
    {
        foreach (Transform item in transform)
        {
            if (!isSelected.ContainsKey(item.GetInstanceID()))
                isSelected.Add(item.GetInstanceID(), 0); 
            if (Physics.Raycast(item.position, Vector3.up, 250.0f, spriteLayer))
            {
                isSelected[item.GetInstanceID()] = 1;
                amountToBeSelected++;
                Debug.Log(item.name);
            }

        }
    }

    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000.0f, cubeLayer))
            {
                FindObjectOfType<AudioManager>().Play("Click");

                DeleteCube(hit.transform.gameObject);

            }
        }
    }

    private void DeleteCube(GameObject hitObj)
    {
        hitObj.SetActive(false);
        isSelected[hitObj.transform.GetInstanceID()] +=1;
    }


    public void CalclatePercentage()
    {
        foreach (Transform item in transform)
        {
            if (isSelected[item.GetInstanceID()] >=2)
            {
                playerSelectTrue++;
            }
            else if (isSelected[item.GetInstanceID()] == 1)
            {
                playerSelectFalse++;
            }

        }
        gameData.percentage = ((playerSelectTrue/ amountToBeSelected) - (playerSelectFalse / amountToBeSelected)) * 100;
        gameState.Raise();
    }


    private void OnDrawGizmos()
    {
        foreach (Transform item in transform)
        {
            Gizmos.DrawRay(item.position, Vector3.up);
        }

    }
}

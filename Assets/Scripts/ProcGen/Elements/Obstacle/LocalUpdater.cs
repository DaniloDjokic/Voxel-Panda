using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Elements;

public class LocalUpdater<T> : ObsBehaviour
{
    protected List<T> currentlyContained = new List<T>();

    protected virtual void PlayerEnters(GameObject player)
    {

    }

    protected virtual void PlayerExits(GameObject player)
    {

    }

    private void OnDisable()
    {
       foreach (T component in currentlyContained)
       {
           if(component is MonoBehaviour)
            {
                if ((component as MonoBehaviour).gameObject.CompareTag("Player"))
                    PlayerExits((component as MonoBehaviour).gameObject);
            }         
       }
       currentlyContained.Clear();     
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerEnters(other.gameObject);
            T component = other.GetComponent<T>();
            if(!currentlyContained.Contains(component))
            {
                currentlyContained.Add(component);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerExits(other.gameObject);
            T component = other.GetComponent<T>();
            if (component != null)
            {
                if (currentlyContained.Contains(component))
                {
                    currentlyContained.Remove(component);
                }
            }
        }
    }
}

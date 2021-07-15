using System;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Sorts sort = null;
    public Sorts activeSort = null;
    public InputField speed = null;
    public InputField numberOfCubes = null;
    public SortChoices sortChoices = new SortChoices();
    public Tracks tracks = null;

    Sorts secondSort = null;
    Sorts thirdSort = null;
    float defaultSpeed = 0.1f;
    int defaultCubes = 30;


    void Update() 
    {
        if(Input.GetKey(KeyCode.Return))
        {
            StartSort();
        }
    }

    public void StartSort()
    {
        tracks.AudioClip(1);
        if(secondSort != null && thirdSort != null)
        {
            ResetSort(secondSort);
            ResetSort(thirdSort);
        }
        if(activeSort != null)
        {
            ResetSort(activeSort);
        }

        activeSort = Instantiate(sort);

        try
        {
            activeSort.speed = float.Parse(speed.text);
            activeSort.numberOfCubes = int.Parse(numberOfCubes.text);
        }
        catch(Exception)
        {
            activeSort.speed = defaultSpeed;
            activeSort.numberOfCubes = defaultCubes;
        }

        if(sortChoices != SortChoices.all)
        {
            activeSort.StartSort(sortChoices, new Vector3(-activeSort.numberOfCubes, 0f, 0f));
        }
        else
        {
            secondSort = activeSort;
            thirdSort = activeSort;
            activeSort.StartSort(SortChoices.bubble, new Vector3(-activeSort.numberOfCubes, 23f, 0f));
            secondSort.StartSort(SortChoices.selection, new Vector3(-activeSort.numberOfCubes, 11.5f, 0f));
            thirdSort.StartSort(SortChoices.insertion, new Vector3(-activeSort.numberOfCubes, 0f, 0f));
        }
    }

    public void ResetSort(Sorts sort)
    {
        Destroy(sort.gameObject);
    }

}

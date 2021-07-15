using System.Collections;
using UnityEngine;

public class Sorts : MonoBehaviour
{
    public int numberOfCubes = 10;
    public int cubeHeightMax = 10;
    public float speed = 1f;
    public GameObject cubePrefab = null;
    public GameObject[] cubes = null;
    public Canvas canvasPrefab = null;
    
    [Header("Material Lookup: \n0: Unsorted \n1: Sorted \n2: Current Place \n3: Best \n4: Looking At")]
    public Material[] materials = new Material[5];

    public void StartSort(SortChoices choice, Vector3 offset)
    {
        IntializeRandomCubes(offset);
        if(choice == SortChoices.selection) StartCoroutine(SelectionSortAlgorithm(cubes));
        else if(choice == SortChoices.bubble) StartCoroutine(BubbleSortAlgorithm(cubes));
        else if(choice == SortChoices.insertion) StartCoroutine(InsertionSortAlgorithm(cubes));
    }

    IEnumerator SelectionSortAlgorithm(GameObject[] unsortedList)
    {
        for (int i = 0; i < unsortedList.Length; i++)
        {
            int best = i;

            SetColor(unsortedList[i], 2);
            yield return new WaitForSeconds(speed);

            for (int j = i + 1; j < unsortedList.Length; j++)
            {
                SetColor(unsortedList[j], 4);
                yield return new WaitForSeconds(speed);

                if (unsortedList[j].transform.localScale.y < unsortedList[best].transform.localScale.y)
                {
                    if (best == i)
                    {
                        SetColor(unsortedList[i], 2);
                    }
                    else
                    {
                        SetColor(unsortedList[best], 0);
                    }
                    best = j;
                    SetColor(unsortedList[best], 3);
                }
                else
                {
                    SetColor(unsortedList[j], 0);
                }
            }
            if (best != i)
            {
                Swap(unsortedList, i, best);
                yield return new WaitForSeconds(speed);
            }
            SetColor(unsortedList[best], 0);
            SetColor(unsortedList[i], 1);
            unsortedList[i].GetComponentInChildren<ParticleSystem>().Play();      
        }        
    }

    IEnumerator BubbleSortAlgorithm(GameObject[] unsortedList)
    {

        bool done = false;
        int sorted = 0;

        while (!done)
        {
            done = true;
            for (int k = 0; k < unsortedList.Length - sorted - 1; k++)
            {
                SetColor(unsortedList[k], 2);
                SetColor(unsortedList[k + 1], 4);
                yield return new WaitForSeconds(speed);

                if (unsortedList[k].transform.localScale.y > unsortedList[k + 1].transform.localScale.y)
                {
                    Swap(unsortedList, k, k + 1);
                    yield return new WaitForSeconds(speed);
                    SetColor(unsortedList[k], 0);
                    done = false;
                }
                else
                {
                    SetColor(unsortedList[k], 0);
                }                
            }
            sorted++;
            SetColor(unsortedList[unsortedList.Length - sorted], 1);
            unsortedList[unsortedList.Length - sorted].GetComponentInChildren<ParticleSystem>().Play();
        }
        if (sorted < unsortedList.Length)
        {
            yield return new WaitForSeconds(speed);
            for (int i = 0; i < unsortedList.Length - sorted; i++)
            {
                SetColor(unsortedList[i], 1);
                unsortedList[i].GetComponentInChildren<ParticleSystem>().Play();
            }
        }
    }

    IEnumerator InsertionSortAlgorithm(GameObject[] unsortedList)
    {
        SetColor(unsortedList[0], 1); // Sorted Color
        unsortedList[0].GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSeconds(speed);

        for(int j = 1; j < unsortedList.Length; j++)
        {
            SetColor(unsortedList[j], 4); // Looking At Color 
            yield return new WaitForSeconds(speed);
            int k = j - 1;

            while((k >= 0) && (unsortedList[k].transform.localScale.y > unsortedList[k + 1].transform.localScale.y))
            {
                SetColor(unsortedList[k + 1], 2); // Current Place Color                
                SetColor(unsortedList[k], 4); // Looking At Color
                yield return new WaitForSeconds(speed);
                Swap(unsortedList, k, k + 1);
                SetColor(unsortedList[k + 1], 1); // Sorted Color 
                unsortedList[k + 1].GetComponentInChildren<ParticleSystem>().Play();
                yield return new WaitForSeconds(speed);
                k--;
            }               
            SetColor(unsortedList[k + 1], 1); // Sorted Color
            unsortedList[k + 1].GetComponentInChildren<ParticleSystem>().Play();
        }        
    }

    void Swap(GameObject[] list, int i, int j)
    {
        GameObject temp = list[i];
        list[i] = list[j];
        list[j] = temp;

        GameObject cubeSwapper = new GameObject();
        cubeSwapper.AddComponent<CubeSwapper>();
        cubeSwapper.GetComponent<CubeSwapper>().Setup(list[j], list[i], speed);
    }
    
    void SetColor(GameObject cube, int materialIndex)
    {
        cube.GetComponent<Renderer>().material = materials[materialIndex];
    }

    void IntializeRandomCubes(Vector3 offset)
    {
        cubes = new GameObject[numberOfCubes];

        for(int i = 0; i < numberOfCubes; i++)
        {
            int randomNumber = Random.Range(1, cubeHeightMax + 1);

            Vector3 cubePosition = new Vector3(i + offset.x / 2, randomNumber / 2f + offset.y, offset.z);

            GameObject cube = Instantiate(cubePrefab, cubePosition, Quaternion.identity);
            cube.GetComponentInChildren<ParticleSystem>().Stop();
            cube.transform.localScale = new Vector3(0.7f, randomNumber, 1f);

            Canvas canvas = Instantiate(canvasPrefab, new Vector3(i + offset.x / 2, -1f + offset.y, -0.501f + offset.z), Quaternion.identity);
            canvas.transform.SetParent(cube.transform);
            cube.GetComponent<ValueText>().Setup();

            cube.transform.SetParent(this.transform);

            cubes[i] = cube;
        }
        //this.transform.position = new Vector3(-numberOfCubes / 2f + 0.5f, 0f, 0f);
    }
}



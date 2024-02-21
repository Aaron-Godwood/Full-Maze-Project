using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using graph;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class WallGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject objectToSpawn;
    //Vector3 Vec = new Vector3(0, 0, 0);
    Quaternion Quaternion = Quaternion.identity;
    private int size = 20;
    private Graph3D maze;
    //Quaternion Quaternion2 = Quaternion()
    void Start()
    {
        maze = new Graph3D(size, size);
        maze.Kruskals();
        AddWalls3D();
        RemoveWalls3D();



    }

    public void AddWalls()
    {
        GameObject newWall;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                newWall = Instantiate(objectToSpawn, new Vector3(0 + i * 20, 7, 0 + j * 20), Quaternion);
                newWall.name = ($"HWall{i},{j}");
                newWall = Instantiate(objectToSpawn, new Vector3(-10 + i * 20, 7, 10 + j * 20), Quaternion.Euler(0, 90, 0));
                newWall.name = ($"VWall{i},{j}");
            }

        }
        for (int i = 0; i < size; i++)
        {
            Instantiate(objectToSpawn, new Vector3(0 + i * 20, 7, 0 + size * 20), Quaternion);
            Instantiate(objectToSpawn, new Vector3(-10 + size * 20, 7, 10 + i * 20), Quaternion.Euler(0, 90, 0));
        }
    }

    public void AddWalls3D()
    {
        GameObject newWall;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                for (int  k = 0; k < size; k++)
                {
                    newWall = Instantiate(objectToSpawn, new Vector3(0 + i * 24, 12 + k * 24, 0 + j * 24), Quaternion);
                    newWall.name = ($"HWall{i},{j},{k}");
                    newWall = Instantiate(objectToSpawn, new Vector3(-12 + i * 24, 12 + k*24, 12 + j * 24), Quaternion.Euler(0, 90, 0));
                    newWall.name = ($"VWall{i},{j},{k}");
                    newWall = Instantiate(objectToSpawn, new Vector3(0 + i * 24, 2 + k*24, 12 + j * 24), Quaternion.Euler(90, 0, 0));
                    newWall.name = ($"FWall{i},{j},{k}");
                }
                

            }

        }
        for (int i = 0; i < size; i++)
        {
            for(int k = 0; k < size; k++)
            {
                Instantiate(objectToSpawn, new Vector3(0 + i * 24, 12 + k*24, 0 + size * 24), Quaternion);
                Instantiate(objectToSpawn, new Vector3(-12 + size * 24, 12 + k*24, 12 + i * 24), Quaternion.Euler(0, 90, 0));
                Instantiate(objectToSpawn, new Vector3(0 +k*24, 2 + size * 24, 12 + i * 24), Quaternion.Euler(90, 0, 0));
            }
            

        }
    }


    public void RemoveWalls3D()
    {
        List<int> connections = new List<int>();
        for (int i = 0; i < maze.GetAdjacency().Count; i++)
        {
            for (int j = 0; j < maze.GetAdjacency()[i].Count; j++)
            {
                //Debug.Log(adjacency[i][j]);
                //Debug.Log(i + j);
                if (maze.GetAdjacency()[i][j] == 1)
                {
                    connections.Add(j);
                }
                
            }
            //Debug.Log(connections.Count);
            for (int j = 0; j < connections.Count; j++)
            {
                //Debug.Log(j);
                if ((i % size == connections[j] % size) && (i / (size * size) == connections[j] / (size * size)) && i < connections[j])
                {
                    Destroy(GameObject.Find($"HWall{i % size},{(connections[j] - ((connections[j]/(size*size))*size*size)) / size},{i/(size*size)}"));
                    Debug.Log($"HWall{i % size},{(connections[j] - ((connections[j] / (size * size)) * size * size)) / size},{i / (size * size)}");
                }
                else if ((i % size == connections[j] % size) && (i / (size * size) == connections[j] / (size * size)) && i > connections[j]) //gre\t code!
                {
                    Destroy(GameObject.Find($"HWall{i % size},{(i - ((i / (size * size)) * size * size)) / size},{i / (size * size)}"));
                    Debug.Log($"HWall{i % size},{(i - ((i / (size * size)) * size * size)) / size},{i / (size * size)}");
                }
                else if ((((i - ((i / (size * size)) * size * size)) / size) == (connections[j] - ((connections[j] / (size * size)) * size * size)) / size) && (i / (size * size) == connections[j] / (size * size)) && i < connections[j])
                {
                    Destroy(GameObject.Find($"VWall{connections[j] % size},{(i - ((i / (size * size)) * size * size)) / size},{i / (size * size)}"));
                }
                else if ((((i - ((i / (size * size)) * size * size)) / size) == (connections[j] - ((connections[j] / (size * size)) * size * size)) / size) && (i / (size * size) == connections[j] / (size * size)) && i > connections[j])
                {
                    Destroy(GameObject.Find($"VWall{i % size},{(i - ((i / (size * size)) * size * size)) / size},{i / (size * size)}"));
                }
                else if (((i - ((i / (size * size)) * size * size)) / size == (connections[j] - ((connections[j] / (size * size)) * size * size)) / size) && (i % size == connections[j] % size) && i < connections[j])
                {
                    Destroy(GameObject.Find($"FWall{i % size},{(i - ((i / (size * size)) * size * size)) / size},{connections[j] / (size * size)}"));
                    Debug.Log($"FWall{i % size},{(i - ((i / (size * size)) * size * size)) / size},{connections[j] / (size * size)}");
                }
                else if (((i - ((i / (size * size)) * size * size)) / size == (connections[j] - ((connections[j] / (size * size)) * size * size)) / size) && (i % size == connections[j] % size) && i > connections[j])
                {
                    Destroy(GameObject.Find($"FWall{i % size},{(i - ((i / (size * size)) * size * size)) / size},{i / (size * size)}"));
                    Debug.Log($"FWall{i % size},{(i - ((i / (size * size)) * size * size)) / size},{i / (size * size)}");
                }

            }
            connections.Clear();

        }
    }
    public void RemoveWalls()
    {
        
        //Destroy(GameObject.Find($"HWall{0},{1}"));
        //List<List<int>> adjacency = maze.GetAdjacency();
        List<int> connections = new List<int>();
        for (int i = 0; i < maze.GetAdjacency().Count; i++)
        {
            for (int j = 0; j < maze.GetAdjacency()[i].Count; j++)
            {
                //Debug.Log(adjacency[i][j]);
                //Debug.Log(i + j);
                if (maze.GetAdjacency()[i][j] == 1)
                {
                    connections.Add(j);
                }
            }
            //Debug.Log(connections.Count);
            for (int j = 0; j < connections.Count; j++)
            {
                //Debug.Log(j);
                if(i%size == connections[j] % size && i < connections[j])
                {
                    Destroy(GameObject.Find($"HWall{i % size},{connections[j]/size}"));
                    //Debug.Log($"HWall{i % size},{connections[j] / size}");
                }
                else if(i%size == connections[j] % size && i > connections[j])
                {
                    Destroy(GameObject.Find($"HWall{i % size},{i / size}"));
                }
                else if ( i/size == connections[j] / size &&  i < connections[j])
                {
                    Destroy(GameObject.Find($"VWall{connections[j] % size},{i/size}"));
                }
                else if (i / size == connections[j] / size && i > connections[j])
                {
                    Destroy(GameObject.Find($"VWall{i % size},{i / size}"));
                }
            }
            connections.Clear();
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

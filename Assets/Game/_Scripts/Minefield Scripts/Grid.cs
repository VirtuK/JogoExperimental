using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Grid : MonoBehaviour
{
    public int width;
    public int lenght;
    public int bombCount;
    public GameObject tile;
    public List<GameObject[]> gridPosition = new List<GameObject[]>();
    public List<Vector3> pathPosition = new List<Vector3>();
    public List<GameObject> bombList = new List<GameObject>();
    public Transform cam;
    public GameObject player;
    public Vector3 start;
    public bool finishDecided = false;
    public bool startDecided = false;
    public GameObject bombPrefab;
    public GameObject bombs;
    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i <= width; i++)
        {
            List<GameObject> gridX = new List<GameObject>();
            for (int j = 0; j <= lenght; j++)
            {
                GameObject grid = Instantiate(tile, new Vector3(i, j), Quaternion.identity);
                grid.transform.SetParent(this.transform);
                gridX.Add(grid);
                if (i == 0 || i == width || j == 0 || j == width)
                {
                    if (i != 0 && j != 0 || i != width && j != 0 || i != width && j != lenght || i != 0 && j != lenght)
                    {
                        int x = Random.Range(1, width);
                        if (x == 1 && !finishDecided)
                        {
                            setFinishPoint(grid);

                        }
                        else
                        {
                            setWall(grid);
                        }
                    }
                    else
                    {
                        setWall(grid);
                    }
                }
                else
                {
                    
                    int x = Random.Range(1, 2);
                    if(x == 1)
                    {
                        setPath(grid);
                    }
                    else
                    {
                        setWall(grid);
                    }
                }

                
            }
            gridPosition.Add(gridX.ToArray());
            print(gridPosition[i].Length);
        }
        
        cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)lenght / 2 - 0.5f, -1.5f);
        setPlayer();
        setBombs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void setPlayer()
    {
        player.transform.position = new Vector3(start.x, start.y, -0.3f);
        player.GetComponent<Player>().checkPosition();
    }

    void setWall(GameObject grid)
    {
        grid.name = "Wall";
        grid.GetComponent<SpriteRenderer>().color = Color.black;
    }

    void setFinishPoint(GameObject grid)
    {
        grid.name = "Finish Point";
        grid.GetComponent<SpriteRenderer>().color = Color.green;
        finishDecided = true;
    }

    void setPath(GameObject grid)
    {
        grid.name = "Path";
        grid.GetComponent<SpriteRenderer>().color = Color.white;
        if (!startDecided)
        {
            start = grid.transform.position;
            startDecided = true;
        }
        pathPosition.Add(grid.transform.position);
        
    }

    void setBombs()
    {
        for(int i = 1; i <= bombCount; i++)
        {
            int bombPosIndex = Random.Range(0, pathPosition.Count - 1);
            Vector3 bombPos = pathPosition[bombPosIndex];
            GameObject newBomb = Instantiate(bombPrefab, bombPos, Quaternion.identity);
            newBomb.transform.SetParent(bombs.transform);
            bombList.Add(newBomb);
            pathPosition.Remove(pathPosition[bombPosIndex]);
        }
    }
}

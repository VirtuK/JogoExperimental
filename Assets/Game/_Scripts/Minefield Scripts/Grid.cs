using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Grid : MonoBehaviour
{
    private int width = 16;
    private int lenght = 16;
    public int bombCount;
    public GameObject tile;
    public List<GameObject[]> gridPosition = new List<GameObject[]>();
    public List<Vector3> pathPosition = new List<Vector3>();
    public List<GameObject> bombList = new List<GameObject>();
    public int levelNumbers = 0;
    public Transform cam;
    public GameObject player;
    public Vector3 start;
    public bool finishDecided = false;
    public bool startDecided = false;
    public GameObject bombPrefab;
    public GameObject bombs;
    List<Vector2> level0 = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        
        
        level0.AddRange(new List<Vector2> { new Vector2(1, 15), new Vector2(1,14), new Vector2(1,13),
        new Vector2(1,12), new Vector2(2,12), new Vector2(3,12), new Vector2(4,12),
        new Vector2(5,12), new Vector2(6,12), new Vector2(6,11), new Vector2(6,10),
        new Vector2(5,10), new Vector2(5,9), new Vector2(5,8), new Vector2(5,7),
        new Vector2(5,6), new Vector2(5,5), new Vector2(4,5), new Vector2(3,5),
        new Vector2(2,5), new Vector2(1,5), new Vector2(1,4), new Vector2(1,3),
        new Vector2(1,2), new Vector2(1,1), new Vector2(2,1), new Vector2(3,1),
        new Vector2(4,1), new Vector2(5,1), new Vector2(5,2), new Vector2(5,3),
        new Vector2(6,3), new Vector2(7,3), new Vector2(7,4), new Vector2(8,4),
        new Vector2(9,4), new Vector2(9,5), new Vector2(9,6), new Vector2(9,7),
        new Vector2(9,8), new Vector2(9,9), new Vector2(9,10), new Vector2(9,11),
        new Vector2(9,12), new Vector2(9,13), new Vector2(10,13), new Vector2(11,13),
        new Vector2(11,12), new Vector2(11,11), new Vector2(11,10), new Vector2(12,10), new Vector2(12,9),
        new Vector2(12,8), new Vector2(12,7), new Vector2(13,7), new Vector2(13,6),
        new Vector2(13,5), new Vector2(13,4), new Vector2(14,4), new Vector2(14,3),
        new Vector2(14,2), new Vector2(14,1), new Vector2(14,0)});

        for (int i = 0; i <= width; i++)
        {
            List<GameObject> gridX = new List<GameObject>();
            for (int j = 0; j <= lenght; j++)
            {
                GameObject grid = Instantiate(tile, new Vector3(i, j, 0), Quaternion.identity);
                grid.transform.SetParent(this.transform);
                gridX.Add(grid);
                setWall(grid);

                
            }
            gridPosition.Add(gridX.ToArray());
            print(gridPosition[i].Length);
        }
        
        cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)lenght / 2 - 0.5f, -1.5f);
        setPath();
        
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

    void setPath()
    {
        int rand = 0;
        switch (rand)
        {
            case 0:
                for (int i = 0; i < level0.Count; i++)
                {
                    int x = (int)level0[i].x;
                    int y = (int)level0[i].y;
                    if (i == 0)
                    {
                        setStart(gridPosition[x][y]);
                    }
                    else if (i == level0.Count - 1)
                    {
                        setFinishPoint(gridPosition[x][y]);
                    }
                    else
                    {
                        gridPosition[x][y].name = "Path";
                        gridPosition[x][y].GetComponent<SpriteRenderer>().color = Color.white;
                    }
                    pathPosition.Add(gridPosition[x][y].transform.position);
                }
                break;
        }
        setPlayer();
        setBombs();
    }

    void setStart(GameObject grid)
    {
        grid.name = "Start";
        grid.GetComponent<SpriteRenderer>().color = Color.yellow;
        start = grid.transform.position;

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

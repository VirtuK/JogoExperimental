using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Grid : MonoBehaviour
{
    private int width = 8;
    private int lenght = 8;
    public int bombCount;
    public GameObject tile;
    public List<GameObject[]> gridPosition = new List<GameObject[]>();
    public List<GameObject> paths = new List<GameObject>();
    public List<Vector3> pathPosition = new List<Vector3>();
    public List<Vector3> walkPosition = new List<Vector3>();
    public List<GameObject> bombList = new List<GameObject>();
    public Transform cam;
    public GameObject player;
    public Vector3 start;
    public bool finishDecided = false;
    public bool startDecided = false;
    public GameObject bombPrefab;
    public GameObject bombs;
    List<Vector2> level1 = new List<Vector2>();
    List<Vector2> level2 = new List<Vector2>();
    List<Vector2> level3 = new List<Vector2>();
    List<Vector2> level4 = new List<Vector2>();
    List<Vector2> level5 = new List<Vector2>();
    List<Vector2> level6 = new List<Vector2>();
    List<Vector2[]> levels = new List<Vector2[]>();
    public Sprite pathSprite;
    public Sprite wallSprite;


    // Start is called before the first frame update
    void Start()
    {
        
        
        level1.AddRange(new List<Vector2> { new Vector2(0,7), new Vector2(0, 6), new Vector2(1, 6), new Vector2(1, 5),
         new Vector2(1,4), new Vector2(1,3),  new Vector2(2,3), new Vector2(3,3), new Vector2(3,4), new Vector2(3,5),
         new Vector2(4,5), new Vector2(5,5), new Vector2(5,4), new Vector2(5,3), new Vector2(5,2), new Vector2(6,2),
         new Vector2(6,1), new Vector2(7,1), new Vector2(7,0)});

        level2.AddRange(new List<Vector2> { new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 2), new Vector2(1, 3),
        new Vector2(2,3),new Vector2(3,3),new Vector2(3,2),new Vector2(3,1),new Vector2(4,1),new Vector2(5,1),new Vector2(6,1),
        new Vector2(6,2),new Vector2(6,3),new Vector2(6,4),new Vector2(5,4),new Vector2(5,5),new Vector2(5,6),new Vector2(6,6),
        new Vector2(7,6),new Vector2(8,6)});

        level3.AddRange(new List<Vector2> { new Vector2(3, 0), new Vector2(3, 1),new Vector2(2,1), new Vector2(1, 1), new Vector2(1, 2),
        new Vector2(1,3),new Vector2(1,4),new Vector2(1,5),new Vector2(1,6),new Vector2(2,6),new Vector2(2,7),new Vector2(3,7),
        new Vector2(4,7),new Vector2(4,6),new Vector2(4,5),new Vector2(5,5),new Vector2(6,5),new Vector2(7,5), new Vector2(8,5)});

        level4.AddRange(new List<Vector2> { new Vector2(2, 8),new Vector2(2, 7), new Vector2(2, 6), new Vector2(1, 6), new Vector2(0, 6),
        new Vector2(0, 5), new Vector2(0, 4), new Vector2(1, 4), new Vector2(1, 3), new Vector2(1, 2), new Vector2(0, 2),
        new Vector2(0, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(3, 0), new Vector2(3, 1),
        new Vector2(3, 2), new Vector2(3, 3), new Vector2(4, 3), new Vector2(4, 4), new Vector2(4, 5), new Vector2(4, 6),
        new Vector2(4, 7), new Vector2(4, 8),});

        level5.AddRange(new List<Vector2> { new Vector2(0, 5), new Vector2(1, 5), new Vector2(1, 6), new Vector2(2, 6),
        new Vector2(3, 6),new Vector2(3, 5),new Vector2(3, 4),new Vector2(3, 3),new Vector2(2, 3),new Vector2(2, 2),
        new Vector2(2, 1),new Vector2(3, 1),new Vector2(4, 1),new Vector2(5, 1),new Vector2(6, 1),new Vector2(6, 2),
        new Vector2(6, 3),new Vector2(6, 4),new Vector2(6, 5),new Vector2(7, 5),new Vector2(8, 5),});

        level6.AddRange(new List<Vector2> { new Vector2(0, 7), new Vector2(1, 7), new Vector2(2, 7), new Vector2(2, 6),
        new Vector2(2, 5),new Vector2(3, 5),new Vector2(4, 5),new Vector2(5, 5),new Vector2(5, 4),new Vector2(5, 3),
        new Vector2(4, 3),new Vector2(3, 3),new Vector2(2, 3),new Vector2(1, 3),new Vector2(1, 2),new Vector2(1, 1),
        new Vector2(2, 1),new Vector2(3, 1),new Vector2(4, 1),new Vector2(4, 0),});

        levels.Add(level1.ToArray());
        levels.Add(level2.ToArray());
        levels.Add(level3.ToArray());
        levels.Add(level4.ToArray());
        levels.Add(level5.ToArray());
        levels.Add(level6.ToArray());

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
        }
        
        cam.transform.position = new Vector3((float)width / 2 + 0.3f, (float)lenght / 2 + 0.02f, -1.5f);
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
        grid.GetComponent<SpriteRenderer>().sprite = wallSprite;
    }

    void setFinishPoint(GameObject grid)
    {
        grid.name = "Finish Point";
        grid.GetComponent<SpriteRenderer>().sprite = pathSprite;
        finishDecided = true;
    }

    void setPath()
    {
        int rand = Random.Range(0,levels.Count);
                for (int i = 0; i < levels[rand].Length; i++)
                {
                    int x = (int)levels[rand][i].x;
                    int y = (int)levels[rand][i].y;
                    if (i == 0)
                    {
                        setStart(gridPosition[x][y]);
                    }
                    else if (i == levels[rand].Length - 1)
                    {
                        setFinishPoint(gridPosition[x][y]);
                        
                    }
                    else
                    {
                        gridPosition[x][y].name = "Path";
                        gridPosition[x][y].GetComponent<SpriteRenderer>().sprite = pathSprite;
                      
                    }
                    pathPosition.Add(gridPosition[x][y].transform.position);
                    paths.Add(gridPosition[x][y]);
                    walkPosition.Add(gridPosition[x][y].transform.position);
                }
                

        setPlayer();
        setBombs();
    }

    void setStart(GameObject grid)
    {
        grid.name = "Start";
        grid.GetComponent<SpriteRenderer>().sprite = pathSprite;
        start = grid.transform.position;

    }

    void setBombs()
    {
        for(int i = 1; i <= bombCount; i++)
        {
            int bombPosIndex = Random.Range(0, pathPosition.Count - 1);
            Vector3 bombPos = pathPosition[bombPosIndex];
            paths[bombPosIndex].name = "Bomb";
            GameObject newBomb = Instantiate(bombPrefab, bombPos, Quaternion.identity);
            newBomb.transform.SetParent(bombs.transform);
            newBomb.GetComponent<Animator>().SetInteger("bomb", i);
            newBomb.transform.position = new Vector3(bombPos.x, bombPos.y, -0.2f);
            bombList.Add(newBomb);
            pathPosition.Remove(pathPosition[bombPosIndex]);
        }
    }
}

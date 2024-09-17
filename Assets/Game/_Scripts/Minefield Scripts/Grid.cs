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
    public Sprite bombSprite;
    // Start is called before the first frame update
    void Start()
    {
        
        
        level1.AddRange(new List<Vector2> { new Vector2(1, 15), new Vector2(1,14), new Vector2(1,13),
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

        level2.AddRange(new List<Vector2> { new Vector2(0,3), new Vector2(1,3), new Vector2(1,4), new Vector2(2,4),
        new Vector2(2,5),new Vector2(2,6),new Vector2(2,7),new Vector2(3,7),new Vector2(4,7),new Vector2(4,6),
        new Vector2(4,5),new Vector2(4,4),new Vector2(4,3),new Vector2(5,3),new Vector2(6,3),new Vector2(7,3),
        new Vector2(8,3),new Vector2(8,2),new Vector2(8,1),new Vector2(9,1),new Vector2(10,1),new Vector2(11,1),
        new Vector2(12,1),new Vector2(13,1),new Vector2(14,1),new Vector2(14,2),new Vector2(14,3),new Vector2(14,4),
        new Vector2(14,5),new Vector2(14,6),new Vector2(14,7),new Vector2(14,8),new Vector2(14,9),new Vector2(13,9),
        new Vector2(12,9),new Vector2(12,8),new Vector2(12,7),new Vector2(12,6),new Vector2(12,5),new Vector2(12,4),
        new Vector2(12,3),new Vector2(11,3),new Vector2(10,3),new Vector2(10,4),new Vector2(10,5),new Vector2(10,6),
        new Vector2(10,7),new Vector2(10,8),new Vector2(9,8),new Vector2(9,9),new Vector2(8,9),new Vector2(7,9),
        new Vector2(6,9),new Vector2(5,9),new Vector2(4,9),new Vector2(3,9),new Vector2(3,10),new Vector2(3,11),
        new Vector2(2,11),new Vector2(1,11),new Vector2(1,12),new Vector2(1,13),new Vector2(1,14),new Vector2(2,14),
        new Vector2(3,14),new Vector2(4,14),new Vector2(5,14),new Vector2(5,13),new Vector2(5,12),new Vector2(5,11),
        new Vector2(6,11),new Vector2(7,11),new Vector2(8,11),new Vector2(8,12),new Vector2(8,13),new Vector2(9,13),
        new Vector2(10,13),new Vector2(11,13),new Vector2(11,12),new Vector2(11,11),new Vector2(12,11),new Vector2(13,11),
        new Vector2(14,11),new Vector2(14,12),new Vector2(14,13),new Vector2(14,14),new Vector2(15,14), new Vector2(16,14)});

        level3.AddRange(new List<Vector2> { new Vector2(8,0), new Vector2(8,1), new Vector2(8,2), new Vector2(8,3),
        new Vector2(7,3), new Vector2(6,3), new Vector2(5,3), new Vector2(4,3), new Vector2(3,3), new Vector2(3,4),
        new Vector2(3,5), new Vector2(3,6), new Vector2(3,7), new Vector2(3,8), new Vector2(3,9), new Vector2(3,10), new Vector2(3,11),
        new Vector2(4,11), new Vector2(5,11), new Vector2(5,10), new Vector2(5,9), new Vector2(5,8), new Vector2(5,7), new Vector2(6,7),
        new Vector2(7,7), new Vector2(8,7), new Vector2(9,7), new Vector2(10,7), new Vector2(11,7), new Vector2(11,8), new Vector2(11,9),
        new Vector2(12,9), new Vector2(12,10), new Vector2(12,11), new Vector2(12,12), new Vector2(13,12), new Vector2(14,12),
        new Vector2(14,11), new Vector2(14,10), new Vector2(14,9), new Vector2(15,9), new Vector2(16,9), new Vector2(17,9)});

        level4.AddRange(new List<Vector2> { new Vector2(5,15), new Vector2(5,14), new Vector2(5,13), new Vector2(4,13), new Vector2(3,13),
        new Vector2(3,14),new Vector2(2,14),new Vector2(1,14),new Vector2(1,13),new Vector2(1,12),new Vector2(1,11),new Vector2(1,10),
        new Vector2(2,10),new Vector2(3,10),new Vector2(3,11),new Vector2(4,11),new Vector2(5,11),new Vector2(6,11),new Vector2(6,12),
        new Vector2(7,12),new Vector2(8,12),new Vector2(8,11),new Vector2(8,10),new Vector2(8,9),new Vector2(7,9),new Vector2(6,9),
        new Vector2(5,9),new Vector2(5,8),new Vector2(4,8),new Vector2(3,8),new Vector2(2,8),new Vector2(1,8),new Vector2(1,7),
        new Vector2(1,6),new Vector2(1,5),new Vector2(1,4),new Vector2(1,3),new Vector2(1,2),new Vector2(1,1),new Vector2(2,1),new Vector2(3,1),
        new Vector2(3,2),new Vector2(3,3),new Vector2(3,4),new Vector2(3,5),new Vector2(3,6),new Vector2(4,6),new Vector2(5,6),new Vector2(6,6),
        new Vector2(7,6),new Vector2(7,7),new Vector2(8,7),new Vector2(9,7),new Vector2(10,7),new Vector2(10,8),new Vector2(10,9),new Vector2(10,10),
        new Vector2(10,11),new Vector2(11,11),new Vector2(12,11),new Vector2(12,10),new Vector2(12,9),new Vector2(12,8),new Vector2(12,7),
        new Vector2(12,6),new Vector2(11,6),new Vector2(11,5),new Vector2(10,5),new Vector2(9,5),new Vector2(9,4),new Vector2(9,3),new Vector2(8,3),
        new Vector2(7,3),new Vector2(7,4),new Vector2(6,4),new Vector2(5,4),new Vector2(5,3),new Vector2(5,2),new Vector2(5,1),new Vector2(6,1),
        new Vector2(7,1),new Vector2(8,1),new Vector2(9,1),new Vector2(10,1),new Vector2(11,1),new Vector2(11,2),new Vector2(12,2),new Vector2(12,3),
        new Vector2(13,3),new Vector2(13,4),new Vector2(14,4),new Vector2(14,5),new Vector2(14,6),new Vector2(14,7),new Vector2(14,8),new Vector2(14,9),
        new Vector2(14,10),new Vector2(14,11),new Vector2(14,12),new Vector2(14,13),new Vector2(14,14),new Vector2(13,14),new Vector2(12,14),
        new Vector2(12,13),new Vector2(11,13),new Vector2(10,13),new Vector2(10,14),new Vector2(9,14),new Vector2(8,14),new Vector2(7,14),
        new Vector2(7,15)});

        level5.AddRange(new List<Vector2> { new Vector2(0,8), new Vector2(1,8), new Vector2(2,8), new Vector2(2,9), new Vector2(2,10), new Vector2(2,11),
        new Vector2(3,11), new Vector2(4,11), new Vector2(4,12), new Vector2(4,13), new Vector2(5,13), new Vector2(6,13), new Vector2(7,13),
        new Vector2(7,12),new Vector2(7,11),new Vector2(6,11), new Vector2(6,10), new Vector2(6,9), new Vector2(6,8), new Vector2(6,7), new Vector2(5,7), new Vector2(4,7), new Vector2(4,6),
        new Vector2(4,5), new Vector2(4,4), new Vector2(4,3), new Vector2(5,3), new Vector2(6,3), new Vector2(7,3), new Vector2(8,3),
        new Vector2(8,4), new Vector2(8,5), new Vector2(9,5), new Vector2(9,6), new Vector2(9,7), new Vector2(9,8), new Vector2(8,8),
        new Vector2(8,9), new Vector2(8,10), new Vector2(9,10), new Vector2(9,11), new Vector2(9,12), new Vector2(9,13), new Vector2(10,13),
        new Vector2(11,13), new Vector2(12,13), new Vector2(12,12), new Vector2(12,11), new Vector2(11,11), new Vector2(11,10), new Vector2(11,9),
        new Vector2(11,8), new Vector2(12,8), new Vector2(12,7), new Vector2(12,6), new Vector2(13,6), new Vector2(14,6), new Vector2(14,7),
        new Vector2(14,8), new Vector2(15,8), new Vector2(16,8)});

        level6.AddRange(new List<Vector2> { new Vector2(0,14), new Vector2(1,14), new Vector2(2,14), new Vector2(3,14), new Vector2(3,13),
        new Vector2(4,13),new Vector2(5,13),new Vector2(6,13),new Vector2(7,13),new Vector2(8,13),new Vector2(9,13),new Vector2(10,13),
        new Vector2(10,12),new Vector2(10,11),new Vector2(9,11),new Vector2(8,11),new Vector2(7,11),new Vector2(6,11),new Vector2(5,11),
        new Vector2(4,11),new Vector2(4,10),new Vector2(4,9), new Vector2(5,9),new Vector2(6,9),new Vector2(7,9),new Vector2(8,9),new Vector2(9,9),
        new Vector2(10,9),new Vector2(11,9),new Vector2(12,9),new Vector2(13,9),new Vector2(13,8),new Vector2(13,7),new Vector2(12,7),
        new Vector2(11,7),new Vector2(10,7),new Vector2(9,7),new Vector2(9,6),new Vector2(8,6),new Vector2(7,6),new Vector2(6,6),new Vector2(6,7),
        new Vector2(5,7),new Vector2(4,7),new Vector2(3,7),new Vector2(3,8),new Vector2(2,8),new Vector2(1,8),new Vector2(1,7),new Vector2(1,6),
        new Vector2(1,5),new Vector2(2,5),new Vector2(2,4),new Vector2(3,4),new Vector2(4,4),new Vector2(5,4),new Vector2(6,4),new Vector2(7,4),
        new Vector2(7,3),new Vector2(8,3),new Vector2(8,2),new Vector2(8,1),new Vector2(7,1),new Vector2(6,1),new Vector2(5,1),new Vector2(5,0)});

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
            print(gridPosition[i].Length);
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
            GameObject newBomb = Instantiate(bombPrefab, bombPos, Quaternion.identity);
            newBomb.transform.SetParent(bombs.transform);
            bombList.Add(newBomb);
            pathPosition.Remove(pathPosition[bombPosIndex]);
        }
    }
}

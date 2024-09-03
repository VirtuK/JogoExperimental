using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int width;
    public int lenght;
    public GameObject tile;
    public List<Vector3[]> gridPosition = new List<Vector3[]>();
    public Transform cam;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        int x = 0;
        for (int i = 0; i < width; i++)
        {
            List<Vector3> gridX = new List<Vector3>();
            if (x == 0)
            {
                x = 1;
            }
            else
            {
                x = 0;
            }
            for (int j = 0; j < lenght; j++)
            {
                    if (x == 0)
                    {
                        GameObject grid = Instantiate(tile, new Vector3(i, j), Quaternion.identity);
                        grid.GetComponent<SpriteRenderer>().color = Color.white;
                        grid.name = $"Tile {i} {j}";
                        gridX.Add(grid.transform.position);
                        grid.transform.SetParent(this.transform);
                        x = 1;
                        

                    }
                    else
                    {
                        GameObject grid = Instantiate(tile, new Vector3(i, j), Quaternion.identity);
                        grid.GetComponent<SpriteRenderer>().color = Color.black;
                        grid.name = $"Tile {i} {j}";
                        gridX.Add(grid.transform.position);
                        grid.transform.SetParent(this.transform);
                        x = 0;
                       
                    }
                
            }
            gridPosition.Add(gridX.ToArray());
            print(gridPosition[i].Length);
        }
        
        cam.transform.position = new Vector3((float)width / 2 - 0.5f, (float)lenght / 2 - 0.5f, -1.5f);
        setPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void setPlayer()
    {
        player.transform.position = new Vector3(Random.Range(1, width - 1), Random.Range(1, lenght - 1), -0.3f);
        player.GetComponent<Player>().checkPosition();
    }
}

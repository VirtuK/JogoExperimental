using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player2 : MonoBehaviour
{
    public Grid grid;
    public int lever1;
    public int lever2;
    public List<int[]> bombCodes = new List<int[]>();

    public Image lever1Object;
    public Image lever2Object;
    // Start is called before the first frame update
    void Start()
    {
        List<int> bombs = new List<int>();
        List<Vector2> codes = new List<Vector2>();
        bombs.AddRange(new List<int> {0,1,2,3});
        codes.AddRange(new List<Vector2> { new Vector2(0,0), new Vector2(1,0), new Vector2(1,1), new Vector2(0,1) });
        
        for(int i = 0; i < bombs.Count; i++)
        {
            List<int> bools = new List<int>();
            int r = Random.Range(0,codes.Count);
            if (codes[r].x == 1)
            {
                bools.Add(1);
            }
            else
            {
                bools.Add(0);
            }
            if (codes[r].y == 1)
            {
                bools.Add(1);
            }
            else
            {
                bools.Add(0);
            }
            bombCodes.Add(bools.ToArray());
            codes.Remove(codes[r]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            lever1 = 1;
            lever1Object.color = Color.white;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            lever1 = 0;
            lever1Object.color = Color.black;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            lever2 = 1;
            lever2Object.color = Color.white;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
           lever2 = 0;
           lever2Object.color = Color.black;
        }
        if (Input.GetKeyDown(KeyCode.E)){
            for(int i = 0; i < bombCodes.Count; i++)
            {
               if(lever1 == bombCodes[i][0] && lever2 == bombCodes[i][1])
                {
                    DetonateBomb(i);
                    bombCodes.Remove(bombCodes[i]);
                }
            }
        }
    }

    void DetonateBomb(int bombIndex)
    {
        
            if (grid.bombList[bombIndex].transform.position.x == grid.player.transform.position.x && grid.bombList[bombIndex].transform.position.y == grid.player.transform.position.y)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            Destroy(grid.bombList[bombIndex]);
            grid.bombList.Remove(grid.bombList[bombIndex]);
     
    }
}

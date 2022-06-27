using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class mapGenerator : MonoBehaviour
{
    //SLIDERS//
    [Range(0.0f, 1.0f)]
    public float threshold = 0.5f;

    [Range(0.0f, 10.0f)]
    public float foliageSparsity = 0.0f;

    //PUBLIC VARIABLES//
    public int width;
    public int height;
    public int points    = 2;
    public int numChests = 3;
    public int numPlants = 3;

    public bool showPath = true;

    public Tilemap tilemap;
    public Tilemap pathMap;

    public GameObject endpoint;
    public GameObject chest;

    public GameObject[] plants;

    public List<Tile> tiles;

    //PRIVATE VARAIBLES//
    List<List<float>> field = new List<List<float>>();
    List<GameObject>  Path  = new List<GameObject>();
    List<int>         codes = new List<int>();

    TilemapRenderer pathRenderer;

    //SETS UP SCRIPT//
    public void Start()
    {
        pathRenderer = pathMap.GetComponent<TilemapRenderer>();

        width++;
        height++;

        GenerateField();
    }

    //CREATES RANDOMISED VALUES//
    private void GenerateField()
    {
        field.Clear();
        codes.Clear();

        //DESTROYS CHESTS AND PLANTS FROM PREVIOUS MAP GENERATION//
        GameObject[] oldChests = GameObject.FindGameObjectsWithTag("chest");
        foreach (GameObject obj in oldChests)
            Destroy(obj);

        GameObject[] oldPlants = GameObject.FindGameObjectsWithTag("plant");
        foreach (GameObject obj in oldPlants)
            Destroy(obj);

        for (int x = 0; x < width; x++)
        {
            List<float> row = new List<float>();

            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1)
                    row.Add(1.0f);
                else if (x == (int)width / 2 && y == height - 1)
                    row.Add(0.0f);
                else if (y == height - 1)
                    row.Add(1.0f);
                else if (y == 0 || y == 1)
                    row.Add(0.0f);
                else
                    row.Add(Random.Range(0.0f, 1.0f));
            }

            field.Add(row);
        }

        Normalise();
    }

    //FLOORS/CEILS RANDOMS BASED ON THRESHOLD//
    private void Normalise()
    {
        for (int x = 0; x < field.Count; x++)
            for (int y = 0; y < field[x].Count; y++)
                field[x][y] = field[x][y] >= threshold ? 1.0f : 0.0f;

        //RandomPath();
        GeneratePath();
    }

    //GENERATES A RANDOMISED PATH//
    private void GeneratePath()
    {
        int amount = 20;

        List<Vector2Int> l_points = new List<Vector2Int>();
        l_points.Add(new Vector2Int(width / 2, 0));

        for (int i = 0; i < points; i++)
        {
            int x = Random.Range(1, field.Count - 1);
            int layerSize = field[x].Count / points;
            int y = Random.Range(0 + i * layerSize, layerSize + i * layerSize);

            Vector2Int newPoint = new Vector2Int(x, y);

            l_points.Add(newPoint);
            field[x][y] = 0.1f;
        }
        l_points.Add(new Vector2Int(width / 2, height - 1));

        for(int i = 0; i < l_points.Count - 1; i++)
        {
            float x = l_points[i].x;
            float y = l_points[i].y;

            float xDiff = l_points[i + 1].x - x;
            float yDiff = l_points[i + 1].y - y;

            float pathLength = Mathf.Sqrt((xDiff * xDiff) + (yDiff * yDiff));

            for (int j = 0; j < amount; j++)
            {
                x += xDiff / (float)amount;
                y += yDiff / (float)amount;

                field[(int)x][(int)y] = 0.1f;
            }
        
        }

        GenerateCells();
    }

    //MAKES A LIST OF CODES FOR EACH TILE IN THE AREA OF THE MAP//
    private void GenerateCells()
    {
        float topRight, topLeft, bottomRight, bottomLeft;
        int total;

        for (int x = 0; x < field.Count - 1; x++)
        {
            for (int y = 0; y < field[x].Count - 1; y++)
            {
                total = 0;

                topLeft = field[x][y];
                topRight = field[x + 1][y];
                bottomLeft = field[x][y + 1];
                bottomRight = field[x + 1][y + 1];

                if (topLeft == 1.0f) total += 1;
                if (topRight == 1.0f) total += 2;
                if (bottomRight == 1.0f) total += 4;
                if (bottomLeft == 1.0f) total += 8;

                if (showPath == true)
                {
                    if (topLeft == 0.1f || topRight == 0.1f || bottomLeft == 0.1f || bottomRight == 0.1f)
                    {
                        total++;
                        total *= -1;
                    }
                }

                codes.Add(total);
            }
        }

        PlaceTiles();
    }

    //SETS EACH TILE IN THE TILEMAP AREA BASED ON A LIST OF CODES//
    private void PlaceTiles()
    {
        int x = 0, y = 0;

        //Generate a random position for the chest to spawn
        List<int> empties  = new List<int>();
        List<int> chestPos = new List<int>();

        for (int i = 0; i < codes.Count; i++)
            if (codes[i] < 1)
                empties.Add(i);

        for (int i = 0; i < numChests; i++)
            chestPos.Add(empties[Random.Range(0, empties.Count - 1)]);


        for (int i = 0; i < codes.Count; i++)
        {
            if (y > (height - 2))
            {
                y = 0;
                x++;
            }

            Vector3Int tilePosition = new Vector3Int(x, -y - 1, 0);

            if (showPath == true)
            {
                pathRenderer.enabled = true;

                if (codes[i] < 0)
                {
                    pathMap.SetTile(tilePosition, tiles[tiles.Count - 1]);
                    
                    codes[i] *= -1;
                    codes[i]--;
                }

                else
                    pathMap.SetTile(tilePosition, null);                   
            }
            else
                pathRenderer.enabled = false;

            if (codes[i] != 0)
            {
                tilemap.SetTile(tilePosition, tiles[codes[i] - 1]);


                //PLANT SPAWNING CODE//
                int plantType = Random.Range(0, numPlants + (int)foliageSparsity);

                if (plantType < numPlants)
                {
                    if (codes[i] == 3)
                        Instantiate(plants[plantType], new Vector3(2 * tilePosition.x - 9, 2 * tilePosition.y + 2.6f), Quaternion.identity * Quaternion.Euler(0.0f, 0.0f, 180.0f));
                    if (codes[i] == 12)
                        Instantiate(plants[plantType], new Vector3(2 * tilePosition.x - 9, 2 * tilePosition.y + 2.6f), Quaternion.identity);
                    if (codes[i] == 6)
                        Instantiate(plants[plantType], new Vector3((2 * tilePosition.x - 9) - 0.25f, 2 * tilePosition.y + 2.6f), Quaternion.identity * Quaternion.Euler(0.0f, 0.0f, 90.0f));
                    if (codes[i] == 9)
                        Instantiate(plants[plantType], new Vector3((2 * tilePosition.x - 9) + 0.25f, 2 * tilePosition.y + 2.6f), Quaternion.identity * Quaternion.Euler(0.0f, 0.0f, 270.0f));
                }
            }

            else
                tilemap.SetTile(tilePosition, null);

            if (chestPos.Contains(i))
                Instantiate(chest, new Vector3(2 * tilePosition.x - 9, 2 * tilePosition.y + 2.5f), Quaternion.identity);

            y++;
        }
        Instantiate(endpoint, new Vector3(width / 2, (float)(tilemap.localBounds.min.y + 1.5)), Quaternion.identity);
    }

    //REGENERATES MAP ON SPACE//
    public void Update()
    {
        //if (Input.GetKeyDown("space"))
        //    GenerateField();
    }
}

/*
TILE LISt:
----------
 0 - debug tile
 1 - convex curve - top left
 2 - convex curve - top right
 3 - horizontal   - top
 4 - convex curve - bottom right
 5 - double curve - top left, bottom right
 6 - vertical     - right
 7 - convex curve - bottom left
 8 - concave curve - bottom left
 9 - vertical      - left
10 - double curve  - bottom left, top right
11 - concave curve - bottom right
12 - horizontal    - bottom
13 - concave curve - top right
14 - concave curve - top left
15 - solid
16 - debug tile
*/
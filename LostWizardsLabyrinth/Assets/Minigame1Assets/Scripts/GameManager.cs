using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Transform gameTransform;
    [SerializeField] private Transform piecePrefab;

    [SerializeField] private GameObject winPanel;

    private int emptylocation;
    private int size;
    private List<Transform> pieces;
    private List<Vector2[]> piecesUVs;

    private bool shuffling = false;

    private void CreateGamePieces(float gapThickness)
    {
        float width = 1 / (float)size;
        piecesUVs = new List<Vector2[]>();


        for(int row = 0; row < size; row++)
        {
            for(int col = 0; col < size; col++)
            {
                Transform piece = Instantiate(piecePrefab, gameTransform);
                pieces.Add(piece);

                //centering 
                piece.localPosition = new Vector3(-1 + (2 * width * col) + width, +1 - (2 * width * row) - width, 0);

                piece.localScale = ((2 * width) - gapThickness) * Vector3.one;
                piece.name = $"{(row * size) + col}";
      

              
                float gap = gapThickness / 2;
                Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                Vector2[] uv = new Vector2[4];

                uv[0] = new Vector2((width * col) + gap, 1 - ((width * (row + 1)) - gap));
                uv[1] = new Vector2((width * (col + 1)) - gap, 1 - ((width * (row + 1)) - gap));
                uv[2] = new Vector2((width * col), 1 - ((width * row) + gap));
                uv[3] = new Vector2((width * (col + 1)) - gap, 1 - ((width * row) + gap));

                piecesUVs.Add(uv);
                mesh.uv = uv;

                //make the empty piece
                if((row == size - 1) && (col == size - 1))
                {
                    emptylocation = (size * size - 1);
                    piece.gameObject.SetActive(false);
                }

            }

        }

    }

    // Start is called before the first frame update
    void Start()
    {

        size = 3;
        pieces = new List<Transform>();
        CreateGamePieces(0.01f);
        Shuffle();

        winPanel.SetActive(false);


        
    }

    // Update is called once per frame
    void Update()
    {   

        if(!shuffling && CheckCompletion())
        {

            StartCoroutine(ShowLastPiece(1f));

            //display win message
            //break out and go back to main scene

        }

       
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if(hit)
            {
                for(int i = 0; i < pieces.Count; i++)
                {
                    if(pieces[i] == hit.transform)
                    {

                        //see if any piece on its side is able to be swapped
                        //will hit a  break if a swap works
                        if(SwapIfValid(i, -size, size))
                        {
                            break;
                        }

                        if(SwapIfValid(i, +size, size))
                        {
                            break;
                        }

                        if(SwapIfValid(i, -1, 0))
                        {
                            break;
                        }
                        if(SwapIfValid(i, +1, size -1))
                        {
                            break;
                        }

                    }


                }
            }


        }

        

    }


    private IEnumerator ShowLastPiece(float delay)
    {
        yield return new WaitForSeconds(delay);

        pieces[emptylocation].gameObject.SetActive(true);

        Mesh mesh = pieces[emptylocation].GetComponent<MeshFilter>().mesh;
        mesh.uv = piecesUVs[emptylocation];


        ShowWinMessage();

    }


    private void ShowWinMessage()
    {
        if(winPanel != null)
        {

            winPanel.SetActive(true);
        }


        if(MainSceneManager.instance != null)
        {
            MainSceneManager.instance.RestorePlayerState(transform);
        }

        SceneManager.LoadScene("MainScene");

    }

    

    private bool SwapIfValid(int i, int offset, int colCheck)
    {

        if (((i % size) != colCheck) && ((i + offset) == emptylocation))
        {
            //swap the pieces
            (pieces[i], pieces[i + offset]) = (pieces[i + offset], pieces[i]);

            //swap their positions
            (pieces[i].localPosition, pieces[i + offset].localPosition) = (pieces[i + offset].localPosition, pieces[i].localPosition);

            //update the empty piece
            emptylocation = i;

            //sucessful swap
            return true;
        }

        //unsucessfull swap
        return false;
    }


    private bool CheckCompletion()
    {
        for(int i = 0; i < pieces.Count; i++)
        {
            if(pieces[i].name != $"{i}")
            {
                return false;
            }
        }
        return true;
    }


    private List<int> GetValidMoves(int emptyLocation)
    {
        //list to hold valid moves
        List<int> validMoves = new List<int>();

        //find where the empty space is
        int row = emptyLocation / size;
        int col = emptyLocation % size;

        if(row > 0)
        {
            validMoves.Add(emptyLocation - size);
        }

        if(row < size - 1)
        {
            validMoves.Add(emptyLocation + size);
        }

        if(col > 0)
        {
            validMoves.Add(emptyLocation - 1);
        }

        if(col < size -1 )
        {

            validMoves.Add(emptyLocation + 1);
        }

        return validMoves;
    }


    private void SwapPieces(int emptyLocation, int newLocation)
    {

        Transform temp = pieces[emptylocation];
        pieces[emptylocation] = pieces[newLocation];

        pieces[newLocation] = temp;

        Vector3 tempPosition = pieces[emptyLocation].localPosition;
        pieces[emptyLocation].localPosition = pieces[newLocation].localPosition;

        pieces[newLocation].localPosition = tempPosition;


    }





    private void Shuffle()
    {
        shuffling = true;


        emptylocation = (size * size) - 1;


        int shuffleCount = 100;

       for(int i = 0; i < shuffleCount; i++)
        {
            List<int> validMoves = GetValidMoves(emptylocation);
            int randomMove = validMoves[Random.Range(0, validMoves.Count)];


            SwapPieces(emptylocation, randomMove);

            emptylocation = randomMove;


        }



        shuffling = false;
    }
   
}
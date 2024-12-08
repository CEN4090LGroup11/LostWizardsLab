using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScript : MonoBehaviour
{
    GameObject gameControl;

    public Sprite [] IconFaces = new Sprite[4];
    public Sprite IconBack;
    SpriteRenderer spriteRenderer;
    public int faceIndex;

    public bool matched = false;

    public void OnMouseDown()
    {
        if (matched) return;

        if(matched == false)
        {
            if (spriteRenderer.sprite == IconBack)
            {

                //only flip card if two cards not up
                if (gameControl.GetComponent<GameController>().ShowingTwoCards() == false)
                {

                    if (faceIndex >= 0 && faceIndex < IconFaces.Length)
                    {
                        spriteRenderer.sprite = IconFaces[faceIndex];


                        Debug.Log($"Card flipped : faceIndex = {faceIndex}");

                        Debug.Log($"Visible faces before update: {gameControl.GetComponent<GameController>().visibleFaces[0]}, {gameControl.GetComponent<GameController>().visibleFaces[1]}");

                        //log it into our list of what cards are up
                        gameControl.GetComponent<GameController>().addFaces(faceIndex);

                        matched = gameControl.GetComponent<GameController>().checkMatch();

                        if(matched)
                        {

                            List<CardScript> setToTrue = gameControl.GetComponent<GameController>().findMatchedCard(faceIndex);


                            foreach(var card in setToTrue)
                            {
                                card.matched = true;
                            }

                            gameControl.GetComponent<GameController>().checkWin();

                        }

                    }
                    else
                    {
                        Debug.LogWarning($"Invalid Faceindex: {faceIndex}");
                    }

                }

            }
            else
            {
                spriteRenderer.sprite = IconBack;
                gameControl.GetComponent<GameController>().removeFaces(faceIndex);

            }
        }

    }


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameControl = GameObject.Find("GameController");
    }

    private void Start()
    {
       
    }


}

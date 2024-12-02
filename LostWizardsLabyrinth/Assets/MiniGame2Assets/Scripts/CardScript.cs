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
        if(matched == false)
        {

            if (spriteRenderer.sprite == IconBack)
            {

                //only flip card if two cards not up
                if (gameControl.GetComponent<GameController>().ShowingTwoCards() == false)
                {
                    spriteRenderer.sprite = IconFaces[faceIndex];

                    //log it into our list of what cards are up
                    gameControl.GetComponent<GameController>().addFaces(faceIndex);

                    matched = gameControl.GetComponent<GameController>().checkMatch();

                    if (matched)
                    {

                        List<CardScript> setToTrue = gameControl.GetComponent<GameController>().findMatchedCard(faceIndex);

                        setToTrue[0].matched = true;
                        setToTrue[1].matched = true;
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

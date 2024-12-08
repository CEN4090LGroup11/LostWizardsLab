using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    GameObject token;

    List<int> faceIndexes = new List<int> { 0, 1, 2, 3, 0, 1 , 2, 3};

    //will hold the cards
    List<CardScript> cards = new List<CardScript>();

    //used to randomly place the cards
    public static System.Random rand = new System.Random();
    public int shuffleNum = 0;

    //this will hold what 2 card faces are up
    public int[] visibleFaces = { -1, -2 };


    private void Start()
    {

        int ogLength = faceIndexes.Count;

        float yPosition = 2.3f;
        float xPosition = -2.2f;

        for (int i = 0; i < 7; i++)
        {
            shuffleNum = rand.Next(0, (faceIndexes.Count));

            var temp = Instantiate(token, new Vector3(
                xPosition, yPosition, 0),
                Quaternion.identity);

            //assign face based on random number generated
            temp.GetComponent<CardScript>().faceIndex = faceIndexes[shuffleNum];


            Debug.Log($"Card {i}: faceIndex = {faceIndexes[shuffleNum]}");

            faceIndexes.RemoveAt(shuffleNum);

            cards.Add(temp.GetComponent<CardScript>());


            xPosition = xPosition + 4;

            //reset next row of cards onbce half have been set
            if (i == (ogLength / 2 - 2))
            {
                yPosition = -2.3f;
                xPosition = -6.2f;
            }

        }

        if(faceIndexes.Count > 0)
        {
            token.GetComponent<CardScript>().faceIndex = faceIndexes[0];
        }
        else
        {
            Debug.LogError("faceIndexes is empty after card assignemnt");
        }
        
       
    }


    private void Awake()
    {
        token = GameObject.Find("Card");
    }


    public bool ShowingTwoCards()
    {
        bool twoCards = false;

        //if 2 valid indexes then you know 2 cards up
        if(visibleFaces[0] >= 0 && visibleFaces[1] >= 0)
        {
            twoCards = true;
        }

        return twoCards;
    }


    //func to help keep track of what cards we have up currently
    public void addFaces(int index)
    {
        if(visibleFaces[0] == -1)
        {
            visibleFaces[0] = index;
        }
        else if(visibleFaces[1] == -2)
        {
            visibleFaces[1] = index;
        }


        Debug.Log($"Add faces: visible faces after = {visibleFaces[0]}, {visibleFaces[1]}");

    }

    public void removeFaces(int index)
    {
        if (visibleFaces[0] == index)
        {
            visibleFaces[0] = -1;
        }
        else if (visibleFaces[1] == index)
        {
            visibleFaces[1] = -2;
        }
    }


    //func to check if the two cards we have up match
    public bool checkMatch()
    {
        bool success = false;

        if(visibleFaces[0] == visibleFaces[1])
        {
            success = true;

            //take them out of array bc we will remove them once match
            visibleFaces[0] = -1;
            visibleFaces[1] = -2;

        }

        return success;
    }



    //maybe have a func that find the indexes of cards that are matching
    public List<CardScript> findMatchedCard(int index)
    {
        List<CardScript> pair = new List<CardScript>();

        foreach(CardScript card in cards)
        {
            if(card.GetComponent<CardScript>().faceIndex == index)
            {
                pair.Add(card);
            }
        }

        return pair;
    }




    public void checkWin()
    {

        int matchedCount = 0;

        foreach (CardScript card in cards)
        {

            if(card.matched)
            {
                matchedCount++;
            }
        }



        if(matchedCount == cards.Count)
        {
            //switch back to main game scene
            SceneManager.LoadScene("MainScene");
        }
    }
}

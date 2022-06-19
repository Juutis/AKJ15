using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReputationManager
{
    public int StateReputation = 0, RebelReputation = 0;

    public void UpdateStateReputation(int totalBooksForTheDay, int totalBooksHandled, int correctlySavedBooks)
    {
        int booksNotHandled = totalBooksForTheDay - totalBooksHandled;
        StateReputation -= booksNotHandled;
        StateReputation += correctlySavedBooks / 2;
    }

    public void UpdateRebelReputation(int totalAvailableRebelBooks, int rebelBooksDelivered)
    {
        RebelReputation += rebelBooksDelivered - 2;
    }
}

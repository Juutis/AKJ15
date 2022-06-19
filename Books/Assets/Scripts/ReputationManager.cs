using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReputationManager
{
    public int StateReputation = 0, RebelReputation = 0;

    public void UpdateStateReputation(int totalBooksForTheDay, int totalBooksHandled, int correctlySavedBooks, int incorrectlySavedBooks)
    {
        int booksNotHandled = totalBooksForTheDay - totalBooksHandled;
        StateReputation -= booksNotHandled;
        StateReputation -= 2 * incorrectlySavedBooks;
        StateReputation += correctlySavedBooks / 2;
    }

    public void UpdateRebelReputation(int totalAvailableRebelBooks, int rebelBooksDelivered)
    {
        RebelReputation += rebelBooksDelivered - 2;
    }

    public string GetStateReputationText()
    {
        if (StateReputation > 10)
        {
            return "Other than that, you're a good worker! You make the glorious leaders proud!";
        }
        else if (StateReputation > 5)
        {
            return "See you tomorrow.";
        }
        else if (StateReputation > 0)
        {
            return "See you tomorrow.";
        }
        else if (StateReputation > -5)
        {
            return "I am not pleased, you really need to start picking up the slack.";
        }
        else if (StateReputation > -10)
        {
            return "Remember, you are very easily replaced.";
        }
        else
        {
            return "That's it! You're unredeemable! I will get a replacement for you starting tomorrow!";
        }
    }

    public string GetStateDayResult(int totalBooksForTheDay, int totalBooksHandled, int correctlySavedBooks, int incorrectlySavedBooks)
    {
        int booksNotHandled = totalBooksForTheDay - totalBooksHandled;

        if (totalBooksHandled == 0)
        {
            return "What's the matter? Can't read? Stop sitting on your ass!";
        }
        if (incorrectlySavedBooks > 0)
        {
            return "You dimwit! Some forbidden books made it through to the national library! Make sure you read the rules again!";
        }
        if (correctlySavedBooks == 0)
        {
            return "I hate books as much as anybody, but we are in trouble if you just burn everything!";
        }
        if (booksNotHandled > 0)
        {
            return "Can't keep up with the pace? Better learn to read faster, then!";
        }

        return "The shift's over.";
    }

    public string GetRebelReputationText()
    {
        if (RebelReputation > 10)
        {
            return "Revolution! Thanks to the books you've delivered we've raised enough awareness to overthrow the evil regime!";
        }
        else if (RebelReputation > 5)
        {
            return "The books you've delivered are really helping our cause.";
        }
        else if (RebelReputation > 0)
        {
            return "";
        }
        else if (RebelReputation > -5)
        {
            return "I really need you to bring me those books. For the people, remember?";
        }
        else if (RebelReputation > -10)
        {
            return "I'm starting to doubt whether you want to help the oppressed people at all.";
        }
        else
        {
            return "We trusted you, but you seem to have sided with the oppressors. I can't let you rat us out. Sorry, but you're leaving us no choice.";
        }
    }

    public string GetRebelDayResult(int totalAvailableRebelBooks, int rebelBooksDelivered, int otherBooksDelivered)
    {
        if (rebelBooksDelivered == totalAvailableRebelBooks)
        {
            return "Great work! You're really making a difference!";
        }
        if (rebelBooksDelivered == 0 && otherBooksDelivered == 0)
        {
            return "Nothing? Do you not care about the people at all?";
        }
        if (rebelBooksDelivered == 0 && otherBooksDelivered > 0)
        {
            return "What is this crap!? I asked you to bring me books about freedom and liberty!";
        }
        if ((float)rebelBooksDelivered / totalAvailableRebelBooks > 0.5f)
        {
            return "Good job. Keep the books coming.";
        }

        return "A decent delivery, I guess. Maybe try a bit harder tomorrow.";
    }
}

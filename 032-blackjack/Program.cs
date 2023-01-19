//*Blackjack
//*Lukas Larndorfer
#region Var defining
Console.OutputEncoding = System.Text.Encoding.Default;
int money = 100;
Boolean hit = true;
Boolean Playagain = false;
string Playagaininput;
string HoS;
int playersdeck = 0;
int dealersdeck = 0;
int card;
int bet = 0;
int winner = 0;
Boolean betentered = false;
#endregion
void IntializeGame()
{
    System.Console.WriteLine("Welcome to Blackjack!");
    playersdeck = 0;
    dealersdeck = 0;
    bet = 0;
    winner = 0;
    betentered = false;
    hit = true;
}
void HandoutRandomCard()
{
    card = Random.Shared.Next(1, 14);
    if (card == 1 && playersdeck < 11)
    { playersdeck += 11; }
    else if (card == 1 && playersdeck > 11)
    { playersdeck += 1; }
    else if (card == 11 || card == 12 || card == 13)
    { playersdeck += 10; }
    else { playersdeck += card; }
    if (card == 13)
    {
        card = 10;
    }

}
void PrintCard()
{
    if (card == 1)
    {
        System.Console.WriteLine($"Card: Ass. Your current value is: {playersdeck}.");
    }
    else if (card == 11)
    {
        System.Console.WriteLine($"Card: Jack. Your current value is: {playersdeck}.");
    }
    else if (card == 12)
    {
        System.Console.WriteLine($"Card: Queen. Your current value is: {playersdeck}.");
    }
    else if (card == 13)
    {
        System.Console.WriteLine($"Card: King. Your current value is: {playersdeck}.");
    }
    else
    {
        System.Console.WriteLine($"Card: {card}. Your current value is: {playersdeck}.");
    }
}
void AskForBet()
{
    while (!betentered)
    {
        System.Console.WriteLine("How much do you want to bet? Minimum Amount is 10€: ");
        bet = int.Parse(Console.ReadLine()!);
        if (bet < 10 || bet > money)
        { System.Console.WriteLine("You've entered something wrong. Notice that the minimum amount is 10€ and that the bet shouldn't be greater than your money! \nPlease try again: "); }
        else
        {
            betentered = true;
            money -= bet;
        }
    }
}
void HitOrStay()
{
    while (playersdeck < 21 && hit)
    {
        System.Console.WriteLine("Do you want to [H]it or [S]tay?");
        HoS = Console.ReadLine()!;
        if (HoS == "H")
        {
            HandoutRandomCard();
            if (playersdeck > 21)
            {
                System.Console.WriteLine($"You've busted! Value: {playersdeck}");
                winner = 2;

            }
            else
            {
                PrintCard();
            }
        }
        else if (HoS == "S")
        { hit = false; }
        else { System.Console.WriteLine("You've entered something wrong. Try again!"); }
    }

}
void DealersTurn()
{
    while (playersdeck > dealersdeck && dealersdeck < 17)
    {
        card = Random.Shared.Next(1, 14);
        if (card == 1 && dealersdeck < 11)
        { dealersdeck += 11; }
        else if (card == 1 && dealersdeck > 11)
        { dealersdeck += 10;}
        else if (card == 11 || card == 12 || card == 13)
        { dealersdeck += 10; }
        else { dealersdeck += card; }
        System.Console.WriteLine($"Dealers Value: {dealersdeck} ");
        Thread.Sleep(1000);
    }

}
void AnalyzeWinner()
{
    if (playersdeck == 21)
    {
        System.Console.WriteLine($"Winner: Player 1!");
        winner = 1;
    }
    else if (playersdeck > 21)
    {
        System.Console.WriteLine($"Winner: Bank");
        winner = 2;
    }
    else if (dealersdeck > playersdeck && dealersdeck <= 21)
    {
        System.Console.WriteLine($"Winner: Bank");
        winner = 2;
    }
    else if (playersdeck > dealersdeck && playersdeck <= 21)
    {
        System.Console.WriteLine($"Winner: Player 1");
        winner = 1;
    }
    else if (dealersdeck > 21)
    {
        System.Console.WriteLine($"Winner: Player 1");
        winner = 1;
    }

    else if (dealersdeck == playersdeck)
    {
        winner = 3;
    }

    if (winner == 1)
    {
        money = (bet * 2) + money;
        System.Console.WriteLine("You've won! You doubled your money and now you got " + money + "!");
    }
    else if (winner == 2)
    {
        System.Console.WriteLine($"Sorry, you've lost. Remaining money: {money}");
    }
    else if (winner == 3)
    {
        money += bet;
        System.Console.WriteLine("There is a draw. You got your money back. Remaining money: " + money + ".");
    }
}
void PlayAgain()
{
    if (money > 9)
    {
        System.Console.WriteLine("Wanna play again? [Y]es or [N]o");
        Playagaininput = Console.ReadLine()!;
        if (money > 200)
        {
            System.Console.WriteLine("You have at least doubled your money! Now you have to go.");
            Playagain = false;
        }
        else if (Playagaininput == "Y")
        {
            Playagain = true;
        }
        else if (Playagaininput == "N")
        {
            Playagain = false;
        }

    }
}
void MainGame()
{
do
{
    IntializeGame();
    HandoutRandomCard();
    PrintCard();
    AskForBet();
    HandoutRandomCard();
    PrintCard();
    HitOrStay();
    if (playersdeck < 21)
    {
        DealersTurn();
    }
    AnalyzeWinner();
    PlayAgain();
}
while (Playagain == true);
System.Console.WriteLine($"Heres your money: {money}€! \nHave a great day!");
}

MainGame();
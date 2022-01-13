namespace Main;

public class Deposit
{
    public void DepositMenu()
    {
        // TODO

        // show the current users account

        var accountSelected = Console.ReadLine();

        // two ways we can do this either pass in account number of do it by the index of the list
        // easiest in my opinion is i pass account number you cross check it with db and that the current user
        // owns that account and we should be good.

        // i could possible make a singleton class within this project so once we login i can keep the user's customer id
        // so all i need to pass to u is the customer id

        // example you can change it 
        // SQL SELECT accountNumber from Accounts WHERE customerid == customerid <== something like this 
        // this method should return a list of ints so then i will then check if the accountSelected is within your list

        // then make me another method that takes in accountNumberSelected and amountToBeDeposited and a comment AND accountNumber
        // all you do is insert this into the database all those 4 and the db should update accordingly

        // last method is give me the balance of the current account i give you accountNumber 
    }
}
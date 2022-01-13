namespace Main.Sql;

public class Withdraw
{
    public void WithdrawMenu()
    {
        // TODO

        // show the account of the user

        var accountSelected = Console.ReadLine();

        // same as deposit 


        // somehow keep track of the service fee
        // my opinion do a sql query
        // SELECT TransactionType.Count() FROM Transaction WHERE TRANSACTIONTYPE = 'W || TRANSACTIONTYPE = "T' AND accountnumber = acocuntnumber
        // this should give 0  if they have done no withdraw 
        // make a method all i give you is acocuntnumber u give me back an int 

        // when we withdraw i will pass you accountNumeberSelected, amountToBeDeposited and comment and how many withdraws
        // they have made based on this number you will do the service fee charge

        // last method is give me the balance of the current account i give you accountNumber 
    }
}
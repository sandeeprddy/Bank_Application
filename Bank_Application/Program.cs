using Models;
using AllServices;

Dictionary<string, Bank> AllBanks = new();

Console.WriteLine("Welcome..");

Boolean isSessionComplete = false;

while (!isSessionComplete)
{

    Boolean isCustomerSelectedCorrectOptions = false;

    while (!isCustomerSelectedCorrectOptions)
    {

        Console.WriteLine("1.Customer\n2.Staff\n3.Exit");

        string? userSelectionOption = Console.ReadLine();

        switch (userSelectionOption)
        {
            case "1":
                LoginAsAccountHolder();
                isCustomerSelectedCorrectOptions = true;
                break;
            case "2":
                Console.WriteLine("1.Admin\n2.Staff");
                string staffRole = Console.ReadLine();
                switch (staffRole)
                {
                    case "1":
                        Boolean validAdminCredentials = false;
                        while (!validAdminCredentials)
                        {
                            Console.WriteLine("Enter admin ID and Password");
                            string adminId = Console.ReadLine();
                            string adminPassword = Console.ReadLine();
                            
                            if (ValidationServices.ValidateAdmin(adminId, adminPassword))
                            {
                                validAdminCredentials = true;
                                Console.WriteLine("Welcome Admin");
                            }
                            else
                            {
                                Console.WriteLine("Incorrect Credentials");
                            }
                        }
                        LoginAsAdmin();
                        break;
                    case "2":
                        LoginAsStaff();
                        break;
                    default:
                        Console.WriteLine("Please select a valid option");
                        break;
                }
                isCustomerSelectedCorrectOptions = true;
                break;
            default:
                Console.WriteLine("Invalid option\nPlease select valid option");
                break;
        }
    }
}
void LoginAsAccountHolder()
{
    Console.WriteLine("Please Enter your Bank Name.");

    string BankName = "";

    InputAndValidateBank(ref BankName);

    Bank currentUsersBank = AllBanks[key : BankName];

    Console.WriteLine("Please Enter your Account ID");

    string AccountID = "";

    InputAndValidateAccount(currentUsersBank, ref AccountID);

    Customer currentUsersAccount = currentUsersBank.CustomerAccounts[AccountID];

    Boolean isCustomerSessionComplete = false;
    
    while (!isCustomerSessionComplete)
    {
        Console.WriteLine("1.Deposit amount\n2.Withdraw amount\n3.Transfer amount\n4.View Transactions\n5.Exit");

        string customerOptions = Console.ReadLine();

        switch (customerOptions)
        {
            case "1":
                Console.WriteLine("Enter amount to deposit");
                double moneyToDeposit = Convert.ToDouble(Console.ReadLine());
                CustomerService.Deposit(currentUsersAccount,moneyToDeposit);
                Console.WriteLine("Amount deposited successfully");
                Console.WriteLine("Current Balance :" + currentUsersAccount.Balance + "" + currentUsersBank.currency);
                break;
            case "2":
                WithdrawAmount(currentUsersAccount);
                break;
            case "3":
                TransferAmount(currentUsersAccount);
                Console.WriteLine("Amount transferred successfully");
                Console.WriteLine("Current Balance :" + currentUsersAccount.Balance);
                break;
            case "4":
                CustomerService.ViewTransaction(currentUsersAccount);
                break;
            case "5": 
                isCustomerSessionComplete=true; 
                break;
            default:
                Console.WriteLine("Invalid Option.\nPlease Select valid option");
                break;
        }
    }
}
void LoginAsStaff()
{

    string staffBankName = null;
    Bank staffBank = null;
    Staff staffAccount = null;

    InputAndValidateStaff(ref staffBankName,ref staffBank, ref staffAccount);

    Boolean isStaffSessionComplete = false;

    while (!isStaffSessionComplete)
    {
        Console.WriteLine("1.Create new account\n2.Update account\n3.Delete account\n4.Add new Accepted currency with exchange rate");
        Console.WriteLine("5.Add service charge for same bank account\n6.Add service charge for other bank account");
        Console.WriteLine("7.Can view account transaction history\n8.Revert a transaction\n9.View All Customers\n10.Exit");


        string staffOptions = Console.ReadLine();

        switch (staffOptions)
        {
            case "1":
                CreateNewAccount(staffBank);
                break;

            case "2":
                UpdateAccount(staffBank);
                break;

            case "3":
                Console.WriteLine("Enter ID of Account to delete");
                string accountIDToDelete = "";
                InputAndValidateAccount(staffBank, ref accountIDToDelete);
                StaffService.DeleteAccount(staffBank,accountIDToDelete);
                Console.WriteLine("Account deleted successfully");
                break;

            case "4":
                AddCurrencyToAcceptedCurrencies(staffBank.ID);
                break;

            case "5":
                AddServiceChargeForSameBank(staffBank);
                break;

            case "6":
                AddServiceChargeForTransferringBank(staffBank);
                break;

            case "7": 
                Console.WriteLine("Enter the account ID");
                string customerAccountIDToViewTransactions = "";
                InputAndValidateAccount(staffBank,ref customerAccountIDToViewTransactions);
                StaffService.ViewTransaction(staffBank.CustomerAccounts[customerAccountIDToViewTransactions]);
                break;

            case "8":

                Console.WriteLine("Enter customers Ids participated in the transaction");

                string senderAccountId = "";

                InputAndValidateAccount(staffBank,ref senderAccountId);

                Customer sender = staffBank.CustomerAccounts[senderAccountId];

                Console.WriteLine("Enter Receiver's Bank Name");

                string receiverBankName = "";

                InputAndValidateBank(ref receiverBankName);

                Bank receiverBank = AllBanks[receiverBankName];

                Console.WriteLine("Enter receiver Account Id");

                string receiverAccountId = "";

                InputAndValidateAccount(receiverBank,ref receiverAccountId);

                Customer receiver = receiverBank.CustomerAccounts[receiverAccountId];

                RevertTransaction(sender, receiver);

                break;

            case "9":  ViewAllCustomers(staffBank);
                break;

            case "10": isStaffSessionComplete = true; 
                break;

            default : Console.WriteLine("Invalid option\nPlease select a valid option");
                break;
        }
    }
}

void LoginAsAdmin() {

    Boolean isAdminSessionCompleted = false;

    while (!isAdminSessionCompleted)
    {
        Console.WriteLine("1.Create a Bank\n2.Add Staff to an existing Bank\n3.View All staff and their details\n4.Change Bank's Default operating currency\n5.Exit");

        string adminWorkOptions = Console.ReadLine();

        switch (adminWorkOptions)
        {
            case "1":
                CreateBank();
                break;
            case "2":
                AddStaffToExistingBank();
                break;
            case "3":
                ViewAllStaff();
                break;
            case "4":
                ChangeBankDefaultCurrency();
                break;
             case "5":
                isAdminSessionCompleted = true;
                break;
            default:
                Console.WriteLine("Invalid option\n Please select valid option");
                break;
        }
    }


}

void CreateBank() {

    Boolean isNewBankValid = false;

    string newBankName = "";

    string newBankLocation = "";

    while (!isNewBankValid)
    {
        Console.WriteLine("Enter New Bank Name");
        newBankName = Console.ReadLine();
        Console.WriteLine("Enter Bank Location");
        newBankLocation = Console.ReadLine();
        if (AllBanks.ContainsKey(newBankName.ToUpper()))
        {
            Console.WriteLine("Bank already exists with the same name\nPlease enter a new name for the bank");
        }
        else
        {
            isNewBankValid = true;
        }
    }
    AdminServices.CreateBank(newBankName, newBankLocation, AllBanks);
    Console.WriteLine("New Bank " + newBankName.ToUpper() + " Added successfully");
}

void InputAndValidateBank(ref string BankName)
{
    bool isCorrectBank = false;


    while (!isCorrectBank)
    {
        BankName = Console.ReadLine();

        if (ValidationServices.ValidateBank(BankName, AllBanks))
        {
            BankName = BankName.ToUpper();
            isCorrectBank = true;
        }
        else
        {
            Console.WriteLine("Please Enter valid Bank");
        }
    }
}

void InputAndValidateStaff(ref string staffBankName, ref Bank staffBank, ref Staff staffAccount)
{

    bool validStaffCredentials = false;

    Console.WriteLine("Enter Bank Name");

    InputAndValidateBank(ref staffBankName);

    staffBank = AllBanks[staffBankName];

    while (!validStaffCredentials)
    {
        Console.WriteLine("Enter staff ID and password");
        string staffID = Console.ReadLine();
        string staffPassword = Console.ReadLine();

        if (ValidationServices.ValidateStaff(staffID, staffPassword, staffBank)) 
        {
                staffAccount = staffBank.StaffAccounts[staffID];
                Console.WriteLine("Welcome " + staffAccount.FirstName);
                validStaffCredentials = true;
        }
        else
        {
            Console.WriteLine("Incorrect Credentials");
        }
    }

}

void ChangeBankDefaultCurrency()
{
    Console.WriteLine("Enter Bank name for which the default currency to be updated");

    string targetBankId = "";

    InputAndValidateBank(ref targetBankId);

    Console.WriteLine("Default operating currency of " + AllBanks[targetBankId].Name + " is " + AllBanks[targetBankId].currency);

    Console.WriteLine("1.Change default currency from Existing currencies\n2.Add new default currency\n3.Exit");

    string changeCurrencyOptions = "";

    Boolean isCorrectCurrencyChangeOptionSelected = false;

    while (!isCorrectCurrencyChangeOptionSelected)
    {
        changeCurrencyOptions = Console.ReadLine();

        switch (changeCurrencyOptions)
        {
            case "1":
                ChangeDefaultCurrencyFromExistingCurrencies(targetBankId);
                isCorrectCurrencyChangeOptionSelected = true;
                break;
            case "2":
                AddNewDefaultCurrency(targetBankId);
                isCorrectCurrencyChangeOptionSelected = true;
                break;
            case "3":
                isCorrectCurrencyChangeOptionSelected = true;
                break;
            default:
                Console.WriteLine("Invalid Option\nPlease choose a valid option");
                break;
        }
    }
}

void AddNewDefaultCurrency(string targetBankId)
{
    Console.WriteLine("Enter the Code of new Currency(Three letter code)");

    string currencyCode = Console.ReadLine();

    Console.WriteLine("Enter the Exchange value of new Currency to INR");

    double currencyExchangeValue = Convert.ToDouble(Console.ReadLine());

    AdminServices.ChangeDefaultCurrency(targetBankId,AllBanks,currencyCode);

    AdminServices.AddCurrencyToAcceptedCurrencies(targetBankId, AllBanks, currencyCode, currencyExchangeValue);

    Console.WriteLine("New default Currency added successfully");

}

void ChangeDefaultCurrencyFromExistingCurrencies(string targetBankId)
{

    Boolean isCorrectCurrencySelected = false;

    Console.WriteLine("Change default currency to");

    AdminServices.DisplayAcceptedCurrencies(targetBankId, AllBanks);

    while (!isCorrectCurrencySelected)
    {

        Console.WriteLine("Enter the currency code to change to");

        String updatedBankCurrency = (Console.ReadLine());

         if(ValidationServices.ValidateBankCurrency(targetBankId, AllBanks, updatedBankCurrency))
         {
            AdminServices.ChangeDefaultCurrency(targetBankId, AllBanks, updatedBankCurrency);
            Console.WriteLine("Default currency changed to " + updatedBankCurrency.ToUpper() + " successfully");
            isCorrectCurrencySelected = true;
        }
        else
        {
            Console.WriteLine("Please enter currency code from existing accepted currencies");
        }
    }
}

void AddStaffToExistingBank()
{
    Console.WriteLine("Enter Bank Name");
    string BankName = "";
    InputAndValidateBank(ref BankName);
    Bank TargetBank = AllBanks[BankName];

    //creating staff account
    Console.WriteLine("Creating Staff account");
    Console.WriteLine("Enter First Name");
    string firstName = Console.ReadLine();
    Console.WriteLine("Enter Last Name");
    string lastName = Console.ReadLine();

    string email = "";

    ValidateEmail(ref email);
   
    string password = "";

    ValidatePassword(ref password);

    
    AdminServices.CreateNewStaffAccount(firstName, lastName, email, password, TargetBank);

    Console.WriteLine("Account created successfully");
}

void ValidateEmail(ref string email)
{
    bool isEmailStrong = false;


    while (!isEmailStrong)
    {
        Console.WriteLine("Enter Email");

        email = Console.ReadLine();

        if (ValidationServices.EmailValidator(email)) 
        {
            isEmailStrong = true;
        }
        else
        {
            Console.WriteLine("Please enter a valid email");
        }
    }
}

void ValidatePassword(ref string password)
{
    bool ispasswordStrong = false;

 
    while (!ispasswordStrong)
    {
        Console.WriteLine("Enter password");

        password = Console.ReadLine();

        if (ValidationServices.PasswordValidator(password)) 
        {
            ispasswordStrong = true;
        }
        else
        {
            Console.WriteLine("Please include atleast 1 capital letter, 1 small letter and password should be atleast 8 characters long");
        }
    }
}

void ViewAllStaff()
{
    Console.WriteLine("Enter the bank Name");
    string bankName = "";
    InputAndValidateBank(ref bankName);
    Bank Bank = AllBanks[bankName];

    int i = 1;

    foreach (KeyValuePair<string, Staff> staff in Bank.StaffAccounts)
    {
        Console.WriteLine("staff No." + i);
        Console.WriteLine("\n\n");
        Console.WriteLine("                     Account ID : " + staff.Value.ID);
        Console.WriteLine("                     First Name : " + staff.Value.FirstName);
        Console.WriteLine("                     Last Name : " + staff.Value.LastName);
        Console.WriteLine("                     Email : " + staff.Value.Email);

        Console.WriteLine("\n\n");
        i++;
    }
}

void ViewAllCustomers(Bank staffBank)
{

    int i = 1;

    foreach (KeyValuePair<string, Customer> customer in staffBank.CustomerAccounts)
    {
        Console.WriteLine("Customer No." + i);
        Console.WriteLine("\n\n");
        Console.WriteLine("                     Account ID : " + customer.Value.ID);
        Console.WriteLine("                     First Name : " + customer.Value.FirstName);
        Console.WriteLine("                     Last Name : " + customer.Value.LastName);
        Console.WriteLine("                     Email : " + customer.Value.Email);

        Console.WriteLine("\n\n");
        i++;
    }
}

void InputAndValidateAccount(Bank currentUsersBank, ref string AccountID)
{

    bool isCorrectAccount = false;

    while (!isCorrectAccount)
    {
        AccountID = Console.ReadLine();

        if (ValidationServices.ValidateAccount(currentUsersBank, AccountID))
        {
            isCorrectAccount = true;
        }
        else
        {
            Console.WriteLine("Please Enter valid Account Id");
        }

    }
}

void WithdrawAmount(Customer customer)
{
    Console.WriteLine("Enter amount to Debit");
    double moneyToWithdraw = Convert.ToDouble(Console.ReadLine());
    StaffService.ValidateAmountForDebit(customer, moneyToWithdraw);
    CustomerService.Withdraw(customer, moneyToWithdraw);
    Console.WriteLine("Amount withdrawn successfully");
    Console.WriteLine("Current Balance :" + customer.Balance);

}

void TransferAmount(Customer sender)
{
    Console.WriteLine("Enter recievers Bank Name");
    
    string receiversBankName = "";
    
    InputAndValidateBank(ref receiversBankName);

    Bank receiversBank = AdminServices.GetBank(receiversBankName, AllBanks);
    
    Bank SendersBank = AdminServices.GetBank(sender.BankName, AllBanks);

    if (!StaffService.ValidateTransaction(receiversBank, SendersBank.currency))
    { 
        Console.WriteLine("Cannot perform the transaction");
        Console.WriteLine(receiversBankName + " not accepting amount sent in " + SendersBank.currency);
    }

    Console.WriteLine("Enter recievers account number");
    string receiversAccountNumber = Console.ReadLine();
    Customer receiversAccount = StaffService.GetAccount(receiversBank, receiversAccountNumber);
    Console.WriteLine("Enter amount to transfer");
    double moneyToTransfer = Convert.ToDouble(Console.ReadLine());
    Console.WriteLine("Enter Transaction Type");

    DisplayTransactionTypes();

    string transactionType = null;

    SelectTransactionType(ref transactionType);

    double transactionCharge = 0;

    GenerateTransactionCharge(ref transactionCharge, SendersBank.Name, receiversBankName, transactionType,moneyToTransfer,SendersBank);

    double receiverAccountAmountCredited = moneyToTransfer;

    moneyToTransfer += transactionCharge;

    double senderAccountAmountDebited = moneyToTransfer;

    if (!StaffService.ValidateAmountForDebit(sender, moneyToTransfer))
    {
        Console.WriteLine("Insufficient Funds");
    }
    else
    {
        CustomerService.Transfer(sender, receiversAccount, SendersBank, receiversBank, moneyToTransfer, transactionCharge, senderAccountAmountDebited, ref receiverAccountAmountCredited);
    }

}
void DisplayTransactionTypes()
{
    int i = 1;
    foreach (string transactionTypes in Enum.GetNames(typeof(TransactionTypes.Types)))
    {
        Console.WriteLine(i + "." + transactionTypes);
        i++;
    }
}

void SelectTransactionType(ref string transactionType)
{
    Boolean isCorrectTransaction = false;
    while (!isCorrectTransaction)
    {
        string transactionOption = Console.ReadLine();

        switch (transactionOption)
        {
            case "1":
                transactionType = TransactionTypes.Types.RTGS.ToString();
                isCorrectTransaction = true;
                break;
            case "2":
                transactionType = TransactionTypes.Types.IMPS.ToString();
                isCorrectTransaction = true;
                break;
            default:
                Console.WriteLine("Invalid options.\nPlease select valid option");
                break;
        }
    }
}

void GenerateTransactionCharge(ref double transactionCharge, string senderBankName, string receiversBankName,string transactionType,double moneyToTransfer, Bank SenderBank)
{
    if (senderBankName == receiversBankName)
    {
        if (transactionType == "RTGS")
        {
            transactionCharge = (SenderBank.RTGSCharge) * moneyToTransfer;
        }
        else if (transactionType == "IMPS")
        {
            transactionCharge = (SenderBank.IMPSCharge) * moneyToTransfer;
        }
    }
    else
    {
        if (transactionType == "RTGS")
        {
            transactionCharge = (SenderBank.TransferRTGSCharge) * moneyToTransfer;
        }
        else if (transactionType == "IMPS")
        {
            transactionCharge = (SenderBank.TransferIMPSCharge) * moneyToTransfer;
        }

    }
}

 void CreateNewAccount(Bank staffBank)
{
    Console.WriteLine("Creating new account");
    Console.WriteLine("Enter First Name");
    string firstName = Console.ReadLine();
    Console.WriteLine("Enter Last Name");
    string lastName = Console.ReadLine();

    string email = "";

    ValidateEmail(ref email);

    string password = "";

    ValidatePassword(ref password);

    StaffService.CreateCustomerAccount(firstName, lastName, email, password, staffBank);

    Console.WriteLine("Account created successfully");
}

void UpdateAccount(Bank staffBank)
{
    Console.WriteLine("Updating account");

    Console.WriteLine("Enter the ID of the account to update");

    string accountIDToUpdate = Console.ReadLine();

    Customer accountToUpdate = staffBank.CustomerAccounts[accountIDToUpdate];

    Console.WriteLine("Select the fields to update");

    Console.WriteLine("1.First Name\n2.Last Name\n3.Email\n4.Password");

    string updateOptionsForAccount = Console.ReadLine();

    Boolean correctOptionSelected = false;

    while (!correctOptionSelected)
    {
        switch (updateOptionsForAccount)
        {
            case "1":
                Console.WriteLine("Enter new first name");
                accountToUpdate.FirstName = Console.ReadLine();
                Console.WriteLine("First Name updated successfully");
                correctOptionSelected = true;
                break;
            case "2":
                Console.WriteLine("Enter new Last name");
                accountToUpdate.LastName = Console.ReadLine();
                Console.WriteLine("Last Name updated successfully");
                correctOptionSelected = true;
                break;
            case "3":
                Console.WriteLine("Enter new email");
                accountToUpdate.Email = Console.ReadLine();
                Console.WriteLine("email updated successfully");
                correctOptionSelected = true;
                break;
            case "4":
                Console.WriteLine("Enter new password");
                accountToUpdate.Password = Console.ReadLine();
                Console.WriteLine("Password updated successfully");
                correctOptionSelected = true;
                break;
            default:
                Console.WriteLine("Please select a valid option");
                break;
        }
    }
}

void AddCurrencyToAcceptedCurrencies(string BankID)
{
    Console.WriteLine("Existing Accepted currencies and their exchange values to Rupee are ");

    AdminServices.DisplayAcceptedCurrencies(BankID, AllBanks);

    Console.WriteLine("Enter the Code of new Currency(Three letter code)");

    string currencyCode = Console.ReadLine();

    Console.WriteLine("Enter the Exchange value of new Currency to INR");

    double currencyExchangeValue = Convert.ToDouble(Console.ReadLine());

    AdminServices.AddCurrencyToAcceptedCurrencies(BankID, AllBanks, currencyCode, currencyExchangeValue);

    Console.WriteLine("New Accepted currency added successfully");

}

void AddServiceChargeForSameBank(Bank staffBank)
{
    Console.WriteLine("Add RTGS Charge for the bank");
    double updatedRTGSCharge = Convert.ToDouble(Console.ReadLine());
    Console.WriteLine("Add IMPS Charge for the bank");
    double updatedIMPSCharge = Convert.ToDouble(Console.ReadLine());
    StaffService.AddServiceChargeForSameBank(staffBank, updatedRTGSCharge, updatedIMPSCharge);
    Console.WriteLine("Charges updates successfully");
}

void AddServiceChargeForTransferringBank(Bank staffBank)
{
    Console.WriteLine("Add RTGS Charge for other bank");
    double updatedRTGSChargeToOtherBank = Convert.ToDouble(Console.ReadLine());
    Console.WriteLine("Add IMPS Charge for other bank");
    double updatedIMPSChargeToOtherBank = Convert.ToDouble(Console.ReadLine());
    StaffService.AddServiceChargeForTransferringBank(staffBank, updatedRTGSChargeToOtherBank , updatedIMPSChargeToOtherBank);
   Console.WriteLine("Charges updates successfully");
}

void RevertTransaction(Customer sender, Customer receiver)
{

    Console.WriteLine("List of Transactions of" + sender.FirstName + "are \n");

    foreach (Transaction transactions in sender.Transactions.Values)
    {

            Console.WriteLine(transactions.Id);
    };

    Console.WriteLine("\nEnter transaction ID");

    string senderTransactionId = Console.ReadLine();

    while (!ValidationServices.ValidateTransaction(sender, senderTransactionId))
    {
        Console.WriteLine("Enter valid transaction id");
        senderTransactionId = Console.ReadLine();
    }

    Transaction SenderTransaction = sender.Transactions[senderTransactionId];

    CustomerService.Deposit(sender, SenderTransaction.MoneyTransferred);

    Console.WriteLine("Successfully reverted transaction in " + sender.FirstName);

    //revert receiver transaction id

    Console.WriteLine("Reverting the transaction in Receivers Account");

    Console.WriteLine("List of Transactions of" + receiver.FirstName + "are\n");

    foreach (Transaction transactions in receiver.Transactions.Values)
    {
        if (transactions.Sender == sender)
        {
            Console.WriteLine(transactions.Id);
        }
    };

    Console.WriteLine("\nEnter transaction ID");

    string receiverTransactionId = Console.ReadLine();

    while (!ValidationServices.ValidateTransaction(receiver, receiverTransactionId))
    {
        Console.WriteLine("Enter valid transaction id");
        receiverTransactionId = Console.ReadLine();
    }

    Transaction ReceiverTransaction = receiver.Transactions[receiverTransactionId];

    CustomerService.Withdraw(receiver, ReceiverTransaction.MoneyTransferred);

    Console.WriteLine("Transaction reverted successfully");

    sender.Transactions.Remove(senderTransactionId);

    receiver.Transactions.Remove(receiverTransactionId);

}



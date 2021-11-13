using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using Banks.Banks;
using Banks.Clients;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            var service = new CentralBank();
            string line = null;
            string name = null;
            string surname = null;
            string bank = null;
            string account = null;
            string adress = null;
            int passport = 0;
            decimal money = 0;
            decimal debitInterest = 0;
            var interests = new List<(int, decimal)>();
            int amount = 0;
            decimal interest = 0;
            decimal comission = 0;
            int limit = 0;

            while (line != "8")
            {
                Console.WriteLine(" ");
                Console.WriteLine("Welcome to my bank system! :)");
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("Please choose an action: ");
                Console.WriteLine("1 - create new Bank");
                Console.WriteLine("2 - add new client");
                Console.WriteLine("3 - add an account to a client");
                Console.WriteLine("4 - make transaction");
                Console.WriteLine("5 - show available banks");
                Console.WriteLine("6 - show clients in the bank");
                Console.WriteLine("7 - show client account balance");
                Console.WriteLine("8 - exit");
                Console.Write(">  ");
                line = Console.ReadLine();

                switch (line)
                {
                    case "1":
                        Console.WriteLine("Please enter bank name: ");
                        name = Console.ReadLine();
                        Console.WriteLine("Please enter debit interest:");
                        debitInterest = Convert.ToDecimal(Console.ReadLine());
                        Console.WriteLine("Please enter deposit interests:");
                        interests = new List<(int, decimal)>();

                        for (int i = 0; i < 3; i++)
                        {
                            Console.WriteLine("Enter amount of money:");
                            amount = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter interest for this amount");
                            interest = Convert.ToDecimal(Console.ReadLine());
                            interests.Add((amount, interest));
                        }

                        Console.WriteLine("Please enter credit comission");
                        comission = Convert.ToDecimal(Console.ReadLine());
                        Console.WriteLine("Please enter credit limit");
                        limit = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Creating bank .....");
                        service.CreateBank(name, debitInterest, interests, comission, limit).PrintInfo();
                        break;

                    case "2":
                        Console.WriteLine("Please enter name:");
                        name = Console.ReadLine();
                        Console.WriteLine("Please enter surname");
                        surname = Console.ReadLine();
                        Console.WriteLine("Do you want to add adress and passport id? y/n");

                        if (Console.ReadLine() == "y")
                        {
                            Console.WriteLine("Please enter adress: ");
                            adress = Console.ReadLine();
                            Console.WriteLine("Please enter passport id: ");
                            passport = Convert.ToInt32(Console.ReadLine());
                        }

                        Console.WriteLine("Which bank do you want to choose?");
                        Console.WriteLine("Available banks:");
                        foreach (var b in service.Banks)
                        {
                            Console.WriteLine($"{b.Name}");
                        }

                        Console.WriteLine("Please enter bank name");
                        bank = Console.ReadLine();
                        Console.WriteLine("Creating new client....");
                        var client = new Client();

                        foreach (var b in service.Banks)
                        {
                            if (bank == b.Name)
                            {
                                client = new ClientBuilder().SetName(name).SetSurname(surname).SetAdress(adress)
                                    .SetPassportId(passport).GetInfo();
                                service.AddClientToBank(client, b.Id);
                            }
                        }

                        client.PrintInfo();
                        break;

                    case "3":
                        Console.WriteLine("Please enter account type: ");
                        Console.WriteLine("Debit, Deposit, Credit");
                        account = Console.ReadLine();
                        Console.WriteLine("Please enter bank name:");
                        Console.WriteLine("Available banks:");
                        foreach (var b in service.Banks)
                        {
                            Console.WriteLine($"{b.Name}");
                        }

                        bank = Console.ReadLine();
                        Console.WriteLine("Please enter client name:");
                        name = Console.ReadLine();
                        Console.WriteLine("Please enter client surname:");
                        surname = Console.ReadLine();

                        Console.WriteLine("Please enter account balance:");
                        money = Convert.ToDecimal(Console.ReadLine());
                        Console.WriteLine("Creating account.....");
                        var bank2 = service.Banks.FirstOrDefault(b => b.Name == bank);
                        var acc = service.CreateAccount(
                            bank2?.Clients.Keys.FirstOrDefault(cl => cl.Name == name && cl.Surname == surname), bank2.Id, account, money);
                        acc.PrintInfo();
                        break;

                    case "4":
                        Console.WriteLine("What transaction do you want to do?");
                        Console.WriteLine("Replenish, Withdraw, Transfer");
                        var trans = Console.ReadLine();

                        Console.WriteLine("How much money?");
                        money = Convert.ToDecimal(Console.ReadLine());

                        Console.WriteLine("Please enter bank name:");
                        Console.WriteLine("Available banks:");
                        foreach (var b in service.Banks)
                        {
                            Console.WriteLine($"{b.Name}");
                        }

                        bank = Console.ReadLine();
                        Console.WriteLine("Please enter client name:");
                        name = Console.ReadLine();
                        Console.WriteLine("Please enter client surname:");
                        surname = Console.ReadLine();

                        var bank1 = service.Banks.FirstOrDefault(b => b.Name == bank);
                        var client1 = bank1.Clients.Keys.FirstOrDefault(cl => cl.Name == name && cl.Surname == surname);

                        Console.WriteLine("Please enter account type: ");
                        Console.WriteLine("Debit, Deposit, Credit");
                        account = Console.ReadLine();

                        acc = bank1.Accounts.FirstOrDefault(acc => acc.Owner == client1 && acc.AccountType == account);
                        Console.WriteLine("Making a transaction.....");

                        Console.WriteLine($"Money before: {acc.Balance}");
                        switch (trans)
                        {
                            case "Replenish":
                                bank1.ReplenishMoney(acc.Id, money);
                                break;
                            case "Withdraw":
                                bank1.WithdrawMoney(acc.Id, money);
                                break;
                            case "Transfer":
                                Console.WriteLine("To what account do you want to transfer money?");
                                var to = Console.ReadLine();
                                Console.WriteLine("Enter client name:");
                                var name1 = Console.ReadLine();
                                Console.WriteLine("Enter client surname:");
                                var surname1 = Console.ReadLine();

                                Console.WriteLine("Please enter bank name:");
                                Console.WriteLine("Available banks:");
                                foreach (var b in service.Banks)
                                {
                                    Console.WriteLine($"{b.Name}");
                                }

                                bank = Console.ReadLine();
                                bank2 = service.Banks.FirstOrDefault(b => b.Name == bank);
                                var accTo = bank2.Accounts.FirstOrDefault(acc => acc.Owner == client1 && acc.AccountType == account);
                                bank1.TransferMoney(acc.Id, accTo.Id, money);
                                break;
                        }

                        Console.WriteLine($"Money now: {acc.Balance}");
                        break;

                    case "5":
                        Console.WriteLine("Available banks:");
                        foreach (var b in service.Banks)
                        {
                            Console.WriteLine($"{b.Name}");
                        }

                        break;

                    case "6":
                        Console.WriteLine("Please enter bank name");
                        bank = Console.ReadLine();
                        service.Banks.FirstOrDefault(b => b.Name == bank).PrintInfo();
                        break;

                    case "7":
                        Console.WriteLine("Please enter bank name");
                        bank = Console.ReadLine();
                        Console.WriteLine("Please enter client name:");
                        name = Console.ReadLine();
                        Console.WriteLine("Please enter client surname:");
                        surname = Console.ReadLine();

                        Console.WriteLine("Please enter account type: ");
                        Console.WriteLine("Debit, Deposit, Credit");
                        account = Console.ReadLine();

                        bank1 = service.Banks.FirstOrDefault(b => b.Name == bank);
                        client1 = bank1.Clients.Keys.FirstOrDefault(cl => cl.Name == name && cl.Surname == surname);
                        bank1.Accounts.FirstOrDefault(acc => acc.Owner == client1 && acc.AccountType == account).PrintInfo();
                        break;

                    case "8":
                        break;

                    default:
                        Console.WriteLine("Incorrect input, please try again");
                        break;
                }
            }
        }
    }
}

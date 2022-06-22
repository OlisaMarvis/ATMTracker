using System;
using System.Collections.Generic;
using System.Reflection;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace ATMTracker
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            List<ATMList> list = new List<ATMList>();
            list.Add(new ATMList() { ATMId = 1012421, ATMName = "NCR-S2", FaultyATM = "faulty", Fault_Description = "Vacuum pump bad", Resolution = "Change vacuum pump" });
            list.Add(new ATMList() { ATMId = 1012422, ATMName = "NCR-S2", FaultyATM = "Okay", Fault_Description = "", Resolution = "No-resolution" });
            list.Add(new ATMList() { ATMId = 1012423, ATMName = "NCR-S2", FaultyATM = "faulty", Fault_Description = "Rejecting Cards", Resolution = "Check Card Reader" });
            list.Add(new ATMList() { ATMId = 1012424, ATMName = "NCR-S2", FaultyATM = "Okay", Fault_Description = "", Resolution = "No-resolution" });
            list.Add(new ATMList() { ATMId = 1012425, ATMName = "NCR-S1", FaultyATM = "Okay", Fault_Description = "", Resolution = "No-resolution" });
            list.Add(new ATMList() { ATMId = 1012426, ATMName = "NCR-S1", FaultyATM = "faulty", Fault_Description = "Not picking cash", Resolution = "Check suction cups/vacuum pump" });
            list.Add(new ATMList() { ATMId = 1012427, ATMName = "NCR-S1", FaultyATM = "Okay", Fault_Description = "", Resolution = "No-resolution" });
            list.Add(new ATMList() { ATMId = 1012428, ATMName = "NCR-S1", FaultyATM = "Okay", Fault_Description = "", Resolution = "No-resolution" });
            list.Add(new ATMList() { ATMId = 1012429, ATMName = "NCR-S1", FaultyATM = "faulty", Fault_Description = "Card no useable app", Resolution = "Delete custom.dat" });
            list.Add(new ATMList() { ATMId = 1012501, ATMName = "NCR-S1", FaultyATM = "Okay", Fault_Description = "", Resolution = "No-resolution" });

            ATMList.ListAllATMS(list);
           
        }

    }
}

public class ATMList
{
    public int ATMId { get; set; }
    public string ATMName { get; set; }

    public string FaultyATM { get; set; }

    public string Fault_Description { get; set; }

    public string Resolution { get; set; }

    public static void AddfaultyATM()
    {
        Console.WriteLine("Add faulty ATMS");
        Console.Write("Enter ATM Name: ");
        string atmName = Console.ReadLine();
        Console.Write("Enter ATMID: ");
        string ATMId = Console.ReadLine();
        Console.Write("Enter Fault: ");
        string ATMfault = Console.ReadLine();
        Console.WriteLine($"ATM NAME:{atmName} \n ATM ID: {ATMId} ATM FAULT: \n {ATMfault}");

        Console.WriteLine("Faulty ATM'S Added sucessfully");
    }
    public static void ListAllATMS(List<ATMList> aTMLists)
    {
        Console.WriteLine("Menu \n 1: List All ATM'S \n 2: List Faulty ATM'S \n 3: List faulty ATM'S and resolution \n 4: Add faulty ATM'S \n Input a number from any of the list items and press enter");
        int value = Convert.ToInt32(Console.ReadLine());
        foreach (ATMList list in aTMLists)
        {
            if (value == 1)
            {
                Console.WriteLine($"ATM Name: {list.ATMName} ATMID: {list.ATMId}");

            }
            else if (value == 2 && list.FaultyATM == "faulty")
            {
                Console.WriteLine($"ATM Name: {list.ATMName} ATMID: {list.ATMId} Fault: {list.Fault_Description}");
            }
            else if (value == 3 && list.FaultyATM == "faulty")
            {
                Console.WriteLine($"ATM Name: {list.ATMName} ATMID: {list.ATMId} Fault: {list.Fault_Description}\t Resolution: {list.Resolution}");
            }
            else if (value == 4)
            {
                AddfaultyATM();
                break;
            }
            else
            {
                Console.WriteLine("You entered a wrong value! \n Please enter a number from the list below");
                Console.WriteLine();

                ATMList.ListAllATMS(aTMLists);
            }
        }

       
        Console.WriteLine("Do you wish to do something else?, Yes or No.");
       
        string input = Console.ReadLine();
        Console.Clear();
        input = input.ToLower();
        if(input == "yes")
        {
            ATMList.ListAllATMS(aTMLists);
        }
        else
        {
            SendEmail();
            //Console.WriteLine("Nice doing business with you");

        }
        
    }
    public static void SendEmail()
    {
        
        MimeMessage message = new MimeMessage();

        message.From.Add(new MailboxAddress("Olisa", "okpala.olisa30@gmail.com"));
        message.To.Add(MailboxAddress.Parse("olisamarvis@gmail.com"));
        message.Subject = "ATM List";

        message.Body = new TextPart("plain")
        {
            Text = @"Hello Sir,
                    I hope ypu enjoyed our service?!"
        };

        Console.Write("Email: ");
        string emailAddress = Console.ReadLine();

        Console.Write("Password: ");
        string password = Console.ReadLine();

        //Create a new SMTP client
        SmtpClient client = new SmtpClient();
        try
        {
            //Connect to gmail smtp server using port 465 with ssl enabled
            client.Connect("smtp.gmail.com", 465, true);
            //Note: only needed if the SMTP server requires authentication, like gmail for example
            client.Authenticate(emailAddress, password);
            client.Send(message);

            //display a message if no exception was thrown
            Console.WriteLine("Email Sent!.");
        }
        catch (Exception ex)
        {
            //In case of an error in display message
            Console.WriteLine(ex.Message);
        }
        finally
        {
            //at any case always disconnect from the client 
            client.Disconnect(true);
            //and dispose of the client object
            client.Dispose();
        }
    }

}



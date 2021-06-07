using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ConsoleApp2
{
   
    class Program
    {
        
        static void Main(string[] args)
        {
            int index = 0;
            int right_amount = 0;
            String player = " ";
            String crypt_key;
            var random = RandomNumberGenerator.Create();
            var key = new byte[16];
            random.GetBytes(key);
            crypt_key = BitConverter.ToString(key).Replace("-", String.Empty);
            int size= args.Length;
            bool flag = false;
            for (int i = 0; i < args.Length-1; i++)
            {
                if (args[i] == args[i + 1])
                {
                    flag = true;
                }
            }
            if (size%2==0|size<3|flag)
            {
                Console.WriteLine("Error");
                Environment.Exit(0);
            }     
            String computer = args[new Random().Next(0, args.Length)];
            Console.WriteLine(computer);
            Console.WriteLine( "HMAC:{0}",HMACHASH(computer, crypt_key));
            Console.WriteLine("Available moves:\n");
            for (int i = 0; i < args.Length; i++)
                Console.WriteLine("{0} - {1}",i+1,args[i]);
            Console.WriteLine("0 - exit");
            Console.Write("Enter your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            if (choice == 0)
            {
                Environment.Exit(0);
            }
            else  player = args[choice - 1]; 
            Console.WriteLine("Your move: {0}", player);
            Console.WriteLine("Computer move: {0}", computer);
            if (player == computer)
             Console.WriteLine("It's a tie!");
            else
            {
                right_amount = args.Length / 2;
                index = choice - 2;
                while (true)
                {
                    if (right_amount == 0)
                    {
                        Console.WriteLine("You lose!");
                        break;
                    }
                    if (index == -1)
                    {
                        index = args.Length - 1;
                    }
                    if(computer==args[index]){
                        Console.WriteLine("You win!");
                        break;
                    }

                    index -= 1;
                    right_amount -= 1;
                }

            }
            Console.WriteLine("HMAC key: {0}", crypt_key);
            Environment.Exit(0);
        }

        static string HMACHASH(string str, string key)
        {
            byte[] bkey = Encoding.Default.GetBytes(key);
            using (var hmac = new HMACSHA256(bkey))
            {
                byte[] bstr = Encoding.Default.GetBytes(str);
                var bhash = hmac.ComputeHash(bstr);
                return BitConverter.ToString(bhash).Replace("-", string.Empty).ToLower();
            }
        }
    }
}

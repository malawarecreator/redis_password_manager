// tee hee
using NRedisStack;
using NRedisStack.RedisStackCommands;
using StackExchange.Redis;

class Program
{
    static void Main()
    {


        string id;
        string passwd;
        bool? conf;
        Console.Write("Enter your passwd id>");
        id = Console.ReadLine();
        Console.Write("Enter your passwd>");
        passwd = Console.ReadLine();
        Console.WriteLine($"Is this correct (y/n): {id}, {passwd}");
        string confyn = Console.ReadLine();
        if (confyn == "y")
        {
            conf = true;
        } else
        {
            conf = false;
            Console.WriteLine("quitting");

        }
        if (conf == true)
        {
            var User = new User();
            User.setcreds(id, passwd);
            User.publish(true, ["ben", "dover"]);
            Console.WriteLine("finished");
        }

    }

}

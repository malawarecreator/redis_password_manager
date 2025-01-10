using NRedisStack;
using NRedisStack.RedisStackCommands;
using StackExchange.Redis;
public class User
{
    public string username;
    public Password password;
    public void setcreds(string newid, string newpass)
    {
        password.id = newid;
        password.password = newpass;
    }

    public void publish()
    {
        ConfigurationOptions conf  = new ConfigurationOptions
        {
            EndPoints = {"localhost:6379"},
            User = "ben",
            Password = "Dover",
            AbortOnConnectFail = false,
        };
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(conf);
        IDatabase db = redis.GetDatabase();
        Console.WriteLine($"User {username} publishing password with id {password.id}");
        try
        {
            db.StringSet(password.id, password.password);
            Console.WriteLine($"got result from test {db.StringGet(password.id)}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"error {e} is redis running on your server");
            throw;
        }
    }



}
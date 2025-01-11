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

    public void publish(bool encrypt, string[] creds)
    {
        if (encrypt) {
            ConfigurationOptions conf = new ConfigurationOptions
            {
                EndPoints = { "localhost:6379"},
                User = creds[0],
                Password = creds[1],
                AbortOnConnectFail = false,
            };
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(conf);
            IDatabase db = redis.GetDatabase();
            Console.WriteLine($"User {username} publishing encrypted password with id {password.id}");
            try
            {
                db.StringSet(password.id, encryptpass(password.password ,7));
                Console.WriteLine($"got result from test {db.StringGet(password.id)} or {decryptpass(db.StringGet(password.id), 7)} when decrypted");
            }
            catch (Exception e)
            {
                Console.WriteLine($"error {e} is redis running on your server");
                throw;
            }

        }
        else {
            ConfigurationOptions conf = new ConfigurationOptions
            {
                EndPoints = { "localhost:6379" },
                User = creds[0],
                Password = creds[1],
                AbortOnConnectFail = false,
            };
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(conf);
            IDatabase db = redis.GetDatabase();
            Console.WriteLine($"User {username} publishing password with id {password.id}");
            try
            {
                db.StringSet(password.id, password.password);
                Console.WriteLine($"got result from test: {db.StringGet(password.id)}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"error {e} is redis running on your server");
                throw;
            }
        }
            
    }

    public string encryptpass(string txt, int key) 
    {
        string output = string.Empty;
        foreach (char ch in txt)
            output += cipher(ch, key);
        return output;
    }
    public string decryptpass(string txt, int key)
    {
        return encryptpass(txt ,26 - key);   
    }
    public char cipher(char ch, int key)
    {
        if (!char.IsLetter(ch))
        {

            return ch;
        }

        char d = char.IsUpper(ch) ? 'A' : 'a';
        return (char)((((ch + key) - d) % 26) + d);


    }



}
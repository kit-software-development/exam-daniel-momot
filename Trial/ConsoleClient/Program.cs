namespace ConsoleClient
{

    class Program
    {
        static void Main(string[] args)
        {
            using (MyTCPClient module = new MyTCPClient())
            {
                module.SendMessage("It's alive!");
                module.SendMessage("It's alive! x2");
            }

        }
    }
}

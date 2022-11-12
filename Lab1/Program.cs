namespace Lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");

            TenisGame tenisGame = new()
            {
                play1 = "John",
                play2 = "Bill"
            };

            List<Player> players = new List<Player>()
            {
                Player.None,
                Player.Player1,
                Player.Player2,
                Player.Player1,
                Player.Player2,
                Player.Player2,
                Player.Player2,
                Player.Player2,
            };

            foreach (Player player in players)
            {
                Console.WriteLine(tenisGame.Click(player));
            }

            Console.Read();
        }
    }
}
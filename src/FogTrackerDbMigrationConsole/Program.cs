using System;

namespace FogTrackerDbMigrationConsole
{
    using FogTracker.Model;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using (var context = new FogContext())
            {

            }
        }
    }
}

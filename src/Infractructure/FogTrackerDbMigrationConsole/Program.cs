using System;

namespace FogTrackerDbMigrationConsole
{
    using FogTracker.Model;
    using FogTracker.Repos;

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

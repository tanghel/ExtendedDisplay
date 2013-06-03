using System;

namespace ExtendedDisplay
{
    public static class Stopwatch
    {
        private const bool DEBUGGING = false;

        public static void Measure(string name, Action actionToMeasure)
        {
            var start = DateTime.Now;

            actionToMeasure.Invoke();

            if (DEBUGGING)
            {
                Console.WriteLine("Operation '{0}' took {1}", name, DateTime.Now - start);
            }
        }
    }
}

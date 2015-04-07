using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NORM.Mark1
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new NormDbConnection();
            //var data = db.Current.ListExecuteQuery<MTLog>("select MTLogId, UserId, Sender from MTLog where Status=@Sent", new {@Sent="Sent"});
            var profiler = Stopwatch.StartNew();
            var data2 = db.Current.ListExecuteStoredProc<MTLog>("Test_Mt_Log", new {@Status = "Sent"});
            profiler.Stop();
            Console.WriteLine("----------------------");

            foreach (var d in data2)
            {
                Console.WriteLine(d.ProcessingTime);
            }

            Console.WriteLine("Time elapsed: "+profiler.ElapsedMilliseconds+" ms");
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vitual_machine_online_manager.Model
{
    //Singleton
    public class Configs
    {
        private static readonly Configs Self = new Configs();
        public TimeSpan distanceMaxTime { get; private set; }
        public int port { get; private set; }

        private Configs() { }
        public static Configs Instance()
        {
            return Self;
        }
        public static void SetConfigs(TimeSpan? distanceMaxTime, int port = 80)
        {
            Self.distanceMaxTime = distanceMaxTime ?? TimeSpan.FromHours(1);
            Self.port = port;
        }
    }
}

using System;
using Nancy.Hosting.Self;
using Topshelf;

namespace TopShelfService
{

    public class Program
    {
        public static void Main()
        {
            

            HostFactory.Run(x =>
            {
                x.Service<NancyHost>(s =>
                {
                    s.ConstructUsing(()=>new NancyHost(new[]{new Uri("http://localhost:8080"), }));
                    s.WhenStarted(nh => nh.Start());
                    s.WhenStopped(nh => nh.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Sample Topshelf Host");
                x.SetDisplayName("Stuff");
                x.SetServiceName("stuff");
            });
        }
    }

    public class SampleModule : Nancy.NancyModule
    {
        public SampleModule()
        {
            Get["/"] = _ => "Hello World!";
        }
    }
}


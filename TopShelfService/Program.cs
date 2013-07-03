using System;
using System.Configuration;
using Nancy.Hosting.Self;
using Nancy.Responses;
using Topshelf;
using Nancy.ModelBinding;

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
                    s.ConstructUsing(()=>new NancyHost(new[]{new Uri(ConfigurationManager.AppSettings["uri"]), }));
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
        private Status status = new Status("Glop", DateTime.Now);

        public SampleModule()
        {
            Get["/Status"] = _ => new JsonResponse(status, new DefaultJsonSerializer());
            Post["/Status"] = o =>
                {
                    var req = this.Bind<UpdateStatusRequest>(); status = new Status(req.NewStatus, DateTime.Now);
                                       return new JsonResponse(status, new DefaultJsonSerializer());
            };
        }
    }

    public class UpdateStatusRequest
    {
        public string NewStatus { get; set; }
    }

    public class Status
    {
        private readonly string state;
        private readonly DateTime lastUpdate;

        public string State
        {
            get { return state; }
        }

        public DateTime LastUpdate
        {
            get { return lastUpdate; }
        }

        public Status(string state, DateTime lastUpdate)
        {
            this.state = state;
            this.lastUpdate = lastUpdate;
        }
    }
}


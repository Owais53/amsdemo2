using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Quartz;

namespace amsdemo
{
    public class VacancyAvailablityJob : IJob
    {

        public Task Execute(IJobExecutionContext context)
        {





            return Task.CompletedTask;

          
        }
    }
}
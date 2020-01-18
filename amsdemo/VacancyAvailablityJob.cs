using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using amsdemo.Models;
using Quartz;

namespace amsdemo
{
    public class VacancyAvailablityJob : IJob
    {
        private readonly SqlContext db = new SqlContext();
        public Task Execute(IJobExecutionContext context)
        {

            var execute = db.sp_BackEndAvailablity();



            return Task.CompletedTask;

          
        }
    }
}
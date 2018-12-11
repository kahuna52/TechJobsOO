using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.Models;
using TechJobs.ViewModels;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static readonly JobData JobData;

        static JobController()
        {
            JobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            //TODO #1 - get the Job with the given ID and pass it into the view

            var theJob = JobData.Find(id);
            var newJob = new NewJobViewModel
            {
                Name = theJob.Name,
                EmployerId = theJob.Employer.ID,
                CoreCompetencyId = theJob.CoreCompetency.ID,
                LocationId = theJob.Location.ID,
                PositionTypeId = theJob.PositionType.ID
            };

            return View(theJob);
        }

        public IActionResult New()
        {
            var newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job


            if (!ModelState.IsValid) return View(newJobViewModel);
            var newJob = new Job
            {
                Name = newJobViewModel.Name,
                Employer = JobData.Employers.Find(newJobViewModel.EmployerId),
                Location = JobData.Locations.Find(newJobViewModel.LocationId),
                CoreCompetency = JobData.CoreCompetencies.Find(newJobViewModel.CoreCompetencyId),
                PositionType = JobData.PositionTypes.Find(newJobViewModel.PositionTypeId)
            };

            JobData.Jobs.Add(newJob);
            return Redirect(string.Format("/job?id={0}", newJob.ID));

            //JobData data = JobData.GetInstance();
            //Job newJob = new Job();
            //newJob.Employer = data.Employers.Find(newJobViewModel.EmployerID.ID);

        }
    }
}

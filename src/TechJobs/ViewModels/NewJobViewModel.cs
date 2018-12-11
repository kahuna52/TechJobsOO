using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.Models;

namespace TechJobs.ViewModels
{
    public class NewJobViewModel
    {
        [Required]
        public string Name { get; set; }


        // TODO #3 - Include other fields needed to create a job,
        // with correct validation attributes and display names.

        [Required]
        [Display(Name = "Employer")]
        public int EmployerId { get; set; }

        [Required]
        [Display(Name = "Location")]
        public int LocationId { get; set; }

        [Required]
        [Display(Name = "Skill")]
        public int CoreCompetencyId { get; set; }

        [Required]
        [Display(Name = "Position Type")]
        public int PositionTypeId { get; set; }


        public List<SelectListItem> Employers { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Locations { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> CoreCompetencies { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> PositionTypes { get; set; } = new List<SelectListItem>();



        public NewJobViewModel()
        {

            var jobData = JobData.GetInstance();

            foreach (var field in jobData.Employers.ToList())
            {
                Employers.Add(new SelectListItem
                {
                    Value = field.ID.ToString(),
                    Text = field.Value
                });
            }

            // TODO #4 - populate the other List<SelectListItem> 
            // collections needed in the view

            foreach (var field in jobData.CoreCompetencies.ToList())
            {
                CoreCompetencies.Add(new SelectListItem
                {
                    Value = field.ID.ToString(),
                    Text = field.Value
                });
            }

            foreach (var field in jobData.Locations.ToList())
            {
                Locations.Add(new SelectListItem
                {
                    Value = field.ID.ToString(),
                    Text = field.Value
                });
            }

            foreach (var field in jobData.PositionTypes.ToList())
            {
                PositionTypes.Add(new SelectListItem
                {
                    Value = field.ID.ToString(),
                    Text = field.Value
                });
            }




        }




    }
}
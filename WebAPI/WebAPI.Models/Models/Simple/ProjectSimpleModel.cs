using System;

namespace WebAPI.Models.Models.Simple
{
    public class ProjectSimpleModel
    {
        public Guid ProjectId { get; set; }
        
        public string ProjectName { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }
    }
}
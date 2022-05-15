namespace TrackingApi.Models
{
    public class Activity
    {
        public long Id { get; set; }
        public string? Name { get; set; }
         public string? Description { get; set; }
         public string? StartDate { get; set; }
         public double? Hours { get; set; }

        
    }
}
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Task_Tracker_2.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Enter")]
        public DateTime? DueDate { get; set; } = null!;

        [Required(ErrorMessage = "Enter")]
        public string TitleId { get; set; } = string.Empty;

        [ValidateNever]
        public Title Title { get; set; } = null!;
        [Required(ErrorMessage ="Enter")]
        public string StatusId {  get; set; } = string.Empty;
        [ValidateNever]
        public Status Status { get; set; } = null!;
        public bool Overdue => StatusId =="open" && DueDate < DateTime.Today;


   
    }
}

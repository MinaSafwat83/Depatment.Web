﻿
using System.ComponentModel.DataAnnotations;


namespace DepartmentModule.Core.Model
{
    public class Reminder
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime ReminderDateTime { get; set; }

        public bool IsSent { get; set; }
    }
}

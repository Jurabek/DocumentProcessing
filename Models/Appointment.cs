using System;
using System.ComponentModel.DataAnnotations;

namespace DocumentProcessing.Models
{
    public class Appointment
    {
        [Key]
        public Guid Id { get; set; }

        public AppointmentCharacters Character { get; set; }

        public long Number { get; set; }

        public Guid DocumentId { get; set; }

        public virtual Document Document { get; set; }
    }
}
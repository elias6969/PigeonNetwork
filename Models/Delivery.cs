using System;

namespace PigeonPostApi.Models
{
    public class Delivery
    {
        public int Id { get; set; }

        public int PigeonId { get; set; }
        public Pigeon? Pigeon { get; set; }

        public int MessageId { get; set; }
        public Message? Message { get; set; }

        public int FromRoostId { get; set; }
        public int ToRoostId { get; set; }

        public DateTime? DispatchedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }

        public string? Status { get; set; }
    }
}

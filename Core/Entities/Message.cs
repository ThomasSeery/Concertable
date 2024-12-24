﻿using Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Message : BaseEntity
    {
        public string MessageContent { get; set; }
        public int FromId { get; set; }
        public int ToId { get; set; }
        public DateTime SentDate { get; set; }
        public bool Read { get; set; }
        public ApplicationUser FromUser { get; set; }
        public ApplicationUser ToUser { get; set; }
    }
}

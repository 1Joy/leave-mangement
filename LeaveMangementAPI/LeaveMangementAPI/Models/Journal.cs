﻿using System;
using System.Collections.Generic;

namespace LeaveMangementAPI.Models
{
    public partial class Journal
    {
        public int Id { get; set; }
        public int WorkId { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
    }
}

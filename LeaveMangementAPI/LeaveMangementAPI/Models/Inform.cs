﻿using System;
using System.Collections.Generic;

namespace LeaveMangementAPI.Models
{
    public partial class Inform
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsLook { get; set; }
        public int WorkId { get; set; }
    }
}

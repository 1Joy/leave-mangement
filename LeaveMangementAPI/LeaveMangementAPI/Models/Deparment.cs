﻿using System;
using System.Collections.Generic;

namespace LeaveMangementAPI.Models
{
    public partial class Deparment
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int? ManagerId { get; set; }
        public string Name { get; set; }
        public int WorkerCount { get; set; }
        public string Code { get; set; }
    }
}

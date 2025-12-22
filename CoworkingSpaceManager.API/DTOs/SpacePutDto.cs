using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoworkingSpaceManager.API.DTOs
{
    public class SpacePutDto
    {
        public string? Name { get; set; }
        public int Capacity { get; set; }
        public bool Available { get; set; }
    }
}
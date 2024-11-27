
using System.Collections.Generic;

using IPStackApi.Dtos;

namespace IPStackApi.Models
{
    public class BatchUpdateJob
    {
        public required Guid JobId; 
        public required int TotalItems;
        public required int ProcessedItems;
        public required List<IPDetailsRequestDto> Items;
        public required DateTime CreatedAt;
    }
}
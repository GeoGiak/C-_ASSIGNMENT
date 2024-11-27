using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using IPStackDLL.Models;

using IPStackApi.Dtos;
using IPStackApi.Models;
// using IPStackApi.Models.BatchUpdateJob;

namespace IPStackApi.Services
{
    public class UpdateBatchJobService
    {
        private static ConcurrentDictionary<Guid, BatchUpdateJob> _jobs;

        public UpdateBatchJobService(ConcurrentDictionary<Guid, BatchUpdateJob> jobs)
        {
            _jobs = jobs;
        }

        public async Task<Guid> StartBatchUpdateAsync(List<IPDetailsRequestDto> ipDetailsList)
        {
            if (ipDetailsList == null || !ipDetailsList.Any())
                throw new ArgumentException("No IPDetails provided");
            
            BatchUpdateJob job = new BatchUpdateJob
            {
                JobId = Guid.NewGuid(),
                TotalItems = ipDetailsList.Count,
                ProcessedItems = 0,
                Items = ipDetailsList,
                CreatedAt = DateTime.Now
            };
            
            if (!_jobs.TryAdd(job.JobId, job))
                throw new InvalidOperationException("Failed to add job to dictionary");
            
            Task.Run(() => ProcessJobAsync(job));
            
            return job.JobId;
        }

        public async Task ProcessJobAsync(BatchUpdateJob job)
        {
            while (job.ProcessedItems < job.TotalItems)
            {
                // Get next batch of 10 items
                var batch = job.Items.Skip(job.ProcessedItems).Take(10).ToList();

                // Simulate the update methods
                await Task.Delay(1000);
                Console.WriteLine("Finished 10");

                job.ProcessedItems += batch.Count;
            }

            // Job has been completed
            _jobs.Remove(job.JobId, out _);
        }

        public BatchUpdateJob GetJobProgress(Guid jobId)
        {
            if (!_jobs.TryGetValue(jobId, out BatchUpdateJob job))
                throw new KeyNotFoundException("Job not found");

            return job;
        }
    }
}
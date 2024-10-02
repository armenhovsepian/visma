﻿using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using Timelogger.Helpers;
using Timelogger.Results;

namespace Timelogger.Entities
{
    public class Project
    {
        public Project()
        {
            TimeRegistrations = new List<TimeRegistration>();
        }

        public int Id { get; private set; }
        public Guid Guid { get; private set; }
        public string Name { get; private set; }
        public int UserId { get; private set; }
        public DateTime Deadline { get; private set; }
        public bool IsCompleted => CompletedDate != null;
        public DateTime CreatedDate { get; private set; }
        public DateTime? CompletedDate { get; private set; }

        public virtual List<TimeRegistration> TimeRegistrations { get; set; }


        public Project(string name, DateTime deadline)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Project name can not be null or empty.");
            }

            if (deadline < DateTime.UtcNow)
            {
                throw new ArgumentNullException("Project deadline cannot be in the past.");
            }

            Guid = Guid.NewGuid();
            Name = name;
            Deadline = deadline;
            CreatedDate = DateTime.UtcNow;
        }

        public Result RegisterTime(DateTime start, DateTime end)
        {
            if (IsCompleted)
            {
                return Result.Failure(new[] { "Cannot log time to a completed project." });
            }

            if (!DateTimeHelpers.IsDateRangeValid(start, end))
            {
                return Result.Failure(new[] { "The date range is invalid (less than 30 minutes)." });
            }

            if (TimeRegistrations.Any(tr => tr.Start.Equals(start) && tr.End.Equals(end)))
            {
                return Result.Failure(new[] { "The data range are trying to insert already exists in the database." });
            }

            TimeRegistrations.Add(new TimeRegistration(Id, start, end));

            return Result.Success();
        }

        public void Complete()
        {
            if (!IsCompleted)
            {
                CompletedDate = DateTime.UtcNow;
            }
        }
    }
}

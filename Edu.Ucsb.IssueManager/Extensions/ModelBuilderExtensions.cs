using Edu.Ucsb.IssueManager.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Edu.Ucsb.IssueManager.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder MapIssue(this ModelBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            var entityTypeBuilder = builder.Entity<Issue>();

            // Primary Key
            entityTypeBuilder.HasKey(issue => issue.IssueId);

            // Properties
            entityTypeBuilder.Property(t => t.IssueId).ValueGeneratedOnAdd();
            entityTypeBuilder.Property(t => t.Title).HasMaxLength(255);
            entityTypeBuilder.Property(t => t.Description).HasMaxLength(1024);

            //Relationships
            entityTypeBuilder
                .HasOne(t => t.UserIssue)
                .WithOne(userIssue => userIssue.Issue)
                .HasForeignKey<UserIssue>(userIssue => userIssue.IssueId);

            return builder;
        }

        public static ModelBuilder MapUserIssue(this ModelBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            var entityTypeBuilder = builder.Entity<UserIssue>();

            // Primary Key
            entityTypeBuilder.HasKey(t => new { t.IssueId, t.UserId });

            // Properties
            entityTypeBuilder.Property(t => t.IssueId).ValueGeneratedNever();
            entityTypeBuilder.Property(t => t.UserId).ValueGeneratedNever();

            return builder;
        }
    }
}
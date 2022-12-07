using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NotificationDotNet6SignalR.Commons.Extensions;

// https://stackoverflow.com/questions/26957519/ef-core-mapping-entitytypeconfiguration

internal static class ModelBuilderExtensions
{
    public static void AddConfiguration<TEntity>(this ModelBuilder modelBuilder,
        DbEntityConfiguration<TEntity> entityConfiguration) where TEntity : class
    {
        modelBuilder.Entity<TEntity>(entityConfiguration.Configure);
    }

    internal abstract class DbEntityConfiguration<TEntity> where TEntity : class
    {
        public abstract void Configure(EntityTypeBuilder<TEntity> entity);
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationDotNet6SignalR.Domain.Entities;
using static NotificationDotNet6SignalR.Commons.Extensions.ModelBuilderExtensions;

namespace NotificationDotNet6SignalR.Infra.Mappings;

internal class ConnectionMapping : DbEntityConfiguration<Connection>
{
    public override void Configure(EntityTypeBuilder<Connection> entityBuilder)
    {
        entityBuilder.ToTable("Connections");
        entityBuilder.HasKey(t => t.Id).HasName("Pk_Connections_Id");
        entityBuilder.Property(t => t.CreatedAt).IsRequired().HasColumnType("DATETIME");
        entityBuilder.Property(t => t.UpdatedAt).HasColumnType("DATETIME");
        entityBuilder.Property(t => t.Active).IsRequired().HasColumnType("BIT").HasDefaultValueSql("1");
        entityBuilder.Property(t => t.ConnectionId).IsRequired().HasColumnType("TEXT");
        entityBuilder.Property(t => t.Connected).IsRequired().HasColumnType("BIT").HasDefaultValueSql("1");

        entityBuilder.HasOne(s => s.User)
            .WithMany(c => c.Connections)
            .HasForeignKey(fk => fk.UserId)
            .HasConstraintName("Fk_Con_User_Id")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
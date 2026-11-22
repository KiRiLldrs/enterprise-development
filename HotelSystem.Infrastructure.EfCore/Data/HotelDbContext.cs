using HotelSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Infrastructure.EfCore.Data;

/// <summary>
/// Определяет наборы сущностей и настройки их связей
/// </summary>
public class HotelDbContext(DbContextOptions<HotelDbContext> options)
    : DbContext(options)
{
    /// <summary>
    /// Набор клиентов гостиницы
    /// </summary>
    public DbSet<Client> Clients => Set<Client>();

    /// <summary>
    /// Набор типов номеров гостиницы
    /// </summary>
    public DbSet<RoomType> RoomTypes => Set<RoomType>();

    /// <summary>
    /// Набор номеров гостиницы
    /// </summary>
    public DbSet<Room> Rooms => Set<Room>();

    /// <summary>
    /// Набор бронирований гостиницы
    /// </summary>
    public DbSet<Booking> Bookings => Set<Booking>();

    /// <summary>
    /// Настраивает таблицы, ключи и связи между сущностями базы данных
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(client => client.Id);

            entity.Property(client => client.PassportNumber)
                .IsRequired();

            entity.Property(client => client.LastName)
                .IsRequired();

            entity.Property(client => client.FirstName)
                .IsRequired();

            entity.Property(client => client.Citizenship)
                .IsRequired();
        });

        modelBuilder.Entity<RoomType>(entity =>
        {
            entity.HasKey(roomType => roomType.Id);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(room => room.Id);

            entity.HasOne(room => room.RoomType)
                .WithMany()
                .HasForeignKey("RoomTypeId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(booking => booking.Id);

            entity.HasOne(booking => booking.Client)
                .WithMany()
                .HasForeignKey("ClientId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(booking => booking.Room)
                .WithMany()
                .HasForeignKey("RoomId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            entity.Ignore(booking => booking.TotalCost);
        });
    }
}
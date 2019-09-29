using Microsoft.EntityFrameworkCore;
using System;
namespace GameServer.Models
{
    public class PlayerContextPvz : DbContext
    {
        public PlayerContextPvz(DbContextOptions<PlayerContextPvz> options)
            : base(options)
        {
        }

        public DbSet<PlayerPvz> Players { get; set; }
    }
}


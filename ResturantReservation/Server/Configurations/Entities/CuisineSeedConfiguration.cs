using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResturantReservation.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace ResturantReservation.Server.Configurations.Entities
{
    public class CuisineSeedConfiguration : IEntityTypeConfiguration<Cuisine>
    {
        public void Configure(EntityTypeBuilder<Cuisine> builder)
        {
            builder.HasData(
                new Cuisine
                {
                    Id = 1,
                    Name = "French",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Cuisine
                {
                    Id = 2,
                    Name = "Chinese",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Cuisine
                {
                    Id = 3,
                    Name = "Italian",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                }
                );
        }
    }
}

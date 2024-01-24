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
    public class DietSeedConfiguration : IEntityTypeConfiguration<Diet>
    {
        public void Configure(EntityTypeBuilder<Diet> builder)
        {
            builder.HasData(
                new Diet
                {
                    Id = 1,
                    Name = "Vegan",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Diet
                {
                    Id = 2,
                    Name = "Vegetarian",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                },
                new Diet
                {
                    Id = 3,
                    Name = "Non-Dairy",
                    DateCreated = DateTime.Now,
                    DateUpdated = DateTime.Now,
                    CreatedBy = "System",
                    UpdatedBy = "System"
                }
                );
        }
    }
}

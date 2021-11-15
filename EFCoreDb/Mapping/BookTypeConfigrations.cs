using System;
using BookStore.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.API.Configrations
{
    public class BookTypeConfigrations:IEntityTypeConfiguration<Books>
    {
      

        public void Configure(EntityTypeBuilder<Books> builder)
        {
            builder.Property(e => e.Description).IsRequired();
        }
    }
}

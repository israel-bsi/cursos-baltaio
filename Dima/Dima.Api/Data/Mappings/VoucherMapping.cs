﻿using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dima.Api.Data.Mappings;

public class VoucherMapping : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.ToTable("Voucher");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Number)
            .IsRequired()
            .HasColumnType("CHAR")
            .HasMaxLength(8);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

        builder.Property(x => x.Description)
            .IsRequired(false)
            .HasColumnType("NVARCHAR")
            .HasMaxLength(255);

        builder.Property(x => x.Amount)
            .IsRequired()
            .HasColumnType("MONEY");

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasColumnType("BIT");
    }
}
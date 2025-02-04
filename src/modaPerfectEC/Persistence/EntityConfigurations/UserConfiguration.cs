using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Core.Security.Hashing;

namespace Persistence.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users").HasKey(u => u.Id);

        builder.Property(u => u.Id).HasColumnName("Id").IsRequired();
        builder.Property(u => u.Email).HasColumnName("Email").IsRequired();
        builder.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt").IsRequired();
        builder.Property(u => u.PasswordHash).HasColumnName("PasswordHash").IsRequired();
        builder.Property(u => u.AuthenticatorType).HasColumnName("AuthenticatorType").IsRequired();
        builder.Property(u => u.TradeName).HasColumnName("TradeName").IsRequired();
        builder.Property(u => u.FirstName).HasColumnName("FirstName").IsRequired();
        builder.Property(u => u.LastName).HasColumnName("LastName").IsRequired();
        builder.Property(u => u.Country).HasColumnName("Country").IsRequired();
        builder.Property(u => u.City).HasColumnName("City").IsRequired();
        builder.Property(u => u.District).HasColumnName("District");
        builder.Property(u => u.Address).HasColumnName("Address").IsRequired();
        builder.Property(u => u.GsmNumber).HasColumnName("GsmNumber").IsRequired();
        builder.Property(u => u.TaxOffice).HasColumnName("TaxOffice");
        builder.Property(u => u.TaxNumber).HasColumnName("TaxNumber");
        builder.Property(u => u.IdentityNumberSalt).HasColumnName("IdentitySalt");
        builder.Property(u => u.IdentityNumberHash).HasColumnName("IdentityHash");
        builder.Property(u => u.Reference).HasColumnName("Reference").IsRequired();
        builder.Property(u => u.CustomerCode).HasColumnName("CustomerCode");
        builder.Property(u => u.CarrierCompanyInfo).HasColumnName("CarrierCompanyInfo");
        builder.Property(u => u.UserState).HasColumnName("UserState");
        builder.Property(u => u.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(u => u.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(u => u.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(u => !u.DeletedDate.HasValue);

        builder.HasMany(u => u.UserOperationClaims);
        builder.HasMany(u => u.RefreshTokens);
        builder.HasMany(u => u.EmailAuthenticators);
        builder.HasMany(u => u.OtpAuthenticators);
        builder.HasMany(u => u.Baskets);
        builder.HasMany(u => u.Orders);

        builder.HasData(_seeds);

        builder.HasBaseType((string)null!);
    }

    public static Guid AdminId { get; } = Guid.NewGuid();
    private IEnumerable<User> _seeds
    {
        get
        {
            HashingHelper.CreatePasswordHash(
                password: "Llads81awd2a!asd!(",
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            User adminUser =
                new()
                {
                    Id = AdminId,
                    Email = "oncellhsyn@outlook.com",
                    TradeName = "HHH",
                    FirstName = "Hüseyin",
                    LastName = "ÖNCEL",
                    Country = "Türkiye",
                    City = "Konya",
                    District = "Meram",
                    Address = "Dere",
                    GsmNumber = "05555555555",
                    TaxOffice = "Konya VD",
                    TaxNumber = "6666444555",
                    IdentityNumberSalt = null,
                    IdentityNumberHash = null,
                    Reference = "Ben",
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    UserState = Domain.Enums.UserState.Confirmed
                };
            yield return adminUser;
        }
    }
}

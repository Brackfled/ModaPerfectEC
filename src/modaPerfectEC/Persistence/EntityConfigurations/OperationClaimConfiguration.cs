using Application.Features.Auth.Constants;
using Application.Features.OperationClaims.Constants;
using Application.Features.UserOperationClaims.Constants;
using Application.Features.Users.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Core.Security.Constants;
using Application.Features.Categories.Constants;
using Application.Features.SubCategories.Constants;
using Application.Features.Products.Constants;
using Application.Features.ProductVariants.Constants;
using Application.Features.MPFile.Constants;
using Application.Features.Baskets.Constants;
using Application.Features.BasketItems.Constants;
using Application.Features.Orders.Constants;








namespace Persistence.EntityConfigurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("OperationClaims").HasKey(oc => oc.Id);

        builder.Property(oc => oc.Id).HasColumnName("Id").IsRequired();
        builder.Property(oc => oc.Name).HasColumnName("Name").IsRequired();
        builder.Property(oc => oc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(oc => oc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(oc => oc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);

        builder.HasData(_seeds);

        builder.HasBaseType((string)null!);
    }

    public static int AdminId => 456;
    private IEnumerable<OperationClaim> _seeds
    {
        get
        {
            yield return new() { Id = AdminId, Name = GeneralOperationClaims.Admin };

            IEnumerable<OperationClaim> featureOperationClaims = getFeatureOperationClaims(AdminId);
            foreach (OperationClaim claim in featureOperationClaims)
                yield return claim;
        }
    }

#pragma warning disable S1854 // Unused assignments should be removed
    private IEnumerable<OperationClaim> getFeatureOperationClaims(int initialId)
    {
        int lastId = initialId;
        List<OperationClaim> featureOperationClaims = new();

        #region Auth
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = AuthOperationClaims.Admin },
                new() { Id = ++lastId, Name = AuthOperationClaims.Read },
                new() { Id = ++lastId, Name = AuthOperationClaims.Write },
                new() { Id = ++lastId, Name = AuthOperationClaims.RevokeToken },
            ]
        );
        #endregion

        #region OperationClaims
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Admin },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Read },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Write },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Create },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Update },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Delete },
            ]
        );
        #endregion

        #region UserOperationClaims
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Admin },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Read },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Write },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Create },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Update },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Delete },
            ]
        );
        #endregion

        #region Users
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = UsersOperationClaims.Admin },
                new() { Id = ++lastId, Name = UsersOperationClaims.Read },
                new() { Id = ++lastId, Name = UsersOperationClaims.Write },
                new() { Id = ++lastId, Name = UsersOperationClaims.Create },
                new() { Id = ++lastId, Name = UsersOperationClaims.Update },
                new() { Id = ++lastId, Name = UsersOperationClaims.Delete },
            ]
        );
        #endregion

        featureOperationClaims.Add(
                new() { Id = ++lastId, Name = AuthOperationClaims.UpdateUserState}
            );

        
        #region Categories CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Admin },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Read },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Write },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Create },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Update },
                new() { Id = ++lastId, Name = CategoriesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region SubCategories CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = SubCategoriesOperationClaims.Admin },
                new() { Id = ++lastId, Name = SubCategoriesOperationClaims.Read },
                new() { Id = ++lastId, Name = SubCategoriesOperationClaims.Write },
                new() { Id = ++lastId, Name = SubCategoriesOperationClaims.Create },
                new() { Id = ++lastId, Name = SubCategoriesOperationClaims.Update },
                new() { Id = ++lastId, Name = SubCategoriesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Products CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ProductsOperationClaims.Admin },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Read },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Write },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Create },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Update },
                new() { Id = ++lastId, Name = ProductsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region ProductVariants CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ProductVariantsOperationClaims.Admin },
                new() { Id = ++lastId, Name = ProductVariantsOperationClaims.Read },
                new() { Id = ++lastId, Name = ProductVariantsOperationClaims.Write },
                new() { Id = ++lastId, Name = ProductVariantsOperationClaims.Create },
                new() { Id = ++lastId, Name = ProductVariantsOperationClaims.Update },
                new() { Id = ++lastId, Name = ProductVariantsOperationClaims.Delete },
            ]
        );
        #endregion

        featureOperationClaims.AddRange(
           [
                new() {Id = ++lastId, Name = MPFilesOperationClaims.Create},              
                new() {Id = ++lastId, Name = MPFilesOperationClaims.Deleted},              
                new() {Id = ++lastId, Name = MPFilesOperationClaims.Read},              
           ]
        );

        
        #region Baskets CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = BasketsOperationClaims.Admin },
                new() { Id = ++lastId, Name = BasketsOperationClaims.Read },
                new() { Id = ++lastId, Name = BasketsOperationClaims.Write },
                new() { Id = ++lastId, Name = BasketsOperationClaims.Create },
                new() { Id = ++lastId, Name = BasketsOperationClaims.Update },
                new() { Id = ++lastId, Name = BasketsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region BasketItems CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = BasketItemsOperationClaims.Admin },
                new() { Id = ++lastId, Name = BasketItemsOperationClaims.Read },
                new() { Id = ++lastId, Name = BasketItemsOperationClaims.Write },
                new() { Id = ++lastId, Name = BasketItemsOperationClaims.Create },
                new() { Id = ++lastId, Name = BasketItemsOperationClaims.Update },
                new() { Id = ++lastId, Name = BasketItemsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Orders CRUD
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = OrdersOperationClaims.Admin },
                new() { Id = ++lastId, Name = OrdersOperationClaims.Read },
                new() { Id = ++lastId, Name = OrdersOperationClaims.Write },
                new() { Id = ++lastId, Name = OrdersOperationClaims.Create },
                new() { Id = ++lastId, Name = OrdersOperationClaims.Update },
                new() { Id = ++lastId, Name = OrdersOperationClaims.Delete },
            ]
        );
        #endregion
        
        return featureOperationClaims;
    }
#pragma warning restore S1854 // Unused assignments should be removed
}

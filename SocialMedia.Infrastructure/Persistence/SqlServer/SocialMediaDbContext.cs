using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;

namespace SocialMedia.Infrastructure.Persistence.SqlServer;

public class SocialMediaDbContext : DbContext
{
    public SocialMediaDbContext(DbContextOptions<SocialMediaDbContext> options)
        : base(options) { }

    public virtual DbSet<Account> Accounts { get; set; }
    public virtual DbSet<Post> Posts { get; set; }
    public virtual DbSet<Profile> Profiles { get; set; }
    public virtual DbSet<Connection> Connections { get; set; }

    public async Task<List<Post>> GetFollowedProfilePosts(Guid profileId, int page, int pageSize)
    {
		var profileIdParam = new SqlParameter("@ProfileId", profileId);
        var pageParam = new SqlParameter("@PageNumber", page);
        var pageSizeParam = new SqlParameter("@RowsPerPage", pageSize);

        return await Posts.FromSqlRaw(
			"EXEC dbo.GetFollowedProfilesPosts @ProfileId, @PageNumber, @RowsPerPage", 
            profileIdParam, 
            pageParam, 
            pageSizeParam
        ).ToListAsync();
	}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Account>(entity =>
        {
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Fullname).IsRequired();
			entity.Property(e => e.Password).IsRequired();
			entity.Property(e => e.Email).IsRequired();
			entity.Property(e => e.Birthdate).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired(false);
			entity.OwnsOne(e => e.Phone, p =>
            {
                p.Property(e => e.Number).HasColumnName("Number");
                p.Property(e => e.CountryCode).HasColumnName("CountryCode");
                p.Property(e => e.AreaCode).HasColumnName("AreaCode");
                p.Property(e => e.Extension).HasColumnName("Extension");
			});
			entity.HasMany(e => e.Profiles).WithOne().HasForeignKey(e => e.AccountId);
		});

        builder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ExhibitionName).IsRequired();
            entity.Property(e => e.About).IsRequired();
            entity.Property(e => e.Picture).IsRequired();
            entity.Property(e => e.Occupation).IsRequired();
			entity.Property(e => e.CreatedAt).IsRequired();
			entity.Property(e => e.UpdatedAt).IsRequired(false);
            entity.Property(e => e.IsEnabled).HasDefaultValue(true);
			entity.OwnsOne(e => e.Address, a =>
            {
				a.Property(e => e.Street).HasColumnName("Street");
				a.Property(e => e.Number).HasColumnName("Number");
				a.Property(e => e.Neighbourhood).HasColumnName("Neighbourhood");
				a.Property(e => e.City).HasColumnName("City");
				a.Property(e => e.State).HasColumnName("State");
				a.Property(e => e.Country).HasColumnName("Country");
				a.Property(e => e.ZipCode).HasColumnName("ZipCode");
                a.Property(e => e.Region).HasColumnName("Region");
			});
            entity.HasMany(e => e.Posts).WithOne().HasForeignKey(e => e.ProfileId);
            entity.HasOne(e => e.Account).WithMany(e => e.Profiles).HasForeignKey(e => e.AccountId);
            entity.HasMany(e => e.Followers).WithMany(e => e.Followeds)
                .UsingEntity<Connection>(
                    e => e.HasOne(e => e.Follower).WithMany().HasForeignKey(e => e.IdFollower),
                    e => e.HasOne(e => e.Followed).WithMany().HasForeignKey(e => e.IdFollowed),
                    e => e.HasKey(e => e.Id));
            entity.HasMany(e => e.Followeds).WithMany(e => e.Followers)
				.UsingEntity<Connection>(
                    e => e.HasOne(e => e.Follower).WithMany().HasForeignKey(e => e.IdFollowed),
                    e => e.HasOne(e => e.Followed).WithMany().HasForeignKey(e => e.IdFollower),
                    e => e.HasKey(e => e.Id));
        });

        builder.Entity<Post>(entity =>
        {
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Content).IsRequired();
            entity.Property(e => e.PostDate).IsRequired();
            entity.Property(e => e.Likes).HasDefaultValue(0);
			entity.Property(e => e.CreatedAt).IsRequired();
			entity.Property(e => e.UpdatedAt).IsRequired(false);
			entity.HasOne(e => e.Profile).WithMany(e => e.Posts).HasForeignKey(e => e.ProfileId);
		});

        builder.Entity<Connection>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.IdFollower).IsRequired();
            entity.Property(e => e.IdFollowed).IsRequired();
            entity.Property(e => e.Date).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired(false);
            entity.HasOne(e => e.Follower).WithMany().HasForeignKey(e => e.IdFollower);
            entity.HasOne(e => e.Followed).WithMany().HasForeignKey(e => e.IdFollowed);
        });

        base.OnModelCreating(builder);
	}
}

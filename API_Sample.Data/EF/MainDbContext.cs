using System;
using System.Collections.Generic;
using API_Sample.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_Sample.Data.EF;

public partial class MainDbContext : DbContext
{
    public MainDbContext()
    {
    }

    public MainDbContext(DbContextOptions<MainDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Banner> Banners { get; set; }

    public virtual DbSet<Business> Businesses { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<ContactLog> ContactLogs { get; set; }

    public virtual DbSet<EmployeeProfile> EmployeeProfiles { get; set; }

    public virtual DbSet<FeedBack> FeedBacks { get; set; }

    public virtual DbSet<FieldOfActivity> FieldOfActivities { get; set; }

    public virtual DbSet<LibraryCategory> LibraryCategories { get; set; }

    public virtual DbSet<LibraryFile> LibraryFiles { get; set; }

    public virtual DbSet<LibraryImage> LibraryImages { get; set; }

    public virtual DbSet<Libraryy> Libraryys { get; set; }

    public virtual DbSet<LoginHistory> LoginHistories { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<NewsCategory> NewsCategories { get; set; }

    public virtual DbSet<NewsComment> NewsComments { get; set; }

    public virtual DbSet<NewsImage> NewsImages { get; set; }

    public virtual DbSet<NewsLike> NewsLikes { get; set; }

    public virtual DbSet<NewsShare> NewsShares { get; set; }

    public virtual DbSet<NewsView> NewsViews { get; set; }

    public virtual DbSet<NotificationType> NotificationTypes { get; set; }

    public virtual DbSet<Partner> Partners { get; set; }

    public virtual DbSet<PartnerCategory> PartnerCategories { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostComment> PostComments { get; set; }

    public virtual DbSet<PostImage> PostImages { get; set; }

    public virtual DbSet<PostLike> PostLikes { get; set; }

    public virtual DbSet<PostShare> PostShares { get; set; }

    public virtual DbSet<PostTag> PostTags { get; set; }

    public virtual DbSet<PostView> PostViews { get; set; }

    public virtual DbSet<Recruitment> Recruitments { get; set; }

    public virtual DbSet<RecruitmentApplication> RecruitmentApplications { get; set; }

    public virtual DbSet<RecruitmentStatus> RecruitmentStatuses { get; set; }

    public virtual DbSet<SeoDatum> SeoData { get; set; }

    public virtual DbSet<UserNotification> UserNotifications { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-0DG5ETM\\SQLEXPRESS01;Database=CRD;Trusted_Connection=True;TrustServerCertificate=True;");
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=LEVANTUAN;Initial Catalog=CRD;Integrated Security=True;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__Account__F3DBC5731A21DA2D");

            entity.HasOne(d => d.Employee).WithMany(p => p.Accounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Account_Employee");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Account_UserRole");
        });

        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Activity__3213E83F274204A2");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.ActivityLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ActivityLog_Account");
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AuditLog__3213E83FAD943746");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.AuditLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AuditLogs__usern__3C34F16F");
        });

        modelBuilder.Entity<Banner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Banner__3213E83F963FAC79");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Business>(entity =>
        {
            entity.HasKey(e => e.BusinessId).HasName("PK__Business__DC0DC16E52EC062F");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Contact__3213E83F538818D3");

            entity.Property(e => e.ContactStatus).HasDefaultValue("Pending");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<ContactLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ContactL__3213E83FECE282A8");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Contact).WithMany(p => p.ContactLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContactLog_Contact");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.ContactLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ContactLog_Account");
        });

        modelBuilder.Entity<EmployeeProfile>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__C52E0BA83868B31E");
        });

        modelBuilder.Entity<FeedBack>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FeedBack__3213E83F0981B98C");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Contact).WithMany(p => p.FeedBacks).HasConstraintName("FK_FeedBacks_Contact");
        });

        modelBuilder.Entity<FieldOfActivity>(entity =>
        {
            entity.HasKey(e => e.FieldId).HasName("PK__FieldOfA__1BB6F43E85B99BD5");
        });

        modelBuilder.Entity<LibraryCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LibraryC__3213E83F6C7EDB49");
        });

        modelBuilder.Entity<LibraryFile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LibraryF__3213E83F874E1EB4");

            entity.Property(e => e.UploadedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Library).WithMany(p => p.LibraryFiles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LibraryFiles_Library");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.LibraryFiles)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LibraryFiles_Account");
        });

        modelBuilder.Entity<LibraryImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LibraryI__3213E83F4DD49311");

            entity.Property(e => e.UploadedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Library).WithMany(p => p.LibraryImages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LibraryImage_Library");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.LibraryImages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LibraryImage_Account");
        });

        modelBuilder.Entity<Libraryy>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Libraryy__3213E83FBF0887F3");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Category).WithMany(p => p.Libraryys)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Library_Category");
        });

        modelBuilder.Entity<LoginHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LoginHis__3213E83F8435BE66");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.LoginHistories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LoginHistory_Account");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.NewsId).HasName("PK__News__4C27CCD8FE1A8D1F");

            entity.Property(e => e.Status).HasDefaultValue(true);

            entity.HasOne(d => d.NewsCategory).WithMany(p => p.News)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_News_NewsCategory");
        });

        modelBuilder.Entity<NewsCategory>(entity =>
        {
            entity.HasKey(e => e.NewsCategoryId).HasName("PK__NewsCate__01154D786E594BC9");

            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        modelBuilder.Entity<NewsComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NewsComm__3213E83F849079EC");

            entity.HasOne(d => d.News).WithMany(p => p.NewsComments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NewsComment_News");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.NewsComments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NewsComment_Account");
        });

        modelBuilder.Entity<NewsImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NewsImag__3213E83FF329F6E6");

            entity.Property(e => e.IsAvatar).HasDefaultValue(false);

            entity.HasOne(d => d.News).WithMany(p => p.NewsImages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NewsImage_News");
        });

        modelBuilder.Entity<NewsLike>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NewsLike__3213E83FFE9D6DFF");

            entity.HasOne(d => d.News).WithMany(p => p.NewsLikes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NewsLike_News");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.NewsLikes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NewsLike_Account");
        });

        modelBuilder.Entity<NewsShare>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NewsShar__3213E83F30535059");

            entity.HasOne(d => d.News).WithMany(p => p.NewsShares)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NewsShare_News");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.NewsShares)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NewsShare_Account");
        });

        modelBuilder.Entity<NewsView>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__NewsView__3213E83F7CA4B00F");

            entity.HasOne(d => d.News).WithMany(p => p.NewsViews)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NewsView_News");
        });

        modelBuilder.Entity<NotificationType>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__E059842FDBD4A309");
        });

        modelBuilder.Entity<Partner>(entity =>
        {
            entity.HasKey(e => e.PartnerId).HasName("PK__Partners__576F1B276493B2AA");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Category).WithMany(p => p.Partners)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Partners_Categories");
        });

        modelBuilder.Entity<PartnerCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PartnerC__3213E83FF38FF3C3");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permissi__3213E83F85D1F64A");

            entity.HasOne(d => d.Role).WithMany(p => p.Permissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Permission_UserRole");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK__Post__3ED78766BC893D4E");

            entity.Property(e => e.Status).HasDefaultValue(true);

            entity.HasOne(d => d.Field).WithMany(p => p.Posts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Post_FieldOfActivity");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Posts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Post_Account");
        });

        modelBuilder.Entity<PostComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PostComm__3213E83F43998367");

            entity.HasOne(d => d.Post).WithMany(p => p.PostComments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PostComment_Post");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.PostComments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PostComment_Account");
        });

        modelBuilder.Entity<PostImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PostImag__3213E83F497DFF6B");

            entity.Property(e => e.IsAvatar).HasDefaultValue(false);

            entity.HasOne(d => d.Post).WithMany(p => p.PostImages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PostImage_Post");
        });

        modelBuilder.Entity<PostLike>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PostLike__3213E83F7EE62166");

            entity.HasOne(d => d.Post).WithMany(p => p.PostLikes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PostLike_Post");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.PostLikes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PostLike_Account");
        });

        modelBuilder.Entity<PostShare>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PostShar__3213E83F756C83F9");

            entity.HasOne(d => d.Post).WithMany(p => p.PostShares)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PostShare_Post");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.PostShares)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PostShare_Account");
        });

        modelBuilder.Entity<PostTag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PostTag__3213E83F2036C597");

            entity.HasOne(d => d.Post).WithMany(p => p.PostTags)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PostTag_Post");
        });

        modelBuilder.Entity<PostView>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PostView__3213E83F2E5CD723");

            entity.HasOne(d => d.Post).WithMany(p => p.PostViews)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PostView_Post");
        });

        modelBuilder.Entity<Recruitment>(entity =>
        {
            entity.HasKey(e => e.RecruitmentId).HasName("PK__Recruitm__CCF7A0ADE4BDC20D");

            entity.HasOne(d => d.Business).WithMany(p => p.Recruitments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Recruitment_BusinessInfomation");

            entity.HasOne(d => d.CreateByNavigation).WithMany(p => p.RecruitmentCreateByNavigations).HasConstraintName("FK_Recruitment_CreateBy");

            entity.HasOne(d => d.UpdateByNavigation).WithMany(p => p.RecruitmentUpdateByNavigations).HasConstraintName("FK_Recruitment_UpdateBy");
        });

        modelBuilder.Entity<RecruitmentApplication>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PK__Recruitm__3BCBDCF29260C8FD");

            entity.HasOne(d => d.Recruitment).WithMany(p => p.RecruitmentApplications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RecruitmentApplication_Recruitment");
        });

        modelBuilder.Entity<RecruitmentStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Recruitm__3213E83FB3CD50BE");

            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Application).WithMany(p => p.RecruitmentStatuses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RecruitmentStatus_RecruitmentApplication");
        });

        modelBuilder.Entity<SeoDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SeoData__3213E83FE43CB562");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<UserNotification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserNoti__3213E83F1A1F5D7C");

            entity.HasOne(d => d.Notification).WithMany(p => p.UserNotifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserNotification_NotificationType");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.UserNotifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserNotification_Account");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__UserRole__760965CC149A5018");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

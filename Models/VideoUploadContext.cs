using Microsoft.EntityFrameworkCore;

namespace VideoApi.Models
{
    public class VideoUploadContext:DbContext
    {
        public VideoUploadContext(DbContextOptions<VideoUploadContext> options) : base(options)
        {

        }

        public virtual DbSet<VideoMetaData> VideoMetaData { get; set; }
        public virtual DbSet<TemporaryChunkDetails> TemporaryChunkDetails { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<VideoMetaData>(
                e => {
                    e.HasKey(e => e.VideoId).HasName("PK_VIDEOID");
                    e.Property(p => p.VideoId).HasMaxLength(100);
                    e.Property(p => p.VideoTitle).HasMaxLength(100).IsRequired();
                    e.Property(p => p.VideoDescription).HasMaxLength(500).IsRequired();
                    e.Property(p => p.VideoLocation).HasColumnType("text").IsRequired();
                    e.Property(p => p.ThumbnailLocation).HasColumnType("text").IsRequired();
                    e.Property(p => p.Tags).IsRequired(false);
                    e.Property(p => p.isVideoUploaded).IsRequired().HasColumnType("bit").HasDefaultValue(0);
                    e.Property(p => p.isVideoProcessed).IsRequired().HasColumnType("bit").HasDefaultValue(0);
                }
            );

            modelBuilder.Entity<TemporaryChunkDetails>(
                e =>
                {
                    e.HasKey(e => e.id).HasName("PK_TEMP_ID");
                    e.Property(e => e.id).ValueGeneratedOnAdd();
                    e.HasOne(e => e.Video).WithOne(e => e.TemporaryChunks).HasForeignKey<TemporaryChunkDetails>(e => e.VideoId);
                }    
            );
        }
    }
}

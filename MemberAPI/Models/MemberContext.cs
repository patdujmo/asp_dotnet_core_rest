using Microsoft.EntityFrameworkCore;

namespace MemberAPI.Models;

public class MemberContext : DbContext
{
  public MemberContext(DbContextOptions<MemberContext> options): base(options)
  {
  }

  public DbSet<Member> MemberItems { get; set; } = null!;
}
using Microsoft.EntityFrameworkCore;

namespace MemberAPI.Models;

public class MemberMariaDBContext : DbContext
{
  public MemberMariaDBContext(DbContextOptions<MemberMariaDBContext> options): base(options)
  {
  }

  public DbSet<Member> MemberItems { get; set; } = null!;
}
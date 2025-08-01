using Microsoft.EntityFrameworkCore;

namespace Persistence;

internal sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options);
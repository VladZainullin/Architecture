using Microsoft.EntityFrameworkCore;

namespace Persistence;

internal sealed class AppDbContext(DbContextOptions options) : DbContext(options);
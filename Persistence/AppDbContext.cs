using Microsoft.EntityFrameworkCore;

namespace Persistence;

public sealed class AppDbContext(DbContextOptions options) : DbContext(options);
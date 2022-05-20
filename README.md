# EFCoreExample
There is often confusion among the examples you see out there when trying to solve for a 
foreign key relationship that no longer seems to work.  I went through the same thing and 
a frustrating amount of time went to figuring out these monsters.

Problem might be the fact that EFCore ALWAYS does lazy load of relationships.  
There are 2 ways to fix this in applying eager loading

1) See `HomeController` - `Index` Action.  

```
return View(await _db
  .Companies
  .Include("CompanyType")
  .ToListAsync());
```

This applies the fix just to this particular query.  You'll need to do this for every other 
place where you need this relationship eager loaded.   

2) See `Company.cs` in models (All EF classes are in the same file) - Look in the `DbContext` class

```
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
  modelBuilder
    .Entity<Company>()
    .Navigation(d => d.CompanyType)
    .AutoInclude();
  base.OnModelCreating(modelBuilder);
}
```

This is the same area where they tell you to do the `HasOne()`, `HasMany()` stuff.  You just don't need any of that.
This sets eager loading for ALL future queries.  

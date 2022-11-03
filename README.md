# MariaDBNaturalSortExtension

An EntityFramework extension plugin to use the new <a href="https://mariadb.com/kb/en/natural_sort_key/">NATURAL_SORT_KEY MariaDB function</a> with Pomelo.EntityFrameworkCore.MySql
<br/>
<br/>
<b>How to use:</b>
<br/>
<br/>
First set up your context to use this extension:
```csharp
services.AddDbContext<MyDbContext>(options => options.UseMySql(connectionString,
    ServerVersion.AutoDetect(connectionString),
        opt => opt.AddNaturalSortSupport()));
```
Second, use the <b>UseNaturalSort()</b> extension method on string properties that you want to sort naturally:
```csharp
var orderedEmployees = await _db.Employees.AsNoTracking().OrderBy(e => e.LastIp.UseNaturalSort()).ToArrayAsync();
```

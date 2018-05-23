# AutoMapper - QueryableExtensions - Issue 2643

[issue link](https://github.com/AutoMapper/AutoMapper/issues/2643)

This example uses EF Core migrations. Run this to get started `dotnet ef database update -c MyDbContext`

The project will seed on startup.

### Program.cs Example A

This is the vanilla example as taken from the official docs.
This example generates a query per InventoryServer record:

```sql
	exec sp_executesql N'SELECT [dto0].[InventoryServerId], [dto0].[ProductId] AS [Id], CASE
		WHEN [dto0].[ProductId] IS NOT NULL
		THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)
	END, [dto.Product].[Name]
	FROM [InventoryProductLine] AS [dto0]
	INNER JOIN [Product] AS [dto.Product] ON [dto0].[ProductId] = [dto.Product].[Id]
	WHERE @_outer_Id = [dto0].[InventoryServerId]',N'@_outer_Id int',@_outer_Id=2
```

### Program.cs Example B - `Include()`, `ThenInclude()`

No change. This example generates a query per InventoryServer record, same as above.

### Program.cs Example C - Manually building a projection with `.Select()`s

No change. This example generates a query per InventoryServer record, same as above.

### Program.cs Example D - No projections at all.

Much better, only generates two queries no matter how many records there are. I am not sure why there are two still though.

## 1
```sql
	SELECT [x.InventoryServerProductLines].[InventoryServerId], [x.InventoryServerProductLines].[ProductId], [i.Product].[Id], [i.Product].[Name]
	FROM [InventoryProductLine] AS [x.InventoryServerProductLines]
	INNER JOIN [Product] AS [i.Product] ON [x.InventoryServerProductLines].[ProductId] = [i.Product].[Id]
	INNER JOIN (
		SELECT [x0].[Id]
		FROM [InventoryServer] AS [x0]
	) AS [t] ON [x.InventoryServerProductLines].[InventoryServerId] = [t].[Id]
	ORDER BY [t].[Id]
```

## 2
```sql
	SELECT [x].[Id], [x].[Name]
	FROM [InventoryServer] AS [x]
	ORDER BY [x].[Id]
```

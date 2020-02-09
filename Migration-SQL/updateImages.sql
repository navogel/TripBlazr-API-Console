update [Location] set ImageUrl = CONCAT(LocationId, '.jpg') WHERE IsActive = 1 AND LocationId NOT BETWEEN 110 and 231

--select LocationId, ImageUrl from [Location]
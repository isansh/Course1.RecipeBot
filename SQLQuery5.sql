USE RecipeDB;
CREATE TABLE FavoriteRecipes
(
    TableNumber INT IDENTITY,
    Recipe NVARCHAR(2000),
    Id BIGINT 
	PRIMARY KEY(TableNumber, Id)
)

INSERT FavoriteRecipes VALUES (N'Рецепт завтрака', 44)
INSERT FavoriteRecipes VALUES (N'Рецепт обеда', 4343847)
INSERT FavoriteRecipes VALUES (N'Рецепт завтрака', 4343847)
INSERT FavoriteRecipes VALUES (N'Рецепт ужина', 44)
SELECT *
FROM FavoriteRecipes
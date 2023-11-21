﻿using SpaceTrading.Production.General.Resources;

namespace SpaceTrading.Production.Components.ResourceProduction.Recipes
{
    public record ProductionRecipe(ResourceQuantity ResourceQuantity, ProductionRecipeIngredients Ingredients,
        float TimeTaken);
}
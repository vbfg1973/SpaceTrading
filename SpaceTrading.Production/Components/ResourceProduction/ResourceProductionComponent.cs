using SpaceTrading.Production.Components.ResourceProduction.Recipes;
using SpaceTrading.Production.Components.ResourceProduction.StateMachine;
using SpaceTrading.Production.General.Resources;
using Stateless;

namespace SpaceTrading.Production.Components.ResourceProduction
{
    public class ResourceProductionComponent
    {
        private readonly StateMachine<ResourceProductionState, ResourceProductionTrigger> _stateMachine;

        public ResourceProductionComponent(ProductionRecipe recipe)
        {
            Recipe = recipe;
            ResetTimeRemaining();

            _stateMachine =
                new StateMachine<ResourceProductionState, ResourceProductionTrigger>(() => CurrentState,
                    s => CurrentState = s);

            ConfigureStateMachine();
        }

        public ProductionRecipe Recipe { get; set; }
        public float TimeRemaining { get; set; }

        public ResourceProductionState CurrentState { get; set; } = ResourceProductionState.ReadyToStart;

        public void Update(float elapsedSeconds)
        {
            TimeRemaining -= elapsedSeconds;

            if (TimeRemaining <= 0 && CurrentState == ResourceProductionState.InProgress)
                _stateMachine.Fire(ResourceProductionTrigger.Completed);
        }

        public bool TryStartProduction(ProductionRecipeIngredients ingredients)
        {
            // TODO - Need to make class IEquatable!
            if (CurrentState == ResourceProductionState.ReadyToStart && ingredients == Recipe.Ingredients)
                _stateMachine.Fire(ResourceProductionTrigger.Start);

            return CurrentState == ResourceProductionState.InProgress;
        }

        public bool TryGetCompletedResource(out ResourceQuantity resourceQuantity)
        {
            resourceQuantity = null!;

            if (CurrentState != ResourceProductionState.ProductionRunCompleted)
                return false;

            resourceQuantity = Recipe.ResourceQuantity;
            _stateMachine.Fire(ResourceProductionTrigger.CompletedResourcesRemoved);

            return CurrentState == ResourceProductionState.ReadyToStart;
        }

        private void ConfigureStateMachine()
        {
            _stateMachine.Configure(ResourceProductionState.ReadyToStart)
                .Permit(ResourceProductionTrigger.Start, ResourceProductionState.InProgress)
                .Ignore(ResourceProductionTrigger.Completed)
                .Ignore(ResourceProductionTrigger.CompletedResourcesRemoved);

            _stateMachine.Configure(ResourceProductionState.InProgress)
                .Permit(ResourceProductionTrigger.Completed, ResourceProductionState.ProductionRunCompleted)
                .Ignore(ResourceProductionTrigger.Start)
                .Ignore(ResourceProductionTrigger.CompletedResourcesRemoved)
                .OnEntry(ResetTimeRemaining);

            _stateMachine.Configure(ResourceProductionState.ProductionRunCompleted)
                .Permit(ResourceProductionTrigger.CompletedResourcesRemoved, ResourceProductionState.ReadyToStart)
                .Ignore(ResourceProductionTrigger.Start)
                .Ignore(ResourceProductionTrigger.Completed);
        }

        private void ResetTimeRemaining()
        {
            TimeRemaining = Recipe.TimeTaken;
        }
    }
}
using SpaceTrading.Production.General.Resources;

namespace SpaceTrading.Production.Components.ResourceStorage
{
    public class ResourceStorageComponent
    {
        public ResourceStorageComponent(int volume)
        {
            Volume = volume;
        }

        public Dictionary<Resource, ResourceQuantity> Storage { get; set; } = new();

        public int Volume { get; set; }

        public int VolumeTaken => Storage.Values.Sum(x => x.Volume);

        public int VolumeRemaining => Volume - VolumeTaken;


        public IEnumerable<ResourceQuantity> OrderedByVolume()
        {
            return Storage.Values.OrderByDescending(x => x.Volume).ThenBy(x => x.Resource.Name);
        }

        public bool WillFit(int volume)
        {
            return Volume - VolumeTaken >= volume;
        }

        public bool HasAvailable(ResourceQuantity resourceQuantity)
        {
            if (!Storage.ContainsKey(resourceQuantity.Resource)) return false;

            return Storage[resourceQuantity.Resource].Quantity >= resourceQuantity.Quantity;
        }

        public bool TryAdd(ResourceQuantity resourceQuantity)
        {
            if (VolumeTaken < resourceQuantity.Volume) return false;

            Storage.TryAdd(resourceQuantity.Resource,
                new ResourceQuantity { Resource = resourceQuantity.Resource, Quantity = 0 });

            Storage[resourceQuantity.Resource].Quantity += resourceQuantity.Quantity;

            return true;
        }

        public bool TryRemove(ResourceQuantity resourceQuantity, out ResourceQuantity returnedResourceQuantity)
        {
            returnedResourceQuantity = null!;
            if (!Storage.ContainsKey(resourceQuantity.Resource) ||
                Storage[resourceQuantity.Resource].Quantity < resourceQuantity.Quantity) return false;

            returnedResourceQuantity = new ResourceQuantity
                { Resource = resourceQuantity.Resource, Quantity = resourceQuantity.Quantity };
            Storage[resourceQuantity.Resource].Quantity -= resourceQuantity.Quantity;

            if (Storage[resourceQuantity.Resource].Quantity <= 0) Storage.Remove(resourceQuantity.Resource);

            return true;
        }
    }
}
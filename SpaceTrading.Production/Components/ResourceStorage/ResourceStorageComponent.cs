using SpaceTrading.Production.General.Resources;

namespace SpaceTrading.Production.Components.ResourceStorage
{
    public class ResourceStorageComponent
    {
        public ResourceStorageComponent(int volume)
        {
            Storage = new Dictionary<Resource, ResourceQuantity>();
            Volume = volume;
        }

        private Dictionary<Resource, ResourceQuantity> Storage { get; }

        public int Volume { get; set; }

        public int VolumeTaken => Storage.Values.Sum(x => x.Volume);

        public int VolumeRemaining => Volume - VolumeTaken;


        public IEnumerable<ResourceQuantity> OrderedByVolume()
        {
            return Storage.Values.OrderByDescending(x => x.Volume).ThenBy(x => x.Resource.Name);
        }

        internal void AddVolume(int extraVolumeRequired)
        {
            Volume += extraVolumeRequired;
        }
        
        public bool WillFit(int volume)
        {
            return Volume - VolumeTaken >= volume;
        }

        public bool HasAvailable(ResourceQuantity resourceQuantity)
        {
            return Storage.ContainsKey(resourceQuantity.Resource) && Storage[resourceQuantity.Resource].HasAmount(resourceQuantity);
        }

        public bool TryAdd(ResourceQuantity resourceQuantity)
        {
            if (VolumeRemaining < resourceQuantity.Volume) return false;

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
namespace Buckets.Containers
{
    class RainBarrel : Container
    {
        public enum BarrelSize
        {
            small = 80, 
            medium = 120,
            large = 160
        }
        public RainBarrel(BarrelSize size, double content = 0, string name = "Rain Barrel")
        {
            Content = content;
            Name = name;
            Size = (double)size; 
        }
    }
}

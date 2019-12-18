namespace Buckets.Containers
{
    class RainBarrel : Container
    {
        public enum barrelSize
        {
            small,
            medium,
            large
        }
        public RainBarrel(barrelSize size, double content = 0, string name = "Rain Barrel")
        {

            Content = content;
            Name = name;
        }
    }
}

namespace Buckets.Containers
{
    class OilBarrel : Container
    {
        public OilBarrel(double content = 0, string name = "Oil Barrel")
        {
            MinSize = Size = 159;
            Content = content;
            Name = name;
        }
    }
}

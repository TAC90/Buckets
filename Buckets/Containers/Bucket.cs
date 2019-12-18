namespace Buckets.Containers
{
    class Bucket : Container
    {
        public Bucket(double size, double content = 0, string name = "Bucket")
        {
            MinSize = 10;
            Size = size;
            Content = content;
            Name = name;
        }
    }
}

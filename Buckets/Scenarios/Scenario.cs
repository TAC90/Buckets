using Buckets.Containers;

namespace Buckets.Scenarios
{
    class Scenario
    {
        public void Scenario1()
        {
            //TODO: Scenario 1
            OilBarrel a = new OilBarrel();
            //RainBarrel b = new RainBarrel(112);
            RainBarrel b = new RainBarrel(RainBarrel.barrelSize.medium);
            Bucket c = new Bucket(9, name: "Some Stupid Bucket");
            a.Fill(7.0);
        }
    }
}

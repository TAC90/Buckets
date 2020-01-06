using Buckets.Containers;
using System;
using System.Collections.Generic;

namespace Buckets.Scenarios
{
    class Scenario
    {
        public void Scenario1()
        {
            //TODO: Scenario 1
            List<Container> containerList = new List<Container>
            {
                new Bucket(15,name:"First Bucket"),
                new Bucket(9, name: "Second Bucket"),
                new OilBarrel(123, "Barrel One"),
                new RainBarrel(RainBarrel.BarrelSize.medium)
            };
            Container.AllowOverflow = true;
            containerList[0].Fill(7.3);
            containerList[1].Fill(12.7);
            containerList[1].Empty(2);
            containerList[0].Transfer(containerList[2]);
            containerList[1].Transfer(containerList[2], 8);
            containerList[1].Transfer(containerList[2], 8);
            Container.AllowOverflow = false;
            containerList[3].Fill(110);
            containerList[2].Transfer(containerList[3]);
            containerList[3].Empty();
            containerList[3].Empty(8.5);
            containerList[0].Fill(10);
            containerList[0].Fill(6);


            Console.ReadLine();
        }
    }
}

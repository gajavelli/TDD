using System;
using System.Collections.Generic;
//using NUnit.Framework;
using Xunit;

namespace TDD
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Item> mainItems = new List<Item>();
            List<Item> randomItems = new List<Item>();
            string TestletId = "TestletId1";
            //The items added to the mainItems are not in any particular order
            //...you could mix match in any order
            //however the final randomized list that 

            mainItems.Add( new Item("operational_item1", ItemTypeEnum.Operational));
            mainItems.Add( new Item("operational_item2", ItemTypeEnum.Operational));
            mainItems.Add( new Item("operational_item3", ItemTypeEnum.Operational));
            mainItems.Add( new Item("pretest_item1", ItemTypeEnum.Pretest));
            mainItems.Add( new Item("pretest_item2", ItemTypeEnum.Pretest));
            mainItems.Add( new Item("operational_item4", ItemTypeEnum.Operational));
            mainItems.Add( new Item("operational_item5", ItemTypeEnum.Operational));
            mainItems.Add( new Item("operational_item6", ItemTypeEnum.Operational));
            mainItems.Add( new Item("pretest_item3", ItemTypeEnum.Pretest));
            mainItems.Add( new Item("pretest_item4", ItemTypeEnum.Pretest));
            
            
            randomItems =  CreateTestAndRandomize(TestletId, mainItems);
            //Print to Console
            randomItems.ForEach(item => Console.WriteLine(item.ItemId));

        }
        public static List<Item> CreateTestAndRandomize(string testletId, List<Item> mainItems)
        {
            Testlet testlet = new Testlet(testletId, mainItems);
             return testlet.Randomize();
            
        }

    }

     public class Testlet {     
         public string TestletId;     
         private List<Item> Items; 
        static Random _random = new Random();
        public Testlet(string testletId, List<Item> items)     
        {         
            TestletId = testletId;         
            Items = items;     
        } 
        
        public List<Item> Randomize()     {         //Items private collection has 6 Operational and 4 Pretest Items. Randomize the order of these items as per the requirement (with TDD) 
            List<Item> returnItems = new List<Item>();
            int n  =  Items.Count;
            Item[] array = Items.ToArray();
            List<Item> tempItems = new List<Item>();

            for (int i = 0; i < n; i++)
            {
                // Use Next on random instance with an argument.
                // ... The argument is an exclusive bound.
                //     So we will not go past the end of the array.
                int r = i + _random.Next(n - i);
                //Console.WriteLine(r);
                Item t = array[r];
                if(returnItems.Count < 2 && t.ItemType == 0)
                {
                   returnItems.Add(t);
                }
                else{
                    tempItems.Add(t);
                }
                array[r] = array[i];
                array[i] = t;
            }

            //Add the tempItems to the bottom of the returnItems
            returnItems.AddRange(tempItems);
            //returnItems.ForEach(item => Console.WriteLine(item.ItemId));

            return returnItems;
        } 
    
    } 
    
    public class Item {    
        public string ItemId;     
        public ItemTypeEnum ItemType; 

        public Item(string itemId, ItemTypeEnum itemType)     
        {         
            ItemId = itemId;
            ItemType = itemType;     
        } 
    
    } 
    
    public enum ItemTypeEnum {     
        Pretest = 0,     
        Operational = 1 
    } 
 //[TestFixture]
   public class ProgramTests
    {   
        [Fact]
        public void Should_Return_RandomItems_top_two_pretest()
        {
            List<Item> mainItems = new List<Item>();
            mainItems.Add( new Item("operational_item1", ItemTypeEnum.Operational));
            mainItems.Add( new Item("operational_item2", ItemTypeEnum.Operational));
            mainItems.Add( new Item("operational_item4", ItemTypeEnum.Operational));
            mainItems.Add( new Item("operational_item5", ItemTypeEnum.Operational));
            mainItems.Add( new Item("operational_item3", ItemTypeEnum.Operational));
            mainItems.Add( new Item("pretest_item1", ItemTypeEnum.Pretest));
            mainItems.Add( new Item("pretest_item2", ItemTypeEnum.Pretest));
            mainItems.Add( new Item("operational_item6", ItemTypeEnum.Operational));
            mainItems.Add( new Item("pretest_item3", ItemTypeEnum.Pretest));
            mainItems.Add( new Item("pretest_item4", ItemTypeEnum.Pretest));

            List<Item> result = Program.CreateTestAndRandomize("ID1", mainItems);
            //make sure the top 2 items in the list are pretest
            bool bothPretest = (result.ToArray()[0].ItemType ==ItemTypeEnum.Pretest && result.ToArray()[1].ItemType ==ItemTypeEnum.Pretest)?true:false;
            Assert.True(bothPretest);

        }
    }


}

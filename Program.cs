using System;
using System.Collections.Generic;
using Xunit;

namespace TDD
{
    class Program
    {
        static void Main(string[] args) {
            List<Item> testQuestions = new List<Item>();
            List<Item> randomizedQuestions = new List<Item>();
            string assessmentId = "AssessmentId1";
            //The items added to the testQuestions list are not in any particular order
            //...you could mix match and add in any order
            //however the final randomized list should have the first and second 
            //question always of type pretest and rest eight mixed

            testQuestions.Add( new Item("operational_item1", ItemTypeEnum.Operational));
            testQuestions.Add( new Item("operational_item2", ItemTypeEnum.Operational));
            testQuestions.Add( new Item("operational_item3", ItemTypeEnum.Operational));
            testQuestions.Add( new Item("pretest_item1", ItemTypeEnum.Pretest));
            testQuestions.Add( new Item("pretest_item2", ItemTypeEnum.Pretest));
            testQuestions.Add( new Item("operational_item4", ItemTypeEnum.Operational));
            testQuestions.Add( new Item("operational_item5", ItemTypeEnum.Operational));
            testQuestions.Add( new Item("operational_item6", ItemTypeEnum.Operational));
            testQuestions.Add( new Item("pretest_item3", ItemTypeEnum.Pretest));
            testQuestions.Add( new Item("pretest_item4", ItemTypeEnum.Pretest));
            
            randomizedQuestions =  CreateTestAndRandomize(assessmentId, testQuestions);
            //Print to Console
            randomizedQuestions.ForEach(item => Console.WriteLine(item.ItemId));
        }
        public static List<Item> CreateTestAndRandomize(string testletId, List<Item> testQuestions) {
            Testlet testlet = new Testlet(testletId, testQuestions);
            
            return testlet.Randomize();
        }

    }

     public class Testlet {     
         public string TestletId;     
         private List<Item> Items; 
        static Random _random = new Random();
        public Testlet(string testletId, List<Item> items) {         
            TestletId = testletId;         
            Items = items;     
        } 
        
        public List<Item> Randomize() {         
            //Items private collection has 6 Operational and 4 Pretest Items. 
            //...Randomize the order of these items as per the requirement (with TDD) 
            
            List<Item> returnItems = new List<Item>();
            int totalCount  =  Items.Count;
            Item[] itemsArray = Items.ToArray();
            List<Item> tempItems = new List<Item>();

            for (int index = 0; index < totalCount; index++) {
                // Use Next on random instance with an argument.
                // ... The argument is an exclusive bound.
                //     So we will not go past the end of the array.
                int randomIndex = index + _random.Next(totalCount - index);
                Item randomItem = itemsArray[randomIndex];
                
                if(returnItems.Count < 2 && randomItem.ItemType == 0) {
                   returnItems.Add(randomItem);
                }
                else {
                    tempItems.Add(randomItem);
                }
                itemsArray[randomIndex] = itemsArray[index];
                itemsArray[index] = randomItem;
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

        public Item(string itemId, ItemTypeEnum itemType) {         
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
        public void Should_Return_RandomQuestions_Top_Two_Pretest() {
           
            List<Item> testQuestions = new List<Item>();
            testQuestions.Add( new Item("operational_item1", ItemTypeEnum.Operational));
            testQuestions.Add( new Item("operational_item2", ItemTypeEnum.Operational));
            testQuestions.Add( new Item("operational_item4", ItemTypeEnum.Operational));
            testQuestions.Add( new Item("operational_item5", ItemTypeEnum.Operational));
            testQuestions.Add( new Item("operational_item3", ItemTypeEnum.Operational));
            testQuestions.Add( new Item("pretest_item1", ItemTypeEnum.Pretest));
            testQuestions.Add( new Item("pretest_item2", ItemTypeEnum.Pretest));
            testQuestions.Add( new Item("operational_item6", ItemTypeEnum.Operational));
            testQuestions.Add( new Item("pretest_item3", ItemTypeEnum.Pretest));
            testQuestions.Add( new Item("pretest_item4", ItemTypeEnum.Pretest));

            List<Item> resultQuestions = Program.CreateTestAndRandomize("ID1", testQuestions);
            //make sure the top 2 items in the list are pretest
            bool isFirstAndSecondPretest = (resultQuestions.ToArray()[0].ItemType == ItemTypeEnum.Pretest && resultQuestions.ToArray()[1].ItemType == ItemTypeEnum.Pretest)?true:false;
            
            Assert.True(isFirstAndSecondPretest);

        }
    }


}

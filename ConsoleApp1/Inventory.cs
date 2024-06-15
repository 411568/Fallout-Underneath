using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Win32.SafeHandles;


namespace FalloutUnderneath
{
    public class Inventory
    {
        private int weightLimit;
        private List<Item> itemList;

        public Inventory(int playerLevel)
        {
            weightLimit = playerLevel * 10;
            itemList = new List<Item>();
        }

        public void UpdatePlayerLevel(int playerLevel)
        {
            DebugLogger.Log("Updating weight limit in player inventory");
    
            weightLimit = playerLevel * 10;
        }

        public int GetCurrentWeight()
        {
            // TODO
            return 0;
        }

        public bool CheckIfOverweight()
        {
            DebugLogger.Log("Checking if player is over the weight limit");

            if(GetCurrentWeight() > weightLimit)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void ShowInventory(ScreenTextInterface textInteface)
        {
            DebugLogger.Log("showing inventory");

            string textOutput = "";
            int line = 0;

            foreach(Item item in itemList)
            {
                textOutput += item.GetStats();
                textOutput += ", ";

                if(textOutput.Length > 20)
                {
                    textInteface.WriteTextAtLine(textOutput, line);
                    textOutput = "";
                    line++;
                }
            }

            if(line > 4)
            {
                DebugLogger.LogError("text output from inventory too long");
            }
            else
            {
                textInteface.WriteTextAtLine(textOutput, line);
            }
        }

        
        public bool AddItemToInventory(Item item)
        {
            DebugLogger.Log("Adding item to inventory");

            if(GetCurrentWeight() + item.GetWeight() < weightLimit)
            {
                DebugLogger.Log("Added item: " + item.GetItemName());

                itemList.Add(item);

                return true;
            }
            else
            {
                DebugLogger.LogError("can't add item to inventory, weight limit reached");
                return false;
            }
        }
        
        public List<Item> GetListOfItems()
        {
            return itemList;
        }
        
        public Item? GetItemFromInventory(string itemName)
        {
            foreach(Item item in itemList)
            {
                DebugLogger.Log("checking item: " + item.GetItemName());

                if(item.GetItemName() == itemName)
                {
                    return item;
                }
            }

            DebugLogger.LogError("Item not found in inventory");

            return null;  
        }
        
    }
}
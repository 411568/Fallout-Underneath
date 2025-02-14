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
            weightLimit = playerLevel * 2 + 10;
            itemList = new List<Item>();
        }

        public void UpdatePlayerLevel(int playerLevel)
        {
            DebugLogger.Log("Updating weight limit in player inventory");
    
            weightLimit = playerLevel * 2 + 10;
        }

        public int GetCurrentWeight()
        {
            int currentWeight = 0;
            foreach(Item item in itemList)
            {
                currentWeight += item.GetWeight();
            }

            return currentWeight;
        }

        public int GetCurrentWeightLimit()
        {
            return weightLimit;
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

            textInteface.ClearText();

            if(itemList.Count > 0)
            {
                int i = 0;
                foreach(Item item in itemList)
                {
                    string textOutput = i.ToString() + ". " + item.GetStats();

                    if(i > 4)
                    {
                        DebugLogger.LogError("Too many items in inventory");
                    }
                    else
                    {
                        textInteface.WriteTextAtLine(textOutput, i);
                        i++;
                    }
                }

                ConsoleKeyInfo key = Console.ReadKey(true);
                if (char.IsDigit(key.KeyChar))
                {
                    int selectedOption = int.Parse(key.KeyChar.ToString());

                    if(selectedOption >= 0 && selectedOption < i)
                    {
                        ShowDialogForItem(itemList[selectedOption], textInteface);
                    }
                }
            }
            else
            {
                textInteface.WriteTextAtLine("Inventory empty!", 0);
            }

            /*
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
            */
        }

        private void ShowDialogForItem(Item item, ScreenTextInterface textInteface)
        {
            string name = item.GetItemName();
            DebugLogger.Log($"Showing dialog window for item: {name}");

            textInteface.ClearText();

            textInteface.WriteTextAtLine("Chosen item: " + name, 0);
            textInteface.WriteTextAtLine("1. Use item", 1);
            textInteface.WriteTextAtLine("2. Remove item", 2);

            ConsoleKeyInfo key = Console.ReadKey(true);
            DebugLogger.Log($"Pressed button: {key.KeyChar}");

            bool result = false;

            switch(key.KeyChar)
            {
                case '1':
                    DebugLogger.Log("Using item...");    
                    result = item.UseItem();
                    break;
                case '2': 
                    if (itemList == null || item == null)
                    {
                        DebugLogger.LogError("item doesn't exist or the list is empty");
                        break;
                    }
                    bool removed = itemList.Remove(item);
                    if(removed)
                    {
                        DebugLogger.Log("Item removed");
                    }
                    else
                    {
                        DebugLogger.LogError("Item could not be removed");
                    }
                    break;
                default:
                    DebugLogger.Log("Wrong input in item choice");
                    break;
            }

            textInteface.ClearText();
            if(result == true)
            {
                textInteface.WriteTextAtLine("Action completed successfully!", 0);
            }
            else
            {
                textInteface.WriteTextAtLine("Cannot use this item!", 0);
            }
        }
        
        public bool AddItemToInventory(Item item, ScreenTextInterface textInterface)
        {
            DebugLogger.Log("Adding item to inventory");

            if(GetCurrentWeight() + item.GetWeight() <= weightLimit && itemList.Count < 5)
            {
                DebugLogger.Log("Added item: " + item.GetItemName());

                itemList.Add(item);

                return true;
            }
            else
            {
                DebugLogger.Log("Can't add item to inventory, weight limit reached");

                textInterface.ClearText();
                textInterface.WriteTextAtLine("You can't carry any more!", 0);
            
                if(GetCurrentWeight() + item.GetWeight() > weightLimit)
                {
                    textInterface.WriteTextAtLine("   You reached the current weight limit.", 1);
                }
                else
                {
                    textInterface.WriteTextAtLine("   You reached the current item count limit.", 1);
                }

                Console.ReadKey(true);

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
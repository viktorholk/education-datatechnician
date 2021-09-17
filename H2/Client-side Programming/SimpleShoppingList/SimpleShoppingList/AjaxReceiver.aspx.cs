using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
namespace SimpleShoppingList
{
    class ItemList
    {
        public string ListName;
        public List<Item> Items;

        public ItemList(string listName, List<Item> items)
        {
            this.ListName = listName;
            this.Items = items;
        }
    }
    class Item
    {
        public string Name;
        public int Price;
        
        public Item(string name, int price)
        {
            this.Name = name;
            this.Price = price;
        }
    }

    public partial class AjaxReceiver : System.Web.UI.Page
    {
        static readonly List<ItemList> ItemLists = new List<ItemList>()
        {
            new ItemList("Frugt og Grønt", new List<Item>(){
                new Item("Blender", 150),
                new Item("Drikkedunk", 2),
                new Item("Sports Brus", 15),
            }),
            new ItemList("delikatesse", new List<Item>()
            {
                new Item("FIsk", 20),
                new Item("Stor fisk", 25),
                new Item("Død laks", 12),
                new Item("Hugorm", 3000)
            })
        };

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string GetLists()
        {
            return JsonConvert.SerializeObject(ItemLists);
        }
    }
}
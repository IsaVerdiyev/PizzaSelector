using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PizzaSelector
{
    class Root
    {
        int maximumCount = 0;
        int currentCount = 0;
        List<Pizza> pizzaList = new List<Pizza>();

        string textPath = "c_medium.in";
        public void main()
        {
            string[] lines = File.ReadAllLines(textPath);
            maximumCount = Int32.Parse(lines[0].Split()[0]);
            String[] pizzas = lines[1].Split();
            for (int i = 0; i < pizzas.Length; i++)
            {
                pizzaList.Add(new Pizza { Id = i, Slices = Int32.Parse(pizzas[i]) });
            }
            //pizzaList.ForEach(item => Console.WriteLine(item.Slices));

            var selectedPizzas = getPizzaList();
            selectedPizzas = addRest(selectedPizzas);
            Console.WriteLine(selectedPizzas.Sum(item => item.Slices));
            WriteResult(selectedPizzas, textPath);
        }


        public List<Pizza> getPizzaList()
        {
            var selectedPizzas = new List<Pizza>();
            int i = pizzaList.Count - 1;
            while(currentCount + pizzaList[i].Slices <= maximumCount)
            {
                currentCount += pizzaList[i].Slices;
                selectedPizzas.Insert(0, pizzaList[i]);
                pizzaList.RemoveAt(i);
                i--;
            }
           
            return selectedPizzas;
        }

        public List<Pizza> addRest(List<Pizza> pizzas)
        {
            int index = 0;
            while (index != -1)
            {
                int difference = maximumCount - currentCount;
                index = binarySearchBySlices(pizzaList, 0, pizzaList.Count - 1, difference);
                if (index == -1)
                {
                    return pizzas;
                }
                currentCount += pizzaList[index].Slices;
                int foundIndex = binarySearchByIndex(pizzas, 0, pizzas.Count - 1, pizzaList[index].Id);
                pizzas.Insert(foundIndex, pizzaList[index]);
                pizzaList.RemoveAt(index);
                
            }
            return pizzas;
        }

         

        public void WriteResult(List<Pizza> pizzas, String outputFilePath)
        {
            using (StreamWriter sw = new StreamWriter(textPath.Substring(0, textPath.Length - 2) + "out", false))
            {
                sw.WriteLine(pizzas.Count);
                foreach(var pizza in pizzas)
                {
                    sw.Write(pizza.Id + " ");
                }
            }
        }

        public int binarySearchByIndex(List<Pizza> pizzas, int l, int r, int x)
        {
            int mid = l + (r - l) / 2; ;
            if (r >= l)
            {

                // If the element is present at the 
                // middle itself 
                if (pizzas[mid].Id == x)
                    return mid;

                // If element is smaller than mid, then 
                // it can only be present in left subarray 
                if (pizzas[mid].Id > x)
                    return binarySearchByIndex(pizzas, l, mid - 1, x);

                // Else the element can only be present 
                // in right subarray 
                return binarySearchByIndex(pizzas, mid + 1, r, x);
            }

            // We reach here when element is not present 
            // in array 
            
            return mid;

        }

        public int binarySearchBySlices(List<Pizza> pizzas, int l, int r, int x)
        {
            int mid = l + (r - l) / 2; ;
            if (r >= l)
            {

                // If the element is present at the 
                // middle itself 
                if (pizzas[mid].Slices == x)
                    return mid;

                // If element is smaller than mid, then 
                // it can only be present in left subarray 
                if (pizzas[mid].Slices > x)
                    return binarySearchBySlices(pizzas, l, mid - 1, x);

                // Else the element can only be present 
                // in right subarray 
                return binarySearchBySlices(pizzas, mid + 1, r, x);
            }

            // We reach here when element is not present 
            // in array 
            return mid - 1;

        }
    }
}

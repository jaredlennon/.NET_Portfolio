using LINQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ
{
    class Program
    {
        static void Main()
        {
            // PrintAllProducts();
            // PrintAllCustomers();
            // Exercise1();
            // Exercise2();
            // Exercise3();
            // Exercise4();
            // Exercise5();
            // Exercise6();
            // Exercise7();
            // Exercise8();
            // Exercise9();
            // Exercise10();
            // Exercise11();
            // Exercise12();
            // Exercise13();
            // Exercise14();
            // Exercise15();
            // Exercise16();
            // Exercise17();
            // Exercise18();
            // Exercise19();
            // Exercise20();
            // Exercise21();
            // Exercise22();
            // Exercise23();
            // Exercise24();
            // Exercise25();
            // Exercise26();
            // Exercise27();
            // Exercise28();
            // Exercise29();
            // Exercise30();
            // Exercise31();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        // Load and print all the product objects
        static void PrintAllProducts()
        {
            List<Product> products = DataLoader.LoadProducts();
            PrintProductInformation(products);
        }

        // Print a nicely formatted list of products
        // <param name="products">The collection of products to print</param>
        static void PrintProductInformation(IEnumerable<Product> products)
        {
            string line = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6}";
            Console.WriteLine(line, "ID", "Product Name", "Category", "Unit", "Stock");
            Console.WriteLine("==============================================================================");

            foreach (var product in products)
            {
                Console.WriteLine(line, product.ProductID, product.ProductName, product.Category,
                    product.UnitPrice, product.UnitsInStock);
            }

        }

        // Load and print all the customer objects and their orders
        static void PrintAllCustomers()
        {
            var customers = DataLoader.LoadCustomers();
            PrintCustomerInformation(customers);
        }


        // Print a nicely formated list of customers
        // <param name="customers">The collection of customer objects to print</param>
        static void PrintCustomerInformation(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine("==============================================================================");
                Console.WriteLine(customer.CompanyName);
                Console.WriteLine(customer.Address);
                Console.WriteLine("{0}, {1} {2} {3}", customer.City, customer.Region, customer.PostalCode, customer.Country);
                Console.WriteLine("p:{0} f:{1}", customer.Phone, customer.Fax);
                Console.WriteLine();
                Console.WriteLine("\tOrders");
                foreach (var order in customer.Orders)
                {
                    Console.WriteLine("\t{0} {1:MM-dd-yyyy} {2,10:c}", order.OrderID, order.OrderDate, order.Total);
                }
                Console.WriteLine("==============================================================================");
                Console.WriteLine();
            }
        }

        // Print all products that are out of stock.
        static void Exercise1()
        {
            var outOfStock = from p in DataLoader.LoadProducts()
                             where p.UnitsInStock == 0
                             select p;

            PrintProductInformation(outOfStock);
        }

        // Print all products that are in stock and cost more than 3.00 per unit.
        static void Exercise2()
        {
            var inStockGreaterThanThree = from p in DataLoader.LoadProducts()
                                          where p.UnitsInStock > 0 && p.UnitPrice > 3
                                          select p;

            PrintProductInformation(inStockGreaterThanThree);
        }

        // Print all customer and their order information for the Washington (WA) region.
        static void Exercise3()
        {
            var customersOrders = from c in DataLoader.LoadCustomers()
                                  where c.Region == "WA"
                                  select c;

            PrintCustomerInformation(customersOrders);
        }

        // Create and print an anonymous type with just the ProductName
        static void Exercise4()
        {

            var products = from p in DataLoader.LoadProducts()
                                    select new { p.ProductName };

            string lineFormat = "{0, -35}";
            Console.WriteLine(lineFormat, "Product Name");
            Console.WriteLine("-------------------------------------");

            foreach (var product in products)
            {
                Console.WriteLine(lineFormat, product.ProductName);
            }

        }

        // Create and print an anonymous type of all product information but increase the unit price by 25%
        static void Exercise5()
        {

            decimal priceIncrease = 1.25M;

            var productsPriceIncrease = from p in DataLoader.LoadProducts()
                                        select new { p.ProductID, p.ProductName, p.Category, UnitPrice = p.UnitPrice * priceIncrease, p.UnitsInStock };

            string lineFormat = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6}";
            Console.WriteLine(lineFormat, "ID", "Product Name", "Category", "Unit", "Stock");
            Console.WriteLine("==============================================================================");

            foreach (var product in productsPriceIncrease)
            {
                Console.WriteLine(lineFormat, product.ProductID, product.ProductName, product.Category, product.UnitPrice, product.UnitsInStock);
            }

        }

        // Create and print an anonymous type of only ProductName and Category with all the letters in upper case
        static void Exercise6()
        {
            var productsUpperCase = from p in DataLoader.LoadProducts()
                                    select new { ProductName = p.ProductName.ToUpper(), Category = p.Category.ToUpper() };

            string lineFormat = "{0,-40} {1,-20}";
            Console.WriteLine(lineFormat, "PRODUCT NAME", "PRODUCT CATEGORY");
            Console.WriteLine("==========================================================");

            foreach (var product in productsUpperCase)
            {
                Console.WriteLine(lineFormat, product.ProductName, product.Category);
            }
        }

        // Create and print an anonymous type of all Product information with an extra bool property ReOrder which should 
        // be set to true if the Units in Stock is less than 3. Hint: use a ternary expression
        static void Exercise7()
        {
            var allProductInfo = from p in DataLoader.LoadProducts()
                                 select new { p.ProductID, p.ProductName, p.Category, p.UnitPrice, p.UnitsInStock, ReOrder = (p.UnitsInStock < 3) ? true : false };

            string lineFormat = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6} {5,-10}";
            Console.WriteLine(lineFormat, "ID", "Product Name", "Category", "Unit", "Stock", "ReOrder");
            Console.WriteLine("=======================================================================================");


            foreach (var product in allProductInfo)
            {
                Console.WriteLine(lineFormat, product.ProductID, product.ProductName, product.Category, product.UnitPrice, product.UnitsInStock, product.ReOrder);
            }

        }

        // Create and print an anonymous type of all Product information with an extra decimal called 
        // StockValue which should be the product of unit price and units in stock
        static void Exercise8()
        {
            var allProductsPlusStockValue = from p in DataLoader.LoadProducts()
                                            select new { p.ProductID, p.ProductName, p.Category, p.UnitPrice, p.UnitsInStock, StockValue = Convert.ToDecimal(p.UnitPrice) * p.UnitsInStock};

            string lineFormat = "{0,-5} {1,-35} {2,-15} {3,6:c} {4,6} {5,12:c}";
            Console.WriteLine(lineFormat, "ID", "Product Name", "Category", "Unit", "Stock", "Stock Value");
            Console.WriteLine("=======================================================================================");


            foreach (var product in allProductsPlusStockValue)
            {
                Console.WriteLine(lineFormat, product.ProductID, product.ProductName, product.Category, product.UnitPrice, product.UnitsInStock, product.StockValue);
            }

        }

        // Print only the even numbers in NumbersA
        static void Exercise9()
        {
            /*
            // Non-LINQ approach
            // int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersA = DataLoader.NumbersA;

            for (int i = 0 ; i < numbersA.Length; i++)
            {
                if (numbersA[i] % 2 == 0)
                    Console.WriteLine(numbersA[i]);
            }            
            */

            int[] numbersA = DataLoader.NumbersA;

            var numbers = from n in numbersA
                          where n % 2 == 0
                          select n;

            foreach (var num in numbers)
            {
                Console.WriteLine(num);
            }

        }

        // Print only customers that have an order whose total is less than $500
        static void Exercise10()
        {
            var customers = DataLoader.LoadCustomers();
        
            var custOrders = (from c in customers
                             from o in c.Orders
                             where o.Total < 500
                             select c.CustomerID).Distinct();

            foreach (var cust in custOrders)
            {
                Console.WriteLine(cust);
            }

        }

        // Print only the first 3 odd numbers from NumbersC
        static void Exercise11()
        {
            /*
            // Non-LINQ Approach
            // int[] NumbersC = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            int[] NumbersC = DataLoader.NumbersC;

            int countOddNumbers = 0;

            for (int i=0; i < NumbersC.Length; i++)
            {
                
                if (countOddNumbers < 3)
                {
                    if (NumbersC[i] % 2 != 0)
                    {
                        Console.WriteLine(NumbersC[i]);
                        countOddNumbers++;
                    }

                }
                else break;
            }
            */

            int[] NumbersC = DataLoader.NumbersC;

            var numbers = (from n in NumbersC
                          where n % 2 != 0
                          select n).Take(3);
            
            foreach (var num in numbers)
            {
                Console.WriteLine(num);
            } 

        }

        // Print the numbers from NumbersB except the first 3
        static void Exercise12()
        {
            /*
            // Non-LINQ approach
            // int[] NumbersB = { 1, 3, 5, 7, 8 };
            int[] numbersB = DataLoader.NumbersB;

            for (int i=3; i < numbersB.Length; i++)
            {
                Console.WriteLine(numbersB[i]);
            }
            */

            int[] numbersB = DataLoader.NumbersB;

            var numbers = (from n in numbersB
                          select n).Skip(3);

            foreach (var num in numbers)
            {
                Console.WriteLine(num);
            }

        }

        // Print the Company Name and most recent order for each customer in Washington
        static void Exercise13()
        {          
            IEnumerable < Customer > customers = DataLoader.LoadCustomers();
            var result = customers
                .Where(p => p.Region == "WA")
                .Select(p => new
                {
                    CompanyName = p.CompanyName,
                    MostRecentOrder = p.Orders.OrderByDescending(c => c.OrderDate).FirstOrDefault()
                });

            foreach (var order in result)
            {
                Console.WriteLine(order.CompanyName);
                Console.WriteLine(order.MostRecentOrder.OrderDate);
                Console.WriteLine();
            }
            
        }

        // Print all the numbers in NumbersC until a number is >= 6
        static void Exercise14()
        {
            /*
            // Non-LINQ approach
            // int[] NumbersC = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            int[] NumbersC = DataLoader.NumbersC;

            for (int i=0; i < NumbersC.Length; i++)
            {
                if (NumbersC[i] >= 6)
                {
                    break;
                }
                else
                {
                    Console.WriteLine(NumbersC[i]);
                }
            }
            */
            
            var numbers = DataLoader.NumbersC.Select(p => p).TakeWhile(p => p < 6);

            foreach (var num in numbers)
            {
                Console.WriteLine(num);
            }
            
        }

        // Print all the numbers in NumbersC that come after the first number divisible by 3
        static void Exercise15()
        {
            /*
            // Non-LINQ Approach
            // int[] NumbersC = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            int[] NumbersC = DataLoader.NumbersC;

            int startPrintHere = 0;

            for (int i=0; i < NumbersC.Length; i++)
            {
                if (NumbersC[i] % 3 == 0)
                    startPrintHere++;

                if (startPrintHere > 1)
                    Console.WriteLine(NumbersC[i]);
            }
            */

            var numbers = DataLoader.NumbersC.Select(n => n).SkipWhile(n => n % 3 != 0);

            foreach (var num in numbers.Skip(1))
            {
                Console.WriteLine(num);
            }

        }

        // Print the products alphabetically by name
        static void Exercise16()
        {
            var productsAlpha = from p in DataLoader.LoadProducts()
                                orderby p.ProductName
                                select p;

            PrintProductInformation(productsAlpha);

        }

        // Print the products in descending order by units in stock
        static void Exercise17()
        {
            var productsDesc = from p in DataLoader.LoadProducts()
                                orderby p.UnitsInStock descending
                                select p;

            PrintProductInformation(productsDesc);
        }

        // Print the list of products ordered first by category, then by unit price, from highest to lowest.
        static void Exercise18()
        {
            var productsMultiSort = from p in DataLoader.LoadProducts()
                                    orderby p.Category, p.UnitPrice descending
                                    select p;

            PrintProductInformation(productsMultiSort);
        }

        // Print NumbersB in reverse order
        static void Exercise19()
        {
            /*
            // Non-LINQ Approach
            // int[] NumbersB = { 1, 3, 5, 7, 8 };
            int[] NumbersB = DataLoader.NumbersB;

            for (int i = 4; i >= 0; i--)
            {
                Console.WriteLine(NumbersB[i]);
            }
            */

            var numbers = (from n in DataLoader.NumbersB
                          select n).Reverse();

            foreach(var num in numbers)
            {
                Console.WriteLine(num);
            }

        }

        /// Group products by category, then print each category name and its products
        /// ex:
        /// 
        /// Beverages
        /// Tea
        /// Coffee
        /// 
        /// Sandwiches
        /// Turkey
        /// Ham
        static void Exercise20()
        {
            var productsGroup = from product in DataLoader.LoadProducts()
                                group product by product.Category into newgroup
                                orderby newgroup.Key
                                select newgroup;

            string lineFormat = "{0,-15}";

            foreach (var group in productsGroup)
            {
                Console.WriteLine("Category: {0}", group.Key);
                Console.WriteLine("--------------------------------");

                foreach(var product in group)
                {
                    Console.WriteLine(lineFormat, product.ProductName);
                }

                Console.WriteLine();
            }
        }

        /// Print all Customers with their orders by Year then Month
        /// ex:
        /// 
        /// Joe's Diner
        /// 2015
        ///     1 -  $500.00
        ///     3 -  $750.00
        /// 2016
        ///     2 - $1000.00
        static void Exercise21()
        {           
              var customers = DataLoader.LoadCustomers();
              foreach (var customer in customers)
              {
                  Console.WriteLine("======================================");
                  Console.WriteLine(customer.CompanyName);
                  foreach( var order in customer.Orders.GroupBy(o=>o.OrderDate.Year))
                  {
                      Console.WriteLine(order.Key);

                      foreach( var custorder in customer.Orders.Where(i => i.OrderDate.Year==order.Key).OrderBy(t=>t.OrderDate.Month).GroupBy(o=>o.OrderDate.Month)) // where order year = order.Key (order.Key is the year)
                      {
                        // Console.WriteLine("\t {0} - {1:c}", custorder.Key, custorder.Sum(o=> o.Total));
                        // foreach (var orderID in customer.Orders.GroupBy(o=>o.OrderID))

                        foreach (var orderID in customer.Orders.Where(i => i.OrderDate.Month==custorder.Key 
                                && i.OrderDate.Year==order.Key).GroupBy(o => o.OrderID))
                        {
                            Console.WriteLine("\t {0} - \t{1}: \t{2:c}", custorder.Key, orderID.Key, orderID.Sum(o=> o.Total));
                        }

                      }

                  }

                  Console.WriteLine("======================================");
                  Console.WriteLine();
              }
              
        }

        // Print the unique list of product categories
        static void Exercise22()
        {
            var uniqueCategories = DataLoader.LoadProducts().Select(p => p.Category).Distinct();

            string lineFormat = "{0,-15}";
            Console.WriteLine("Unique Product Categories: ");
            Console.WriteLine("-----------------------------");

            foreach (var p in uniqueCategories)
            {
                Console.WriteLine(lineFormat, p);
            }
        }

        // Write code to check to see if Product 789 exists
        static void Exercise23()
        {
            var productCheck = from p in DataLoader.LoadProducts()
                               select p.ProductID;

                if (productCheck.Contains(789))
                {
                    Console.WriteLine("Product 789 exists!");
                }
                else Console.WriteLine("Product 789 does NOT exist.");

        }

        // Print a list of categories that have at least one product out of stock
        static void Exercise24()
        {
            var categoriesWithoutStock = DataLoader.LoadProducts().Where(p => p.UnitsInStock == 0).Select(p => p.Category).Distinct();

            string lineFormat = "{0,-15}";
            Console.WriteLine("Categories with 1+ item(s) out of stock: ");
            Console.WriteLine("------------------------------------------");

            foreach(var p in categoriesWithoutStock)
            {
                Console.WriteLine(lineFormat, p);
            }

        }

        // Print a list of categories that have no products out of stock
        // In other words: print a list of the categories that have all products in stock
        static void Exercise25()
        {
            /*
            // Non-LINQ approach
            var discreteCategoryList = DataLoader.LoadProducts().GroupBy(p => p.Category);
                           
            foreach (var items in discreteCategoryList)
            {
                if (items.All(q => q.UnitsInStock != 0))
                {
                    Console.WriteLine(items.Key);
                }
            }
            */
            
            var discreteCategoryList = DataLoader.LoadProducts().GroupBy(p => p.Category);
            var secondDiscreteList = discreteCategoryList.Where(p => p.All(q => q.UnitsInStock != 0));

            foreach (var items in secondDiscreteList)
            {
                Console.WriteLine(items.Key);
            }
            
        }

        // Count the number of odd numbers in NumbersA
        static void Exercise26()
        {
            /*
            // Non-LINQ approach
            // int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersA = DataLoader.NumbersA;

            int countOdd = 0;

            for (int i = 0; i < numbersA.Length; i++)
            {
                if (numbersA[i] % 2 != 0)
                    countOdd++;
            }

            Console.WriteLine("There are {0} odd numbers in NumbersA.", countOdd);
            */

            var numbersA = from p in DataLoader.NumbersA
                             where p % 2 != 0
                             select p;

            int oddNumbersCount = numbersA.Count();

            Console.WriteLine("There are {0} odd numbers in NumbersA.", oddNumbersCount);
        }

        // Create and print an anonymous type containing CustomerId and the count of their orders
        static void Exercise27()
        {
            var results = from c in DataLoader.LoadCustomers()
                          select new
                          {
                              CustomerID = c.CustomerID,
                              OrderCount = c.Orders.Count()
                          };

            string lineFormat = "{0,-5} {1,-5}";
            Console.WriteLine(lineFormat, "ID", "Order Count");
            Console.WriteLine("===============================");

            foreach (var customer in results)
            {                
                Console.WriteLine(lineFormat, customer.CustomerID, customer.OrderCount);
            }

        }

        // Print a distinct list of product categories and the count of the products they contain
        static void Exercise28()
        {
            var distinct = from d in DataLoader.LoadProducts()
                           group d by d.Category into g
                           select new
                           {
                               ProductCategory = g.Key,
                               ProductCount = g.Count()
                           };

            string lineFormat = "{0,-15} {1,-5}";
            Console.WriteLine(lineFormat, "Category", "Product Count");
            Console.WriteLine("===============================");

            foreach (var product in distinct)
            {
                Console.WriteLine(lineFormat, product.ProductCategory, product.ProductCount);
            }

        }

        // Print a distinct list of product categories and the total units in stock
        static void Exercise29()
        {
            var distinctList = from d in DataLoader.LoadProducts()
                               group d by d.Category into g
                               select new
                                {
                                    ProductCategory = g.Key,
                                    TotalUnitsInStock = g.Sum(p => p.UnitsInStock)
                                };

            string lineFormat = "{0,-20} {1,-5}";
            Console.WriteLine(lineFormat, "Category", "Total Units In Stock");
            Console.WriteLine("=========================================");

            foreach (var product in distinctList)
            {
                Console.WriteLine(lineFormat, product.ProductCategory, product.TotalUnitsInStock);
            }
        }

        // Print a distinct list of product categories and the lowest priced product in that category
        static void Exercise30()
        {
            var results = from p in DataLoader.LoadProducts()
                            group p by p.Category into g
                            let leastExpensive = g.Aggregate((p1, p2) => (p1.UnitPrice < p2.UnitPrice) ? p1 : p2)
                            select new
                            {
                                Category = g.Key,
                                ProductName = leastExpensive.ProductName,
                                leastExpensivePrice = leastExpensive.UnitPrice
                            };

            string lineFormat = "{0,-20} {1,-30} {2,-5:c}";
            Console.WriteLine(lineFormat, "Category", "Product", "Price");
            Console.WriteLine("============================================================");

            foreach (var product in results)
            {
                Console.WriteLine(lineFormat, product.Category, product.ProductName, product.leastExpensivePrice);
            }
        }

        // Print the top 3 categories by the average unit price of their products
        static void Exercise31()
        {           
            var products = from p in DataLoader.LoadProducts()
                          select new
                          {
                              Category = p.Category,
                              Product = p.ProductName,
                              UnitPrice = p.UnitPrice
                          };
                          
            var results = from r in products
                          group r by r.Category into g
                          select new
                          {
                              Category = g.Key,
                              AvgUnitPrice = g.Average(p => p.UnitPrice)
                          };

            var toPrint = results.OrderByDescending(p => p.AvgUnitPrice).Take(3);

            string lineFormat = "{0,-15} {1,-5:c}";
            Console.WriteLine(lineFormat, "Category", "AvgUnitPrice");
            Console.WriteLine("===================================");

            foreach (var product in toPrint)
            {
                Console.WriteLine(lineFormat, product.Category, product.AvgUnitPrice);
            }

        }
    }
}

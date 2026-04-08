using L2O___D09;

namespace Task01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Product> products = ListGenerators.ProductList;
            List<Customer> customers = ListGenerators.CustomerList;

            #region Restriction Operators
            var productsOutOfStock = products.Where(p => p.UnitsInStock == 0);
            //Print(productsOutOfStock, "All Products that are out of stock");

            var productsInStockAndLessThanCost = products.Where(p => p.UnitsInStock > 0 && p.UnitPrice > 3.00m);
            //Print(productsInStockAndLessThanCost, "All products that are in stock and cost more than 3.00 per unit");

            string[] Arr = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var shortNamedDigits = Arr.Select((name, idx) => new { name, idx })
                                        .Where(d => d.name.Length < d.idx)
                                        .Select(d => d.name);
            //Print(shortNamedDigits, "Returns digits whose name is shorter than their value");
            #endregion

            #region Element Operators
            var firstProductOutOfStock = products.FirstOrDefault(p => p.UnitsInStock == 0);
            //Print(new List<Product>() { firstProductOutOfStock }, "First Product out of Stock");

            var productWithPriceThanPrice = products.FirstOrDefault(p => p.UnitPrice > 1000m);
            //Print(new List<Product>() { productWithPriceThanPrice }, "First product whose Price > 1000");

            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var secondNumberThanFive = numbers.Where(n => n > 5).Skip(1).First();
            //Print(new List<int>() { secondNumberThanFive }, "Second number greater than 5");
            #endregion

            #region Set Operators
            var uniqueCategory = products.Select(p => p.Category).Distinct();
            //Print(uniqueCategory, "Unique Category names from Product List");

            var uniqueFirstLetters = products.Select(p => p.ProductName[0])
                                                                .Union(customers.Select(c => c.CompanyName[0]));
            //Print(uniqueFirstLetters, "Uunique first letter from both product and customer names");

            var commonFirstLetters = products.Select(p => p.ProductName[0])
                                                                .Intersect(customers.Select(c => c.CompanyName[0]));
            //Print(commonFirstLetters, "Common first letter from both product and customer names");

            var productOnlyFirstLetters = products.Select(p => p.ProductName[0])
                                                                    .Except(customers.Select(c => c.CompanyName[0]));
            //Print(productOnlyFirstLetters, "First letters of product names that are not also first letters of customer names");

            var lastThreeCharacters = products.Select(p => p.ProductName.Length > 3 ? p.ProductName.Substring(p.ProductName.Length - 3) : p.ProductName)
                                                                .Concat(customers.Select(c => c.CompanyName.Length > 3 ? c.CompanyName.Substring(c.CompanyName.Length - 3) : c.CompanyName));
            //Print(lastThreeCharacters, "Last Three Characters in each names of all customers and products, including any duplicates");

            #endregion

            #region Aggregate Operators
            int[] aggreNumbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var countOddNumbers = aggreNumbers.Count(n => n % 2 == 1);
            //Print(new List<int>() { countOddNumbers }, "Count odd numbers in the array");

            var customerOrderCounts = customers.Select(c => new {c.CompanyName, OrderCount = c.Orders.Count()});
            //Print(customerOrderCounts, "List of customers and how many orders each has");

            var categoryProductCounts = products.GroupBy(p => p.Category)
                                                                    .Select(cg => new { Category = cg.Key, ProductCount = cg.Count() });
            //Print(categoryProductCounts, "List of categories and how many products each has");

            var total = aggreNumbers.Sum();
            //Print(new List<int>() { total }, "Total of the numbers in an array.");

            string[] wordsFromFile = File.ReadAllLines("../../../dictionary_english.txt");
            var totalCharacters = wordsFromFile.Sum(w => w.Length);
            //Print(new List<int>() { totalCharacters }, "Total number of characters of all words in dictionary_english.txt");

            var unitsInStockByCategory = products.GroupBy(p => p.Category)
                                                                    .Select(cg => new { Category = cg.Key, TotalUnits = cg.Sum(p => p.UnitsInStock) });
            //Print(unitsInStockByCategory, "Total units in stock for each product category");

            var shortestWordLength = wordsFromFile.Min(w => w.Length);
            //Print(new List<int>() { shortestWordLength }, "Length of the shortest word in dictionary_english.txt");

            var cheapestPriceByCategory = products.GroupBy(p => p.Category)
                                                                    .Select(cg => new { Category = cg.Key, CheapestPrice = cg.Min(p => p.UnitPrice) });
            //Print(cheapestPriceByCategory, "Cheapest price among each category's products");

            var cheapestProductsByCategory = from p in products
                                             group p by p.Category into cg
                                             let minPrice = cg.Min(p => p.UnitPrice)
                                             select new { Category = cg.Key, Products = cg.Where(p => p.UnitPrice == minPrice) };
            //Print(new List<char>() { '#' }, "Products with the cheapest price in each category (Use Let)");
            //foreach(var group in cheapestProductsByCategory)
            //    Print(group.Products, $"Less Expensive Products in Category: {group.Category}");

            var longestWordLength = wordsFromFile.Max(w => w.Length);
            //Print(new List<int>(){ longestWordLength }, "Longest word in dictionary_english.txt");

            var mostExpensivePriceByCategory = products.GroupBy(p => p.Category)
                                                                          .Select(cg => new { Category = cg.Key, MaxPrice = cg.Max(p => p.UnitPrice) });
            //Print(mostExpensivePriceByCategory, "Most expensive price among each category's products");

            var mostExpensiveProductsByCategory = from p in products
                                                  group p by p.Category into cg
                                                  let maxPrice = cg.Max(p => p.UnitPrice)
                                                  select new { Category = cg.Key, Products = cg.Where(p => p.UnitPrice == maxPrice) };
            //Print(new List<char>() { '#' }, "Products with the most expensive price in each category");
            //foreach (var group in mostExpensiveProductsByCategory)
            //    Print(group.Products, $"Most Expensive Products in Category: {group.Category}");


            double averageWordLength = wordsFromFile.Average(w => w.Length);
            //Print(new List<double>() { averageWordLength }, "Average length of the words in dictionary_english.txt");

            var averagePriceByCategory = products.GroupBy(p => p.Category)
                                                                      .Select(cg => new { Category = cg.Key, AveragePrice = cg.Average(p => p.UnitPrice) });

            //Print(averagePriceByCategory, "Average price of each category's products.");

            #endregion

            #region Ordering Operators
            var sortProductsByName = products.OrderBy(p => p.ProductName);
            //Print(sortProductsByName, "Sort the products by name");

            string[] caseInsensitiveArr = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            var caseInsensitiveSorted = caseInsensitiveArr.OrderBy(x => x, StringComparer.OrdinalIgnoreCase);
            //Print(caseInsensitiveSorted, "Custom comparer to do a case-insensitive sort of the words in an array");

            var sortProductsByStockDesc = products.OrderByDescending(p => p.UnitsInStock);
            //Print(sortProductsByStockDesc, "Sort the products by units in stock from highest to lowest");
                
            string[] numbersArr = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var sortedDigits = numbersArr.OrderBy(x => x.Length).ThenBy(x => x);
            //Print(sortedDigits, "Sort the digits, first by length and then alphabetically by the name itself");

            var sortedWords = caseInsensitiveArr.OrderBy(x => x.Length).ThenBy(x => x, StringComparer.OrdinalIgnoreCase);
            //Print(sortedWords, "Sort first by word length and then by a case-insensitive");

            var sortedByCategoryAndPrice = products.OrderBy(p => p.Category).ThenByDescending(p => p.UnitPrice);
            //Print(sortedByCategoryAndPrice, "Sort the products, first by category, and then by unit price, from highest to lowest");

            var sortedWordsDesc = caseInsensitiveArr.OrderBy(x => x.Length).ThenByDescending(x => x, StringComparer.OrdinalIgnoreCase);
            //Print(sortedWordsDesc, "Sort first by word length and then by a case-insensitive descending");

            var filteredReversed = numbersArr.Where(x => x[1] == 'i').Reverse();
            //Print(filteredReversed, "Digits whose second letter is 'i' that is reversed");
            #endregion

            #region Partitioning Operators
            var first3OrdersWA = customers.Where(c => c.Region == "WA").SelectMany(c => c.Orders).Take(3);
            //Print(first3OrdersWA, "First 3 orders from customers in Washington");

            var skipFirst2OrdersWA = customers.Where(c => c.Region == "WA").SelectMany(c => c.Orders).Skip(2);
            //Print(skipFirst2OrdersWA, "All but the first 2 orders from customers in Washington");

            var resultUntilLessPos = numbers.TakeWhile((num, idx) => num >= idx);
            //Print(resultUntilLessPos, "Elements from start until number < its position");

            var resultSkipWhileMod3 = numbers.SkipWhile(n => n % 3 != 0);
            //Print(resultSkipWhileMod3, "Elements starting from first number while divisible by 3");

            var resultFromSkipToEnd = numbers.SkipWhile((num, idx) => num >= idx);
            //Print(resultFromSkipToEnd, "Elements starting from the first element less than its position");

            #endregion

            #region Projection Operators
            var productNames = products.Select(p => p.ProductName);
            //Print(productNames, "Names of all products");

            var upperLowerWords = caseInsensitiveArr.Select(w => new { Upper = w.ToUpper(), Lower = w.ToLower() });
            //Print(upperLowerWords, "Uppercase and Lowercase versions");

            var productProjection = products.Select(p => new { p.ProductName, p.Category, Price = p.UnitPrice });
            //Print(productProjection, "Selected properties with renamed UnitPrice");

            var numberMatchPosition = numbers.Select((num, idx) => new { Number = num, InPlace = num == idx });
            //Print(numberMatchPosition, "Number: In-place?");

            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 }, numbersB = { 1, 3, 5, 7, 8 };
            var pairs = numbersA.SelectMany(a => numbersB.Where(b => a < b), (a, b) => new { a, b });
            //Print(pairs.Select(p => $"{p.a} is less than {p.b}"), "Pairs where a < b");

            var smallOrders = customers.SelectMany(c => c.Orders).Where(o => o.Total < 500m);
            //Print(smallOrders, "Orders with total < 500");

            var ordersFrom1998 = customers.SelectMany(c => c.Orders).Where(o => o.OrderDate.Year >= 1998);
            //Print(ordersFrom1998, "Orders from 1998 or later");
            #endregion

            #region Quantifiers

            var containsEi = wordsFromFile.Any(w => w.Contains("ei"));
            //Print(new List<bool> { containsEi }, "Any word contains 'ei'");

            var categoriesWithOutOfStock = products.GroupBy(p => p.Category).Where(g => g.Any(p => p.UnitsInStock == 0));
            //Print(categoriesWithOutOfStock.Select(g => g.Key), "Categories with at least one product out of stock");

            var categoriesAllInStock = products.GroupBy(p => p.Category).Where(g => g.All(p => p.UnitsInStock > 0));
            //Print(categoriesAllInStock.Select(g => g.Key), "Categories where all products are in stock");

            #endregion

            #region Grouping Operators

            int[] numbersGroup = Enumerable.Range(0, 30).ToArray();
            var remainderGroups = numbersGroup.GroupBy(n => n % 5).OrderBy(g => g.Key);
            //foreach (var group in remainderGroups)
            //{
            //    Console.WriteLine($"Numbers with remainder {group.Key} when divided by 5:");
            //    foreach (var n in group)
            //        Console.WriteLine(n);
            //    Console.WriteLine();
            //}

            var wordsByFirstLetter = wordsFromFile.Where(w => !string.IsNullOrWhiteSpace(w))
                                                    .GroupBy(w => char.ToUpper(w[0]))
                                                    .OrderBy(g => g.Key).SelectMany(x => x);
            //Print(wordsByFirstLetter, "Grouped words by first letter");

            string[] ArrGroup = { "from   ", " salt", " earn ", "  last   ", " near ", " form  " };
            var anagramGroups = ArrGroup.Select(w => w.Trim()).GroupBy(w => String.Concat(w.OrderBy(c => c)));
            //foreach (var group in anagramGroups)
            //{
            //    Console.WriteLine("...");
            //    foreach (var word in group)
            //        Console.WriteLine(word);
            //}

            #endregion

        }
        static void Print<T>(IEnumerable<T> collection, string title/*, Func<T, string> formatter*/)
        {
            Console.WriteLine($"\n\t\t===== {title} =====\n");

            if (collection is null || !collection.Any())
            {
                Console.WriteLine("No items found");
                return;
            }

            if (typeof(T) == typeof(char) && collection is IEnumerable<char> chars && chars.Count() == 1 && chars.First() == '#')
                return;

            foreach (var item in collection)
                Console.WriteLine(/*formatter*/(item));

            Console.WriteLine();
        }
    }
}

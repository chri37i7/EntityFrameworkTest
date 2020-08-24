using EFTest.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFTest.ConsoleApp
{
    public class Program
    {
        static NorthwindContext context = new NorthwindContext();

        public static void Main()
        {
            FindAmericanCustomersWithoutFax();
        }

        #region Select
        /// <summary>
        /// 1. Find alle produkter der ikke længere føres.
        /// </summary>
        public static void FindExpiredProducts()
        {
            // Find all expired products
            IQueryable<Product> expiredProducts = (
                from p in context.Products
                where p.Discontinued == true
                select p
            );

            // Output query result
            foreach(Product product in expiredProducts)
            {
                Console.WriteLine($"{product.ProductId} : {product.ProductName}");
            }
        }

        /// <summary>
        /// 2. Find alle leverandører fra Québec.
        /// </summary>
        public static void FindSuppliersFromQuebec()
        {
            // Find suppliers from Québec
            IQueryable<Supplier> suppliers = (
                from s in context.Suppliers
                where s.City == "Québec"
                select s
            );

            // Output query result
            foreach(Supplier supplier in suppliers)
            {
                Console.WriteLine($"{supplier.CompanyName} : {supplier.Country} : {supplier.City}");
            }
        }

        /// <summary>
        /// 3. Find alle leverandører fra Tyskland og Frankrig.
        /// </summary>
        public static void FindSuppliersFromGermanyAndFrance()
        {
            // Find all suppliers from Germany and France
            IQueryable<Supplier> suppliers = (
                from s in context.Suppliers
                where s.Country == "Germany" || s.Country == "France"
                select s
            );

            // Output result
            foreach(Supplier supplier in suppliers)
            {
                Console.WriteLine($"{supplier.CompanyName} : {supplier.Country} : {supplier.City}");
            }
        }

        /// <summary>
        /// 4. Find alle leverandører der ikke har en hjemmeside.
        /// </summary>
        public static void FindSuppliersWithoutWebsite()
        {
            // Find all suppliers without a website
            IQueryable<Supplier> suppliers = (
                from s in context.Suppliers
                where s.HomePage == null
                select s
            );

            // Output result
            foreach(Supplier supplier in suppliers)
            {
                Console.WriteLine($"{supplier.CompanyName} : {supplier.Country} : {supplier.City}");
            }
        }

        /// <summary>
        /// 5. Find alle leverandører fra europæsiske lande, der har en hjemmeside.
        /// </summary>
        public static void FindEuropenSuppliersWithwebsite()
        {
            // Find all european suppliers with a website
            IQueryable<Supplier> suppliers = (
                from s in context.Suppliers
                where s.Country == "Germany" || s.Country == "France" || s.Country == "UK"
                || s.Country == "Sweden" || s.Country == "Spain" || s.Country == "Italy"
                || s.Country == "Norway" || s.Country == "Denmark" || s.Country == "Netherlands"
                || s.Country == "Finland"
                && s.HomePage != null
                select s
            );

            // Output result
            foreach(Supplier supplier in suppliers)
            {
                Console.WriteLine($"{supplier.CompanyName} : {supplier.Country} : {supplier.City} : {supplier.HomePage}");
            }
        }

        /// <summary>
        /// 6. Find alle ansatte hvis fornavn begynder med M.
        /// </summary>
        public static void FindEmployeesWithFirstnamesThatStartsWithM()
        {
            // Find employees with firstnames that starts with m
            IQueryable<Employee> employees = (
                from e in context.Employees
                where e.FirstName.StartsWith("M")
                select e
            );

            // Output result
            foreach(Employee employee in employees)
            {
                Console.WriteLine($"{employee.EmployeeId} : {employee.FirstName} {employee.LastName}");
            }
        }

        /// <summary>
        /// 7. Find alle ansatte hvis efternavn slutter på an.
        /// </summary>
        public static void FindEmployeesWithLastnamesThatEndOnAn()
        {
            // Find employees with lastnames that end on an
            IQueryable<Employee> employees = (
                from e in context.Employees
                where e.LastName.Substring(e.LastName.Length - 2, 2) == "an"
                select e
            );

            // Output result
            foreach(Employee employee in employees)
            {
                Console.WriteLine($"{employee.EmployeeId} : {employee.FirstName} {employee.LastName}");
            }
        }

        /// <summary>
        /// 8. Find alle kvindelige ansatte der ikke er læger (benyt en OR).
        /// </summary>
        public static void FindFemaleEmployeesThatAreNotDoctors()
        {
            // Find employees with lastnames that end on an
            IQueryable<Employee> employees = (
                from e in context.Employees
                where e.TitleOfCourtesy == "Dr."
                select e
            );

            // Output result
            foreach(Employee employee in employees)
            {
                Console.WriteLine($"{employee.EmployeeId} : {employee.TitleOfCourtesy} {employee.FirstName} {employee.LastName}");
            }
        }

        /// <summary>
        /// 9. Find alle medarbejdere der er Sales Representative og kommer fra UK.
        /// </summary>
        public static void FindSalesRepresentativesFromTheUk()
        {
            // Find sales representatives from the uk
            IQueryable<Employee> employees = (
                from e in context.Employees
                where e.Title == "Sales Representative" && e.Country == "UK"
                select e
            );

            // Output result
            foreach(Employee employee in employees)
            {
                Console.WriteLine($"{employee.EmployeeId} : {employee.Title} {employee.FirstName} {employee.LastName} : {employee.Country}");
            }
        }

        /// <summary>
        /// 10. Find ud af hvor mange produkter der er.
        /// </summary>
        public static void FindTotalProductAmount()
        {
            // Get product amount
            int productAmount = context.Products.ToList().Count;

            // Output result
            Console.WriteLine($"Amount of products: {productAmount}");
        }

        /// <summary>
        /// 11. Find gennemsnitsprisen for alle produkter.
        /// </summary>
        public static void FindAverageProductPrice()
        {
            // Find the average product price
            decimal? averagePrice = context.Products.Sum(p => p.UnitPrice) / context.Products.Count<Product>();

            // Output result
            Console.WriteLine($"{averagePrice:c}");
        }

        /// <summary>
        /// 12. Find antal produkter med en enhedspris over 20,00. Sorter efter dyreste.
        /// </summary>
        public static void FindProductsWithUnitPriceOverTwentySortByHighestPrice()
        {
            // Find products with unit price over twenty sort by highest
            IOrderedQueryable<Product> products = (
                from p in context.Products
                where p.UnitPrice >= 20
                select p
            ).OrderByDescending(p => p.UnitPrice);

            // Output result
            foreach(Product product in products)
            {
                Console.WriteLine($"{product.ProductId} : {product.ProductName} : {product.UnitPrice}");
            }
        }

        /// <summary>
        /// 13. Find de produkter der ikke er flere af, sorter alfabetisk.
        /// </summary>
        public static void FindSoldOutProductsSortAlphabetically()
        {
            // Find sold out products sort alphabetically
            IOrderedQueryable<Product> soldOutProducts = (
                from p in context.Products
                where p.UnitsInStock == 0
                select p
            ).OrderBy(p => p.ProductName);

            // Output result
            foreach(Product product in soldOutProducts)
            {
                Console.WriteLine($"{product.ProductName} : {product.UnitPrice:c} : {product.UnitsInStock}");
            }
        }

        /// <summary>
        /// 14. Find alle de produkter der ikke er flere af, og som ikke er bestilt, men heller ikke udgået, sorter efter produktnavn, omvendt alfabetisk rækkefølge.
        /// </summary>
        public static void FindProductsOutOfStockWithNoOrders()
        {
            // Find products out of stock with no orders
            IOrderedQueryable<Product> products = (
                from p in context.Products
                where p.UnitsInStock == 0 && p.UnitsOnOrder == 0 && p.Discontinued == false
                select p
            ).OrderBy(p => p.ProductName);

            // Output result
            foreach(Product product in products)
            {
                Console.WriteLine($"{product.ProductName} : {product.UnitPrice:c} : {product.UnitsInStock}");
            }
        }

        /// <summary>
        /// 15. Find alle kunder der er enten er franske ejere eller britiske sælgere, sorter efter land, dernæst navn.
        /// </summary>
        public static void FindFrenchOwnersAndBritishSellers()
        {
            // Find french owners and british sellers
            IOrderedQueryable<Customer> customers = (
                from c in context.Customers
                where c.Country == "France" && c.ContactTitle == "Owner" || c.Country == "UK" && c.ContactTitle.Contains("Sales")
                select c
            ).OrderBy(c => c.Country)
            .ThenBy(c => c.ContactName);

            // Output result
            foreach(Customer customer in customers)
            {
                Console.WriteLine($"{customer.Country} : {customer.ContactName} : {customer.ContactTitle}");
            }
        }

        /// <summary>
        /// 16. Find alle nord-, mellem-, og sydamerikanske kunder der ikke har en fax, sorter alfabetisk
        /// </summary>
        public static void FindAmericanCustomersWithoutFax()
        {
            // Find american customers without fax
            IOrderedQueryable<Customer> customers = (
                from c in context.Customers
                where c.Fax == null && c.Country == "Mexico"
                || c.Fax == null && c.Country == "Argentina"
                || c.Fax == null && c.Country == "Brazil"
                || c.Fax == null && c.Country == "Venezuela"
                || c.Fax == null && c.Country == "USA"
                select c
            ).OrderBy(c => c.CompanyName);

            // Output result
            foreach(Customer customer in customers)
            {
                Console.WriteLine($"{customer.CompanyName} : {customer.ContactName}");
            }
        } 
        #endregion

    }
}
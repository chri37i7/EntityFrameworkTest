using EFTest.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFTest.ConsoleApp
{
    public class Program
    {
        static NorthwindContext context;

        public static void Main()
        {
            try
            {
                UpdateCustomerSimon();
            }
            catch(Exception)
            {
                throw;
            }
        }

        #region Select
        /// <summary>
        /// 1. Find alle produkter der ikke længere føres.
        /// </summary>
        public static void FindExpiredProducts()
        {
            try
            {
                using(context = new NorthwindContext())
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
            }
            catch(Exception ex)
            {
                throw new DataAccessException("Finding expired products failed. See innerException for details", ex);
            }
        }

        /// <summary>
        /// 2. Find alle leverandører fra Québec.
        /// </summary>
        public static void FindSuppliersFromQuebec()
        {
            try
            {
                using(context = new NorthwindContext())
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
            }
            catch(Exception ex)
            {
                throw new DataAccessException("Finding suppliers from Québec failed. See innerException for details.", ex);
            }
        }

        /// <summary>
        /// 3. Find alle leverandører fra Tyskland og Frankrig.
        /// </summary>
        public static void FindSuppliersFromGermanyAndFrance()
        {
            try
            {
                using(context = new NorthwindContext())
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
            }
            catch(Exception ex)
            {
                throw new DataAccessException("Finding suppliers from Germany and France failed. See innerException for details.", ex);
            }
        }

        /// <summary>
        /// 4. Find alle leverandører der ikke har en hjemmeside.
        /// </summary>
        public static void FindSuppliersWithoutWebsite()
        {
            try
            {
                using(context = new NorthwindContext())
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
            }
            catch(Exception ex)
            {
                throw new DataAccessException("Finding suppliers without a website failed. See innerException for details.", ex);
            }
        }

        /// <summary>
        /// 5. Find alle leverandører fra europæsiske lande, der har en hjemmeside.
        /// </summary>
        public static void FindEuropenSuppliersWithwebsite()
        {
            try
            {
                using(context = new NorthwindContext())
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
            }
            catch(Exception ex)
            {
                throw new DataAccessException("Finding european suppliers without a website failed. See innerException for details.", ex);
            }
        }

        /// <summary>
        /// 6. Find alle ansatte hvis fornavn begynder med M.
        /// </summary>
        public static void FindEmployeesWithFirstnamesThatStartsWithM()
        {
            try
            {
                using(context = new NorthwindContext())
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
            }
            catch(Exception ex)
            {
                throw new DataAccessException("Finding employees with firstnames that starts with M failed. See innerException for details.", ex);
            }
        }

        /// <summary>
        /// 7. Find alle ansatte hvis efternavn slutter på an.
        /// </summary>
        public static void FindEmployeesWithLastnamesThatEndOnAn()
        {
            try
            {
                using(context = new NorthwindContext())
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
            }
            catch(Exception ex)
            {
                throw new DataAccessException("Finding employees with lastnames that end on 'an' failed. See innerException for details.", ex);
            }
        }

        /// <summary>
        /// 8. Find alle kvindelige ansatte der ikke er læger (benyt en OR).
        /// </summary>
        public static void FindFemaleEmployeesThatAreNotDoctors()
        {
            try
            {
                using(context = new NorthwindContext())
                {
                    // Find employees with lastnames that end on an
                    IQueryable<Employee> employees = (
                        from e in context.Employees
                        where e.TitleOfCourtesy != "Dr."
                        select e
                    );

                    // Output result
                    foreach(Employee employee in employees)
                    {
                        Console.WriteLine($"{employee.EmployeeId} : {employee.TitleOfCourtesy} {employee.FirstName} {employee.LastName}");
                    }
                }
            }
            catch(Exception ex)
            {
                throw new DataAccessException("Finding female employees that are not doctors failed. See innerException for details.", ex);
            }
        }

        /// <summary>
        /// 9. Find alle medarbejdere der er Sales Representative og kommer fra UK.
        /// </summary>
        public static void FindSalesRepresentativesFromTheUk()
        {
            try
            {
                using(context = new NorthwindContext())
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
            }
            catch(Exception ex)
            {
                throw new DataAccessException("Finding sales representatives from the UK failed. See innerException for details.", ex);
            }
        }

        /// <summary>
        /// 10. Find ud af hvor mange produkter der er.
        /// </summary>
        public static void FindTotalProductAmount()
        {
            try
            {
                using(context = new NorthwindContext())
                {
                    // Get product amount
                    int productAmount = context.Products.ToList().Count;

                    // Output result
                    Console.WriteLine($"Amount of products: {productAmount}");
                }
            }
            catch(Exception ex)
            {
                throw new DataAccessException("Finding the total product amount failed. See innerException for details.", ex);
            }
        }

        /// <summary>
        /// 11. Find gennemsnitsprisen for alle produkter.
        /// </summary>
        public static void FindAverageProductPrice()
        {
            try
            {
                using(context = new NorthwindContext())
                {
                    // Find the average product price
                    decimal? averagePrice = context.Products.Sum(p => p.UnitPrice) / context.Products.Count<Product>();

                    // Output result
                    Console.WriteLine($"{averagePrice:c}");
                }
            }
            catch(Exception ex)
            {
                throw new DataAccessException("Finding the average product price failed. See innerException for details.", ex);
            }
        }

        /// <summary>
        /// 12. Find antal produkter med en enhedspris over 20,00. Sorter efter dyreste.
        /// </summary>
        public static void FindProductsWithUnitPriceOverTwentySortByHighestPrice()
        {
            try
            {
                using(context = new NorthwindContext())
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
            }
            catch(Exception ex)
            {
                throw new DataAccessException("Finding products with a unit price over twenty failed. See innerException for details.", ex);
            }
        }

        /// <summary>
        /// 13. Find de produkter der ikke er flere af, sorter alfabetisk.
        /// </summary>
        public static void FindSoldOutProductsSortAlphabetically()
        {
            try
            {
                using(context = new NorthwindContext())
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
            }
            catch(Exception ex)
            {
                throw new DataAccessException("Finding sold out products failed. See innerException for details.", ex);
            }
        }

        /// <summary>
        /// 14. Find alle de produkter der ikke er flere af, og som ikke er bestilt, men heller ikke udgået, sorter efter produktnavn, omvendt alfabetisk rækkefølge.
        /// </summary>
        public static void FindProductsOutOfStockWithNoOrders()
        {
            try
            {
                using(context = new NorthwindContext())
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
            }
            catch(Exception ex)
            {
                throw new DataAccessException("Finding products out of stock with no orders failed. See innerException for details.", ex);
            }
        }

        /// <summary>
        /// 15. Find alle kunder der er enten er franske ejere eller britiske sælgere, sorter efter land, dernæst navn.
        /// </summary>
        public static void FindFrenchOwnersAndBritishSellers()
        {
            try
            {
                using(context = new NorthwindContext())
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
            }
            catch(Exception ex)
            {
                throw new DataAccessException("Finding french owners and british sellers failed. See innerException for details.", ex);
            }
        }

        /// <summary>
        /// 16. Find alle nord-, mellem-, og sydamerikanske kunder der ikke har en fax, sorter alfabetisk
        /// </summary>
        public static void FindAmericanCustomersWithoutFax()
        {
            try
            {
                using(context = new NorthwindContext())
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
            }
            catch(Exception ex)
            {
                throw new DataAccessException("Finding american customers with no fax failed. See innerException for details.", ex);
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// 1. Opdater alle leverandørers fax til no fax number, hvis ikke der er et fax nummer. Gør det samme for alle kunder
        /// </summary>
        public static void UpdateFax()
        {
            try
            {
                using(context = new NorthwindContext())
                {
                    IQueryable<Supplier> suppliers = from s in context.Suppliers where s.Fax == null select s;

                    IQueryable<Customer> customers = from c in context.Customers where c.Fax == null select c;

                    foreach(Supplier supplier in suppliers)
                    {
                        supplier.Fax = "no fax number";
                    }

                    foreach(Customer customer in customers)
                    {
                        customer.Fax = "no fax number";
                    }

                    context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw new DataAccessException("Updating fax number failed. See inner exception for details.", ex);
            }
        }

        /// <summary>
        /// 2. Opdater genbestillingsmængden for alle ikke-ugåede produkter, hvis nuværende genbestillings-mængde er 0 og nuværende beholdning er mindre en 20, til 10.
        /// </summary>
        public static void UpdateReorderAmount()
        {
            try
            {
                using(context = new NorthwindContext())
                {
                    IQueryable<Product> products = from p in context.Products where p.Discontinued == false && p.ReorderLevel == 0 && p.UnitsInStock < 20 select p;

                    foreach(Product product in products)
                    {
                        product.UnitsOnOrder = 10;
                    }

                    context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw new DataAccessException("Updating reorder amount failed. See innerException for details.", ex);
            }
        }

        /// <summary>
        /// 3. Opdater alle spanske kunder med den korrekte region. Se spanske regioner på wikipedia og/eller google maps.
        /// </summary>
        public static void UpdateSpanishCustomers()
        {
            try
            {
                using(context = new NorthwindContext())
                {
                    IQueryable customers = from c in context.Customers where c.Country == "Spain" select c;

                    foreach(Customer customer in customers)
                    {
                        switch(customer.City)
                        {
                            case "Madrid":
                                customer.Region = "Madrid";
                                break;
                            case "Barcelona":
                                customer.Region = "Catalonien";
                                break;
                            case "Sevilla":
                                customer.Region = "Andalusia";
                                break;

                        }
                    }

                    context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw new DataAccessException("Updating spanish customers failed. See innerException for details.", ex);
            }
        }

        /// <summary>
        /// 4. Simons bistro har ændret navn til Simons Vaffelhus og flyttet til Strandvejen 65, 7100 Vejle. Foretag opdateringen.
        /// </summary>
        public static void UpdateCustomerSimon()
        {
            try
            {
                using(context = new NorthwindContext())
                {
                    Customer customer = (from c in context.Customers where c.CustomerId == "SIMOB" select c).FirstOrDefault();

                    if(customer != null)
                    {
                        customer.CompanyName = "Simons Vaffelhus";
                        customer.Address = "Strandvejen 65, 7100 Vejle";

                        context.SaveChanges();
                    }
                }
            }
            catch(Exception ex)
            {
                throw new DataAccessException("Updating customer failed. See innerException for details.", ex);
            }
        }

        /// <summary>
        /// 5. White Clover Markets er flyttet til 247 New Avenue, Chicago og har skifter nummer til 555-20159. Foretag opdateringen.
        /// </summary>
        public static void UpdateWhiteCloverMarket()
        {
            try
            {
                using(context = new NorthwindContext())
                {
                    Customer customer = (from c in context.Customers where c.CustomerId == "WHITC" select c).FirstOrDefault();

                    if(customer != null)
                    {
                        customer.City = "Chicago";
                        customer.Address = "247 New Avenue";
                        customer.Phone = "555-20159";

                        context.SaveChanges();
                    }
                }
            }
            catch(Exception ex)
            {
                throw new DataAccessException("Updating customer failed. See innerException for details.", ex);
            }
        }

        /// <summary>
        /// 6. Medarbejderen Janet er flyttet ind hos medarbejderen Andrew. Foretag opdateringen.
        /// </summary>
        public static void UpdateEmployee()
        {
            try
            {
                using(context = new NorthwindContext())
                {
                    Employee employee = (from e in context.Employees where e.EmployeeId == 3 select e).FirstOrDefault();

                    if(employee != null)
                    {
                        employee.City = "Tacoma";
                        employee.PostalCode = "98401";
                        employee.HomePhone = "(206) 555-9482";
                        employee.Address = "908 W. Capital Way";
                    }
                }
            }
            catch(Exception ex)
            {
                throw new DataAccessException("Updating employee Janet failed. See innerException for details.", ex);
            }
        }
        #endregion

        #region Insert
        /// <summary>
        /// 1. Northwind har ansat Kim Larsen CPR: 190583-7856, Violvej 45, Sønderborg, tlf 75835264, pr. 1. januar i år. IT support gav han tlf. ext. 0745. Der er ikke npoget billede af Kim.
        /// </summary>
        public static void InsertKimLarsen()
        {
            try
            {
                using(context = new NorthwindContext())
                {
                    Employee employee = new Employee()
                    {
                        EmployeeId = 0,
                        TitleOfCourtesy = "Mr.",
                        FirstName = "Kim",
                        LastName = "Larsen",
                        Address = "Violvej 45",
                        City = "Sønderborg",
                        PostalCode = "6400",
                        Country = "Denmark",
                        HireDate = DateTime.Now,
                        Title = "Sales Assistent",
                        HomePhone = "75835264",
                        BirthDate = new DateTime(1983, 05, 19),
                        Extension = "0745",
                    };

                    context.Employees.Add(employee);

                    context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw new DataAccessException("Inserting Kim Larsen failed. See innerException for details.", ex);
            }
        }

        /// <summary>
        /// 2. Northwind har hyret alle lærerne på din AspIT afdeling pr. næste 1. i måneden. 
        /// Disse medarbejdere skal INSERTes i det samme statement og ikke i hvert sit statement – altså et INSERT men mange VA-LUES-rækker. 
        /// Alle medarbejderne får AspITs hovednummer som tlf men hver sin ext. 
        /// Du skal vælge AspIT afdelingernes adresser som adresser til medarbejderne – som skal være forskellige.
        /// </summary>
        public static void RegisterEmployees()
        {
            try
            {
                using(context = new NorthwindContext())
                {
                    List<Employee> employees = new List<Employee>()
                    {
                        new Employee() {
                            EmployeeId = 0,
                            FirstName = "Mads",
                            LastName = "Rasmussen",
                            BirthDate = new DateTime(1991, 03, 14),
                            HireDate = DateTime.Now,
                            Title = "Pajeet",
                            TitleOfCourtesy = "Mr.",
                            HomePhone = "72 162 742",
                            Extension = "2836",
                            Address = "Strandvejen 1",
                            City = "Vejle",
                            PostalCode = "7120",
                            Country = "Denmark",
                        },
                        new Employee()
                        {
                            EmployeeId = 0,
                            FirstName = "Preben",
                            LastName = "Bisgaard",
                            BirthDate = new DateTime(1987, 06, 27),
                            HireDate = DateTime.Now,
                            Title = "Britbong",
                            TitleOfCourtesy = "Mr.",
                            HomePhone = "72 162 742",
                            Extension = "8375",
                            Address = "Holmen 23",
                            City = "Vejle",
                            PostalCode = "7100",
                            Country = "Denmark",
                        },
                    };

                    foreach(Employee employee in employees)
                    {
                        context.Employees.Add(employee);
                    }

                    context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw new DataAccessException("Inserting employees failed. See innerException for details.", ex);
            }
        }

        /// <summary>
        /// 3. Der er et nyt produkt på banen: SuperDuperBeer. Tilføj dette produkt med passende valg af værdier til kolonnerne.
        /// </summary>
        public static void RegisterProduct()
        {
            try
            {
                using(context = new NorthwindContext())
                {
                    Product product = new Product()
                    {
                        ProductId = 0,
                        ProductName = "SuperDuperBeer",
                        CategoryId = 1,
                        UnitsInStock = 45,
                        UnitPrice = 25,
                        ReorderLevel = 0,
                        Discontinued = false
                    };

                    context.Products.Add(product);

                    context.SaveChanges();
                }
            }
            catch(Exception ex)
            {

                throw new DataAccessException("Inserting SuperDuperBeer failed. See innerException for details.", ex);
            }
        }


        #endregion
    }
}
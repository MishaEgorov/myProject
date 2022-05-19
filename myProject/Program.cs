using System;
using System.Collections.Generic;
using System.Linq;

namespace myProject
{
    enum Catalog { apple, banana, tomato, pear };
    class Program
    {
        static void Main(string[] args)
        {
            List<Products> products = new List<Products>()
            {
                 new Fruit(1, Catalog.apple, 10, 11.5f, 100, 1),
                 new Fruit(2, Catalog.banana, 5, 6.1f, 50, 2),
                 new Fruit(3, Catalog.pear, 10, 10.2f, 156.5m, 2),
                 new Vegetables(4, Catalog.tomato, 6, 2, 15.1m, 3)
            };

            List<Supplier> suppliers = new List<Supplier>()
            {
                 new Supplier(1, "Магнит", "Тула"),
                 new Supplier(2, "Спар", "Москва"),
                 new Supplier(3, "Пятёрочка", "Тула"),
            };

            List<Products> GetProducts()
            {
                return products;
            }

            List<Supplier> GetSupplier()
            {
                return suppliers;
            }

            var result =
                from allProducts in GetProducts()
                join allSuppler in GetSupplier()
                on allProducts.supplerId equals allSuppler.id into productsSuplerGroup
                from subSuppler in productsSuplerGroup.DefaultIfEmpty()
                select new { productsName = allProducts.catalog, supplerName = subSuppler?.name };

            foreach (var curr in result)
                Console.WriteLine($"{curr.productsName} от поставщика {curr.supplerName}");

            SortList sortList = new SortList();

            Console.WriteLine("Сортировка по цене: ");
            products.Sort(sortList.ComparePrise);
            foreach (var curr in products) Console.WriteLine(curr.Print());

            Console.WriteLine("Сортировка по количеству: ");
            products.Sort(sortList.CompareAmount);
            foreach (var curr in products) Console.WriteLine(curr.Print());
        }
    }
    class Products
    {
        public int id;
        public Catalog catalog;
        public int amount;
        public decimal price;
        protected string str;
        public int supplerId;

        public Products(int id, Catalog catalog, int amount, decimal price, int supplerId)
        {
            this.id = id;
            this.catalog = catalog;
            this.amount = amount;
            this.price = price;
            this.supplerId = supplerId;
        }

        public Products() { }

        public virtual string Print()
        {
            str = "Название продукта:" + catalog +
                   " ID:" + id + " Количество товара:" + amount + " Цена:" + price;
            return str;
        }
    }
    class Fruit : Products
    {
        public float kg;

        public Fruit(int id, Catalog catalog, int amount, float kg, decimal price, int supplerId) : base(id, catalog, amount, price, supplerId)
        {
            this.kg = kg;
        }

        public override string Print()
        {
            base.Print();
            return str + " кг:" + kg;
        }
    }
    class Vegetables : Products
    {
        public int pcs;

        public Vegetables(int id, Catalog catalog, int amount, int pcs, decimal price, int supplerId) : base(id, catalog, amount, price, supplerId)
        {
            this.pcs = pcs;
        }

        public override string Print()
        {
            base.Print();
            return str + " шт:" + pcs;
        }
    }
    class SortList : IComparePrise<Products>, ICompareAmount<Products>
    {
        public int ComparePrise(Products obj1, Products obj2)
        {
            if (obj1.price > obj2.price) return 1;
            else if (obj1.price < obj2.price) return -1;
            return 0;
        }

        public int CompareAmount(Products obj1, Products obj2)
        {
            if (obj1.amount > obj2.amount) return 1;
            else if (obj1.amount < obj2.amount) return -1;
            return 0;
        }

    }
    class Supplier
    {
        public int id;
        public string name;
        public string city;

        public Supplier(int id, string name, string city)
        {
            this.id = id;
            this.name = name;
            this.city = city;
        }
    }
    public interface IComparePrise<in T>
    {
        int ComparePrise(T x, T y);
    }
    public interface ICompareAmount<in T>
    {
        int CompareAmount(T x, T y);
    }
}
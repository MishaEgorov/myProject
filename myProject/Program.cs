using System;
using System.Collections.Generic;

namespace myProject
{
    enum  Catalog {apple, banana, tomato, pear };
    class Program
    {
 
        static void Main(string[] args)
        {
            List<Products> products = new List<Products>();



            List<Supplier> suppliers = new List<Supplier>()
            {
                 new Supplier(1, "Магнит", "Тула"),
                 new Supplier(2, "Спар", "Москва"),
                 new Supplier(3, "Пятёрочка", "Тула"),
            };


            Vegetables tomato = new Vegetables(Catalog.tomato, 3, 6, 2, 15.1m);

            products.Add(apple);
            products.Add(banana);
            products.Add(tomato);
            products.Add(pear);

            foreach (Products pr in products)
            {
                Console.WriteLine(pr.Print());
            }

            Console.WriteLine();
            SortList sortList = new SortList();
            products.Sort(sortList.ComparePrise);

            foreach (Products pr in products)
            {           
                Console.WriteLine(pr.Print());
            }
            Console.WriteLine();
            products.Sort(sortList.CompareAmount);

            foreach (Products pr in products)
            {
                Console.WriteLine(pr.Print());
            }

        }
    }
    class Products
    {
        public Catalog catalog;
        public int id;
        public int amount;
        public decimal price;
        protected string str;
       
        public Products(Catalog catalog,int id, int amount,decimal price)
        {
            this.catalog = catalog;
            this.id = id;
            this.amount = amount;
            this.price = price;
        }
        public Products() { }

        public virtual string Print()
        {
            str = "Название продукта:" + catalog +
                            " ID:" + id + " Количество товара:" + amount + " Цена:"+price;
            return str;
        }
    }
    class Fruit:Products
    {
        public float kg;
        public Fruit(Catalog catalog, int id, int amount,float kg,decimal price) : base(catalog,id,amount,price)
        {
            this.kg = kg;
        }
        public override string Print()
        {
            base.Print();
            return str + " кг:" + kg;
        }
    }
    class Vegetables:Products
    {
        public int pcs;
        public Vegetables(Catalog catalog, int id, int amount, int pcs, decimal price) : base(catalog, id, amount,price)
        {
            this.pcs = pcs;
        }
        public override string Print()
        {
            base.Print();
            return str + " шт:" + pcs;
        }
    }
    class SortList : IComparePrise<Products> , ICompareAmount<Products>
    {
        public int ComparePrise(Products obj1,Products obj2)
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
    public interface IComparePrise <in T>
    {
        int ComparePrise(T x, T y);
    }
    public interface ICompareAmount <in T>
    {
        int CompareAmount(T x, T y);
    }
}

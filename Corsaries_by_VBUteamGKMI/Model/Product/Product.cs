using System;
using System.Collections.Generic;
using System.Text;

namespace Corsaries_by_VBUteamGKMI.Model.Products
{
    public enum Product_type { Rum, Silk, Water, Food, Leather, Wood, Tobacco, Coffee }
    public  class Product
    {
        public Product_type _product_Type; // тип товара
        public string _name; // названи продукта
        public int _weight; // вес продукта
        public int _price; // цена продукта
        public int _count =0; // доступное количество

        private Product()
        {

        }

        public Product(Product_type product_Type)
        {
            _product_Type = product_Type;
            switch (_product_Type)
            {
                case Product_type.Rum:
                    _name = "Ром";
                    _weight = 1;
                    _price = 20;
                    

                    break;
                case Product_type.Silk:
                    _name = "Шелк";
                    _weight = 2;
                    _price = 50;
                    

                    break;
                case Product_type.Water:
                    _name = "Вода";
                    _weight = 1;
                    _price = 5;
                    

                    break;
                case Product_type.Food:
                    _name = "Еда";
                    _weight = 1;
                    _price = 10;
                    

                    break;
                case Product_type.Leather:
                    _name = "Кожа";
                    _weight = 10;
                    _price = 30;
                    

                    break;
                case Product_type.Wood:
                    _name = "Дерево";
                    _weight = 20;
                    _price = 20;
                    

                    break;
                case Product_type.Tobacco:
                    _name = "Табак";
                    _weight = 3;
                    _price = 30;
                    

                    break;
                case Product_type.Coffee:
                    _name = "Коффе";
                    _weight = 7;
                    _price = 20;
                    

                    break;
            }
        }

        public override string ToString()
        {
            return $"{_product_Type},{_name},{_weight},{_price},{_count}";
        }

        public static Product FromString(string data)
        {
            Product product = new Product();
            product._product_Type = (Product_type)Enum.Parse(typeof(Product_type), data.Split(',')[0]);
            product._name = data.Split(',')[1];
            product._weight = int.Parse(data.Split(',')[2]);
            product._price = int.Parse(data.Split(',')[3]);
            product._count = int.Parse(data.Split(',')[4]);
            return product;
        }
    }
}
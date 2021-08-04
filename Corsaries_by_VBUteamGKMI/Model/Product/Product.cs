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
        public Product(Product_type product_Type)
        {
            _product_Type = product_Type;
            switch (_product_Type)
            {
                case Product_type.Rum:
                    _name = "Ром";
                    _weight = 5;
                    _price = 20;
                    

                    break;
                case Product_type.Silk:
                    _name = "Шёлк";
                    _weight = 2;
                    _price = 50;
                    

                    break;
                case Product_type.Water:
                    _name = "Вода";
                    _weight = 5;
                    _price = 5;
                    

                    break;
                case Product_type.Food:
                    _name = "Еда";
                    _weight = 7;
                    _price = 10;
                    

                    break;
                case Product_type.Leather:
                    _name = "Кожа";
                    _weight = 10;
                    _price = 30;
                    

                    break;
                case Product_type.Wood:
                    _name = "Дерево";
                    _weight = 25;
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
                    _price = 15;
                    

                    break;
            }
        }
    }
}
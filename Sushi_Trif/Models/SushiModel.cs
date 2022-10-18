using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sushi_Trif.Models
{
    public class SushiModel
    {
        public SushiModel(Sushi maska)
            {
            Id=maska.Id;
            Image=maska.Image;
            Name=maska.Name;
            Compound=maska.Compound;
            Price = (int)maska.Price;

            }
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }

        public string Compound { get; set; }
        public int Price { get; set; }
    }
}